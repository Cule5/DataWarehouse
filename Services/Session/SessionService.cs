using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Domain.Common;
using Microsoft.AspNetCore.Http;
using Services.ETL;

namespace Services.Session
{
    public class SessionService:ISessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor = null;
        
        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public Task AddToBufferAsync(transaction transaction,EShopType shopType)
        {
            return Task.Factory.StartNew(() =>
            {
                var key = "BufferedData" + shopType;
                var bufferedData = _httpContextAccessor.HttpContext.Session.GetObject<List<transaction>>(key);
                if(bufferedData != null)
                    bufferedData.Add(transaction);
                else
                {
                    bufferedData = new List<transaction>();
                    bufferedData.Add(transaction);
                }
                _httpContextAccessor.HttpContext.Session.AddObject(key, bufferedData);
            });
            
        }

        public Task<List<transaction>> GetBufferAsync(EShopType shopType)
        {
            var key= "BufferedData" + shopType;
            return Task.Factory.StartNew(() => _httpContextAccessor.HttpContext.Session.GetObject<List<transaction>>(key));
        }

        public  Task ClearBufferAsync(EShopType shopType)
        {
            var key = "BufferedData" + shopType;
            return Task.Factory.StartNew(()=>_httpContextAccessor.HttpContext.Session.Remove(key));
        }
    }
}
