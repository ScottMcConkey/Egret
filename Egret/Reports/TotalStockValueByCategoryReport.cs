using System.Collections;

namespace Egret.Reports
{
    public class TotalStockValueByCategoryReport : BaseReport
    {
        public TotalStockValueByCategoryReport(IList objectList)
        {
            base.Header.Title = "Total Stock Value Report";
            base.Body.Details = objectList;
        }
    }
}
