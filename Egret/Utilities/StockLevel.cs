using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egret.Utilities
{
    public class ItemStockLevel
    {
        private ItemStockLevel(string value) { Value = value; }

        public string Value { get; set; }


        public static ItemStockLevel InStock { get { return new ItemStockLevel("In Stock"); } }

        public static ItemStockLevel OutOfStock { get { return new ItemStockLevel("Out of Stock"); } }

        public static ItemStockLevel Unknown { get { return new ItemStockLevel("Unknown"); } }

        public static ItemStockLevel Error { get { return new ItemStockLevel("Error"); } }
    }
}
