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

        public class Test
        {
            public string Quantity { get; set; }
            public string Name { get; set; }
        }

        [HttpGet]
        public FileStreamResult CurrentInventoryReport()
        {
            var report = new CurrentInventoryModel();
            report.Categories = new List<CategoryReport>();

            MemoryStream ms = new MemoryStream();

            Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;


            // Get the Data from EF and get stock value
            //var categories = Context.InventoryItems
            //    .Include(i => i.ConsumptionEventsNavigation)
            //    .Include(i => i.CategoryNavigation)
            //    .GroupBy(x => new { CategoryId = x.CategoryNavigation.Id, Category = x.CategoryNavigation.Name })
            //    .Select(y => new { Name = y.Key.Category, Quantity = y.Sum(z => (decimal)z.ConsumptionEventsNavigation.) })
            //    .ToList();
            //var categories = Context.InventoryItems
            //    .Include(i => i.ConsumptionEventsNavigation)
            //    .Include(i => i.CategoryNavigation)
            //    //.GroupBy(x => new { CategoryId = x.CategoryNavigation.Id, Category = x.CategoryNavigation.Name })
            //    .GroupBy(x => x.CategoryNavigation.Name)
            //    .Select(y => new { Name = y.Key })
            //    .ToList();
            var categories = from inv in Context.Set<InventoryItem>()
                             from c in Context.Set<InventoryCategory>()
                                .Where(c => c.Id == inv.CategoryId)
                                .DefaultIfEmpty()
                             select new { Name = c.Name, Test = inv.QtyPurchased };
                             
            //var categories = Context.InventoryCategories.OrderBy(x => x.Name).ToList();

            // Process the Data
            foreach (var i in categories)
            {
                var category = new CategoryReport();
                category.CategoryName = i.Name;
                category.CurrentStockValue = i.Test;
                report.Categories.Add(category);
            }

            using (XLWorkbook workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Test Sheet");
                for (var row = 0; row < report.Categories.Count; row++)
                {
                    worksheet.Cell(row + 1, 1).Value = report.Categories[row].CategoryName;
                    worksheet.Cell(row + 1, 2).Value = report.Categories[row].CurrentStockValue.ToString();
                    //worksheet.Cell(row + 1, 3).Value = report.Categories[row].GetErrorCount.ToString();
                }
                //worksheet.Cell("A1").Value = categories.Count.ToString();
                workbook.SaveAs(ms);
            }

            ms.Position = 0;

            return File(ms, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Report.xlsx");
        }

    }
}
