using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Egret.DataAccess;
using Egret.Models;
using Microsoft.AspNetCore.Http;

namespace Egret.Controllers
{
    public class SuppliersController : ManagedController
    {

        public SuppliersController(EgretContext context)
            :base(context) { }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Edit()
        {
            var egretContext = Context.Suppliers.OrderBy(x => x.Name);
            return View(egretContext.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(List<Supplier> suppliers)
        {
            for (int i = 0; i < suppliers.Count; i++)
            {
                Context.Update(suppliers[i]);
            }
            Context.SaveChanges();

            return RedirectToAction(nameof(Edit));
        }

        
    }
}