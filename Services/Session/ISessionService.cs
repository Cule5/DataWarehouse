using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Services.ETL;

namespace Services.Session
{
    public interface ISessionService
    {
        Task AddToBufferAsync(transaction transaction);
        Task<List<transaction>> GetBufferAsync();
        Task ClearBuffer();
    }
}
