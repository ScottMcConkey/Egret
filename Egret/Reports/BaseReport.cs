using Egret.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egret.Reports
{
    public class BaseReport
    {
        public virtual ReportHeader Header { get; set; }

        public virtual ReportBody Body { get; set; }
    }
}
