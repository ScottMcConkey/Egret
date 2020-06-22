using Egret.DataAccess;
using Egret.Extensions;
using Egret.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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
                            .OrderBy(x => x.SortOrder), nameof(InventoryCategory.InventoryCategoryId), nameof(InventoryCategory.Name), selected);
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
                            .OrderBy(x => x.SortOrder), nameof(InventoryCategory.InventoryCategoryId), nameof(InventoryCategory.Name), selected);
        }

        /// <summary>
        /// Returns a SelectList containing all active Inventory Categories
        /// plus any selected Inventory Category, whether it is active or not.
        /// </summary>
        public virtual SelectList CategoriesActivePlusCurrent(InventoryCategory selected)
        {
            var all = Context.InventoryCategories;
            var actives = Context.InventoryCategories.OrderBy(x => x.SortOrder).Where(x => x.Active == true);
            var defaultSelectList = new SelectList(actives, nameof(InventoryCategory.InventoryCategoryId), nameof(InventoryCategory.Name));

            if (selected != null)
            {
                if (!all.Any(x => x.Name == selected.Name))
                {
                    return defaultSelectList;
                }

                var list = actives.ToList();
                list.Add(selected);
                List<InventoryCategory> orderedList = list.OrderBy(x => x.Name).DistinctBy(x => x.Name).ToList();

                return new SelectList(orderedList, nameof(InventoryCategory.InventoryCategoryId), nameof(InventoryCategory.Name), selected.Name);
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
                            .OrderBy(x => x.SortOrder), nameof(Unit.UnitId), nameof(Unit.Abbreviation), selected);
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
                            .OrderBy(x => x.SortOrder), nameof(Unit.UnitId), nameof(Unit.Abbreviation), selected);
        }

        /// <summary>
        /// Returns a SelectList containing all active Units
        /// plus any selected Unit whether it is active or not.
        /// </summary>
        public virtual SelectList UnitsActivePlusCurrent(Unit selected)
        {
            var all = Context.Units;
            var actives = Context.Units.OrderBy(x => x.SortOrder).Where(x => x.Active == true);
            var defaultSelectList = new SelectList(actives, nameof(Unit.UnitId), nameof(Unit.Abbreviation));

            if (selected != null)
            {
                if (!all.Any(x => x.Name == selected.Name))
                {
                    return defaultSelectList;
                }

                var list = actives.ToList();
                list.Add(selected);
                List<Unit> orderedList = list.OrderBy(x => x.SortOrder).DistinctBy(x => x.Abbreviation).ToList();

                return new SelectList(orderedList, nameof(Unit.UnitId), nameof(Unit.Abbreviation), selected.Name);
            }

            return defaultSelectList;
        }

        /// <summary>
        /// Returns all Storage Locations
        /// </summary>
        /// <param name="selected"></param>
        /// <returns></returns>
        public virtual SelectList StorageLocationsAll(int? selected = null)
        {
            return new SelectList(Context.StorageLocations
                .OrderBy(x => x.SortOrder), nameof(StorageLocation.StorageLocationId), nameof(StorageLocation.Name), selected);
        }

        /// <summary>
        /// Returns all Active Storage Locations
        /// </summary>
        /// <param name="selected"></param>
        /// <returns></returns>
        public virtual SelectList StorageLocationsActive(int? selected = null)
        {
            return new SelectList(Context.StorageLocations
                .Where(x => x.Active == true)
                .OrderBy(x => x.SortOrder), nameof(StorageLocation.StorageLocationId), nameof(StorageLocation.Name), selected);
        }

        /// <summary>
        /// Default system options for all ResultsPerPage dropdowns
        /// </summary>
        /// <returns></returns>
        public SelectList ResultsPerPage()
        {
            List<SelectListItem> pageOptions = new List<SelectListItem>
            {
                new SelectListItem() { Text = "10", Value = "10" },
                new SelectListItem() { Text = "25", Value = "25" },
                new SelectListItem() { Text = "50", Value = "50" },
                new SelectListItem() { Text = "100", Value = "100" }
            };

            return new SelectList(pageOptions, "Value", "Text");
        }
    }
}
