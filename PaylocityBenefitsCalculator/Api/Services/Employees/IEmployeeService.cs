using Api.Dtos.Employee;

namespace Api.Services.Employees;

public interface IEmployeeService
{
    public Task<GetEmployeeForPaycheckDto> GetEmployeeForPaycheck(int employeeId);
}

