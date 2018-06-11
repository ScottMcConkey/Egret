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
            modelBuilder.Entity<Role>()
                .ToTable("aspnet_roles")
                .Property(u => u.Id)
                .HasMaxLength(450);
            // Nobody should have to use double quotes for ad-hoc queries (FTS),
            // so give all the Identity tables custom names that won't be generated
            // as string literals.
            modelBuilder.Entity<IdentityUserRole<string>>()
                .ToTable("aspnet_userroles")
                ;//.HasIndex("ix_aspnet_userroles_roleid");
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

            // Create Sequences
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
                // Table definition
                entity.ToTable("currency_types");

                // Indexes
                entity.HasIndex(e => e.Abbreviation)
                    .HasName("ix_currencytypes_abbreviation")
                    .IsUnique();

                entity.HasIndex(e => e.SortOrder)
                    .HasName("ix_currencytypes_sortorder")
                    .IsUnique();

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
                entity.ToTable("inventory_categories");

                entity.HasIndex(e => e.Name)
                    .HasName("ix_inventorycategories_name")
                    .IsUnique();

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
                entity.ToTable("units");

                entity.HasIndex(e => e.SortOrder)
                    .HasName("ix_units_sortorder")
                    .IsUnique();

                entity.HasIndex(e => e.Abbreviation)
                    .HasName("ix_units_abbreviation")
                    .IsUnique();

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
                entity.HasKey(e => e.Code);

                entity.ToTable("inventory_items");

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

                entity.Property(e => e.Buyunit).HasColumnName("buyunit_fk");

                entity.Property(e => e.Sellcurrency).HasColumnName("sellcurrency");

                entity.Property(e => e.Sellprice).HasColumnName("sellprice");

                entity.Property(e => e.Sellunit).HasColumnName("sellunit_fk");

                entity.Property(e => e.Supplier).HasColumnName("supplier_fk");

                

                entity.HasOne(d => d.BuycurrencyNavigation)
                    .WithMany(p => p.InventoryItemsBuycurrencyNavigation)
                    .HasPrincipalKey(p => p.Abbreviation)
                    .HasForeignKey(d => d.Buycurrency)
                    .HasConstraintName("inventoryitems_buycurrency_fk")
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.SellcurrencyNavigation)
                    .WithMany(p => p.InventoryItemsSellcurrencyNavigation)
                    .HasPrincipalKey(p => p.Abbreviation)
                    .HasForeignKey(d => d.Sellcurrency)
                    .HasConstraintName("inventoryitems_sellcurrency_fk")
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.BuyunitNavigation)
                    .WithMany(p => p.InventoryItemsBuyunitNavigation)
                    .HasPrincipalKey(p => p.Abbreviation)
                    .HasForeignKey(d => d.Buyunit)
                    .HasConstraintName("inventoryitems_buyunit_fk")
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.SellunitNavigation)
                    .WithMany(p => p.InventoryItemsSellunitNavigation)
                    .HasPrincipalKey(p => p.Abbreviation)
                    .HasForeignKey(d => d.Sellunit)
                    .HasConstraintName("inventoryitems_sellunit_fk")
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.CategoryNavigation)
                    .WithMany(p => p.InventoryItems)
                    .HasPrincipalKey(p => p.Name)
                    .HasForeignKey(d => d.Category)
                    .HasConstraintName("inventoryitems_category_fk")
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(d => d.FabricTests)
                    .WithOne(p => p.InventoryItem)
                    .HasPrincipalKey(p => p.Code)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("suppliers");

                entity.HasIndex(e => e.Id)
                    .HasName("ix_suppliers_pk")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

            });

            modelBuilder.Entity<FabricTest>(entity =>
                {
                    entity.ToTable("fabric_tests");

                    entity.HasIndex(e => e.Id)
                        .HasName("ix_fabrictests_pk")
                        .IsUnique();

                    entity.Property(e => e.Id).HasColumnName("id");

                    entity.Property(e => e.Name).HasColumnName("name");

                    entity.Property(e => e.Result).HasColumnName("result");
                }
            );

            // Seed Admin Data
            modelBuilder.Entity<CurrencyType>().HasData(
                                                new { Id = 1, Name = "United States Dollars", Symbol = "$", Abbreviation = "USD", SortOrder = 1, Active = true, DefaultSelection = false },
                                                new { Id = 2, Name = "Nepali Rupees", Symbol = "रु", Abbreviation = "NRP", SortOrder = 2, Active = true, DefaultSelection = true },
                                                new { Id = 3, Name = "Indian Rupees", Symbol = "₹", Abbreviation = "INR", SortOrder = 3, Active = true, DefaultSelection = false });

            modelBuilder.Entity<InventoryCategory>().HasData(
                                                new { Id = 1, Name = "Buckle Thread", Description = "", SortOrder = 1, Active = true },
                                                new { Id = 2, Name = "Button", Description = "", SortOrder = 2, Active = true },
                                                new { Id = 3, Name = "Elastic", Description = "", SortOrder = 3, Active = true },
                                                new { Id = 4, Name = "Hang-Tag", Description = "", SortOrder = 4, Active = true },
                                                new { Id = 5, Name = "Knit Fabric", Description = "", SortOrder = 5, Active = true },
                                                new { Id = 6, Name = "Label", Description = "", SortOrder = 6, Active = true },
                                                new { Id = 7, Name = "Leather", Description = "", SortOrder = 7, Active = true },
                                                new { Id = 8, Name = "Other", Description = "", SortOrder = 8, Active = true },
                                                new { Id = 9, Name = "Snap", Description = "", SortOrder = 9, Active = true },
                                                new { Id = 10, Name = "Woven Fabric", Description = "", SortOrder = 10, Active = true },
                                                new { Id = 11, Name = "Zipper", Description = "", SortOrder = 11, Active = true });

            modelBuilder.Entity<Unit>().HasData(new { Id = 1, Name = "kilograms", Abbreviation = "kg", SortOrder = 1, Active = true },
                                                new { Id = 2, Name = "meters", Abbreviation = "m", SortOrder = 2, Active = true },
                                                new { Id = 3, Name = "each", Abbreviation = "ea", SortOrder = 3, Active = true },
                                                new { Id = 4, Name = "grams per square meter", Abbreviation = "g/m2", SortOrder = 4, Active = true },
                                                new { Id = 5, Name = "centimeters", Abbreviation = "cm", SortOrder = 5, Active = true },
                                                new { Id = 6, Name = "square feet", Abbreviation = "sqf", SortOrder = 6, Active = true });

            modelBuilder.Entity<User>().HasData(new
            {
                Id = new Guid().ToString(),
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
                TwoFactorEnabled = false
            });
        }
    }
}
