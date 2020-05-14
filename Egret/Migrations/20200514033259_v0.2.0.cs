using Microsoft.EntityFrameworkCore.Migrations;

namespace Egret.Migrations
{
    public partial class v020 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_inventory_items_inventory_categories_category_id",
                table: "inventory_items");

            migrationBuilder.DropForeignKey(
                name: "fk_inventory_items_currency_types_fob_cost_currency_id",
                table: "inventory_items");

            migrationBuilder.DropForeignKey(
                name: "fk_inventory_items_currency_types_import_cost_currency_id",
                table: "inventory_items");

            migrationBuilder.DropForeignKey(
                name: "fk_inventory_items_currency_types_shipping_cost_currency_id",
                table: "inventory_items");

            migrationBuilder.DropForeignKey(
                name: "fk_inventory_items_units_unit_id",
                table: "inventory_items");

            migrationBuilder.DropPrimaryKey(
                name: "pk_units",
                table: "units");

            migrationBuilder.DropIndex(
                name: "ix_inventory_items_category_id",
                table: "inventory_items");

            migrationBuilder.DropPrimaryKey(
                name: "pk_inventory_categories",
                table: "inventory_categories");

            migrationBuilder.DropPrimaryKey(
                name: "pk_fabric_tests",
                table: "fabric_tests");

            migrationBuilder.DropPrimaryKey(
                name: "pk_currency_types",
                table: "currency_types");

            migrationBuilder.DeleteData(
                table: "currency_types",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "inventory_categories",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "inventory_categories",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "inventory_categories",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "inventory_categories",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "inventory_categories",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "inventory_categories",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "inventory_categories",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "inventory_categories",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "inventory_categories",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "units",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "units",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "units",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "units",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "id",
                table: "units");

            migrationBuilder.DropColumn(
                name: "category_id",
                table: "inventory_items");

            migrationBuilder.DropColumn(
                name: "id",
                table: "inventory_categories");

            migrationBuilder.DropColumn(
                name: "id",
                table: "fabric_tests");

            migrationBuilder.DropColumn(
                name: "id",
                table: "currency_types");

            migrationBuilder.AddColumn<int>(
                name: "unit_id",
                table: "units",
                nullable: false,
                defaultValueSql: "nextval('units_id_seq'::regclass)");

            migrationBuilder.AddColumn<int>(
                name: "inventory_category_id",
                table: "inventory_items",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "inventory_category_id",
                table: "inventory_categories",
                nullable: false,
                defaultValueSql: "nextval('inventory_categories_id_seq'::regclass)");

            migrationBuilder.AddColumn<string>(
                name: "fabric_test_id",
                table: "fabric_tests",
                nullable: false,
                defaultValueSql: "nextval('fabric_tests_id_seq'::regclass)");

            migrationBuilder.AddColumn<int>(
                name: "currency_type_id",
                table: "currency_types",
                nullable: false,
                defaultValueSql: "nextval('currency_types_id_seq'::regclass)");

            migrationBuilder.AddPrimaryKey(
                name: "pk_units",
                table: "units",
                column: "unit_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_inventory_categories",
                table: "inventory_categories",
                column: "inventory_category_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_fabric_tests",
                table: "fabric_tests",
                column: "fabric_test_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_currency_types",
                table: "currency_types",
                column: "currency_type_id");

            migrationBuilder.InsertData(
                table: "currency_types",
                columns: new[] { "currency_type_id", "abbreviation", "active", "default_selection", "name", "sort_order", "symbol" },
                values: new object[] { 1, "NRP", true, true, "Nepali Rupees", 1, "रु" });

            migrationBuilder.InsertData(
                table: "inventory_categories",
                columns: new[] { "inventory_category_id", "active", "description", "name", "sort_order" },
                values: new object[,]
                {
                    { 1, true, "", "Elastic", 1 },
                    { 2, true, "", "Fastener", 2 },
                    { 3, true, "", "Knit", 3 },
                    { 4, true, "", "Labels and Tags", 4 },
                    { 5, true, "", "Leather", 5 },
                    { 6, true, "", "Other", 6 },
                    { 7, true, "", "Thread", 7 },
                    { 8, true, "", "Woven", 8 },
                    { 9, true, "", "Zipper", 9 }
                });

            migrationBuilder.InsertData(
                table: "units",
                columns: new[] { "unit_id", "abbreviation", "active", "name", "sort_order" },
                values: new object[,]
                {
                    { 1, "kg", true, "kilogram", 1 },
                    { 2, "m", true, "meter", 2 },
                    { 3, "piece", true, "piece", 3 },
                    { 4, "set", true, "set", 4 }
                });

            migrationBuilder.CreateIndex(
                name: "ix_inventory_items_inventory_category_id",
                table: "inventory_items",
                column: "inventory_category_id");

            migrationBuilder.AddForeignKey(
                name: "fk_inventory_items_currency_types_fob_cost_currency_id",
                table: "inventory_items",
                column: "fob_cost_currency_id",
                principalTable: "currency_types",
                principalColumn: "currency_type_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_inventory_items_currency_types_import_cost_currency_id",
                table: "inventory_items",
                column: "import_cost_currency_id",
                principalTable: "currency_types",
                principalColumn: "currency_type_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_inventory_items_inventory_categories_inventory_category_id",
                table: "inventory_items",
                column: "inventory_category_id",
                principalTable: "inventory_categories",
                principalColumn: "inventory_category_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_inventory_items_currency_types_shipping_cost_currency_id",
                table: "inventory_items",
                column: "shipping_cost_currency_id",
                principalTable: "currency_types",
                principalColumn: "currency_type_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_inventory_items_units_unit_id",
                table: "inventory_items",
                column: "unit_id",
                principalTable: "units",
                principalColumn: "unit_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_inventory_items_currency_types_fob_cost_currency_id",
                table: "inventory_items");

            migrationBuilder.DropForeignKey(
                name: "fk_inventory_items_currency_types_import_cost_currency_id",
                table: "inventory_items");

            migrationBuilder.DropForeignKey(
                name: "fk_inventory_items_inventory_categories_inventory_category_id",
                table: "inventory_items");

            migrationBuilder.DropForeignKey(
                name: "fk_inventory_items_currency_types_shipping_cost_currency_id",
                table: "inventory_items");

            migrationBuilder.DropForeignKey(
                name: "fk_inventory_items_units_unit_id",
                table: "inventory_items");

            migrationBuilder.DropPrimaryKey(
                name: "pk_units",
                table: "units");

            migrationBuilder.DropIndex(
                name: "ix_inventory_items_inventory_category_id",
                table: "inventory_items");

            migrationBuilder.DropPrimaryKey(
                name: "pk_inventory_categories",
                table: "inventory_categories");

            migrationBuilder.DropPrimaryKey(
                name: "pk_fabric_tests",
                table: "fabric_tests");

            migrationBuilder.DropPrimaryKey(
                name: "pk_currency_types",
                table: "currency_types");

            migrationBuilder.DeleteData(
                table: "currency_types",
                keyColumn: "currency_type_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "inventory_categories",
                keyColumn: "inventory_category_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "inventory_categories",
                keyColumn: "inventory_category_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "inventory_categories",
                keyColumn: "inventory_category_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "inventory_categories",
                keyColumn: "inventory_category_id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "inventory_categories",
                keyColumn: "inventory_category_id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "inventory_categories",
                keyColumn: "inventory_category_id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "inventory_categories",
                keyColumn: "inventory_category_id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "inventory_categories",
                keyColumn: "inventory_category_id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "inventory_categories",
                keyColumn: "inventory_category_id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "units",
                keyColumn: "unit_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "units",
                keyColumn: "unit_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "units",
                keyColumn: "unit_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "units",
                keyColumn: "unit_id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "unit_id",
                table: "units");

            migrationBuilder.DropColumn(
                name: "inventory_category_id",
                table: "inventory_items");

            migrationBuilder.DropColumn(
                name: "inventory_category_id",
                table: "inventory_categories");

            migrationBuilder.DropColumn(
                name: "fabric_test_id",
                table: "fabric_tests");

            migrationBuilder.DropColumn(
                name: "currency_type_id",
                table: "currency_types");

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "units",
                type: "integer",
                nullable: false,
                defaultValueSql: "nextval('units_id_seq'::regclass)");

            migrationBuilder.AddColumn<int>(
                name: "category_id",
                table: "inventory_items",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "inventory_categories",
                type: "integer",
                nullable: false,
                defaultValueSql: "nextval('inventory_categories_id_seq'::regclass)");

            migrationBuilder.AddColumn<string>(
                name: "id",
                table: "fabric_tests",
                type: "text",
                nullable: false,
                defaultValueSql: "nextval('fabric_tests_id_seq'::regclass)");

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "currency_types",
                type: "integer",
                nullable: false,
                defaultValueSql: "nextval('currency_types_id_seq'::regclass)");

            migrationBuilder.AddPrimaryKey(
                name: "pk_units",
                table: "units",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_inventory_categories",
                table: "inventory_categories",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_fabric_tests",
                table: "fabric_tests",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_currency_types",
                table: "currency_types",
                column: "id");

            migrationBuilder.InsertData(
                table: "currency_types",
                columns: new[] { "id", "abbreviation", "active", "default_selection", "name", "sort_order", "symbol" },
                values: new object[] { 1, "NRP", true, true, "Nepali Rupees", 1, "रु" });

            migrationBuilder.InsertData(
                table: "inventory_categories",
                columns: new[] { "id", "active", "description", "name", "sort_order" },
                values: new object[,]
                {
                    { 1, true, "", "Elastic", 1 },
                    { 2, true, "", "Fastener", 2 },
                    { 3, true, "", "Knit", 3 },
                    { 4, true, "", "Labels and Tags", 4 },
                    { 5, true, "", "Leather", 5 },
                    { 6, true, "", "Other", 6 },
                    { 7, true, "", "Thread", 7 },
                    { 8, true, "", "Woven", 8 },
                    { 9, true, "", "Zipper", 9 }
                });

            migrationBuilder.InsertData(
                table: "units",
                columns: new[] { "id", "abbreviation", "active", "name", "sort_order" },
                values: new object[,]
                {
                    { 1, "kg", true, "kilogram", 1 },
                    { 2, "m", true, "meter", 2 },
                    { 3, "piece", true, "piece", 3 },
                    { 4, "set", true, "set", 4 }
                });

            migrationBuilder.CreateIndex(
                name: "ix_inventory_items_category_id",
                table: "inventory_items",
                column: "category_id");

            migrationBuilder.AddForeignKey(
                name: "fk_inventory_items_inventory_categories_category_id",
                table: "inventory_items",
                column: "category_id",
                principalTable: "inventory_categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_inventory_items_currency_types_fob_cost_currency_id",
                table: "inventory_items",
                column: "fob_cost_currency_id",
                principalTable: "currency_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_inventory_items_currency_types_import_cost_currency_id",
                table: "inventory_items",
                column: "import_cost_currency_id",
                principalTable: "currency_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_inventory_items_currency_types_shipping_cost_currency_id",
                table: "inventory_items",
                column: "shipping_cost_currency_id",
                principalTable: "currency_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_inventory_items_units_unit_id",
                table: "inventory_items",
                column: "unit_id",
                principalTable: "units",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
