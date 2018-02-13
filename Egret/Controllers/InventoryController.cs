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
        public readonly string BackButtonText = "Back to Inventory";

        private IQueryable<CurrencyType> _activeCurrencyTypes;
        private IQueryable<Unit> _activeUnits;
        private IQueryable<InventoryCategory> _activeInventoryCategories;

        public InventoryController(EgretContext context) :base(context)
        {
            _activeCurrencyTypes = _context.CurrencyTypes.Where(x => x.Active == true).OrderBy(x => x.Sortorder);
            _activeUnits = _context.Units.Where(x => x.Active == true).OrderBy(x => x.Sortorder);
            _activeInventoryCategories = _context.InventoryCategories.Where(x => x.Active == true).OrderBy(x => x.Sortorder);
            var egretContext = _context.InventoryItems
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
            ViewData["BackText"] = BackButtonText;

            var CurrencyDefault = _context.CurrencyTypes.Where(x => x.Defaultselection == true);

            if (CurrencyDefault.Any())
            {
                ViewData["Buycurrency"] = new SelectList(_activeCurrencyTypes, "Abbreviation", "Abbreviation", CurrencyDefault.First().Abbreviation);
                ViewData["Sellcurrency"] = new SelectList(_activeCurrencyTypes, "Abbreviation", "Abbreviation", CurrencyDefault.First().Abbreviation);
            }
            else
            {
                ViewData["Buycurrency"] = new SelectList(_activeCurrencyTypes, "Abbreviation", "Abbreviation");
                ViewData["Sellcurrency"] = new SelectList(_activeCurrencyTypes, "Abbreviation", "Abbreviation");
            }

            ViewData["Buyunit"] = new SelectList(_activeUnits, "Abbreviation", "Abbreviation");
            ViewData["Category"] = new SelectList(_activeInventoryCategories, "Name", "Name");
            ViewData["Sellunit"] = new SelectList(_activeUnits, "Abbreviation", "Abbreviation");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Code,Description,Category,Comment,Sellprice,Sellcurrency,Sellunit,Buyprice,Buycurrency,Buyunit,Stockvalue,SupplierFk,Salesacct,Stockacct,Cogacct,Sohcount,Stocktakenewqty,Flags,Qtybrksellprice,Costprice,Isconversion,Conversionsource,Useraddedby,Userupdatedby,Dateadded,Dateupdated")] InventoryItem inventoryItems)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inventoryItems);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Buycurrency"] = new SelectList(_activeCurrencyTypes, "Abbreviation", "Abbreviation", inventoryItems.Buycurrency);
            ViewData["Buyunit"] = new SelectList(_activeUnits, "Abbreviation", "Abbreviation", inventoryItems.Buyunit);
            ViewData["Category"] = new SelectList(_activeInventoryCategories, "Name", "Name", inventoryItems.Category);
            ViewData["Sellcurrency"] = new SelectList(_activeCurrencyTypes, "Abbreviation", "Abbreviation", inventoryItems.Sellcurrency);
            ViewData["Sellunit"] = new SelectList(_activeUnits, "Abbreviation", "Abbreviation", inventoryItems.Sellunit);
            return View(inventoryItems);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            ViewData["BackText"] = BackButtonText;

            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.InventoryItems.SingleOrDefaultAsync(m => m.Code == id);
            if (item == null)
            {
                return NotFound();
            }
            ViewData["Buycurrency"] = new SelectList(_activeCurrencyTypes, "Abbreviation", "Abbreviation", item.Buycurrency);
            ViewData["Buyunit"] = new SelectList(_activeUnits, "Id", "Abbreviation", item.Buyunit);
            ViewData["Category"] = new SelectList(_activeInventoryCategories, "Name", "Name", item.Category);
            ViewData["Sellcurrency"] = new SelectList(_activeCurrencyTypes, "Abbreviation", "Abbreviation", item.Sellcurrency);
            ViewData["Sellunit"] = new SelectList(_activeUnits, "Abbreviation", "Abbreviation", item.Sellunit);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Code,Description,Category,Comment,Sellprice,Sellcurrency,Sellunit,Buyprice,Buycurrency,Buyunit,Stockvalue,SupplierFk,Salesacct,Stockacct,Cogacct,Sohcount,Stocktakenewqty,Flags,Qtybrksellprice,Costprice,Isconversion,Conversionsource,Useraddedby,Userupdatedby,Dateadded,Dateupdated")] InventoryItem item)
        {

            ViewData["BackText"] = BackButtonText;

            if (ModelState.IsValid)
            {
                _context.Update(item);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Save Complete";
            }
            ViewData["Buycurrency"] = new SelectList(_activeCurrencyTypes, "Abbreviation", "Abbreviation", item.Buycurrency);
            ViewData["Buyunit"] = new SelectList(_activeUnits, "Id", "Abbreviation", item.Buyunit);
            ViewData["Category"] = new SelectList(_activeInventoryCategories, "Name", "Name", item.Category);
            ViewData["Sellcurrency"] = new SelectList(_activeCurrencyTypes, "Abbreviation", "Abbreviation", item.Sellcurrency);
            ViewData["Sellunit"] = new SelectList(_activeUnits, "Abbreviation", "Abbreviation", item.Sellunit);
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

            var inventoryItems = await _context.InventoryItems
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
            var inventoryItems = await _context.InventoryItems.SingleOrDefaultAsync(m => m.Code == id);
            _context.InventoryItems.Remove(inventoryItems);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Search()
        {
            ViewData["BackText"] = BackButtonText;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search(InventorySearchViewModel item)
        {
            ViewData["BackText"] = BackButtonText;

            var results = _context.InventoryItems
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
            ViewData["BackText"] = BackButtonText;

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
            return _context.InventoryItems.Any(e => e.Code == id);
        }
    }
}
