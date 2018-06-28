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
        private IQueryable<Unit> ActiveUnits { get; set; }

        public ConsumptionController(EgretContext context)
            : base(context)
        {
            ActiveUnits = Context.Units.Where(x => x.Active == true).OrderBy(x => x.SortOrder);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Unit"] = new SelectList(ActiveUnits, "Abbreviation", "Abbreviation");
            return View();
        }

        [HttpPost]
        public IActionResult Create(string inventoryid, ConsumptionEvent consumptionEvent)
        {
            ViewData["Unit"] = new SelectList(ActiveUnits, "Abbreviation", "Abbreviation");

            if (ModelState.IsValid)
            {
                consumptionEvent.AddedBy = User.Identity.Name;
                consumptionEvent.UpdatedBy = User.Identity.Name;
                consumptionEvent.DateAdded = DateTime.Now;
                consumptionEvent.DateUpdated = DateTime.Now;
                Context.ConsumptionEvents.Add(consumptionEvent);
                Context.SaveChanges();

                return RedirectToAction("Edit", "Inventory", new { id = consumptionEvent.InventoryItemCode });
            }

            return View();
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
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
            ViewData["Unit"] = new SelectList(ActiveUnits, "Abbreviation", "Abbreviation");

            if (ModelState.IsValid)
            {
                consumptionEvent.UpdatedBy = User.Identity.Name;
                consumptionEvent.DateUpdated = DateTime.Now;
                Context.Update(consumptionEvent);
                await Context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Save Complete";
                return RedirectToAction(nameof(Edit));
            }
            
            return View(consumptionEvent);
        }

    }
}