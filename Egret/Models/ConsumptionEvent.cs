﻿using System;
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

        [Display(Name = "Quantity Consumed")]
        public decimal? QuantityConsumed { get; set; }

        public string Unit { get; set; }

        [Display(Name = "Consumed By")]
        public string ConsumedBy { get; set; }

        [Display(Name = "Date Consumed")]
        [UIHint("date")]
        public DateTime? DateOfConsumption { get; set; }

        [Display(Name = "Sample Order Number")]
        public string SampleOrderNumber { get; set; }

        [Display(Name = "Production Order Number")]
        public string ProductionOrderNumber { get; set; }

        [Display(Name = "Pattern Number")]
        public string PatternNumber { get; set; }

        [Display(Name = "Value Consumed")]
        public decimal? ValueConsumed { get; set; }


        public string InventoryItemCode { get; set; }

        [NotMapped]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string RelationshipDisplay
        {
            get
            {
                return (DateOfConsumption != null ? DateOfConsumption.Value.ToShortDateString() + " - " : "") 
                    + "Units Consumed: " + (QuantityConsumed != null ? QuantityConsumed.ToString() : "0");
            }
            private set { }
        }


        public Unit UnitNavigation { get; set; }
        public InventoryItem InventoryItemNavigation { get; set; }
    }
}
