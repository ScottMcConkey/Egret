using System;
using System.Collections.Generic;

namespace Egret.DataAccess
{
    public partial class InventoryCategories
    {
        public InventoryCategories()
        {
            InventoryItems = new HashSet<InventoryItems>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Sortorder { get; set; }
        public bool? Active { get; set; }

        public ICollection<InventoryItems> InventoryItems { get; set; }
    }
}
