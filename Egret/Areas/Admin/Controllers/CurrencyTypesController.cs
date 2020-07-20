using Egret.DataAccess;
using Egret.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Egret.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin_Access")]
    public class CurrencyTypesController : Controller
    {
        private readonly ILogger _logger;

        private readonly EgretDbContext _context;

        public CurrencyTypesController(EgretDbContext context, ILogger<CurrencyTypesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var currencyTypes = _context.CurrencyTypes;
            return View(currencyTypes.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(List<CurrencyType> types)
        {
            if (ModelState.IsValid)
            {
                for (int i = 0; i < types.Count(); i++)
                {
                    _context.Update(types[i]);
                }
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Save Complete";
                return RedirectToAction(nameof(Index));
            }

            return View(types);
        }
    }
}