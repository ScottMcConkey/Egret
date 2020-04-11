using Egret.DataAccess;
using Egret.Models;
using Egret.Services;
using Egret.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Egret.Controllers
{
    [Area("Inventory")]
    public class ConsumptionEventsController : BaseController
    {
        private static ILogger _logger;
        private static ISelectListFactoryService _selectListService;
        private static IConsumptionEventService _eventService;

        public ConsumptionEventsController(ILogger<ConsumptionEventsController> logger, 
            ISelectListFactoryService selectListService, IConsumptionEventService consumptionEventService)
        {
            _logger = logger;
            _selectListService = selectListService;
            _eventService = consumptionEventService;
        }

        [HttpGet]
        [Authorize(Roles = "ConsumptionEvent_Read")]
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumptionEvent = _eventService.GetConsumptionEvent(id, true);

            if (consumptionEvent == null)
            {
                return NotFound();
            }

            ConsumptionEventModel presentation = new ConsumptionEventModel()
            {
                ConsumptionEvent = consumptionEvent,
                InventoryItems = new List<InventoryItem>() { consumptionEvent.InventoryItemNavigation }
            };

            return View(presentation);
        }

        [HttpGet]
        [Authorize(Roles = "Item_Read, ConsumptionEvent_Create")]
        public IActionResult CreateFromItem(string sourceId)
        {
            var consumptionEvent = new ConsumptionEvent()
            {
                DateOfConsumption = DateTime.Now,
                ConsumedBy = User.Identity.Name,
                InventoryItemCode = sourceId
            };

            return View(consumptionEvent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Item_Read, ConsumptionEvent_Create")]
        public IActionResult CreateFromItem(string sourceId, ConsumptionEvent consumptionEvent)
        {
            if (ModelState.IsValid)
            {
                consumptionEvent.InventoryItemCode = sourceId;
                _eventService.CreateConsumptionEvent(consumptionEvent, User);
                TempData["SuccessMessage"] = "Consumption Event Created";

                return RedirectToAction("Edit", "Items", new { id = consumptionEvent.InventoryItemCode });
            }

            return View("CreateFromItem", consumptionEvent);
        }

        [HttpGet]
        [Authorize(Roles = "ConsumptionEvent_Create")]
        public IActionResult Create()
        {
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
            if (ModelState.IsValid)
            {
                _eventService.CreateConsumptionEvent(consumptionEvent, User);
                TempData["SuccessMessage"] = "Consumption Event Created";

                return RedirectToAction("Edit", new { id = consumptionEvent.Id });
            }

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

            var consumptionEvent = _eventService.GetConsumptionEvent(id);

            if (consumptionEvent == null)
            {
                return NotFound();
            }

            ConsumptionEventModel presentation = new ConsumptionEventModel()
            {
                ConsumptionEvent = consumptionEvent,
                InventoryItems = new List<InventoryItem>() { consumptionEvent.InventoryItemNavigation }
            };

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

            var temp = _eventService.GetConsumptionEvent(id, true);

            if (temp != null)
            {
                vm.ConsumptionEvent.AddedBy = temp.AddedBy;
                vm.ConsumptionEvent.DateAdded = temp.DateAdded;
                vm.ConsumptionEvent.UpdatedBy = User.Identity.Name;
                vm.ConsumptionEvent.DateUpdated = DateTime.Now;
            }

            if (ModelState.IsValid)
            {
                _eventService.UpdateConsumptionEvent(vm.ConsumptionEvent, User);

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
            ViewData["ResultsPerPage"] = _selectListService.ResultsPerPage();

            var presentation = new ConsumptionEventSearchModel();

            return View(presentation);
        }

        [HttpGet]
        [Authorize(Roles = "ConsumptionEvent_Edit")]
        public IActionResult Results(ConsumptionEventSearchModel searchModel)
        {
            searchModel.ItemsPerPage = searchModel.ResultsPerPage;
            var results = _eventService.FindConsumptionSearchResults(searchModel);

            var filterResults = results.Skip((searchModel.CurrentPage - 1) * searchModel.ItemsPerPage).Take(searchModel.ItemsPerPage).ToList();

            var presentation = new ConsumptionEventSearchResultsModel
            {
                CurrentPage = searchModel.CurrentPage,
                Events = filterResults,
                ItemsPerPage = searchModel.ResultsPerPage,
                TotalItems = (results.Count() > 0 ? results.Count() : 1)
            };

            return View(presentation);
        }

    }

}