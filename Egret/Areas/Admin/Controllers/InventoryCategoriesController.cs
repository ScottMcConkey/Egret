using Egret.DataAccess;
using Egret.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Egret.Controllers
{
    [Area("Admin")]
    public class InventoryCategoriesController : ManagedController
    {
        public InventoryCategoriesController(EgretContext context)
            : base(context) { }

        
        [HttpGet]
        public IActionResult Index()
        {
            var egretContext = Context.InventoryCategories.OrderBy(x => x.SortOrder);
            return View(egretContext.ToList());
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
                    Context.Update(inventoryCategories[i]);
                }
                Context.SaveChanges();
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
            category.SortOrder = Context.InventoryCategories.Max(x => x.SortOrder) + 1;

            if (ModelState.IsValid)
            {
                Context.Add(category);
                Context.SaveChanges();
                TempData["SuccessMessage"] = "Inventory Category Created";
                return RedirectToAction(nameof(Index));
            }
            
            return View(category);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var category = Context.InventoryCategories.SingleOrDefault(m => m.Id == id);
            Context.InventoryCategories.Remove(category);
            Context.SaveChanges();
            TempData["SuccessMessage"] = $"Inventory Category '{category.Name}' Deleted";
            return RedirectToAction(nameof(Index));
        }
    }
}