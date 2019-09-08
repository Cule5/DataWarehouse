using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Service;
using Services.ETL.Command;

namespace Services.ETL.Handlers
{
    class ProcessEShopDataHandler: ICommandHandler<ProcessData>
    {
        private readonly IETLService _etlService = null;
        public ProcessEShopDataHandler(IETLService etlService)
        {
            _etlService = etlService;
        }
        public async Task HandleAsync(ProcessData command)
        {
            await _etlService.EShopDataAsync(command.File);
        }
    }
}
