using System;
using System.Collections.Generic;
using System.Linq;
using Egret.Controllers;
using Egret.DataAccess;
using Egret.Models;
using Egret.ViewModels;



namespace Egret.Controllers
{
    public class PurchasesController : BaseController
    {
        private IQueryable<CurrencyType> ActiveCurrencyTypes { get; set; }
        private IQueryable<Unit> ActiveUnits { get; set; }
        private IQueryable<InventoryCategory> ActiveInventoryCategories { get; set; }
        private IQueryable<InventoryCategory> AllInventoryCategories { get; set; }

        public PurchasesController(EgretContext context)
            : base(context)
        {
            ActiveCurrencyTypes = Context.CurrencyTypes.Where(x => x.Active == true).OrderBy(x => x.SortOrder);
            ActiveUnits = Context.Units.Where(x => x.Active == true).OrderBy(x => x.SortOrder);
            ActiveInventoryCategories = Context.InventoryCategories.Where(x => x.Active == true).OrderBy(x => x.SortOrder);

            AllInventoryCategories = Context.InventoryCategories.OrderBy(x => x.SortOrder);
        }

        //public IActionResult
    }
}
