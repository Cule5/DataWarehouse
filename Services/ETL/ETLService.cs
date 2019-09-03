using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Core.Domain.Product;
using Core.Domain.Product.Repositories;
using Core.Domain.Shop;
using Core.Domain.Shop.Repositories;
using Core.Domain.Transaction;
using Core.Domain.Transaction.Repositories;
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
        private readonly IUnitOfWork _unitOfWork = null;
        public ETLService(ISessionService sessionService,IShopRepository shopRepository,IProductRepository productRepository,ITransactionRepository transactionRepository,IUnitOfWork unitOfWork)
        {
            _sessionService = sessionService;
            _shopRepository = shopRepository;
            _productRepository = productRepository;
            _transactionRepository = transactionRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task ProcessAsync(IFormFile file)
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
                    else if(extension.Equals(".xml"))
                    {

                        XmlSerializer serializer = new XmlSerializer(typeof(transaction));
                        using (TextReader reader = new StringReader(line))
                        {
                            transaction result = (transaction)serializer.Deserialize(reader);
                        }
                    }
                        
                        
                    
                }
            }
            if (transaction!=null)
            {
                if (transaction.MessagesLeft == 0)
                {
                    await _sessionService.AddToBufferAsync(transaction);
                    await ProcessAsync();
                }
                else
                    await _sessionService.AddToBufferAsync(transaction);
            }
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
                    Type = rawData.ShopType
                };
                var transaction = new Transaction();
                var resultShop = await _shopRepository.FindAsync(shop) ?? shop;

                resultShop.Transactions.Add(transaction);
                transaction.Shop = resultShop;
               
                await _transactionRepository.AddAsync(transaction);
                await _shopRepository.AddAsync(resultShop);


            }

            await _sessionService.ClearBuffer();
            await _unitOfWork.SaveAsync();
        }
        
    }
}
