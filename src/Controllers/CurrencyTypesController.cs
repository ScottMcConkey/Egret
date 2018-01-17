using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Egret.DataAccess;
using Egret.Models;

namespace Egret.Controllers
{
    public class CurrencyTypesController : Controller
    {
        private readonly EgretContext _context;

        public CurrencyTypesController(EgretContext context)
        {
            _context = context;
        }

        // GET: Unit
        public IActionResult Index()
        {
            var egretContext = _context.CurrencyTypes.OrderBy(x => x.Sortorder);
            return View(egretContext.ToList());
        }

        // POST: Unit/Index/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(List<CurrencyType> types)
        {
            if (ModelState.IsValid)
            {
                for (int i = 0; i < types.Count; i++)
                {
                    try
                    {
                        _context.Update(types[i]);
                    }
                    catch (DbUpdateConcurrencyException)
                    {

                    }
                }
            }
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}