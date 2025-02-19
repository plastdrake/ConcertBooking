using ConcertBooking.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBooking.Data
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<Concert> Concerts { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Performance> Performances { get; set; } = null!;
        public virtual DbSet<Booking> Bookings { get; set; } = null!;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Booking Entity
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(b => b.Id);

                entity.Property(b => b.Id).ValueGeneratedOnAdd();

                entity.HasOne(b => b.Performance)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(b => b.PerformanceId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(b => b.Customer)
                    .WithMany(c => c.Bookings)
                    .HasForeignKey(b => b.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure Concert Entity
            modelBuilder.Entity<Concert>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Id).ValueGeneratedOnAdd();

                entity.Property(c => c.Title)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(c => c.Description)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasMany(c => c.Performances)
                    .WithOne(p => p.Concert)
                    .HasForeignKey(p => p.ConcertId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure Performance Entity
            modelBuilder.Entity<Performance>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Id).ValueGeneratedOnAdd();

                entity.Property(p => p.PerformanceDateAndTime)
                    .IsRequired();

                entity.Property(p => p.Venue)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(p => p.City)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(p => p.Country)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasMany(p => p.Bookings)
                    .WithOne(b => b.Performance)
                    .HasForeignKey(b => b.PerformanceId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure Customer Entity
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Id).ValueGeneratedOnAdd();

                entity.Property(c => c.FirstName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(c => c.LastName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(c => c.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(c => c.Password)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.HasMany(c => c.Bookings)
                    .WithOne(b => b.Customer)
                    .HasForeignKey(b => b.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Seed data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder builder)
        {
            // Seed Concerts
            builder.Entity<Concert>().HasData(
                new Concert { Id = 1, Title = "Rock Legends", Description = "A night of legendary rock performances." },
                new Concert { Id = 2, Title = "Jazz Night", Description = "Smooth and soulful jazz performances." },
                new Concert { Id = 3, Title = "Pop Extravaganza", Description = "A showcase of the biggest pop hits." }
            );

            // Seed Performances
            builder.Entity<Performance>().HasData(
                new Performance { Id = 1, ConcertId = 1, PerformanceDateAndTime = new DateTime(2025, 6, 15, 19, 0, 0), Venue = "Madison Square Garden", City = "New York", Country = "USA" },
                new Performance { Id = 2, ConcertId = 1, PerformanceDateAndTime = new DateTime(2025, 6, 16, 20, 0, 0), Venue = "Staples Center", City = "Los Angeles", Country = "USA" },
                new Performance { Id = 3, ConcertId = 2, PerformanceDateAndTime = new DateTime(2025, 7, 10, 18, 30, 0), Venue = "Blue Note", City = "Chicago", Country = "USA" },
                new Performance { Id = 4, ConcertId = 3, PerformanceDateAndTime = new DateTime(2025, 8, 20, 21, 0, 0), Venue = "O2 Arena", City = "London", Country = "UK" }
            );

            // Seed Customers
            builder.Entity<Customer>().HasData(
                new Customer { Id = 1, FirstName = "Alice", LastName = "Johnson", Email = "alice.johnson@example.com", Password = "hashedpassword1" },
                new Customer { Id = 2, FirstName = "Bob", LastName = "Smith", Email = "bob.smith@example.com", Password = "hashedpassword2" },
                new Customer { Id = 3, FirstName = "Charlie", LastName = "Brown", Email = "charlie.brown@example.com", Password = "hashedpassword3" }
            );

            // Seed Bookings
            builder.Entity<Booking>().HasData(
                new Booking { Id = 1, PerformanceId = 1, CustomerId = 1 },
                new Booking { Id = 2, PerformanceId = 2, CustomerId = 2 },
                new Booking { Id = 3, PerformanceId = 3, CustomerId = 3 },
                new Booking { Id = 4, PerformanceId = 4, CustomerId = 1 }
            );
        }
    }
}
