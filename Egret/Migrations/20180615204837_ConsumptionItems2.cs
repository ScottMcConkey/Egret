using Microsoft.EntityFrameworkCore.Migrations;

namespace Egret.Migrations
{
    public partial class ConsumptionItems2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Unit",
                table: "consumption_events",
                newName: "unit_id");

            migrationBuilder.RenameColumn(
                name: "SampleOrderNumber",
                table: "consumption_events",
                newName: "sample_order_number");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "unit_id",
                table: "consumption_events",
                newName: "Unit");

            migrationBuilder.RenameColumn(
                name: "sample_order_number",
                table: "consumption_events",
                newName: "SampleOrderNumber");
        }
    }
}
