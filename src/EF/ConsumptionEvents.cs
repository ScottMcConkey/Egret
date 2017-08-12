using System;
using System.Collections.Generic;

namespace Egret_Dev.EF
{
    public partial class ConsumptionEvents
    {
        public int Eventid { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? Dateentered { get; set; }
        public int? Itemid { get; set; }
        public int? Projectid { get; set; }
        public int? Qtyconsumed { get; set; }

        public virtual Projects Project { get; set; }
    }
}
