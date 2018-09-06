using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Egret.Controllers;
using Egret.DataAccess;
using Egret.Models;
using Egret.ViewModels;

namespace Egret.Controllers
{
    [Area("Inventory")]
    public class ConsumptionEventsController : ManagedController
    {
        private IQueryable<Unit> ActiveUnits { get; set; }

        public ConsumptionEventsController(EgretContext context)
            : base(context)
        {
            ActiveUnits = Context.Units.Where(x => x.Active).OrderBy(x => x.SortOrder);
        }

        [HttpGet]
        public IActionResult CreateFromItem(string sourceid)
        {
            var item = Context.InventoryItems.Where(x => x.Code == sourceid);
            ViewData["Unit"] = new SelectList(item, "Unit", "Unit", item.FirstOrDefault().Unit);//new SelectList(ActiveUnits, "Abbreviation", "Abbreviation");


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateFromItem(string sourceid, ConsumptionEvent consumptionEvent)
        {
            string sourceId = sourceid;

            var item = Context.InventoryItems.Where(x => x.Code == consumptionEvent.InventoryItemCode);
            ViewData["Unit"] = new SelectList(item, "Unit", "Unit", item.FirstOrDefault().Unit);

            consumptionEvent.AddedBy = User.Identity.Name;
            consumptionEvent.UpdatedBy = User.Identity.Name;
            consumptionEvent.DateAdded = DateTime.Now;
            consumptionEvent.DateUpdated = DateTime.Now;

            if (ModelState.IsValid)
            {
                Context.ConsumptionEvents.Add(consumptionEvent);
                Context.SaveChanges();

                return RedirectToAction("Edit", "Items", new { id = consumptionEvent.InventoryItemCode });
            }

            return View("CreateFromItem", consumptionEvent);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Unit"] = new SelectList(ActiveUnits, "Abbreviation", "Abbreviation");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ConsumptionEvent consumptionEvent)
        {
            ViewData["Unit"] = new SelectList(ActiveUnits, "Abbreviation", "Abbreviation");

            consumptionEvent.AddedBy = User.Identity.Name;
            consumptionEvent.UpdatedBy = User.Identity.Name;
            consumptionEvent.DateAdded = DateTime.Now;
            consumptionEvent.DateUpdated = DateTime.Now;

            if (ModelState.IsValid)
            {
                Context.ConsumptionEvents.Add(consumptionEvent);
                Context.SaveChanges();

                return RedirectToAction("Edit", new { id = consumptionEvent.Id });
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ConsumptionEventViewModel vm)
        {
            ViewData["Unit"] = new SelectList(ActiveUnits, "Abbreviation", "Abbreviation");

            vm.ConsumptionEvent.UpdatedBy = User.Identity.Name;
            vm.ConsumptionEvent.DateUpdated = DateTime.Now;

            if (ModelState.IsValid)
            {
                Context.Update(vm.ConsumptionEvent);
                await Context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Save Complete";
                return RedirectToAction(nameof(Edit));
            }
            
            return View(vm.ConsumptionEvent);
        }

    }
}