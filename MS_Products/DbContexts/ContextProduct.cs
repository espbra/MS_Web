using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MS_Products.Models;

namespace MS_Products.DbContexts
{
    public class ContextProduct : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public ContextProduct(DbContextOptions<ContextProduct> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Category Cat_Electronics = new Category() { Id = 1, Name = "Electronics", Description = "Electronic Items", };
            Category Cat_Clothes = new Category() { Id = 2, Name = "Clothes", Description = "Dresses", };
            Category Cat_Grocery = new Category() { Id = 3, Name = "Grocery", Description = "Grocery Items" };

            Product Prd_StereoSmall  = new Product() { Id = 1, Name = "Small Stereo",  CategoryId = 1, Description = "This is a small stereo", Price = 100.00 };
            Product Prd_StereoMedium = new Product() { Id = 2, Name = "Medium Stereo", CategoryId = 1, Description = "This is a medium stereo", Price = 200.00 };
            Product Prd_StereoLarge  = new Product() { Id = 3, Name = "Large Stereo",  CategoryId = 1, Description = "This is a large stereo", Price = 300.00 };

            modelBuilder.Entity<Category>().HasData(
                Cat_Electronics,
                Cat_Clothes,
                Cat_Grocery
            );

            modelBuilder.Entity<Product>().HasData(
                Prd_StereoSmall,
                Prd_StereoMedium,
                Prd_StereoLarge
            );
        }
    }
}
