﻿// <auto-generated />
using System;
using Egret.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Egret.Migrations
{
    [DbContext(typeof(EgretContext))]
    partial class EgretContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:PostgresExtension:adminpack", "'adminpack', '', ''")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.0-rtm-30799")
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("Relational:Sequence:.currencytypes_id_seq", "'currencytypes_id_seq', '', '100', '1', '', '', 'Int64', 'False'")
                .HasAnnotation("Relational:Sequence:.inventorycategories_id_seq", "'inventorycategories_id_seq', '', '100', '1', '', '', 'Int64', 'False'")
                .HasAnnotation("Relational:Sequence:.master_seq", "'master_seq', '', '1000', '1', '', '', 'Int64', 'False'")
                .HasAnnotation("Relational:Sequence:.units_id_seq", "'units_id_seq', '', '100', '1', '', '', 'Int64', 'False'");

            modelBuilder.Entity("Egret.DataAccess.Supplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique()
                        .HasName("ix_suppliers_pk");

                    b.ToTable("suppliers");
                });

            modelBuilder.Entity("Egret.Models.ConsumptionEvent", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasDefaultValueSql("nextval('master_seq'::regclass)");

                    b.Property<string>("ConsumedBy")
                        .HasColumnName("consumed_by");

                    b.Property<DateTime?>("DateOfConsumption")
                        .HasColumnName("date_of_consumption");

                    b.Property<string>("InventoryItemCode")
                        .HasColumnName("inventory_item_code");

                    b.Property<string>("PatternNumber")
                        .HasColumnName("pattern_number");

                    b.Property<string>("ProductionOrderNumber")
                        .HasColumnName("production_order_number");

                    b.Property<decimal?>("QuantityConsumed")
                        .HasColumnName("quantity_consumed");

                    b.Property<string>("SampleOrderNumber")
                        .HasColumnName("sample_order_number");

                    b.Property<string>("Unit")
                        .HasColumnName("unit");

                    b.Property<decimal?>("ValueConsumed")
                        .HasColumnName("value_consumed");

                    b.HasKey("Id")
                        .HasName("pk_consumptionevents_id");

                    b.HasIndex("Id")
                        .IsUnique()
                        .HasName("ix_consumptionevents_id");

                    b.HasIndex("InventoryItemCode");

                    b.HasIndex("Unit");

                    b.ToTable("consumption_events");
                });

            modelBuilder.Entity("Egret.Models.CurrencyType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasDefaultValueSql("nextval('currencytypes_id_seq'::regclass)");

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasColumnName("abbreviation");

                    b.Property<bool>("Active")
                        .HasColumnName("active");

                    b.Property<bool>("DefaultSelection")
                        .HasColumnName("defaultselection");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<int>("SortOrder")
                        .HasColumnName("sortorder");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnName("symbol");

                    b.HasKey("Id")
                        .HasName("pk_currencytypes_id");

                    b.HasAlternateKey("Abbreviation")
                        .HasName("uk_currencytypes_abbreviation");

                    b.HasIndex("Abbreviation")
                        .IsUnique()
                        .HasName("ix_currencytypes_abbreviation");

                    b.HasIndex("Id")
                        .IsUnique()
                        .HasName("ix_currencytypes_id");

                    b.ToTable("currency_types");

                    b.HasData(
                        new { Id = 1, Abbreviation = "USD", Active = true, DefaultSelection = false, Name = "United States Dollars", SortOrder = 1, Symbol = "$" },
                        new { Id = 2, Abbreviation = "NRP", Active = true, DefaultSelection = true, Name = "Nepali Rupees", SortOrder = 2, Symbol = "रु" },
                        new { Id = 3, Abbreviation = "INR", Active = true, DefaultSelection = false, Name = "Indian Rupees", SortOrder = 3, Symbol = "₹" }
                    );
                });

            modelBuilder.Entity("Egret.Models.FabricTest", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasDefaultValueSql("nextval('master_seq'::regclass)");

                    b.Property<string>("InventoryItemCode")
                        .HasColumnName("inventory_item_code");

                    b.Property<string>("Name")
                        .HasColumnName("name");

                    b.Property<string>("Result")
                        .HasColumnName("result");

                    b.HasKey("Id")
                        .HasName("pk_fabrictests_id");

                    b.HasIndex("Id")
                        .IsUnique()
                        .HasName("ix_fabrictests_id");

                    b.HasIndex("InventoryItemCode");

                    b.ToTable("fabric_tests");
                });

            modelBuilder.Entity("Egret.Models.InventoryCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasDefaultValueSql("nextval('inventorycategories_id_seq'::regclass)");

                    b.Property<bool>("Active")
                        .HasColumnName("active");

                    b.Property<string>("Description")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<int>("SortOrder")
                        .HasColumnName("sortorder");

                    b.HasKey("Id")
                        .HasName("pk_inventorycategories_id");

                    b.HasAlternateKey("Name")
                        .HasName("uk_inventorycategories_name");

                    b.HasIndex("Id")
                        .HasName("ix_inventorycategories_id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasName("ix_inventorycategories_name");

                    b.ToTable("inventory_categories");

                    b.HasData(
                        new { Id = 1, Active = true, Description = "", Name = "Elastic", SortOrder = 1 },
                        new { Id = 2, Active = true, Description = "", Name = "Fastener", SortOrder = 2 },
                        new { Id = 3, Active = true, Description = "", Name = "Knit", SortOrder = 3 },
                        new { Id = 4, Active = true, Description = "", Name = "Labels and Tags", SortOrder = 4 },
                        new { Id = 5, Active = true, Description = "", Name = "Leather", SortOrder = 5 },
                        new { Id = 6, Active = true, Description = "", Name = "Other", SortOrder = 6 },
                        new { Id = 7, Active = true, Description = "", Name = "Thread", SortOrder = 7 },
                        new { Id = 8, Active = true, Description = "", Name = "Woven", SortOrder = 8 },
                        new { Id = 9, Active = true, Description = "", Name = "Zipper", SortOrder = 9 }
                    );
                });

            modelBuilder.Entity("Egret.Models.InventoryItem", b =>
                {
                    b.Property<string>("Code")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("code")
                        .HasDefaultValueSql("nextval('master_seq'::regclass)");

                    b.Property<string>("AddedBy")
                        .HasColumnName("user_added_by");

                    b.Property<string>("ApproxProdQty")
                        .HasColumnName("approx_prod_qty");

                    b.Property<bool>("BondedWarehouse")
                        .HasColumnName("bonded_warehouse");

                    b.Property<string>("BuyUnit")
                        .HasColumnName("buy_unit");

                    b.Property<string>("Buycurrency")
                        .HasColumnName("buy_currency");

                    b.Property<decimal?>("Buyprice")
                        .HasColumnName("buy_price");

                    b.Property<string>("Category")
                        .HasColumnName("category");

                    b.Property<string>("Comment")
                        .HasColumnName("comment");

                    b.Property<string>("ConversionSource")
                        .HasColumnName("conversion_source");

                    b.Property<string>("CustomerPurchasedFor")
                        .HasColumnName("customer_purchased_for");

                    b.Property<string>("CustomerReservedFor")
                        .HasColumnName("customer_reserved_for");

                    b.Property<DateTime?>("DateAdded")
                        .HasColumnName("date_added");

                    b.Property<DateTime?>("DateArrived")
                        .HasColumnName("date_arrived");

                    b.Property<DateTime?>("DateConfirmed")
                        .HasColumnName("date_confirmed");

                    b.Property<DateTime?>("DateShipped")
                        .HasColumnName("date_shipped");

                    b.Property<DateTime?>("DateUpdated")
                        .HasColumnName("date_updated");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("description");

                    b.Property<decimal?>("FOBCost")
                        .HasColumnName("fob_cost");

                    b.Property<string>("FabricTestResults")
                        .HasColumnName("fabrictestresults");

                    b.Property<string>("FabricTests_Conversion")
                        .HasColumnName("fabrictests_conversion");

                    b.Property<decimal?>("ImportCosts")
                        .HasColumnName("import_cost");

                    b.Property<bool>("IsConversion")
                        .HasColumnName("is_conversion");

                    b.Property<DateTime?>("NeededBefore")
                        .HasColumnName("needed_before");

                    b.Property<decimal?>("QtyPurchased")
                        .HasColumnName("qty+purchased");

                    b.Property<string>("QtyToPurchaseNow")
                        .HasColumnName("qty_to_purchase_now");

                    b.Property<string>("SellUnit");

                    b.Property<string>("ShippingCompany")
                        .HasColumnName("shipping_company");

                    b.Property<decimal?>("ShippingCost")
                        .HasColumnName("shipping_cost");

                    b.Property<string>("Supplier")
                        .HasColumnName("supplier");

                    b.Property<string>("TargetPrice")
                        .HasColumnName("target_price");

                    b.Property<string>("Unit")
                        .HasColumnName("unit");

                    b.Property<string>("UnitNavigationAbbreviation");

                    b.Property<string>("UpdatedBy")
                        .HasColumnName("user_updated_by");

                    b.HasKey("Code")
                        .HasName("pk_inventoryitems_id");

                    b.HasIndex("BuyUnit");

                    b.HasIndex("Buycurrency");

                    b.HasIndex("Category");

                    b.HasIndex("Code")
                        .IsUnique()
                        .HasName("ix_inventoryitems_description");

                    b.HasIndex("UnitNavigationAbbreviation");

                    b.ToTable("inventory_items");
                });

            modelBuilder.Entity("Egret.Models.Unit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasDefaultValueSql("nextval('units_id_seq'::regclass)");

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasColumnName("abbreviation");

                    b.Property<bool>("Active")
                        .HasColumnName("active");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<int>("SortOrder")
                        .HasColumnName("sortorder");

                    b.HasKey("Id")
                        .HasName("pk_units_id");

                    b.HasAlternateKey("Abbreviation")
                        .HasName("uk_units_abbreviation");

                    b.HasIndex("Abbreviation")
                        .IsUnique()
                        .HasName("ix_units_abbreviation");

                    b.HasIndex("SortOrder")
                        .IsUnique()
                        .HasName("ix_units_sortorder");

                    b.ToTable("units");

                    b.HasData(
                        new { Id = 1, Abbreviation = "kg", Active = true, Name = "kilogram", SortOrder = 1 },
                        new { Id = 2, Abbreviation = "meter", Active = true, Name = "meter", SortOrder = 2 },
                        new { Id = 3, Abbreviation = "piece", Active = true, Name = "piece", SortOrder = 3 },
                        new { Id = 4, Abbreviation = "set", Active = true, Name = "set", SortOrder = 4 }
                    );
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasMaxLength(450);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnName("concurrency_stamp");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnName("normalized_name")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("aspnet_roles");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityRole");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("aspnet_roleclaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(450);

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.ToTable("aspnet_users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("aspnet_userclaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("aspnet_userlogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId")
                        .IsUnique()
                        .HasName("ix_aspnet_userroles_roleid");

                    b.ToTable("aspnet_userroles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("aspnet_usertokens");
                });

            modelBuilder.Entity("Egret.Models.Role", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityRole");


                    b.ToTable("aspnet_roles");

                    b.HasDiscriminator().HasValue("Role");
                });

            modelBuilder.Entity("Egret.Models.User", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<bool>("IsActive");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("aspnet_users");

                    b.HasDiscriminator().HasValue("User");

                    b.HasData(
                        new { Id = "0", AccessFailedCount = 0, ConcurrencyStamp = "2cebd9d0-694d-4ed3-8dc2-384f41557310", Email = "bob@example.com", EmailConfirmed = false, LockoutEnabled = false, NormalizedEmail = "BOB@EXAMPLE.COM", NormalizedUserName = "BOB", PasswordHash = "AQAAAAEAACcQAAAAEI4jEmRsUYzL6KnpR2/OjIPvkI9BWNmnnCZYah1GFvB2EOCWkgkk49YqCJBz38N8rg==", PhoneNumberConfirmed = false, SecurityStamp = "3YILVFJYDKC4OK7QLLR4TO4KT6V4ZK5E", TwoFactorEnabled = false, UserName = "Bob", IsActive = true }
                    );
                });

            modelBuilder.Entity("Egret.Models.ConsumptionEvent", b =>
                {
                    b.HasOne("Egret.Models.InventoryItem", "InventoryItemNavigation")
                        .WithMany("ConsumptionEventsNavigation")
                        .HasForeignKey("InventoryItemCode")
                        .HasConstraintName("fk_consumptionevents_inventory_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Egret.Models.Unit", "UnitNavigation")
                        .WithMany()
                        .HasForeignKey("Unit")
                        .HasConstraintName("fk_consumptionevents_units")
                        .HasPrincipalKey("Abbreviation")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Egret.Models.FabricTest", b =>
                {
                    b.HasOne("Egret.Models.InventoryItem", "InventoryItem")
                        .WithMany("FabricTestsNavigation")
                        .HasForeignKey("InventoryItemCode")
                        .HasConstraintName("fk_inventoryitems_fabrictests")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Egret.Models.InventoryItem", b =>
                {
                    b.HasOne("Egret.Models.Unit", "BuyUnitNavigation")
                        .WithMany()
                        .HasForeignKey("BuyUnit")
                        .HasConstraintName("fk_inventoryitems_buyunit")
                        .HasPrincipalKey("Abbreviation")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Egret.Models.CurrencyType", "BuycurrencyNavigation")
                        .WithMany()
                        .HasForeignKey("Buycurrency")
                        .HasConstraintName("fk_inventoryitems_buycurrency")
                        .HasPrincipalKey("Abbreviation")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Egret.Models.InventoryCategory", "CategoryNavigation")
                        .WithMany()
                        .HasForeignKey("Category")
                        .HasConstraintName("fk_inventoryitems_category")
                        .HasPrincipalKey("Name")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Egret.Models.Unit", "UnitNavigation")
                        .WithMany()
                        .HasForeignKey("UnitNavigationAbbreviation")
                        .HasConstraintName("fk_inventoryitems_unit")
                        .HasPrincipalKey("Abbreviation");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Egret.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Egret.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Egret.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Egret.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
