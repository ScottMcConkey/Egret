using System;
using System.Collections.Generic;

namespace Egret_Dev.EF
{
    public partial class InventoryItem
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public double? Stockonhand { get; set; }
        public double? Stockvalue { get; set; }
        public double? Sellingunitprice { get; set; }
        public string Comment { get; set; }
        public int? Supplierid { get; set; }
        public int? Salesacct { get; set; }
        public int? Stockacct { get; set; }
        public int? Cogacct { get; set; }
        public int? Sellingunitid { get; set; }
        public int? Createdbyuser { get; set; }
        public int? Lastupdatedbyuser { get; set; }
        public int? Stocktakenewqty { get; set; }
        public int? Flags { get; set; }
        public int? Minbuildqty { get; set; }
        public int? Normalbuildqty { get; set; }
        public int? Sohcount { get; set; }
        public double? Costprice { get; set; }
        public int? Buyingunitid { get; set; }
        public string Buyingunit { get; set; }
        public string MigrCounted { get; set; }
        public string Sellingunit { get; set; }
        public double? Buyingunitprice { get; set; }

        public virtual Units CreatedbyuserNavigation { get; set; }
    }
}
