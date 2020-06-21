using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Egret.Models
{
    public class FabricTest
    {
        public string FabricTestId { get; set; }

        public string Name { get; set; }

        public string Result { get; set; }

        [NotMapped]
        public string InventoryItemId { get; set; }

        [NotMapped]
        public InventoryItem InventoryItemNavigation { get; set; }

    }
}
