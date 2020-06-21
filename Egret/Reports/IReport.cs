using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egret.Reports
{
    public interface IReport
    {
        byte[] FileContents { get; set; }

        string ContentType { get; set; }

        string FileDownloadName { get; set; }

        string ImagePath { get; set; }

        DateTime ReportDate { get; set; }

        string Title { get; set; }

        IList Details { get; set; }
    }
}
