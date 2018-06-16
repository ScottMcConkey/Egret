using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Egret.DataAccess;
using Egret.Models;

namespace Egret.Controllers
{
    public class ConsumptionController : ManagedController
    {
        public ConsumptionController(EgretContext context)
            : base(context) { }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            ConsumptionEvent consumptionEvent = Context.ConsumptionEvents.Where(x => x.Id == id).FirstOrDefault();

            if (consumptionEvent != null)
            {
                return View(consumptionEvent);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, ConsumptionEvent consumptionEvent)
        {
            if (ModelState.IsValid)
            {
                Context.Update(consumptionEvent);
                await Context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Save Complete";
                return RedirectToAction(nameof(Edit));
            }
            return View(consumptionEvent);
        }
    }
}