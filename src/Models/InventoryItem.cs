using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Egret.Models;

namespace Egret.Models
{
    public partial class InventoryItem
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Comment { get; set; }
        [Display(Name = "Sell Price")]
        public double? Sellprice { get; set; }
        public string Sellcurrency { get; set; }
        public int? Sellunit { get; set; }
        [Display(Name = "Buy Price")]
        public double? Buyprice { get; set; }
        public string Buycurrency { get; set; }
        public int? Buyunit { get; set; }
        [Display(Name = "Stock Value")]
        public double? Stockvalue { get; set; }
        [Display(Name = "Supplier")]
        [UIHint("Text")]
        public int? Supplier { get; set; }
        [Display(Name = "Sales Account")]
        [UIHint("Text")]
        public int? Salesacct { get; set; }
        [Display(Name = "Stock Account")]
        [UIHint("Text")]
        public int? Stockacct { get; set; }
        [Display(Name = "COG Account")]
        [UIHint("Text")]
        public int? Cogacct { get; set; }
        [Display(Name = "SOH Count")]
        [UIHint("Text")]
        public double? Sohcount { get; set; }
        [Display(Name = "Stock Take New Qty")]
        public double? Stocktakenewqty { get; set; }
        [UIHint("Text")]
        public int? Flags { get; set; }
        [Display(Name = "Qty Brk Sell Price")]
        public double? Qtybrksellprice { get; set; }
        [Display(Name = "Cost Price")]
        public double? Costprice { get; set; }
        [Display(Name = "Is Conversion?")]
        public bool? Isconversion { get; set; }
        [Display(Name = "Conversion Source")]
        public string Conversionsource { get; set; }
        [Display(Name = "Added By")]
        [Editable(false)]
        public string Addedby { get; set; }
        [Display(Name = "Updated By")]
        [Editable(false)]
        public string Updatedby { get; set; }
        [Display(Name = "Added")]
        [Editable(false)]
        public DateTime? Dateadded { get; set; }
        [Display(Name = "Updated")]
        [Editable(false)]
        public DateTime? Dateupdated { get; set; }

        public CurrencyType BuycurrencyNavigation { get; set; }
        public Unit BuyunitNavigation { get; set; }
        public InventoryCategory CategoryNavigation { get; set; }
        public CurrencyType SellcurrencyNavigation { get; set; }
        public Unit SellunitNavigation { get; set; }
    }
}
