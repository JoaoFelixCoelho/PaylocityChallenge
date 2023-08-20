using Api.Dtos.Paycheck;

namespace Api.Services.Paychecks;

public interface IPaycheckService
{
    public Task<EmployeePaycheckDto> CalculatePaycheck(int employeeId);
}

