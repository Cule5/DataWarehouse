using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Services.ETL
{
    public interface IETLService
    {
        Task ProcessAsync(IFormFile file);
    }
}
