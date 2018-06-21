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

            modelBuilder.Entity<IdentityUser>()
                .ToTable("aspnet_users")
                .Property(u => u.Id)
                .HasMaxLength(450);
            modelBuilder.Entity<User>()
                .ToTable("aspnet_users")
                .Property(u => u.Id)
                .HasMaxLength(450);
            modelBuilder.Entity<IdentityRole>()
                .ToTable("aspnet_roles")
                .Property(u => u.Id)
                .HasMaxLength(450);

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("aspnet_roles");
                entity.Property(u => u.Id)
                    .HasMaxLength(450);
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

            modelBuilder.HasPostgresExtension("adminpack");

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

                entity.Property(e => e.DateAdded).HasColumnName("dateadded");

                entity.Property(e => e.AddedBy).HasColumnName("useraddedby");

                entity.Property(e => e.DateUpdated).HasColumnName("dateupdated");

                entity.Property(e => e.UpdatedBy).HasColumnName("userupdatedby");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description");

                entity.Property(e => e.Category).HasColumnName("category");

                entity.Property(e => e.CustomerPurchasedFor).HasColumnName("customerpurchasedfor");

                entity.Property(e => e.CustomerReservedFor).HasColumnName("customerreservedfor");

                entity.Property(e => e.Supplier).HasColumnName("supplier");

                entity.Property(e => e.QtyToPurchaseNow).HasColumnName("qtytopurchasenow");

                entity.Property(e => e.ApproxProdQty).HasColumnName("approxprodqty");

                entity.Property(e => e.FabricTests_Conversion).HasColumnName("fabrictests_conversion");

                entity.Property(e => e.FabricTestResults).HasColumnName("fabrictestresults");

                entity.Property(e => e.NeededBefore).HasColumnName("neededbefore");

                entity.Property(e => e.TargetPrice).HasColumnName("targetprice");

                entity.Property(e => e.ShippingCompany).HasColumnName("shippingcompany");

                entity.Property(e => e.BondedWarehouse).HasColumnName("bondedwarehouse");

                entity.Property(e => e.DateConfirmed).HasColumnName("dateconfirmed");

                entity.Property(e => e.DateShipped).HasColumnName("dateshipped");

                entity.Property(e => e.DateArrived).HasColumnName("datearrived");

                entity.Property(e => e.Comment).HasColumnName("comment");

                entity.Property(e => e.QtyPurchased).HasColumnName("qtypurchased");

                entity.Property(e => e.Unit).HasColumnName("unit");

                entity.Property(e => e.FOBCost).HasColumnName("fobcost");

                entity.Property(e => e.ShippingCost).HasColumnName("shippingcost");

                entity.Property(e => e.ImportCosts).HasColumnName("importcosts");

                entity.Property(e => e.IsConversion).HasColumnName("isconversion");

                entity.Property(e => e.ConversionSource).HasColumnName("conversionsource");

                entity.Property(e => e.Buycurrency).HasColumnName("buycurrency");

                entity.Property(e => e.Buyprice).HasColumnName("buyprice");

                entity.Property(e => e.BuyUnit).HasColumnName("buyunit_fk");

                entity.Property(e => e.Sellcurrency).HasColumnName("sellcurrency");

                entity.Property(e => e.Sellprice).HasColumnName("sellprice");

                entity.Property(e => e.SellUnit).HasColumnName("sellunit");

                entity.Property(e => e.Supplier).HasColumnName("supplier");

                

                entity.HasOne(d => d.BuycurrencyNavigation)
                    .WithMany()
                    .HasPrincipalKey(p => p.Abbreviation)
                    .HasForeignKey(d => d.Buycurrency)
                    .HasConstraintName("fk_inventoryitems_buycurrency")
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.SellcurrencyNavigation)
                    .WithMany()
                    .HasPrincipalKey(p => p.Abbreviation)
                    .HasForeignKey(d => d.Sellcurrency)
                    .HasConstraintName("fk_inventoryitems_sellcurrency")
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.BuyUnitNavigation)
                    .WithMany()
                    .HasPrincipalKey(p => p.Abbreviation)
                    .HasForeignKey(d => d.BuyUnit)
                    .HasConstraintName("fk_inventoryitems_buyunit")
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.SellUnitNavigation)
                    .WithMany()
                    .HasPrincipalKey(p => p.Abbreviation)
                    .HasForeignKey(d => d.SellUnit)
                    .HasConstraintName("fk_inventoryitems_sellunit")
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.CategoryNavigation)
                    .WithMany()
                    .HasPrincipalKey(p => p.Name)
                    .HasForeignKey(d => d.Category)
                    .HasConstraintName("fk_inventoryitems_category")
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(d => d.FabricTests)
                    .WithOne(p => p.InventoryItem)
                    .HasPrincipalKey(p => p.Code)
                    .HasConstraintName("fk_inventoryitems_fabrictests")
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(d => d.ConsumptionEvents)
                    .WithOne(p => p.InventoryItemNavigation)
                    .HasForeignKey(f => f.InventoryItemCode)
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

                // Keys
                entity.HasKey(k => k.Id)
                    .HasName("pk_consumptionevents_id");


                // Properties
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('master_seq'::regclass)");

                entity.Property(e => e.Unit)
                    .HasColumnName("unit");

                entity.Property(e => e.QuantityConsumed)
                    .HasColumnName("quantity_consumed");

                entity.Property(e => e.ConsumedBy)
                    .HasColumnName("consumed_by");

                entity.Property(e => e.DateOfConsumption)
                    .HasColumnName("date_of_consumption");

                entity.Property(e => e.SampleOrderNumber)
                    .HasColumnName("sample_order_number");

                entity.Property(e => e.ProductionOrderNumber)
                    .HasColumnName("production_order_number");

                entity.Property(e => e.PatternNumber)
                    .HasColumnName("pattern_number");

                entity.Property(e => e.ValueConsumed)
                    .HasColumnName("value_consumed");

                entity.Property(p => p.InventoryItemCode)
                    .HasColumnName("inventory_item_code");

                // Relationships
                entity.HasOne(d => d.InventoryItemNavigation)
                    .WithMany(p => p.ConsumptionEvents)
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

            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                // Table
                entity.ToTable("suppliers");

                // Indexes
                entity.HasIndex(e => e.Id)
                    .HasName("ix_suppliers_pk")
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
                        .WithMany(p => p.FabricTests)
                        .HasForeignKey(f => f.InventoryItemCode)
                        .HasPrincipalKey(k => k.Code);
                }
            );

            // Seed Admin Data
            modelBuilder.Entity<CurrencyType>().HasData(
                                                new { Id = 1, Name = "United States Dollars", Symbol = "$", Abbreviation = "USD", SortOrder = 1, Active = true, DefaultSelection = false },
                                                new { Id = 2, Name = "Nepali Rupees", Symbol = "रु", Abbreviation = "NRP", SortOrder = 2, Active = true, DefaultSelection = true },
                                                new { Id = 3, Name = "Indian Rupees", Symbol = "₹", Abbreviation = "INR", SortOrder = 3, Active = true, DefaultSelection = false });

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
    }
}
