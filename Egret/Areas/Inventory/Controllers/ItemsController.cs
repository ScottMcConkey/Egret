using Egret.Utilities;
using Egret.DataAccess;
using Egret.Models;
using Egret.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Http.Extensions;

namespace Egret.Controllers
{
    [Area("Inventory")]
    public class ItemsController : BaseController
    {
        private static ILogger _logger;

        public ItemsController(EgretContext context, ILogger<ItemsController> logger) 
            :base(context)
        {
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "Item_Read")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ItemModel presentation = new ItemModel();

            InventoryItem item = await Context.InventoryItems
                .Where(i => i.Code == id)
                .Include(i => i.ConsumptionEventsNavigation)
                .Include(i => i.FabricTestsNavigation)
                .SingleOrDefaultAsync(m => m.Code == id);

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
            ViewData["FOBCostCurrency"] = new SelectListFactory(Context).CurrencyTypesActive();
            ViewData["ShippingCostCurrency"] = new SelectListFactory(Context).CurrencyTypesActive();
            ViewData["ImportCostCurrency"] = new SelectListFactory(Context).CurrencyTypesActive();
            ViewData["Category"] = new SelectListFactory(Context).CategoriesActive();
            ViewData["Unit"] = new SelectListFactory(Context).UnitsActive();

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Item_Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InventoryItem inventoryItem)
        {
            inventoryItem.AddedBy = User.Identity.Name;
            inventoryItem.UpdatedBy = User.Identity.Name;
            inventoryItem.DateAdded = DateTime.Now;
            inventoryItem.DateUpdated = DateTime.Now;

            if (ModelState.IsValid)
            {
                Context.Add(inventoryItem);
                await Context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Inventory Item Created";

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _logger.LogInformation($"Item {inventoryItem.Code} created by user {userId}");

                return RedirectToAction(nameof(Edit), new { Id = inventoryItem.Code });
            }

            ViewData["FOBCostCurrency"] = new SelectListFactory(Context).CurrencyTypesActive(inventoryItem.FOBCostCurrency);
            ViewData["ShippingCostCurrency"] = new SelectListFactory(Context).CurrencyTypesActive(inventoryItem.ShippingCostCurrency);
            ViewData["ImportCostCurrency"] = new SelectListFactory(Context).CurrencyTypesActive(inventoryItem.ImportCostCurrency);
            ViewData["Category"] = new SelectListFactory(Context).CategoriesActive(inventoryItem.Category);
            ViewData["Unit"] = new SelectListFactory(Context).UnitsActive(inventoryItem.Unit);

            return View(inventoryItem);
        }

        [HttpGet]
        [Authorize(Roles = "Item_Edit")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ItemModel presentation = new ItemModel();
            InventoryItem item = await Context.InventoryItems
                .Where(i => i.Code == id)
                .Include(i => i.CategoryNavigation)
                .Include(i => i.UnitNavigation)
                .Include(i => i.ConsumptionEventsNavigation)
                .Include(i => i.FabricTestsNavigation)
                .Include(i => i.FOBCostCurrencyNavigation)
                .Include(i => i.ShippingCostCurrencyNavigation)
                .Include(i => i.ImportCostCurrencyNavigation)
                .SingleOrDefaultAsync(m => m.Code == id);

            if (item == null)
            {
                return NotFound();
            }

            ViewData["Category"] = new SelectListFactory(Context).CategoriesActivePlusCurrent(item.CategoryNavigation);
            ViewData["Unit"] = new SelectListFactory(Context).UnitsActivePlusCurrent(item.UnitNavigation);
            ViewData["FOBCostCurrency"] = new SelectListFactory(Context).CurrencyTypesPlusCurrent(item.FOBCostCurrencyNavigation);
            ViewData["ShippingCostCurrency"] = new SelectListFactory(Context).CurrencyTypesPlusCurrent(item.ShippingCostCurrencyNavigation);
            ViewData["ImportCostCurrency"] = new SelectListFactory(Context).CurrencyTypesPlusCurrent(item.ImportCostCurrencyNavigation);

            presentation.Item = item;
            presentation.FabricTests = item.FabricTestsNavigation.OrderBy(x => x.Id).ToList();
            presentation.ConsumptionEvents = item.ConsumptionEventsNavigation.ToList();

            return View(presentation);
        }

        [HttpPost]
        [Authorize(Roles = "Item_Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ItemModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }

            InventoryItem temp = Context.InventoryItems
                .Include(y => y.CategoryNavigation)
                .Include(y => y.UnitNavigation)
                .Include(y => y.FabricTestsNavigation)
                .Include(y => y.ConsumptionEventsNavigation)
                .Include(i => i.FOBCostCurrencyNavigation)
                .Include(i => i.ShippingCostCurrencyNavigation)
                .Include(i => i.ImportCostCurrencyNavigation)
                .AsNoTracking()
                .Where(x => x.Code == id)
                .FirstOrDefault();

            vm.Item.AddedBy = temp.AddedBy;
            vm.Item.DateAdded = temp.DateAdded;
            vm.Item.UpdatedBy = User.Identity.Name;
            vm.Item.DateUpdated = DateTime.Now;

            if (ModelState.IsValid)
            {
                if (vm.FabricTests != null)
                {
                    // Set item Code on tests
                    foreach (FabricTest i in vm.FabricTests)
                    {
                        i.InventoryItemCode = vm.Item.Code;
                        Context.FabricTests.Update(i);
                    }

                    // Get related Fabric Tests from database
                    List<FabricTest> DbTests = Context.FabricTests.Where(x => x.InventoryItemCode == vm.Item.Code).ToList();
                    foreach (FabricTest test in DbTests)
                    {
                        if (!vm.FabricTests.Contains(test))
                        {
                            Context.FabricTests.Remove(test);
                        }
                    }
                }
                else
                {
                    // Remove all tests - model is authority
                    List<FabricTest> DbTests = Context.FabricTests.Where(x => x.InventoryItemCode == vm.Item.Code).ToList();
                    foreach (FabricTest test in DbTests)
                    {
                        Context.FabricTests.Remove(test);
                    }
                }

                Context.InventoryItems.Update(vm.Item);

                await Context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Save Complete";

                return RedirectToAction();
            }

            // Rebuild viewmodel
            ViewData["Category"] = new SelectListFactory(Context).CategoriesActivePlusCurrent(vm.Item.CategoryNavigation);
            ViewData["Unit"] = new SelectListFactory(Context).UnitsActivePlusCurrent(vm.Item.UnitNavigation);
            ViewData["FOBCostCurrency"] = new SelectListFactory(Context).CurrencyTypesPlusCurrent(vm.Item.FOBCostCurrencyNavigation);
            ViewData["ShippingCostCurrency"] = new SelectListFactory(Context).CurrencyTypesPlusCurrent(vm.Item.ShippingCostCurrencyNavigation);
            ViewData["ImportCostCurrency"] = new SelectListFactory(Context).CurrencyTypesPlusCurrent(vm.Item.ImportCostCurrencyNavigation);

            vm.FabricTests = temp.FabricTestsNavigation.ToList();
            vm.ConsumptionEvents = temp.ConsumptionEventsNavigation.ToList();

            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Item_Delete")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventoryItem = await Context.InventoryItems.Where(x => x.Code == id).SingleOrDefaultAsync();

            if (inventoryItem == null)
            {
                return NotFound();
            }

            Context.InventoryItems.Remove(inventoryItem);
            Context.SaveChanges();
            TempData["SuccessMessage"] = $"Item {id} Deleted";

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize(Roles = "Item_Read")]
        public IActionResult Search()
        {
            ViewData["ResultsPerPage"] = DropDownFactory.ResultsPerPage();
            ViewData["Category"] = new SelectListFactory(Context).CategoriesAll();

            var presentation = new ItemSearchModel();

            return View(presentation);
        }

        [HttpGet]
        [Authorize(Roles = "Item_Read")]
        public IActionResult Results(ItemSearchModel searchModel)
        {
            searchModel.ItemsPerPage = searchModel.ResultsPerPage;
            var results = FindItemSearchResults(searchModel);

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

        [NonAction]
        private List<InventoryItem> FindItemSearchResults(ItemSearchModel searchModel)
        {
            var results = Context.InventoryItems
                .Include(x => x.ConsumptionEventsNavigation)
                .AsQueryable()
                .AsNoTracking();

            // Code
            if (!String.IsNullOrEmpty(searchModel.Code))
                results = results.Where(x => x.Code.Contains(searchModel.Code));

            // Description
            if (!String.IsNullOrEmpty(searchModel.Description))
                results = results.Where(x => x.Description.Contains(searchModel.Description));

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

            // Category
            if (!String.IsNullOrEmpty(searchModel.Category))
                results = results.Where(x => x.Category == searchModel.Category);

            // Customer Purchased For
            if (!String.IsNullOrEmpty(searchModel.CustomerPurchasedFor))
                results = results.Where(x => x.CustomerPurchasedFor.Contains(searchModel.CustomerPurchasedFor));

            // Customer Reserved For
            if (!String.IsNullOrEmpty(searchModel.CustomerReservedFor))
                results = results.Where(x => x.CustomerReservedFor.Contains(searchModel.CustomerReservedFor));

            // In Stock
            if (searchModel.InStock == "Yes")
                results = results.Where(x => x.QtyPurchased - x.ConsumptionEventsNavigation.Select(y => y.QuantityConsumed).Sum() > 0);
            else if (searchModel.InStock == "No")
                results = results.Where(x => x.QtyPurchased - x.ConsumptionEventsNavigation.Select(y => y.QuantityConsumed).Sum() == 0);

            var realResults = results.OrderBy(x => x.Code).ToList();

            return realResults;
        }

    }
}
