using System;
using System.Collections.Generic;

namespace Egret.DataAccess
{
    public partial class CurrencyTypes
    {
        public CurrencyTypes()
        {
            InventoryItemsBuycurrencyNavigation = new HashSet<InventoryItems>();
            InventoryItemsSellcurrencyNavigation = new HashSet<InventoryItems>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public int Sortorder { get; set; }
        public string Abbreviation { get; set; }
        public bool? Active { get; set; }

        public ICollection<InventoryItems> InventoryItemsBuycurrencyNavigation { get; set; }
        public ICollection<InventoryItems> InventoryItemsSellcurrencyNavigation { get; set; }
    }
}
