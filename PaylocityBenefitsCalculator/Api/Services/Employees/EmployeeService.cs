using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Extensions;
using Api.Models;
using Api.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace Api.Services.Employees;

public class EmployeeService : IDBBasicOperations<InsertEmployeeDto, UpdateEmployeeDto, RemoveEmployeeDto, ReadEmployeeDto>, IGetOperations<GetEmployeeDto>, IEmployeeService
{
    private readonly SqliteDbContext _context;
    private readonly IMapper _mapper;

    public EmployeeService(SqliteDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    #region DB Basic Operations

    public async Task<ReadEmployeeDto> Insert(InsertEmployeeDto employeeDto)
    {
        try
        {
            var employee = _mapper.Map<Employee>(employeeDto);

            await _context.AddAsync(employee);
            await _context.SaveChangesAsync();

            var newEmployee = _mapper.Map<ReadEmployeeDto>(employee);
            return newEmployee;
        }
        catch
        {
            throw;
        }
    }

    public async Task<ReadEmployeeDto> Update(UpdateEmployeeDto employeeDto)
    {
        try
        {
            var employee = _mapper.Map<Employee>(employeeDto);

            _context.Update(employee);
            await _context.SaveChangesAsync();

            var updatedEmployee = _mapper.Map<ReadEmployeeDto>(employee);
            return updatedEmployee;
        }
        catch
        {
            throw;
        }
    }

    public async Task Delete(RemoveEmployeeDto employeeDto)
    {
        try
        {
            await _context.Employees.Where(x => x.Id == employeeDto.Id).ExecuteDeleteAsync();
        }
        catch
        {
            throw;
        }
    }

    #endregion DB Basic Operations

    #region Get Operations

    public async Task<GetEmployeeDto> Get(int id)
    {
        try
        {
            var employee = await _context.Employees.AsNoTracking().Where(x => x.Id == id)
                                      .Include(x => x.Dependents).AsNoTracking()
                                      .FirstOrDefaultAsync();

            var employeeDto = _mapper.Map<GetEmployeeDto>(employee);

            return employeeDto;
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<GetEmployeeDto>> GetAll()
    {
        try
        {
            var allEmployees = _context.Employees.AsNoTracking()
                                       .Include(x => x.Dependents).AsNoTracking()
                                       .ToList();
            var allEmployeesDto = _mapper.Map<List<GetEmployeeDto>>(allEmployees);

            return allEmployeesDto;
        }
        catch
        {
            throw;
        }

    }

    public async Task<GetEmployeeForPaycheckDto> GetEmployeeForPaycheck(int id)
    {
        try
        {
            var employee = (from emp in _context.Employees
                           from dep in _context.Dependents.Where(x => x.EmployeeId == emp.Id).DefaultIfEmpty()
                           where emp.Id == id
                           select new
                           {
                               emp.Id,
                               emp.DateOfBirth,
                               emp.Salary,
                               DependentsBD = emp.Dependents.Select(x => x.DateOfBirth)
                           }).ToList().GroupBy(x => new { x.Id, x.DateOfBirth, x.Salary})
                           .Select(y =>
                           {
                               var emp = y.First();

                               return new GetEmployeeForPaycheckDto
                               {
                                   Id = emp.Id,
                                   Age = GenericUtil.GetAge(emp.DateOfBirth),
                                   Salary = emp.Salary,
                                   DependentsAge = emp.DependentsBD.Select(y => GenericUtil.GetAge(y)).ToList()
                               };
                           }).ToList();

            return employee.FirstOrDefault();
        }
        catch
        {
            throw;
        }
    }

    #endregion Get Operations
}

