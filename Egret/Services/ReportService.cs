using Egret.DataAccess;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egret.Services
{
    public class ReportService : BaseService
    {
        ILogger _logger;

        public ReportService(EgretContext context, ILogger logger)
            : base(context)
        {
            _logger = logger;
        }

        
    }
}
