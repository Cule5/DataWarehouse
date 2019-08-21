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

        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task AddToBufferAsync(RawDataDTO rawDataDto)
        {
            var currentBufferedDat = _httpContextAccessor.HttpContext.Session.GetObject<List<RawDataDTO>>("BufferedData");
            currentBufferedDat.Add(rawDataDto);
            _httpContextAccessor.HttpContext.Session.AddObject("BufferedData",currentBufferedDat);
        }
    }
}
