﻿using Egret.Models;
using Egret.Services;
using Egret.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Egret.Controllers
{
    [Area("Inventory")]
    public class ItemsController : BaseController
    {
        private static ILogger _logger;
        private static IItemService _itemService;
        private static ISelectListFactoryService _selectListService;

        public ItemsController(IItemService itemService, ILogger<ItemsController> logger, ISelectListFactoryService selectListService)
        {
            _logger = logger;
            _itemService = itemService;
            _selectListService = selectListService;
        }

        [HttpGet]
        [Authorize(Roles = "Item_Read")]
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ItemModel presentation = new ItemModel();

            var item = _itemService.GetItem(id, true);

            if (item == null)
            {
                return NotFound();
            }

            presentation.Item = item;
            presentation.FabricTests = item.FabricTestsNavigation.OrderBy(x => x.Id).ToList();
            presentation.ConsumptionEvents = item.ConsumptionEventsNavigation.ToList();

            return View(presentation);
        }

        [HttpGet]
        [Authorize(Roles = "Item_Create")]
        public IActionResult Create()
        {
            ViewData["Category"] = _selectListService.CategoriesActive();
            ViewData["Unit"] = _selectListService.UnitsActive();
            ViewData["FOBCostCurrency"] = _selectListService.CurrencyTypesActive();
            ViewData["ShippingCostCurrency"] = _selectListService.CurrencyTypesActive();
            ViewData["ImportCostCurrency"] = _selectListService.CurrencyTypesActive();

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Item_Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(InventoryItem item)
        {
            if (ModelState.IsValid)
            {
                var userId = _itemService.CreateItem(item, User);
                if (!string.IsNullOrWhiteSpace(userId))
                {
                    TempData["SuccessMessage"] = "Inventory Item Created";
                }
                else
                {
                    TempData["WarningMessage"] = "There was an error creating your item. Please try again";
                    return View(item);
                }

                return RedirectToAction(nameof(Edit), new { Id = userId });
            }

            ViewData["Category"] = _selectListService.CategoriesActive(item.CategoryId);
            ViewData["Unit"] = _selectListService.UnitsActive(item.UnitId);
            ViewData["FOBCostCurrency"] = _selectListService.CurrencyTypesActive(item.FOBCostCurrencyId);
            ViewData["ShippingCostCurrency"] = _selectListService.CurrencyTypesActive(item.ShippingCostCurrencyId);
            ViewData["ImportCostCurrency"] = _selectListService.CurrencyTypesActive(item.ImportCostCurrencyId);

            return View(item);
        }

        [HttpGet]
        [Authorize(Roles = "Item_Edit")]
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = _itemService.GetItem(id);

            if (item == null)
            {
                return NotFound();
            }

            ViewData["Category"] = _selectListService.CategoriesActivePlusCurrent(item.CategoryNavigation);
            ViewData["Unit"] = _selectListService.UnitsActivePlusCurrent(item.UnitNavigation);
            ViewData["FOBCostCurrency"] = _selectListService.CurrencyTypesPlusCurrent(item.FOBCostCurrencyNavigation);
            ViewData["ShippingCostCurrency"] = _selectListService.CurrencyTypesPlusCurrent(item.ShippingCostCurrencyNavigation);
            ViewData["ImportCostCurrency"] = _selectListService.CurrencyTypesPlusCurrent(item.ImportCostCurrencyNavigation);

            var viewModel = new ItemModel
            {
                Item = item,
                FabricTests = item.FabricTestsNavigation.OrderBy(x => x.Id).ToList(),
                ConsumptionEvents = item.ConsumptionEventsNavigation.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Item_Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, ItemModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }

            var temp = _itemService.GetItem(id, true);

            if (temp != null)
            {
                vm.Item.AddedBy = temp.AddedBy;
                vm.Item.DateAdded = temp.DateAdded;
                vm.Item.UpdatedBy = User.Identity.Name;
                vm.Item.DateUpdated = DateTime.Now;
            }
            
            if (ModelState.IsValid)
            {
                _itemService.DefineFabricTestsForItem(vm.Item, vm.FabricTests);

                _itemService.UpdateItem(vm.Item, User);
                
                TempData["SuccessMessage"] = "Save Complete";

                return RedirectToAction();
            }

            // Rebuild viewmodel
            ViewData["Category"] = _selectListService.CategoriesActivePlusCurrent(vm.Item.CategoryNavigation);
            ViewData["Unit"] = _selectListService.UnitsActivePlusCurrent(vm.Item.UnitNavigation);
            ViewData["FOBCostCurrency"] = _selectListService.CurrencyTypesPlusCurrent(vm.Item.FOBCostCurrencyNavigation);
            ViewData["ShippingCostCurrency"] = _selectListService.CurrencyTypesPlusCurrent(vm.Item.ShippingCostCurrencyNavigation);
            ViewData["ImportCostCurrency"] = _selectListService.CurrencyTypesPlusCurrent(vm.Item.ImportCostCurrencyNavigation);

            //vm.FabricTests = temp.FabricTestsNavigation.ToList();
            //vm.ConsumptionEvents = temp.ConsumptionEventsNavigation.ToList();

            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Item_Delete")]
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventoryItem = _itemService.GetItem(id);

            if (inventoryItem == null)
            {
                return NotFound();
            }

            _itemService.DeleteItem(id);

            TempData["SuccessMessage"] = $"Item {id} Deleted";

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize(Roles = "Item_Read")]
        public IActionResult Search()
        {
            ViewData["ResultsPerPage"] = _selectListService.ResultsPerPage();
            ViewData["Category"] = _selectListService.CategoriesAll();

            var presentation = new ItemSearchModel();

            return View(presentation);
        }

        [HttpGet]
        [Authorize(Roles = "Item_Read")]
        public IActionResult Results(ItemSearchModel searchModel)
        {
            searchModel.ItemsPerPage = searchModel.ResultsPerPage;
            var results = _itemService.FindItemSearchResults(searchModel);

            var filterResults = results.Skip((searchModel.CurrentPage - 1) * searchModel.ItemsPerPage).Take(searchModel.ItemsPerPage).ToList();

            var presentation = new ItemSearchResultsModel
            {
                CurrentPage = searchModel.CurrentPage,
                Items = filterResults,
                ItemsPerPage = searchModel.ResultsPerPage,
                TotalItems = results.Count()
            };

            return View(presentation);
        }
    }
}
