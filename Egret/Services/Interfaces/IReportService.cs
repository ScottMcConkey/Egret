using Egret.DataAccess.QueryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egret.Services
{
    public interface IReportService
    {
        List<Test> TotalValueByCategory();
    }
}
