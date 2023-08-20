using Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Api;

public class SqliteDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Dependent> Dependents { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("FileName=BenefitsDB", option => 
        {
            option.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
        });

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>().ToTable("Employees");
        modelBuilder.Entity<Dependent>().ToTable("Dependents");

        base.OnModelCreating(modelBuilder);
    }
}

