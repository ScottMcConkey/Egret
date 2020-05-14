using DocumentFormat.OpenXml.Office2010.PowerPoint;
using Egret.DataAccess.QueryModels;
using Egret.Extensions;
using Egret.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.RegularExpressions;

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
        public virtual DbSet<StorageLocation> StorageLocations { get; set; }

        public DbSet<Test> TestResults { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Test>()
                .HasNoKey();

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
            modelBuilder.HasSequence<long>("inventory_categories_id_seq")
                .StartsAt(100);
            modelBuilder.HasSequence<long>("units_id_seq")
                .StartsAt(100);
            modelBuilder.HasSequence<long>("currency_types_id_seq")
                .StartsAt(100);
            modelBuilder.HasSequence<long>("access_groups_id_seq")
                .StartsAt(100);
            modelBuilder.HasSequence<long>("storage_location_id_seq")
                .StartsAt(1);
            #endregion

            #region Entities

            modelBuilder.Entity<AccessGroup>(entity =>
            {
                // Table
                entity.ToTable("access_groups");

                // Properties
                entity.Property(p => p.Id)
                    .HasDefaultValueSql("nextval('access_groups_id_seq'::regclass)"); ;

                entity.Property(p => p.Name);

                entity.Property(e => e.Description);
            });

            modelBuilder.Entity<AccessGroupRole>(entity =>
            {
                // Table
                entity.ToTable("access_group_roles");

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
                // Properties
                entity.Property(e => e.Id)
                    .HasDefaultValueSql("'CE' || nextval('consumption_events_id_seq'::regclass)");

                // Relationships
                entity.HasOne(d => d.InventoryItemNavigation)
                    .WithMany(p => p.ConsumptionEventsNavigation)
                    .HasForeignKey(f => f.InventoryItemCode)
                    .OnDelete(DeleteBehavior.Cascade);

            });

            modelBuilder.Entity<CurrencyType>(entity =>
            {
                // Indexes
                entity.HasIndex(i => i.Name).IsUnique();

                entity.HasIndex(i => i.Abbreviation).IsUnique();

                entity.HasIndex(i => i.SortOrder).IsUnique();

                // Properties
                entity.Property(e => e.CurrencyTypeId)
                    .HasDefaultValueSql("nextval('currency_types_id_seq'::regclass)");
            });

            modelBuilder.Entity<FabricTest>(entity =>
            {
                // Properties
                entity.Property(e => e.FabricTestId)
                    .HasDefaultValueSql("nextval('fabric_tests_id_seq'::regclass)");

                // Relationships
                entity.HasOne(e => e.InventoryItem)
                    .WithMany(p => p.FabricTestsNavigation)
                    .HasForeignKey(f => f.InventoryItemCode)
                    .HasPrincipalKey(k => k.Code);
            });

            modelBuilder.Entity<InventoryCategory>(entity =>
            {
                // Indexes
                entity.HasIndex(i => i.Name).IsUnique();

                entity.HasIndex(i => i.SortOrder).IsUnique();

                // Properties
                entity.Property(e => e.InventoryCategoryId)
                    .HasDefaultValueSql("nextval('inventory_categories_id_seq'::regclass)");
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

                entity.Property(e => e.InventoryCategoryId).IsRequired();

                entity.Property(e => e.StorageLocationId);

                entity.Property(e => e.QtyPurchased);

                entity.Property(e => e.UnitId);

                entity.Property(e => e.CustomerPurchasedFor);

                entity.Property(e => e.CustomerReservedFor);

                entity.Property(e => e.Supplier);

                entity.Property(e => e.QtyToPurchaseNow);

                entity.Property(e => e.TargetPrice);

                entity.Property(e => e.ShippingCompany);

                entity.Property(e => e.BondedWarehouse);

                entity.Property(e => e.Comments);

                entity.Property(e => e.FobCost);

                entity.Property(e => e.FobCostCurrencyId);

                entity.Property(e => e.ShippingCost);

                entity.Property(e => e.ShippingCostCurrencyId);

                entity.Property(e => e.ImportCosts);

                entity.Property(e => e.ImportCostCurrencyId);

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
                    .HasForeignKey(k => k.UnitId);

                entity.HasOne(d => d.FobCostCurrencyNavigation)
                    .WithMany()
                    .HasForeignKey(k => k.FobCostCurrencyId);

                entity.HasOne(d => d.ShippingCostCurrencyNavigation)
                    .WithMany()
                    .HasForeignKey(k => k.ShippingCostCurrencyId);

                entity.HasOne(d => d.ImportCostCurrencyNavigation)
                    .WithMany()
                    .HasForeignKey(k => k.ImportCostCurrencyId);

                entity.HasOne(d => d.CategoryNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.InventoryCategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(d => d.FabricTestsNavigation)
                    .WithOne(p => p.InventoryItem)
                    .HasPrincipalKey(p => p.Code)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(s => s.ConsumptionEventsNavigation)
                    .WithOne(p => p.InventoryItemNavigation)
                    .HasPrincipalKey(p => p.Code)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(s => s.StorageLocationNavigation)
                    .WithMany()
                    .HasForeignKey(f => f.StorageLocationId);
            });

            modelBuilder.Entity<Unit>(entity =>
            {
                // Indexes
                entity.HasIndex(e => e.Name)
                    .IsUnique();

                entity.HasIndex(e => e.Abbreviation)
                    .IsUnique();

                entity.HasIndex(e => e.SortOrder)
                    .IsUnique();

                // Properties
                entity.Property(e => e.UnitId)
                    .HasDefaultValueSql("nextval('units_id_seq'::regclass)");
            });

            modelBuilder.Entity<UserAccessGroup>(entity =>
            {
                // Table
                entity.ToTable("user_access_groups");

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

            modelBuilder.Entity<StorageLocation>(entity =>
            {
                // Properties
                entity.Property(p => p.StorageLocationId)
                    .HasDefaultValueSql("nextval('storage_location_id_seq'::regclass)");
            });

            #endregion

            #region Lowercase Names
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                // Replace Table Names
                entity.SetTableName(entity.GetTableName().ToSnakeCase());

                // Replace Column Names            
                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(property.Name.ToSnakeCase());
                }

                // Replace Keys
                foreach (var key in entity.GetKeys())
                {
                    key.SetName(key.GetName().ToSnakeCase().ToLower());
                }

                // Replace Foreign Keys
                foreach (var key in entity.GetForeignKeys())
                {
                    key.SetConstraintName(key.GetConstraintName().ToSnakeCase());
                }

                // Replace Indexes
                foreach (var index in entity.GetIndexes())
                {
                    index.SetName(index.GetName().ToSnakeCase());
                }
            }
            #endregion

            #region Seed Data
            modelBuilder.Entity<CurrencyType>().HasData(
                new { CurrencyTypeId = 1, Name = "Nepali Rupees", Symbol = "रु", Abbreviation = "NRP", SortOrder = 1, Active = true, DefaultSelection = true }
            );

            modelBuilder.Entity<InventoryCategory>().HasData(
                new { InventoryCategoryId = 1, Name = "Elastic", Description = "", SortOrder = 1, Active = true },
                new { InventoryCategoryId = 2, Name = "Fastener", Description = "", SortOrder = 2, Active = true },
                new { InventoryCategoryId = 3, Name = "Knit", Description = "", SortOrder = 3, Active = true },
                new { InventoryCategoryId = 4, Name = "Labels and Tags", Description = "", SortOrder = 4, Active = true },
                new { InventoryCategoryId = 5, Name = "Leather", Description = "", SortOrder = 5, Active = true },
                new { InventoryCategoryId = 6, Name = "Other", Description = "", SortOrder = 6, Active = true },
                new { InventoryCategoryId = 7, Name = "Thread", Description = "", SortOrder = 7, Active = true },
                new { InventoryCategoryId = 8, Name = "Woven", Description = "", SortOrder = 8, Active = true },
                new { InventoryCategoryId = 9, Name = "Zipper", Description = "", SortOrder = 9, Active = true }
            );

            modelBuilder.Entity<Unit>().HasData(
                new { UnitId = 1, Name = "kilogram", Abbreviation = "kg", SortOrder = 1, Active = true },
                new { UnitId = 2, Name = "meter", Abbreviation = "m", SortOrder = 2, Active = true },
                new { UnitId = 3, Name = "piece", Abbreviation = "piece", SortOrder = 3, Active = true },
                new { UnitId = 4, Name = "set", Abbreviation = "set", SortOrder = 4, Active = true }
            );

            var userId = "20551684-b958-4581-af23-96c1528b0e29";
            var role_id1 = "faffc6d3-f72f-4b64-b208-3c7cfec71270";
            var role_id2 = "a08e13a5-00a8-4d7d-9aaf-c0d3a816e48b";
            var role_id3 = "9de4e55f-b26c-4b62-812a-cf52000b97bf";
            var role_id4 = "6ce169eb-8cfc-49da-9306-15e41ef13562";
            var role_id5 = "bcabe6d9-e245-40f8-ad4a-38f76ee73614";
            var role_id6 = "a56ff8b3-479f-4d1c-aed3-b1b7ec5d6998";
            var role_id7 = "a8a0c676-d58e-4be3-94db-ca7a5198692a";
            var role_id8 = "d6206540-4ba5-4dce-a608-37ba6523be27";
            var role_id9 = "d9fe7909-63eb-496f-a81c-bcff6f0456c5";
            var role_id10 = "f6bb4564-6919-484b-897b-4a2c994721e5";
            var access_group_id1 = 1;

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
                .HasData(new { Id = access_group_id1, Name = "Administrator" });

            modelBuilder.Entity<AccessGroupRole>()
                .HasData(new { AccessGroupId = access_group_id1, RoleId = role_id1 },
                         new { AccessGroupId = access_group_id1, RoleId = role_id2 },
                         new { AccessGroupId = access_group_id1, RoleId = role_id3 },
                         new { AccessGroupId = access_group_id1, RoleId = role_id4 },
                         new { AccessGroupId = access_group_id1, RoleId = role_id5 },
                         new { AccessGroupId = access_group_id1, RoleId = role_id6 },
                         new { AccessGroupId = access_group_id1, RoleId = role_id7 },
                         new { AccessGroupId = access_group_id1, RoleId = role_id8 },
                         new { AccessGroupId = access_group_id1, RoleId = role_id9 },
                         new { AccessGroupId = access_group_id1, RoleId = role_id10 }
                );

            modelBuilder.Entity<UserAccessGroup>()
                .HasData(new { UserId = userId, AccessGroupId = access_group_id1 });

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

        }
        #endregion



    }
}
