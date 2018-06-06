﻿using System;
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
    public partial class EgretContext : IdentityDbContext<AppUser>
    {
        public EgretContext(DbContextOptions<EgretContext> options) 
            : base(options) {}

        public virtual DbSet<CurrencyType> CurrencyTypes { get; set; }
        public virtual DbSet<InventoryCategory> InventoryCategories { get; set; }
        public virtual DbSet<InventoryItem> InventoryItems { get; set; }
        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>()
                .ToTable("asp_net_users")
                .Property(u => u.Id)
                .HasMaxLength(450);
            
            modelBuilder.Entity<Role>()
                .ToTable("asp_net_roles")
                .Property(u => u.Id)
                .HasMaxLength(450);

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

                entity.Property(e => e.DateAdded).HasColumnName("dateadded");

                entity.Property(e => e.Addedby).HasColumnName("useraddedby");

                entity.Property(e => e.Dateupdated).HasColumnName("dateupdated");

                entity.Property(e => e.Updatedby).HasColumnName("userupdatedby");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description");

                entity.Property(e => e.Category).HasColumnName("category");

                entity.Property(e => e.CustomerPurchasedFor).HasColumnName("customer_purchasedfor");

                entity.Property(e => e.CustomerReservedFor).HasColumnName("customer_reservedfor");


                entity.Property(e => e.Buycurrency).HasColumnName("buycurrency");

                entity.Property(e => e.Buyprice).HasColumnName("buyprice");

                entity.Property(e => e.Buyunit).HasColumnName("buyunit_fk");

                

                entity.Property(e => e.Comment).HasColumnName("comment");

                entity.Property(e => e.ConversionSource).HasColumnName("conversionsource");

                

                

                


                entity.Property(e => e.Sellcurrency).HasColumnName("sellcurrency");

                entity.Property(e => e.Sellprice).HasColumnName("sellprice");

                entity.Property(e => e.Sellunit).HasColumnName("sellunit_fk");

                entity.Property(e => e.Supplier).HasColumnName("supplier_fk");

                

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
