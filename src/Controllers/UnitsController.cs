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
        public IActionResult Edit()
        {
            var egretContext = _context.Units.OrderBy(x => x.Sortorder);
            return View(egretContext.ToList());
        }

        // POST: Unit/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(List<Unit> units)
        {

            for (int i = 0; i < units.Count; i++)
            {
                _context.Update(units[i]);
            }


            _context.SaveChanges();
            


            //for (int i = 0; i < units.Count; i++)
            //{
            //    test += units[i].Id + " ";
            //    test += units[i].Name + " ";
            //    test += units[i].Abbreviation + " ";
            //    test += units[i].Sortorder + " ";
            //    test += units[i].Active + " ";
            //
            //    
            //    //var ent = _context.Find<Unit>(units[i].Id);
            //    //test += ent.ToString();
            //
            //    test += " \n";
            //
            //}

            return RedirectToAction(nameof(Edit));
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

            return RedirectToAction(nameof(Edit));

        }

        // GET: Unit/Delete/5
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var unit = _context.Units.SingleOrDefault(m => m.Id == id);
            _context.Units.Remove(unit);
            _context.SaveChanges();
            return RedirectToAction(nameof(Edit));
        }


    }
}