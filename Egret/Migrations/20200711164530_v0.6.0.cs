using Microsoft.EntityFrameworkCore.Migrations;

namespace Egret.Migrations
{
    public partial class v060 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_currency_types_sort_order",
                table: "currency_types");

            migrationBuilder.DropColumn(
                name: "active",
                table: "currency_types");

            migrationBuilder.DropColumn(
                name: "default_selection",
                table: "currency_types");

            migrationBuilder.DropColumn(
                name: "sort_order",
                table: "currency_types");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "active",
                table: "currency_types",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "default_selection",
                table: "currency_types",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "sort_order",
                table: "currency_types",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "currency_types",
                keyColumn: "currency_type_id",
                keyValue: 1,
                columns: new[] { "active", "default_selection", "sort_order" },
                values: new object[] { true, true, 1 });

            migrationBuilder.CreateIndex(
                name: "ix_currency_types_sort_order",
                table: "currency_types",
                column: "sort_order",
                unique: true);
        }
    }
}
