using Egret.DataAccess;
using Egret.Extensions;
using Egret.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egret.Services
{
    /// <summary>
    /// Factory Service for Select Lists generated from database relationships
    /// </summary>
    public class SelectListFactoryService : BaseService, ISelectListFactoryService
    {
        public SelectListFactoryService(EgretContext context/*, ILogger logger*/)
            : base(context)
        {
            //_logger = logger;
            Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        /// <summary>
        /// Returns all Inventory Categories
        /// </summary>
        /// <param name="selected"></param>
        /// <returns></returns>
        public virtual SelectList CategoriesAll(int? selected = null)
        {
            return new SelectList(Context.InventoryCategories
                            .OrderBy(x => x.SortOrder), "Id", "Name", selected);
        }

        /// <summary>
        /// Returns all Active Inventory Categories
        /// </summary>
        /// <param name="selected"></param>
        /// <returns></returns>
        public virtual SelectList CategoriesActive(int? selected = null)
        {
            return new SelectList(Context.InventoryCategories
                            .Where(x => x.Active == true)
                            .OrderBy(x => x.SortOrder), "Id", "Name", selected);
        }

        /// <summary>
        /// Returns a SelectList containing all active Inventory Categories
        /// plus any selected Inventory Category, whether it is active or not.
        /// </summary>
        public virtual SelectList CategoriesActivePlusCurrent(InventoryCategory selected)
        {
            var all = Context.InventoryCategories;
            var actives = Context.InventoryCategories.OrderBy(x => x.SortOrder).Where(x => x.Active == true);
            var defaultSelectList = new SelectList(actives, "Id", "Name");

            if (selected != null)
            {
                if (!all.Any(x => x.Name == selected.Name))
                {
                    return defaultSelectList;
                }

                var list = actives.ToList();
                list.Add(selected);
                List<InventoryCategory> orderedList = list.OrderBy(x => x.Name).DistinctBy(x => x.Name).ToList();

                return new SelectList(orderedList, "Id", "Name", selected.Name);
            }

            return defaultSelectList;
        }

        /// <summary>
        /// Returns all Units
        /// </summary>
        /// <param name="selected"></param>
        /// <returns></returns>
        public virtual SelectList UnitsAll(string selected = null)
        {
            return new SelectList(Context.Units
                            .OrderBy(x => x.SortOrder), "Id", "Abbreviation", selected);
        }

        /// <summary>
        /// Returns all Active Units
        /// </summary>
        /// <param name="selected"></param>
        /// <returns></returns>
        public virtual SelectList UnitsActive(int? selected = null)
        {
            return new SelectList(Context.Units
                            .Where(x => x.Active == true)
                            .OrderBy(x => x.SortOrder), "Id", "Abbreviation", selected);
        }

        /// <summary>
        /// Returns a SelectList containing all active Units
        /// plus any selected Unit whether it is active or not.
        /// </summary>
        public virtual SelectList UnitsActivePlusCurrent(Unit selected)
        {
            var all = Context.Units;
            var actives = Context.Units.OrderBy(x => x.SortOrder).Where(x => x.Active == true);
            var defaultSelectList = new SelectList(actives, "Id", "Abbreviation");

            if (selected != null)
            {
                if (!all.Any(x => x.Name == selected.Name))
                {
                    return defaultSelectList;
                }

                var list = actives.ToList();
                list.Add(selected);
                List<Unit> orderedList = list.OrderBy(x => x.SortOrder).DistinctBy(x => x.Abbreviation).ToList();

                return new SelectList(orderedList, "Id", "Abbreviation", selected.Name);
            }

            return defaultSelectList;
        }

        /// <summary>
        /// Returns all Currency Types
        /// </summary>
        /// <param name="selected"></param>
        /// <returns></returns>
        public virtual SelectList CurrencyTypesAll(int? selected = null)
        {
            return new SelectList(Context.CurrencyTypes
                            .OrderBy(x => x.SortOrder), "Id", "Abbreviation", selected);
        }

        /// <summary>
        /// Returns all Active Currency Types
        /// </summary>
        /// <param name="selected"></param>
        /// <returns></returns>
        public virtual SelectList CurrencyTypesActive(int? selected = null)
        {
            var defaultType = Context.CurrencyTypes.Where(x => x.DefaultSelection == true).FirstOrDefault();

            return new SelectList(Context.CurrencyTypes
                            .Where(x => x.Active == true)
                            .OrderBy(x => x.SortOrder), "Id", "Abbreviation", selected ?? defaultType.Id);
        }

        /// <summary>
        /// Returns a SelectList containing all active Currency Types
        /// plus any selected Currency Type whether it is active or not.
        /// </summary>
        public virtual SelectList CurrencyTypesPlusCurrent(CurrencyType selected)
        {
            var all = Context.CurrencyTypes;
            var actives = Context.CurrencyTypes.OrderBy(x => x.SortOrder).Where(x => x.Active == true);
            var defaultSelectList = new SelectList(actives, "Id", "Abbreviation");

            if (selected != null)
            {
                if (!all.Any(x => x.Name == selected.Name))
                {
                    return defaultSelectList;
                }

                var list = actives.ToList();
                list.Add(selected);
                List<CurrencyType> orderedList = list.OrderBy(x => x.SortOrder).DistinctBy(x => x.Abbreviation).ToList();

                return new SelectList(orderedList, "Id", "Abbreviation", selected.Name);
            }

            return defaultSelectList;
        }

        public SelectList ResultsPerPage()
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
