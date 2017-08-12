using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace Egret_Dev.Models
{
    [Table("InventoryItem")]
    public partial class InventoryItem
    {
        [Key]
        public string ProductCode { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        
    }
}
