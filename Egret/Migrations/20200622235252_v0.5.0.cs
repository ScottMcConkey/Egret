using Microsoft.EntityFrameworkCore.Migrations;

namespace Egret.Migrations
{
    public partial class v050 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_inventory_items_currency_types_fob_cost_currency_id",
                table: "inventory_items");

            migrationBuilder.DropForeignKey(
                name: "fk_inventory_items_currency_types_import_cost_currency_id",
                table: "inventory_items");

            migrationBuilder.DropForeignKey(
                name: "fk_inventory_items_currency_types_shipping_cost_currency_id",
                table: "inventory_items");

            migrationBuilder.DropIndex(
                name: "ix_inventory_items_fob_cost_currency_id",
                table: "inventory_items");

            migrationBuilder.DropIndex(
                name: "ix_inventory_items_import_cost_currency_id",
                table: "inventory_items");

            migrationBuilder.DropIndex(
                name: "ix_inventory_items_shipping_cost_currency_id",
                table: "inventory_items");

            migrationBuilder.DropColumn(
                name: "fob_cost_currency_id",
                table: "inventory_items");

            migrationBuilder.DropColumn(
                name: "import_cost_currency_id",
                table: "inventory_items");

            migrationBuilder.DropColumn(
                name: "shipping_cost_currency_id",
                table: "inventory_items");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "fob_cost_currency_id",
                table: "inventory_items",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "import_cost_currency_id",
                table: "inventory_items",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "shipping_cost_currency_id",
                table: "inventory_items",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_inventory_items_fob_cost_currency_id",
                table: "inventory_items",
                column: "fob_cost_currency_id");

            migrationBuilder.CreateIndex(
                name: "ix_inventory_items_import_cost_currency_id",
                table: "inventory_items",
                column: "import_cost_currency_id");

            migrationBuilder.CreateIndex(
                name: "ix_inventory_items_shipping_cost_currency_id",
                table: "inventory_items",
                column: "shipping_cost_currency_id");

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
                name: "fk_inventory_items_currency_types_shipping_cost_currency_id",
                table: "inventory_items",
                column: "shipping_cost_currency_id",
                principalTable: "currency_types",
                principalColumn: "currency_type_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
