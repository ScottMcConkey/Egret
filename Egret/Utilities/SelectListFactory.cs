﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Egret.DataAccess;
using Egret.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Egret.Extensions;

namespace Egret.Utilities
{
    /// <summary>
    /// Factory object for Select Lists generated from database relationships.
    /// </summary>
    public class SelectListFactory
    {
        private readonly EgretContext _egretContext;

        public SelectListFactory(EgretContext context)
        {
            _egretContext = context;
            _egretContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public virtual SelectList CategoriesAll(string selected = null)
        {
            return new SelectList(_egretContext.InventoryCategories
                            .OrderBy(x => x.SortOrder), "Name", "Name", selected);
        }

        public virtual SelectList CategoriesActive(string selected = null)
        {
            return new SelectList(_egretContext.InventoryCategories
                            .Where(x => x.Active == true)
                            .OrderBy(x => x.SortOrder), "Name", "Name", selected);
        }

        /// <summary>
        /// Creates a SelectList containing all active Inventory Categories
        /// plus any selected Inventory Category whether it is active or not.
        /// </summary>
        public virtual SelectList CategoriesActivePlusCurrent(InventoryCategory selected)
        {
            var all = _egretContext.InventoryCategories;
            var actives = _egretContext.InventoryCategories.OrderBy(x => x.SortOrder).Where(x => x.Active == true);
            var defaultSelectList = new SelectList(actives, "Name", "Name");

            if (selected != null)
            {
                if (!all.Any(x => x.Name == selected.Name))
                {
                    return defaultSelectList;
                }

                var list = actives.ToList();
                list.Add(selected);
                List <InventoryCategory> orderedList = list.OrderBy(x => x.Name).DistinctBy(x => x.Name).ToList();

                return new SelectList(orderedList, "Name", "Name", selected.Name);
            }

            return defaultSelectList;
        }

        public virtual SelectList UnitsAll(string selected = null)
        {
            return new SelectList(_egretContext.Units
                            .OrderBy(x => x.SortOrder), "Abbreviation", "Abbreviation", selected);
        }

        public virtual SelectList UnitsActive(string selected = null)
        {
            return new SelectList(_egretContext.Units
                            .Where(x => x.Active == true)
                            .OrderBy(x => x.SortOrder), "Abbreviation", "Abbreviation", selected);
        }

        /// <summary>
        /// Creates a SelectList containing all active Units
        /// plus any selected Unit whether it is active or not.
        /// </summary>
        public virtual SelectList UnitsActivePlusCurrent(Unit selected)
        {
            var all = _egretContext.Units;
            var actives = _egretContext.Units.OrderBy(x => x.SortOrder).Where(x => x.Active == true);
            var defaultSelectList = new SelectList(actives, "Abbreviation", "Abbreviation");

            if (selected != null)
            {
                if (!all.Any(x => x.Name == selected.Name))
                {
                    return defaultSelectList;
                }

                var list = actives.ToList();
                list.Add(selected);
                List<Unit> orderedList = list.OrderBy(x => x.SortOrder).DistinctBy(x => x.Abbreviation).ToList();

                return new SelectList(orderedList, "Abbreviation", "Abbreviation", selected.Name);
            }

            return defaultSelectList;
        }

        public virtual SelectList CurrencyTypesAll(string selected = null)
        {
            return new SelectList(_egretContext.CurrencyTypes
                            .OrderBy(x => x.SortOrder), "Abbreviation", "Abbreviation", selected);
        }

        public virtual SelectList CurrencyTypesActive(string selected = null)
        {
            var defaultType = _egretContext.CurrencyTypes.Where(x => x.DefaultSelection == true);

            return new SelectList(_egretContext.CurrencyTypes
                            .Where(x => x.Active == true)
                            .OrderBy(x => x.SortOrder), "Abbreviation", "Abbreviation", selected ?? defaultType.FirstOrDefault().Abbreviation);
        }

        /// <summary>
        /// Creates a SelectList containing all active Currency Types
        /// plus any selected Currency Type whether it is active or not.
        /// </summary>
        public virtual SelectList CurrencyTypesPlusCurrent(CurrencyType selected)
        {
            var all = _egretContext.CurrencyTypes;
            var actives = _egretContext.CurrencyTypes.OrderBy(x => x.SortOrder).Where(x => x.Active == true);
            var defaultSelectList = new SelectList(actives, "Abbreviation", "Abbreviation");

            if (selected != null)
            {
                if (!all.Any(x => x.Name == selected.Name))
                {
                    return defaultSelectList;
                }

                var list = actives.ToList();
                list.Add(selected);
                List<CurrencyType> orderedList = list.OrderBy(x => x.SortOrder).DistinctBy(x => x.Abbreviation).ToList();

                return new SelectList(orderedList, "Abbreviation", "Abbreviation", selected.Name);
            }

            return defaultSelectList;
        }

        public static SelectList ResultsPerPage()
        {
            List<SelectListItem> pageOptions = new List<SelectListItem>();
            pageOptions.Add(new SelectListItem() { Text = "10", Value = "10" });
            pageOptions.Add(new SelectListItem() { Text = "25", Value = "25" });
            pageOptions.Add(new SelectListItem() { Text = "50", Value = "50" });
            pageOptions.Add(new SelectListItem() { Text = "100", Value = "100" });

            return new SelectList(pageOptions, "Value", "Text");
        }
    }
}
