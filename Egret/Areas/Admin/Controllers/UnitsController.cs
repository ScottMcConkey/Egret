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
    public class UnitsController : BaseController
    {
        private static ILogger _logger;

        public UnitsController (EgretContext context, ILogger<UnitsController> logger)
            :base(context)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var units = Context.Units.OrderBy(x => x.SortOrder);
            return View(units.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(List<Unit> units)
        {
            // Find duplicates
            var duplicateName = units.GroupBy(x => x.Name).Where(g => g.Count() > 1)
                .Select(y => y.Key)
                .ToList();
            var duplicateAbbr = units.GroupBy(x => x.Abbreviation).Where(g => g.Count() > 1)
                .Select(y => y.Key)
                .ToList();
            var duplicateSortOrder = units.GroupBy(x => x.SortOrder).Where(g => g.Count() > 1)
                .Select(y => y.Key)
                .ToList();

            // Find Sort Order <= 0
            var badSort = units.Where(x => x.SortOrder <= 0);

            // Find usage

            // Error on duplicates
            if (duplicateName.Count > 0)
                ModelState.AddModelError("", "Duplicate Name detected. Please assign every Unit a unique Name.");

            if (duplicateAbbr.Count > 0)
                ModelState.AddModelError("", "Duplicate Abbreviation detected. Please assign every Unit a unique Abbreviation.");

            if (duplicateSortOrder.Count > 0)
                ModelState.AddModelError("", "Duplicate Sort Order detected. Please assign every Unit a unique Sort Order.");

            if (badSort.Count() > 0)
                ModelState.AddModelError("", "Sort Order below 1 detected. Please assign every Unit a positive Sort Order.");

            if (ModelState.IsValid)
            {
                for (int i = 0; i < units.Count(); i++)
                {
                    Context.Update(units[i]);
                }
                Context.SaveChanges();
                TempData["SuccessMessage"] = "Save Complete";
                return RedirectToAction(nameof(Index));
            }

            return View(units);            
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Unit unit)
        {
            unit.SortOrder = Context.Units.Max(x => x.SortOrder) + 1;

            if (ModelState.IsValid)
            {
                Context.Add(unit);
                Context.SaveChanges();
                TempData["SuccessMessage"] = "Unit Created";
                return RedirectToAction(nameof(Index));
            }

            return View(unit);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var unit = Context.Units.SingleOrDefault(m => m.UnitId == id);
            Context.Units.Remove(unit);
            Context.SaveChanges();
            TempData["SuccessMessage"] = $"Unit '{unit.Name}' Deleted";
            return RedirectToAction(nameof(Index));
        }

    }
}