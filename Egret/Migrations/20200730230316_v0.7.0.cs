using Microsoft.EntityFrameworkCore.Migrations;

namespace Egret.Migrations
{
    public partial class v070 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "stock_value_report_results");

            migrationBuilder.DropColumn(
                name: "import_costs",
                table: "inventory_items");

            migrationBuilder.AddColumn<decimal>(
                name: "import_cost",
                table: "inventory_items",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "vat_cost",
                table: "inventory_items",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "import_cost",
                table: "inventory_items");

            migrationBuilder.DropColumn(
                name: "vat_cost",
                table: "inventory_items");

            migrationBuilder.AddColumn<decimal>(
                name: "import_costs",
                table: "inventory_items",
                type: "numeric",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "stock_value_report_results",
                columns: table => new
                {
                    name = table.Column<string>(type: "text", nullable: true),
                    stock_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                });
        }
    }
}
