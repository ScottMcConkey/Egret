using Egret.DataAccess;
using Egret.Models;
using Egret.ViewModels;
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
    public class ItemsController : BaseController
    {
        private IQueryable<CurrencyType> ActiveCurrencyTypes { get; set; }
        private IQueryable<Unit> ActiveUnits { get; set; }
        private IQueryable<InventoryCategory> ActiveInventoryCategories { get; set; }
        private IQueryable<InventoryCategory> AllInventoryCategories { get; set; }
        private static ILogger _logger;

        public ItemsController(EgretContext context, ILogger<ItemsController> logger) 
            :base(context)
        {
            ActiveCurrencyTypes = Context.CurrencyTypes.Where(x => x.Active == true).OrderBy(x => x.SortOrder);
            ActiveUnits = Context.Units.Where(x => x.Active == true).OrderBy(x => x.SortOrder);
            ActiveInventoryCategories = Context.InventoryCategories.Where(x => x.Active == true).OrderBy(x => x.SortOrder);
            AllInventoryCategories = Context.InventoryCategories.OrderBy(x => x.SortOrder);
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            var CurrencyDefault = Context.CurrencyTypes.Where(x => x.DefaultSelection == true);
            ViewData["FOBCostCurrency"] = new SelectList(ActiveCurrencyTypes, "Abbreviation", "Abbreviation", CurrencyDefault.Any() ? CurrencyDefault.First().Abbreviation : "");
            ViewData["ShippingCostCurrency"] = new SelectList(ActiveCurrencyTypes, "Abbreviation", "Abbreviation", CurrencyDefault.Any() ? CurrencyDefault.First().Abbreviation : "");
            ViewData["ImportCostCurrency"] = new SelectList(ActiveCurrencyTypes, "Abbreviation", "Abbreviation", CurrencyDefault.Any() ? CurrencyDefault.First().Abbreviation : "");
            ViewData["Category"] = new SelectList(ActiveInventoryCategories, "Name", "Name");
            ViewData["Unit"] = new SelectList(ActiveUnits, "Abbreviation", "Abbreviation");
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InventoryItem inventoryItem)
        {
            ViewData["FOBCostCurrency"] = new SelectList(ActiveCurrencyTypes, "Abbreviation", "Abbreviation", inventoryItem.FOBCostCurrency);
            ViewData["ShippingCostCurrency"] = new SelectList(ActiveCurrencyTypes, "Abbreviation", "Abbreviation", inventoryItem.ShippingCostCurrency);
            ViewData["ImportCostCurrency"] = new SelectList(ActiveCurrencyTypes, "Abbreviation", "Abbreviation", inventoryItem.ImportCostCurrency);
            ViewData["Category"] = new SelectList(ActiveInventoryCategories, "Name", "Name", inventoryItem.Category);
            ViewData["Unit"] = new SelectList(ActiveUnits, "Abbreviation", "Abbreviation", inventoryItem.Unit);

            inventoryItem.AddedBy = User.Identity.Name;
            inventoryItem.UpdatedBy = User.Identity.Name;
            inventoryItem.DateAdded = DateTime.Now;
            inventoryItem.DateUpdated = DateTime.Now;

            if (ModelState.IsValid)
            {
                Context.Add(inventoryItem);
                await Context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Inventory Item Created";
                return RedirectToAction(nameof(Edit), new { Id = inventoryItem.Code });
            }
            
            return View(inventoryItem);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            InventoryItemViewModel presentation = new InventoryItemViewModel();

            InventoryItem item = await Context.InventoryItems
                .Include(i => i.ConsumptionEventsNavigation)
                .Include(i => i.FabricTestsNavigation)
                .SingleOrDefaultAsync(m => m.Code == id);

            if (item == null || id == null)
            {
                return NotFound();
            }

            ViewData["FOBCostCurrency"] = new SelectList(ActiveCurrencyTypes, "Abbreviation", "Abbreviation", item.FOBCostCurrency);
            ViewData["ShippingCostCurrency"] = new SelectList(ActiveCurrencyTypes, "Abbreviation", "Abbreviation", item.ShippingCostCurrency);
            ViewData["ImportCostCurrency"] = new SelectList(ActiveCurrencyTypes, "Abbreviation", "Abbreviation", item.ImportCostCurrency);

            if (!ActiveInventoryCategories.Any(x => x.Name == item.Category))
            {
                List<InventoryCategory> ActiveAndCurrentCategories = ActiveInventoryCategories.ToList();
                InventoryCategory something = Context.InventoryCategories.Where(x => x.Name == item.Category).SingleOrDefault();
                ActiveAndCurrentCategories.Add(something);
                ViewData["Category"] = new SelectList(ActiveAndCurrentCategories, "Name", "Name", item.Category);
            }
            else
            {
                ViewData["Category"] = new SelectList(ActiveInventoryCategories, "Name", "Name", item.Category);
            }
            
            ViewData["Unit"] = new SelectList(ActiveUnits, "Abbreviation", "Abbreviation", item.Unit);

            presentation.Item = item;
            presentation.FabricTests = item.FabricTestsNavigation.OrderBy(x => x.Id).ToList();
            presentation.ConsumptionEvents = item.ConsumptionEventsNavigation.ToList();

            return View(presentation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, InventoryItemViewModel vm)
        {
            ViewData["FOBCostCurrency"] = new SelectList(ActiveCurrencyTypes, "Abbreviation", "Abbreviation", vm.Item.FOBCostCurrency);
            ViewData["ShippingCostCurrency"] = new SelectList(ActiveCurrencyTypes, "Abbreviation", "Abbreviation", vm.Item.ShippingCostCurrency);
            ViewData["ImportCostCurrency"] = new SelectList(ActiveCurrencyTypes, "Abbreviation", "Abbreviation", vm.Item.ImportCostCurrency);
            var ActiveAndCurrentCategories = ActiveInventoryCategories;
            if (!ActiveInventoryCategories.Any(x => x.Name == vm.Item.Category))
            {
                ActiveAndCurrentCategories.Append(Context.InventoryCategories.Where(x => x.Name == vm.Item.Category).SingleOrDefault());
            }
            ViewData["Category"] = new SelectList(ActiveAndCurrentCategories, "Name", "Name", vm.Item.Category);
            ViewData["Unit"] = new SelectList(ActiveUnits, "Abbreviation", "Abbreviation", vm.Item.Unit);

            List<ConsumptionEvent> events = Context.ConsumptionEvents.Where(x => x.InventoryItemCode == vm.Item.Code).ToList();

            // Rebuild the view model since not all values will be passed to this action
            vm.ConsumptionEvents = events;

            InventoryItem temp = Context.InventoryItems.AsNoTracking().Where(x => x.Code == id).FirstOrDefault();
            vm.Item.AddedBy = temp.AddedBy;
            vm.Item.DateAdded = temp.DateAdded;
            vm.Item.UpdatedBy = temp.UpdatedBy;
            vm.Item.DateUpdated = temp.DateUpdated;

            if (ModelState.IsValid)
            {
                vm.Item.UpdatedBy = User.Identity.Name;
                vm.Item.DateUpdated = DateTime.Now;

                if (vm.FabricTests != null)
                {
                    foreach (FabricTest i in vm.FabricTests)
                    {
                        i.InventoryItemCode = vm.Item.Code;
                        Context.FabricTests.Update(i);
                    }

                    // Get related Fabric Tests from database
                    List<FabricTest> DbTests = Context.FabricTests?.Where(x => x.InventoryItemCode == vm.Item.Code).ToList();
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
                    List<FabricTest> DbTests = Context.FabricTests?.Where(x => x.InventoryItemCode == vm.Item.Code).ToList();
                    foreach (FabricTest ft in DbTests)
                    {
                        Context.FabricTests.Remove(ft);
                    }
                }
                
                Context.InventoryItems.Update(vm.Item);

                await Context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Save Complete";

                return RedirectToAction();
            }

            return View(vm);
        }

        [HttpGet]
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var inventoryItems = await Context.InventoryItems.SingleOrDefaultAsync(m => m.Code == id);
            Context.InventoryItems.Remove(inventoryItems);
            await Context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Search()
        {
            ViewData["Category"] = new SelectList(AllInventoryCategories, "Name", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search(InventorySearchViewModel item)
        {
            ViewData["Category"] = new SelectList(AllInventoryCategories, "Name", "Name");
            var results = Context.InventoryItems.Include(x => x.ConsumptionEventsNavigation).AsQueryable();

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
            {
                results = results.Where(x => x.QtyPurchased - x.ConsumptionEventsNavigation.Select(y => y.QuantityConsumed).Sum() > 0);
            }
            else if (item.InStock == "No")
            {
                results = results.Where(x => x.QtyPurchased - x.ConsumptionEventsNavigation.Select(y => y.QuantityConsumed).Sum() == 0);
            }

            return View("Results", results.OrderBy(x => x.Code).ToList());
        }

        [HttpGet]
        public IActionResult Results(List<InventoryItem> results)
        {
            return View();
        }

    }
}
