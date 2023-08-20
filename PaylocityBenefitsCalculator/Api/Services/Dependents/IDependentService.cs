using Api.Dtos.Dependent;

namespace Api.Services.Dependents;

public interface IDependentService
{
    public Task<IEnumerable<DependentForRelationshipValidationDto>> GetDependentsForRelationshipValidation(int employeeId);
}

