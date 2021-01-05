using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Web_ASP.NET_MVC.Models
{
    public partial class ShopFashionContext : DbContext
    {
        public ShopFashionContext()
            : base("name=ShopFashionContext")
        {
        }

        public virtual DbSet<Administrator> Administrators { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<FeedBack> FeedBacks { get; set; }
        public virtual DbSet<FSOrder> FSOrders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductCetegory> ProductCetegories { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Slide> Slides { get; set; }
        public virtual DbSet<WebUser> WebUsers { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<FSOrder>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.FSOrder)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Product>()
                .Property(e => e.PromotionPrice)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Reviews)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WebUser>()
                .Property(e => e.Account)
                .IsUnicode(false);

            modelBuilder.Entity<WebUser>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<WebUser>()
                .HasMany(e => e.FSOrders)
                .WithRequired(e => e.WebUser)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderDetail>()
                .Property(e => e.TotalPrice)
                .HasPrecision(18, 0);
        }
    }
}
