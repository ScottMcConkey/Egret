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

            var egretContext = _context.Units.OrderBy(x => x.Sortorder);
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
                    _context.Update(units[i]);
                }
                _context.SaveChanges();
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
            unit.Sortorder = _context.Units.Max(x => x.Sortorder) + 1;

            if (ModelState.IsValid)
            {
                _context.Add(unit);
                _context.SaveChanges();
            }
            return View(unit);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var unit = _context.Units.SingleOrDefault(m => m.Id == id);
            _context.Units.Remove(unit);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


    }
}