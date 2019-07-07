using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Egret.Models;
using Egret.ViewModels;
using Egret.DataAccess;
using Microsoft.Extensions.Logging;
using Egret.Controllers;
using Microsoft.EntityFrameworkCore;

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

        public IActionResult CurrentInventoryReport()
        {
            var report = new CurrentInventoryModel();
            //foreach (InventoryCategory category in Context.InventoryCategories.AsNoTracking().Include(x => x.)
            //{
            //    var category = new CategoryReport();
            //    category.CurrentStockValue = category.
            //    report.Categories.Add(category);
            //}

            var cats = Context.InventoryItems
                .AsNoTracking()
                .Include(i => i.ConsumptionEventsNavigation)
                .Include(i => i.CategoryNavigation)
                .GroupBy(x => new { CategoryId = x.CategoryNavigation.Id, Category = x.CategoryNavigation.Name })
                .Select(y => new { Name = y.Key.Category, Quantity = y.Sum(z => (decimal)z.StockQuantity) })
                .ToList();

            report.Categories = new List<CategoryReport>();

            foreach (var i in cats)
            {
                var category = new CategoryReport();
                //category.CategoryName = Context.InventoryCategories.AsNoTracking().Where(x => x.Name == i.)
                category.CategoryName = i.Name;
                category.AvailableLotCount = i.Quantity;
                report.Categories.Add(category);
            }

            //foreach ()
            //{
            //    var category = new CategoryReport();
            //    category.AvailableLotCount = (decimal)item.StockQuantity;
            //    category.CurrentStockValue = (decimal)item.StockQuantity * (decimal)item.TotalCostPerUnit;
            //    report.Categories.Add(category);
            //}

            return View("CurrentInventoryReport", report);
        }
    }
}
