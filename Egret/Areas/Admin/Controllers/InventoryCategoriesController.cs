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
            if (ModelState.IsValid)
            {
                for (int i = 0; i < inventoryCategories.Count; i++)
                {
                    Context.Update(inventoryCategories[i]);
                }
                Context.SaveChanges();
                TempData["SuccessMessage"] = "Save Complete";
                return RedirectToAction("Index");
            }
            else
            {
                return View(inventoryCategories);
            }
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