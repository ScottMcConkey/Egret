using Microsoft.EntityFrameworkCore.Migrations;

namespace Egret.Migrations
{
    public partial class ModifyRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_consumption_events_units_UnitNavigationAbbreviation",
                table: "consumption_events");

            migrationBuilder.DropIndex(
                name: "IX_consumption_events_UnitNavigationAbbreviation",
                table: "consumption_events");

            migrationBuilder.DropColumn(
                name: "UnitNavigationAbbreviation",
                table: "consumption_events");

            migrationBuilder.RenameColumn(
                name: "InventoryItemCode",
                table: "consumption_events",
                newName: "inventory_item_code");

            migrationBuilder.RenameIndex(
                name: "IX_consumption_events_InventoryItemCode",
                table: "consumption_events",
                newName: "IX_consumption_events_inventory_item_code");

            migrationBuilder.CreateIndex(
                name: "IX_consumption_events_unit",
                table: "consumption_events",
                column: "unit");

            migrationBuilder.AddForeignKey(
                name: "FK_consumption_events_units_unit",
                table: "consumption_events",
                column: "unit",
                principalTable: "units",
                principalColumn: "abbreviation",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_consumption_events_units_unit",
                table: "consumption_events");

            migrationBuilder.DropIndex(
                name: "IX_consumption_events_unit",
                table: "consumption_events");

            migrationBuilder.RenameColumn(
                name: "inventory_item_code",
                table: "consumption_events",
                newName: "InventoryItemCode");

            migrationBuilder.RenameIndex(
                name: "IX_consumption_events_inventory_item_code",
                table: "consumption_events",
                newName: "IX_consumption_events_InventoryItemCode");

            migrationBuilder.AddColumn<string>(
                name: "UnitNavigationAbbreviation",
                table: "consumption_events",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_consumption_events_UnitNavigationAbbreviation",
                table: "consumption_events",
                column: "UnitNavigationAbbreviation");

            migrationBuilder.AddForeignKey(
                name: "FK_consumption_events_units_UnitNavigationAbbreviation",
                table: "consumption_events",
                column: "UnitNavigationAbbreviation",
                principalTable: "units",
                principalColumn: "abbreviation",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
