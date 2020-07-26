using System.IO;

namespace Egret.Services
{
    public interface IReportService
    {
        //List<StockValueReport> TotalValueByCategory();

        Stream GetTotalStockValueByCategoryReport();
    }
}
