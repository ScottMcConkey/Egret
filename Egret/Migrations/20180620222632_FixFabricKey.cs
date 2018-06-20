using Microsoft.EntityFrameworkCore.Migrations;

namespace Egret.Migrations
{
    public partial class FixFabricKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_fabric_tests",
                table: "fabric_tests");

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "fabric_tests",
                nullable: false,
                defaultValueSql: "nextval('master_seq'::regclass)",
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "pk_fabrictests_id",
                table: "fabric_tests",
                column: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_fabrictests_id",
                table: "fabric_tests");

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "fabric_tests",
                nullable: false,
                oldClrType: typeof(string),
                oldDefaultValueSql: "nextval('master_seq'::regclass)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_fabric_tests",
                table: "fabric_tests",
                column: "id");
        }
    }
}
