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
        public async Task<IActionResult> Edit()
        {
            var egretContext = _context.Units.OrderBy(x => x.Sortorder);
            return View(await egretContext.ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string Edit([Bind("Id, Name, Abbreviation, Sortorder, Active")] List<Unit> units)
        {

            if (ModelState.IsValid)
            {
                foreach (var i in units)
                {
                    try
                    {
                        _context.Update(i);

                    }
                    catch (DbUpdateException)
                    {
                        //throw;
                    }
                    //return "dur"; // RedirectToAction(nameof(Index));
                }
                //_context.SaveChanges();
            }

            string test = "";
            foreach (var i in units)
            {
                test += " " + i.Abbreviation;
                //= units[5].Abbreviation.ToString();
            }

            //try
            //{
            //    
            //    
            //}
            //catch (Exception ex)
            //{
            //    //return ex.ToString();
            //}
            ////if (ModelState.IsValid)
            //{
            //    return RedirectToAction(nameof(Index));
            //}
            return test;// View(test);
        }

        // GET: Unit/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Unit/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here
        //
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}


        // POST: Unit/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here
        //
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: Unit/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Unit/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here
        //
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}