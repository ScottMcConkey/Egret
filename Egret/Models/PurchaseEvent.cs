using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Egret.Models
{
    public partial class PurchaseEvent
    {
        public int Id { get; set; }

        [Display(Name = "Date Added")]
        public DateTime? DateAdded { get; set; }

        [Display(Name = "Added By")]
        public string AddedBy { get; set; }

        [Display(Name = "Date Updated")]
        public DateTime? DateUpdated { get; set; }

        [Display(Name = "Updated By")]
        public string UpdatedBy { get; set; }



        public string Description { get; set; }

        public string CustomerPurchasedFor { get; set; }

        public string Supplier { get; set; }

        public string QtyToPurchase { get; set; }

        public string FabricTesting { get; set; }

        public string TargetPrice { get; set; }

        public bool BondedWarehouse { get; set; }


    }
}
