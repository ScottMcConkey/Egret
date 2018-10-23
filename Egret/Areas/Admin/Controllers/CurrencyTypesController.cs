using Egret.DataAccess;
using Egret.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Egret.Controllers
{
    [Area("Admin")]
    public class CurrencyTypesController : BaseController
    {
        public CurrencyTypesController(EgretContext context)
            : base(context) { }

        [HttpGet]
        public IActionResult Index()
        {
            var currencyTypes = Context.CurrencyTypes.OrderBy(x => x.SortOrder);
            return View(currencyTypes.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(List<CurrencyType> types)
        {
            // Find duplicates
            var duplicateName = types.GroupBy(x => x.Name)
                .Where(g => g.Count() > 1)
                .Select(y => y.Key)
                .ToList();
            var duplicateAbbr = types.GroupBy(x => x.Abbreviation)
                .Where(g => g.Count() > 1)
                .Select(y => y.Key)
                .ToList();
            var duplicateSymbol = types.GroupBy(x => x.Symbol)
                .Where(g => g.Count() > 1)
                .Select(y => y.Key)
                .ToList();
            var duplicateSortOrder = types.GroupBy(x => x.SortOrder)
                .Where(g => g.Count() > 1)
                .Select(y => y.Key)
                .ToList();

            // Find Sort Order <= 0
            var badSort = types.Where(x => x.SortOrder <= 0);

            // Error on duplicates
            if (duplicateName.Count() > 0)
                ModelState.AddModelError("", "Duplicate Name detected. Please assign every Currency Type a unique Name.");

            if (duplicateAbbr.Count() > 0)
                ModelState.AddModelError("", "Duplicate Abbreviation detected. Please assign every Currency Type a unique Abbreviation.");

            if (duplicateSymbol.Count() > 0)
                ModelState.AddModelError("", "Duplicate Symbol detected. Please assign every Currency Type a unique Symbol.");

            if (duplicateSortOrder.Count() > 0)
                ModelState.AddModelError("", "Duplicate Sort Order detected. Please assign every Currency Type a unique Sort Order.");

            if (badSort.Count() > 0)
                ModelState.AddModelError("", "Sort Order below 1 detected. Please assign every Currency Type a positive Sort Order.");


            // Ensure inactive currency type is not set as default
            foreach (var type in types)
            {
                if (type.DefaultSelection && type.Active == false)
                {
                    ModelState.AddModelError(String.Empty, "Default row must be active.");
                    return View(types);
                }
            }

            if (ModelState.IsValid)
            {
                for (int i = 0; i < types.Count(); i++)
                {
                    Context.Update(types[i]);
                }
                Context.SaveChanges();
                TempData["SuccessMessage"] = "Save Complete";
                return RedirectToAction(nameof(Index));
            }

            return View(types);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CurrencyType currencyType)
        {
            currencyType.SortOrder = Context.CurrencyTypes.Max(x => x.SortOrder) + 1;
            currencyType.DefaultSelection = false;

            if (ModelState.IsValid)
            {
                Context.Add(currencyType);
                Context.SaveChanges();
                TempData["SuccessMessage"] = "Currency Type Created";
                return RedirectToAction(nameof(Index));
            }

            return View(currencyType);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var currencyType = Context.CurrencyTypes.SingleOrDefault(m => m.Id == id);
            Context.CurrencyTypes.Remove(currencyType);
            Context.SaveChanges();
            TempData["SuccessMessage"] = $"Currency Type '{currencyType.Name}' Deleted";
            return RedirectToAction(nameof(Index));
        }

    }
}