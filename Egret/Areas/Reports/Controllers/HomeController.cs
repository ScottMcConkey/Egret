using ClosedXML.Excel;
using Egret.Controllers;
using Egret.DataAccess;
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
            var categories = _reportService.TotalValueByCategory();

            var stream = new MemoryStream();
            // Refactor to be Wrapper
            using (XLWorkbook workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Test Sheet");
                for (var row = 0; row < categories.Count; row++)
                {
                    worksheet.Cell(row + 1, 1).Value = categories[row].Name;
                    worksheet.Cell(row + 1, 2).Value = categories[row].StockValue.ToString();
                }
                workbook.SaveAs(stream);
            }

            stream.Position = 0;

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Report.xlsx");
        }

    }
}
