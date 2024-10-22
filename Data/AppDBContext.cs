using Microsoft.EntityFrameworkCore;
using NestHR.Model;
using System.Collections.Generic;

namespace NestHR.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasKey(e => e.EmployeeId); // Set EmployeeId as the primary key

            modelBuilder.Entity<Employee>()
                .Property(e => e.EmployeeId)
                .ValueGeneratedOnAdd(); // This ensures it is an identity column
        }
    }
}