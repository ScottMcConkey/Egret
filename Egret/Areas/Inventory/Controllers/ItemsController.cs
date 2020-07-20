using Egret.Areas.Inventory.Models;
using Egret.Controllers;
using Egret.Models;
using Egret.Models.Common;
using Egret.Services;
using Egret.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Egret.Areas.Inventory.Controllers
{
    [Area("Inventory")]
    public class ItemsController : Controller
    {
        private static ILogger _logger;
        private static IItemService _itemService;
        private static ISelectListFactoryService _selectListService;

        public ItemsController(ILogger<ItemsController> logger, IItemService itemService, ISelectListFactoryService selectListService)
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
            presentation.FabricTests = item.FabricTestsNavigation.OrderBy(x => x.FabricTestId).ToList();
            presentation.ConsumptionEvents = item.ConsumptionEventsNavigation.ToList();
            SetCurrency();

            return View(presentation);
        }

        [HttpGet]
        [Authorize(Roles = "Item_Create")]
        public IActionResult Create()
        {
            ViewBag.Categories = _selectListService.CategoriesActive();
            ViewBag.Units = _selectListService.UnitsActive();
            ViewBag.StorageLocations = _selectListService.StorageLocationsActive();
            SetCurrency();

            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Item_Create")]
        public IActionResult CreateFromCopy(InventoryItem copy)
        {
            ViewData["Categories"] = _selectListService.CategoriesActive();
            ViewData["Units"] = _selectListService.UnitsActive();
            ViewData["StorageLocations"] = _selectListService.StorageLocationsActive();
            SetCurrency();

            return View("Create", copy);
        }

        [HttpPost]
        [Authorize(Roles = "Item_Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(InventoryItem item)
        {
            if (item.QtyPurchased < 0)
                ModelState.AddModelError("", "Qty Purchased must be a positive number.");

            if (ModelState.IsValid)
            {
                _itemService.CreateItem(item, User);
                TempData["SuccessMessage"] = "Inventory Item Created";

                return RedirectToAction(nameof(Edit), new { Id = item.InventoryItemId });
            }

            ViewData["Categories"] = _selectListService.CategoriesActive(item.InventoryCategoryId);
            ViewData["Unit"] = _selectListService.UnitsActive(item.UnitId);
            ViewData["StorageLocations"] = _selectListService.StorageLocationsActive(item.StorageLocationId);
            SetCurrency();

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

            ViewData["Categories"] = _selectListService.CategoriesActivePlusCurrent(item.CategoryNavigation);
            ViewData["Units"] = _selectListService.UnitsActivePlusCurrent(item.UnitNavigation);
            ViewData["StorageLocations"] = _selectListService.StorageLocationsActive(item.StorageLocationId);
            SetCurrency();

            var viewModel = new ItemModel
            {
                Item = item,
                FabricTests = item.FabricTestsNavigation.OrderBy(x => x.FabricTestId).ToList(),
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

            if (vm.Item.QtyPurchased < 0)
                ModelState.AddModelError("", "Qty Purchased must be a positive number.");

            if (ModelState.IsValid)
            {
                _itemService.DefineFabricTestsForItem(vm.Item, vm.FabricTests);

                _itemService.UpdateItem(vm.Item, User);
                
                TempData["SuccessMessage"] = "Save Complete";

                return RedirectToAction();
            }

            // Rebuild viewmodel
            ViewData["Categories"] = _selectListService.CategoriesActivePlusCurrent(vm.Item.CategoryNavigation);
            ViewData["Units"] = _selectListService.UnitsActivePlusCurrent(vm.Item.UnitNavigation);
            ViewData["StorageLocations"] = _selectListService.StorageLocationsActive(vm.Item.StorageLocationId);
            SetCurrency();

            vm.Item = temp;
            vm.FabricTests = temp.FabricTestsNavigation.ToList();
            vm.ConsumptionEvents = temp.ConsumptionEventsNavigation.ToList();

            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Item_Delete")]
        [ValidateAntiForgeryToken]
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
            ViewBag.ResultsPerPage = _selectListService.ResultsPerPage();
            ViewBag.Categories = _selectListService.CategoriesAll();
            ViewBag.StorageLocations = _selectListService.StorageLocationsAll();

            var presentation = new ItemSearchModel();

            return View(presentation);
        }

        [HttpGet]
        [Authorize(Roles = "Item_Read")]
        public IActionResult Results(ItemSearchModel searchModel)
        {
            searchModel.ItemsPerPage = searchModel.ResultsPerPage;

            var queryObject = new ItemSearchQueryEntity()
            {
                ItemId = searchModel.Code,
                Description = searchModel.Description,
                DateCreatedStart = searchModel.DateCreatedStart,
                DateCreatedEnd = searchModel.DateCreatedEnd,
                Category = searchModel.Category,
                StorageLocation = searchModel.StorageLocation,
                CustomerPurchasedFor = searchModel.CustomerPurchasedFor,
                CustomerReservedFor = searchModel.CustomerReservedFor,
                StockLevel = searchModel.StockLevel,
                ResultsPerPage = searchModel.ResultsPerPage
            };

            var results = _itemService.FindItemSearchResults(queryObject);

            var filterResults = results.Skip((searchModel.CurrentPage - 1) * searchModel.ItemsPerPage).Take(searchModel.ItemsPerPage).ToList();

            var presentation = new ItemSearchResultsModel
            {
                CurrentPage = searchModel.CurrentPage,
                Items = filterResults,
                ItemsPerPage = searchModel.ResultsPerPage,
                TotalItems = (results.Count() > 0 ? results.Count() : 1)
            };

            return View(presentation);
        }

        private void SetCurrency()
        {
            ViewBag.Currency = _itemService.GetSystemCurrency();
        }
    }
}
