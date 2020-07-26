using System;
using System.Collections;

namespace Egret.Reports
{
    public class Report
    {
        public string WorksheetName { get; set; } = "Report.xlsx";
        public string FileDownloadName { get; set; } = "Report.xlsx";
        public string Title { get; set; }
        public DateTime ReportDate { get; set; } = DateTime.Today;
        public string ContentType { get; set; } = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        public string ColumnNames { get; set; }
        public IList Details { get; set; }
    }
}
