using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egret.Utilities
{
    /// <summary>
    /// This class maps static references from the established Stock Levels to string values. 
    /// A typical enum would require convoluted reflection to perform the same functionality,
    /// since it naturally maps to ints instead of strings.
    /// </summary>
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
