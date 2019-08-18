using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Transaction.Command;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        public TransactionController()
        {

        }
        [HttpPost]
        [Route("StandardShop")]
        public async Task<IActionResult> StandardShop([FromBody]AddTransaction command)
        {

            return Ok();
        }

        [HttpPost]
        [Route("EShop")]
        public async Task<IActionResult> EShop([FromBody]AddTransaction command)
        {

            return Ok();
        }

        [HttpPost]
        [Route("PhoneShop")]
        public async Task<IActionResult> PhoneShop([FromBody]AddTransaction command)
        {
            return Ok();
        }
    }
}