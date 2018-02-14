﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using Npgsql;
using Npgsql.EntityFrameworkCore;
using Egret.DataAccess;
using Egret.Models;

namespace Egret.Controllers
{
    public class CurrencyTypesController : ManagedController
    {
        public string BackButtonText = "Back to Admin";
        public string BackButtonText2 = "Back to Currency Types";

        public CurrencyTypesController(EgretContext context)
            : base(context) { }

        [HttpGet]
        public IActionResult Index()
        {
            ViewData["BackText"] = BackButtonText;
            var egretContext = Context.CurrencyTypes.OrderBy(x => x.Sortorder);
            return View(egretContext.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(List<CurrencyType> types)
        {
            if (ModelState.IsValid)
            {
                for (int i = 0; i < types.Count; i++)
                {
                    Context.Update(types[i]);
                }
                Context.SaveChanges();
                TempData["SuccessMessage"] = "Save Complete";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["BackText"] = BackButtonText2;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CurrencyType category)
        {
            category.Sortorder = Context.CurrencyTypes.Max(x => x.Sortorder) + 1;
            category.Defaultselection = false;

            if (ModelState.IsValid)
            {
                Context.Add(category);
                Context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var inventoryItems = Context.CurrencyTypes.SingleOrDefault(m => m.Id == id);
            Context.CurrencyTypes.Remove(inventoryItems);
            Context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}