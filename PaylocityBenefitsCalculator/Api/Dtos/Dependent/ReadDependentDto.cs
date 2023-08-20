using Api.Models;

namespace Api.Dtos.Dependent;

public class ReadDependentDto
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Relationship Relationship { get; set; }
}
