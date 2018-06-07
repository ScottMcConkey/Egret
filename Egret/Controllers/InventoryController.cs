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
            var egretContext = Context.InventoryItems
                .Include(i => i.BuycurrencyNavigation)
                .Include(i => i.BuyunitNavigation)
                .Include(i => i.CategoryNavigation)
                .Include(i => i.SellcurrencyNavigation)
                .Include(i => i.SellunitNavigation);
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

            if (CurrencyDefault.Any())
            {
                ViewData["Buycurrency"] = new SelectList(ActiveCurrencyTypes, "Abbreviation", "Abbreviation", CurrencyDefault.First().Abbreviation);
                ViewData["Sellcurrency"] = new SelectList(ActiveCurrencyTypes, "Abbreviation", "Abbreviation", CurrencyDefault.First().Abbreviation);
            }
            else
            {
                ViewData["Buycurrency"] = new SelectList(ActiveCurrencyTypes, "Abbreviation", "Abbreviation");
                ViewData["Sellcurrency"] = new SelectList(ActiveCurrencyTypes, "Abbreviation", "Abbreviation");
            }

            ViewData["Buyunit"] = new SelectList(ActiveUnits, "Abbreviation", "Abbreviation");
            ViewData["Category"] = new SelectList(ActiveInventoryCategories, "Name", "Name");
            ViewData["Sellunit"] = new SelectList(ActiveUnits, "Abbreviation", "Abbreviation");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Code,Description,Category,Comment,Sellprice,Sellcurrency,Sellunit,Buyprice,Buycurrency,Buyunit,Isconversion,Conversionsource,Useraddedby,Userupdatedby,Dateadded,Dateupdated")] InventoryItem inventoryItems)
        {
            if (ModelState.IsValid)
            {
                Context.Add(inventoryItems);
                await Context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Buycurrency"] = new SelectList(ActiveCurrencyTypes, "Abbreviation", "Abbreviation", inventoryItems.Buycurrency);
            ViewData["Buyunit"] = new SelectList(ActiveUnits, "Abbreviation", "Abbreviation", inventoryItems.Buyunit);
            ViewData["Category"] = new SelectList(ActiveInventoryCategories, "Name", "Name", inventoryItems.Category);
            ViewData["Sellcurrency"] = new SelectList(ActiveCurrencyTypes, "Abbreviation", "Abbreviation", inventoryItems.Sellcurrency);
            ViewData["Sellunit"] = new SelectList(ActiveUnits, "Abbreviation", "Abbreviation", inventoryItems.Sellunit);
            return View(inventoryItems);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await Context.InventoryItems.SingleOrDefaultAsync(m => m.Code == id);
            if (item == null)
            {
                return NotFound();
            }
            ViewData["Buycurrency"] = new SelectList(ActiveCurrencyTypes, "Abbreviation", "Abbreviation", item.Buycurrency);
            ViewData["Buyunit"] = new SelectList(ActiveUnits, "Abbreviation", "Abbreviation", item.Buyunit);
            ViewData["Category"] = new SelectList(ActiveInventoryCategories, "Name", "Name", item.Category);
            ViewData["Sellcurrency"] = new SelectList(ActiveCurrencyTypes, "Abbreviation", "Abbreviation", item.Sellcurrency);
            ViewData["Sellunit"] = new SelectList(ActiveUnits, "Abbreviation", "Abbreviation", item.Sellunit);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Code,Description,Category,Comment,Sellprice,Sellcurrency,Sellunit,Buyprice,Buycurrency,Buyunit,Isconversion,Conversionsource,Useraddedby,Userupdatedby,Dateadded,Dateupdated")] InventoryItem item)
        {
            if (ModelState.IsValid)
            {
                Context.Update(item);
                await Context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Save Complete";
            }
            ViewData["Buycurrency"] = new SelectList(ActiveCurrencyTypes, "Abbreviation", "Abbreviation", item.Buycurrency);
            ViewData["Buyunit"] = new SelectList(ActiveUnits, "Abbreviation", "Abbreviation", item.Buyunit);
            ViewData["Category"] = new SelectList(ActiveInventoryCategories, "Name", "Name", item.Category);
            ViewData["Sellcurrency"] = new SelectList(ActiveCurrencyTypes, "Abbreviation", "Abbreviation", item.Sellcurrency);
            ViewData["Sellunit"] = new SelectList(ActiveUnits, "Abbreviation", "Abbreviation", item.Sellunit);
            //return RedirectToAction(nameof(Edit));
            return View(item);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventoryItems = await Context.InventoryItems
                .Include(i => i.BuycurrencyNavigation)
                .Include(i => i.BuyunitNavigation)
                .Include(i => i.CategoryNavigation)
                .Include(i => i.SellcurrencyNavigation)
                .Include(i => i.SellunitNavigation)
                .SingleOrDefaultAsync(m => m.Code == id);
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
            var results = Context.InventoryItems
                .Include(i => i.BuycurrencyNavigation)
                .Include(i => i.BuyunitNavigation)
                .Include(i => i.CategoryNavigation)
                .Include(i => i.SellcurrencyNavigation)
                .Include(i => i.SellunitNavigation)
                .AsQueryable();

            if (!String.IsNullOrEmpty(item.Code))
            {
                results = results.Where(x => x.Code.Contains(item.Code));
            }
            if (!String.IsNullOrEmpty(item.Description))
            {
                results = results.Where(x => x.Description.Contains(item.Description));
            }
            return View("Results", results.ToList());
            //return RedirectToAction(SearchResults(results.ToList()));

        }

        [HttpGet]
        public IActionResult Results(List<InventoryItem> results)
        {
            //var egretContext = _context.InventoryItems
            //    .Include(i => i.BuycurrencyNavigation)
            //    .Include(i => i.BuyunitNavigation)
            //    .Include(i => i.CategoryNavigation)
            //    .Include(i => i.SellcurrencyNavigation)
            //    .Include(i => i.SellunitNavigation);
            return View();
        }

        private bool InventoryItemsExists(string id)
        {
            return Context.InventoryItems.Any(e => e.Code == id);
        }
    }
}
