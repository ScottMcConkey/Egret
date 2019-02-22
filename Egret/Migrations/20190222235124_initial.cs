using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Egret.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:adminpack", "'adminpack', '', ''");

            migrationBuilder.CreateSequence(
                name: "accessgroups_id_seq",
                startValue: 100L);

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
                name: "accessgroups",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false, defaultValueSql: "nextval('accessgroups_id_seq'::regclass)"),
                    name = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_accessgroups", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "currency_types",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false, defaultValueSql: "nextval('currencytypes_id_seq'::regclass)"),
                    name = table.Column<string>(nullable: false),
                    abbreviation = table.Column<string>(nullable: false),
                    symbol = table.Column<string>(nullable: true),
                    sortorder = table.Column<int>(nullable: false),
                    active = table.Column<bool>(nullable: false),
                    defaultselection = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_currency_types", x => x.id);
                    table.UniqueConstraint("ak_currency_types_abbreviation", x => x.abbreviation);
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
                    table.PrimaryKey("pk_inventory_categories", x => x.id);
                    table.UniqueConstraint("ak_inventory_categories_name", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orders", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "purchase_events",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false, defaultValueSql: "nextval('master_seq'::regclass)"),
                    dateadded = table.Column<DateTime>(nullable: true),
                    addedby = table.Column<string>(nullable: true),
                    dateupdated = table.Column<DateTime>(nullable: true),
                    updatedby = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    customerpurchasedfor = table.Column<string>(nullable: true),
                    supplier = table.Column<string>(nullable: true),
                    qtytopurchase = table.Column<string>(nullable: true),
                    fabrictesting = table.Column<string>(nullable: true),
                    targetprice = table.Column<string>(nullable: true),
                    bondedwarehouse = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_purchase_events", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    name = table.Column<string>(maxLength: 256, nullable: true),
                    normalizedname = table.Column<string>(maxLength: 256, nullable: true),
                    concurrencystamp = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    displayname = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_roles", x => x.id);
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
                    table.PrimaryKey("pk_units", x => x.id);
                    table.UniqueConstraint("ak_units_abbreviation", x => x.abbreviation);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    accessfailedcount = table.Column<int>(nullable: false),
                    emailconfirmed = table.Column<bool>(nullable: false),
                    lockoutenabled = table.Column<bool>(nullable: false),
                    lockoutend = table.Column<DateTimeOffset>(nullable: true),
                    phonenumberconfirmed = table.Column<bool>(nullable: false),
                    twofactorenabled = table.Column<bool>(nullable: false),
                    id = table.Column<string>(nullable: false),
                    username = table.Column<string>(maxLength: 256, nullable: true),
                    normalizedusername = table.Column<string>(maxLength: 256, nullable: true),
                    email = table.Column<string>(maxLength: 256, nullable: true),
                    normalizedemail = table.Column<string>(maxLength: 256, nullable: true),
                    passwordhash = table.Column<string>(nullable: true),
                    securitystamp = table.Column<string>(nullable: true),
                    concurrencystamp = table.Column<string>(nullable: true),
                    phonenumber = table.Column<string>(nullable: true),
                    isactive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "accessgroup_roles",
                columns: table => new
                {
                    accessgroupid = table.Column<int>(nullable: false),
                    roleid = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_accessgroup_roles", x => new { x.accessgroupid, x.roleid });
                    table.ForeignKey(
                        name: "fk_accessgroup_roles_accessgroups_accessgroupid",
                        column: x => x.accessgroupid,
                        principalTable: "accessgroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_accessgroup_roles_roles_roleid",
                        column: x => x.roleid,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "role_claims",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    roleid = table.Column<string>(nullable: false),
                    claimtype = table.Column<string>(nullable: true),
                    claimvalue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_role_claims_roles_roleid",
                        column: x => x.roleid,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "inventory_items",
                columns: table => new
                {
                    code = table.Column<string>(nullable: false, defaultValueSql: "nextval('master_seq'::regclass)"),
                    dateadded = table.Column<DateTime>(nullable: true),
                    addedby = table.Column<string>(nullable: true),
                    dateupdated = table.Column<DateTime>(nullable: true),
                    updatedby = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: false),
                    category = table.Column<string>(nullable: true),
                    qtypurchased = table.Column<decimal>(nullable: true),
                    unit = table.Column<string>(nullable: true),
                    fobcost = table.Column<decimal>(nullable: true),
                    fobcostcurrency = table.Column<string>(nullable: true),
                    shippingcost = table.Column<decimal>(nullable: true),
                    shippingcostcurrency = table.Column<string>(nullable: true),
                    importcosts = table.Column<decimal>(nullable: true),
                    importcostcurrency = table.Column<string>(nullable: true),
                    customerpurchasedfor = table.Column<string>(nullable: true),
                    customerreservedfor = table.Column<string>(nullable: true),
                    supplier = table.Column<string>(nullable: true),
                    qtytopurchasenow = table.Column<string>(nullable: true),
                    approxprodqty = table.Column<string>(nullable: true),
                    neededbefore = table.Column<DateTime>(nullable: true),
                    targetprice = table.Column<string>(nullable: true),
                    shippingcompany = table.Column<string>(nullable: true),
                    bondedwarehouse = table.Column<bool>(nullable: false),
                    dateconfirmed = table.Column<DateTime>(nullable: true),
                    dateshipped = table.Column<DateTime>(nullable: true),
                    datearrived = table.Column<DateTime>(nullable: true),
                    comments = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_inventory_items", x => x.code);
                    table.ForeignKey(
                        name: "fk_inventory_items_inventory_categories_category",
                        column: x => x.category,
                        principalTable: "inventory_categories",
                        principalColumn: "name",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_inventory_items_currency_types_fobcostcurrency",
                        column: x => x.fobcostcurrency,
                        principalTable: "currency_types",
                        principalColumn: "abbreviation",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_inventory_items_currency_types_importcostcurrency",
                        column: x => x.importcostcurrency,
                        principalTable: "currency_types",
                        principalColumn: "abbreviation",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_inventory_items_currency_types_shippingcostcurrency",
                        column: x => x.shippingcostcurrency,
                        principalTable: "currency_types",
                        principalColumn: "abbreviation",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_inventory_items_units_unit",
                        column: x => x.unit,
                        principalTable: "units",
                        principalColumn: "abbreviation",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "user_accessgroups",
                columns: table => new
                {
                    userid = table.Column<string>(nullable: false),
                    accessgroupid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_accessgroups", x => new { x.accessgroupid, x.userid });
                    table.ForeignKey(
                        name: "fk_user_accessgroups_accessgroups_accessgroupid",
                        column: x => x.accessgroupid,
                        principalTable: "accessgroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_accessgroups_users_userid",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_claims",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    userid = table.Column<string>(nullable: false),
                    claimtype = table.Column<string>(nullable: true),
                    claimvalue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_claims_users_userid",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_logins",
                columns: table => new
                {
                    loginprovider = table.Column<string>(nullable: false),
                    providerkey = table.Column<string>(nullable: false),
                    providerdisplayname = table.Column<string>(nullable: true),
                    userid = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_logins", x => new { x.loginprovider, x.providerkey });
                    table.ForeignKey(
                        name: "fk_user_logins_users_userid",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_roles",
                columns: table => new
                {
                    userid = table.Column<string>(nullable: false),
                    roleid = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_roles", x => new { x.userid, x.roleid });
                    table.ForeignKey(
                        name: "fk_user_roles_roles_roleid",
                        column: x => x.roleid,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_roles_users_userid",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_tokens",
                columns: table => new
                {
                    userid = table.Column<string>(nullable: false),
                    loginprovider = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_tokens", x => new { x.userid, x.loginprovider, x.name });
                    table.ForeignKey(
                        name: "fk_user_tokens_users_userid",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "consumption_events",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false, defaultValueSql: "nextval('master_seq'::regclass)"),
                    dateadded = table.Column<DateTime>(nullable: true),
                    addedby = table.Column<string>(nullable: true),
                    dateupdated = table.Column<DateTime>(nullable: true),
                    updatedby = table.Column<string>(nullable: true),
                    quantityconsumed = table.Column<decimal>(nullable: false),
                    unit = table.Column<string>(nullable: true),
                    consumedby = table.Column<string>(nullable: false),
                    dateofconsumption = table.Column<DateTime>(nullable: false),
                    ordernumber = table.Column<string>(nullable: true),
                    patternnumber = table.Column<string>(nullable: true),
                    comments = table.Column<string>(nullable: true),
                    inventoryitemcode = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_consumption_events", x => x.id);
                    table.ForeignKey(
                        name: "fk_consumption_events_inventory_items_inventoryitemcode",
                        column: x => x.inventoryitemcode,
                        principalTable: "inventory_items",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_consumption_events_orders_ordernumber",
                        column: x => x.ordernumber,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_consumption_events_units_unit",
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
                    inventoryitemcode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_fabric_tests", x => x.id);
                    table.ForeignKey(
                        name: "fk_fabric_tests_inventory_items_inventoryitemcode",
                        column: x => x.inventoryitemcode,
                        principalTable: "inventory_items",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "accessgroups",
                columns: new[] { "id", "description", "name" },
                values: new object[] { 1, null, "Administrator" });

            migrationBuilder.InsertData(
                table: "currency_types",
                columns: new[] { "id", "abbreviation", "active", "defaultselection", "name", "sortorder", "symbol" },
                values: new object[,]
                {
                    { 1, "USD", false, false, "United States Dollars", 1, "$" },
                    { 2, "NRP", true, true, "Nepali Rupees", 2, "रु" },
                    { 3, "INR", false, false, "Indian Rupees", 3, "₹" }
                });

            migrationBuilder.InsertData(
                table: "inventory_categories",
                columns: new[] { "id", "active", "description", "name", "sortorder" },
                values: new object[,]
                {
                    { 9, true, "", "Zipper", 9 },
                    { 8, true, "", "Woven", 8 },
                    { 7, true, "", "Thread", 7 },
                    { 6, true, "", "Other", 6 },
                    { 5, true, "", "Leather", 5 },
                    { 4, true, "", "Labels and Tags", 4 },
                    { 3, true, "", "Knit", 3 },
                    { 2, true, "", "Fastener", 2 },
                    { 1, true, "", "Elastic", 1 }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "concurrencystamp", "description", "displayname", "name", "normalizedname" },
                values: new object[,]
                {
                    { "1aea22b9-79ce-4233-8bc0-44ac18f0a521", null, null, "Administrator Access", "Admin_Access", "ADMIN_ACCESS" },
                    { "68b77443-5d0f-4fa0-a716-605e9ee9ee4d", null, null, "Consumption Event Delete", "ConsumptionEvent_Delete", "CONSUMPTIONEVENT_DELETE" },
                    { "3597a307-60cd-475c-a3ef-579ada15e6a6", null, null, "Consumption Event Update", "ConsumptionEvent_Edit", "CONSUMPTIONEVENT_EDIT" },
                    { "0da17aff-28b8-4c8f-b24c-63951be378c0", null, null, "Consumption Event Read", "ConsumptionEvent_Read", "CONSUMPTIONEVENT_READ" },
                    { "107dc2af-bc58-4648-99d8-24415a62e235", null, null, "Item Create", "Item_Create", "ITEM_CREATE" },
                    { "69575dfc-c26a-47c5-ba54-4824b229e85e", null, null, "Item Delete", "Item_Delete", "ITEM_DELETE" },
                    { "ed377c8f-d90b-4a19-a61c-1c878608ce0e", null, null, "Item Update", "Item_Edit", "ITEM_EDIT" },
                    { "d630c14b-28e8-4c98-9433-267e7364fede", null, null, "Item Read", "Item_Read", "ITEM_READ" },
                    { "72f6c5b2-93f0-41da-b808-80003bd4ee10", null, null, "Consumption Event Create", "ConsumptionEvent_Create", "CONSUMPTIONEVENT_CREATE" }
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

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "accessfailedcount", "concurrencystamp", "email", "emailconfirmed", "isactive", "lockoutenabled", "lockoutend", "normalizedemail", "normalizedusername", "passwordhash", "phonenumber", "phonenumberconfirmed", "securitystamp", "twofactorenabled", "username" },
                values: new object[] { "c2f3ae79-fd8a-465b-801a-439684d216da", 0, "2cebd9d0-694d-4ed3-8dc2-384f41557310", "bob@example.com", false, true, false, null, "BOB@EXAMPLE.COM", "BOB", "AQAAAAEAACcQAAAAEI4jEmRsUYzL6KnpR2/OjIPvkI9BWNmnnCZYah1GFvB2EOCWkgkk49YqCJBz38N8rg==", null, false, "3YILVFJYDKC4OK7QLLR4TO4KT6V4ZK5E", false, "Bob" });

            migrationBuilder.InsertData(
                table: "accessgroup_roles",
                columns: new[] { "accessgroupid", "roleid" },
                values: new object[,]
                {
                    { 1, "107dc2af-bc58-4648-99d8-24415a62e235" },
                    { 1, "d630c14b-28e8-4c98-9433-267e7364fede" },
                    { 1, "ed377c8f-d90b-4a19-a61c-1c878608ce0e" },
                    { 1, "69575dfc-c26a-47c5-ba54-4824b229e85e" },
                    { 1, "72f6c5b2-93f0-41da-b808-80003bd4ee10" },
                    { 1, "0da17aff-28b8-4c8f-b24c-63951be378c0" },
                    { 1, "3597a307-60cd-475c-a3ef-579ada15e6a6" },
                    { 1, "68b77443-5d0f-4fa0-a716-605e9ee9ee4d" },
                    { 1, "1aea22b9-79ce-4233-8bc0-44ac18f0a521" }
                });

            migrationBuilder.InsertData(
                table: "user_accessgroups",
                columns: new[] { "accessgroupid", "userid" },
                values: new object[] { 1, "c2f3ae79-fd8a-465b-801a-439684d216da" });

            migrationBuilder.InsertData(
                table: "user_roles",
                columns: new[] { "userid", "roleid" },
                values: new object[,]
                {
                    { "c2f3ae79-fd8a-465b-801a-439684d216da", "107dc2af-bc58-4648-99d8-24415a62e235" },
                    { "c2f3ae79-fd8a-465b-801a-439684d216da", "d630c14b-28e8-4c98-9433-267e7364fede" },
                    { "c2f3ae79-fd8a-465b-801a-439684d216da", "ed377c8f-d90b-4a19-a61c-1c878608ce0e" },
                    { "c2f3ae79-fd8a-465b-801a-439684d216da", "69575dfc-c26a-47c5-ba54-4824b229e85e" },
                    { "c2f3ae79-fd8a-465b-801a-439684d216da", "72f6c5b2-93f0-41da-b808-80003bd4ee10" },
                    { "c2f3ae79-fd8a-465b-801a-439684d216da", "0da17aff-28b8-4c8f-b24c-63951be378c0" },
                    { "c2f3ae79-fd8a-465b-801a-439684d216da", "3597a307-60cd-475c-a3ef-579ada15e6a6" },
                    { "c2f3ae79-fd8a-465b-801a-439684d216da", "68b77443-5d0f-4fa0-a716-605e9ee9ee4d" },
                    { "c2f3ae79-fd8a-465b-801a-439684d216da", "1aea22b9-79ce-4233-8bc0-44ac18f0a521" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_accessgroup_roles_roleid",
                table: "accessgroup_roles",
                column: "roleid");

            migrationBuilder.CreateIndex(
                name: "ix_consumption_events_inventoryitemcode",
                table: "consumption_events",
                column: "inventoryitemcode");

            migrationBuilder.CreateIndex(
                name: "ix_consumption_events_ordernumber",
                table: "consumption_events",
                column: "ordernumber");

            migrationBuilder.CreateIndex(
                name: "ix_consumption_events_unit",
                table: "consumption_events",
                column: "unit");

            migrationBuilder.CreateIndex(
                name: "ix_fabric_tests_inventoryitemcode",
                table: "fabric_tests",
                column: "inventoryitemcode");

            migrationBuilder.CreateIndex(
                name: "ix_inventory_items_category",
                table: "inventory_items",
                column: "category");

            migrationBuilder.CreateIndex(
                name: "ix_inventory_items_fobcostcurrency",
                table: "inventory_items",
                column: "fobcostcurrency");

            migrationBuilder.CreateIndex(
                name: "ix_inventory_items_importcostcurrency",
                table: "inventory_items",
                column: "importcostcurrency");

            migrationBuilder.CreateIndex(
                name: "ix_inventory_items_shippingcostcurrency",
                table: "inventory_items",
                column: "shippingcostcurrency");

            migrationBuilder.CreateIndex(
                name: "ix_inventory_items_unit",
                table: "inventory_items",
                column: "unit");

            migrationBuilder.CreateIndex(
                name: "ix_role_claims_roleid",
                table: "role_claims",
                column: "roleid");

            migrationBuilder.CreateIndex(
                name: "rolenameindex",
                table: "roles",
                column: "normalizedname",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_units_sortorder",
                table: "units",
                column: "sortorder",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_accessgroups_userid",
                table: "user_accessgroups",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "ix_user_claims_userid",
                table: "user_claims",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "ix_user_logins_userid",
                table: "user_logins",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "ix_user_roles_roleid",
                table: "user_roles",
                column: "roleid");

            migrationBuilder.CreateIndex(
                name: "emailindex",
                table: "users",
                column: "normalizedemail");

            migrationBuilder.CreateIndex(
                name: "usernameindex",
                table: "users",
                column: "normalizedusername",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "accessgroup_roles");

            migrationBuilder.DropTable(
                name: "consumption_events");

            migrationBuilder.DropTable(
                name: "fabric_tests");

            migrationBuilder.DropTable(
                name: "purchase_events");

            migrationBuilder.DropTable(
                name: "role_claims");

            migrationBuilder.DropTable(
                name: "user_accessgroups");

            migrationBuilder.DropTable(
                name: "user_claims");

            migrationBuilder.DropTable(
                name: "user_logins");

            migrationBuilder.DropTable(
                name: "user_roles");

            migrationBuilder.DropTable(
                name: "user_tokens");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "inventory_items");

            migrationBuilder.DropTable(
                name: "accessgroups");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "inventory_categories");

            migrationBuilder.DropTable(
                name: "currency_types");

            migrationBuilder.DropTable(
                name: "units");

            migrationBuilder.DropSequence(
                name: "accessgroups_id_seq");

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
