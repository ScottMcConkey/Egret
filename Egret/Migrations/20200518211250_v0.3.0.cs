using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Egret.Migrations
{
    public partial class v030 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_consumption_events_inventory_items_inventory_item_code",
                table: "consumption_events");

            migrationBuilder.DropForeignKey(
                name: "fk_fabric_tests_inventory_items_inventory_item_code",
                table: "fabric_tests");

            migrationBuilder.DropPrimaryKey(
                name: "pk_inventory_items",
                table: "inventory_items");

            migrationBuilder.DropIndex(
                name: "ix_fabric_tests_inventory_item_code",
                table: "fabric_tests");

            migrationBuilder.DropPrimaryKey(
                name: "pk_consumption_events",
                table: "consumption_events");

            migrationBuilder.DropIndex(
                name: "ix_consumption_events_inventory_item_code",
                table: "consumption_events");

            migrationBuilder.DropColumn(
                name: "code",
                table: "inventory_items");

            migrationBuilder.DropColumn(
                name: "inventory_item_code",
                table: "fabric_tests");

            migrationBuilder.DropColumn(
                name: "id",
                table: "consumption_events");

            migrationBuilder.DropColumn(
                name: "date_of_consumption",
                table: "consumption_events");

            migrationBuilder.DropColumn(
                name: "inventory_item_code",
                table: "consumption_events");

            migrationBuilder.AddColumn<bool>(
                name: "active",
                table: "storage_locations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "sort_order",
                table: "storage_locations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "inventory_item_id",
                table: "inventory_items",
                nullable: false,
                defaultValueSql: "'I' || nextval('items_id_seq'::regclass)");

            migrationBuilder.AddColumn<string>(
                name: "inventory_item_id",
                table: "fabric_tests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "consumption_event_id",
                table: "consumption_events",
                nullable: false,
                defaultValueSql: "'CE' || nextval('consumption_events_id_seq'::regclass)");

            migrationBuilder.AddColumn<DateTime>(
                name: "date_consumed",
                table: "consumption_events",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "inventory_item_id",
                table: "consumption_events",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "pk_inventory_items",
                table: "inventory_items",
                column: "inventory_item_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_consumption_events",
                table: "consumption_events",
                column: "consumption_event_id");

            migrationBuilder.CreateIndex(
                name: "ix_fabric_tests_inventory_item_id",
                table: "fabric_tests",
                column: "inventory_item_id");

            migrationBuilder.CreateIndex(
                name: "ix_consumption_events_inventory_item_id",
                table: "consumption_events",
                column: "inventory_item_id");

            migrationBuilder.AddForeignKey(
                name: "fk_consumption_events_inventory_items_inventory_item_id",
                table: "consumption_events",
                column: "inventory_item_id",
                principalTable: "inventory_items",
                principalColumn: "inventory_item_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_fabric_tests_inventory_items_inventory_item_id",
                table: "fabric_tests",
                column: "inventory_item_id",
                principalTable: "inventory_items",
                principalColumn: "inventory_item_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_consumption_events_inventory_items_inventory_item_id",
                table: "consumption_events");

            migrationBuilder.DropForeignKey(
                name: "fk_fabric_tests_inventory_items_inventory_item_id",
                table: "fabric_tests");

            migrationBuilder.DropPrimaryKey(
                name: "pk_inventory_items",
                table: "inventory_items");

            migrationBuilder.DropIndex(
                name: "ix_fabric_tests_inventory_item_id",
                table: "fabric_tests");

            migrationBuilder.DropPrimaryKey(
                name: "pk_consumption_events",
                table: "consumption_events");

            migrationBuilder.DropIndex(
                name: "ix_consumption_events_inventory_item_id",
                table: "consumption_events");

            migrationBuilder.DropColumn(
                name: "active",
                table: "storage_locations");

            migrationBuilder.DropColumn(
                name: "sort_order",
                table: "storage_locations");

            migrationBuilder.DropColumn(
                name: "inventory_item_id",
                table: "inventory_items");

            migrationBuilder.DropColumn(
                name: "inventory_item_id",
                table: "fabric_tests");

            migrationBuilder.DropColumn(
                name: "consumption_event_id",
                table: "consumption_events");

            migrationBuilder.DropColumn(
                name: "date_consumed",
                table: "consumption_events");

            migrationBuilder.DropColumn(
                name: "inventory_item_id",
                table: "consumption_events");

            migrationBuilder.AddColumn<string>(
                name: "code",
                table: "inventory_items",
                type: "text",
                nullable: false,
                defaultValueSql: "'I' || nextval('items_id_seq'::regclass)");

            migrationBuilder.AddColumn<string>(
                name: "inventory_item_code",
                table: "fabric_tests",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "id",
                table: "consumption_events",
                type: "text",
                nullable: false,
                defaultValueSql: "'CE' || nextval('consumption_events_id_seq'::regclass)");

            migrationBuilder.AddColumn<DateTime>(
                name: "date_of_consumption",
                table: "consumption_events",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "inventory_item_code",
                table: "consumption_events",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "pk_inventory_items",
                table: "inventory_items",
                column: "code");

            migrationBuilder.AddPrimaryKey(
                name: "pk_consumption_events",
                table: "consumption_events",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_fabric_tests_inventory_item_code",
                table: "fabric_tests",
                column: "inventory_item_code");

            migrationBuilder.CreateIndex(
                name: "ix_consumption_events_inventory_item_code",
                table: "consumption_events",
                column: "inventory_item_code");

            migrationBuilder.AddForeignKey(
                name: "fk_consumption_events_inventory_items_inventory_item_code",
                table: "consumption_events",
                column: "inventory_item_code",
                principalTable: "inventory_items",
                principalColumn: "code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_fabric_tests_inventory_items_inventory_item_code",
                table: "fabric_tests",
                column: "inventory_item_code",
                principalTable: "inventory_items",
                principalColumn: "code",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
