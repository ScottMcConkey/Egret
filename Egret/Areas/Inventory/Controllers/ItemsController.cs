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

        [HttpGet]
        [Authorize(Roles = "Item_Delete")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventoryItems = await Context.InventoryItems.SingleOrDefaultAsync(m => m.Code == id);

            if (inventoryItems == null)
            {
                return NotFound();
            }

            return View(inventoryItems);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Item_Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var inventoryItems = await Context.InventoryItems.SingleOrDefaultAsync(m => m.Code == id);
            Context.InventoryItems.Remove(inventoryItems);
            await Context.SaveChangesAsync();
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        [Authorize(Roles = "Item_Read")]
        public IActionResult Search()
        {
            ViewData["ResultsPerPage"] = DropDownFactory.ResultsPerPage();
            ViewData["Category"] = new SelectListFactory(Context).CategoriesAll();

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Item_Read")]
        [ValidateAntiForgeryToken]
        public IActionResult Search(ItemSearchModel item)
        {
            ViewData["ResultsPerPage"] = DropDownFactory.ResultsPerPage();
            ViewData["Category"] = new SelectListFactory(Context).CategoriesAll();

            var results = Context.InventoryItems
                .Include(x => x.ConsumptionEventsNavigation)
                .AsQueryable();

            // Code
            if (!String.IsNullOrEmpty(item.Code))
                results = results.Where(x => x.Code.Contains(item.Code));

            // Description
            if (!String.IsNullOrEmpty(item.Description))
                results = results.Where(x => x.Description.Contains(item.Description));

            // Date Added
            if (item.DateCreatedStart != null && item.DateCreatedEnd != null)
            {
                results = results.Where(x => x.DateAdded.Value.Date >= item.DateCreatedStart.Value.Date && x.DateAdded.Value.Date <= item.DateCreatedEnd.Value.Date);
            }
            else if (item.DateCreatedStart != null && item.DateCreatedEnd == null)
            {
                results = results.Where(x => x.DateAdded.Value.Date >= item.DateCreatedStart.Value.Date);
            }
            else if (item.DateCreatedStart == null && item.DateCreatedEnd != null)
            {
                results = results.Where(x => x.DateAdded.Value.Date <= item.DateCreatedEnd.Value.Date);
            }

            // Category
            if (!String.IsNullOrEmpty(item.Category))
                results = results.Where(x => x.Category == item.Category);

            // Customer Purchased For
            if (!String.IsNullOrEmpty(item.CustomerPurchasedFor))
                results = results.Where(x => x.CustomerPurchasedFor.Contains(item.CustomerPurchasedFor));

            // Customer Reserved For
            if (!String.IsNullOrEmpty(item.CustomerReservedFor))
                results = results.Where(x => x.CustomerReservedFor.Contains(item.CustomerReservedFor));

            // In Stock
            if (item.InStock == "Yes")
                results = results.Where(x => x.QtyPurchased - x.ConsumptionEventsNavigation.Select(y => y.QuantityConsumed).Sum() > 0);
            else if (item.InStock == "No")
                results = results.Where(x => x.QtyPurchased - x.ConsumptionEventsNavigation.Select(y => y.QuantityConsumed).Sum() == 0);

            var realResults = results.OrderBy(x => x.Code).ToList();

            var presentation = new ItemSearchResultsModel();
            presentation.Count = realResults.Count();
            presentation.CurrentPage = 1;
            presentation.ResultsPerPage = item.ResultsPerPage;
            presentation.Items = realResults.Skip((presentation.CurrentPage - 1) * presentation.ResultsPerPage).Take(presentation.ResultsPerPage).ToList();

            //return View(nameof(Results), presentation);
            //return RedirectToAction("Results", "Index", new ItemSearchResultsModel() = presentation);
            return View(nameof(Results), presentation);
        }

        [HttpGet]
        [Authorize(Roles = "Item_Read")]
        public IActionResult Results(ItemSearchResultsModel results)
        {
            var presentation = new ItemSearchResultsModel();
            //presentation.Items = results;

            return View(results);
        }

        [HttpPost]
        [Authorize(Roles = "Item_Read")]
        public IActionResult Results(ItemSearchResultsModel results, int page)
        {
            results.CurrentPage = page;

            return View(results);
        }

    }
}
