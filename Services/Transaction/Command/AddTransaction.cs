using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Service.Transaction.Command
{
    public class AddTransaction:ICommand
    {
        public IFormFile File { get; set; }
    }
}
