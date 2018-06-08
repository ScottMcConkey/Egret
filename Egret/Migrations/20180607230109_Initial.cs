using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Egret.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:adminpack", "'adminpack', '', ''");

            migrationBuilder.CreateSequence(
                name: "currency_types_sortorder_seq",
                startValue: 100L);

            migrationBuilder.CreateSequence(
                name: "inventory_categories_id_seq",
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
                    id = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    symbol = table.Column<string>(nullable: false),
                    sortorder = table.Column<int>(nullable: false, defaultValueSql: "nextval('currency_types_sortorder_seq'::regclass)"),
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
                    id = table.Column<int>(nullable: false, defaultValueSql: "nextval('inventory_categories_id_seq'::regclass)"),
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
                name: "suppliers",
                schema: "public",
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
                name: "AspNetRoleClaims",
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
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_aspnet_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "aspnet_roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
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
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_aspnet_users_UserId",
                        column: x => x.UserId,
                        principalTable: "aspnet_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_aspnet_users_UserId",
                        column: x => x.UserId,
                        principalTable: "aspnet_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_aspnet_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "aspnet_roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_aspnet_users_UserId",
                        column: x => x.UserId,
                        principalTable: "aspnet_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_aspnet_users_UserId",
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
                        name: "inventory_items_buycurrency_fk",
                        column: x => x.buycurrency,
                        principalTable: "currency_types",
                        principalColumn: "abbreviation",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "inventory_items_buyunit_fk",
                        column: x => x.buyunit_fk,
                        principalTable: "units",
                        principalColumn: "abbreviation",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "inventory_items_category_fk",
                        column: x => x.category,
                        principalTable: "inventory_categories",
                        principalColumn: "name",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "inventory_items_sellcurrency_fk",
                        column: x => x.sellcurrency,
                        principalTable: "currency_types",
                        principalColumn: "abbreviation",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "inventory_items_sellunit_fk",
                        column: x => x.sellunit_fk,
                        principalTable: "units",
                        principalColumn: "abbreviation",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FabricTest",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Result = table.Column<string>(nullable: true),
                    InventoryItemCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FabricTest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FabricTest_inventory_items_InventoryItemCode",
                        column: x => x.InventoryItemCode,
                        principalTable: "inventory_items",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "currency_types",
                columns: new[] { "id", "abbreviation", "active", "defaultselection", "name", "sortorder", "symbol" },
                values: new object[,]
                {
                    { 1, "USD", true, false, "United States Dollars", 1, "$" },
                    { 2, "NRP", true, true, "Nepali Rupees", 2, "रु" },
                    { 3, "INR", true, false, "Indian Rupees", 3, "₹" }
                });

            migrationBuilder.InsertData(
                table: "inventory_categories",
                columns: new[] { "id", "active", "description", "name", "sortorder" },
                values: new object[,]
                {
                    { 11, true, "", "Zipper", 11 },
                    { 10, true, "", "Woven Fabric", 10 },
                    { 9, true, "", "Snap", 9 },
                    { 8, true, "", "Other", 8 },
                    { 6, true, "", "Label", 6 },
                    { 7, true, "", "Leather", 7 },
                    { 4, true, "", "Hang-Tag", 4 },
                    { 3, true, "", "Elastic", 3 },
                    { 2, true, "", "Button", 2 },
                    { 1, true, "", "Buckle Thread", 1 },
                    { 5, true, "", "Knit Fabric", 5 }
                });

            migrationBuilder.InsertData(
                table: "units",
                columns: new[] { "id", "abbreviation", "active", "name", "sortorder" },
                values: new object[,]
                {
                    { 5, "cm", true, "centimeters", 5 },
                    { 1, "kg", true, "kilograms", 1 },
                    { 2, "m", true, "meters", 2 },
                    { 3, "ea", true, "each", 3 },
                    { 4, "g/m2", true, "grams per square meter", 4 },
                    { 6, "sqf", true, "square feet", 6 }
                });

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "aspnet_roles",
                column: "NormalizedName",
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
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "currency_types_abbreviation_key",
                table: "currency_types",
                column: "abbreviation",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "currency_types_sort_key",
                table: "currency_types",
                column: "sortorder",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FabricTest_InventoryItemCode",
                table: "FabricTest",
                column: "InventoryItemCode");

            migrationBuilder.CreateIndex(
                name: "inventory_categories_name_key",
                table: "inventory_categories",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "inventory_categories_sort_key",
                table: "inventory_categories",
                column: "sortorder",
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
                name: "units_abbreviation_key",
                table: "units",
                column: "abbreviation",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "units_sort_key",
                table: "units",
                column: "sortorder",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "suppliers_pkey",
                schema: "public",
                table: "suppliers",
                column: "id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "FabricTest");

            migrationBuilder.DropTable(
                name: "suppliers",
                schema: "public");

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
                name: "currency_types_sortorder_seq");

            migrationBuilder.DropSequence(
                name: "inventory_categories_id_seq");

            migrationBuilder.DropSequence(
                name: "master_seq");

            migrationBuilder.DropSequence(
                name: "units_id_seq");
        }
    }
}
