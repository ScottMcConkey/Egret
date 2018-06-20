using Microsoft.EntityFrameworkCore.Migrations;

namespace Egret.Migrations
{
    public partial class DatabaseScrub : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_consumption_events_inventory_items_InventoryItemNavigationC~",
                table: "consumption_events");

            migrationBuilder.DropForeignKey(
                name: "FK_consumption_events_units_UnitId",
                table: "consumption_events");

            migrationBuilder.DropForeignKey(
                name: "FK_fabric_tests_inventory_items_InventoryItemCode",
                table: "fabric_tests");

            migrationBuilder.DropForeignKey(
                name: "inventoryitems_buycurrency_fk",
                table: "inventory_items");

            migrationBuilder.DropForeignKey(
                name: "inventoryitems_buyunit_fk",
                table: "inventory_items");

            migrationBuilder.DropForeignKey(
                name: "inventoryitems_category_fk",
                table: "inventory_items");

            migrationBuilder.DropForeignKey(
                name: "inventoryitems_sellcurrency_fk",
                table: "inventory_items");

            migrationBuilder.DropForeignKey(
                name: "inventoryitems_sellunit_fk",
                table: "inventory_items");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_units_abbreviation",
                table: "units");

            migrationBuilder.DropPrimaryKey(
                name: "PK_units",
                table: "units");

            migrationBuilder.DropPrimaryKey(
                name: "PK_inventory_items",
                table: "inventory_items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_inventory_categories",
                table: "inventory_categories");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_inventory_categories_name",
                table: "inventory_categories");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_currency_types_abbreviation",
                table: "currency_types");

            migrationBuilder.DropPrimaryKey(
                name: "PK_currency_types",
                table: "currency_types");

            migrationBuilder.DropIndex(
                name: "ix_currencytypes_sortorder",
                table: "currency_types");

            migrationBuilder.DropPrimaryKey(
                name: "PK_consumption_events",
                table: "consumption_events");

            migrationBuilder.DropIndex(
                name: "IX_consumption_events_UnitId",
                table: "consumption_events");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "consumption_events");

            migrationBuilder.RenameColumn(
                name: "supplier_fk",
                table: "inventory_items",
                newName: "supplier");

            migrationBuilder.RenameColumn(
                name: "sellunit_fk",
                table: "inventory_items",
                newName: "sellunit");

            migrationBuilder.RenameIndex(
                name: "IX_inventory_items_sellunit_fk",
                table: "inventory_items",
                newName: "IX_inventory_items_sellunit");

            migrationBuilder.RenameIndex(
                name: "ix_fabrictests_pk",
                table: "fabric_tests",
                newName: "ix_fabrictests_id");

            migrationBuilder.RenameColumn(
                name: "unit_id",
                table: "consumption_events",
                newName: "unit");

            migrationBuilder.RenameColumn(
                name: "InventoryItemNavigationCode",
                table: "consumption_events",
                newName: "UnitNavigationAbbreviation");

            migrationBuilder.RenameColumn(
                name: "InventoryItem",
                table: "consumption_events",
                newName: "InventoryItemCode");

            migrationBuilder.RenameIndex(
                name: "IX_consumption_events_InventoryItemNavigationCode",
                table: "consumption_events",
                newName: "IX_consumption_events_UnitNavigationAbbreviation");

            migrationBuilder.RenameIndex(
                name: "id",
                table: "consumption_events",
                newName: "ix_consumptionevents_id");

            migrationBuilder.AddUniqueConstraint(
                name: "uk_units_abbreviation",
                table: "units",
                column: "abbreviation");

            migrationBuilder.AddPrimaryKey(
                name: "pk_units_id",
                table: "units",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_inventoryitems",
                table: "inventory_items",
                column: "code");

            migrationBuilder.AddPrimaryKey(
                name: "pk_inventorycategories_id",
                table: "inventory_categories",
                column: "id");

            migrationBuilder.AddUniqueConstraint(
                name: "uk_inventorycategories_name",
                table: "inventory_categories",
                column: "name");

            migrationBuilder.AddUniqueConstraint(
                name: "uk_currencytypes_abbreviation",
                table: "currency_types",
                column: "abbreviation");

            migrationBuilder.AddPrimaryKey(
                name: "pk_currencytypes_id",
                table: "currency_types",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_consumptionevents_id",
                table: "consumption_events",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_inventoryitems_description",
                table: "inventory_items",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_inventorycategories_id",
                table: "inventory_categories",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_currencytypes_id",
                table: "currency_types",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_consumption_events_InventoryItemCode",
                table: "consumption_events",
                column: "InventoryItemCode");

            migrationBuilder.AddForeignKey(
                name: "fk_inventoryitems_consumptionevents",
                table: "consumption_events",
                column: "InventoryItemCode",
                principalTable: "inventory_items",
                principalColumn: "code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_consumption_events_units_UnitNavigationAbbreviation",
                table: "consumption_events",
                column: "UnitNavigationAbbreviation",
                principalTable: "units",
                principalColumn: "abbreviation",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_inventoryitems_fabrictests",
                table: "fabric_tests",
                column: "InventoryItemCode",
                principalTable: "inventory_items",
                principalColumn: "code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_inventoryitems_buyunit",
                table: "inventory_items",
                column: "buyunit_fk",
                principalTable: "units",
                principalColumn: "abbreviation",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_inventoryitems_buycurrency",
                table: "inventory_items",
                column: "buycurrency",
                principalTable: "currency_types",
                principalColumn: "abbreviation",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_inventoryitems_category",
                table: "inventory_items",
                column: "category",
                principalTable: "inventory_categories",
                principalColumn: "name",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_inventoryitems_sellunit",
                table: "inventory_items",
                column: "sellunit",
                principalTable: "units",
                principalColumn: "abbreviation",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_inventoryitems_sellcurrency",
                table: "inventory_items",
                column: "sellcurrency",
                principalTable: "currency_types",
                principalColumn: "abbreviation",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_inventoryitems_consumptionevents",
                table: "consumption_events");

            migrationBuilder.DropForeignKey(
                name: "FK_consumption_events_units_UnitNavigationAbbreviation",
                table: "consumption_events");

            migrationBuilder.DropForeignKey(
                name: "fk_inventoryitems_fabrictests",
                table: "fabric_tests");

            migrationBuilder.DropForeignKey(
                name: "fk_inventoryitems_buyunit",
                table: "inventory_items");

            migrationBuilder.DropForeignKey(
                name: "fk_inventoryitems_buycurrency",
                table: "inventory_items");

            migrationBuilder.DropForeignKey(
                name: "fk_inventoryitems_category",
                table: "inventory_items");

            migrationBuilder.DropForeignKey(
                name: "fk_inventoryitems_sellunit",
                table: "inventory_items");

            migrationBuilder.DropForeignKey(
                name: "fk_inventoryitems_sellcurrency",
                table: "inventory_items");

            migrationBuilder.DropUniqueConstraint(
                name: "uk_units_abbreviation",
                table: "units");

            migrationBuilder.DropPrimaryKey(
                name: "pk_units_id",
                table: "units");

            migrationBuilder.DropPrimaryKey(
                name: "pk_inventoryitems",
                table: "inventory_items");

            migrationBuilder.DropIndex(
                name: "ix_inventoryitems_description",
                table: "inventory_items");

            migrationBuilder.DropPrimaryKey(
                name: "pk_inventorycategories_id",
                table: "inventory_categories");

            migrationBuilder.DropUniqueConstraint(
                name: "uk_inventorycategories_name",
                table: "inventory_categories");

            migrationBuilder.DropIndex(
                name: "ix_inventorycategories_id",
                table: "inventory_categories");

            migrationBuilder.DropUniqueConstraint(
                name: "uk_currencytypes_abbreviation",
                table: "currency_types");

            migrationBuilder.DropPrimaryKey(
                name: "pk_currencytypes_id",
                table: "currency_types");

            migrationBuilder.DropIndex(
                name: "ix_currencytypes_id",
                table: "currency_types");

            migrationBuilder.DropPrimaryKey(
                name: "pk_consumptionevents_id",
                table: "consumption_events");

            migrationBuilder.DropIndex(
                name: "IX_consumption_events_InventoryItemCode",
                table: "consumption_events");

            migrationBuilder.RenameColumn(
                name: "supplier",
                table: "inventory_items",
                newName: "supplier_fk");

            migrationBuilder.RenameColumn(
                name: "sellunit",
                table: "inventory_items",
                newName: "sellunit_fk");

            migrationBuilder.RenameIndex(
                name: "IX_inventory_items_sellunit",
                table: "inventory_items",
                newName: "IX_inventory_items_sellunit_fk");

            migrationBuilder.RenameIndex(
                name: "ix_fabrictests_id",
                table: "fabric_tests",
                newName: "ix_fabrictests_pk");

            migrationBuilder.RenameColumn(
                name: "unit",
                table: "consumption_events",
                newName: "unit_id");

            migrationBuilder.RenameColumn(
                name: "UnitNavigationAbbreviation",
                table: "consumption_events",
                newName: "InventoryItemNavigationCode");

            migrationBuilder.RenameColumn(
                name: "InventoryItemCode",
                table: "consumption_events",
                newName: "InventoryItem");

            migrationBuilder.RenameIndex(
                name: "IX_consumption_events_UnitNavigationAbbreviation",
                table: "consumption_events",
                newName: "IX_consumption_events_InventoryItemNavigationCode");

            migrationBuilder.RenameIndex(
                name: "ix_consumptionevents_id",
                table: "consumption_events",
                newName: "id");

            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "consumption_events",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_units_abbreviation",
                table: "units",
                column: "abbreviation");

            migrationBuilder.AddPrimaryKey(
                name: "PK_units",
                table: "units",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_inventory_items",
                table: "inventory_items",
                column: "code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_inventory_categories",
                table: "inventory_categories",
                column: "id");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_inventory_categories_name",
                table: "inventory_categories",
                column: "name");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_currency_types_abbreviation",
                table: "currency_types",
                column: "abbreviation");

            migrationBuilder.AddPrimaryKey(
                name: "PK_currency_types",
                table: "currency_types",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_consumption_events",
                table: "consumption_events",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_currencytypes_sortorder",
                table: "currency_types",
                column: "sortorder",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_consumption_events_UnitId",
                table: "consumption_events",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_consumption_events_inventory_items_InventoryItemNavigationC~",
                table: "consumption_events",
                column: "InventoryItemNavigationCode",
                principalTable: "inventory_items",
                principalColumn: "code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_consumption_events_units_UnitId",
                table: "consumption_events",
                column: "UnitId",
                principalTable: "units",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_fabric_tests_inventory_items_InventoryItemCode",
                table: "fabric_tests",
                column: "InventoryItemCode",
                principalTable: "inventory_items",
                principalColumn: "code",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "inventoryitems_buycurrency_fk",
                table: "inventory_items",
                column: "buycurrency",
                principalTable: "currency_types",
                principalColumn: "abbreviation",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "inventoryitems_buyunit_fk",
                table: "inventory_items",
                column: "buyunit_fk",
                principalTable: "units",
                principalColumn: "abbreviation",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "inventoryitems_category_fk",
                table: "inventory_items",
                column: "category",
                principalTable: "inventory_categories",
                principalColumn: "name",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "inventoryitems_sellcurrency_fk",
                table: "inventory_items",
                column: "sellcurrency",
                principalTable: "currency_types",
                principalColumn: "abbreviation",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "inventoryitems_sellunit_fk",
                table: "inventory_items",
                column: "sellunit_fk",
                principalTable: "units",
                principalColumn: "abbreviation",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
