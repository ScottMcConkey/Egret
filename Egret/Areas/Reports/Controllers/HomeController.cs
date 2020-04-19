using ClosedXML.Excel;
using Egret.Controllers;
using Egret.DataAccess;
using Egret.Models;
using Egret.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Egret.Areas.Reports.Controllers
{
    [Area("Reports")]
    public class HomeController : BaseController
    {
        private static ILogger _logger;

        public HomeController(EgretContext context, ILogger<HomeController> logger)
            : base(context)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public FileStreamResult CurrentInventoryReport()
        {
            var report = new CurrentInventoryModel();
            report.Categories = new List<CategoryReport>();

            MemoryStream ms = new MemoryStream();

            Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            // Get the Data from EF and get stock value
            //var cats = Context.InventoryItems
            //    .Include(i => i.ConsumptionEventsNavigation)
            //    .Include(i => i.CategoryNavigation)
            //    .GroupBy(x => new { CategoryId = x.CategoryNavigation.Id, Category = x.CategoryNavigation.Name })
            //    .Select(y => new { Name = y.Key.Category, Quantity = y.Sum(z => (decimal)z.StockQuantity) })
            //    .ToList();
            var categories = Context.InventoryCategories.OrderBy(x => x.Name).ToList();

            // Process the Data
            //foreach (var i in cats)
            //{
            //    var category = new CategoryReport();
            //    category.CategoryName = i.Name;
            //    category.AvailableLotCount = i.Quantity;
            //    report.Categories.Add(category);
            //}

            using (XLWorkbook workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Test Sheet");
                for (var row = 0; row < categories.Count; row++)
                {
                    worksheet.Cell(row + 1, 1).Value = categories[row].Name;
                }
                //worksheet.Cell("A1").Value = categories.Count.ToString();
                workbook.SaveAs(ms);
            }

            ms.Position = 0;

            return File(ms, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Report.xlsx");
        }

    }
}
