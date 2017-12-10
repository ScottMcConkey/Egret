using System;
using System.Collections.Generic;

namespace Egret.DataAccess
{
    public partial class InventoryItems
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Comment { get; set; }
        public double? Sellprice { get; set; }
        public string Sellcurrency { get; set; }
        public string Sellunit { get; set; }
        public double? Buyprice { get; set; }
        public string Buycurrency { get; set; }
        public string Buyunit { get; set; }
        public double? Stockvalue { get; set; }
        public int? SupplierFk { get; set; }
        public int? Salesacct { get; set; }
        public int? Stockacct { get; set; }
        public int? Cogacct { get; set; }
        public double? Sohcount { get; set; }
        public double? Stocktakenewqty { get; set; }
        public int? Flags { get; set; }
        public double? Qtybrksellprice { get; set; }
        public double? Costprice { get; set; }
        public bool? Isconversion { get; set; }
        public string Conversionsource { get; set; }
        public string Useraddedby { get; set; }
        public string Userupdatedby { get; set; }
        public DateTime? Dateadded { get; set; }
        public DateTime? Dateupdated { get; set; }

        public CurrencyTypes BuycurrencyNavigation { get; set; }
        public Units BuyunitNavigation { get; set; }
        public InventoryCategories CategoryNavigation { get; set; }
        public CurrencyTypes SellcurrencyNavigation { get; set; }
        public Units SellunitNavigation { get; set; }
    }
}
