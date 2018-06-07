﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Egret.Models;

namespace Egret.Models
{
    /// <summary>
    /// Test
    /// </summary>
    public partial class InventoryItem
    {
        [Key]
        public string Code { get; set; }

        [Display(Name = "Added")]
        [ReadOnly(true)]
        public DateTime? DateAdded { get; set; }

        [Display(Name = "Added By")]
        [ReadOnly(true)]
        public string AddedBy { get; set; }

        [Display(Name = "Updated")]
        [ReadOnly(true)]
        public DateTime? DateUpdated { get; set; }

        [Display(Name = "Updated By")]
        [ReadOnly(true)]
        public string UpdatedBy { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        [Display(Name = "Customer Purchased For")]
        public string CustomerPurchasedFor { get; set; }

        [Display(Name = "Customer Reserved For")]
        public string CustomerReservedFor { get; set; }

        [UIHint("Text")]
        public int? Supplier { get; set; }

        [Display(Name = "Quantity to Purchase Now")]
        public string QtyToPurchaseNow { get; set; }

        [Display(Name = "Approximate Production Qty")]
        public string ApproxProdQty { get; set; }

        [Display(Name = "Fabric Tests")]
        public string FabricTests_Conversion { get; set; }

        [Display(Name = "Fabric Test Results")]
        public string FabricTestResults { get; set; }

        [Display(Name = "Needed Before")]
        public DateTime? NeededBefore { get; set; }

        [Display(Name = "Target Price")]
        public string TargetPrice { get; set; }

        [Display(Name = "Shipping Company")]
        public string ShippingCompany { get; set; }

        [Display(Name = "Bonded Warehouse?")]
        public bool? BondedWarehouse { get; set; }

        [Display(Name = "Date of Order Confirmed")]
        public DateTime? DateConfirmed { get; set; }

        [Display(Name = "Date of Shipping")]
        public DateTime? DateShipped { get; set; }

        [Display(Name = "Date of Arrival")]
        public DateTime? DateArrived { get; set; }

        [Display(Name = "No. of Days to Confirm Order")]
        [ReadOnly(true)]
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

        [Display(Name = "No. of Days for Shipping")]
        [ReadOnly(true)]
        [NotMapped]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int? DaysToShip
        {
            get
            {
                if (DateArrived != null && DateShipped != null)
                {
                    return (int) ((DateTime)DateArrived - (DateTime) DateShipped).TotalDays;
                }
                else
                {
                    return null;
                }
            }
            private set { }
        }

        [Display(Name = "No. of Days for Completion")]
        [ReadOnly(true)]
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



        public string Comment { get; set; }

        [Display(Name = "Qty Purchased")]
        public decimal? QtyPurchased { get; set; }

        public string Unit { get; set; }

        [Display(Name = "FOB Cost Or Local Cost no VAT (Nrs)*")]
        public decimal? FOBCost { get; set; }

        [Display(Name = "Shipping Cost (Nrs)*")]
        public decimal? ShippingCost { get; set; }

        [Display(Name = "Import/Custom/Delivery Costs/VAT (Nrs)*")]
        public decimal? ImportCosts { get; set; }



        [Display(Name = "Total Cost")]
        [ReadOnly(true)]
        [NotMapped]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal? TotalCost
        {
            get
            {
                return (int) (FOBCost ?? 0) + (ImportCosts ?? 0);
            }
            private set { }
        }

        //Cost Per Unit for Accounting
        [Display(Name = "Cost Per Unit")]
        [ReadOnly(true)]
        [NotMapped]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
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


        [Display(Name = "Sell Price")]
        public double? Sellprice { get; set; }
        [Display(Name = "Sell Currency")]
        public string Sellcurrency { get; set; }
        [Display(Name = "Sell Unit")]
        public int? Sellunit { get; set; }
        [Display(Name = "Buy Price")]
        public double? Buyprice { get; set; }
        [Display(Name = "Buy Currency")]
        public string Buycurrency { get; set; }
        [Display(Name = "Buy Unit")]
        public int? Buyunit { get; set; }
        

        [Display(Name = "Is Conversion?")]
        public bool IsConversion { get; set; }
        [Display(Name = "Conversion Source")]
        [ReadOnly(true)]
        public string ConversionSource { get; set; }
        

        public CurrencyType BuycurrencyNavigation { get; set; }
        public Unit BuyunitNavigation { get; set; }
        public InventoryCategory CategoryNavigation { get; set; }
        public CurrencyType SellcurrencyNavigation { get; set; }
        public Unit SellunitNavigation { get; set; }
        public ICollection<FabricTest> FabricTests { get; set; }
    }
}
