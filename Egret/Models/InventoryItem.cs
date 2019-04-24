using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions;
using Egret.Attributes;
using Egret.Interfaces;

namespace Egret.Models
{
    public partial class InventoryItem : IAuditable, IHasStockCount
    {
        [Language(Name = "Nepali", Value = "कोड")]
        public string Code { get; set; }

        [Display(Name = "Date Added")]
        [BindNever]
        public DateTime? DateAdded { get; set; }

        [Display(Name = "Added By")]
        [BindNever]
        public string AddedBy { get; set; }

        [Display(Name = "Date Updated")]
        [BindNever]
        public DateTime? DateUpdated { get; set; }

        [Display(Name = "Updated By")]
        [BindNever]
        public string UpdatedBy { get; set; }

        [Required]
        [Language(Name = "Nepali", Value = "वर्णन")]
        public string Description { get; set; }

        [Required]
        [Language(Name = "Nepali", Value = "वर्ग")]
        public string Category { get; set; }

        [Required]
        [Display(Name = "Quantity Purchased")]
        [Language(Name = "Nepali", Value = "मात्रा खरिद गरियो")]
        [DisplayFormat(DataFormatString = "{0:0.00##}", ApplyFormatInEditMode = true)]
        public decimal? QtyPurchased { get; set; }

        [Required]
        public string Unit { get; set; }

        [Display(Name = "FOB Cost Or Local Cost no VAT")]
        [Language(Name = "Nepali", Value = "लागत")]
        [DisplayFormat(DataFormatString = "{0:0.00##}", ApplyFormatInEditMode = true)]
        public decimal? FOBCost { get; set; }

        public string FOBCostCurrency { get; set; }

        [Display(Name = "Shipping Cost")]
        [Language(Name = "Nepali", Value = "ढुवानी खर्च")]
        [DisplayFormat(DataFormatString = "{0:0.00##}", ApplyFormatInEditMode = true)]
        public decimal? ShippingCost { get; set; }

        public string ShippingCostCurrency { get; set; }

        [Display(Name = "Import/Custom/Delivery Costs/VAT")]
        [Language(Name = "Nepali", Value = "आयात लागत")]
        [DisplayFormat(DataFormatString = "{0:0.00##}", ApplyFormatInEditMode = true)]
        public decimal? ImportCosts { get; set; }

        public string ImportCostCurrency { get; set; }

        [Required]
        [Display(Name = "Customer Purchased For")]
        [Language(Name = "Nepali", Value = "ग्राहक खरिद गरियो")]
        public string CustomerPurchasedFor { get; set; }

        [Required]
        [Display(Name = "Customer Reserved For")]
        [Language(Name = "Nepali", Value = "ग्राहक आरक्षितको लागी")]
        public string CustomerReservedFor { get; set; }

        [Language(Name = "Nepali", Value = "प्रदायक")]
        public string Supplier { get; set; }

        [Display(Name = "Quantity to Purchase Now")]
        [Language(Name = "Nepali", Value = "अब खरिद गर्न मात्रा")]
        public string QtyToPurchaseNow { get; set; }

        [Display(Name = "Approximate Production Qty")]
        [Language(Name = "Nepali", Value = "अनुमानित उत्पादन मात्रा")]
        public string ApproxProdQty { get; set; }

        [Display(Name = "Needed Before")]
        [Language(Name = "Nepali", Value = "पहिले चाहिन्छ")]
        [UIHint("date")]
        public DateTime? NeededBefore { get; set; }

        [Display(Name = "Target Price")]
        [Language(Name = "Nepali", Value = "लक्षित मुल्य")]
        public string TargetPrice { get; set; }

        [Display(Name = "Shipping Company")]
        [Language(Name = "Nepali", Value = "ढुवानी कम्पनी")]
        public string ShippingCompany { get; set; }

        [Display(Name = "Bonded Warehouse?")]
        [Language(Name = "Nepali", Value = "बंधुआ गोदाम")]
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

        [Language(Name = "Nepali", Value = "टिप्पणीहरू")]
        public string Comments { get; set; }

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
        public decimal? StockQuantity
        {
            get
            {
                if (QtyPurchased == null)
                { return null; }

                if (QtyPurchased < 0)
                { return QtyPurchased; }

                return (QtyPurchased - (ConsumptionEventsNavigation?.Sum(x => x.QuantityConsumed) ?? 0));
            }

            private set { }
        }

        [Display(Name = "Stock Level")]
        [UIHint("text")]
        [NotMapped]
        public string StockLevel
        {
            get
            {
                if (StockQuantity == null)
                {
                    return "Unknown";
                }
                else if (StockQuantity == 0)
                {
                    return "Out of Stock";
                }
                else if (StockQuantity > 0)
                {
                    return "In Stock";
                }
                else
                {
                    return "Error";
                }
                
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
        [Language(Name = "Nepali", Value = "कुल खर्च")]
        [ReadOnly(true)]
        [NotMapped]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DisplayFormat(DataFormatString = "{0:#,###,##0.00##}", ApplyFormatInEditMode = true)]
        public decimal? TotalCost
        {
            get
            {
                return (FOBCost ?? 0) + (ShippingCost ?? 0) + (ImportCosts ?? 0);
            }
            private set { }
        }

        //Cost Per Unit for Accounting
        [Display(Name = "Cost Per Unit")]
        [Language(Name = "Nepali", Value = "लागत प्रति एकाइ")]
        [ReadOnly(true)]
        [NotMapped]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DisplayFormat(DataFormatString = "{0:#,###,##0.00##}", ApplyFormatInEditMode = true)]
        public decimal? CostPerUnit
        {
            get
            {
                if (QtyPurchased != null && QtyPurchased != 0 && FOBCost != null && FOBCost != 0)
                {
                    return (int)(FOBCost / QtyPurchased);
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
        [Language(Name = "Nepali", Value = "कुल लागत प्रति इकाई")]
        [ReadOnly(true)]
        [NotMapped]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DisplayFormat(DataFormatString = "{0:#,###,##0.00##}", ApplyFormatInEditMode = true)]
        public decimal? TotalCostPerUnit
        {
            get
            {
                if (TotalCost != null && TotalCost != 0 && QtyPurchased != null && QtyPurchased != 0)
                {
                    return (int)(TotalCost / QtyPurchased);
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
                string output = (Code != "" ? "Code: " + Code + " - " : "") + (Description ?? "");
                if (output.Length > 40)
                {
                    output = output.Substring(0, 40);
                }
                return output;
            }
            private set { }

        }
    }
}
