using Microsoft.EntityFrameworkCore.Migrations;

namespace Egret.Migrations
{
    public partial class v040 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "test_results");

            migrationBuilder.CreateTable(
                name: "stock_value_report_results",
                columns: table => new
                {
                    name = table.Column<string>(nullable: true),
                    stock_value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateIndex(
                name: "ix_consumption_events_consumed_by",
                table: "consumption_events",
                column: "consumed_by");

            migrationBuilder.CreateIndex(
                name: "ix_consumption_events_date_added",
                table: "consumption_events",
                column: "date_added");

            migrationBuilder.CreateIndex(
                name: "ix_consumption_events_order_number",
                table: "consumption_events",
                column: "order_number");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "stock_value_report_results");

            migrationBuilder.DropIndex(
                name: "ix_consumption_events_consumed_by",
                table: "consumption_events");

            migrationBuilder.DropIndex(
                name: "ix_consumption_events_date_added",
                table: "consumption_events");

            migrationBuilder.DropIndex(
                name: "ix_consumption_events_order_number",
                table: "consumption_events");

            migrationBuilder.CreateTable(
                name: "test_results",
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
