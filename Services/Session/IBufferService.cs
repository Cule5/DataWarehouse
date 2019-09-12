using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Domain.Common;
using Services.ETL;

namespace Services.Session
{
    public interface IBufferService
    {
        Task AddToBufferAsync(transaction transaction,EShopType shopType);
        Task<List<transaction>> GetBufferAsync(EShopType shopType);
        Task ClearBufferAsync(EShopType shopType);

       
    }
}
