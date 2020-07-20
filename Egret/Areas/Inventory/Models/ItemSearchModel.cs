using Egret.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace Egret.Areas.Inventory.Models
{
    public class ItemSearchModel : PagingInfo
    {
        public string Code { get; set; }
        public string Description { get; set; }

        [UIHint("date")]
        public DateTime? DateCreatedStart { get; set; }

        [UIHint("date")]
        public DateTime? DateCreatedEnd { get; set; }

        public int? Category { get; set; }

        [Display(Name = "Storage Location")]
        public int? StorageLocation { get; set; }

        [Display(Name = "Customer Purchased For")]
        public string CustomerPurchasedFor { get; set; }

        [Display(Name = "Customer Reserved For")]
        public string CustomerReservedFor { get; set; }

        [Display(Name = "Stock Level")]
        public string StockLevel { get; set; }

        public int ResultsPerPage { get; set; }
    }
}
