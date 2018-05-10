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
            if (ModelState.IsValid)
            {
                for (int i = 0; i < units.Count; i++)
                {
                    Context.Update(units[i]);
                }
                Context.SaveChanges();
                TempData["SuccessMessage"] = "Save Complete";
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