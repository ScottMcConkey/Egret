using Egret.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Egret.DataAccess
{
    // Please notice, if no primary key is specified for a table, EF Core defaults to the [id] field or the
    // [<entity name>id] property. The following code reflects a reliance on this convention and does not
    // manually specify primary keys except where necessary.



    /// <summary>
    /// This class is the DbContext used throughout the project for accessing database stores with Entity Framework.
    /// </summary>
    public partial class EgretContext : IdentityDbContext<User, Role, string>
    {
        public EgretContext(DbContextOptions<EgretContext> options) 
            : base(options) {}

        public virtual DbSet<AccessGroup> AccessGroups { get; set; }
        public virtual DbSet<AccessGroupRole> AccessGroupRoles { get; set; }
        public virtual DbSet<ConsumptionEvent> ConsumptionEvents { get; set; }
        public virtual DbSet<CurrencyType> CurrencyTypes { get; set; }
        public virtual DbSet<FabricTest> FabricTests { get; set; }
        public virtual DbSet<InventoryCategory> InventoryCategories { get; set; }
        public virtual DbSet<InventoryItem> InventoryItems { get; set; }
        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<UserAccessGroup> UserAccessGroups { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region PostgreSQL-Specific
            modelBuilder.HasPostgresExtension("adminpack");
            #endregion

            #region Identity Table Names
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("roles");
            });

            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("user_roles");
            });

            modelBuilder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("user_claims");
            });

            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("user_logins");
            });

            modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("role_claims");
            });

            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("user_tokens");
            });
            #endregion

            #region Sequences
            modelBuilder.HasSequence<long>("items_id_seq")
                .StartsAt(1000);
            modelBuilder.HasSequence<long>("fabric_tests_id_seq")
                .StartsAt(1000);
            modelBuilder.HasSequence<long>("consumption_events_id_seq")
                .StartsAt(1000);
            modelBuilder.HasSequence<long>("inventorycategories_id_seq")
                .StartsAt(100);
            modelBuilder.HasSequence<long>("units_id_seq")
                .StartsAt(100);
            modelBuilder.HasSequence<long>("currencytypes_id_seq")
                .StartsAt(100);
            modelBuilder.HasSequence<long>("accessgroups_id_seq")
                .StartsAt(100);
            #endregion

            #region Entities

            modelBuilder.Entity<AccessGroup>(entity =>
            {
                // Table
                entity.ToTable("accessgroups");

                // Properties
                entity.Property(p => p.Id)
                    .HasDefaultValueSql("nextval('accessgroups_id_seq'::regclass)"); ;

                entity.Property(p => p.Name);

                entity.Property(e => e.Description);
            });

            modelBuilder.Entity<AccessGroupRole>(entity =>
            {
                // Table
                entity.ToTable("accessgroup_roles");

                // Keys
                entity.HasKey(k => new { k.AccessGroupId, k.RoleId });

                // Relationships
                entity.HasOne(p => p.AccessGroup)
                    .WithMany(c => c.AccessGroupRoles)
                    .HasForeignKey(k => k.AccessGroupId);

                entity.HasOne(p => p.Role)
                    .WithMany(c => c.AccessGroupRoles)
                    .HasForeignKey(k => k.RoleId);
            });

            modelBuilder.Entity<ConsumptionEvent>(entity =>
            {
                // Table
                entity.ToTable("consumption_events");

                // Properties
                entity.Property(e => e.Id)
                    .HasDefaultValueSql("'CE' || nextval('consumption_events_id_seq'::regclass)");

                entity.Property(e => e.DateAdded);

                entity.Property(e => e.AddedBy);

                entity.Property(e => e.DateUpdated);

                entity.Property(e => e.UpdatedBy);

                entity.Property(e => e.QuantityConsumed);

                entity.Property(e => e.ConsumedBy);

                entity.Property(e => e.DateOfConsumption);

                entity.Property(e => e.OrderNumber);

                entity.Property(e => e.PatternNumber);

                entity.Property(e => e.Comments);

                entity.Property(p => p.InventoryItemCode);

                // Relationships
                entity.HasOne(d => d.InventoryItemNavigation)
                    .WithMany(p => p.ConsumptionEventsNavigation)
                    .HasPrincipalKey(p => p.Code)
                    .HasForeignKey(f => f.InventoryItemCode)
                    .OnDelete(DeleteBehavior.Cascade);

            });

            modelBuilder.Entity<CurrencyType>(entity =>
            {
                // Table
                entity.ToTable("currency_types");

                // Keys
                entity.HasAlternateKey(k => k.Abbreviation);

                // Properties
                entity.Property(e => e.Id)
                    .HasDefaultValueSql("nextval('currencytypes_id_seq'::regclass)");

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Abbreviation).IsRequired();

                entity.Property(e => e.Symbol);

                entity.Property(e => e.Active);

                entity.Property(e => e.DefaultSelection);

                entity.Property(e => e.SortOrder);
            });

            modelBuilder.Entity<FabricTest>(entity =>
            {
                // Table
                entity.ToTable("fabric_tests");

                // Properties
                entity.Property(e => e.Id)
                    .HasDefaultValueSql("nextval('fabric_tests_id_seq'::regclass)");

                entity.Property(e => e.Name);

                entity.Property(e => e.Result);

                entity.Property(p => p.InventoryItemCode);

                // Relationships
                entity.HasOne(e => e.InventoryItem)
                    .WithMany(p => p.FabricTestsNavigation)
                    .HasForeignKey(f => f.InventoryItemCode)
                    .HasPrincipalKey(k => k.Code);
            });

            modelBuilder.Entity<InventoryCategory>(entity =>
            {
                // Table
                entity.ToTable("inventory_categories");

                // Keys
                entity.HasAlternateKey(k => k.Name);

                // Properties
                entity.Property(e => e.Id)
                    .HasDefaultValueSql("nextval('inventorycategories_id_seq'::regclass)");

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Description);

                entity.Property(e => e.SortOrder).IsRequired();

                entity.Property(e => e.Active);
            });

            modelBuilder.Entity<InventoryItem>(entity =>
            {
                // Table
                entity.ToTable("inventory_items");

                // Keys
                entity.HasKey(k => k.Code);

                // Properties
                entity.Property(e => e.Code)
                    .HasDefaultValueSql("'I' || nextval('items_id_seq'::regclass)");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Category).IsRequired();

                entity.Property(e => e.QtyPurchased);

                entity.Property(e => e.Unit);

                entity.Property(e => e.CustomerPurchasedFor);

                entity.Property(e => e.CustomerReservedFor);

                entity.Property(e => e.Supplier);

                entity.Property(e => e.QtyToPurchaseNow);

                entity.Property(e => e.TargetPrice);

                entity.Property(e => e.ShippingCompany);

                entity.Property(e => e.BondedWarehouse);

                entity.Property(e => e.Comments);

                entity.Property(e => e.FOBCost);

                entity.Property(e => e.FOBCostCurrency);

                entity.Property(e => e.ShippingCost);

                entity.Property(e => e.ShippingCostCurrency);

                entity.Property(e => e.ImportCosts);

                entity.Property(e => e.ImportCostCurrency);

                entity.Property(e => e.DateAdded);

                entity.Property(e => e.AddedBy);

                entity.Property(e => e.DateUpdated);

                entity.Property(e => e.UpdatedBy);

                entity.Property(e => e.DateConfirmed);

                entity.Property(e => e.DateShipped);

                entity.Property(e => e.DateArrived);

                // Relationships
                entity.HasOne(d => d.UnitNavigation)
                    .WithMany()
                    .HasPrincipalKey(k => k.Abbreviation)
                    .HasForeignKey(k => k.Unit);

                entity.HasOne(d => d.FOBCostCurrencyNavigation)
                    .WithMany()
                    .HasPrincipalKey(k => k.Abbreviation)
                    .HasForeignKey(k => k.FOBCostCurrency);

                entity.HasOne(d => d.ShippingCostCurrencyNavigation)
                    .WithMany()
                    .HasPrincipalKey(k => k.Abbreviation)
                    .HasForeignKey(k => k.ShippingCostCurrency);

                entity.HasOne(d => d.ImportCostCurrencyNavigation)
                    .WithMany()
                    .HasPrincipalKey(k => k.Abbreviation)
                    .HasForeignKey(k => k.ImportCostCurrency);

                entity.HasOne(d => d.CategoryNavigation)
                    .WithMany()
                    .HasPrincipalKey(p => p.Name)
                    .HasForeignKey(d => d.Category)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(d => d.FabricTestsNavigation)
                    .WithOne(p => p.InventoryItem)
                    .HasPrincipalKey(p => p.Code)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(s => s.ConsumptionEventsNavigation)
                    .WithOne(p => p.InventoryItemNavigation)
                    .HasPrincipalKey(p => p.Code)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            //modelBuilder.Entity<PurchaseEvent>(entity =>
            //    {
            //        // Table
            //        entity.ToTable("purchase_events");

            //        // Properties
            //        entity.Property(p => p.Id)
            //            .HasDefaultValueSql("nextval('master_seq'::regclass)");

            //        entity.Property(e => e.DateAdded);

            //        entity.Property(e => e.AddedBy);

            //        entity.Property(e => e.DateUpdated);

            //        entity.Property(e => e.UpdatedBy);

            //        entity.Property(e => e.Description);

            //        entity.Property(e => e.CustomerPurchasedFor);

            //        entity.Property(e => e.Supplier);

            //        entity.Property(e => e.CustomerPurchasedFor);
            //    });

            modelBuilder.Entity<Unit>(entity =>
            {
                // Table
                entity.ToTable("units");

                // Keys
                entity.HasAlternateKey(k => k.Abbreviation);

                // Indexes
                entity.HasIndex(e => e.SortOrder)
                    .IsUnique();

                // Properties
                entity.Property(e => e.Id)
                    .HasDefaultValueSql("nextval('units_id_seq'::regclass)");

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Abbreviation).IsRequired();

                entity.Property(e => e.SortOrder);

                entity.Property(e => e.Active);
            });

            modelBuilder.Entity<UserAccessGroup>(entity =>
            {
                // Table
                entity.ToTable("user_accessgroups");

                // Keys
                entity.HasKey(k => new { k.AccessGroupId, k.UserId });

                // Properties
                entity.Property(p => p.AccessGroupId);

                entity.Property(p => p.UserId);

                // Relationships
                entity.HasOne(p => p.AccessGroup)
                    .WithMany(c => c.UserAccessGroups)
                    .HasForeignKey(k => k.AccessGroupId);

                entity.HasOne(p => p.User)
                    .WithMany(c => c.UserAccessGroups)
                    .HasForeignKey(k => k.UserId);
            });

            #endregion

            #region Lowercase Names
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                // Replace Table Names
                entity.Relational().TableName = entity.Relational().TableName.ToLower();

                // Replace Column Names            
                foreach (var property in entity.GetProperties())
                {
                    property.Relational().ColumnName = property.Name.ToLower();
                }

                // Replace Keys
                foreach (var key in entity.GetKeys())
                {
                    key.Relational().Name = key.Relational().Name.ToLower();
                }

                // Replace Foreign Keys
                foreach (var key in entity.GetForeignKeys())
                {
                    key.Relational().Name = key.Relational().Name.ToLower();
                }

                // Replace Indexes
                foreach (var index in entity.GetIndexes())
                {
                    index.Relational().Name = index.Relational().Name.ToLower();
                }
            }
            #endregion

            #region Seed Data
            modelBuilder.Entity<CurrencyType>().HasData(
                new { Id = 1, Name = "Nepali Rupees", Symbol = "रु", Abbreviation = "NRP", SortOrder = 2, Active = true, DefaultSelection = true }
            );

            modelBuilder.Entity<InventoryCategory>().HasData(
                new { Id = 1, Name = "Elastic", Description = "", SortOrder = 1, Active = true },
                new { Id = 2, Name = "Fastener", Description = "", SortOrder = 2, Active = true },
                new { Id = 3, Name = "Knit", Description = "", SortOrder = 3, Active = true },
                new { Id = 4, Name = "Labels and Tags", Description = "", SortOrder = 4, Active = true },
                new { Id = 5, Name = "Leather", Description = "", SortOrder = 5, Active = true },
                new { Id = 6, Name = "Other", Description = "", SortOrder = 6, Active = true },
                new { Id = 7, Name = "Thread", Description = "", SortOrder = 7, Active = true },
                new { Id = 8, Name = "Woven", Description = "", SortOrder = 8, Active = true },
                new { Id = 9, Name = "Zipper", Description = "", SortOrder = 9, Active = true }
            );

            modelBuilder.Entity<Unit>().HasData(
                new { Id = 1, Name = "kilogram", Abbreviation = "kg", SortOrder = 1, Active = true },
                new { Id = 2, Name = "meter", Abbreviation = "meter", SortOrder = 2, Active = true },
                new { Id = 3, Name = "piece", Abbreviation = "piece", SortOrder = 3, Active = true },
                new { Id = 4, Name = "set", Abbreviation = "set", SortOrder = 4, Active = true }
            );

            var userId = Guid.NewGuid().ToString();
            var role_id1 = Guid.NewGuid().ToString();
            var role_id2 = Guid.NewGuid().ToString();
            var role_id3 = Guid.NewGuid().ToString();
            var role_id4 = Guid.NewGuid().ToString();
            var role_id5 = Guid.NewGuid().ToString();
            var role_id6 = Guid.NewGuid().ToString();
            var role_id7 = Guid.NewGuid().ToString();
            var role_id8 = Guid.NewGuid().ToString();
            var role_id9 = Guid.NewGuid().ToString();
            var role_id10 = Guid.NewGuid().ToString();
            var accessgroup_id1 = 1;

            modelBuilder.Entity<User>().HasData(new
            {
                Id = userId,
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
                Active = true
            });

            modelBuilder.Entity<Role>()
                .HasData(new { Id = role_id1, Name = "Item_Create", DisplayName = "Item Create", NormalizedName = "ITEM_CREATE" },
                         new { Id = role_id2, Name = "Item_Read", DisplayName = "Item Read", NormalizedName = "ITEM_READ" },
                         new { Id = role_id3, Name = "Item_Edit", DisplayName = "Item Update", NormalizedName = "ITEM_EDIT" },
                         new { Id = role_id4, Name = "Item_Delete", DisplayName = "Item Delete", NormalizedName = "ITEM_DELETE" },
                         new { Id = role_id5, Name = "ConsumptionEvent_Create", DisplayName = "Consumption Event Create", NormalizedName = "CONSUMPTIONEVENT_CREATE" },
                         new { Id = role_id6, Name = "ConsumptionEvent_Read", DisplayName = "Consumption Event Read", NormalizedName = "CONSUMPTIONEVENT_READ" },
                         new { Id = role_id7, Name = "ConsumptionEvent_Edit", DisplayName = "Consumption Event Update", NormalizedName = "CONSUMPTIONEVENT_EDIT" },
                         new { Id = role_id8, Name = "ConsumptionEvent_Delete", DisplayName = "Consumption Event Delete", NormalizedName = "CONSUMPTIONEVENT_DELETE" },
                         new { Id = role_id9, Name = "Admin_Access", DisplayName = "Administrator Access", NormalizedName = "ADMIN_ACCESS" },
                         new { Id = role_id10, Name = "Report_Read", DisplayName = "Report Read", NormalizedName = "REPORT_READ" });

            modelBuilder.Entity<AccessGroup>()
                .HasData(new { Id = accessgroup_id1, Name = "Administrator" });

            modelBuilder.Entity<AccessGroupRole>()
                .HasData(new { AccessGroupId = accessgroup_id1, RoleId = role_id1 },
                         new { AccessGroupId = accessgroup_id1, RoleId = role_id2 },
                         new { AccessGroupId = accessgroup_id1, RoleId = role_id3 },
                         new { AccessGroupId = accessgroup_id1, RoleId = role_id4 },
                         new { AccessGroupId = accessgroup_id1, RoleId = role_id5 },
                         new { AccessGroupId = accessgroup_id1, RoleId = role_id6 },
                         new { AccessGroupId = accessgroup_id1, RoleId = role_id7 },
                         new { AccessGroupId = accessgroup_id1, RoleId = role_id8 },
                         new { AccessGroupId = accessgroup_id1, RoleId = role_id9 },
                         new { AccessGroupId = accessgroup_id1, RoleId = role_id10 }
                );

            modelBuilder.Entity<UserAccessGroup>()
                .HasData(new { UserId = userId, AccessGroupId = accessgroup_id1 });

            modelBuilder.Entity<IdentityUserRole<string>>()
                .HasData(new { UserId = userId, RoleId = role_id1 },
                         new { UserId = userId, RoleId = role_id2 },
                         new { UserId = userId, RoleId = role_id3 },
                         new { UserId = userId, RoleId = role_id4 },
                         new { UserId = userId, RoleId = role_id5 },
                         new { UserId = userId, RoleId = role_id6 },
                         new { UserId = userId, RoleId = role_id7 },
                         new { UserId = userId, RoleId = role_id8 },
                         new { UserId = userId, RoleId = role_id9 },
                         new { UserId = userId, RoleId = role_id10 });
            #endregion
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.EnableSensitiveDataLogging();
        //}

    }
}
