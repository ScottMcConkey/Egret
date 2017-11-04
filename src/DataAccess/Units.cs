using System;
using System.Collections.Generic;

namespace Egret_Dev.EF
{
    public partial class Units
    {
        public Units()
        {
            InventoryItems = new HashSet<InventoryItem>();
            Purchases = new HashSet<Purchases>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public int? Sortorder { get; set; }

        public virtual ICollection<InventoryItem> InventoryItems { get; set; }
        public virtual ICollection<Purchases> Purchases { get; set; }
    }
}
