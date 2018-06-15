using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Egret.Migrations
{
    public partial class ConsumptionEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:adminpack", "'adminpack', '', ''");

            migrationBuilder.CreateSequence(
                name: "currencytypes_id_seq",
                startValue: 100L);

            migrationBuilder.CreateSequence(
                name: "inventorycategories_id_seq",
                startValue: 100L);

            migrationBuilder.CreateSequence(
                name: "master_seq",
                startValue: 1000L);

            migrationBuilder.CreateSequence(
                name: "units_id_seq",
                startValue: 100L);

            migrationBuilder.CreateTable(
                name: "aspnet_roles",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 450, nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aspnet_roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "aspnet_users",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 450, nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    IsActive = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aspnet_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "currency_types",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false, defaultValueSql: "nextval('currencytypes_id_seq'::regclass)"),
                    name = table.Column<string>(nullable: false),
                    symbol = table.Column<string>(nullable: false),
                    sortorder = table.Column<int>(nullable: false),
                    abbreviation = table.Column<string>(nullable: false),
                    active = table.Column<bool>(nullable: false),
                    defaultselection = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_currency_types", x => x.id);
                    table.UniqueConstraint("AK_currency_types_abbreviation", x => x.abbreviation);
                });

            migrationBuilder.CreateTable(
                name: "inventory_categories",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false, defaultValueSql: "nextval('inventorycategories_id_seq'::regclass)"),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    sortorder = table.Column<int>(nullable: false),
                    active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventory_categories", x => x.id);
                    table.UniqueConstraint("AK_inventory_categories_name", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "suppliers",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_suppliers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "units",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false, defaultValueSql: "nextval('units_id_seq'::regclass)"),
                    name = table.Column<string>(nullable: false),
                    abbreviation = table.Column<string>(nullable: false),
                    sortorder = table.Column<int>(nullable: false),
                    active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_units", x => x.id);
                    table.UniqueConstraint("AK_units_abbreviation", x => x.abbreviation);
                });

            migrationBuilder.CreateTable(
                name: "aspnet_roleclaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aspnet_roleclaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_aspnet_roleclaims_aspnet_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "aspnet_roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "aspnet_userclaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aspnet_userclaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_aspnet_userclaims_aspnet_users_UserId",
                        column: x => x.UserId,
                        principalTable: "aspnet_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "aspnet_userlogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aspnet_userlogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_aspnet_userlogins_aspnet_users_UserId",
                        column: x => x.UserId,
                        principalTable: "aspnet_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "aspnet_userroles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aspnet_userroles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_aspnet_userroles_aspnet_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "aspnet_roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_aspnet_userroles_aspnet_users_UserId",
                        column: x => x.UserId,
                        principalTable: "aspnet_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "aspnet_usertokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aspnet_usertokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_aspnet_usertokens_aspnet_users_UserId",
                        column: x => x.UserId,
                        principalTable: "aspnet_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "inventory_items",
                columns: table => new
                {
                    code = table.Column<string>(nullable: false, defaultValueSql: "nextval('master_seq'::regclass)"),
                    dateadded = table.Column<DateTime>(nullable: true),
                    useraddedby = table.Column<string>(nullable: true),
                    dateupdated = table.Column<DateTime>(nullable: true),
                    userupdatedby = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: false),
                    customerpurchasedfor = table.Column<string>(nullable: true),
                    customerreservedfor = table.Column<string>(nullable: true),
                    supplier_fk = table.Column<int>(nullable: true),
                    qtytopurchasenow = table.Column<string>(nullable: true),
                    approxprodqty = table.Column<string>(nullable: true),
                    fabrictests_conversion = table.Column<string>(nullable: true),
                    fabrictestresults = table.Column<string>(nullable: true),
                    neededbefore = table.Column<DateTime>(nullable: true),
                    targetprice = table.Column<string>(nullable: true),
                    shippingcompany = table.Column<string>(nullable: true),
                    bondedwarehouse = table.Column<bool>(nullable: true),
                    dateconfirmed = table.Column<DateTime>(nullable: true),
                    dateshipped = table.Column<DateTime>(nullable: true),
                    datearrived = table.Column<DateTime>(nullable: true),
                    comment = table.Column<string>(nullable: true),
                    qtypurchased = table.Column<decimal>(nullable: true),
                    unit = table.Column<string>(nullable: true),
                    fobcost = table.Column<decimal>(nullable: true),
                    shippingcost = table.Column<decimal>(nullable: true),
                    importcosts = table.Column<decimal>(nullable: true),
                    sellprice = table.Column<decimal>(nullable: true),
                    sellcurrency = table.Column<string>(nullable: true),
                    sellunit_fk = table.Column<string>(nullable: true),
                    buyprice = table.Column<decimal>(nullable: true),
                    buycurrency = table.Column<string>(nullable: true),
                    buyunit_fk = table.Column<string>(nullable: true),
                    category = table.Column<string>(nullable: true),
                    isconversion = table.Column<bool>(nullable: false),
                    conversionsource = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventory_items", x => x.code);
                    table.ForeignKey(
                        name: "inventoryitems_buycurrency_fk",
                        column: x => x.buycurrency,
                        principalTable: "currency_types",
                        principalColumn: "abbreviation",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "inventoryitems_buyunit_fk",
                        column: x => x.buyunit_fk,
                        principalTable: "units",
                        principalColumn: "abbreviation",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "inventoryitems_category_fk",
                        column: x => x.category,
                        principalTable: "inventory_categories",
                        principalColumn: "name",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "inventoryitems_sellcurrency_fk",
                        column: x => x.sellcurrency,
                        principalTable: "currency_types",
                        principalColumn: "abbreviation",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "inventoryitems_sellunit_fk",
                        column: x => x.sellunit_fk,
                        principalTable: "units",
                        principalColumn: "abbreviation",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "consumption_events",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false, defaultValueSql: "nextval('master_seq'::regclass)"),
                    quantity_consumed = table.Column<decimal>(nullable: true),
                    Unit = table.Column<string>(nullable: true),
                    consumed_by = table.Column<string>(nullable: true),
                    date_of_consumption = table.Column<DateTime>(nullable: false),
                    SampleOrderNumber = table.Column<string>(nullable: true),
                    production_order_number = table.Column<string>(nullable: true),
                    pattern_number = table.Column<string>(nullable: true),
                    value_consumed = table.Column<decimal>(nullable: true),
                    InventoryItemCode = table.Column<string>(nullable: true),
                    UnitId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_consumption_events", x => x.id);
                    table.ForeignKey(
                        name: "FK_consumption_events_inventory_items_InventoryItemCode",
                        column: x => x.InventoryItemCode,
                        principalTable: "inventory_items",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_consumption_events_units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "units",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "fabric_tests",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    result = table.Column<string>(nullable: true),
                    InventoryItemCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fabric_tests", x => x.id);
                    table.ForeignKey(
                        name: "FK_fabric_tests_inventory_items_InventoryItemCode",
                        column: x => x.InventoryItemCode,
                        principalTable: "inventory_items",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "aspnet_users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "IsActive" },
                values: new object[] { "0", 0, "2cebd9d0-694d-4ed3-8dc2-384f41557310", "User", "bob@example.com", false, false, null, "BOB@EXAMPLE.COM", "BOB", "AQAAAAEAACcQAAAAEI4jEmRsUYzL6KnpR2/OjIPvkI9BWNmnnCZYah1GFvB2EOCWkgkk49YqCJBz38N8rg==", null, false, "3YILVFJYDKC4OK7QLLR4TO4KT6V4ZK5E", false, "Bob", true });

            migrationBuilder.InsertData(
                table: "currency_types",
                columns: new[] { "id", "abbreviation", "active", "defaultselection", "name", "sortorder", "symbol" },
                values: new object[,]
                {
                    { 3, "INR", true, false, "Indian Rupees", 3, "₹" },
                    { 1, "USD", true, false, "United States Dollars", 1, "$" },
                    { 2, "NRP", true, true, "Nepali Rupees", 2, "रु" }
                });

            migrationBuilder.InsertData(
                table: "inventory_categories",
                columns: new[] { "id", "active", "description", "name", "sortorder" },
                values: new object[,]
                {
                    { 1, true, "", "Buckle Thread", 1 },
                    { 11, true, "", "Zipper", 11 },
                    { 10, true, "", "Woven Fabric", 10 },
                    { 7, true, "", "Leather", 7 },
                    { 9, true, "", "Snap", 9 },
                    { 5, true, "", "Knit Fabric", 5 },
                    { 4, true, "", "Hang-Tag", 4 },
                    { 3, true, "", "Elastic", 3 },
                    { 2, true, "", "Button", 2 },
                    { 6, true, "", "Label", 6 },
                    { 8, true, "", "Other", 8 }
                });

            migrationBuilder.InsertData(
                table: "units",
                columns: new[] { "id", "abbreviation", "active", "name", "sortorder" },
                values: new object[,]
                {
                    { 6, "sqf", true, "square feet", 6 },
                    { 1, "kg", true, "kilograms", 1 },
                    { 2, "m", true, "meters", 2 },
                    { 3, "ea", true, "each", 3 },
                    { 4, "g/m2", true, "grams per square meter", 4 },
                    { 5, "cm", true, "centimeters", 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_aspnet_roleclaims_RoleId",
                table: "aspnet_roleclaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "aspnet_roles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_aspnet_userclaims_UserId",
                table: "aspnet_userclaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_aspnet_userlogins_UserId",
                table: "aspnet_userlogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "ix_aspnet_userroles_roleid",
                table: "aspnet_userroles",
                column: "RoleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "aspnet_users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "aspnet_users",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "id",
                table: "consumption_events",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_consumption_events_InventoryItemCode",
                table: "consumption_events",
                column: "InventoryItemCode");

            migrationBuilder.CreateIndex(
                name: "IX_consumption_events_UnitId",
                table: "consumption_events",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "ix_currencytypes_abbreviation",
                table: "currency_types",
                column: "abbreviation",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_currencytypes_sortorder",
                table: "currency_types",
                column: "sortorder",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_fabrictests_pk",
                table: "fabric_tests",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_fabric_tests_InventoryItemCode",
                table: "fabric_tests",
                column: "InventoryItemCode");

            migrationBuilder.CreateIndex(
                name: "ix_inventorycategories_name",
                table: "inventory_categories",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_inventory_items_buycurrency",
                table: "inventory_items",
                column: "buycurrency");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_items_buyunit_fk",
                table: "inventory_items",
                column: "buyunit_fk");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_items_category",
                table: "inventory_items",
                column: "category");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_items_sellcurrency",
                table: "inventory_items",
                column: "sellcurrency");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_items_sellunit_fk",
                table: "inventory_items",
                column: "sellunit_fk");

            migrationBuilder.CreateIndex(
                name: "ix_suppliers_pk",
                table: "suppliers",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_units_abbreviation",
                table: "units",
                column: "abbreviation",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_units_sortorder",
                table: "units",
                column: "sortorder",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "aspnet_roleclaims");

            migrationBuilder.DropTable(
                name: "aspnet_userclaims");

            migrationBuilder.DropTable(
                name: "aspnet_userlogins");

            migrationBuilder.DropTable(
                name: "aspnet_userroles");

            migrationBuilder.DropTable(
                name: "aspnet_usertokens");

            migrationBuilder.DropTable(
                name: "consumption_events");

            migrationBuilder.DropTable(
                name: "fabric_tests");

            migrationBuilder.DropTable(
                name: "suppliers");

            migrationBuilder.DropTable(
                name: "aspnet_roles");

            migrationBuilder.DropTable(
                name: "aspnet_users");

            migrationBuilder.DropTable(
                name: "inventory_items");

            migrationBuilder.DropTable(
                name: "currency_types");

            migrationBuilder.DropTable(
                name: "units");

            migrationBuilder.DropTable(
                name: "inventory_categories");

            migrationBuilder.DropSequence(
                name: "currencytypes_id_seq");

            migrationBuilder.DropSequence(
                name: "inventorycategories_id_seq");

            migrationBuilder.DropSequence(
                name: "master_seq");

            migrationBuilder.DropSequence(
                name: "units_id_seq");
        }
    }
}
