using Egret.Models;
using System.Collections.Generic;

namespace Egret.ViewModels
{
    public class ConsumptionEventViewModel
    {
        public ConsumptionEvent ConsumptionEvent { get; set; }
        public List<InventoryItem> InventoryItems { get; set; }
    }
}
