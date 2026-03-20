using MakersMarkt.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace MakersMarkt.Data
{
    class AppDbContext : DbContext
    {
        // Tabellen definiëren
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ComplexityLevel> ComplexityLevels { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                ConfigurationManager.ConnectionStrings["MakersMarktDb"].ConnectionString,
                ServerVersion.Parse("8.0.30")
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Seed Roles
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "Maker" },
                new Role { Id = 3, Name = "Buyer" }
            );

            // 2. Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Woodwork" },
                new Category { Id = 2, Name = "Electronics" },
                new Category { Id = 3, Name = "Ceramics" }
            );

            // 3. Seed Complexity Levels
            modelBuilder.Entity<ComplexityLevel>().HasData(
                new ComplexityLevel { Id = 1, Name = "Beginner" },
                new ComplexityLevel { Id = 2, Name = "Intermediate" },
                new ComplexityLevel { Id = 3, Name = "Advanced" }
            );

            // 4. Seed Order Statuses
            modelBuilder.Entity<OrderStatus>().HasData(
                new OrderStatus { Id = 1, Name = "Pending" },
                new OrderStatus { Id = 2, Name = "In Production" },
                new OrderStatus { Id = 3, Name = "Shipped" },
                new OrderStatus { Id = 4, Name = "Delivered" }
            );

            // 5. Seed een voorbeeld Maker (User)
            // Let op: In een echte app zou je wachtwoorden hashen!
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "MasterMaker",
                    Password = "HashedPassword123", // In productie: hash dit!
                    Email = "info@mastermaker.com", // Voeg deze regel toe
                    RoleId = 2,
                    IsVerified = true,
                    CreatedAt = new DateTime(2024, 1, 1),
                    Credits = 0m
                });

            // 6. Seed een voorbeeld Product
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    MakerId = 1,
                    CategoryId = 1,
                    ComplexityLevelId = 2,
                    Name = "Handcrafted Oak Table",
                    Description = "A beautiful solid oak table.",
                    Materials = "Oak wood, Oil finish",
                    ProductionTimeDays = 14,

                    // Deze velden zijn verplicht volgens je model:
                    DurabilityInfo = "Lasts for generations with proper waxing.",
                    UniqueFeatures = "Natural wood grain patterns, no two are the same.",

                    IsApproved = true,
                    CreatedAt = new DateTime(2024, 1, 5)
                }
            );
        }
    }
}