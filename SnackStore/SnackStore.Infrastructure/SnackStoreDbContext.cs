using Microsoft.EntityFrameworkCore;
using SnackStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnackStore.Infrastructure
{
    public class SnackStoreDbContext: DbContext
    {
        public SnackStoreDbContext(DbContextOptions<SnackStoreDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductLike>()
                .HasKey(pl => new { pl.ProductId, pl.AccountId });

            modelBuilder.Entity<ProductLike>()
                .HasOne(pl => pl.Product)
                .WithMany(p => p.ProductLikes)
                .HasForeignKey(a => a.ProductId);

            modelBuilder.Entity<ProductLike>()
                .HasOne(pl => pl.Account)
                .WithMany(p => p.ProductLikes)
                .HasForeignKey(a => a.AccountId);

            modelBuilder.Entity<Product>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Account>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<PriceLog>()
               .Property(p => p.Id)
               .ValueGeneratedOnAdd();

            modelBuilder.Entity<PurchaseLog>()
               .Property(p => p.Id)
               .ValueGeneratedOnAdd();
        }



        public DbSet<Product> Product { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<ProductLike> ProductLikes { get; set; }

        public DbSet<PriceLog> PriceLog { get; set; }
        public DbSet<PurchaseLog> PurchaseLog { get; set; }
    }
}
