using Egret.Models;
using Egret.ViewModels;
using System.Collections.Generic;
using System.Security.Claims;

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
