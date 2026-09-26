using Microsoft.EntityFrameworkCore;
using CoffeeShopApi.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace CoffeeShopApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Еспресо", Price = 45.00m, Description = "Класичний міцний еспресо" },
                new Product { Id = 2, Name = "Капучино", Price = 65.00m, Description = "Еспресо з гарячим молоком та густою пінкою" },
                new Product { Id = 3, Name = "Лате", Price = 75.00m, Description = "Ніжний кавовий напій з великою кількістю молока" }
            );
        }
    }
}