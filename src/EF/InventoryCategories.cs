using System;
using System.Collections.Generic;

namespace Egret_Dev.EF
{
    public partial class InventoryCategories
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Sortorder { get; set; }
    }
}
