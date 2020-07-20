using Egret.DataAccess;
using Egret.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Egret.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin_Access")]
    public class InventoryCategoriesController : Controller
    {
        private readonly ILogger _logger;

        private readonly EgretDbContext _context;

        public InventoryCategoriesController(EgretDbContext context, ILogger<InventoryCategoriesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        
        [HttpGet]
        public IActionResult Index()
        {
            var EgretDbContext = _context.InventoryCategories.OrderBy(x => x.SortOrder);
            return View(EgretDbContext.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(List<InventoryCategory> inventoryCategories)
        {
            // Find duplicates
            var duplicateName = inventoryCategories.GroupBy(x => x.Name).Where(g => g.Count() > 1)
                .Select(y => y.Key)
                .ToList();
            var duplicateSortOrder = inventoryCategories.GroupBy(x => x.SortOrder).Where(g => g.Count() > 1)
                .Select(y => y.Key)
                .ToList();

            // Find Sort Order <= 0
            var badSort = inventoryCategories.Where(x => x.SortOrder <= 0);

            // Find usage

            // Error on duplicates
            if (duplicateName.Count() > 0)
                ModelState.AddModelError("", "Duplicate Name detected. Please assign every Inventory Category a unique Name.");

            if (duplicateSortOrder.Count() > 0)
                ModelState.AddModelError("", "Duplicate Sort Order detected. Please assign every Inventory Category a unique Sort Order.");

            if (badSort.Count() > 0)
                ModelState.AddModelError("", "Sort Order below 1 detected. Please assign every Inventory Category a positive Sort Order.");

            if (ModelState.IsValid)
            {
                for (int i = 0; i < inventoryCategories.Count(); i++)
                {
                    _context.Update(inventoryCategories[i]);
                }
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Save Complete";
                return RedirectToAction(nameof(Index));
            }

            return View(inventoryCategories);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InventoryCategory category)
        {
            category.SortOrder = _context.InventoryCategories
                .Select(x => x.SortOrder)
                .DefaultIfEmpty()
                .ToList()
                .Max() + 1;

            if (ModelState.IsValid)
            {
                _context.Add(category);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Inventory Category Created";
                return RedirectToAction(nameof(Index));
            }
            
            return View(category);
        }

        public ActionResult Delete(int id)
        {
            var category = _context.InventoryCategories.SingleOrDefault(m => m.InventoryCategoryId == id);
            _context.InventoryCategories.Remove(category);
            _context.SaveChanges();
            TempData["SuccessMessage"] = $"Inventory Category '{category.Name}' Deleted";
            return RedirectToAction(nameof(Index));
        }
    }
}