using System;
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
        public string Code { get; set; }

        [Display(Name = "Added")]
        [ReadOnly(true)]
        public DateTime? DateAdded { get; set; }

        [Display(Name = "Updated By")]
        [ReadOnly(true)]
        public string Updatedby { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public string CustomerPurchasedFor { get; set; }

        public string CustomerReservedFor { get; set; }

        [UIHint("Text")]
        public int? Supplier { get; set; }

        [Display(Name = "Quantity to Purchase Now")]
        public string QtyToPurchaseNow { get; set; }

        [Display(Name = "Approximate Production Qty")]
        public string ApproxProdQty { get; set; }

        [Display(Name = "Fabric Tests")]
        public string FabricTests { get; set; }

        [Display(Name = "Fabric Test Results")]
        public string FabricTestResults { get; set; }

        [Display(Name = "Needed Before")]
        public DateTime? NeededBefore { get; set; }

        [Display(Name = "Target Price")]
        public string TargetPrice { get; set; }

        [Display(Name = "Bonded Warehouse?")]
        public string ShippingCompany { get; set; }

        [Display(Name = "Bonded Warehouse?")]
        public bool? BondedWarehouse { get; set; }

        [Display(Name = "Date of Order Confirmed")]
        public DateTime? DateOrderConfirmed { get; set; }

        // Calculate no of days

        [Display(Name = "Date of Shipping")]
        public DateTime? DateShipped { get; set; }

        [Display(Name = "Total Days for Shipping")]
        public int? DaysForShipping { get; set; }

        [Display(Name = "Arrival Date / Completed Task Date")]
        public DateTime? DateCompleted { get; set; }

        public string Comment { get; set; }

        [Display(Name = "Qty Purchased")]
        public decimal? QtyPruchased { get; set; }

        public string Unit { get; set; }

        [Display(Name = "FOB Cost Or Local Cost no VAT (Nrs)*")]
        public decimal? FOBCost { get; set; }

        [Display(Name = "Shipping Cost (Nrs)*")]
        public decimal? ShippingCost { get; set; }

        [Display(Name = "Import/Custom/Delivery Costs/VAT (Nrs)*")]
        public decimal? ImportCosts { get; set; }

        [Display(Name = "Total Cost")]
        public decimal? TotalCost { get; set; }

        //Cost Per Unit for Accounting
        [NotMapped]
        public decimal? CostPerUnit { get; }

        //Total Cost/Unit for Pricing
        [NotMapped]
        public decimal? TotalCostPerUnit { get; }




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
        //[Display(Name = "Stock Value")]
        //public double? Stockvalue { get; set; }
        
        //[Display(Name = "Sales Account")]
        //[UIHint("Text")]
        //public int? Salesacct { get; set; }
        //[Display(Name = "Stock Account")]
        //[UIHint("Text")]
        //public int? Stockacct { get; set; }
        //[Display(Name = "COG Account")]
        //[UIHint("Text")]
        //public int? Cogacct { get; set; }
        //[Display(Name = "SOH Count")]
        //[UIHint("Text")]
        //public double? Sohcount { get; set; }
        //[Display(Name = "Stock Take New Qty")]
        //public double? Stocktakenewqty { get; set; }
        //[UIHint("Text")]
        //public int? Flags { get; set; }
        //[Display(Name = "Qty Brk Sell Price")]
        //public double? Qtybrksellprice { get; set; }
        //[Display(Name = "Cost Price")]
        //public double? Costprice { get; set; }

        [Display(Name = "Is Conversion?")]
        public bool IsConversion { get; set; }
        [Display(Name = "Conversion Source")]
        [ReadOnly(true)]
        public string ConversionSource { get; set; }
        [Display(Name = "Added By")]
        [ReadOnly(true)]
        public string Addedby { get; set; }
        
        
        
        
        [Display(Name = "Updated")]
        [ReadOnly(true)]
        public DateTime? Dateupdated { get; set; }

        public CurrencyType BuycurrencyNavigation { get; set; }
        public Unit BuyunitNavigation { get; set; }
        public InventoryCategory CategoryNavigation { get; set; }
        public CurrencyType SellcurrencyNavigation { get; set; }
        public Unit SellunitNavigation { get; set; }
    }
}
