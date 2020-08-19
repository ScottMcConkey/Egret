using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egret.Models.QueryModels
{
    public class InventoryFinancialData
    {
		public string Category { get; set; }
	    public string ItemId { get; set; }
		public string ItemDescription { get; set; }
		public string QtyPurchased { get; set; }
		public string Unit { get; set; }
		public string FOBCost { get; set; }
		public string ShippingCost { get; set; }
		public string ImportCost { get; set; }
		public string VATCost { get; set; }
		public string ConsumptionId { get; set; }
		public string QtyConsumed { get; set; }
		public string DateConsumed { get; set; }
    }
}
