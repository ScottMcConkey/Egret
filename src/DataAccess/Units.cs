using System;
using System.Collections.Generic;

namespace Egret.DataAccess
{
    public partial class Units
    {
        public Units()
        {
            InventoryItemsBuyunitNavigation = new HashSet<InventoryItems>();
            InventoryItemsSellunitNavigation = new HashSet<InventoryItems>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public int Sortorder { get; set; }
        public bool? Active { get; set; }

        public ICollection<InventoryItems> InventoryItemsBuyunitNavigation { get; set; }
        public ICollection<InventoryItems> InventoryItemsSellunitNavigation { get; set; }
    }
}
