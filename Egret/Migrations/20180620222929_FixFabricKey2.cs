using Microsoft.EntityFrameworkCore.Migrations;

namespace Egret.Migrations
{
    public partial class FixFabricKey2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InventoryItemCode",
                table: "fabric_tests",
                newName: "inventory_item_code");

            migrationBuilder.RenameIndex(
                name: "IX_fabric_tests_InventoryItemCode",
                table: "fabric_tests",
                newName: "IX_fabric_tests_inventory_item_code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "inventory_item_code",
                table: "fabric_tests",
                newName: "InventoryItemCode");

            migrationBuilder.RenameIndex(
                name: "IX_fabric_tests_inventory_item_code",
                table: "fabric_tests",
                newName: "IX_fabric_tests_InventoryItemCode");
        }
    }
}
