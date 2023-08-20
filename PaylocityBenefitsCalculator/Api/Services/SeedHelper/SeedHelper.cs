using Api.Models;

namespace Api.Services.SeedHelper;

public class SeedHelper : ISeedHelper
{
    private readonly SqliteDbContext _context;

    public SeedHelper(SqliteDbContext context)
    {
        _context = context;
    }

    public async Task<bool> SeedDataBase()
    {
        try
        {
            await _context.Database.EnsureDeletedAsync();
            await _context.Database.EnsureCreatedAsync();

            await _context.Employees.AddRangeAsync(new Employee[]
            {
                new Employee() { Id = 1, FirstName = "LeBron", LastName = "James", Salary = 75420.99m, DateOfBirth = new DateTime(1984, 12, 30)},
                new Employee() { Id = 2, FirstName = "Ja", LastName = "Morant", Salary = 92365.22m, DateOfBirth = new DateTime(1999, 8, 10)},
                new Employee() { Id = 3, FirstName = "Michael", LastName = "Jordan", Salary = 143211.12m, DateOfBirth = new DateTime(1963, 2, 17)},
            });

            await _context.Dependents.AddRangeAsync(new Dependent[]
            {
                new Dependent() { Id = 1, FirstName = "Spouse", LastName = "Morant", DateOfBirth = new DateTime(1998, 3, 3), Relationship = Relationship.Spouse, EmployeeId = 2},
                new Dependent() { Id = 2, FirstName = "Child1", LastName = "Morant", DateOfBirth = new DateTime(2020, 6, 23), Relationship = Relationship.Child, EmployeeId = 2},
                new Dependent() { Id = 3, FirstName = "Child2", LastName = "Morant", DateOfBirth = new DateTime(2021, 5, 18), Relationship = Relationship.Child, EmployeeId = 2},
                new Dependent() { Id = 4, FirstName = "DP", LastName = "Jordan", DateOfBirth = new DateTime(1974, 1, 2), Relationship = Relationship.DomesticPartner, EmployeeId = 3}
            });

            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            throw;
        }
    }
}

