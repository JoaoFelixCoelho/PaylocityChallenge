using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using AutoMapper;

namespace Api.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Employee, GetEmployeeDto>().ReverseMap();
        CreateMap<Employee, InsertEmployeeDto>().ReverseMap();
        CreateMap<ReadEmployeeDto, Employee>().ReverseMap();
        CreateMap<Employee, UpdateEmployeeDto>().ReverseMap();
        CreateMap<Dependent, GetDependentDto>().ReverseMap();
        CreateMap<ReadDependentDto, Dependent>().ReverseMap();
        CreateMap<Dependent, InsertDependentDto>().ReverseMap();
        CreateMap<Dependent, UpdateDependentDto>().ReverseMap();
        CreateMap<InsertDependentDto, DependentForRelationshipValidationDto>().ReverseMap();
        CreateMap<UpdateDependentDto, DependentForRelationshipValidationDto>().ReverseMap();
        CreateMap<InsertEmployeeDto, DependentForRelationshipValidationDto>().ReverseMap();
    }
}