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
        public string BackButtonText = "Back to Admin";
        public string BackButtonText2 = "Back to Units";

        public UnitsController (EgretContext context)
            :base(context) {}

        [HttpGet]
        public IActionResult Index()
        {
            ViewData["BackText"] = BackButtonText;

            var egretContext = base._context.Units.OrderBy(x => x.Sortorder);
            return View(egretContext.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(List<Unit> units)
        {
            ViewData["BackText"] = BackButtonText;

            if (ModelState.IsValid)
            {
                for (int i = 0; i < units.Count; i++)
                {
                    try
                    {
                        _context.Update(units[i]);
                    }
                    catch (DbUpdateConcurrencyException)
                    {

                    }
                }
            }

            _context.SaveChanges();

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
            ViewData["BackText"] = BackButtonText2;

            unit.Sortorder = _context.Units.Max(x => x.Sortorder) + 1;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(unit);
                    _context.SaveChanges();
                }
                catch
                {
                    //ViewData[ExceptionResults] = Exception;
                    return View();
                }
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var unit = _context.Units.SingleOrDefault(m => m.Id == id);
            _context.Units.Remove(unit);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


    }
}