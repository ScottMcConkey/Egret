using Microsoft.EntityFrameworkCore.Migrations;

namespace Egret.Migrations
{
    public partial class v080 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "qty_purchased",
                table: "inventory_items",
                newName: "quantity_purchased");

            migrationBuilder.RenameColumn(
                name: "qty_to_purchase_now",
                table: "inventory_items",
                newName: "quantity_to_purchase_now");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "quantity_purchased",
                table: "inventory_items",
                newName: "qty_purchased");

            migrationBuilder.RenameColumn(
                name: "quantity_to_purchase_now",
                table: "inventory_items",
                newName: "qty_to_purchase_now");
        }
    }
}
