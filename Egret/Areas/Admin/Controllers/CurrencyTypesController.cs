using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using Npgsql;
using Npgsql.EntityFrameworkCore;
using Egret.DataAccess;
using Egret.Models;

namespace Egret.Controllers
{
    [Area("Admin")]
    public class CurrencyTypesController : ManagedController
    {
        private string GetBackText(object method)
        {
            string test = method.GetType().Name + "Test" + method.GetType().ToString();

            return test;
        }

        public string BackButtonText = "Back to Admin";
        public string BackButtonText2 = "Back to Currency Types";

        public CurrencyTypesController(EgretContext context)
            : base(context) { }

        [HttpGet]
        public IActionResult Index()
        {
            ViewData["BackText"] = GetBackText(this);//BackButtonText;
            var egretContext = Context.CurrencyTypes.OrderBy(x => x.Sortorder);
            return View(egretContext.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(List<CurrencyType> types)
        {
            foreach (var type in types)
            {
                if (type.Defaultselection && type.Active == false)
                {
                    ModelState.AddModelError(String.Empty, "Default row must be active.");
                    return View(types);
                }
            }

            if (ModelState.IsValid)
            {
                for (int i = 0; i < types.Count; i++)
                {
                    Context.Update(types[i]);
                }
                Context.SaveChanges();
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
            category.Sortorder = Context.CurrencyTypes.Max(x => x.Sortorder) + 1;
            category.Defaultselection = false;

            if (ModelState.IsValid)
            {
                Context.Add(category);
                Context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var inventoryItems = Context.CurrencyTypes.SingleOrDefault(m => m.Id == id);
            Context.CurrencyTypes.Remove(inventoryItems);
            Context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}