﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Egret.Models;

namespace Egret.Models
{
    /// <summary>
    /// Test
    /// </summary>
    public partial class InventoryItem
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Comment { get; set; }
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
        public bool Isconversion { get; set; }
        [Display(Name = "Conversion Source")]
        [ReadOnly(true)]
        public string Conversionsource { get; set; }
        [Display(Name = "Added By")]
        [ReadOnly(true)]
        public string Addedby { get; set; }
        [Display(Name = "Updated By")]
        [ReadOnly(true)]
        public string Updatedby { get; set; }
        [Display(Name = "Added")]
        [ReadOnly(true)]
        public DateTime? Dateadded { get; set; }
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