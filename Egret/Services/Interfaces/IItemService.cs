using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Egret.Models;
using Egret.Services;
using Egret.ViewModels;

namespace Egret.Services
{
    public interface IItemService
    {
        InventoryItem GetItem(string id, bool noTracking = false);

        void DeleteItem(string id);

        void UpdateItem(InventoryItem item, ClaimsPrincipal user);

        void CreateItem(InventoryItem item, ClaimsPrincipal user);

        List<InventoryItem> FindItemSearchResults(ItemSearchModel searchModel);

        void DefineFabricTestsForItem(InventoryItem item, List<FabricTest> fabricTests);
    }
}
