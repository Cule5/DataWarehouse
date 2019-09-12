using System;
using System.Collections.Generic;
using System.Text;
using Core.Domain.Product;
using Core.Domain.Shop;
using Core.Domain.Transaction;
using Core.Domain.TransactionProduct;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()

        {
            


        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionProduct> TransactionProducts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer(@"Server=localhost;Database=DataWarehouse;Trusted_Connection=True;");
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(p => p.ProductId)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Transaction>()
                .Property(t=>t.TransactionId)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Shop>()
                .Property(s=>s.ShopId)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<TransactionProduct>()
                .HasKey(tp => new { tp.ProductId, tp.TransactionId });
            modelBuilder.Entity<Shop>()
                .HasMany(t => t.Transactions)
                .WithOne(s => s.Shop);
            modelBuilder.Entity<Transaction>()
                .HasMany(t => t.TransactionProducts)
                .WithOne(s => s.Transaction);
            modelBuilder.Entity<Product>()
                .HasMany(t => t.TransactionProducts)
                .WithOne(s => s.Product);

            
        }


    }
}
