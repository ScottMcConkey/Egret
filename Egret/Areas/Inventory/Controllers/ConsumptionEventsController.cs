using Egret.Utilities;
using Egret.DataAccess;
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
        private static ILogger _logger;

        public ConsumptionEventsController(EgretContext context, ILogger<ConsumptionEventsController> logger)
            : base(context)
        {
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "ConsumptionEvent_Read")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ConsumptionEventModel presentation = new ConsumptionEventModel();
            ConsumptionEvent consumptionEvent = await Context.ConsumptionEvents
                .Include(i => i.InventoryItemNavigation)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (consumptionEvent == null)
            {
                return NotFound();
            }

            presentation.ConsumptionEvent = consumptionEvent;
            presentation.InventoryItems = new List<InventoryItem>() { consumptionEvent.InventoryItemNavigation };

            return View(presentation);
        }

        [HttpGet]
        [Authorize(Roles = "Item_Read, ConsumptionEvent_Create")]
        public IActionResult CreateFromItem(string sourceid)
        {
            var item = Context.InventoryItems.Where(x => x.Code == sourceid);

            // Set Defaults
            var consumptionEvent = new ConsumptionEvent()
            {
                DateOfConsumption = DateTime.Now,
                ConsumedBy = User.Identity.Name,
                Unit = item.SingleOrDefault().Unit
            };

            return View(consumptionEvent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Item_Read, ConsumptionEvent_Create")]
        public IActionResult CreateFromItem(string sourceid, ConsumptionEvent consumptionEvent)
        {
            string sourceId = sourceid;

            var item = Context.InventoryItems.Where(x => x.Code == consumptionEvent.InventoryItemCode);

            consumptionEvent.AddedBy = User.Identity.Name;
            consumptionEvent.UpdatedBy = User.Identity.Name;
            consumptionEvent.DateAdded = DateTime.Now;
            consumptionEvent.DateUpdated = DateTime.Now;

            if (ModelState.IsValid)
            {
                Context.ConsumptionEvents.Add(consumptionEvent);
                Context.SaveChanges();
                TempData["SuccessMessage"] = "Consumption Event Created";

                return RedirectToAction("Edit", "Items", new { id = consumptionEvent.InventoryItemCode });
            }

            // Set Defaults
            consumptionEvent.DateOfConsumption = DateTime.Now;
            consumptionEvent.ConsumedBy = User.Identity.Name;

            return View("CreateFromItem", consumptionEvent);
        }

        [HttpGet]
        [Authorize(Roles = "ConsumptionEvent_Create")]
        public IActionResult Create()
        {
            // Set Defaults
            var consumptionEvent = new ConsumptionEvent()
            {
                DateOfConsumption = DateTime.Now,
                ConsumedBy = User.Identity.Name
            };

            return View(consumptionEvent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ConsumptionEvent_Create")]
        public IActionResult Create(ConsumptionEvent consumptionEvent)
        {
            consumptionEvent.AddedBy = User.Identity.Name;
            consumptionEvent.UpdatedBy = User.Identity.Name;
            consumptionEvent.DateAdded = DateTime.Now;
            consumptionEvent.DateUpdated = DateTime.Now;

            if (ModelState.IsValid)
            {
                Context.ConsumptionEvents.Add(consumptionEvent);
                Context.SaveChanges();
                TempData["SuccessMessage"] = "Consumption Event Created";

                return RedirectToAction("Edit", new { id = consumptionEvent.Id });
            }

            // Set Defaults
            consumptionEvent.DateOfConsumption = DateTime.Now;
            consumptionEvent.ConsumedBy = User.Identity.Name;

            return View();
        }

        [HttpGet]
        [Authorize(Roles = "ConsumptionEvent_Edit")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ConsumptionEventModel presentation = new ConsumptionEventModel();
            ConsumptionEvent consumptionEvent = await Context.ConsumptionEvents
                .Include(i => i.InventoryItemNavigation)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (consumptionEvent == null)
            {
                return NotFound();
            }

            presentation.ConsumptionEvent = consumptionEvent;
            presentation.InventoryItems = new List<InventoryItem>() { consumptionEvent.InventoryItemNavigation };

            return View(presentation);
        }

        [HttpPost]
        [Authorize(Roles = "ConsumptionEvent_Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ConsumptionEventModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }

            var temp = Context.ConsumptionEvents
                .Include(y => y.InventoryItemNavigation)
                .AsNoTracking()
                .Where(x => x.Id == id)
                .FirstOrDefault();

            vm.ConsumptionEvent.AddedBy = temp.AddedBy;
            vm.ConsumptionEvent.DateAdded = temp.DateAdded;
            vm.ConsumptionEvent.UpdatedBy = User.Identity.Name;
            vm.ConsumptionEvent.DateUpdated = DateTime.Now;

            if (ModelState.IsValid)
            {
                Context.ConsumptionEvents.Update(vm.ConsumptionEvent);
                await Context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Save Complete";
                return RedirectToAction();
            }

            vm.ConsumptionEvent.UpdatedBy = temp.UpdatedBy;
            vm.ConsumptionEvent.DateUpdated = temp.DateUpdated;
            vm.InventoryItems = new List<InventoryItem>() { temp.InventoryItemNavigation };

            return View(vm);
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

            return View("Results", results.OrderByDescending(x => x.DateOfConsumption).ToList());
        }

    }

}