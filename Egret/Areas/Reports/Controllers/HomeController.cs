using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Egret.Controllers;
using Egret.DataAccess;
using Egret.Reports;
using Egret.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.IO;

namespace Egret.Areas.Reports.Controllers
{
    [Area("Reports")]
    [Authorize(Roles = "Item_Read")]
    public class HomeController : Controller
    {
        private readonly ILogger _logger;
        private readonly IReportService _reportService;

        public HomeController(EgretDbContext context, ILogger<HomeController> logger,IReportService reportService)
        {
            _logger = logger;
            _reportService = reportService;
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
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

    }
}
