using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egret.Models.Common
{
    public class ConsumptionEventSearchQueryEntity
    {
        public string InventoryItemId { get; set; }
        public DateTime? DateCreatedStart { get; set; }
        public DateTime? DateCreatedEnd { get; set; }
        public string ConsumedBy { get; set; }
        public string OrderNumber { get; set; }
        public int ResultsPerPage { get; set; }
    }
}
