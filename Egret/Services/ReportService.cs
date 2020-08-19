using Egret.DataAccess;
using Egret.Models.QueryModels;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
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
			                (i.quantity_purchased - sum(COALESCE(c.quantity_consumed, 0))) --stock quantity
			                * 
			                ((COALESCE(i.fob_cost, 0) + COALESCE(i.shipping_cost, 0) + COALESCE(i.import_cost, 0) + COALESCE(i.vat_cost, 0)) / i.quantity_purchased) --total cost per unit
		                , 2) as stock_value
	                FROM   public.inventory_items i
	                LEFT OUTER JOIN public.consumption_events c on c.inventory_item_id = i.inventory_item_id
	                -- watch out for performance issues below
	                WHERE COALESCE(i.fob_cost, i.shipping_cost, i.import_cost, i.vat_cost) IS NOT NULL
	                GROUP BY i.inventory_item_id, i.quantity_purchased
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

        public List<InventoryFinancialData> GetInventoryFinancialData()
        {
            var sql = @"
                SELECT 
                    cat.name as ""Category"",
	                i.inventory_item_id as ""Item Id"",
	                i.description as ""Item Description"",
	                i.quantity_purchased as ""Qty Purchased"",
	                u.abbreviation as ""Unit(s)"",
	                i.fob_cost as ""FOB Cost"",
	                i.shipping_cost as ""Shipping Cost"",
	                i.import_cost as ""Import Cost"",
	                i.vat_cost as ""VAT Cost"",
	                c.consumption_event_id as ""Consumption Id"",
	                c.quantity_consumed as ""Qty Consumed"",
	                c.date_consumed as ""Date Consumed""
                FROM

                    public.inventory_categories cat

                    LEFT OUTER JOIN public.inventory_items i ON i.inventory_category_id = cat.inventory_category_id
                    LEFT OUTER JOIN public.units u ON u.unit_id = i.unit_id
                    LEFT OUTER JOIN public.consumption_events c on c.inventory_item_id = i.inventory_item_id
                ORDER BY cat.name";

            var dbConnectionString = _config.GetConnectionString("DefaultConnection");

            var list = new List<InventoryFinancialData>();

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
                        list.Add(new InventoryFinancialData { 
                            Category = !dr.IsDBNull(0) ? dr.GetString(0) : string.Empty,
                            ItemId = !dr.IsDBNull(1) ? dr.GetString(1) : string.Empty,
                            ItemDescription = !dr.IsDBNull(2) ? dr.GetString(2) : string.Empty,
                            QtyPurchased = !dr.IsDBNull(3) ? dr.GetDecimal(3).ToString() : string.Empty,
                            Unit = !dr.IsDBNull(4) ? dr.GetString(4) : string.Empty,
                            FOBCost = !dr.IsDBNull(5) ? dr.GetDecimal(5).ToString() : string.Empty,
                            ShippingCost = !dr.IsDBNull(6) ? dr.GetDecimal(6).ToString() : string.Empty,
                            ImportCost = !dr.IsDBNull(7) ? dr.GetDecimal(7).ToString() : string.Empty,
                            VATCost = !dr.IsDBNull(8) ? dr.GetDecimal(8).ToString() : string.Empty,
                            ConsumptionId = !dr.IsDBNull(9) ? dr.GetString(9).ToString() : string.Empty,
                            QtyConsumed = !dr.IsDBNull(10) ? dr.GetDecimal(10).ToString() : string.Empty,
                            DateConsumed = !dr.IsDBNull(11) ? dr.GetDateTime(11).ToString() : string.Empty
                        });
                    }
                }
            }

            return list;
        }
    }
}
