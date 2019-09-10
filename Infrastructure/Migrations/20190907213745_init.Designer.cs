﻿// <auto-generated />
using System;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20190907213745_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Core.Domain.Product.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<double>("Price");

                    b.Property<int>("Quantity");

                    b.HasKey("ProductId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Core.Domain.Shop.Shop", b =>
                {
                    b.Property<int>("ShopId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City");

                    b.Property<string>("Name");

                    b.Property<string>("PostCode");

                    b.Property<int>("Type");

                    b.HasKey("ShopId");

                    b.ToTable("Shops");
                });

            modelBuilder.Entity("Core.Domain.Transaction.Transaction", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City");

                    b.Property<DateTime>("Date");

                    b.Property<int>("PaymentType");

                    b.Property<string>("PostCode");

                    b.Property<int?>("ShopId");

                    b.HasKey("TransactionId");

                    b.HasIndex("ShopId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Core.Domain.TransactionProduct.TransactionProduct", b =>
                {
                    b.Property<int>("ProductId");

                    b.Property<int>("TransactionId");

                    b.HasKey("ProductId", "TransactionId");

                    b.HasIndex("TransactionId");

                    b.ToTable("TransactionProducts");
                });

            modelBuilder.Entity("Core.Domain.Transaction.Transaction", b =>
                {
                    b.HasOne("Core.Domain.Shop.Shop", "Shop")
                        .WithMany("Transactions")
                        .HasForeignKey("ShopId");
                });

            modelBuilder.Entity("Core.Domain.TransactionProduct.TransactionProduct", b =>
                {
                    b.HasOne("Core.Domain.Product.Product", "Product")
                        .WithMany("TransactionProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Core.Domain.Transaction.Transaction", "Transaction")
                        .WithMany("TransactionProducts")
                        .HasForeignKey("TransactionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
