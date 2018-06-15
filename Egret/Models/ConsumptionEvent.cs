using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Egret.Models
{
    public class ConsumptionEvent
    {
        [Key]
        public string Id { get; set; }

        [Display(Name = "Quantity Consumed")]
        public decimal? QuantityConsumed { get; set; }

        public string Unit { get; set; }

        [Display(Name = "Consumed By")]
        public string ConsumedBy { get; set; }

        [Display(Name = "Date Consumed")]
        public DateTime? DateOfConsumption { get; set; }

        [Display(Name = "Sample Order Number")]
        public string SampleOrderNumber { get; set; }

        [Display(Name = "Production Order Number")]
        public string ProductionOrderNumber { get; set; }

        [Display(Name = "Pattern Number")]
        public string PatternNumber { get; set; }

        [Display(Name = "Value Consumed")]
        public decimal? ValueConsumed { get; set; }

        public InventoryItem InventoryItem { get; set; }
    }
}
