using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            var ActiveUnits = Context.Units.Where(x => x.Active == true).OrderBy(x => x.SortOrder);
            ViewData["Unit"] = new SelectList(ActiveUnits, "Abbreviation", "Abbreviation");
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
            var ActiveUnits = Context.Units.Where(x => x.Active == true).OrderBy(x => x.SortOrder);
            ViewData["Unit"] = new SelectList(ActiveUnits, "Abbreviation", "Abbreviation");

            return View(consumptionEvent);
        }

        [HttpGet]
        public IActionResult Create(string inventoryid)
        {
            var ActiveUnits = Context.Units.Where(x => x.Active == true).OrderBy(x => x.SortOrder);
            ViewData["Unit"] = new SelectList(ActiveUnits, "Abbreviation", "Abbreviation");
            return View();
        }

        [HttpPost]
        public IActionResult Create(ConsumptionEvent consumptionEvent)
        {
            if (ModelState.IsValid)
            {

                Context.ConsumptionEvents.Add(consumptionEvent);
                Context.SaveChanges();
                return RedirectToAction("Edit", "Inventory", new { id = consumptionEvent.InventoryItemCode });
            }
            var ActiveUnits = Context.Units.Where(x => x.Active == true).OrderBy(x => x.SortOrder);
            ViewData["Unit"] = new SelectList(ActiveUnits, "Abbreviation", "Abbreviation");

            return View();
        }
    }
}