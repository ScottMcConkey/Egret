﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Egret.DataAccess;
using Egret.Models;

namespace Egret.Controllers
{
    public class InventoryController : Controller
    {
        private readonly EgretContext _context;
        private IQueryable<CurrencyType> _activeCurrencyTypes;
        private IQueryable<Unit> _activeUnits;
        private IQueryable<InventoryCategory> _activeInventoryCategories;

        public InventoryController(EgretContext context)
        {
            _context = context;
            _activeCurrencyTypes = _context.CurrencyTypes.Where(x => x.Active == true).OrderBy(x => x.Sortorder);
            _activeUnits = _context.Units.Where(x => x.Active == true).OrderBy(x => x.Sortorder);
            _activeInventoryCategories = _context.InventoryCategories.Where(x => x.Active == true).OrderBy(x => x.Sortorder);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
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
            if (id == null)
            {
                return NotFound();
            }

            var inventoryItems = await _context.InventoryItems.SingleOrDefaultAsync(m => m.Code == id);
            if (inventoryItems == null)
            {
                return NotFound();
            }
            ViewData["Buycurrency"] = new SelectList(_activeCurrencyTypes, "Abbreviation", "Abbreviation", inventoryItems.Buycurrency);
            ViewData["Buyunit"] = new SelectList(_context.Units.Where(x => x.Active == true).OrderBy(x => x.Sortorder), "Id", "Abbreviation", inventoryItems.Buyunit);
            ViewData["Category"] = new SelectList(_activeInventoryCategories, "Name", "Name", inventoryItems.Category);
            ViewData["Sellcurrency"] = new SelectList(_activeCurrencyTypes, "Id", "Abbreviation", inventoryItems.Sellcurrency);
            ViewData["Sellunit"] = new SelectList(_activeUnits, "Abbreviation", "Abbreviation", inventoryItems.Sellunit);
            return View(inventoryItems);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Code,Description,Category,Comment,Sellprice,Sellcurrency,Sellunit,Buyprice,Buycurrency,Buyunit,Stockvalue,SupplierFk,Salesacct,Stockacct,Cogacct,Sohcount,Stocktakenewqty,Flags,Qtybrksellprice,Costprice,Isconversion,Conversionsource,Useraddedby,Userupdatedby,Dateadded,Dateupdated")] InventoryItem inventoryItems)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inventoryItems);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventoryItemsExists(inventoryItems.Code))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
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

        // POST: Inventory/Delete/5
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
        public async Task<IActionResult> Search()
        {
            var inventoryItems = _context.InventoryItems
            .Include(i => i.BuycurrencyNavigation)
            .Include(i => i.BuyunitNavigation)
            .Include(i => i.CategoryNavigation)
            .Include(i => i.SellcurrencyNavigation)
            .Include(i => i.SellunitNavigation);

            /*if (inventoryItems == null)
            {
                return NotFound();
            }*/

            return View(await inventoryItems.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> SearchResults()
        {
            var egretContext = _context.InventoryItems
                .Include(i => i.BuycurrencyNavigation)
                .Include(i => i.BuyunitNavigation)
                .Include(i => i.CategoryNavigation)
                .Include(i => i.SellcurrencyNavigation)
                .Include(i => i.SellunitNavigation);
            return View(await egretContext.ToListAsync());
        }

        private bool InventoryItemsExists(string id)
        {
            return _context.InventoryItems.Any(e => e.Code == id);
        }
    }
}
