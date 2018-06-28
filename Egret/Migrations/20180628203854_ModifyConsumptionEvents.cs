using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Egret.Migrations
{
    public partial class ModifyConsumptionEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_inventoryitems_buyunit",
                table: "inventory_items");

            migrationBuilder.DropIndex(
                name: "IX_inventory_items_buy_unit",
                table: "inventory_items");

            migrationBuilder.RenameColumn(
                name: "buy_unit",
                table: "inventory_items",
                newName: "BuyUnit");

            migrationBuilder.AddColumn<int>(
                name: "BuyUnitNavigationId",
                table: "inventory_items",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "user_added_by",
                table: "consumption_events",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "date_added",
                table: "consumption_events",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "date_updated",
                table: "consumption_events",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "user_updated_by",
                table: "consumption_events",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_inventory_items_BuyUnitNavigationId",
                table: "inventory_items",
                column: "BuyUnitNavigationId");

            migrationBuilder.AddForeignKey(
                name: "FK_inventory_items_units_BuyUnitNavigationId",
                table: "inventory_items",
                column: "BuyUnitNavigationId",
                principalTable: "units",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_inventory_items_units_BuyUnitNavigationId",
                table: "inventory_items");

            migrationBuilder.DropIndex(
                name: "IX_inventory_items_BuyUnitNavigationId",
                table: "inventory_items");

            migrationBuilder.DropColumn(
                name: "BuyUnitNavigationId",
                table: "inventory_items");

            migrationBuilder.DropColumn(
                name: "user_added_by",
                table: "consumption_events");

            migrationBuilder.DropColumn(
                name: "date_added",
                table: "consumption_events");

            migrationBuilder.DropColumn(
                name: "date_updated",
                table: "consumption_events");

            migrationBuilder.DropColumn(
                name: "user_updated_by",
                table: "consumption_events");

            migrationBuilder.RenameColumn(
                name: "BuyUnit",
                table: "inventory_items",
                newName: "buy_unit");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_items_buy_unit",
                table: "inventory_items",
                column: "buy_unit");

            migrationBuilder.AddForeignKey(
                name: "fk_inventoryitems_buyunit",
                table: "inventory_items",
                column: "buy_unit",
                principalTable: "units",
                principalColumn: "abbreviation",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
