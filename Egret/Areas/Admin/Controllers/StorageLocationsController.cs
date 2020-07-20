using Egret.DataAccess;
using Egret.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Egret.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin_Access")]
    public class StorageLocationsController : Controller
    {
        private readonly ILogger _logger;

        private readonly EgretDbContext _context;

        public StorageLocationsController(EgretDbContext context, ILogger<StorageLocationsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var locations =  _context.StorageLocations.OrderBy(x => x.SortOrder);
            return View(locations.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(List<StorageLocation> locations)
        {
            // Find duplicates
            var duplicateName = locations.GroupBy(x => x.Name).Where(g => g.Count() > 1)
                .Select(y => y.Key)
                .ToList();

            var duplicateSortOrder = locations.GroupBy(x => x.SortOrder).Where(g => g.Count() > 1)
                .Select(y => y.Key)
                .ToList();

            // Find Sort Order <= 0
            var badSort = locations.Where(x => x.SortOrder <= 0);

            // Find usage

            // Error on duplicates
            if (duplicateName.Count > 0)
                ModelState.AddModelError("", "Duplicate Name detected. Please assign every Storage Location a unique Name.");

            if (duplicateSortOrder.Count > 0)
                ModelState.AddModelError("", "Duplicate Sort Order detected. Please assign every Storage Location a unique Sort Order.");

            if (badSort.Count() > 0)
                ModelState.AddModelError("", "Sort Order below 1 detected. Please assign every Storage Location a positive Sort Order.");

            if (ModelState.IsValid)
            {
                for (int i = 0; i < locations.Count(); i++)
                {
                     _context.Update(locations[i]);
                }
                 _context.SaveChanges();
                TempData["SuccessMessage"] = "Save Complete";
                return RedirectToAction(nameof(Index));
            }

            return View(locations);            
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StorageLocation location)
        {
            location.SortOrder =  _context.StorageLocations
                .Select(x => x.SortOrder)
                .DefaultIfEmpty()
                .ToList()
                .Max() + 1;

            if (ModelState.IsValid)
            {
                 _context.Add(location);
                 _context.SaveChanges();
                TempData["SuccessMessage"] = "Storage Location Created";
                return RedirectToAction(nameof(Index));
            }

            return View(location);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var location =  _context.StorageLocations.SingleOrDefault(m => m.StorageLocationId == id);
             _context.StorageLocations.Remove(location);
             _context.SaveChanges();
            TempData["SuccessMessage"] = $"Storage Location '{location.Name}' Deleted";
            return RedirectToAction(nameof(Index));
        }

    }
}