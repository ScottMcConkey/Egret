using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Egret_Dev.EF
{
    public partial class EgretContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseNpgsql(@"Server=localhost;Database=Egret;User Id=postgres;Password=postgres;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ConsumptionEvents>(entity =>
            {
                entity.HasKey(e => e.Eventid)
                    .HasName("PK_consumption_events");

                entity.ToTable("consumption_events");

                entity.Property(e => e.Eventid)
                    .HasColumnName("eventid")
                    .HasDefaultValueSql("nextval('consumption_event_consumptionid_seq'::regclass)");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.Dateentered).HasColumnName("dateentered");

                entity.Property(e => e.Itemid).HasColumnName("itemid");

                entity.Property(e => e.Projectid).HasColumnName("projectid");

                entity.Property(e => e.Qtyconsumed).HasColumnName("qtyconsumed");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ConsumptionEvents)
                    .HasForeignKey(d => d.Projectid)
                    .HasConstraintName("consumption_events_projectid_fkey");
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.Customerid)
                    .HasName("PK_customers");

                entity.ToTable("customers");

                entity.Property(e => e.Customerid).HasColumnName("customerid");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.Address).HasColumnName("address");

                entity.Property(e => e.Business).HasColumnName("business");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Phonenumber)
                    .HasColumnName("phonenumber")
                    .HasColumnType("varchar")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<InventoryCategory>(entity =>
            {
                entity.ToTable("inventory_categories", "admin");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('admin.inventory_categories_id_seq'::regclass)");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.Sortorder).HasColumnName("sortorder");
            });

            modelBuilder.Entity<InventoryItem>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK_inventory_items");

                entity.ToTable("inventory_items");

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Buyingunit).HasColumnName("buyingunit");

                entity.Property(e => e.Buyingunitid).HasColumnName("buyingunitid");

                entity.Property(e => e.Buyingunitprice).HasColumnName("buyingunitprice");

                entity.Property(e => e.Cogacct).HasColumnName("cogacct");

                entity.Property(e => e.Comment).HasColumnName("comment");

                entity.Property(e => e.Costprice).HasColumnName("costprice");

                entity.Property(e => e.Createdbyuser).HasColumnName("createdbyuser");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description");

                entity.Property(e => e.Flags).HasColumnName("flags");

                entity.Property(e => e.Lastupdatedbyuser).HasColumnName("lastupdatedbyuser");

                entity.Property(e => e.MigrCounted).HasColumnName("migr_counted");

                entity.Property(e => e.Minbuildqty).HasColumnName("minbuildqty");

                entity.Property(e => e.Normalbuildqty).HasColumnName("normalbuildqty");

                entity.Property(e => e.Salesacct).HasColumnName("salesacct");

                entity.Property(e => e.Sellingunit).HasColumnName("sellingunit");

                entity.Property(e => e.Sellingunitid).HasColumnName("sellingunitid");

                entity.Property(e => e.Sellingunitprice).HasColumnName("sellingunitprice");

                entity.Property(e => e.Sohcount).HasColumnName("sohcount");

                entity.Property(e => e.Stockacct).HasColumnName("stockacct");

                entity.Property(e => e.Stockonhand).HasColumnName("stockonhand");

                entity.Property(e => e.Stocktakenewqty).HasColumnName("stocktakenewqty");

                entity.Property(e => e.Stockvalue).HasColumnName("stockvalue");

                entity.Property(e => e.Supplierid).HasColumnName("supplierid");

                entity.HasOne(d => d.CreatedbyuserNavigation)
                    .WithMany(p => p.InventoryItems)
                    .HasForeignKey(d => d.Createdbyuser)
                    .HasConstraintName("inventory_items_sellingunitid_fkey");
            });

            modelBuilder.Entity<ProjectItem>(entity =>
            {
                entity.HasKey(e => new { e.Projectid, e.Itemid })
                    .HasName("PK_project_items");

                entity.ToTable("project_items");

                entity.Property(e => e.Projectid).HasColumnName("projectid");

                entity.Property(e => e.Itemid).HasColumnName("itemid");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectItems)
                    .HasForeignKey(d => d.Projectid)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("project_items_projectid_fkey");
            });

            modelBuilder.Entity<Projects>(entity =>
            {
                entity.HasKey(e => e.Projectid)
                    .HasName("PK_projects");

                entity.ToTable("projects");

                entity.Property(e => e.Projectid).HasColumnName("projectid");

                entity.Property(e => e.Customerid).HasColumnName("customerid");

                entity.Property(e => e.Productid).HasColumnName("productid");

                entity.Property(e => e.Productqty).HasColumnName("productqty");

                entity.Property(e => e.Startdate)
                    .HasColumnName("startdate")
                    .HasColumnType("date");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.Customerid)
                    .HasConstraintName("projects_customerid_fkey");
            });

            modelBuilder.Entity<Purchases>(entity =>
            {
                entity.HasKey(e => e.Purchaseid)
                    .HasName("PK_purchases");

                entity.ToTable("purchases");

                entity.Property(e => e.Purchaseid).HasColumnName("purchaseid");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.Dateentered).HasColumnName("dateentered");

                entity.Property(e => e.Itemid).HasColumnName("itemid");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("money");

                entity.Property(e => e.Projectid).HasColumnName("projectid");

                entity.Property(e => e.Qtypurchased).HasColumnName("qtypurchased");

                entity.Property(e => e.Unitid).HasColumnName("unitid");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(d => d.Projectid)
                    .HasConstraintName("purchases_projectid_fkey");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(d => d.Unitid)
                    .HasConstraintName("purchases_unitid_fkey");
            });

            modelBuilder.Entity<Units>(entity =>
            {
                entity.ToTable("units", "admin");

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

            modelBuilder.HasSequence("inventory_categories_id_seq", "admin");

            modelBuilder.HasSequence("units_id_seq", "admin");

            modelBuilder.HasSequence("consumption_event_consumptionid_seq");
        }

        public virtual DbSet<ConsumptionEvents> ConsumptionEvents { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<InventoryCategory> InventoryCategories { get; set; }
        public virtual DbSet<InventoryItem> InventoryItems { get; set; }
        public virtual DbSet<ProjectItem> ProjectItems { get; set; }
        public virtual DbSet<Projects> Projects { get; set; }
        public virtual DbSet<Purchases> Purchases { get; set; }
        public virtual DbSet<Units> Units { get; set; }

        // Unable to generate entity type for table 'public.migrate_inventory_old'. Please see the warning messages.
        // Unable to generate entity type for table 'public.migrate_inventory'. Please see the warning messages.
    }
}