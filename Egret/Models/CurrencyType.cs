using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Egret.Models
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
        [Display(Name = "Sort Order")]
        [Range(1, System.Int32.MaxValue, ErrorMessage = "Sort Order must be a postitive integer.")]
        public int SortOrder { get; set; }
        public string Abbreviation { get; set; }
        public bool Active { get; set; }
        [Display(Name = "Default")]
        public bool DefaultSelection { get; set; }

        public ICollection<InventoryItem> InventoryItemsBuycurrencyNavigation { get; set; }
        public ICollection<InventoryItem> InventoryItemsSellcurrencyNavigation { get; set; }
    }
}
