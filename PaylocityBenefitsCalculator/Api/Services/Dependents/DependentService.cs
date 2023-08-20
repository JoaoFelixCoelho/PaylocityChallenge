using Api.Dtos.Dependent;
using Api.Extensions;
using Api.Models;
using Api.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Api.Services.Dependents;

public class DependentService : IDBBasicOperations<InsertDependentDto, UpdateDependentDto, RemoveDependentDto, ReadDependentDto>, IGetOperations<GetDependentDto>, IDependentService
{
    private readonly SqliteDbContext _context;
    private readonly IMapper _mapper;

    public DependentService(SqliteDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    #region DB Basic Operations

    public async Task<ReadDependentDto> Insert(InsertDependentDto dependentDto)
    {
        try
        {
            var dependentsForValidation = _mapper.Map<DependentForRelationshipValidationDto>(dependentDto);
            await ValidateDependentsRelationship(dependentsForValidation);

            var dependent = _mapper.Map<Dependent>(dependentDto);

            await _context.AddAsync(dependent);
            await _context.SaveChangesAsync();

            var newDependent = _mapper.Map<ReadDependentDto>(dependent);
            return newDependent;
        }
        catch
        {
            throw;
        }
    }

    public async Task<ReadDependentDto> Update(UpdateDependentDto dependentDto)
    {
        try
        {
            var dependentsForValidation = _mapper.Map<DependentForRelationshipValidationDto>(dependentDto);
            await ValidateDependentsRelationship(dependentsForValidation);

            var dependent = _mapper.Map<Dependent>(dependentDto);

            _context.Update(dependent);
            await _context.SaveChangesAsync();

            var updatedDependent = _mapper.Map<ReadDependentDto>(dependent);
            return updatedDependent;
        }
        catch
        {
            throw;
        }
    }

    public async Task Delete(RemoveDependentDto dependentDto)
    {
        try
        {
            await _context.Dependents.Where(x => x.Id == dependentDto.Id).ExecuteDeleteAsync();
        }
        catch
        {
            throw;
        }
    }

    #endregion DB Basic Operations

    #region Get Operations

    public async Task<GetDependentDto> Get(int id)
    {
        try
        {
            var dependent = _context.Dependents.Find(id);
            var dependentDto = _mapper.Map<GetDependentDto>(dependent);

            return dependentDto;
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<GetDependentDto>> GetAll()
    {
        try
        {
            var allDependents = _context.Dependents.AsNoTracking().ToList();
            var allDependetsDto = _mapper.Map<List<GetDependentDto>>(allDependents);

            return allDependetsDto;
        }
        catch
        {
            throw;
        }

    }

    #endregion Get Operations

    public async Task<IEnumerable<DependentForRelationshipValidationDto>> GetDependentsForRelationshipValidation(int employeeId)
    {
        try
        {
            var dependents = (from dep in _context.Dependents
                              where
                                  dep.EmployeeId == employeeId
                              select new DependentForRelationshipValidationDto
                              {
                                  Id = dep.Id,
                                  EmployeeId = dep.EmployeeId,
                                  Relationship = dep.Relationship
                              }).ToList();

            return dependents;
        }
        catch
        {
            throw;
        }

    }

    private async Task<bool> ValidateDependentsRelationship(DependentForRelationshipValidationDto dependentsForValidation)
    {
        var employeeDependents = await GetDependentsForRelationshipValidation(dependentsForValidation.EmployeeId);

        var allDependents = new List<DependentForRelationshipValidationDto>() { dependentsForValidation };
        allDependents = allDependents.Union(employeeDependents).ToList();

        if (!DependentExtensions.ValidateDependents(allDependents))
        {
            throw new Exception("Relationship dependents error - more than 1 spouse or domestic partner");
        }

        return true;
    }
}

