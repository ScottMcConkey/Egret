using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Egret.Models
{
    public class FabricTest
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Result { get; set; }

        public string InventoryItemCode { get; set; }

        public InventoryItem InventoryItem { get; set; }

    }
}
