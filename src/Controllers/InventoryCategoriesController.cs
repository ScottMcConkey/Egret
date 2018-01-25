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

        // GET: InventoryCategories
        public IActionResult Index()
        {
            var egretContext = base._context.InventoryCategories.OrderBy(x => x.Sortorder);
            return View(egretContext.ToList());
        }

        // POST: InventoryCategories/Index/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(List<InventoryCategory> inventoryCategories)
        {
            if (ModelState.IsValid)
            {
                for (int i = 0; i < inventoryCategories.Count; i++)
                {
                    try
                    {
                        _context.Update(inventoryCategories[i]);
                    }
                    catch (DbUpdateConcurrencyException)
                    {

                    }
                }
            }

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // GET: InventoryCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InventoryCategories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InventoryCategory category)
        {
            category.Sortorder = _context.InventoryCategories.Max(x => x.Sortorder) + 1;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(category);
                    _context.SaveChanges();
                }
                catch
                {
                    return View();
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: InventoryCategories/Delete/5
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