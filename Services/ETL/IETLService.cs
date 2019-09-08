using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Services.ETL
{
    public interface IETLService
    {
        Task StandardShopDataAsync(IFormFile file);
        Task EShopDataAsync(IFormFile file);
        Task PhoneShopDataAsync(IFormFile file);
    }
}
