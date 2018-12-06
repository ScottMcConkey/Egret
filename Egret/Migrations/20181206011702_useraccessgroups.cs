using Microsoft.EntityFrameworkCore.Migrations;

namespace Egret.Migrations
{
    public partial class useraccessgroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "accessgrouproles",
                keyColumns: new[] { "accessgroupid", "roleid" },
                keyValues: new object[] { 1, "06444a29-e816-40a2-962e-7d9049199c33" });

            migrationBuilder.DeleteData(
                table: "accessgrouproles",
                keyColumns: new[] { "accessgroupid", "roleid" },
                keyValues: new object[] { 1, "10a5f79e-7223-4a53-9647-595ca17f1c54" });

            migrationBuilder.DeleteData(
                table: "accessgrouproles",
                keyColumns: new[] { "accessgroupid", "roleid" },
                keyValues: new object[] { 1, "2c0fe1e8-df87-4394-bf6f-48eb63d10a5c" });

            migrationBuilder.DeleteData(
                table: "accessgrouproles",
                keyColumns: new[] { "accessgroupid", "roleid" },
                keyValues: new object[] { 1, "36b8707e-8b64-4f1e-b3e1-88c3f04e3be8" });

            migrationBuilder.DeleteData(
                table: "accessgrouproles",
                keyColumns: new[] { "accessgroupid", "roleid" },
                keyValues: new object[] { 1, "497a9878-9472-4286-9f50-21872e517e56" });

            migrationBuilder.DeleteData(
                table: "accessgrouproles",
                keyColumns: new[] { "accessgroupid", "roleid" },
                keyValues: new object[] { 1, "8040731e-4f61-4b07-a59c-f4167baf4093" });

            migrationBuilder.DeleteData(
                table: "accessgrouproles",
                keyColumns: new[] { "accessgroupid", "roleid" },
                keyValues: new object[] { 1, "b2196ec6-2648-4eb2-9d92-005043046bf0" });

            migrationBuilder.DeleteData(
                table: "accessgrouproles",
                keyColumns: new[] { "accessgroupid", "roleid" },
                keyValues: new object[] { 1, "d6efd9ba-cfc8-4ce4-9261-858d60f0886f" });

            migrationBuilder.DeleteData(
                table: "aspnet_users",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "7b4684d7-38bf-4d80-978f-ace6063dffeb", "2cebd9d0-694d-4ed3-8dc2-384f41557310" });

            migrationBuilder.DeleteData(
                table: "aspnet_roles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "06444a29-e816-40a2-962e-7d9049199c33", null });

            migrationBuilder.DeleteData(
                table: "aspnet_roles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "10a5f79e-7223-4a53-9647-595ca17f1c54", null });

            migrationBuilder.DeleteData(
                table: "aspnet_roles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "2c0fe1e8-df87-4394-bf6f-48eb63d10a5c", null });

            migrationBuilder.DeleteData(
                table: "aspnet_roles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "36b8707e-8b64-4f1e-b3e1-88c3f04e3be8", null });

            migrationBuilder.DeleteData(
                table: "aspnet_roles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "497a9878-9472-4286-9f50-21872e517e56", null });

            migrationBuilder.DeleteData(
                table: "aspnet_roles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "8040731e-4f61-4b07-a59c-f4167baf4093", null });

            migrationBuilder.DeleteData(
                table: "aspnet_roles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "b2196ec6-2648-4eb2-9d92-005043046bf0", null });

            migrationBuilder.DeleteData(
                table: "aspnet_roles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "d6efd9ba-cfc8-4ce4-9261-858d60f0886f", null });

            migrationBuilder.CreateTable(
                name: "useraccessgroups",
                columns: table => new
                {
                    userid = table.Column<string>(nullable: false),
                    accessgroupid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_useraccessgroups", x => new { x.accessgroupid, x.userid });
                    table.ForeignKey(
                        name: "FK_useraccessgroups_accessgroups_accessgroupid",
                        column: x => x.accessgroupid,
                        principalTable: "accessgroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_useraccessgroups_aspnet_users_userid",
                        column: x => x.userid,
                        principalTable: "aspnet_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "aspnet_roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "DisplayName", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "75fe53f2-e444-4549-8f81-d1869ef23548", null, null, "Item Create", "Item_Create", null },
                    { "cdbfc83d-7108-4eda-b357-69c866a188a6", null, null, "Item Read", "Item_Read", null },
                    { "4282ae87-2a4b-4387-91e3-8b4064e966ec", null, null, "Item Update", "Item_Update", null },
                    { "0aacdeee-cbfd-49ae-bda5-5969b8297007", null, null, "Item Delete", "Item_Delete", null },
                    { "55011f6c-7b11-4b51-bbc2-96bf2557f1cd", null, null, "Consumption Event Create", "ConsumptionEvent_Create", null },
                    { "71b94b5c-e6c1-4ad1-921a-fc330a176f81", null, null, "Consumption Event Read", "ConsumptionEvent_Read", null },
                    { "c42bb537-0c20-4755-a84c-32cc01ec838e", null, null, "Consumption Event Update", "ConsumptionEvent_Update", null },
                    { "1a0e8d7c-d112-484d-8ae9-fa7ca3ee5ed9", null, null, "Consumption Event Delete", "ConsumptionEvent_Delete", null }
                });

            migrationBuilder.InsertData(
                table: "aspnet_users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsActive", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "281a9937-e5be-41aa-bd03-de463229fdf2", 0, "2cebd9d0-694d-4ed3-8dc2-384f41557310", "bob@example.com", false, true, false, null, "BOB@EXAMPLE.COM", "BOB", "AQAAAAEAACcQAAAAEI4jEmRsUYzL6KnpR2/OjIPvkI9BWNmnnCZYah1GFvB2EOCWkgkk49YqCJBz38N8rg==", null, false, "3YILVFJYDKC4OK7QLLR4TO4KT6V4ZK5E", false, "Bob" });

            migrationBuilder.InsertData(
                table: "accessgrouproles",
                columns: new[] { "accessgroupid", "roleid" },
                values: new object[,]
                {
                    { 1, "75fe53f2-e444-4549-8f81-d1869ef23548" },
                    { 1, "cdbfc83d-7108-4eda-b357-69c866a188a6" },
                    { 1, "4282ae87-2a4b-4387-91e3-8b4064e966ec" },
                    { 1, "0aacdeee-cbfd-49ae-bda5-5969b8297007" },
                    { 1, "55011f6c-7b11-4b51-bbc2-96bf2557f1cd" },
                    { 1, "71b94b5c-e6c1-4ad1-921a-fc330a176f81" },
                    { 1, "c42bb537-0c20-4755-a84c-32cc01ec838e" },
                    { 1, "1a0e8d7c-d112-484d-8ae9-fa7ca3ee5ed9" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_useraccessgroups_accessgroupid",
                table: "useraccessgroups",
                column: "accessgroupid");

            migrationBuilder.CreateIndex(
                name: "ix_useraccessgroups_userid",
                table: "useraccessgroups",
                column: "userid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "useraccessgroups");

            migrationBuilder.DeleteData(
                table: "accessgrouproles",
                keyColumns: new[] { "accessgroupid", "roleid" },
                keyValues: new object[] { 1, "0aacdeee-cbfd-49ae-bda5-5969b8297007" });

            migrationBuilder.DeleteData(
                table: "accessgrouproles",
                keyColumns: new[] { "accessgroupid", "roleid" },
                keyValues: new object[] { 1, "1a0e8d7c-d112-484d-8ae9-fa7ca3ee5ed9" });

            migrationBuilder.DeleteData(
                table: "accessgrouproles",
                keyColumns: new[] { "accessgroupid", "roleid" },
                keyValues: new object[] { 1, "4282ae87-2a4b-4387-91e3-8b4064e966ec" });

            migrationBuilder.DeleteData(
                table: "accessgrouproles",
                keyColumns: new[] { "accessgroupid", "roleid" },
                keyValues: new object[] { 1, "55011f6c-7b11-4b51-bbc2-96bf2557f1cd" });

            migrationBuilder.DeleteData(
                table: "accessgrouproles",
                keyColumns: new[] { "accessgroupid", "roleid" },
                keyValues: new object[] { 1, "71b94b5c-e6c1-4ad1-921a-fc330a176f81" });

            migrationBuilder.DeleteData(
                table: "accessgrouproles",
                keyColumns: new[] { "accessgroupid", "roleid" },
                keyValues: new object[] { 1, "75fe53f2-e444-4549-8f81-d1869ef23548" });

            migrationBuilder.DeleteData(
                table: "accessgrouproles",
                keyColumns: new[] { "accessgroupid", "roleid" },
                keyValues: new object[] { 1, "c42bb537-0c20-4755-a84c-32cc01ec838e" });

            migrationBuilder.DeleteData(
                table: "accessgrouproles",
                keyColumns: new[] { "accessgroupid", "roleid" },
                keyValues: new object[] { 1, "cdbfc83d-7108-4eda-b357-69c866a188a6" });

            migrationBuilder.DeleteData(
                table: "aspnet_users",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "281a9937-e5be-41aa-bd03-de463229fdf2", "2cebd9d0-694d-4ed3-8dc2-384f41557310" });

            migrationBuilder.DeleteData(
                table: "aspnet_roles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "0aacdeee-cbfd-49ae-bda5-5969b8297007", null });

            migrationBuilder.DeleteData(
                table: "aspnet_roles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "1a0e8d7c-d112-484d-8ae9-fa7ca3ee5ed9", null });

            migrationBuilder.DeleteData(
                table: "aspnet_roles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "4282ae87-2a4b-4387-91e3-8b4064e966ec", null });

            migrationBuilder.DeleteData(
                table: "aspnet_roles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "55011f6c-7b11-4b51-bbc2-96bf2557f1cd", null });

            migrationBuilder.DeleteData(
                table: "aspnet_roles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "71b94b5c-e6c1-4ad1-921a-fc330a176f81", null });

            migrationBuilder.DeleteData(
                table: "aspnet_roles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "75fe53f2-e444-4549-8f81-d1869ef23548", null });

            migrationBuilder.DeleteData(
                table: "aspnet_roles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "c42bb537-0c20-4755-a84c-32cc01ec838e", null });

            migrationBuilder.DeleteData(
                table: "aspnet_roles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "cdbfc83d-7108-4eda-b357-69c866a188a6", null });

            migrationBuilder.InsertData(
                table: "aspnet_roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "DisplayName", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d6efd9ba-cfc8-4ce4-9261-858d60f0886f", null, null, "Item Create", "Item_Create", null },
                    { "2c0fe1e8-df87-4394-bf6f-48eb63d10a5c", null, null, "Item Read", "Item_Read", null },
                    { "b2196ec6-2648-4eb2-9d92-005043046bf0", null, null, "Item Update", "Item_Update", null },
                    { "36b8707e-8b64-4f1e-b3e1-88c3f04e3be8", null, null, "Item Delete", "Item_Delete", null },
                    { "497a9878-9472-4286-9f50-21872e517e56", null, null, "Consumption Event Create", "ConsumptionEvent_Create", null },
                    { "8040731e-4f61-4b07-a59c-f4167baf4093", null, null, "Consumption Event Read", "ConsumptionEvent_Read", null },
                    { "10a5f79e-7223-4a53-9647-595ca17f1c54", null, null, "Consumption Event Update", "ConsumptionEvent_Update", null },
                    { "06444a29-e816-40a2-962e-7d9049199c33", null, null, "Consumption Event Delete", "ConsumptionEvent_Delete", null }
                });

            migrationBuilder.InsertData(
                table: "aspnet_users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsActive", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "7b4684d7-38bf-4d80-978f-ace6063dffeb", 0, "2cebd9d0-694d-4ed3-8dc2-384f41557310", "bob@example.com", false, true, false, null, "BOB@EXAMPLE.COM", "BOB", "AQAAAAEAACcQAAAAEI4jEmRsUYzL6KnpR2/OjIPvkI9BWNmnnCZYah1GFvB2EOCWkgkk49YqCJBz38N8rg==", null, false, "3YILVFJYDKC4OK7QLLR4TO4KT6V4ZK5E", false, "Bob" });

            migrationBuilder.InsertData(
                table: "accessgrouproles",
                columns: new[] { "accessgroupid", "roleid" },
                values: new object[,]
                {
                    { 1, "d6efd9ba-cfc8-4ce4-9261-858d60f0886f" },
                    { 1, "2c0fe1e8-df87-4394-bf6f-48eb63d10a5c" },
                    { 1, "b2196ec6-2648-4eb2-9d92-005043046bf0" },
                    { 1, "36b8707e-8b64-4f1e-b3e1-88c3f04e3be8" },
                    { 1, "497a9878-9472-4286-9f50-21872e517e56" },
                    { 1, "8040731e-4f61-4b07-a59c-f4167baf4093" },
                    { 1, "10a5f79e-7223-4a53-9647-595ca17f1c54" },
                    { 1, "06444a29-e816-40a2-962e-7d9049199c33" }
                });
        }
    }
}
