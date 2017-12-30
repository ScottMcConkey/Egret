using System;
using System.Collections.Generic;

namespace Egret.DataAccess
{
    public partial class CurrencyType
    {
        public CurrencyType()
        {
            InventoryItemsBuycurrencyNavigation = new HashSet<InventoryItem>();
            InventoryItemsSellcurrencyNavigation = new HashSet<InventoryItem>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public int Sortorder { get; set; }
        public string Abbreviation { get; set; }
        public bool? Active { get; set; }

        public ICollection<InventoryItem> InventoryItemsBuycurrencyNavigation { get; set; }
        public ICollection<InventoryItem> InventoryItemsSellcurrencyNavigation { get; set; }
    }
}
