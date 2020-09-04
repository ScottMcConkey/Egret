using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Egret.Migrations
{
    public partial class v010 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:adminpack", ",,");

            migrationBuilder.CreateSequence(
                name: "access_groups_id_seq",
                startValue: 100L);

            migrationBuilder.CreateSequence(
                name: "consumption_events_id_seq",
                startValue: 1000L);

            migrationBuilder.CreateSequence(
                name: "currency_types_id_seq",
                startValue: 100L);

            migrationBuilder.CreateSequence(
                name: "fabric_tests_id_seq",
                startValue: 1000L);

            migrationBuilder.CreateSequence(
                name: "inventory_categories_id_seq",
                startValue: 100L);

            migrationBuilder.CreateSequence(
                name: "items_id_seq",
                startValue: 1000L);

            migrationBuilder.CreateSequence(
                name: "storage_location_id_seq");

            migrationBuilder.CreateSequence(
                name: "units_id_seq",
                startValue: 100L);

            migrationBuilder.CreateTable(
                name: "access_groups",
                columns: table => new
                {
                    access_group_id = table.Column<int>(nullable: false, defaultValueSql: "nextval('access_groups_id_seq'::regclass)"),
                    name = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_access_groups", x => x.access_group_id);
                });

            migrationBuilder.CreateTable(
                name: "currency_types",
                columns: table => new
                {
                    currency_type_id = table.Column<int>(nullable: false, defaultValueSql: "nextval('currency_types_id_seq'::regclass)"),
                    name = table.Column<string>(nullable: false),
                    abbreviation = table.Column<string>(nullable: false),
                    symbol = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_currency_types", x => x.currency_type_id);
                });

            migrationBuilder.CreateTable(
                name: "inventory_categories",
                columns: table => new
                {
                    inventory_category_id = table.Column<int>(nullable: false, defaultValueSql: "nextval('inventory_categories_id_seq'::regclass)"),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    sort_order = table.Column<int>(nullable: false),
                    active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_inventory_categories", x => x.inventory_category_id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    name = table.Column<string>(maxLength: 256, nullable: true),
                    normalized_name = table.Column<string>(maxLength: 256, nullable: true),
                    concurrency_stamp = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    display_name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "storage_locations",
                columns: table => new
                {
                    storage_location_id = table.Column<int>(nullable: false, defaultValueSql: "nextval('storage_location_id_seq'::regclass)"),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    sort_order = table.Column<int>(nullable: false),
                    active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_storage_locations", x => x.storage_location_id);
                });

            migrationBuilder.CreateTable(
                name: "units",
                columns: table => new
                {
                    unit_id = table.Column<int>(nullable: false, defaultValueSql: "nextval('units_id_seq'::regclass)"),
                    name = table.Column<string>(nullable: false),
                    abbreviation = table.Column<string>(nullable: false),
                    sort_order = table.Column<int>(nullable: false),
                    active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_units", x => x.unit_id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    user_name = table.Column<string>(maxLength: 256, nullable: true),
                    normalized_user_name = table.Column<string>(maxLength: 256, nullable: true),
                    email = table.Column<string>(maxLength: 256, nullable: true),
                    normalized_email = table.Column<string>(maxLength: 256, nullable: true),
                    email_confirmed = table.Column<bool>(nullable: false),
                    password_hash = table.Column<string>(nullable: true),
                    security_stamp = table.Column<string>(nullable: true),
                    concurrency_stamp = table.Column<string>(nullable: true),
                    phone_number = table.Column<string>(nullable: true),
                    phone_number_confirmed = table.Column<bool>(nullable: false),
                    two_factor_enabled = table.Column<bool>(nullable: false),
                    lockout_end = table.Column<DateTimeOffset>(nullable: true),
                    lockout_enabled = table.Column<bool>(nullable: false),
                    access_failed_count = table.Column<int>(nullable: false),
                    active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "access_group_roles",
                columns: table => new
                {
                    access_group_id = table.Column<int>(nullable: false),
                    role_id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_access_group_roles", x => new { x.access_group_id, x.role_id });
                    table.ForeignKey(
                        name: "fk_access_group_roles_access_groups_access_group_id",
                        column: x => x.access_group_id,
                        principalTable: "access_groups",
                        principalColumn: "access_group_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_access_group_roles_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "role_claims",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    role_id = table.Column<string>(nullable: false),
                    claim_type = table.Column<string>(nullable: true),
                    claim_value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_role_claims_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "inventory_items",
                columns: table => new
                {
                    inventory_item_id = table.Column<string>(nullable: false, defaultValueSql: "'I' || nextval('items_id_seq'::regclass)"),
                    date_added = table.Column<DateTime>(nullable: true),
                    added_by = table.Column<string>(nullable: true),
                    date_updated = table.Column<DateTime>(nullable: true),
                    updated_by = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: false),
                    inventory_category_id = table.Column<int>(nullable: false),
                    storage_location_id = table.Column<int>(nullable: true),
                    quantity_purchased = table.Column<decimal>(nullable: false),
                    unit_id = table.Column<int>(nullable: false),
                    fob_cost = table.Column<decimal>(nullable: false),
                    shipping_cost = table.Column<decimal>(nullable: false),
                    import_cost = table.Column<decimal>(nullable: false),
                    vat_cost = table.Column<decimal>(nullable: false),
                    customer_purchased_for = table.Column<string>(nullable: false),
                    customer_reserved_for = table.Column<string>(nullable: false),
                    supplier = table.Column<string>(nullable: true),
                    quantity_to_purchase_now = table.Column<string>(nullable: true),
                    target_price = table.Column<string>(nullable: true),
                    shipping_company = table.Column<string>(nullable: true),
                    bonded_warehouse = table.Column<bool>(nullable: false),
                    date_confirmed = table.Column<DateTime>(nullable: true),
                    date_shipped = table.Column<DateTime>(nullable: true),
                    date_arrived = table.Column<DateTime>(nullable: true),
                    comments = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_inventory_items", x => x.inventory_item_id);
                    table.ForeignKey(
                        name: "fk_inventory_items_inventory_categories_inventory_category_id",
                        column: x => x.inventory_category_id,
                        principalTable: "inventory_categories",
                        principalColumn: "inventory_category_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_inventory_items_storage_locations_storage_location_id",
                        column: x => x.storage_location_id,
                        principalTable: "storage_locations",
                        principalColumn: "storage_location_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_inventory_items_units_unit_id",
                        column: x => x.unit_id,
                        principalTable: "units",
                        principalColumn: "unit_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_access_groups",
                columns: table => new
                {
                    user_id = table.Column<string>(nullable: false),
                    access_group_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_access_groups", x => new { x.access_group_id, x.user_id });
                    table.ForeignKey(
                        name: "fk_user_access_groups_access_groups_access_group_id",
                        column: x => x.access_group_id,
                        principalTable: "access_groups",
                        principalColumn: "access_group_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_access_groups_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_claims",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<string>(nullable: false),
                    claim_type = table.Column<string>(nullable: true),
                    claim_value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_claims_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_logins",
                columns: table => new
                {
                    login_provider = table.Column<string>(nullable: false),
                    provider_key = table.Column<string>(nullable: false),
                    provider_display_name = table.Column<string>(nullable: true),
                    user_id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_logins", x => new { x.login_provider, x.provider_key });
                    table.ForeignKey(
                        name: "fk_user_logins_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_roles",
                columns: table => new
                {
                    user_id = table.Column<string>(nullable: false),
                    role_id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_roles", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "fk_user_roles_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_roles_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_tokens",
                columns: table => new
                {
                    user_id = table.Column<string>(nullable: false),
                    login_provider = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_tokens", x => new { x.user_id, x.login_provider, x.name });
                    table.ForeignKey(
                        name: "fk_user_tokens_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "consumption_events",
                columns: table => new
                {
                    consumption_event_id = table.Column<string>(nullable: false, defaultValueSql: "'CE' || nextval('consumption_events_id_seq'::regclass)"),
                    date_added = table.Column<DateTime>(nullable: true),
                    added_by = table.Column<string>(nullable: true),
                    date_updated = table.Column<DateTime>(nullable: true),
                    updated_by = table.Column<string>(nullable: true),
                    quantity_consumed = table.Column<decimal>(nullable: false),
                    consumed_by = table.Column<string>(nullable: false),
                    date_consumed = table.Column<DateTime>(nullable: false),
                    order_number = table.Column<string>(nullable: true),
                    pattern_number = table.Column<string>(nullable: true),
                    comments = table.Column<string>(nullable: true),
                    inventory_item_id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_consumption_events", x => x.consumption_event_id);
                    table.ForeignKey(
                        name: "fk_consumption_events_inventory_items_inventory_item_id",
                        column: x => x.inventory_item_id,
                        principalTable: "inventory_items",
                        principalColumn: "inventory_item_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fabric_tests",
                columns: table => new
                {
                    fabric_test_id = table.Column<string>(nullable: false, defaultValueSql: "nextval('fabric_tests_id_seq'::regclass)"),
                    name = table.Column<string>(nullable: true),
                    result = table.Column<string>(nullable: true),
                    inventory_item_id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_fabric_tests", x => x.fabric_test_id);
                    table.ForeignKey(
                        name: "fk_fabric_tests_inventory_items_inventory_item_id",
                        column: x => x.inventory_item_id,
                        principalTable: "inventory_items",
                        principalColumn: "inventory_item_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "access_groups",
                columns: new[] { "access_group_id", "description", "name" },
                values: new object[] { 1, null, "Administrator" });

            migrationBuilder.InsertData(
                table: "currency_types",
                columns: new[] { "currency_type_id", "abbreviation", "name", "symbol" },
                values: new object[] { 1, "NRP", "Nepali Rupees", "रु" });

            migrationBuilder.InsertData(
                table: "inventory_categories",
                columns: new[] { "inventory_category_id", "active", "description", "name", "sort_order" },
                values: new object[,]
                {
                    { 1, true, "", "Elastic", 1 },
                    { 2, true, "", "Fastener", 2 },
                    { 3, true, "", "Knit", 3 },
                    { 4, true, "", "Labels and Tags", 4 },
                    { 5, true, "", "Leather", 5 },
                    { 6, true, "", "Other", 6 },
                    { 7, true, "", "Thread", 7 },
                    { 8, true, "", "Woven", 8 },
                    { 9, true, "", "Zipper", 9 }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "concurrency_stamp", "description", "display_name", "name", "normalized_name" },
                values: new object[,]
                {
                    { "f6bb4564-6919-484b-897b-4a2c994721e5", null, null, "Report Read", "Report_Read", "REPORT_READ" },
                    { "d9fe7909-63eb-496f-a81c-bcff6f0456c5", null, null, "Administrator Access", "Admin_Access", "ADMIN_ACCESS" },
                    { "d6206540-4ba5-4dce-a608-37ba6523be27", null, null, "Consumption Event Delete", "ConsumptionEvent_Delete", "CONSUMPTIONEVENT_DELETE" },
                    { "a8a0c676-d58e-4be3-94db-ca7a5198692a", null, null, "Consumption Event Update", "ConsumptionEvent_Edit", "CONSUMPTIONEVENT_EDIT" },
                    { "a56ff8b3-479f-4d1c-aed3-b1b7ec5d6998", null, null, "Consumption Event Read", "ConsumptionEvent_Read", "CONSUMPTIONEVENT_READ" },
                    { "a08e13a5-00a8-4d7d-9aaf-c0d3a816e48b", null, null, "Item Read", "Item_Read", "ITEM_READ" },
                    { "6ce169eb-8cfc-49da-9306-15e41ef13562", null, null, "Item Delete", "Item_Delete", "ITEM_DELETE" },
                    { "9de4e55f-b26c-4b62-812a-cf52000b97bf", null, null, "Item Update", "Item_Edit", "ITEM_EDIT" },
                    { "faffc6d3-f72f-4b64-b208-3c7cfec71270", null, null, "Item Create", "Item_Create", "ITEM_CREATE" },
                    { "bcabe6d9-e245-40f8-ad4a-38f76ee73614", null, null, "Consumption Event Create", "ConsumptionEvent_Create", "CONSUMPTIONEVENT_CREATE" }
                });

            migrationBuilder.InsertData(
                table: "units",
                columns: new[] { "unit_id", "abbreviation", "active", "name", "sort_order" },
                values: new object[,]
                {
                    { 4, "set", true, "set", 4 },
                    { 1, "kg", true, "kilogram", 1 },
                    { 2, "m", true, "meter", 2 },
                    { 3, "piece", true, "piece", 3 }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "access_failed_count", "active", "concurrency_stamp", "email", "email_confirmed", "lockout_enabled", "lockout_end", "normalized_email", "normalized_user_name", "password_hash", "phone_number", "phone_number_confirmed", "security_stamp", "two_factor_enabled", "user_name" },
                values: new object[] { "20551684-b958-4581-af23-96c1528b0e29", 0, true, "2cebd9d0-694d-4ed3-8dc2-384f41557310", "bob@example.com", false, false, null, "BOB@EXAMPLE.COM", "BOB", "AQAAAAEAACcQAAAAEI4jEmRsUYzL6KnpR2/OjIPvkI9BWNmnnCZYah1GFvB2EOCWkgkk49YqCJBz38N8rg==", null, false, "3YILVFJYDKC4OK7QLLR4TO4KT6V4ZK5E", false, "Bob" });

            migrationBuilder.InsertData(
                table: "access_group_roles",
                columns: new[] { "access_group_id", "role_id" },
                values: new object[,]
                {
                    { 1, "faffc6d3-f72f-4b64-b208-3c7cfec71270" },
                    { 1, "a08e13a5-00a8-4d7d-9aaf-c0d3a816e48b" },
                    { 1, "9de4e55f-b26c-4b62-812a-cf52000b97bf" },
                    { 1, "6ce169eb-8cfc-49da-9306-15e41ef13562" },
                    { 1, "bcabe6d9-e245-40f8-ad4a-38f76ee73614" },
                    { 1, "a56ff8b3-479f-4d1c-aed3-b1b7ec5d6998" },
                    { 1, "a8a0c676-d58e-4be3-94db-ca7a5198692a" },
                    { 1, "d6206540-4ba5-4dce-a608-37ba6523be27" },
                    { 1, "d9fe7909-63eb-496f-a81c-bcff6f0456c5" },
                    { 1, "f6bb4564-6919-484b-897b-4a2c994721e5" }
                });

            migrationBuilder.InsertData(
                table: "user_access_groups",
                columns: new[] { "access_group_id", "user_id" },
                values: new object[] { 1, "20551684-b958-4581-af23-96c1528b0e29" });

            migrationBuilder.InsertData(
                table: "user_roles",
                columns: new[] { "user_id", "role_id" },
                values: new object[,]
                {
                    { "20551684-b958-4581-af23-96c1528b0e29", "faffc6d3-f72f-4b64-b208-3c7cfec71270" },
                    { "20551684-b958-4581-af23-96c1528b0e29", "a08e13a5-00a8-4d7d-9aaf-c0d3a816e48b" },
                    { "20551684-b958-4581-af23-96c1528b0e29", "9de4e55f-b26c-4b62-812a-cf52000b97bf" },
                    { "20551684-b958-4581-af23-96c1528b0e29", "6ce169eb-8cfc-49da-9306-15e41ef13562" },
                    { "20551684-b958-4581-af23-96c1528b0e29", "bcabe6d9-e245-40f8-ad4a-38f76ee73614" },
                    { "20551684-b958-4581-af23-96c1528b0e29", "a56ff8b3-479f-4d1c-aed3-b1b7ec5d6998" },
                    { "20551684-b958-4581-af23-96c1528b0e29", "a8a0c676-d58e-4be3-94db-ca7a5198692a" },
                    { "20551684-b958-4581-af23-96c1528b0e29", "d6206540-4ba5-4dce-a608-37ba6523be27" },
                    { "20551684-b958-4581-af23-96c1528b0e29", "d9fe7909-63eb-496f-a81c-bcff6f0456c5" },
                    { "20551684-b958-4581-af23-96c1528b0e29", "f6bb4564-6919-484b-897b-4a2c994721e5" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_access_group_roles_role_id",
                table: "access_group_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_consumption_events_consumed_by",
                table: "consumption_events",
                column: "consumed_by");

            migrationBuilder.CreateIndex(
                name: "ix_consumption_events_date_added",
                table: "consumption_events",
                column: "date_added");

            migrationBuilder.CreateIndex(
                name: "ix_consumption_events_inventory_item_id",
                table: "consumption_events",
                column: "inventory_item_id");

            migrationBuilder.CreateIndex(
                name: "ix_consumption_events_order_number",
                table: "consumption_events",
                column: "order_number");

            migrationBuilder.CreateIndex(
                name: "ix_currency_types_abbreviation",
                table: "currency_types",
                column: "abbreviation",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_currency_types_name",
                table: "currency_types",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_fabric_tests_inventory_item_id",
                table: "fabric_tests",
                column: "inventory_item_id");

            migrationBuilder.CreateIndex(
                name: "ix_inventory_categories_name",
                table: "inventory_categories",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_inventory_categories_sort_order",
                table: "inventory_categories",
                column: "sort_order",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_inventory_items_inventory_category_id",
                table: "inventory_items",
                column: "inventory_category_id");

            migrationBuilder.CreateIndex(
                name: "ix_inventory_items_storage_location_id",
                table: "inventory_items",
                column: "storage_location_id");

            migrationBuilder.CreateIndex(
                name: "ix_inventory_items_unit_id",
                table: "inventory_items",
                column: "unit_id");

            migrationBuilder.CreateIndex(
                name: "ix_role_claims_role_id",
                table: "role_claims",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "role_name_index",
                table: "roles",
                column: "normalized_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_units_abbreviation",
                table: "units",
                column: "abbreviation",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_units_name",
                table: "units",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_units_sort_order",
                table: "units",
                column: "sort_order",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_access_groups_user_id",
                table: "user_access_groups",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_claims_user_id",
                table: "user_claims",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_logins_user_id",
                table: "user_logins",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_roles_role_id",
                table: "user_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "email_index",
                table: "users",
                column: "normalized_email");

            migrationBuilder.CreateIndex(
                name: "user_name_index",
                table: "users",
                column: "normalized_user_name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "access_group_roles");

            migrationBuilder.DropTable(
                name: "consumption_events");

            migrationBuilder.DropTable(
                name: "currency_types");

            migrationBuilder.DropTable(
                name: "fabric_tests");

            migrationBuilder.DropTable(
                name: "role_claims");

            migrationBuilder.DropTable(
                name: "user_access_groups");

            migrationBuilder.DropTable(
                name: "user_claims");

            migrationBuilder.DropTable(
                name: "user_logins");

            migrationBuilder.DropTable(
                name: "user_roles");

            migrationBuilder.DropTable(
                name: "user_tokens");

            migrationBuilder.DropTable(
                name: "inventory_items");

            migrationBuilder.DropTable(
                name: "access_groups");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "inventory_categories");

            migrationBuilder.DropTable(
                name: "storage_locations");

            migrationBuilder.DropTable(
                name: "units");

            migrationBuilder.DropSequence(
                name: "access_groups_id_seq");

            migrationBuilder.DropSequence(
                name: "consumption_events_id_seq");

            migrationBuilder.DropSequence(
                name: "currency_types_id_seq");

            migrationBuilder.DropSequence(
                name: "fabric_tests_id_seq");

            migrationBuilder.DropSequence(
                name: "inventory_categories_id_seq");

            migrationBuilder.DropSequence(
                name: "items_id_seq");

            migrationBuilder.DropSequence(
                name: "storage_location_id_seq");

            migrationBuilder.DropSequence(
                name: "units_id_seq");
        }
    }
}
