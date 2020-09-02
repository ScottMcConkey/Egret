using Microsoft.EntityFrameworkCore.Migrations;

namespace Egret.Migrations
{
    public partial class v090 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "access_groups",
                newName: "access_group_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "access_group_id",
                table: "access_groups",
                newName: "id");
        }
    }
}
