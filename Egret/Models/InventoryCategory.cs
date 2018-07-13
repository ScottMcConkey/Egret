using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Egret.Models
{
    public partial class InventoryCategory
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Display(Name = "Sort Order")]
        public int SortOrder { get; set; }

        public bool Active { get; set; }
    }
}
