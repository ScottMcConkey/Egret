using Egret.DataAccess;
using Egret.DataAccess.QueryModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Egret.Services
{
    public class ReportService : BaseService, IReportService
    {
        //ILogger _logger;

        public ReportService(EgretContext context/*, ILogger logger*/)
            : base(context)
        {
            //_logger = logger;
        }

        public List<Test> TotalValueByCategory()
        {
            return 
                Context.TestResults.FromSqlRaw(@"
                SELECT 
                    cat.name as name, 
                    to_char(coalesce(sum(t.stock_value), 0), '99999D9999') as stock_value
                FROM   public.inventory_categories cat
                LEFT OUTER JOIN
                (
	                SELECT
		                i.code,
		                i.category_id,
		                round(
			                (i.qty_purchased - sum(c.quantity_consumed)) --stock quantity
			                * 
			                ((i.fob_cost + i.shipping_cost + i.import_costs) / i.qty_purchased) --total cost per unit
		                , 4) as stock_value
	                FROM   public.inventory_items i
	                LEFT OUTER JOIN public.consumption_events c on c.inventory_item_code = i.code
                    -- watch out for performance issues below
                    WHERE COALESCE(i.fob_cost, i.shipping_cost, i.import_costs) IS NOT NULL
	                GROUP BY i.code, i.qty_purchased
                ) t on t.category_id = cat.id
                GROUP BY cat.name
                ORDER BY cat.name").ToList();
        }
    }
}
