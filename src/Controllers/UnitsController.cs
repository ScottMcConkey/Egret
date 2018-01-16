using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Egret.DataAccess;

namespace Egret.Controllers
{
    public class UnitsController : Controller
    {
        private readonly EgretContext _context;

        public UnitsController(EgretContext context)
        {
            _context = context;
        }

        // GET: Unit
        public IActionResult Index()
        {
            var egretContext = _context.Units.OrderBy(x => x.Sortorder);
            return View(egretContext.ToList());
        }

        // POST: Unit/Index/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(List<Unit> units)
        {
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

        // GET: Unit/Create
        public ActionResult Create()
        {
            ViewData["DefaultSortOrder"] = _context.Units.Max(x => x.Sortorder) + 1;
            return View();
        }

        // POST: Unit/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Unit unit)
        {
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

        // GET: Unit/Delete/5
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