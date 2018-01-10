using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Egret.DataAccess
{
    public partial class EgretContext : DbContext
    {
        public virtual DbSet<CurrencyType> CurrencyTypes { get; set; }
        public virtual DbSet<InventoryCategory> InventoryCategories { get; set; }
        public virtual DbSet<InventoryItem> InventoryItems { get; set; }
        public virtual DbSet<Unit> Units { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql(@"Server=localhost;Database=Egret;User Id=postgres;Password=postgres;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.Defaultselection).HasColumnName("defaultselection");

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

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.Sortorder).HasColumnName("sortorder");
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

                entity.Property(e => e.Buyunit).HasColumnName("buyunit");

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

                entity.Property(e => e.Sellunit).HasColumnName("sellunit");

                entity.Property(e => e.Sohcount).HasColumnName("sohcount");

                entity.Property(e => e.Stockacct).HasColumnName("stockacct");

                entity.Property(e => e.Stocktakenewqty).HasColumnName("stocktakenewqty");

                entity.Property(e => e.Stockvalue).HasColumnName("stockvalue");

                entity.Property(e => e.SupplierFk).HasColumnName("supplier_fk");

                entity.Property(e => e.Addedby).HasColumnName("useraddedby");

                entity.Property(e => e.Updatedby).HasColumnName("userupdatedby");

                entity.HasOne(d => d.BuycurrencyNavigation)
                    .WithMany(p => p.InventoryItemsBuycurrencyNavigation)
                    .HasPrincipalKey(p => p.Abbreviation)
                    .HasForeignKey(d => d.Buycurrency)
                    .HasConstraintName("inventory_items_buycurrency_fk");

                entity.HasOne(d => d.BuyunitNavigation)
                    .WithMany(p => p.InventoryItemsBuyunitNavigation)
                    .HasPrincipalKey(p => p.Abbreviation)
                    .HasForeignKey(d => d.Buyunit)
                    .HasConstraintName("inventory_items_buyunit_fk");

                entity.HasOne(d => d.CategoryNavigation)
                    .WithMany(p => p.InventoryItems)
                    .HasPrincipalKey(p => p.Name)
                    .HasForeignKey(d => d.Category)
                    .HasConstraintName("inventory_items_category_fk");

                entity.HasOne(d => d.SellcurrencyNavigation)
                    .WithMany(p => p.InventoryItemsSellcurrencyNavigation)
                    .HasPrincipalKey(p => p.Abbreviation)
                    .HasForeignKey(d => d.Sellcurrency)
                    .HasConstraintName("inventory_items_sellcurrency_fk");

                entity.HasOne(d => d.SellunitNavigation)
                    .WithMany(p => p.InventoryItemsSellunitNavigation)
                    .HasPrincipalKey(p => p.Abbreviation)
                    .HasForeignKey(d => d.Sellunit)
                    .HasConstraintName("inventory_items_sellunit_fk");
            });

            modelBuilder.Entity<Unit>(entity =>
            {
                entity.ToTable("units", "admin");

                entity.HasIndex(e => e.Abbreviation)
                    .HasName("units_abbreviation_key")
                    .IsUnique();

                entity.HasIndex(e => e.Sortorder)
                    .HasName("units_sort_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('admin.units_id_seq'::regclass)");

                entity.Property(e => e.Abbreviation)
                    .IsRequired()
                    .HasColumnName("abbreviation");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.Sortorder).HasColumnName("sortorder");
            });
        }
    }
}
