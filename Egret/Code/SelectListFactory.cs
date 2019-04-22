using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Egret.DataAccess;
using Egret.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace Egret.Code
{
    public class SelectListFactory
    {
        private readonly EgretContext _egretContext;

        public SelectListFactory(EgretContext context)
        {
            _egretContext = context;
        }

        public SelectList CategoriesAll(string selected = null)
        {
            return new SelectList(_egretContext.InventoryCategories
                            .OrderBy(x => x.SortOrder), "Name", "Name", selected);
        }

        public SelectList CategoriesActive(string selected = null)
        {
            return new SelectList(_egretContext.InventoryCategories
                            .Where(x => x.Active == true)
                            .OrderBy(x => x.SortOrder), "Name", "Name", selected);
        }

        /// <summary>
        /// Returns a SelectList containing all active Inventory Categories
        /// plus any current Inventory Category that happens to be inactive
        /// </summary>
        /// <param name="inactiveSelected"></param>
        /// <returns></returns>
        public SelectList CategoriesActivePlusCurrent(InventoryCategory inactiveSelected)
        {
            var all = _egretContext.InventoryCategories.AsNoTracking();
            var actives = _egretContext.InventoryCategories.AsNoTracking().OrderBy(x => x.SortOrder).Where(x => x.Active == true);
            var defaultSelectList = new SelectList(actives, "Name", "Name");

            if (inactiveSelected != null)
            {
                if (!all.Any(x => x.Name == inactiveSelected.Name))
                {
                    return defaultSelectList;
                }

                var list = actives.ToList();
                list.Add(inactiveSelected);
                List <InventoryCategory> orderedList = list.OrderBy(x => x.Name).ToList();

                return new SelectList(orderedList, "Name", "Name", inactiveSelected.Name);
            }

            return defaultSelectList;
        }
    }
}
