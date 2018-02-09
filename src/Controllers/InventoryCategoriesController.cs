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
            var egretContext = _context.InventoryCategories.OrderBy(x => x.Sortorder);
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
                    _context.Update(inventoryCategories[i]);
                }
                _context.SaveChanges();
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
            category.Sortorder = _context.InventoryCategories.Max(x => x.Sortorder) + 1;

            if (ModelState.IsValid)
            {
                _context.Add(category);
                
            }
            _context.SaveChanges();
            TempData["SuccessMessage"] = "Save Complete";
            return View();
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var category = _context.InventoryCategories.SingleOrDefault(m => m.Id == id);
            _context.InventoryCategories.Remove(category);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}