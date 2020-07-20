using Egret.Models;
using System.Collections.Generic;

namespace Egret.Areas.Inventory.Models
{
    public class ConsumptionEventModel
    {
        public ConsumptionEvent ConsumptionEvent { get; set; }
        public List<InventoryItem> InventoryItems { get; set; }
    }
}
