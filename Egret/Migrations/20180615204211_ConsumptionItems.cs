using Microsoft.EntityFrameworkCore.Migrations;

namespace Egret.Migrations
{
    public partial class ConsumptionItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_consumption_events_inventory_items_InventoryItemCode",
                table: "consumption_events");

            migrationBuilder.RenameColumn(
                name: "InventoryItemCode",
                table: "consumption_events",
                newName: "InventoryItemNavigationCode");

            migrationBuilder.RenameIndex(
                name: "IX_consumption_events_InventoryItemCode",
                table: "consumption_events",
                newName: "IX_consumption_events_InventoryItemNavigationCode");

            migrationBuilder.AddColumn<string>(
                name: "InventoryItem",
                table: "consumption_events",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_consumption_events_inventory_items_InventoryItemNavigationC~",
                table: "consumption_events",
                column: "InventoryItemNavigationCode",
                principalTable: "inventory_items",
                principalColumn: "code",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_consumption_events_inventory_items_InventoryItemNavigationC~",
                table: "consumption_events");

            migrationBuilder.DropColumn(
                name: "InventoryItem",
                table: "consumption_events");

            migrationBuilder.RenameColumn(
                name: "InventoryItemNavigationCode",
                table: "consumption_events",
                newName: "InventoryItemCode");

            migrationBuilder.RenameIndex(
                name: "IX_consumption_events_InventoryItemNavigationCode",
                table: "consumption_events",
                newName: "IX_consumption_events_InventoryItemCode");

            migrationBuilder.AddForeignKey(
                name: "FK_consumption_events_inventory_items_InventoryItemCode",
                table: "consumption_events",
                column: "InventoryItemCode",
                principalTable: "inventory_items",
                principalColumn: "code",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
