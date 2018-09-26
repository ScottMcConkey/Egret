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

            if (id == null || consumptionEvent == null)
            {
                return NotFound();
            }

            presentation.ConsumptionEvent = consumptionEvent;
            var test = new List<InventoryItem>();
            test.Add(consumptionEvent.InventoryItemNavigation);
            presentation.InventoryItems = test;

            return View(presentation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ConsumptionEventViewModel vm)
        {
            ViewData["Unit"] = new SelectList(ActiveUnits, "Abbreviation", "Abbreviation");

            var temp = Context.ConsumptionEvents.Where(x => x.Id == id).FirstOrDefault();
            vm.ConsumptionEvent.AddedBy = temp.AddedBy;
            vm.ConsumptionEvent.DateAdded = temp.DateAdded;
            vm.ConsumptionEvent.UpdatedBy = temp.UpdatedBy;
            vm.ConsumptionEvent.DateUpdated = temp.DateUpdated;

            if (ModelState.IsValid)
            {
                vm.ConsumptionEvent.UpdatedBy = User.Identity.Name;
                vm.ConsumptionEvent.DateUpdated = DateTime.Now;
                Context.Update(vm.ConsumptionEvent);
                await Context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Save Complete";
                return RedirectToAction();
            }

            return View(vm.ConsumptionEvent);
        }

        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search(ConsumptionEventSearchModel search)
        {
            var results = Context.ConsumptionEvents.AsQueryable();

            // Code
            if (!String.IsNullOrEmpty(search.Code))
                results = results.Where(x => x.InventoryItemCode.Contains(search.Code));

            // Date Added
            if (search.DateCreatedStart != null && search.DateCreatedEnd != null)
            {
                results = results.Where(x => x.DateAdded.Value.Date >= search.DateCreatedStart.Value.Date && x.DateAdded.Value.Date <= search.DateCreatedEnd.Value.Date);
            }
            else if (search.DateCreatedStart != null && search.DateCreatedEnd == null)
            {
                results = results.Where(x => x.DateAdded.Value.Date >= search.DateCreatedStart.Value.Date);
            }
            else if (search.DateCreatedStart == null && search.DateCreatedEnd != null)
            {
                results = results.Where(x => x.DateAdded.Value.Date <= search.DateCreatedEnd.Value.Date);
            }

            // Consumed By
            if (!String.IsNullOrEmpty(search.ConsumedBy))
                results = results.Where(x => x.ConsumedBy.ToLowerInvariant().Contains(search.ConsumedBy.ToLowerInvariant()));

            return View("Results", results.OrderBy(x => x.InventoryItemCode).ToList());
        }

    }

}