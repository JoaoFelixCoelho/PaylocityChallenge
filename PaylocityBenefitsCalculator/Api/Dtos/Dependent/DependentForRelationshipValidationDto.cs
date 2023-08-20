using Api.Models;

namespace Api.Dtos.Dependent;

public class DependentForRelationshipValidationDto
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public Relationship Relationship { get; set; }
}
