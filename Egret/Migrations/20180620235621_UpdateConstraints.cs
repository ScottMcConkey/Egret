using Microsoft.EntityFrameworkCore.Migrations;

namespace Egret.Migrations
{
    public partial class UpdateConstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_inventoryitems_consumptionevents",
                table: "consumption_events");

            migrationBuilder.DropForeignKey(
                name: "FK_consumption_events_units_unit",
                table: "consumption_events");

            migrationBuilder.DropPrimaryKey(
                name: "pk_inventoryitems",
                table: "inventory_items");

            migrationBuilder.AddPrimaryKey(
                name: "pk_inventoryitems_id",
                table: "inventory_items",
                column: "code");

            migrationBuilder.AddForeignKey(
                name: "fk_consumptionevents_inventory_code",
                table: "consumption_events",
                column: "inventory_item_code",
                principalTable: "inventory_items",
                principalColumn: "code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_consumptionevents_units",
                table: "consumption_events",
                column: "unit",
                principalTable: "units",
                principalColumn: "abbreviation",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_consumptionevents_inventory_code",
                table: "consumption_events");

            migrationBuilder.DropForeignKey(
                name: "fk_consumptionevents_units",
                table: "consumption_events");

            migrationBuilder.DropPrimaryKey(
                name: "pk_inventoryitems_id",
                table: "inventory_items");

            migrationBuilder.AddPrimaryKey(
                name: "pk_inventoryitems",
                table: "inventory_items",
                column: "code");

            migrationBuilder.AddForeignKey(
                name: "fk_inventoryitems_consumptionevents",
                table: "consumption_events",
                column: "inventory_item_code",
                principalTable: "inventory_items",
                principalColumn: "code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_consumption_events_units_unit",
                table: "consumption_events",
                column: "unit",
                principalTable: "units",
                principalColumn: "abbreviation",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
