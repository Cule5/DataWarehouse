using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
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
        public ETLService(ISessionService sessionService,IShopRepository shopRepository,IProductRepository productRepository,ITransactionRepository transactionRepository,ITransactionProductRepository transactionProductRepository,IUnitOfWork unitOfWork)
        {
            _sessionService = sessionService;
            _shopRepository = shopRepository;
            _productRepository = productRepository;
            _transactionRepository = transactionRepository;
            _transactionProductRepository = transactionProductRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task StandardShopDataAsync(IFormFile file)
        {
           
        }

        public Task EShopDataAsync(IFormFile file)
        {
            throw new NotImplementedException();
        }

        public Task PhoneShopDataAsync(IFormFile file)
        {
            throw new NotImplementedException();
        }

        private async Task<transaction> ParseFileAsync(IFormFile file)
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
            
        }

        private async Task ProcessAsync()
        {
            var bufferedData = await _sessionService.GetBufferAsync();
            
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
                
                var transaction = new Transaction()
                {
                    City = rawData.ClientCity,
                    Date = rawData.TransactionDateTime,
                    PaymentType = rawData.PaymentType,
                    PostalCode = rawData.ClientPostCode
                };


                var resultProduct = await _productRepository.FindAsync(product) ?? product;
                var resultShop = await _shopRepository.FindAsync(shop)??shop;
               
                var transactionProduct = new TransactionProduct()
                {
                    Product = product,
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

            await _sessionService.ClearBuffer();
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
