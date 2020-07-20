using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Egret.Models;
using Egret.Models.Common;
using Egret.ViewModels;

namespace Egret.Services
{
    public interface IConsumptionEventService
    {
        void CreateConsumptionEvent(ConsumptionEvent consumptionEvent, ClaimsPrincipal user);

        ConsumptionEvent GetConsumptionEvent(string id, bool noTracking = false);

        void DeleteConsumptionEvent(string id);

        void UpdateConsumptionEvent(ConsumptionEvent consumptionEvent, ClaimsPrincipal user);

        List<ConsumptionEvent> FindConsumptionSearchResults(ConsumptionEventSearchQueryEntity searchModel);
    }
}
