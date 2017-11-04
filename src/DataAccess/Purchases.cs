using System;
using System.Collections.Generic;

namespace Egret_Dev.EF
{
    public partial class Purchases
    {
        public int Purchaseid { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? Dateentered { get; set; }
        public int? Itemid { get; set; }
        public int? Projectid { get; set; }
        public int? Qtypurchased { get; set; }
        public decimal? Price { get; set; }
        public int? Unitid { get; set; }

        public virtual Projects Project { get; set; }
        public virtual Units Unit { get; set; }
    }
}
