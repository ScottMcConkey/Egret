﻿using ClosedXML.Excel;
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
    [Authorize(Roles = "Report_Read")]
    public class HomeController : Controller
    {
        private readonly ILogger _logger;
        private readonly ReportService _reportService;

        public HomeController(EgretDbContext context, ILogger<HomeController> logger, ReportService reportService)
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
            var report = new Report()
            {
                Title = "Total Stock Value by Category Report",
                ColumnNames = "Category,Stock Value",
                Details = _reportService.GetTotalStockValueByCategoryReport()
            };

            var reportBuilder = new ReportBuilder();

            var stream = reportBuilder.Build(report);

            return File(stream, report.ContentType, report.FileDownloadName);
        }

        [HttpGet]
        public FileStreamResult InventoryFinancialDataReport()
        {
            var report = new Report()
            {
                Title = "Total Inventory Financial Data Report",
                ColumnNames = "Category,Item Id,Item Description,Qty Purchased,Unit(s),FOB Cost,Shipping Cost,Import Cost,VAT Cost,Consumption Id,Qty Consumed,Date Consumed",
                Details = _reportService.GetInventoryFinancialData()
            };

            var reportBuilder = new ReportBuilder();

            var stream = reportBuilder.Build(report);

            return File(stream, report.ContentType, report.FileDownloadName);
        }

    }
}
