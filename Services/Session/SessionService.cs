using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Services.ETL;

namespace Services.Session
{
    public class SessionService:ISessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor = null;
        private readonly string key = "BufferedData";
        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public  Task AddToBufferAsync(transaction transaction)
        {
            return Task.Factory.StartNew(() =>
            {
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

        public Task<List<transaction>> GetBufferAsync()
        {
            return Task.Factory.StartNew(() => _httpContextAccessor.HttpContext.Session.GetObject<List<transaction>>(key));
        }

        public  Task ClearBuffer()
        {
            return Task.Factory.StartNew(()=>_httpContextAccessor.HttpContext.Session.Clear());
        }
    }
}
