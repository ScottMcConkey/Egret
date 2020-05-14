using Microsoft.EntityFrameworkCore.Migrations;

namespace Egret.Migrations
{
    public partial class v010 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "storage_location_id_seq");

            migrationBuilder.AddColumn<int>(
                name: "storage_location_id",
                table: "inventory_items",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "storage_locations",
                columns: table => new
                {
                    storage_location_id = table.Column<int>(nullable: false, defaultValueSql: "nextval('storage_location_id_seq'::regclass)"),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_storage_locations", x => x.storage_location_id);
                });

            migrationBuilder.CreateTable(
                name: "test_results",
                columns: table => new
                {
                    name = table.Column<string>(nullable: true),
                    stock_value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateIndex(
                name: "ix_inventory_items_storage_location_id",
                table: "inventory_items",
                column: "storage_location_id");

            migrationBuilder.AddForeignKey(
                name: "fk_inventory_items_storage_locations_storage_location_id",
                table: "inventory_items",
                column: "storage_location_id",
                principalTable: "storage_locations",
                principalColumn: "storage_location_id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_inventory_items_storage_locations_storage_location_id",
                table: "inventory_items");

            migrationBuilder.DropTable(
                name: "storage_locations");

            migrationBuilder.DropTable(
                name: "test_results");

            migrationBuilder.DropIndex(
                name: "ix_inventory_items_storage_location_id",
                table: "inventory_items");

            migrationBuilder.DropSequence(
                name: "storage_location_id_seq");

            migrationBuilder.DropColumn(
                name: "storage_location_id",
                table: "inventory_items");
        }
    }
}
