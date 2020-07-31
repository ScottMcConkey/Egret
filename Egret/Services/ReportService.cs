using Egret.DataAccess;
using Egret.Models.QueryModels;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Collections.Generic;
using System.Data;

namespace Egret.Services
{
    public class ReportService : BaseService
    {
        private readonly IConfiguration _config;

        public ReportService(EgretDbContext context, IConfiguration config)
            : base(context)
        {
            _config = config;
        }

        public List<CategoryStockValue> GetTotalStockValueByCategoryReport()
        {
            var sql = @"
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
			                ((i.fob_cost + i.shipping_cost + i.import_cost + i.vat_cost) / i.qty_purchased) --total cost per unit
		                , 2) as stock_value
	                FROM   public.inventory_items i
	                LEFT OUTER JOIN public.consumption_events c on c.inventory_item_id = i.inventory_item_id
                    -- watch out for performance issues below
                    WHERE COALESCE(i.fob_cost, i.shipping_cost, i.import_cost, i.vat_cost) IS NOT NULL
	                GROUP BY i.inventory_item_id, i.qty_purchased
                ) t on t.inventory_category_id = cat.inventory_category_id
                GROUP BY cat.name
                ORDER BY cat.name";

            var dbConnectionString = _config.GetConnectionString("DefaultConnection");

            var list = new List<CategoryStockValue>();

            using (var sqlConnection = new NpgsqlConnection(dbConnectionString))
            {
                sqlConnection.Open();
                var command = new NpgsqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = sql,
                    Connection = sqlConnection
                };

                using (NpgsqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        list.Add(new CategoryStockValue { Name = dr.GetString(0), StockValue = dr.GetString(1) });
                    }
                }
            }

            return list;
        }
    }
}
