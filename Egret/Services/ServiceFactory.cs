using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egret.Services
{
    public class ServiceFactory
    {
        public IItemService ItemService { get; private set; }
        public IConsumptionEventService ConsumptionEventService { get; private set; }
        public IReportService ReportService { get; private set; }
        public ISelectListFactoryService SelectListFactoryService { get; private set; }
        public ILogger Logger { get; private set; }

        public ServiceFactory(
            IItemService itemService,
            IConsumptionEventService consumptionEventService,
            IReportService reportService,
            ISelectListFactoryService selectListFactoryService,
            ILogger logger
        )
        {
            ItemService = itemService;
            ConsumptionEventService = consumptionEventService;
            ReportService = reportService;
            SelectListFactoryService = selectListFactoryService;
            Logger = logger;
        }
    }
}
