using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Egret.DataAccess;

namespace Egret.Controllers
{
    public class InventoryController : Controller
    {
        private readonly EgretContext _context;

        public InventoryController(EgretContext context)
        {
            _context = context;
        }

        // GET: Inventory
        public async Task<IActionResult> Index()
        {
            var egretContext = _context.InventoryItems.Include(i => i.BuycurrencyNavigation).Include(i => i.BuyunitNavigation).Include(i => i.CategoryNavigation).Include(i => i.SellcurrencyNavigation).Include(i => i.SellunitNavigation);
            return View(await egretContext.ToListAsync());
        }

        // GET: Inventory/Details/5
        public async Task<IActionResult> Details(string id)
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

        // GET: Inventory/Create
        public IActionResult Create()
        {
            ViewData["Buycurrency"] = new SelectList(_context.CurrencyTypes.OrderBy(x => x.Sortorder), "Abbreviation", "Abbreviation", _context.CurrencyTypes.Where(x => x.Defaultselection == true).First().Abbreviation);
            ViewData["Buyunit"] = new SelectList(_context.Units, "Abbreviation", "Abbreviation");
            ViewData["Category"] = new SelectList(_context.InventoryCategories.Where(x => x.Active == true).OrderBy(x => x.Sortorder), "Name", "Name");
            ViewData["Sellcurrency"] = new SelectList(_context.CurrencyTypes.OrderBy(x => x.Sortorder), "Abbreviation", "Abbreviation", _context.CurrencyTypes.Where(x => x.Defaultselection == true).First().Abbreviation);
            ViewData["Sellunit"] = new SelectList(_context.Units, "Abbreviation", "Abbreviation");
            return View();
        }

        // POST: Inventory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
            ViewData["Buycurrency"] = new SelectList(_context.CurrencyTypes, "Abbreviation", "Abbreviation", inventoryItems.Buycurrency);
            ViewData["Buyunit"] = new SelectList(_context.Units, "Abbreviation", "Abbreviation", inventoryItems.Buyunit);
            ViewData["Category"] = new SelectList(_context.InventoryCategories.Where(x => x.Active == true), "Name", "Name", inventoryItems.Category);
            ViewData["Sellcurrency"] = new SelectList(_context.CurrencyTypes, "Abbreviation", "Abbreviation", inventoryItems.Sellcurrency);
            ViewData["Sellunit"] = new SelectList(_context.Units, "Abbreviation", "Abbreviation", inventoryItems.Sellunit);
            return View(inventoryItems);
        }

        // GET: Inventory/Edit/5
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
            ViewData["Buycurrency"] = new SelectList(_context.CurrencyTypes, "Abbreviation", "Abbreviation", inventoryItems.Buycurrency);
            ViewData["Buyunit"] = new SelectList(_context.Units, "Abbreviation", "Abbreviation", inventoryItems.Buyunit);
            ViewData["Category"] = new SelectList(_context.InventoryCategories.Where(x => x.Active == true).OrderBy(x => x.Sortorder), "Name", "Name", inventoryItems.Category);
            ViewData["Sellcurrency"] = new SelectList(_context.CurrencyTypes, "Abbreviation", "Abbreviation", inventoryItems.Sellcurrency);
            ViewData["Sellunit"] = new SelectList(_context.Units, "Abbreviation", "Abbreviation", inventoryItems.Sellunit);
            return View(inventoryItems);
        }

        // POST: Inventory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
            ViewData["Buycurrency"] = new SelectList(_context.CurrencyTypes, "Abbreviation", "Abbreviation", inventoryItems.Buycurrency);
            ViewData["Buyunit"] = new SelectList(_context.Units, "Abbreviation", "Abbreviation", inventoryItems.Buyunit);
            ViewData["Category"] = new SelectList(_context.InventoryCategories, "Name", "Name", inventoryItems.Category);
            ViewData["Sellcurrency"] = new SelectList(_context.CurrencyTypes, "Abbreviation", "Abbreviation", inventoryItems.Sellcurrency);
            ViewData["Sellunit"] = new SelectList(_context.Units, "Abbreviation", "Abbreviation", inventoryItems.Sellunit);
            return View(inventoryItems);
            //return View("Index");
        }

        // GET: Inventory/Delete/5
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

        // GET: Inventory/Search
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

        private bool InventoryItemsExists(string id)
        {
            return _context.InventoryItems.Any(e => e.Code == id);
        }
    }
}
