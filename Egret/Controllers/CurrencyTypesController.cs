using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using Npgsql;
using Npgsql.EntityFrameworkCore;
using Egret.DataAccess;
using Egret.Models;

namespace Egret.Controllers
{
    public class CurrencyTypesController : ManagedController
    {
        public string BackButtonText = "Back to Admin";
        public string BackButtonText2 = "Back to Currency Types";

        public CurrencyTypesController(EgretContext context)
            : base(context) { }

        [HttpGet]
        public IActionResult Index()
        {
            ViewData["BackText"] = BackButtonText;
            var egretContext = _context.CurrencyTypes.OrderBy(x => x.Sortorder);
            return View(egretContext.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(List<CurrencyType> types)
        {
            if (ModelState.IsValid)
            {
                for (int i = 0; i < types.Count; i++)
                {
                    _context.Update(types[i]);
                }
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Save Complete";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["BackText"] = BackButtonText2;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CurrencyType category)
        {
            category.Sortorder = _context.CurrencyTypes.Max(x => x.Sortorder) + 1;
            category.Defaultselection = false;

            if (ModelState.IsValid)
            {
                _context.Add(category);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var inventoryItems = _context.CurrencyTypes.SingleOrDefault(m => m.Id == id);
            _context.CurrencyTypes.Remove(inventoryItems);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}