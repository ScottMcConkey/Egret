using Egret.Controllers;
using Egret.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
//using SpreadsheetLight;
using ClosedXML.Excel;
using System.IO;
//using System.IO;

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
        //public FileStreamResult CurrentInventoryReport()
        //{
        //    var report = new CurrentInventoryModel();
        //    //foreach (InventoryCategory category in Context.InventoryCategories.AsNoTracking().Include(x => x.)
        //    //{
        //    //    var category = new CategoryReport();
        //    //    category.CurrentStockValue = category.
        //    //    report.Categories.Add(category);
        //    //}

        //    var cats = Context.InventoryItems
        //        .AsNoTracking()
        //        .Include(i => i.ConsumptionEventsNavigation)
        //        .Include(i => i.CategoryNavigation)
        //        .GroupBy(x => new { CategoryId = x.CategoryNavigation.Id, Category = x.CategoryNavigation.Name })
        //        .Select(y => new { Name = y.Key.Category, Quantity = y.Sum(z => (decimal)z.StockQuantity) })
        //        .ToList();

        //    report.Categories = new List<CategoryReport>();

        //    foreach (var i in cats)
        //    {
        //        var category = new CategoryReport();
        //        //category.CategoryName = Context.InventoryCategories.AsNoTracking().Where(x => x.Name == i.)
        //        category.CategoryName = i.Name;
        //        category.AvailableLotCount = i.Quantity;
        //        report.Categories.Add(category);
        //    }

        //    //using (var sl = new SLDocument())
        //    //{
        //    //    sl.SetCellValue("A1", 3.145);
        //    //    sl.SaveAs("derp.xlsx");
        //    //}

        //    //var sl = new SLDocument();
        //    //sl.SetCellValue("A1", 3.145);
        //    //sl.SaveAs("derp.xlsx");

        //    //var cd = new System.Net.Mime.ContentDisposition
        //    //{
        //    //    FileName = "Test File",
        //    //    Inline = false
        //    //};


        //    //return View("CurrentInventoryReport", report);
        //    //return FileResult(cd.);

        //    var filePath = System.IO.Path.GetTempPath();


        //    SLDocument sl = new SLDocument();
        //    sl.SetCellValue(1, 1, "Some Text");
        //    //Response.Clear();
        //    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //    Response.AddHeader("Content-Disposition", "attachment; filename=WebStreamDownload.xlsx");
        //    sl.SaveAs(Response.OutputStream);
        //    Response.End();
        //}

        public FileStreamResult GenerateReport()
        {
            MemoryStream ms = new MemoryStream();
            using (XLWorkbook workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Test Sheet");
                worksheet.Cell("A1").Value = "hi there";
                //sl.SetCellValue("B3", "I love ASP.NET MVC");
                workbook.SaveAs(ms);
            }
            // this is important. Otherwise you get an empty file
            // (because you'd be at EOF after the stream is written to, I think...).
            ms.Position = 0;

            return File(ms, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Report.xlsx");
        }

        public string Test()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Test Sheet");
                worksheet.Cell("A1").Value = "hi there";
                
            }

            return "Test";
        }
    }
}
