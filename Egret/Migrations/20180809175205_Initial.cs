using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Egret.Migrations
{
    public partial class Initial : Migration
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
                    id = table.Column<string>(maxLength: 450, nullable: false),
                    name = table.Column<string>(maxLength: 256, nullable: true),
                    normalized_name = table.Column<string>(maxLength: 256, nullable: true),
                    concurrency_stamp = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aspnet_roles", x => x.id);
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
                    IsActive = table.Column<bool>(nullable: false)
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
                    table.PrimaryKey("pk_currencytypes_id", x => x.id);
                    table.UniqueConstraint("uk_currencytypes_abbreviation", x => x.abbreviation);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUser",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 450, nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUser", x => x.id);
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
                    table.PrimaryKey("pk_inventorycategories_id", x => x.id);
                    table.UniqueConstraint("uk_inventorycategories_name", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "suppliers",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    table.PrimaryKey("pk_units_id", x => x.id);
                    table.UniqueConstraint("uk_units_abbreviation", x => x.abbreviation);
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
                        principalColumn: "id",
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
                        principalColumn: "id",
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
                    date_added = table.Column<DateTime>(nullable: true),
                    user_added_by = table.Column<string>(nullable: true),
                    date_updated = table.Column<DateTime>(nullable: true),
                    user_updated_by = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: false),
                    customer_purchased_for = table.Column<string>(nullable: true),
                    customer_reserved_for = table.Column<string>(nullable: true),
                    supplier = table.Column<string>(nullable: true),
                    qty_to_purchase_now = table.Column<string>(nullable: true),
                    approx_prod_qty = table.Column<string>(nullable: true),
                    needed_before = table.Column<DateTime>(nullable: true),
                    target_price = table.Column<string>(nullable: true),
                    shipping_company = table.Column<string>(nullable: true),
                    bonded_warehouse = table.Column<bool>(nullable: false),
                    date_confirmed = table.Column<DateTime>(nullable: true),
                    date_shipped = table.Column<DateTime>(nullable: true),
                    date_arrived = table.Column<DateTime>(nullable: true),
                    comment = table.Column<string>(nullable: true),
                    qty_purchased = table.Column<decimal>(nullable: true),
                    unit = table.Column<string>(nullable: true),
                    fob_cost = table.Column<decimal>(nullable: true),
                    FOBCostCurrency = table.Column<string>(nullable: true),
                    shipping_cost = table.Column<decimal>(nullable: true),
                    ShippingCostCurrency = table.Column<string>(nullable: true),
                    import_cost = table.Column<decimal>(nullable: true),
                    ImportCostCurrency = table.Column<string>(nullable: true),
                    buy_price = table.Column<decimal>(nullable: true),
                    buy_currency = table.Column<string>(nullable: true),
                    category = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_inventoryitems_id", x => x.code);
                    table.ForeignKey(
                        name: "fk_inventoryitems_buycurrency",
                        column: x => x.buy_currency,
                        principalTable: "currency_types",
                        principalColumn: "abbreviation",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_inventoryitems_category",
                        column: x => x.category,
                        principalTable: "inventory_categories",
                        principalColumn: "name",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_inventoryitems_fobcostunit",
                        column: x => x.FOBCostCurrency,
                        principalTable: "currency_types",
                        principalColumn: "abbreviation",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_inventoryitems_importcostunit",
                        column: x => x.ImportCostCurrency,
                        principalTable: "currency_types",
                        principalColumn: "abbreviation",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_inventoryitems_shippingcostunit",
                        column: x => x.ShippingCostCurrency,
                        principalTable: "currency_types",
                        principalColumn: "abbreviation",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_inventoryitems_unit",
                        column: x => x.unit,
                        principalTable: "units",
                        principalColumn: "abbreviation",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "consumption_events",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false, defaultValueSql: "nextval('master_seq'::regclass)"),
                    date_added = table.Column<DateTime>(nullable: true),
                    user_added_by = table.Column<string>(nullable: true),
                    date_updated = table.Column<DateTime>(nullable: true),
                    user_updated_by = table.Column<string>(nullable: true),
                    quantity_consumed = table.Column<decimal>(nullable: false),
                    unit = table.Column<string>(nullable: true),
                    consumed_by = table.Column<string>(nullable: false),
                    date_of_consumption = table.Column<DateTime>(nullable: false),
                    order_number = table.Column<string>(nullable: true),
                    pattern_number = table.Column<string>(nullable: true),
                    inventory_item_code = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_consumptionevents_id", x => x.id);
                    table.ForeignKey(
                        name: "fk_consumptionevents_inventory_code",
                        column: x => x.inventory_item_code,
                        principalTable: "inventory_items",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_order_id",
                        column: x => x.order_number,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_consumptionevents_units",
                        column: x => x.unit,
                        principalTable: "units",
                        principalColumn: "abbreviation",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "fabric_tests",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false, defaultValueSql: "nextval('master_seq'::regclass)"),
                    name = table.Column<string>(nullable: true),
                    result = table.Column<string>(nullable: true),
                    inventory_item_code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_fabrictests_id", x => x.id);
                    table.ForeignKey(
                        name: "fk_inventoryitems_fabrictests",
                        column: x => x.inventory_item_code,
                        principalTable: "inventory_items",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "aspnet_users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsActive", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "0", 0, "2cebd9d0-694d-4ed3-8dc2-384f41557310", "bob@example.com", false, true, false, null, "BOB@EXAMPLE.COM", "BOB", "AQAAAAEAACcQAAAAEI4jEmRsUYzL6KnpR2/OjIPvkI9BWNmnnCZYah1GFvB2EOCWkgkk49YqCJBz38N8rg==", null, false, "3YILVFJYDKC4OK7QLLR4TO4KT6V4ZK5E", false, "Bob" });

            migrationBuilder.InsertData(
                table: "currency_types",
                columns: new[] { "id", "abbreviation", "active", "defaultselection", "name", "sortorder", "symbol" },
                values: new object[,]
                {
                    { 3, "INR", false, false, "Indian Rupees", 3, "₹" },
                    { 1, "USD", false, false, "United States Dollars", 1, "$" },
                    { 2, "NRP", true, true, "Nepali Rupees", 2, "रु" }
                });

            migrationBuilder.InsertData(
                table: "inventory_categories",
                columns: new[] { "id", "active", "description", "name", "sortorder" },
                values: new object[,]
                {
                    { 1, true, "", "Elastic", 1 },
                    { 2, true, "", "Fastener", 2 },
                    { 3, true, "", "Knit", 3 },
                    { 4, true, "", "Labels and Tags", 4 },
                    { 5, true, "", "Leather", 5 },
                    { 7, true, "", "Thread", 7 },
                    { 8, true, "", "Woven", 8 },
                    { 9, true, "", "Zipper", 9 },
                    { 6, true, "", "Other", 6 }
                });

            migrationBuilder.InsertData(
                table: "units",
                columns: new[] { "id", "abbreviation", "active", "name", "sortorder" },
                values: new object[,]
                {
                    { 4, "set", true, "set", 4 },
                    { 1, "kg", true, "kilogram", 1 },
                    { 2, "meter", true, "meter", 2 },
                    { 3, "piece", true, "piece", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_aspnet_roleclaims_RoleId",
                table: "aspnet_roleclaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "aspnet_roles",
                column: "normalized_name",
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
                name: "ix_consumptionevents_id",
                table: "consumption_events",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_consumption_events_inventory_item_code",
                table: "consumption_events",
                column: "inventory_item_code");

            migrationBuilder.CreateIndex(
                name: "ix_consumptionevents_ordernumber",
                table: "consumption_events",
                column: "order_number");

            migrationBuilder.CreateIndex(
                name: "IX_consumption_events_unit",
                table: "consumption_events",
                column: "unit");

            migrationBuilder.CreateIndex(
                name: "ix_currencytypes_abbreviation",
                table: "currency_types",
                column: "abbreviation",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_currencytypes_id",
                table: "currency_types",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_fabrictests_id",
                table: "fabric_tests",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_fabric_tests_inventory_item_code",
                table: "fabric_tests",
                column: "inventory_item_code");

            migrationBuilder.CreateIndex(
                name: "ix_inventorycategories_id",
                table: "inventory_categories",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_inventorycategories_name",
                table: "inventory_categories",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_inventory_items_buy_currency",
                table: "inventory_items",
                column: "buy_currency");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_items_category",
                table: "inventory_items",
                column: "category");

            migrationBuilder.CreateIndex(
                name: "ix_inventoryitems_description",
                table: "inventory_items",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_inventory_items_FOBCostCurrency",
                table: "inventory_items",
                column: "FOBCostCurrency");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_items_ImportCostCurrency",
                table: "inventory_items",
                column: "ImportCostCurrency");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_items_ShippingCostCurrency",
                table: "inventory_items",
                column: "ShippingCostCurrency");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_items_unit",
                table: "inventory_items",
                column: "unit");

            migrationBuilder.CreateIndex(
                name: "ix_orders_id",
                table: "orders",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_suppliers_id",
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
                name: "IdentityUser");

            migrationBuilder.DropTable(
                name: "suppliers");

            migrationBuilder.DropTable(
                name: "aspnet_roles");

            migrationBuilder.DropTable(
                name: "aspnet_users");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "inventory_items");

            migrationBuilder.DropTable(
                name: "currency_types");

            migrationBuilder.DropTable(
                name: "inventory_categories");

            migrationBuilder.DropTable(
                name: "units");

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
