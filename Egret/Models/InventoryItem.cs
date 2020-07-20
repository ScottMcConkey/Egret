using Egret.Attributes;
using Egret.Widgets;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Egret.Models
{
    [CssIconClass("egret egret-inventory")]
    public partial class InventoryItem : IAuditable, IStockLevel
    {
        [Display(Name = "Code")]
        [Language(Name = "Nepali", Value = "कोड")]
        public string InventoryItemId { get; set; }

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
        [Display(Name = "Category")]
        [Language(Name = "Nepali", Value = "वर्ग")]
        public int? InventoryCategoryId { get; set; }

        [Display(Name = "Storage Location")]
        [Language(Name = "Nepali", Value = "भण्डारण स्थान")]
        public int? StorageLocationId { get; set; }

        [Required]
        [Display(Name = "Quantity Purchased")]
        [Language(Name = "Nepali", Value = "मात्रा खरिद गरियो")]
        [DisplayFormat(DataFormatString = "{0:0.####}", ApplyFormatInEditMode = true)]
        public decimal? QtyPurchased { get; set; }

        [Required]
        [Display(Name = "Unit")]
        public int UnitId { get; set; }

        [Display(Name = "FOB Cost Or Local Cost no VAT")]
        [Language(Name = "Nepali", Value = "लागत")]
        [DisplayFormat(DataFormatString = "{0:0.00##}", ApplyFormatInEditMode = true)]
        public decimal? FobCost { get; set; }

        [Display(Name = "Shipping Cost")]
        [Language(Name = "Nepali", Value = "ढुवानी खर्च")]
        [DisplayFormat(DataFormatString = "{0:0.00##}", ApplyFormatInEditMode = true)]
        public decimal? ShippingCost { get; set; }

        [Display(Name = "Import/Custom/Delivery Costs/VAT")]
        [Language(Name = "Nepali", Value = "आयात लागत")]
        [DisplayFormat(DataFormatString = "{0:0.00##}", ApplyFormatInEditMode = true)]
        public decimal? ImportCosts { get; set; }

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

        [Display(Name = "Date Confirmed")]
        [UIHint("Date")]
        public DateTime? DateConfirmed { get; set; }

        [Display(Name = "Date Shipped")]
        [UIHint("Date")]
        public DateTime? DateShipped { get; set; }

        [Display(Name = "Date Arrived")]
        [UIHint("Date")]
        public DateTime? DateArrived { get; set; }

        [Language(Name = "Nepali", Value = "टिप्पणीहरू")]
        public string Comments { get; set; }

        [NotMapped]
        public InventoryCategory CategoryNavigation { get; set; }
        [NotMapped]
        public Unit UnitNavigation { get; set; }
        [NotMapped]
        public CurrencyType FobCostCurrencyNavigation { get; set; }
        [NotMapped]
        public CurrencyType ShippingCostCurrencyNavigation { get; set; }
        [NotMapped]
        public CurrencyType ImportCostCurrencyNavigation { get; set; }
        [NotMapped]
        public StorageLocation StorageLocationNavigation { get; set; }
        [NotMapped]
        public ICollection<FabricTest> FabricTestsNavigation { get; set; }
        [NotMapped]
        public ICollection<ConsumptionEvent> ConsumptionEventsNavigation { get; set; }

        [Display(Name = "Quantity in Stock")]
        [DisplayFormat(DataFormatString = "{0:0.####}", ApplyFormatInEditMode = true)]
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
                    return Utilities.ItemStockLevel.Unknown.Value;
                }
                else if (StockQuantity == 0)
                {
                    return Utilities.ItemStockLevel.OutOfStock.Value;
                }
                else if (StockQuantity > 0)
                {
                    return Utilities.ItemStockLevel.InStock.Value;
                }
                else
                {
                    return Utilities.ItemStockLevel.Error.Value;
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
                if (DateArrived < DateShipped)
                {
                    return null;
                }

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
                if (DateArrived < DateAdded)
                {
                    return null;
                }

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
                if (FobCost == null && ShippingCost == null && ImportCosts == null)
                {
                    return null;
                }

                return (FobCost ?? 0) + (ShippingCost ?? 0) + (ImportCosts ?? 0);
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
                if (QtyPurchased > 0 && FobCost > 0)
                {
                    return (decimal)(FobCost / QtyPurchased);
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
                if (TotalCost > 0 && QtyPurchased > 0)
                {
                    return (decimal)(TotalCost / QtyPurchased);
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
                string output = (InventoryItemId != "" ? "Code: " + InventoryItemId + " - " : "") + (Description ?? "");
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
