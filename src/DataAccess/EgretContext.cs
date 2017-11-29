using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Egret.Controllers;

namespace Egret.DataAccess
{
    public partial class EgretContext : DbContext
    {
        public virtual DbSet<InventoryCategories> InventoryCategories { get; set; }
        public virtual DbSet<InventoryItems> InventoryItems { get; set; }
        public virtual DbSet<Units> Units { get; set; }

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

            modelBuilder.Entity<InventoryCategories>(entity =>
            {
                entity.ToTable("inventory_categories", "admin");

                entity.HasIndex(e => e.Name)
                    .HasName("inventory_categories_name_key")
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

            modelBuilder.Entity<InventoryItems>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.ToTable("inventory_items");

                entity.Property(e => e.Code).HasColumnName("code");

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

                entity.Property(e => e.Useraddedby).HasColumnName("useraddedby");

                entity.Property(e => e.Userlastupdatedby).HasColumnName("userlastupdatedby");
            });

            modelBuilder.Entity<Units>(entity =>
            {
                entity.ToTable("units", "admin");

                entity.HasIndex(e => e.Sortorder)
                    .HasName("units_sortorder_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('admin.units_id_seq'::regclass)");

                entity.Property(e => e.Abbreviation)
                    .IsRequired()
                    .HasColumnName("abbreviation");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.Sortorder).HasColumnName("sortorder");
            });
        }
    }
}
