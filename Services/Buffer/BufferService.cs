using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Domain.Common;
using Microsoft.Extensions.Caching.Memory;
using Services.ETL;
using Services.ETL.DTO;

namespace Services.Buffer
{
    public class BufferService:IBufferService
    {
        private readonly IMemoryCache _memoryCache=null;
        
        public BufferService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            
        }
        
        public Task AddToBufferAsync(transaction transaction,EShopType shopType)
        {
            return Task.Factory.StartNew(() =>
            {
                var key = "BufferedData" + shopType;
                if(!_memoryCache.TryGetValue(key,out List<transaction> currentBuffer))
                {
                    var newList = new List<transaction>();
                    newList.Add(transaction);
                    _memoryCache.Set(key, newList);
                }
                else
                {
                    currentBuffer.Add(transaction);
                    _memoryCache.Set(key,currentBuffer,TimeSpan.FromMinutes(5));
                }
            });
        }

        public Task<List<transaction>> GetBufferAsync(EShopType shopType)
        {
            var key = "BufferedData"+shopType;
            return Task.Factory.StartNew<List<transaction>>(() => _memoryCache.Get<List<transaction>>(key));
        }

        public  Task ClearBufferAsync(EShopType shopType)
        {
            var key = "BufferedData" + shopType;
            
            return Task.Factory.StartNew(() =>
            {
                _memoryCache.Remove(key);
            });
        }
    }
}
