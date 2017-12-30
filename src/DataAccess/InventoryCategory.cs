using System;
using System.Collections.Generic;

namespace Egret.DataAccess
{
    public partial class InventoryCategory
    {
        public InventoryCategory()
        {
            InventoryItems = new HashSet<InventoryItem>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Sortorder { get; set; }
        public bool? Active { get; set; }

        public ICollection<InventoryItem> InventoryItems { get; set; }
    }
}
