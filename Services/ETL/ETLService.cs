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
using Core.Domain.Product.Factories;
using Core.Domain.Product.Repositories;
using Core.Domain.Shop;
using Core.Domain.Shop.Factories;
using Core.Domain.Shop.Repositories;
using Core.Domain.Transaction;
using Core.Domain.Transaction.Factories;
using Core.Domain.Transaction.Repositories;
using Core.Domain.TransactionProduct;
using Core.Domain.TransactionProduct.Factories;
using Core.Domain.TransactionProduct.Repositories;
using Core.Domain.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Polenter.Serialization;
using Services.Buffer;

namespace Services.ETL
{
    public class ETLService:IETLService
    {
        private readonly IBufferService _bufferService = null;
        private readonly IShopRepository _shopRepository = null;
        private readonly IProductRepository _productRepository = null;
        private readonly ITransactionRepository _transactionRepository=null;
        private readonly ITransactionProductRepository _transactionProductRepository = null;
        private readonly IUnitOfWork _unitOfWork = null;
        private readonly IProductFactory _productFactory = null;
        private readonly IShopFactory _shopFactory = null;
        private readonly ITransactionFactory _transactionFactory = null;
        private readonly ITransactionProductFactory _transactionProductFactory = null;
        public ETLService(IBufferService bufferService,
            IShopRepository shopRepository,
            IProductRepository productRepository,
            ITransactionRepository transactionRepository,
            ITransactionProductRepository transactionProductRepository,
            IUnitOfWork unitOfWork,
            IProductFactory productFactory,
            IShopFactory shopFactory,
            ITransactionFactory transactionFactory,
            ITransactionProductFactory transactionProductFactory)
        {
            _bufferService = bufferService;
            _shopRepository = shopRepository;
            _productRepository = productRepository;
            _transactionRepository = transactionRepository;
            _transactionProductRepository = transactionProductRepository;
            _unitOfWork = unitOfWork;
            _productFactory = productFactory;
            _shopFactory = shopFactory;
            _transactionFactory = transactionFactory;
            _transactionProductFactory = transactionProductFactory;
        }
        public async Task StandardShopDataAsync(IFormFile file)
        {
            var transaction = await ParseFileAsync(file);
            await _bufferService.AddToBufferAsync(transaction,EShopType.StationaryShop);
            if (transaction.MessagesLeft == 0)
                await this.ProcessAsync(EShopType.StationaryShop);
            
                
        }

        public async Task EShopDataAsync(IFormFile file)
        {
            var transaction = await ParseFileAsync(file);
            await _bufferService.AddToBufferAsync(transaction,EShopType.EShop);
            if (transaction.MessagesLeft == 0)
                await this.ProcessAsync(EShopType.EShop);
        }

        public async Task PhoneShopDataAsync(IFormFile file)
        {
            var transaction = await ParseFileAsync(file);
            await _bufferService.AddToBufferAsync(transaction,EShopType.TeleShop);
            if (transaction.MessagesLeft == 0)
                await this.ProcessAsync(EShopType.TeleShop);
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
                            XmlSerializer serializer = new XmlSerializer(typeof(transaction));
                            transaction = (transaction)serializer.Deserialize(streamReader);
                        }
                    }
                }
                return transaction;
            });
        }

        private async Task ProcessAsync(EShopType shopType)
        {
            var bufferedData = await _bufferService.GetBufferAsync(shopType);
            await _bufferService.ClearBufferAsync(shopType);
            if(bufferedData.Count==0)
                return;
            var transaction = await _transactionFactory.CreateAsync(bufferedData[0].ClientCity, 
                bufferedData[0].TransactionDateTime, bufferedData[0].PaymentType, bufferedData[0].ClientPostCode);

            foreach (var rawData in bufferedData)
            {
                var shop = await _shopFactory.CreateAsync(rawData.ShopName, rawData.ShopType, rawData.ShopPostCode, rawData.ShopCity);
                var product = await _productFactory.CreateAsync(rawData.Price, rawData.Product, rawData.Quantity);
                shop.Transactions.Add(transaction);
                var transactionProduct = await _transactionProductFactory.CreateAsync(product, transaction);
                transaction.Shop = shop;
                transaction.TransactionProducts.Add(transactionProduct);
                product.TransactionProducts.Add(transactionProduct);

                await _shopRepository.AddAsync(shop);
                await _productRepository.AddAsync(product);
                await _transactionRepository.AddAsync(transaction);
                await _transactionProductRepository.AddAsync(transactionProduct);
            }
            await _unitOfWork.SaveAsync();





        }
        
    }
}
