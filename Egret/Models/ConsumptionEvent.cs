using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Egret.Models
{
    public class ConsumptionEvent
    {
        [Key]
        public string Id { get; set; }

        [Display(Name = "Date Added")]
        public DateTime? DateAdded { get; set; }

        [Display(Name = "Added By")]
        public string AddedBy { get; set; }

        [Display(Name = "Date Updated")]
        public DateTime? DateUpdated { get; set; }

        [Display(Name = "Updated By")]
        public string UpdatedBy { get; set; }

        [Required]
        [Display(Name = "Quantity Consumed")]
        public decimal? QuantityConsumed { get; set; }

        public string Unit { get; set; }

        [Required]
        [Display(Name = "Consumed By")]
        public string ConsumedBy { get; set; }

        [Required]
        [Display(Name = "Date Consumed")]
        [UIHint("date")]
        public DateTime? DateOfConsumption { get; set; }

        [Display(Name = "Order Number")]
        public string OrderNumber { get; set; }

        [Display(Name = "Pattern Number")]
        public string PatternNumber { get; set; }

        [Required]
        [Display(Name = "Item Code")]
        public string InventoryItemCode { get; set; }


        public Unit UnitNavigation { get; set; }

        public InventoryItem InventoryItemNavigation { get; set; }

        public Order OrderNavigation { get; set; }

        [NotMapped]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string RelationshipDisplay
        {
            get
            {
                return (DateOfConsumption != null ? DateOfConsumption.Value.ToShortDateString() + " - " : "")
                    + "Quantity Consumed: " + (QuantityConsumed != null ? QuantityConsumed.ToString() : "0");
            }
            private set { }
        }
    }
}
