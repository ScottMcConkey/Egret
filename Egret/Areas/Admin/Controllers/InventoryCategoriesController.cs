using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Egret.DataAccess;
using Egret.Models;

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
            var egretContext = Context.InventoryCategories.OrderBy(x => x.Sortorder);
            return View(egretContext.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(List<InventoryCategory> inventoryCategories)
        {
            ViewData["BackText"] = "Back to Admin";

            // Find duplicates
            var duplicateName = inventoryCategories.GroupBy(x => x.Name).Where(g => g.Count() > 1)
                .Select(y => y.Key)
                .ToList();
            var duplicateSortOrder = inventoryCategories.GroupBy(x => x.Sortorder).Where(g => g.Count() > 1)
                .Select(y => y.Key)
                .ToList();

            // Find Sort Order <= 0
            var badSort = inventoryCategories.Where(x => x.Sortorder <= 0);

            // Find usage


            if (duplicateName.Count > 0)
            {
                ModelState.AddModelError("", "Duplicate Name detected. Please assign every Inventory Category a unique Name.");
            }

            if (duplicateSortOrder.Count > 0)
            {
                ModelState.AddModelError("", "Duplicate Sort Order detected. Please assign every Inventory Category a unique Sort Order.");
            }

            if (badSort.Count() > 0)
            {
                ModelState.AddModelError("", "Sort Order below 1 detected. Please assign every Inventory Category a positive Sort Order.");
            }

            if (ModelState.IsValid)
            {
                for (int i = 0; i < inventoryCategories.Count; i++)
                {
                    Context.Update(inventoryCategories[i]);
                }
                Context.SaveChanges();
                TempData["SuccessMessage"] = "Save Complete";
            }
            else
            {
                return View(inventoryCategories);
            }

            return RedirectToAction(nameof(Index));
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
            category.Sortorder = Context.InventoryCategories.Max(x => x.Sortorder) + 1;

            if (ModelState.IsValid)
            {
                Context.Add(category);
                
            }
            Context.SaveChanges();
            TempData["SuccessMessage"] = "Save Complete";
            return View();
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var category = Context.InventoryCategories.SingleOrDefault(m => m.Id == id);
            Context.InventoryCategories.Remove(category);
            Context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}