using Egret.DataAccess;
using Egret.Models;
using Egret.Models.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Egret.Services
{
    public class ConsumptionEventService : BaseService, IConsumptionEventService
    {
        public ConsumptionEventService(EgretDbContext context/*, ILogger logger*/)
            : base(context)
        {
            //_logger = logger;
        }

        /// <summary>
        /// Creates a Consumption Event
        /// </summary>
        /// <param name="consumptionEvent"></param>
        /// <param name="user"></param>
        public void CreateConsumptionEvent(ConsumptionEvent consumptionEvent, ClaimsPrincipal user)
        {
            consumptionEvent.AddedBy = user.Identity.Name;
            consumptionEvent.UpdatedBy = user.Identity.Name;
            consumptionEvent.DateAdded = DateTime.Now;
            consumptionEvent.DateUpdated = DateTime.Now;

            Context.ConsumptionEvents.Add(consumptionEvent);
            Context.SaveChanges();
        }

        /// <summary>
        /// Returns a single Consumption Event object if the
        /// supplied Id exists in the database
        /// </summary>
        /// <param name="id">The unique identifier for an Item</param>
        /// <param name="noTracking">Signals whether Entity Framework should not use tracking for the returned entity</param>
        /// <returns></returns>
        public ConsumptionEvent GetConsumptionEvent(string id, bool noTracking = false)
        {
            if (noTracking)
            {
                var consumptionEvent = Context.ConsumptionEvents.AsNoTracking()
                    .Where(i => i.ConsumptionEventId == id)
                    .Include(i => i.InventoryItemNavigation)
                        .ThenInclude(i => i.UnitNavigation)
                    .FirstOrDefault(m => m.ConsumptionEventId == id);
                return consumptionEvent;
            }
            else
            {
                var consumptionEvent = Context.ConsumptionEvents
                    .Where(i => i.ConsumptionEventId == id)
                    .Include(i => i.InventoryItemNavigation)
                        .ThenInclude(i => i.UnitNavigation)
                    .FirstOrDefault(m => m.ConsumptionEventId == id);
                return consumptionEvent;
            }
        }

        /// <summary>
        /// Deletes the specified Consumption Event
        /// </summary>
        /// <param name="id"></param>
        public void DeleteConsumptionEvent(string id)
        {
            var consumptionEvent = Context.ConsumptionEvents.Where(x => x.ConsumptionEventId == id).FirstOrDefault();
            Context.ConsumptionEvents.Remove(consumptionEvent);
            Context.SaveChanges();
        }

        /// <summary>
        /// Edit the Consumption Event
        /// </summary>
        /// <param name="id"></param>
        public void UpdateConsumptionEvent(ConsumptionEvent consumptionEvent, ClaimsPrincipal user)
        {
            var eventToUpdate = Context.ConsumptionEvents.AsNoTracking().Where(x => x.ConsumptionEventId == consumptionEvent.ConsumptionEventId).FirstOrDefault();
            consumptionEvent.UpdatedBy = user.Identity.Name;
            consumptionEvent.DateUpdated = DateTime.Now;

            if (eventToUpdate != null)
            {
                Context.ConsumptionEvents.Update(consumptionEvent);
                Context.SaveChanges();
            }
        }

        /// <summary>
        /// Return all Consumption Events matching the specified criteria.
        /// All results are returned if no criteria are specified
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public List<ConsumptionEvent> FindConsumptionSearchResults(ConsumptionEventSearchQueryEntity searchModel)
        {
            var results = Context.ConsumptionEvents
                .AsQueryable()
                .AsNoTracking();

            // Code
            if (!string.IsNullOrEmpty(searchModel.InventoryItemId))
                results = results.Where(x => x.InventoryItemId.Contains(searchModel.InventoryItemId));

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

            var realResults = results.OrderBy(x => x.ConsumptionEventId).ToList();

            // Consumed By
            if (!string.IsNullOrEmpty(searchModel.ConsumedBy))
                realResults = realResults.Where(x => x.ConsumedBy.Contains(searchModel.ConsumedBy, StringComparison.OrdinalIgnoreCase)).ToList();

            // Order Number
            if (!string.IsNullOrEmpty(searchModel.OrderNumber))
                realResults = realResults.Where(x => x.OrderNumber.Contains(searchModel.OrderNumber, StringComparison.OrdinalIgnoreCase)).ToList();            

            return realResults;
        }
    }
}
