using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Service;

namespace Services.ETL.Command
{
    public class ProcessStandardShopData:ICommand
    {
        public IFormFile File { get; set; }
    }
}
