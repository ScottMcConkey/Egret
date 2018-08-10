using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions;
using Egret.Attributes;

namespace Egret.Models
{
    public partial class InventoryItem
    {
        public string Code { get; set; }

        [Display(Name = "Date Added")]
        public DateTime? DateAdded { get; set; }

        [Display(Name = "Added By")]
        public string AddedBy { get; set; }

        [Display(Name = "Date Updated")]
        public DateTime? DateUpdated { get; set; }

        [Display(Name = "Updated By")]
        public string UpdatedBy { get; set; }

        [Required]
        [Language(Name = "Nepali", Value = "वर्णन")]
        public string Description { get; set; }

        [Display(Name = "Customer Purchased For")]
        public string CustomerPurchasedFor { get; set; }

        [Display(Name = "Customer Reserved For")]
        public string CustomerReservedFor { get; set; }

        public string Supplier { get; set; }

        [Display(Name = "Quantity to Purchase Now")]
        public string QtyToPurchaseNow { get; set; }

        [Display(Name = "Approximate Production Qty")]
        public string ApproxProdQty { get; set; }

        [Display(Name = "Needed Before")]
        [UIHint("date")]
        public DateTime? NeededBefore { get; set; }

        [Display(Name = "Target Price")]
        public string TargetPrice { get; set; }

        [Display(Name = "Shipping Company")]
        public string ShippingCompany { get; set; }

        [Display(Name = "Bonded Warehouse?")]
        [UIHint("checkbox")]
        public bool BondedWarehouse { get; set; }

        [Display(Name = "Date of Order Confirmed")]
        [UIHint("Date")]
        public DateTime? DateConfirmed { get; set; }

        [Display(Name = "Date of Shipping")]
        [UIHint("Date")]
        public DateTime? DateShipped { get; set; }

        [Display(Name = "Date of Arrival")]
        [UIHint("Date")]
        public DateTime? DateArrived { get; set; }

        public string Comment { get; set; }

        [Display(Name = "Quantity Purchased")]
        [DisplayFormat(DataFormatString = "{0:0.00##}", ApplyFormatInEditMode = true)]
        public decimal? QtyPurchased { get; set; }

        public string Unit { get; set; }

        [Display(Name = "FOB Cost Or Local Cost no VAT")]
        [DisplayFormat(DataFormatString = "{0:0.00##}", ApplyFormatInEditMode = true)]
        public decimal? FOBCost { get; set; }

        public string FOBCostCurrency { get; set; }

        [Display(Name = "Shipping Cost")]
        [DisplayFormat(DataFormatString = "{0:0.00##}", ApplyFormatInEditMode = true)]
        public decimal? ShippingCost { get; set; }

        public string ShippingCostCurrency { get; set; }

        [Display(Name = "Import/Custom/Delivery Costs/VAT")]
        [DisplayFormat(DataFormatString = "{0:0.00##}", ApplyFormatInEditMode = true)]
        public decimal? ImportCosts { get; set; }

        public string ImportCostCurrency { get; set; }

        public string Category { get; set; }

        public InventoryCategory CategoryNavigation { get; set; }
        public Unit UnitNavigation { get; set; }

        public CurrencyType FOBCostCurrencyNavigation { get; set; }
        public CurrencyType ShippingCostCurrencyNavigation { get; set; }
        public CurrencyType ImportCostCurrencyNavigation { get; set; }

        public ICollection<FabricTest> FabricTestsNavigation { get; set; }
        public ICollection<ConsumptionEvent> ConsumptionEventsNavigation { get; set; }

        [Display(Name = "Quantity in Stock")]
        [UIHint("text")]
        [NotMapped]
        public decimal StockQuantity
        {
            get
            {
                return (QtyPurchased ?? 0) - (ConsumptionEventsNavigation?.Sum(x => x.QuantityConsumed) ?? 0);
            }

            private set { }
        }

        [Display(Name = "Days to Confirm Order")]
        [ReadOnly(true)]
        [UIHint("text")]
        [NotMapped]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int? DaysToConfirm
        {
            get
            {
                if (DateConfirmed != null && DateAdded != null)
                {
                    return (int)((DateTime)DateConfirmed - (DateTime)DateAdded).TotalDays;
                }
                else
                {
                    return null;
                }
            }
            private set { }
        }

        [Display(Name = "Days for Shipping")]
        [ReadOnly(true)]
        [UIHint("text")]
        [NotMapped]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int? DaysToShip
        {
            get
            {
                if (DateArrived != null && DateShipped != null)
                {
                    return (int)((DateTime)DateArrived - (DateTime)DateShipped).TotalDays;
                }
                else
                {
                    return null;
                }
            }
            private set { }
        }

        [Display(Name = "Days for Completion")]
        [ReadOnly(true)]
        [UIHint("text")]
        [NotMapped]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int? DaysToComplete
        {
            get
            {
                if (DateArrived != null && DateAdded != null)
                {
                    return (int)((DateTime)DateArrived - (DateTime)DateAdded).TotalDays;
                }
                else
                {
                    return null;
                }
            }
            private set { }
        }

        [Display(Name = "Total Cost")]
        [ReadOnly(true)]
        [NotMapped]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DisplayFormat(DataFormatString = "{0:0.00##}", ApplyFormatInEditMode = true)]
        public decimal? TotalCost
        {
            get
            {
                if (FOBCost != null && FOBCost != 0  && ShippingCost != null && ShippingCost != 0 && ImportCosts != null && ImportCosts != 0)
                {
                    return FOBCost + ShippingCost + ImportCosts;
                }
                else
                {
                    return null;
                }
            }
            private set { }
        }

        //Cost Per Unit for Accounting
        [Display(Name = "Cost Per Unit")]
        [ReadOnly(true)]
        [NotMapped]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DisplayFormat(DataFormatString = "{0:0.00##}", ApplyFormatInEditMode = true)]
        public decimal? CostPerUnit
        {
            get
            {
                if (QtyPurchased != null && QtyPurchased != 0 && FOBCost != null && FOBCost != 0)
                {
                    return (int)(FOBCost ?? 0) / (QtyPurchased ?? 0);
                }
                else
                {
                    return null;
                }

            }
            private set { }
        }

        //Total Cost/Unit for Pricing
        [Display(Name = "Total Cost Per Unit")]
        [ReadOnly(true)]
        [NotMapped]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DisplayFormat(DataFormatString = "{0:0.00##}", ApplyFormatInEditMode = true)]
        public decimal? TotalCostPerUnit
        {
            get
            {
                if (TotalCost != null && TotalCost != 0 && QtyPurchased != null && QtyPurchased != 0)
                {
                    return (int)(TotalCost ?? 0) / (QtyPurchased ?? 0);
                }
                else
                {
                    return null;
                }
            }
            private set { }
        }

        [NotMapped]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string RelationshipDisplay
        {
            get
            {
                return (Code != "" ? "Code: " + Code + " - " : "") + (Description ?? "");
            }
            private set { }

            //get
            //{
            //    return (DateOfConsumption != null ? DateOfConsumption.Value.ToShortDateString() + " - " : "")
            //        + "Units Consumed: " + (QuantityConsumed != null ? QuantityConsumed.ToString() : "0");
            //}
            //private set { }
        }
    }
}
