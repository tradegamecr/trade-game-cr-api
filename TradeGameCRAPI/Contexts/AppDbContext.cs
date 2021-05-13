﻿using Microsoft.EntityFrameworkCore;
using TradeGameCRAPI.Entities;

namespace TradeGameCRAPI.Contexts
{
    public class AppDbContext: BaseAppDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Deal> Deals { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Deal>()
                .HasOne(e => e.Retailer)
                .WithMany(e => e.Retails)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
