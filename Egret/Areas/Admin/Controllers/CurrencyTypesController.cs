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
            ViewData["BackText"] = "Back to Admin";

            // Find duplicates
            var duplicateName = types.GroupBy(x => x.Name).Where(g => g.Count() > 1)
                .Select(y => y.Key)
                .ToList();
            var duplicateAbbr = types.GroupBy(x => x.Abbreviation).Where(g => g.Count() > 1)
                .Select(y => y.Key)
                .ToList();
            var duplicateSymbol = types.GroupBy(x => x.Symbol).Where(g => g.Count() > 1)
                .Select(y => y.Key)
                .ToList();
            var duplicateSortOrder = types.GroupBy(x => x.Sortorder).Where(g => g.Count() > 1)
                .Select(y => y.Key)
                .ToList();

            // Find Sort Order <= 0
            var badSort = types.Where(x => x.Sortorder <= 0);


            if (duplicateName.Count > 0)
            {
                ModelState.AddModelError("", "Duplicate Name detected. Please assign every Currency Type a unique Name.");
            }

            if (duplicateAbbr.Count > 0)
            {
                ModelState.AddModelError("", "Duplicate Abbreviation detected. Please assign every Currency Type a unique Abbreviation.");
            }

            if (duplicateSymbol.Count > 0)
            {
                ModelState.AddModelError("", "Duplicate Symbol detected. Please assign every Currency Type a unique Symbol.");
            }

            if (duplicateSortOrder.Count > 0)
            {
                ModelState.AddModelError("", "Duplicate Sort Order detected. Please assign every Currency Type a unique Sort Order.");
            }

            if (badSort.Count() > 0)
            {
                ModelState.AddModelError("", "Sort Order below 1 detected. Please assign every Currency Type a positive Sort Order.");
            }


            // Ensure inactive currency type is not set as default
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
            else
            {
                return View(types);
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