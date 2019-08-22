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
        public  Task AddToBufferAsync(RawDataDTO rawDataDto)
        {
            return Task.Factory.StartNew(() =>
            {
                var bufferedData = _httpContextAccessor.HttpContext.Session.GetObject<List<RawDataDTO>>(key);
                if(bufferedData != null)
                    bufferedData.Add(rawDataDto);
                else
                {
                    bufferedData = new List<RawDataDTO>();
                    bufferedData.Add(rawDataDto);
                }
                _httpContextAccessor.HttpContext.Session.AddObject(key, bufferedData);
            });
            
        }

        public Task<List<RawDataDTO>> GetBufferAsync()
        {
            return Task.Factory.StartNew(() => _httpContextAccessor.HttpContext.Session.GetObject<List<RawDataDTO>>(key));
        }
    }
}
