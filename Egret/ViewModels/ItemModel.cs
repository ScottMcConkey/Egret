using Egret.Models;
using System.Collections.Generic;

namespace Egret.ViewModels
{
    public class ItemModel
    {
        public List<FabricTest> FabricTests { get; set; }
        public List<ConsumptionEvent> ConsumptionEvents { get; set; }
        public InventoryItem Item { get; set; }
    }
}
