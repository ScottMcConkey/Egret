using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Egret.DataAccess;
using Egret.Models;
using Egret.ViewModels;
using System.Diagnostics;

namespace Egret.Controllers
{
    public class InventoryController : ManagedController
    {
        private IQueryable<CurrencyType> ActiveCurrencyTypes { get; set; }
        private IQueryable<Unit> ActiveUnits { get; set; }
        private IQueryable<InventoryCategory> ActiveInventoryCategories { get; set; }

        public InventoryController(EgretContext context) 
            :base(context)
        {
            ActiveCurrencyTypes = Context.CurrencyTypes.Where(x => x.Active == true).OrderBy(x => x.SortOrder);
            ActiveUnits = Context.Units.Where(x => x.Active == true).OrderBy(x => x.SortOrder);
            ActiveInventoryCategories = Context.InventoryCategories.Where(x => x.Active == true).OrderBy(x => x.SortOrder);
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
            ViewData["Buycurrency"] = new SelectList(ActiveCurrencyTypes, "Abbreviation", "Abbreviation", CurrencyDefault.Any() ? CurrencyDefault.First().Abbreviation : "");
            ViewData["Category"] = new SelectList(ActiveInventoryCategories, "Name", "Name");
            ViewData["Unit"] = new SelectList(ActiveUnits, "Abbreviation", "Abbreviation");
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InventoryItem inventoryItem)
        {
            ViewData["Buycurrency"] = new SelectList(ActiveCurrencyTypes, "Abbreviation", "Abbreviation", inventoryItem.BuyCurrency);
            ViewData["Category"] = new SelectList(ActiveInventoryCategories, "Name", "Name", inventoryItem.Category);
            ViewData["Unit"] = new SelectList(ActiveUnits, "Abbreviation", "Abbreviation", inventoryItem.Unit);

            if (ModelState.IsValid)
            {
                inventoryItem.AddedBy = User.Identity.Name;
                inventoryItem.UpdatedBy = User.Identity.Name;
                inventoryItem.DateAdded = DateTime.Now;
                inventoryItem.DateUpdated = DateTime.Now;
                Context.Add(inventoryItem);
                await Context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Inventory Item Created";
                return RedirectToAction(nameof(Index));
            }
            
            return View(inventoryItem);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            InventoryItemViewModel presentation = new InventoryItemViewModel();
            if (id == null)
            {
                return NotFound();
            }

            InventoryItem item = await Context.InventoryItems
                .Include(i => i.ConsumptionEventsNavigation)
                .Include(i => i.FabricTestsNavigation)
                .SingleOrDefaultAsync(m => m.Code == id);

            if (item == null)
            {
                return NotFound();
            }

            ViewData["Buycurrency"] = new SelectList(ActiveCurrencyTypes, "Abbreviation", "Abbreviation", item.BuyCurrency);
            ViewData["Category"] = new SelectList(ActiveInventoryCategories, "Name", "Name", item.Category);
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
            ViewData["BuyCurrency"] = new SelectList(ActiveCurrencyTypes, "Abbreviation", "Abbreviation", vm.Item.BuyCurrency);
            ViewData["Category"] = new SelectList(ActiveInventoryCategories, "Name", "Name", vm.Item.Category);
            ViewData["Unit"] = new SelectList(ActiveUnits, "Abbreviation", "Abbreviation", vm.Item.Unit);

            InventoryItem item = await Context.InventoryItems
                .Include(i => i.ConsumptionEventsNavigation)
                .Include(i => i.FabricTestsNavigation)
                .SingleOrDefaultAsync(m => m.Code == id);

            // Rebuild the view model since not all values will be passed to this action
            //vm.Item = item;
            vm.ConsumptionEvents = item.ConsumptionEventsNavigation.OrderBy(x => x.Id).ToList();
            vm.FabricTests = item.FabricTestsNavigation.ToList();

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

                // Bypass binding to make sure these values are not changed
                Context.Entry(vm.Item).Property(x => x.AddedBy).IsModified = false;
                Context.Entry(vm.Item).Property(x => x.DateAdded).IsModified = false;

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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search(InventorySearchViewModel item)
        {
            var results = Context.InventoryItems.AsQueryable();

            // Code
            if (!String.IsNullOrEmpty(item.Code))
            {
                results = results.Where(x => x.Code.Contains(item.Code));
            }

            // Description
            if (!String.IsNullOrEmpty(item.Description))
            {
                results = results.Where(x => x.Description.Contains(item.Description));
            }

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


            return View("Results", results.OrderBy(x => x.Code).ToList());
        }

        [HttpGet]
        public IActionResult Results(List<InventoryItem> results)
        {
            return View();
        }

        private bool InventoryItemsExists(string id)
        {
            return Context.InventoryItems.Any(e => e.Code == id);
        }
    }
}
