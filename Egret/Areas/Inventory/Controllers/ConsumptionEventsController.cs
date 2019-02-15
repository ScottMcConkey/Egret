﻿using Egret.DataAccess;
using Egret.Models;
using Egret.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egret.Controllers
{
    [Area("Inventory")]
    public class ConsumptionEventsController : BaseController
    {
        private IQueryable<Unit> _activeUnits { get; set; }
        private static ILogger _logger;

        public ConsumptionEventsController(EgretContext context, ILogger<ConsumptionEventsController> logger)
            : base(context)
        {
            _activeUnits = Context.Units.Where(x => x.Active).OrderBy(x => x.SortOrder);
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "Item_Read, ConsumptionEvent_Create")]
        public IActionResult CreateFromItem(string sourceid)
        {
            var item = Context.InventoryItems.Where(x => x.Code == sourceid);
            ViewData["Unit"] = new SelectList(item, "Unit", "Unit", item.FirstOrDefault().Unit);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Item_Read, ConsumptionEvent_Create")]
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
        [Authorize(Roles = "ConsumptionEvent_Create")]
        public IActionResult Create()
        {
            ViewData["Unit"] = new SelectList(_activeUnits, "Abbreviation", "Abbreviation");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ConsumptionEvent_Create")]
        public IActionResult Create(ConsumptionEvent consumptionEvent)
        {
            ViewData["Unit"] = new SelectList(_activeUnits, "Abbreviation", "Abbreviation");

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
        [Authorize(Roles = "ConsumptionEvent_Edit")]
        public async Task<IActionResult> Edit(string id)
        {
            ConsumptionEventViewModel presentation = new ConsumptionEventViewModel();

            ViewData["Unit"] = new SelectList(_activeUnits, "Abbreviation", "Abbreviation");

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
        [Authorize(Roles = "ConsumptionEvent_Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ConsumptionEventViewModel vm)
        {
            ViewData["Unit"] = new SelectList(_activeUnits, "Abbreviation", "Abbreviation");

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