using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Egret.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.EnsureSchema(
                name: "admin");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:adminpack", "'adminpack', '', ''");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(450)", maxLength: 450, nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    Discriminator = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(450)", maxLength: 450, nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int4", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bool", nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "bool", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamptz", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bool", nullable: false),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bool", nullable: false),
                    UserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            //migrationBuilder.CreateTable(
            //    name: "currency_types",
            //    schema: "admin",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(type: "int4", nullable: false),
            //        abbreviation = table.Column<string>(type: "text", nullable: false),
            //        active = table.Column<bool>(type: "bool", nullable: false),
            //        defaultselection = table.Column<bool>(type: "bool", nullable: false),
            //        name = table.Column<string>(type: "text", nullable: false),
            //        sortorder = table.Column<int>(type: "int4", nullable: false, defaultValueSql: "nextval('admin.currency_types_sortorder_seq'::regclass)"),
            //        symbol = table.Column<string>(type: "text", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_currency_types", x => x.id);
            //        table.UniqueConstraint("AK_currency_types_abbreviation", x => x.abbreviation);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "inventory_categories",
            //    schema: "admin",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(type: "int4", nullable: false, defaultValueSql: "nextval('admin.inventory_categories_id_seq'::regclass)"),
            //        active = table.Column<bool>(type: "bool", nullable: false),
            //        description = table.Column<string>(type: "text", nullable: true),
            //        name = table.Column<string>(type: "text", nullable: false),
            //        sortorder = table.Column<int>(type: "int4", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_inventory_categories", x => x.id);
            //        table.UniqueConstraint("AK_inventory_categories_name", x => x.name);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "units",
            //    schema: "admin",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(type: "int4", nullable: false, defaultValueSql: "nextval('admin.units_id_seq'::regclass)"),
            //        abbreviation = table.Column<string>(type: "text", nullable: false),
            //        active = table.Column<bool>(type: "bool", nullable: false),
            //        name = table.Column<string>(type: "text", nullable: false),
            //        sortorder = table.Column<int>(type: "int4", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_units", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "suppliers",
            //    schema: "public",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(type: "int4", nullable: false),
            //        name = table.Column<string>(type: "text", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_suppliers", x => x.id);
            //    });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true),
                    RoleId = table.Column<string>(type: "varchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "varchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "varchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "varchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            //migrationBuilder.CreateTable(
            //    name: "inventory_items",
            //    columns: table => new
            //    {
            //        code = table.Column<string>(type: "text", nullable: false, defaultValueSql: "nextval('master_seq'::regclass)"),
            //        useraddedby = table.Column<string>(type: "text", nullable: true),
            //        buycurrency = table.Column<string>(type: "text", nullable: true),
            //        buyprice = table.Column<double>(type: "float8", nullable: true),
            //        buyunit_fk = table.Column<int>(type: "int4", nullable: true),
            //        category = table.Column<string>(type: "text", nullable: true),
            //        cogacct = table.Column<int>(type: "int4", nullable: true),
            //        comment = table.Column<string>(type: "text", nullable: true),
            //        conversionsource = table.Column<string>(type: "text", nullable: true),
            //        costprice = table.Column<double>(type: "float8", nullable: true),
            //        dateadded = table.Column<DateTime>(type: "timestamp", nullable: true),
            //        dateupdated = table.Column<DateTime>(type: "timestamp", nullable: true),
            //        description = table.Column<string>(type: "text", nullable: false),
            //        flags = table.Column<int>(type: "int4", nullable: true),
            //        isconversion = table.Column<bool>(type: "bool", nullable: true),
            //        qtybrksellprice = table.Column<double>(type: "float8", nullable: true),
            //        salesacct = table.Column<int>(type: "int4", nullable: true),
            //        sellcurrency = table.Column<string>(type: "text", nullable: true),
            //        sellprice = table.Column<double>(type: "float8", nullable: true),
            //        sellunit_fk = table.Column<int>(type: "int4", nullable: true),
            //        sohcount = table.Column<double>(type: "float8", nullable: true),
            //        stockacct = table.Column<int>(type: "int4", nullable: true),
            //        stocktakenewqty = table.Column<double>(type: "float8", nullable: true),
            //        stockvalue = table.Column<double>(type: "float8", nullable: true),
            //        supplier_fk = table.Column<int>(type: "int4", nullable: true),
            //        userupdatedby = table.Column<string>(type: "text", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_inventory_items", x => x.code);
            //        table.ForeignKey(
            //            name: "inventory_items_buycurrency_fk",
            //            column: x => x.buycurrency,
            //            principalSchema: "admin",
            //            principalTable: "currency_types",
            //            principalColumn: "abbreviation",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "inventory_items_buyunit_fk",
            //            column: x => x.buyunit_fk,
            //            principalSchema: "admin",
            //            principalTable: "units",
            //            principalColumn: "id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "inventory_items_category_fk",
            //            column: x => x.category,
            //            principalSchema: "admin",
            //            principalTable: "inventory_categories",
            //            principalColumn: "name",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "inventory_items_sellcurrency_fk",
            //            column: x => x.sellcurrency,
            //            principalSchema: "admin",
            //            principalTable: "currency_types",
            //            principalColumn: "abbreviation",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "inventory_items_sellunit_fk",
            //            column: x => x.sellunit_fk,
            //            principalSchema: "admin",
            //            principalTable: "units",
            //            principalColumn: "id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

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
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_inventory_items_buycurrency",
            //    table: "inventory_items",
            //    column: "buycurrency");

            //migrationBuilder.CreateIndex(
            //    name: "IX_inventory_items_buyunit_fk",
            //    table: "inventory_items",
            //    column: "buyunit_fk");

            //migrationBuilder.CreateIndex(
            //    name: "IX_inventory_items_category",
            //    table: "inventory_items",
            //    column: "category");

            //migrationBuilder.CreateIndex(
            //    name: "IX_inventory_items_sellcurrency",
            //    table: "inventory_items",
            //    column: "sellcurrency");

            //migrationBuilder.CreateIndex(
            //    name: "IX_inventory_items_sellunit_fk",
            //    table: "inventory_items",
            //    column: "sellunit_fk");

            //migrationBuilder.CreateIndex(
            //    name: "currency_types_abbreviation_key",
            //    schema: "admin",
            //    table: "currency_types",
            //    column: "abbreviation",
            //    unique: true);
            //
            //migrationBuilder.CreateIndex(
            //    name: "currency_types_sort_key",
            //    schema: "admin",
            //    table: "currency_types",
            //    column: "sortorder",
            //    unique: true);
            //
            //migrationBuilder.CreateIndex(
            //    name: "inventory_categories_name_key",
            //    schema: "admin",
            //    table: "inventory_categories",
            //    column: "name",
            //    unique: true);
            //
            //migrationBuilder.CreateIndex(
            //    name: "inventory_categories_sort_key",
            //    schema: "admin",
            //    table: "inventory_categories",
            //    column: "sortorder",
            //    unique: true);
            //
            //migrationBuilder.CreateIndex(
            //    name: "units_abbreviation_key",
            //    schema: "admin",
            //    table: "units",
            //    column: "abbreviation",
            //    unique: true);
            //
            //migrationBuilder.CreateIndex(
            //    name: "units_sort_key",
            //    schema: "admin",
            //    table: "units",
            //    column: "sortorder",
            //    unique: true);
            //
            //migrationBuilder.CreateIndex(
            //    name: "suppliers_pkey",
            //    schema: "public",
            //    table: "suppliers",
            //    column: "id",
            //    unique: true);
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

            //migrationBuilder.DropTable(
            //    name: "inventory_items");

            //migrationBuilder.DropTable(
            //    name: "suppliers",
            //    schema: "public");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            //migrationBuilder.DropTable(
            //    name: "currency_types",
            //    schema: "admin");

            //migrationBuilder.DropTable(
            //    name: "units",
            //    schema: "admin");

            //migrationBuilder.DropTable(
            //    name: "inventory_categories",
            //    schema: "admin");
        }
    }
}
