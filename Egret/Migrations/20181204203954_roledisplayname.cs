using Microsoft.EntityFrameworkCore.Migrations;

namespace Egret.Migrations
{
    public partial class roledisplayname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "accessgrouproles",
                keyColumns: new[] { "accessgroupid", "roleid" },
                keyValues: new object[] { 1, "0a73f6bd-316f-4f3f-8ca9-14606f1a277a" });

            migrationBuilder.DeleteData(
                table: "accessgrouproles",
                keyColumns: new[] { "accessgroupid", "roleid" },
                keyValues: new object[] { 1, "7c4f6a61-1d3e-4c8d-be57-aa07f79b04be" });

            migrationBuilder.DeleteData(
                table: "accessgrouproles",
                keyColumns: new[] { "accessgroupid", "roleid" },
                keyValues: new object[] { 1, "843060d1-9870-438f-877b-ccb8ecb34bdb" });

            migrationBuilder.DeleteData(
                table: "accessgrouproles",
                keyColumns: new[] { "accessgroupid", "roleid" },
                keyValues: new object[] { 1, "8ee765f3-f602-4b4b-bd3a-c4cdb9e78b61" });

            migrationBuilder.DeleteData(
                table: "accessgrouproles",
                keyColumns: new[] { "accessgroupid", "roleid" },
                keyValues: new object[] { 1, "a8c49863-52ad-4e4d-9bdd-149d828e7932" });

            migrationBuilder.DeleteData(
                table: "accessgrouproles",
                keyColumns: new[] { "accessgroupid", "roleid" },
                keyValues: new object[] { 1, "a9e2f381-984f-4a61-a23d-87e275f1754a" });

            migrationBuilder.DeleteData(
                table: "accessgrouproles",
                keyColumns: new[] { "accessgroupid", "roleid" },
                keyValues: new object[] { 1, "e92ad308-cd5d-4069-836c-fce42f832264" });

            migrationBuilder.DeleteData(
                table: "accessgrouproles",
                keyColumns: new[] { "accessgroupid", "roleid" },
                keyValues: new object[] { 1, "fec9a8ca-a1c6-4bc2-9d54-e6b3dea198d6" });

            migrationBuilder.DeleteData(
                table: "aspnet_users",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "fbae6926-e793-49e2-8b78-58f29255d2da", "2cebd9d0-694d-4ed3-8dc2-384f41557310" });

            migrationBuilder.DeleteData(
                table: "aspnet_roles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "0a73f6bd-316f-4f3f-8ca9-14606f1a277a", null });

            migrationBuilder.DeleteData(
                table: "aspnet_roles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "7c4f6a61-1d3e-4c8d-be57-aa07f79b04be", null });

            migrationBuilder.DeleteData(
                table: "aspnet_roles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "843060d1-9870-438f-877b-ccb8ecb34bdb", null });

            migrationBuilder.DeleteData(
                table: "aspnet_roles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "8ee765f3-f602-4b4b-bd3a-c4cdb9e78b61", null });

            migrationBuilder.DeleteData(
                table: "aspnet_roles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "a8c49863-52ad-4e4d-9bdd-149d828e7932", null });

            migrationBuilder.DeleteData(
                table: "aspnet_roles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "a9e2f381-984f-4a61-a23d-87e275f1754a", null });

            migrationBuilder.DeleteData(
                table: "aspnet_roles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "e92ad308-cd5d-4069-836c-fce42f832264", null });

            migrationBuilder.DeleteData(
                table: "aspnet_roles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "fec9a8ca-a1c6-4bc2-9d54-e6b3dea198d6", null });

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "aspnet_roles",
                nullable: true);

            migrationBuilder.InsertData(
                table: "aspnet_roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "DisplayName", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "db40cc7c-dcd5-46f2-823d-9eaa180a3a38", null, null, null, "Item_Create", null },
                    { "6d394fb1-f0f0-40d2-a03a-724010946e12", null, null, null, "Item_Read", null },
                    { "553aee38-65f6-41b1-93fb-14c8deb3b960", null, null, null, "Item_Update", null },
                    { "b468d0c3-0d01-4e3e-a5ef-6dc2147e2e2c", null, null, null, "Item_Delete", null },
                    { "91cf1565-2bb2-4cf9-9d77-da811fc0144f", null, null, null, "ConsumptionEvent_Create", null },
                    { "da394bd2-abb0-4d4d-b2d6-655f73ddb6fa", null, null, null, "ConsumptionEvent_Read", null },
                    { "c08e47c0-1cc1-47e5-b88d-5c9754d480be", null, null, null, "ConsumptionEvent_Update", null },
                    { "ef237762-5888-4de5-a35e-27a15fa66792", null, null, null, "ConsumptionEvent_Delete", null }
                });

            migrationBuilder.InsertData(
                table: "aspnet_users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsActive", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "450a31a5-9ae7-43ee-8160-b94c1f320637", 0, "2cebd9d0-694d-4ed3-8dc2-384f41557310", "bob@example.com", false, true, false, null, "BOB@EXAMPLE.COM", "BOB", "AQAAAAEAACcQAAAAEI4jEmRsUYzL6KnpR2/OjIPvkI9BWNmnnCZYah1GFvB2EOCWkgkk49YqCJBz38N8rg==", null, false, "3YILVFJYDKC4OK7QLLR4TO4KT6V4ZK5E", false, "Bob" });

            migrationBuilder.InsertData(
                table: "accessgrouproles",
                columns: new[] { "accessgroupid", "roleid" },
                values: new object[,]
                {
                    { 1, "db40cc7c-dcd5-46f2-823d-9eaa180a3a38" },
                    { 1, "6d394fb1-f0f0-40d2-a03a-724010946e12" },
                    { 1, "553aee38-65f6-41b1-93fb-14c8deb3b960" },
                    { 1, "b468d0c3-0d01-4e3e-a5ef-6dc2147e2e2c" },
                    { 1, "91cf1565-2bb2-4cf9-9d77-da811fc0144f" },
                    { 1, "da394bd2-abb0-4d4d-b2d6-655f73ddb6fa" },
                    { 1, "c08e47c0-1cc1-47e5-b88d-5c9754d480be" },
                    { 1, "ef237762-5888-4de5-a35e-27a15fa66792" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "accessgrouproles",
                keyColumns: new[] { "accessgroupid", "roleid" },
                keyValues: new object[] { 1, "553aee38-65f6-41b1-93fb-14c8deb3b960" });

            migrationBuilder.DeleteData(
                table: "accessgrouproles",
                keyColumns: new[] { "accessgroupid", "roleid" },
                keyValues: new object[] { 1, "6d394fb1-f0f0-40d2-a03a-724010946e12" });

            migrationBuilder.DeleteData(
                table: "accessgrouproles",
                keyColumns: new[] { "accessgroupid", "roleid" },
                keyValues: new object[] { 1, "91cf1565-2bb2-4cf9-9d77-da811fc0144f" });

            migrationBuilder.DeleteData(
                table: "accessgrouproles",
                keyColumns: new[] { "accessgroupid", "roleid" },
                keyValues: new object[] { 1, "b468d0c3-0d01-4e3e-a5ef-6dc2147e2e2c" });

            migrationBuilder.DeleteData(
                table: "accessgrouproles",
                keyColumns: new[] { "accessgroupid", "roleid" },
                keyValues: new object[] { 1, "c08e47c0-1cc1-47e5-b88d-5c9754d480be" });

            migrationBuilder.DeleteData(
                table: "accessgrouproles",
                keyColumns: new[] { "accessgroupid", "roleid" },
                keyValues: new object[] { 1, "da394bd2-abb0-4d4d-b2d6-655f73ddb6fa" });

            migrationBuilder.DeleteData(
                table: "accessgrouproles",
                keyColumns: new[] { "accessgroupid", "roleid" },
                keyValues: new object[] { 1, "db40cc7c-dcd5-46f2-823d-9eaa180a3a38" });

            migrationBuilder.DeleteData(
                table: "accessgrouproles",
                keyColumns: new[] { "accessgroupid", "roleid" },
                keyValues: new object[] { 1, "ef237762-5888-4de5-a35e-27a15fa66792" });

            migrationBuilder.DeleteData(
                table: "aspnet_users",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "450a31a5-9ae7-43ee-8160-b94c1f320637", "2cebd9d0-694d-4ed3-8dc2-384f41557310" });

            migrationBuilder.DeleteData(
                table: "aspnet_roles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "553aee38-65f6-41b1-93fb-14c8deb3b960", null });

            migrationBuilder.DeleteData(
                table: "aspnet_roles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "6d394fb1-f0f0-40d2-a03a-724010946e12", null });

            migrationBuilder.DeleteData(
                table: "aspnet_roles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "91cf1565-2bb2-4cf9-9d77-da811fc0144f", null });

            migrationBuilder.DeleteData(
                table: "aspnet_roles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "b468d0c3-0d01-4e3e-a5ef-6dc2147e2e2c", null });

            migrationBuilder.DeleteData(
                table: "aspnet_roles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "c08e47c0-1cc1-47e5-b88d-5c9754d480be", null });

            migrationBuilder.DeleteData(
                table: "aspnet_roles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "da394bd2-abb0-4d4d-b2d6-655f73ddb6fa", null });

            migrationBuilder.DeleteData(
                table: "aspnet_roles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "db40cc7c-dcd5-46f2-823d-9eaa180a3a38", null });

            migrationBuilder.DeleteData(
                table: "aspnet_roles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "ef237762-5888-4de5-a35e-27a15fa66792", null });

            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "aspnet_roles");

            migrationBuilder.InsertData(
                table: "aspnet_roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "843060d1-9870-438f-877b-ccb8ecb34bdb", null, null, "Item_Create", null },
                    { "7c4f6a61-1d3e-4c8d-be57-aa07f79b04be", null, null, "Item_Read", null },
                    { "8ee765f3-f602-4b4b-bd3a-c4cdb9e78b61", null, null, "Item_Update", null },
                    { "e92ad308-cd5d-4069-836c-fce42f832264", null, null, "Item_Delete", null },
                    { "fec9a8ca-a1c6-4bc2-9d54-e6b3dea198d6", null, null, "ConsumptionEvent_Create", null },
                    { "a8c49863-52ad-4e4d-9bdd-149d828e7932", null, null, "ConsumptionEvent_Read", null },
                    { "0a73f6bd-316f-4f3f-8ca9-14606f1a277a", null, null, "ConsumptionEvent_Update", null },
                    { "a9e2f381-984f-4a61-a23d-87e275f1754a", null, null, "ConsumptionEvent_Delete", null }
                });

            migrationBuilder.InsertData(
                table: "aspnet_users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsActive", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "fbae6926-e793-49e2-8b78-58f29255d2da", 0, "2cebd9d0-694d-4ed3-8dc2-384f41557310", "bob@example.com", false, true, false, null, "BOB@EXAMPLE.COM", "BOB", "AQAAAAEAACcQAAAAEI4jEmRsUYzL6KnpR2/OjIPvkI9BWNmnnCZYah1GFvB2EOCWkgkk49YqCJBz38N8rg==", null, false, "3YILVFJYDKC4OK7QLLR4TO4KT6V4ZK5E", false, "Bob" });

            migrationBuilder.InsertData(
                table: "accessgrouproles",
                columns: new[] { "accessgroupid", "roleid" },
                values: new object[,]
                {
                    { 1, "843060d1-9870-438f-877b-ccb8ecb34bdb" },
                    { 1, "7c4f6a61-1d3e-4c8d-be57-aa07f79b04be" },
                    { 1, "8ee765f3-f602-4b4b-bd3a-c4cdb9e78b61" },
                    { 1, "e92ad308-cd5d-4069-836c-fce42f832264" },
                    { 1, "fec9a8ca-a1c6-4bc2-9d54-e6b3dea198d6" },
                    { 1, "a8c49863-52ad-4e4d-9bdd-149d828e7932" },
                    { 1, "0a73f6bd-316f-4f3f-8ca9-14606f1a277a" },
                    { 1, "a9e2f381-984f-4a61-a23d-87e275f1754a" }
                });
        }
    }
}
