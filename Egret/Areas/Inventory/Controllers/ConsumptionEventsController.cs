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
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ConsumptionEventModel presentation = new ConsumptionEventModel();
            ConsumptionEvent consumptionEvent = Context.ConsumptionEvents
                .Include(i => i.InventoryItemNavigation)
                .SingleOrDefault(m => m.Id == id);

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
                ConsumedBy = User.Identity.Name
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
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ConsumptionEventModel presentation = new ConsumptionEventModel();
            ConsumptionEvent consumptionEvent = Context.ConsumptionEvents
                .Include(i => i.InventoryItemNavigation)
                .SingleOrDefault(m => m.Id == id);

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
        public IActionResult Edit(string id, ConsumptionEventModel vm)
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
                Context.SaveChanges();
                TempData["SuccessMessage"] = "Save Complete";
                return RedirectToAction();
            }

            vm.ConsumptionEvent.UpdatedBy = temp.UpdatedBy;
            vm.ConsumptionEvent.DateUpdated = temp.DateUpdated;
            vm.InventoryItems = new List<InventoryItem>() { temp.InventoryItemNavigation };

            return View(vm);
        }

        [HttpGet]
        [Authorize(Roles = "ConsumptionEvent_Read")]
        public IActionResult Search()
        {
            ViewData["ResultsPerPage"] = SelectListFactory.ResultsPerPage();

            var presentation = new ConsumptionEventSearchModel();

            return View(presentation);
        }

        [HttpGet]
        [Authorize(Roles = "ConsumptionEvent_Edit")]
        public IActionResult Results(ConsumptionEventSearchModel searchModel)
        {
            searchModel.ItemsPerPage = searchModel.ResultsPerPage;
            var results = FindConsumptionSearchResults(searchModel);

            var filterResults = results.Skip((searchModel.CurrentPage - 1) * searchModel.ItemsPerPage).Take(searchModel.ItemsPerPage).ToList();

            var presentation = new ConsumptionEventSearchResultsModel
            {
                CurrentPage = searchModel.CurrentPage,
                Events = filterResults,
                ItemsPerPage = searchModel.ResultsPerPage,
                TotalItems = results.Count()
            };

            return View(presentation);
        }

        [NonAction]
        private List<ConsumptionEvent> FindConsumptionSearchResults(ConsumptionEventSearchModel searchModel)
        {
            var results = Context.ConsumptionEvents
                .AsQueryable()
                .AsNoTracking();

            // Code
            if (!String.IsNullOrEmpty(searchModel.Code))
                results = results.Where(x => x.InventoryItemCode.Contains(searchModel.Code));

            // Date Added
            if (searchModel.DateCreatedStart != null && searchModel.DateCreatedEnd != null)
            {
                results = results.Where(x => x.DateAdded.Value.Date >= searchModel.DateCreatedStart.Value.Date && x.DateAdded.Value.Date <= searchModel.DateCreatedEnd.Value.Date);
            }
            else if (searchModel.DateCreatedStart != null && searchModel.DateCreatedEnd == null)
            {
                results = results.Where(x => x.DateAdded.Value.Date >= searchModel.DateCreatedStart.Value.Date);
            }
            else if (searchModel.DateCreatedStart == null && searchModel.DateCreatedEnd != null)
            {
                results = results.Where(x => x.DateAdded.Value.Date <= searchModel.DateCreatedEnd.Value.Date);
            }

            // Consumed By
            if (!String.IsNullOrEmpty(searchModel.ConsumedBy))
                results = results.Where(x => x.ConsumedBy.Contains(searchModel.ConsumedBy, StringComparison.InvariantCultureIgnoreCase));

            // Order Number
            if (!String.IsNullOrEmpty(searchModel.OrderNumber))
                results = results.Where(x => x.OrderNumber.Contains(searchModel.OrderNumber, StringComparison.InvariantCultureIgnoreCase));

            var realResults = results.OrderBy(x => x.Id).ToList();

            return realResults;
        }

    }

}