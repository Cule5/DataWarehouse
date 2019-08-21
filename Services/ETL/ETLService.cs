using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Services.ETL
{
    public class ETLService:IETLService
    {
        public async Task Process(IFormFile file)
        {
            throw new NotImplementedException();
        }
    }
}
