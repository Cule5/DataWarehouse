using System;
using System.Collections.Generic;
using System.Text;
using Core.Domain.Product;
using Core.Domain.Shop;
using Core.Domain.Transaction;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework
{
    class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=DataWarehouse;Trusted_Connection=True;");
        }
    }
}
