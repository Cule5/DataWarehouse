using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Core.Domain.Common;
using Core.Domain.Product;
using Core.Domain.Product.Repositories;
using Core.Domain.Shop;
using Core.Domain.Shop.Repositories;
using Core.Domain.Transaction;
using Core.Domain.Transaction.Repositories;
using Core.Domain.TransactionProduct;
using Core.Domain.TransactionProduct.Repositories;
using Core.Domain.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Polenter.Serialization;
using Services.Session;

namespace Services.ETL
{
    public class ETLService:IETLService
    {
        private readonly ISessionService _sessionService = null;
        private readonly IShopRepository _shopRepository = null;
        private readonly IProductRepository _productRepository = null;
        private readonly ITransactionRepository _transactionRepository=null;
        private readonly ITransactionProductRepository _transactionProductRepository = null;
        private readonly IUnitOfWork _unitOfWork = null;
        private readonly ManualResetEventSlim _manualResetEventSlim=null;
        public ETLService(ISessionService sessionService,
            IShopRepository shopRepository,
            IProductRepository productRepository,
            ITransactionRepository transactionRepository,
            ITransactionProductRepository transactionProductRepository,
            IUnitOfWork unitOfWork)
        {
            _sessionService = sessionService;
            _shopRepository = shopRepository;
            _productRepository = productRepository;
            _transactionRepository = transactionRepository;
            _transactionProductRepository = transactionProductRepository;
            _unitOfWork = unitOfWork;
            _manualResetEventSlim=new ManualResetEventSlim(true);
        }
        public async Task StandardShopDataAsync(IFormFile file)
        {
            var transaction = await ParseFileAsync(file);
            _manualResetEventSlim.Wait();
            await _sessionService.AddToBufferAsync(transaction,EShopType.StandardShop);
            if (transaction.MessagesLeft == 0)
            {
                _manualResetEventSlim.Reset();
                await this.ProcessAsync(EShopType.StandardShop);
            }
                
        }

        public async Task EShopDataAsync(IFormFile file)
        {
            var transaction = await ParseFileAsync(file);
            await _sessionService.AddToBufferAsync(transaction,EShopType.StandardShop);
            if (transaction.MessagesLeft == 0)
                await this.ProcessAsync(EShopType.EShop);
        }

        public async Task PhoneShopDataAsync(IFormFile file)
        {
            var transaction = await ParseFileAsync(file);
            await _sessionService.AddToBufferAsync(transaction,EShopType.PhoneShop);
            if (transaction.MessagesLeft == 0)
                await this.ProcessAsync(EShopType.PhoneShop);
        }

        private  Task<transaction> ParseFileAsync(IFormFile file)
        {
            return Task.Factory.StartNew(() =>
            {
                var extension = Path.GetExtension(file.FileName);
                transaction transaction = null;

                using (var stream = file.OpenReadStream())
                {
                    using (var streamReader = new StreamReader(stream))
                    {
                        var line = streamReader.ReadLine();
                        if (extension.Equals(".JSON"))
                            transaction = line != null ? JsonConvert.DeserializeObject<transaction>(line) : null;
                        else if (extension.Equals(".xml"))
                        {
                            var doc = XDocument.Parse(line);

                            bool success = bool.Parse(doc.Root.Value);

                            XmlSerializer serializer = new XmlSerializer(typeof(transaction));
                            // serializer.Deserialize(doc);
                            using (TextReader reader = new StringReader(line))
                            {
                                transaction result = (transaction)serializer.Deserialize(stream);
                            }
                        }
                    }
                }
                return transaction;
            });
        }

        private async Task ProcessAsync(EShopType shopType)
        {
            var bufferedData = await _sessionService.GetBufferAsync(shopType);
            await _sessionService.ClearBufferAsync(shopType);
            _manualResetEventSlim.Set();
            if(bufferedData.Count==0)
                return;
            Transaction transaction=new Transaction()
            {
                City = bufferedData[0].ClientCity,
                Date = bufferedData[0].TransactionDateTime,
                PaymentType = bufferedData[0].PaymentType,
                PostCode = bufferedData[0].ClientPostCode
            };
            foreach (var rawData in bufferedData)
            {
                var shop = new Shop()
                {
                    City = rawData.ShopCity,
                    PostalCode = rawData.ShopPostCode,
                    Type = rawData.ShopType,
                    Name = rawData.ShopName
                };
                var product = new Product()
                {
                    Price = rawData.Price,
                    Name = rawData.Product,
                    Quantity = rawData.Quantity
                };
                var resultProduct = await _productRepository.FindAsync(product) ?? product;
                var resultShop = await _shopRepository.FindAsync(shop)??shop;

                var transactionProduct = new TransactionProduct()
                {
                    Product = resultProduct,
                    Transaction = transaction
                };
                resultProduct.TransactionProducts.Add(transactionProduct);
                resultShop.Transactions.Add(transaction);
                transaction.Shop = resultShop;
                transaction.TransactionProducts.Add(transactionProduct);
               
                await _transactionRepository.AddAsync(transaction);
                await _shopRepository.AddAsync(resultShop);
                await _transactionProductRepository.AddAsync(transactionProduct);
                await _productRepository.AddAsync(resultProduct);
            }

            
            try
            {
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {

            }
            
        }
        
    }
}
