using Microsoft.EntityFrameworkCore;

namespace Flowsell.Internship.LinqTask;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Person> People { get; set; }
    public DbSet<Organisation> Organisations { get; set; }
    public DbSet<Employee> Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //var dataSeeder = new DataSeeder();
        //modelBuilder.Entity<Person>().HasData(dataSeeder.People);
        //modelBuilder.Entity<Organisation>().HasData(dataSeeder.Organisations);
        //modelBuilder.Entity<Employee>().HasData(dataSeeder.Employees);
    }
}