using Microsoft.EntityFrameworkCore.Migrations;

namespace Egret.Migrations
{
    public partial class addroledefaults : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
