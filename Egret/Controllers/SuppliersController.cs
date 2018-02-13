using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Egret.DataAccess;
using Egret.Models;

namespace Egret.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly EgretContext _context;

        public SuppliersController(EgretContext context)
        {
            _context = context;
        }

        public IActionResult Edit()
        {
            var egretContext = _context.Suppliers.OrderBy(x => x.Name);
            return View(egretContext.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(List<Supplier> suppliers)
        {
            for (int i = 0; i < suppliers.Count; i++)
            {
                _context.Update(suppliers[i]);
            }
            _context.SaveChanges();

            return RedirectToAction(nameof(Edit));
        }

        
    }
}