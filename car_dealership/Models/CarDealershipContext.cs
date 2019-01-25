using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace car_dealership.Models
{
    public partial class CarDealershipContext : DbContext
    {
        public CarDealershipContext()
        {
        }

        public CarDealershipContext(DbContextOptions<CarDealershipContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Inventory> Inventory { get; set; }
        public virtual DbSet<Manufacturer> Manufacturer { get; set; }
        public virtual DbSet<Model> Model { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=CYD12E\\SQLEXPRESS;Database=CarDealership;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.HasKey(e => e.StockId);

                entity.Property(e => e.StockId)
                    .HasColumnName("stockId")
                    .ValueGeneratedNever();

                entity.Property(e => e.ModelModelName)
                    .HasColumnName("model_modelName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModelModelYear).HasColumnName("model_modelYear");

                entity.Property(e => e.Qty).HasColumnName("qty");

                entity.HasOne(d => d.ModelModel)
                    .WithMany(p => p.Inventory)
                    .HasForeignKey(d => new { d.ModelModelName, d.ModelModelYear })
                    .HasConstraintName("FK__Inventory__1BC821DD");
            });

            modelBuilder.Entity<Manufacturer>(entity =>
            {
                entity.HasKey(e => e.Make);

                entity.Property(e => e.Make)
                    .HasColumnName("make")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Origin)
                    .HasColumnName("origin")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Model>(entity =>
            {
                entity.HasKey(e => new { e.ModelName, e.ModelYear });

                entity.Property(e => e.ModelName)
                    .HasColumnName("modelName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModelYear).HasColumnName("modelYear");

                entity.Property(e => e.Category)
                    .HasColumnName("category")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Horsepower).HasColumnName("horsepower");

                entity.Property(e => e.ManufacturerMake)
                    .HasColumnName("manufacturer_make")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Torque).HasColumnName("torque");

                entity.HasOne(d => d.ManufacturerMakeNavigation)
                    .WithMany(p => p.Model)
                    .HasForeignKey(d => d.ManufacturerMake)
                    .HasConstraintName("FK__Model__manufactu__18EBB532");
            });
        }
    }
}
