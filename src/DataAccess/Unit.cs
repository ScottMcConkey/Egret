﻿using System;
using System.Collections.Generic;

namespace Egret.DataAccess
{
    public partial class Unit
    {
        public Unit()
        {
            InventoryItemsBuyunitNavigation = new HashSet<InventoryItem>();
            InventoryItemsSellunitNavigation = new HashSet<InventoryItem>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public int Sortorder { get; set; }
        public bool? Active { get; set; }

        public ICollection<InventoryItem> InventoryItemsBuyunitNavigation { get; set; }
        public ICollection<InventoryItem> InventoryItemsSellunitNavigation { get; set; }
    }
}
