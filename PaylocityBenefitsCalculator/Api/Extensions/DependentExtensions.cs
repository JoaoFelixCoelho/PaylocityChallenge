using Api.Dtos.Dependent;
using Api.Models;

namespace Api.Extensions;

public static class DependentExtensions
{
    //An employee may only have 1 spouse or domestic partner (not both)
    public static bool ValidateDependents(IEnumerable<DependentForRelationshipValidationDto> dependents)
    {
        if (dependents == null)
            return true;

        int spouseOrDPCount = dependents.Count(x => x.Relationship == Relationship.Spouse || x.Relationship == Relationship.DomesticPartner);

        if (spouseOrDPCount > 1)
            return false;

        return true;
    }
}

