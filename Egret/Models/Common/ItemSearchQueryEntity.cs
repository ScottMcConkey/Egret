using System;

namespace Egret.Models.Common
{
    public class ItemSearchQueryEntity
    {
        public string ItemId { get; set; }

        public string Description { get; set; }

        public DateTime? DateCreatedStart { get; set; }

        public DateTime? DateCreatedEnd { get; set; }

        public int? Category { get; set; }

        public int? StorageLocation { get; set; }

        public string CustomerPurchasedFor { get; set; }

        public string CustomerReservedFor { get; set; }

        public string StockLevel { get; set; }

        public int ResultsPerPage { get; set; }
    }
}
