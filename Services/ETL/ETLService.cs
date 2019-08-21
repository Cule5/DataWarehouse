using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Core.Domain.Product.Repositories;
using Core.Domain.Shop.Repositories;
using Core.Domain.Transaction.Repositories;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Services.Session;

namespace Services.ETL
{
    public class ETLService:IETLService
    {
        private readonly ISessionService _sessionService = null;
        private readonly IShopRepository _shopRepository = null;
        private readonly IProductRepository _productRepository = null;
        private readonly ITransactionRepository _transactionRepository=null;
        public ETLService(ISessionService sessionService,IShopRepository shopRepository,IProductRepository productRepository,ITransactionRepository transactionRepository)
        {
            _sessionService = sessionService;
            _shopRepository = shopRepository;
            _productRepository = productRepository;
            _transactionRepository = transactionRepository;
        }
        public async Task ProcessAsync(IFormFile file)
        {
            RawDataDTO rawDataDTO = null;
            using (var stream = file.OpenReadStream())
            {
                using (var streamReader = new StreamReader(stream))
                {
                    var line = streamReader.ReadLine();
                    rawDataDTO = line != null ? JsonConvert.DeserializeObject<RawDataDTO>(line) : null;
                }
            }

            if (rawDataDTO.PackagesLeft == 0)
            {
                await _sessionService.AddToBufferAsync(rawDataDTO);
                await ProcessAsync();
            }
            else
            {
                await _sessionService.AddToBufferAsync(rawDataDTO);
            }
            
        }

        private async Task ProcessAsync()
        {

        }


        
    }
}
