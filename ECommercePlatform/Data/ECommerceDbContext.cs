using ECommercePlatform.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommercePlatform.Data
{
    public class ECommerceDbContext:IdentityDbContext<IdentityUser>
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options):base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().Property(p => p.Price).HasPrecision(18, 2); // Precision: 18 digits, Scale: 2 decimal places
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics" },
                new Category { Id = 2, Name = "Clothing" });
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Smartphone", Description = "Latest model", Price = 699.99M, CategoryId = 1 },
                new Product { Id = 2, Name = "Shirt", Description = "Cotton material", Price = 19.99M, CategoryId = 2 });
            modelBuilder.Entity<Order>().Property(o => o.TotalAmount)
                .HasPrecision(18, 2);
            modelBuilder.Entity<OrderItem>().Property(o => o.Price)
                .HasPrecision(18, 2);
            modelBuilder.Entity<CartItem>().Property(p => p.Price).HasPrecision(18, 2);

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
    }
}
