using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egret.Models.Common
{
    /// <summary>
    /// Basic representation of an Inventory Lot
    /// use for API
    /// </summary>
    public class BasicInventoryLot
    {
        public string Description { get; set; }
        public string CustomerReservedFor { get; set; }
        public string Unit { get; set; }
    }
}
