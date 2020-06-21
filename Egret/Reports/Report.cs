using Egret.Reports;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egret.Reports
{
    public class Report : IReport
    {
        // Metadata
        public byte[] FileContents { get; set; }

        public string ContentType { get; set; } = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        public string FileDownloadName { get; set; } = "Report.xlsx";

        // Header
        public virtual string ImagePath { get; set; } = "~Images/report-title.png";

        public DateTime ReportDate { get; set; } = DateTime.Today;

        public virtual string Title { get; set; }

        // Body

        public IList Details { get; set; }
    }
}
