using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Dispatcher;
using Services.ETL.Command;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher = null;
        public TransactionController(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }
        [HttpPost]
        [Route("StandardShop")]
        public async Task<IActionResult> StandardShop([FromForm]ProcessStandardShopData command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return Ok();
        }

        [HttpPost]
        [Route("EShop")]
        public async Task<IActionResult> EShop([FromForm]ProcessEShopData command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return Ok();
        }

        [HttpPost]
        [Route("PhoneShop")]
        public async Task<IActionResult> PhoneShop([FromForm]ProcessPhoneShopData command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return Ok();
        }

    }
}