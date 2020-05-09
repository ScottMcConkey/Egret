using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egret.Reports
{
    public class ReportHeader
    {
        public virtual string ImagePath { get; set; } = "~Images/report-title.png";

        public DateTime ReportDate { get; set; } = DateTime.Now;

        public virtual string Title { get; set; }
    }
}
