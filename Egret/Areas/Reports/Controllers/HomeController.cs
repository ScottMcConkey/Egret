using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Egret.Controllers;
using Egret.DataAccess;
using Egret.Reports;
using Egret.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Egret.Areas.Reports.Controllers
{
    [Area("Reports")]
    [Authorize(Roles = "Item_Read")]
    public class HomeController : BaseController
    {
        //private static ILogger _logger;
        private static IReportService _reportService;

        public HomeController(EgretContext context, IReportService reportService)
            : base(context)
        {
            //_logger = logger;
            _reportService = reportService;
            Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public FileStreamResult CurrentInventoryReport()
        {
            var reportFile = _reportService.GetTotalStockValueByCategoryReport();

            //return reportFile;
            return File(reportFile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Report.xlsx");
        }

        private void GetFile()
        {

        }

    }
}
