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
    public class CurrencyTypesController : BaseController
    {
        private static ILogger _logger;

        public CurrencyTypesController(EgretContext context, ILogger<CurrencyTypesController> logger)
            : base(context)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var currencyTypes = Context.CurrencyTypes.OrderBy(x => x.SortOrder);
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
                    Context.Update(types[i]);
                }
                Context.SaveChanges();
                TempData["SuccessMessage"] = "Save Complete";
                return RedirectToAction(nameof(Index));
            }

            return View(types);
        }
    }
}