using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Services.ETL;

namespace Services.Session
{
    public interface ISessionService
    {
        Task AddToBufferAsync(RawDataDTO rawDataDto);
    }
}
