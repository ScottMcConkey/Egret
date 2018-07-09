using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Egret.DataAccess;
using Egret.Models;
using Egret.ViewModels;

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
        public IActionResult Create(string sourceid, ConsumptionEvent consumptionEvent)
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
        public async Task<IActionResult> Edit(string id)
        {
            ConsumptionEventViewModel presentation = new ConsumptionEventViewModel();

            ViewData["Unit"] = new SelectList(ActiveUnits, "Abbreviation", "Abbreviation");

            ConsumptionEvent consumptionEvent = await Context.ConsumptionEvents
                .Include(i => i.InventoryItemNavigation)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (consumptionEvent == null)
            {
                return NotFound();
            }

            presentation.ConsumptionEvent = consumptionEvent;
            //presentation.InventoryItems 
            var test = new List<InventoryItem>();
            test.Add(consumptionEvent.InventoryItemNavigation);
            presentation.InventoryItems = test;

            if (presentation.ConsumptionEvent != null)
            {
                return View(presentation);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, ConsumptionEventViewModel vm)
        {
            ViewData["Unit"] = new SelectList(ActiveUnits, "Abbreviation", "Abbreviation");

            if (ModelState.IsValid)
            {
                vm.ConsumptionEvent.UpdatedBy = User.Identity.Name;
                vm.ConsumptionEvent.DateUpdated = DateTime.Now;
                Context.Update(vm.ConsumptionEvent);
                await Context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Save Complete";
                return RedirectToAction(nameof(Edit));
            }
            
            return View(vm.ConsumptionEvent);
        }

    }
}