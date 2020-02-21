using Egret.DataAccess;
using Egret.Models;
using Egret.Utilities;
using Egret.Services;
using Egret.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Egret.Services
{
    public class ItemService : BaseService, IItemService
    {
        //ILogger _logger;

        public ItemService(EgretContext context/*, ILogger logger*/)
            : base(context)
        {
            //_logger = logger;
        }


        /// <summary>
        /// Returns a single InventoryItem object if the
        /// supplied Id exists in the database
        /// </summary>
        /// <param name="id">The unique identifier for an Item</param>
        /// <param name="noTracking">Signals whether Entity Framework should not use tracking for the returned entity</param>
        /// <returns></returns>
        public InventoryItem GetItem(string id, bool noTracking = false)
        {
            if (noTracking)
            {
                var item = Context.InventoryItems.AsNoTracking()
                    .Where(i => i.Code == id)
                    .Include(i => i.CategoryNavigation).AsNoTracking()
                    .Include(i => i.UnitNavigation).AsNoTracking()
                    .Include(i => i.ConsumptionEventsNavigation).AsNoTracking()
                    .Include(i => i.FabricTestsNavigation).AsNoTracking()
                    .Include(i => i.FOBCostCurrencyNavigation).AsNoTracking()
                    .Include(i => i.ShippingCostCurrencyNavigation).AsNoTracking()
                    .Include(i => i.ImportCostCurrencyNavigation).AsNoTracking()
                    .FirstOrDefault(m => m.Code == id);
                return item;
            }
            else
            {
                var item = Context.InventoryItems
                    .Where(i => i.Code == id)
                    .Include(i => i.CategoryNavigation)
                    .Include(i => i.UnitNavigation)
                    .Include(i => i.ConsumptionEventsNavigation)
                    .Include(i => i.FabricTestsNavigation)
                    .Include(i => i.FOBCostCurrencyNavigation)
                    .Include(i => i.ShippingCostCurrencyNavigation)
                    .Include(i => i.ImportCostCurrencyNavigation)
                    .FirstOrDefault(m => m.Code == id);
                return item;
            }
        }

        /// <summary>
        /// Deletes the specified user
        /// </summary>
        /// <param name="id"></param>
        public void DeleteItem(string id)
        {
            var item = Context.InventoryItems.Where(x => x.Code == id).FirstOrDefault();
            Context.InventoryItems.Remove(item);
            Context.SaveChanges();
        }

        /// <summary>
        /// Edit the Item
        /// </summary>
        /// <param name="id"></param>
        public void UpdateItem(InventoryItem item, ClaimsPrincipal user)
        {
            var itemToUpdate = Context.InventoryItems.AsNoTracking().Where(x => x.Code == item.Code).FirstOrDefault();

            if (itemToUpdate != null)
            {
                Context.InventoryItems.Update(item);
                Context.SaveChanges();
            }
        }

        /// <summary>
        /// Creates an Item. If item creation is successfull, the
        /// Item Code is returned
        /// </summary>
        /// <param name="item"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public string CreateItem(InventoryItem item, ClaimsPrincipal user)
        {
            item.AddedBy = user.Identity.Name;
            item.UpdatedBy = user.Identity.Name;
            item.DateAdded = DateTime.Now;
            item.DateUpdated = DateTime.Now;

            Context.InventoryItems.Add(item);
            Context.SaveChanges();
            //_logger.LogInformation($"Item {item.Code} created by user {user.Identity.Name}");

            return item.Code;
        }

        /// <summary>
        /// Return all Inventory Items matching the specified criteria.
        /// All results are returned if no criteria are specified
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public List<InventoryItem> FindItemSearchResults(ItemSearchModel searchModel)
        {
            var results = Context.InventoryItems
                .Include(x => x.ConsumptionEventsNavigation)
                .AsQueryable()
                .AsNoTracking();

            // Code
            if (!String.IsNullOrEmpty(searchModel.Code))
                results = results.Where(x => x.Code.Contains(searchModel.Code));

            // Description
            if (!String.IsNullOrEmpty(searchModel.Description))
                results = results.Where(x => x.Description.Contains(searchModel.Description, StringComparison.InvariantCultureIgnoreCase));

            // Date Added
            if (searchModel.DateCreatedStart != null && searchModel.DateCreatedEnd != null)
            {
                results = results.Where(x => x.DateAdded.Value.Date >= searchModel.DateCreatedStart.Value.Date && x.DateAdded.Value.Date <= searchModel.DateCreatedEnd.Value.Date);
            }
            else if (searchModel.DateCreatedStart != null && searchModel.DateCreatedEnd == null)
            {
                results = results.Where(x => x.DateAdded.Value.Date >= searchModel.DateCreatedStart.Value.Date);
            }
            else if (searchModel.DateCreatedStart == null && searchModel.DateCreatedEnd != null)
            {
                results = results.Where(x => x.DateAdded.Value.Date <= searchModel.DateCreatedEnd.Value.Date);
            }

            // Category
            if (searchModel.Category != null)
                results = results.Where(x => x.CategoryId == searchModel.Category);

            // Customer Purchased For
            if (!String.IsNullOrEmpty(searchModel.CustomerPurchasedFor))
                results = results.Where(x => x.CustomerPurchasedFor.Contains(searchModel.CustomerPurchasedFor, StringComparison.InvariantCultureIgnoreCase));

            // Customer Reserved For
            if (!String.IsNullOrEmpty(searchModel.CustomerReservedFor))
                results = results.Where(x => x.CustomerReservedFor.Contains(searchModel.CustomerReservedFor, StringComparison.InvariantCultureIgnoreCase));

            // In Stock
            if (searchModel.InStock == "Yes")
                results = results.Where(x => x.StockLevel == "In Stock");
            else if (searchModel.InStock == "No")
                results = results.Where(x => x.StockLevel != "In Stock");

            var realResults = results.OrderBy(x => x.Code).ToList();

            return realResults;
        }
    
        public void DefineFabricTestsForItem(InventoryItem item, List<FabricTest> fabricTests)
        {
            var dbTests = Context.FabricTests.AsNoTracking().Where(x => x.InventoryItemCode == item.Code).ToList();

            if (fabricTests.Count > 0)
            {
                // Remove from db if not in model
                foreach (FabricTest test in dbTests)
                {
                    if (!fabricTests.Contains(test))
                    {
                        Context.FabricTests.Remove(test);
                    }
                }

                // Add all from model - model is authority
                foreach (FabricTest test in fabricTests)
                {
                    test.InventoryItemCode = item.Code;
                    Context.FabricTests.Update(test);
                }
            }
            else
            {
                // Remove all tests
                foreach (FabricTest test in dbTests)
                {
                    Context.FabricTests.Remove(test);
                }
            }

            Context.SaveChanges();
        }
    }
}
