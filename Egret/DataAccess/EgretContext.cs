using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Egret.Models;

namespace Egret.DataAccess
{
    /// <summary>
    /// This class is the DbContext used throughout the project for accessing database stores with Entity Framework.
    /// </summary>
    public partial class EgretContext : IdentityDbContext<User>
    {
        public EgretContext(DbContextOptions<EgretContext> options) 
            : base(options) {}

        public virtual DbSet<CurrencyType> CurrencyTypes { get; set; }
        public virtual DbSet<InventoryCategory> InventoryCategories { get; set; }
        public virtual DbSet<InventoryItem> InventoryItems { get; set; }
        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<FabricTest> FabricTests { get; set; }
        public virtual DbSet<ConsumptionEvent> ConsumptionEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // AspNet Identity
            modelBuilder.Entity<IdentityUser>(entity =>
            {
                //entity.ToTable("aspnet_users");

                entity.Property(p => p.Id)
                    .HasColumnName("id")
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("aspnet_users");

                entity.Property(p => p.Id)
                    .HasColumnName("Id")
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable("aspnet_roles");

                entity.Property(p => p.Id)
                    .HasColumnName("id")
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("aspnet_roles");

                entity.Property(u => u.Id)
                    .HasColumnName("id")
                    .HasMaxLength(450);

                entity.Property(p => p.Name)
                    .HasColumnName("name");

                entity.Property(p => p.NormalizedName)
                    .HasColumnName("normalized_name");

                entity.Property(p => p.ConcurrencyStamp)
                    .HasColumnName("concurrency_stamp");
            });

            // Give all the Identity tables custom names that won't be generated
            // as string literals.
            modelBuilder.Entity<IdentityUserRole<string>>()
                .ToTable("aspnet_userroles")
                .HasIndex(u => u.RoleId)
                .HasName("ix_aspnet_userroles_roleid")
                .IsUnique();
            modelBuilder.Entity<IdentityUserClaim<string>>()
                .ToTable("aspnet_userclaims")
                ;//.HasIndex("ix_aspnet_userclaims_userid");
            modelBuilder.Entity<IdentityUserLogin<string>>()
                .ToTable("aspnet_userlogins")
                ;//.HasIndex("ix_aspnet_userlogins_userid");
            modelBuilder.Entity<IdentityRoleClaim<string>>()
                .ToTable("aspnet_roleclaims")
                ;//.HasIndex("ix_aspnet_roleclaims_roleid");
            modelBuilder.Entity<IdentityUserToken<string>>()
                .ToTable("aspnet_usertokens");

            // Sequences
            modelBuilder.HasSequence<long>("master_seq")
                .StartsAt(1000);
            modelBuilder.HasSequence<long>("inventorycategories_id_seq")
                .StartsAt(100);
            modelBuilder.HasSequence<long>("units_id_seq")
                .StartsAt(100);
            modelBuilder.HasSequence<long>("currencytypes_id_seq")
                .StartsAt(100);

            // PostgreSQL-specific
            modelBuilder.HasPostgresExtension("adminpack");

            // Entities
            modelBuilder.Entity<CurrencyType>(entity =>
            {
                // Table
                entity.ToTable("currency_types");

                // Indexes
                entity.HasIndex(e => e.Id)
                    .HasName("ix_currencytypes_id")
                    .IsUnique();

                entity.HasIndex(e => e.Abbreviation)
                    .HasName("ix_currencytypes_abbreviation")
                    .IsUnique();

                // Keys
                entity.HasKey(k => k.Id)
                    .HasName("pk_currencytypes_id");

                entity.HasAlternateKey(k => k.Abbreviation)
                    .HasName("uk_currencytypes_abbreviation");

                // Properties
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('currencytypes_id_seq'::regclass)");

                entity.Property(e => e.Abbreviation)
                    .IsRequired()
                    .HasColumnName("abbreviation");

                entity.Property(e => e.Active)
                    .HasColumnName("active");

                entity.Property(e => e.DefaultSelection)
                    .HasColumnName("defaultselection");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.SortOrder)
                    .HasColumnName("sortorder");

                entity.Property(e => e.Symbol)
                    .IsRequired()
                    .HasColumnName("symbol");
            });

            modelBuilder.Entity<InventoryCategory>(entity =>
            {
                // Table
                entity.ToTable("inventory_categories");

                // Indexes
                entity.HasIndex(i => i.Id)
                    .HasName("ix_inventorycategories_id");

                entity.HasIndex(i => i.Name)
                    .HasName("ix_inventorycategories_name")
                    .IsUnique();

                // Keys
                entity.HasKey(k => k.Id)
                    .HasName("pk_inventorycategories_id");

                entity.HasAlternateKey(k => k.Name)
                    .HasName("uk_inventorycategories_name");

                // Properties
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('inventorycategories_id_seq'::regclass)");

                entity.Property(e => e.Active)
                    .HasColumnName("active");

                entity.Property(e => e.Description)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.SortOrder)
                    .HasColumnName("sortorder");
            });

            modelBuilder.Entity<Unit>(entity =>
            {
                // Table
                entity.ToTable("units");

                // Indexes
                entity.HasIndex(e => e.SortOrder)
                    .HasName("ix_units_sortorder")
                    .IsUnique();

                entity.HasIndex(e => e.Abbreviation)
                    .HasName("ix_units_abbreviation")
                    .IsUnique();

                // Keys
                entity.HasKey(e => e.Id)
                    .HasName("pk_units_id");

                entity.HasAlternateKey(e => e.Abbreviation)
                    .HasName("uk_units_abbreviation");

                // Properties
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('units_id_seq'::regclass)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.Abbreviation)
                    .IsRequired()
                    .HasColumnName("abbreviation");

                entity.Property(e => e.SortOrder)
                    .HasColumnName("sortorder");

                entity.Property(e => e.Active)
                    .HasColumnName("active");

            });

            modelBuilder.Entity<InventoryItem>(entity =>
            {
                // Table
                entity.ToTable("inventory_items");

                // Indexes
                entity.HasIndex(i => i.Code)
                    .HasName("ix_inventoryitems_code")
                    .IsUnique();

                entity.HasIndex(i => i.Code)
                    .HasName("ix_inventoryitems_description");

                // Keys
                entity.HasKey(k => k.Code)
                    .HasName("pk_inventoryitems_id");

                // Properties
                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasDefaultValueSql("nextval('master_seq'::regclass)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description");

                entity.Property(e => e.Category).HasColumnName("category");

                entity.Property(e => e.QtyPurchased).HasColumnName("qty_purchased");

                entity.Property(e => e.Unit).HasColumnName("unit");

                entity.Property(e => e.CustomerPurchasedFor).HasColumnName("customer_purchased_for");

                entity.Property(e => e.CustomerReservedFor).HasColumnName("customer_reserved_for");

                entity.Property(e => e.Supplier).HasColumnName("supplier");

                entity.Property(e => e.QtyToPurchaseNow).HasColumnName("qty_to_purchase_now");

                entity.Property(e => e.ApproxProdQty).HasColumnName("approx_prod_qty");

                entity.Property(e => e.NeededBefore).HasColumnName("needed_before");

                entity.Property(e => e.TargetPrice).HasColumnName("target_price");

                entity.Property(e => e.ShippingCompany).HasColumnName("shipping_company");

                entity.Property(e => e.BondedWarehouse).HasColumnName("bonded_warehouse");

                entity.Property(e => e.Comments).HasColumnName("comments");

                entity.Property(e => e.FOBCost).HasColumnName("fob_cost");

                entity.Property(e => e.FOBCostCurrency).HasColumnName("fob_cost_currency");

                entity.Property(e => e.ShippingCost).HasColumnName("shipping_cost");

                entity.Property(e => e.ShippingCostCurrency).HasColumnName("shipping_cost_currency");

                entity.Property(e => e.ImportCosts).HasColumnName("import_cost");

                entity.Property(e => e.ImportCostCurrency).HasColumnName("import_cost_currency");

                entity.Property(e => e.DateAdded).HasColumnName("date_added");

                entity.Property(e => e.AddedBy).HasColumnName("user_added_by");

                entity.Property(e => e.DateUpdated).HasColumnName("date_updated");

                entity.Property(e => e.UpdatedBy).HasColumnName("user_updated_by");

                entity.Property(e => e.DateConfirmed).HasColumnName("date_confirmed");

                entity.Property(e => e.DateShipped).HasColumnName("date_shipped");

                entity.Property(e => e.DateArrived).HasColumnName("date_arrived");
                
                // Relationships
                entity.HasOne(d => d.UnitNavigation)
                    .WithMany()
                    .HasPrincipalKey(k => k.Abbreviation)
                    .HasForeignKey(k => k.Unit)
                    .HasConstraintName("fk_inventoryitems_unit");

                entity.HasOne(d => d.FOBCostCurrencyNavigation)
                    .WithMany()
                    .HasPrincipalKey(k => k.Abbreviation)
                    .HasForeignKey(k => k.FOBCostCurrency)
                    .HasConstraintName("fk_inventoryitems_fobcostunit");

                entity.HasOne(d => d.ShippingCostCurrencyNavigation)
                    .WithMany()
                    .HasPrincipalKey(k => k.Abbreviation)
                    .HasForeignKey(k => k.ShippingCostCurrency)
                    .HasConstraintName("fk_inventoryitems_shippingcostunit");

                entity.HasOne(d => d.ImportCostCurrencyNavigation)
                    .WithMany()
                    .HasPrincipalKey(k => k.Abbreviation)
                    .HasForeignKey(k => k.ImportCostCurrency)
                    .HasConstraintName("fk_inventoryitems_importcostunit");

                entity.HasOne(d => d.CategoryNavigation)
                    .WithMany()
                    .HasPrincipalKey(p => p.Name)
                    .HasForeignKey(d => d.Category)
                    .HasConstraintName("fk_inventoryitems_category")
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(d => d.FabricTestsNavigation)
                    .WithOne(p => p.InventoryItem)
                    .HasPrincipalKey(p => p.Code)
                    .HasConstraintName("fk_inventoryitems_fabrictests")
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(s => s.ConsumptionEventsNavigation)
                    .WithOne(p => p.InventoryItemNavigation)
                    .HasPrincipalKey(p => p.Code)
                    //.HasForeignKey(f => f.InventoryItemCode)
                    .HasConstraintName("fk_inventoryitems_consumptionevents")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ConsumptionEvent>(entity =>
            {
                // Table
                entity.ToTable("consumption_events");

                // Indexes
                entity.HasIndex(i => i.Id)
                    .HasName("ix_consumptionevents_id")
                    .IsUnique();

                entity.HasIndex(i => i.OrderNumber)
                    .HasName("ix_consumptionevents_ordernumber");

                // Keys
                entity.HasKey(k => k.Id)
                    .HasName("pk_consumptionevents_id");

                // Properties
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('master_seq'::regclass)");

                entity.Property(e => e.DateAdded)
                    .HasColumnName("date_added");

                entity.Property(e => e.AddedBy)
                    .HasColumnName("user_added_by");

                entity.Property(e => e.DateUpdated)
                    .HasColumnName("date_updated");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("user_updated_by");

                entity.Property(e => e.Unit)
                    .HasColumnName("unit");

                entity.Property(e => e.QuantityConsumed)
                    .HasColumnName("quantity_consumed");

                entity.Property(e => e.ConsumedBy)
                    .HasColumnName("consumed_by");

                entity.Property(e => e.DateOfConsumption)
                    .HasColumnName("date_of_consumption");

                entity.Property(e => e.OrderNumber)
                    .HasColumnName("order_number");

                entity.Property(e => e.PatternNumber)
                    .HasColumnName("pattern_number");

                entity.Property(p => p.InventoryItemCode)
                    .HasColumnName("inventory_item_code");

                // Relationships
                entity.HasOne(d => d.InventoryItemNavigation)
                    .WithMany(p => p.ConsumptionEventsNavigation)
                    .HasPrincipalKey(p => p.Code)
                    .HasForeignKey(f => f.InventoryItemCode)
                    .HasConstraintName("fk_consumptionevents_inventory_code")
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.UnitNavigation)
                    .WithMany()
                    .HasPrincipalKey(p => p.Abbreviation)
                    .HasForeignKey(f => f.Unit)
                    .HasConstraintName("fk_consumptionevents_units")
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.OrderNavigation)
                    .WithMany()
                    .HasPrincipalKey(k => k.Id)
                    .HasForeignKey(f => f.OrderNumber)
                    .HasConstraintName("fk_order_id")
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                // Table
                entity.ToTable("orders");

                // Indexes
                entity.HasIndex(e => e.Id)
                    .HasName("ix_orders_id")
                    .IsUnique();

                // Properties
                entity.Property(p => p.Id)
                    .HasColumnName("id");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                // Table
                entity.ToTable("suppliers");

                // Indexes
                entity.HasIndex(e => e.Id)
                    .HasName("ix_suppliers_id")
                    .IsUnique();

                // Properties
                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

            });

            modelBuilder.Entity<FabricTest>(entity =>
                {
                    // Table
                    entity.ToTable("fabric_tests");

                    // Indexes
                    entity.HasIndex(i => i.Id)
                        .HasName("ix_fabrictests_id")
                        .IsUnique();

                    // Keys
                    entity.HasKey(k => k.Id)
                        .HasName("pk_fabrictests_id");

                    // Properties
                    entity.Property(e => e.Id)
                        .HasColumnName("id")
                        .HasDefaultValueSql("nextval('master_seq'::regclass)");

                    entity.Property(e => e.Name).HasColumnName("name");

                    entity.Property(e => e.Result).HasColumnName("result");

                    entity.Property(p => p.InventoryItemCode).HasColumnName("inventory_item_code");

                    // Relationships
                    entity.HasOne(e => e.InventoryItem)
                        .WithMany(p => p.FabricTestsNavigation)
                        .HasForeignKey(f => f.InventoryItemCode)
                        .HasPrincipalKey(k => k.Code);
                }
            );

            modelBuilder.Entity<PurchaseEvent>(entity =>
                {
                    // Table
                    entity.ToTable("purchase_events");

                    // Indexes
                    entity.HasIndex(i => i.Id)
                        .HasName("ix_purchaseevents_id")
                        .IsUnique();

                    // Keys
                    entity.HasKey(k => k.Id)
                        .HasName("pk_purchaseevents_id");

                    // Properties
                    entity.Property(p => p.Id)
                        .HasColumnName("id")
                        .HasDefaultValueSql("nextval('master_seq'::regclass)");

                    entity.Property(e => e.DateAdded).HasColumnName("date_added");

                    entity.Property(e => e.AddedBy).HasColumnName("user_added_by");

                    entity.Property(e => e.DateUpdated).HasColumnName("date_updated");

                    entity.Property(e => e.UpdatedBy).HasColumnName("user_updated_by");

                    entity.Property(e => e.Description).HasColumnName("description");

                    entity.Property(e => e.CustomerPurchasedFor).HasColumnName("customer_purchased_for");

                    entity.Property(e => e.Supplier).HasColumnName("supplier");

                    entity.Property(e => e.CustomerPurchasedFor).HasColumnName("customer_purchased_for");
                }
            );



            // Seed Admin Data
            modelBuilder.Entity<CurrencyType>().HasData(
                                                new { Id = 1, Name = "United States Dollars", Symbol = "$", Abbreviation = "USD", SortOrder = 1, Active = false, DefaultSelection = false },
                                                new { Id = 2, Name = "Nepali Rupees", Symbol = "रु", Abbreviation = "NRP", SortOrder = 2, Active = true, DefaultSelection = true },
                                                new { Id = 3, Name = "Indian Rupees", Symbol = "₹", Abbreviation = "INR", SortOrder = 3, Active = false, DefaultSelection = false });

            modelBuilder.Entity<InventoryCategory>().HasData(
                                                new { Id = 1, Name = "Elastic", Description = "", SortOrder = 1, Active = true },
                                                new { Id = 2, Name = "Fastener", Description = "", SortOrder = 2, Active = true },
                                                new { Id = 3, Name = "Knit", Description = "", SortOrder = 3, Active = true },
                                                new { Id = 4, Name = "Labels and Tags", Description = "", SortOrder = 4, Active = true },
                                                new { Id = 5, Name = "Leather", Description = "", SortOrder = 5, Active = true },
                                                new { Id = 6, Name = "Other", Description = "", SortOrder = 6, Active = true },
                                                new { Id = 7, Name = "Thread", Description = "", SortOrder = 7, Active = true },
                                                new { Id = 8, Name = "Woven", Description = "", SortOrder = 8, Active = true },
                                                new { Id = 9, Name = "Zipper", Description = "", SortOrder = 9, Active = true });

            modelBuilder.Entity<Unit>().HasData(new { Id = 1, Name = "kilogram", Abbreviation = "kg", SortOrder = 1, Active = true },
                                                new { Id = 2, Name = "meter", Abbreviation = "meter", SortOrder = 2, Active = true },
                                                new { Id = 3, Name = "piece", Abbreviation = "piece", SortOrder = 3, Active = true },
                                                new { Id = 4, Name = "set", Abbreviation = "set", SortOrder = 4, Active = true });

            modelBuilder.Entity<User>().HasData(new
            {
                Id = new Guid().GetHashCode().ToString(),
                UserName = "Bob",
                NormalizedUserName = "BOB",
                Email = "bob@example.com",
                NormalizedEmail = "BOB@EXAMPLE.COM",
                PasswordHash = "AQAAAAEAACcQAAAAEI4jEmRsUYzL6KnpR2/OjIPvkI9BWNmnnCZYah1GFvB2EOCWkgkk49YqCJBz38N8rg==",
                SecurityStamp = "3YILVFJYDKC4OK7QLLR4TO4KT6V4ZK5E",
                ConcurrencyStamp = "2cebd9d0-694d-4ed3-8dc2-384f41557310",
                AccessFailedCount = 0,
                EmailConfirmed = false,
                LockoutEnabled = false,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                IsActive = true
            });
        }

        public DbSet<Egret.Models.PurchaseEvent> Purchase { get; set; }
    }
}
