using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Models.Framework
{
    public partial class ShopDbContext : DbContext
    {
        public ShopDbContext()
            : base("name=ShopDbContext")
        {
        }

        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<tbChiTietDDH> tbChiTietDDHs { get; set; }
        public virtual DbSet<tbDonDatHang> tbDonDatHangs { get; set; }
        public virtual DbSet<tbNhaCungCap> tbNhaCungCaps { get; set; }
        public virtual DbSet<tbUser> tbUsers { get; set; }
        public virtual DbSet<tbXe> tbXes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tbChiTietDDH>()
                .Property(e => e.TongTien)
                .HasPrecision(19, 4);

            modelBuilder.Entity<tbDonDatHang>()
                .HasMany(e => e.tbChiTietDDHs)
                .WithRequired(e => e.tbDonDatHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbXe>()
                .Property(e => e.GiaBan)
                .HasPrecision(19, 4);

            modelBuilder.Entity<tbXe>()
                .HasMany(e => e.tbChiTietDDHs)
                .WithRequired(e => e.tbXe)
                .WillCascadeOnDelete(false);
        }
    }
}
