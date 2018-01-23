using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Egret.Models;

namespace Egret.DataAccess
{
    public partial class EgretContext : DbContext  //: IdentityDbContext<User>
    {
        public EgretContext(DbContextOptions options) 
            : base(options) {}
        //public EgretContext(DbContextOptions<EgretContext> options)
        //    : base(options) { }

        public virtual DbSet<CurrencyType> CurrencyTypes { get; set; }
        public virtual DbSet<InventoryCategory> InventoryCategories { get; set; }
        public virtual DbSet<InventoryItem> InventoryItems { get; set; }
        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        //public virtual DbSet<Role> Roles { get; set; }
        //public virtual DbSet<DbUser> DbUsers { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .Property(u => u.Id)
                .HasMaxLength(450);
            
            modelBuilder.Entity<Role>()
                .Property(u => u.Id)
                .HasMaxLength(450);
            
            //modelBuilder.Entity<IdentityUserLogin<string>>()
            //.Property(l => l.LoginProvider)
            //.HasMaxLength(450);
            
            //modelBuilder.Entity<IdentityUserToken<string>>()
            //    .Property(t => t.LoginProvider)
            //    .HasMaxLength(450);
            
            //modelBuilder.Entity<IdentityUserToken<string>>()
            //    .Property(t => t.Name)
            //    .HasMaxLength(450);



            modelBuilder.HasPostgresExtension("adminpack");

            modelBuilder.Entity<CurrencyType>(entity =>
            {
                entity.ToTable("currency_types", "admin");

                entity.HasIndex(e => e.Abbreviation)
                    .HasName("currency_types_abbreviation_key")
                    .IsUnique();

                entity.HasIndex(e => e.Sortorder)
                    .HasName("currency_types_sort_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Abbreviation)
                    .IsRequired()
                    .HasColumnName("abbreviation");

                entity.Property(e => e.Active)
                    .HasColumnName("active");

                entity.Property(e => e.Defaultselection)
                    .HasColumnName("defaultselection");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.Sortorder)
                    .HasColumnName("sortorder")
                    .HasDefaultValueSql("nextval('admin.currency_types_sortorder_seq'::regclass)");

                entity.Property(e => e.Symbol)
                    .IsRequired()
                    .HasColumnName("symbol");
            });

            modelBuilder.Entity<InventoryCategory>(entity =>
            {
                entity.ToTable("inventory_categories", "admin");

                entity.HasIndex(e => e.Name)
                    .HasName("inventory_categories_name_key")
                    .IsUnique();

                entity.HasIndex(e => e.Sortorder)
                    .HasName("inventory_categories_sort_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('admin.inventory_categories_id_seq'::regclass)");

                entity.Property(e => e.Active)
                    .HasColumnName("active");

                entity.Property(e => e.Description)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.Sortorder)
                    .HasColumnName("sortorder");
            });

            modelBuilder.Entity<InventoryItem>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.ToTable("inventory_items");

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasDefaultValueSql("nextval('master_seq'::regclass)");

                entity.Property(e => e.Buycurrency).HasColumnName("buycurrency");

                entity.Property(e => e.Buyprice).HasColumnName("buyprice");

                entity.Property(e => e.Buyunit).HasColumnName("buyunit_fk");

                entity.Property(e => e.Category).HasColumnName("category");

                entity.Property(e => e.Cogacct).HasColumnName("cogacct");

                entity.Property(e => e.Comment).HasColumnName("comment");

                entity.Property(e => e.Conversionsource).HasColumnName("conversionsource");

                entity.Property(e => e.Costprice).HasColumnName("costprice");

                entity.Property(e => e.Dateadded).HasColumnName("dateadded");

                entity.Property(e => e.Dateupdated).HasColumnName("dateupdated");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description");

                entity.Property(e => e.Flags).HasColumnName("flags");

                entity.Property(e => e.Isconversion).HasColumnName("isconversion");

                entity.Property(e => e.Qtybrksellprice).HasColumnName("qtybrksellprice");

                entity.Property(e => e.Salesacct).HasColumnName("salesacct");

                entity.Property(e => e.Sellcurrency).HasColumnName("sellcurrency");

                entity.Property(e => e.Sellprice).HasColumnName("sellprice");

                entity.Property(e => e.Sellunit).HasColumnName("sellunit_fk");

                entity.Property(e => e.Sohcount).HasColumnName("sohcount");

                entity.Property(e => e.Stockacct).HasColumnName("stockacct");

                entity.Property(e => e.Stocktakenewqty).HasColumnName("stocktakenewqty");

                entity.Property(e => e.Stockvalue).HasColumnName("stockvalue");

                entity.Property(e => e.Supplier).HasColumnName("supplier_fk");

                entity.Property(e => e.Addedby).HasColumnName("useraddedby");

                entity.Property(e => e.Updatedby).HasColumnName("userupdatedby");

                entity.HasOne(d => d.BuycurrencyNavigation)
                    .WithMany(p => p.InventoryItemsBuycurrencyNavigation)
                    .HasPrincipalKey(p => p.Abbreviation)
                    .HasForeignKey(d => d.Buycurrency)
                    .HasConstraintName("inventory_items_buycurrency_fk")
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.BuyunitNavigation)
                    .WithMany(p => p.InventoryItemsBuyunitNavigation)
                    .HasPrincipalKey(p => p.Id)
                    .HasForeignKey(d => d.Buyunit)
                    .HasConstraintName("inventory_items_buyunit_fk")
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.CategoryNavigation)
                    .WithMany(p => p.InventoryItems)
                    .HasPrincipalKey(p => p.Name)
                    .HasForeignKey(d => d.Category)
                    .HasConstraintName("inventory_items_category_fk")
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.SellcurrencyNavigation)
                    .WithMany(p => p.InventoryItemsSellcurrencyNavigation)
                    .HasPrincipalKey(p => p.Abbreviation)
                    .HasForeignKey(d => d.Sellcurrency)
                    .HasConstraintName("inventory_items_sellcurrency_fk")
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.SellunitNavigation)
                    .WithMany(p => p.InventoryItemsSellunitNavigation)
                    .HasPrincipalKey(p => p.Id)
                    .HasForeignKey(d => d.Sellunit)
                    .HasConstraintName("inventory_items_sellunit_fk")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Unit>(entity =>
            {
                entity.ToTable("units", "admin");

                entity.HasIndex(e => e.Sortorder)
                    .HasName("units_sort_key")
                    .IsUnique();

                entity.HasIndex(e => e.Abbreviation)
                    .HasName("units_abbreviation_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('admin.units_id_seq'::regclass)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.Abbreviation)
                    .IsRequired()
                    .HasColumnName("abbreviation");

                entity.Property(e => e.Sortorder)
                    .HasColumnName("sortorder");

                entity.Property(e => e.Active)
                    .HasColumnName("active");

            });

            // This is a test to see if I can plug into the db users
            // instead of using identity

            //modelBuilder.Entity<DbUser>(entity =>
            //{
            //    entity.ToTable("pg_user", "pg_catalog");
            //
            //    entity.Property(e => e.Name)
            //        .HasColumnName("usename");
            //
            //    entity.Property(e => e.Usesysid)
            //        .HasColumnName("usesysid");
            //
            //    entity.Property(e => e.Usecreatedb)
            //        .HasColumnName("usecreateddb");
            //
            //    entity.Property(e => e.Userepl)
            //        .HasColumnName("userepl");
            //
            //    entity.Property(e => e.Usebypassrls)
            //        .HasColumnName("usebypassrls");
            //
            //    entity.Property(e => e.Valuntil)
            //        .HasColumnName("valuntil");
            //
            //    entity.Property(e => e.Useconfig)
            //        .HasColumnName("useconfig");
            //});

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("suppliers", "public");

                entity.HasIndex(e => e.Id)
                    .HasName("suppliers_pkey")
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
        }
    }
}
