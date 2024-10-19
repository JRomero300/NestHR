using Microsoft.EntityFrameworkCore;
using NestHR.Model;
using System.Collections.Generic;

namespace NestHR.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}