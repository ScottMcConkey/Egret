using Egret.DataAccess;
using Egret.DataAccess.QueryModels;
using Egret.Reports;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Egret.Services
{
    public class ReportService : BaseService, IReportService
    {
        //ILogger _logger;

        public ReportService(EgretDbContext context/*, ILogger logger*/)
            : base(context)
        {
            //_logger = logger;
        }

        private List<StockValueReport> TotalValueByCategory()
        {
            Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            return 
                Context.StockValueReportResults.FromSqlRaw(@"
                SELECT 
                    cat.name as name, 
                    cast(coalesce(sum(t.stock_value), 0) as text) as stock_value
                FROM   public.inventory_categories cat
                LEFT OUTER JOIN
                (
	                SELECT
		                i.inventory_item_id,
		                i.inventory_category_id,
		                round(
			                (i.qty_purchased - sum(c.quantity_consumed)) --stock quantity
			                * 
			                ((i.fob_cost + i.shipping_cost + i.import_costs) / i.qty_purchased) --total cost per unit
		                , 4) as stock_value
	                FROM   public.inventory_items i
	                LEFT OUTER JOIN public.consumption_events c on c.inventory_item_id = i.inventory_item_id
                    -- watch out for performance issues below
                    WHERE COALESCE(i.fob_cost, i.shipping_cost, i.import_costs) IS NOT NULL
	                GROUP BY i.inventory_item_id, i.qty_purchased
                ) t on t.inventory_category_id = cat.inventory_category_id
                GROUP BY cat.name
                ORDER BY cat.name").ToList();
        }

        public Stream GetTotalStockValueByCategoryReport()
        {
            var report = new Report
            {
                Title = "Total Stock Value By Category",
                Details = TotalValueByCategory()
            };

            var builder = new ReportBuilder();

            return builder.Build(report);
        }
    }
}
