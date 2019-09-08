using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Service;
using Services.ETL.Command;

namespace Services.ETL.Handlers
{
    public class ProcessPhoneShopDataHandler: ICommandHandler<ProcessData>
    {
        private readonly IETLService _etlService = null;
        public ProcessPhoneShopDataHandler(IETLService etlService)
        {
            _etlService = etlService;
        }
        public async Task HandleAsync(ProcessData command)
        {
            await _etlService.PhoneShopDataAsync(command.File);
        }
    }
}
