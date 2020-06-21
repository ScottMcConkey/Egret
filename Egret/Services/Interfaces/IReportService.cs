using Egret.DataAccess.QueryModels;
using Egret.Reports;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Egret.Services
{
    public interface IReportService
    {
        //List<StockValueReport> TotalValueByCategory();

        Stream GetTotalStockValueByCategoryReport();
    }
}
