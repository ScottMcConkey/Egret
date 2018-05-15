using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Egret.DataAccess;
using Egret.Models;
using Egret.Code;

namespace Egret.Controllers
{
    [Area("Admin")]
    public class UnitsController : ManagedController
    {
        public readonly string BackButtonText = "Back to Admin";
        public readonly string BackButtonText2 = "Back to Units";

        public UnitsController (EgretContext context)
            :base(context) {}

        [HttpGet]
        public IActionResult Index()
        {
            ViewData["BackText"] = BackButtonText;

            var egretContext = Context.Units.OrderBy(x => x.Sortorder);
            return View(egretContext.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(List<Unit> units)
        {
            ViewData["BackText"] = "Back to Admin";

            // Find duplicates
            var duplicateName = units.GroupBy(x => x.Name).Where(g => g.Count() > 1)
                .Select(y => y.Key)
                .ToList();
            var duplicateAbbr = units.GroupBy(x => x.Abbreviation).Where(g => g.Count() > 1)
                .Select(y => y.Key)
                .ToList();
            var duplicateSortOrder = units.GroupBy(x => x.Sortorder).Where(g => g.Count() > 1)
                .Select(y => y.Key)
                .ToList();

            // Find Sort Order <= 0
            var badSort = units.Where(x => x.Sortorder <= 0);

            // Find usage


            if (duplicateName.Count > 0)
            {
                ModelState.AddModelError("", "Duplicate Name detected. Please assign every Unit a unique Name.");
            }

            if (duplicateAbbr.Count > 0)
            {
                ModelState.AddModelError("", "Duplicate Abbreviation detected. Please assign every Unit a unique Abbreviation.");
            }

            if (duplicateSortOrder.Count > 0)
            {
                ModelState.AddModelError("", "Duplicate Sort Order detected. Please assign every Unit a unique Sort Order.");
            }

            if (badSort.Count() > 0)
            {
                ModelState.AddModelError("", "Sort Order below 1 detected. Please assign every Unit a positive Sort Order.");
            }

            if (ModelState.IsValid)
            {
                for (int i = 0; i < units.Count; i++)
                {
                    Context.Update(units[i]);
                }
                Context.SaveChanges();
                TempData["SuccessMessage"] = "Save Complete";
            }
            else
            {
                return View(units);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewData["BackText"] = BackButtonText2;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Unit unit)
        {
            unit.Sortorder = Context.Units.Max(x => x.Sortorder) + 1;

            if (ModelState.IsValid)
            {
                Context.Add(unit);
                Context.SaveChanges();
            }
            return View(unit);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var unit = Context.Units.SingleOrDefault(m => m.Id == id);
            Context.Units.Remove(unit);
            Context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


    }
}