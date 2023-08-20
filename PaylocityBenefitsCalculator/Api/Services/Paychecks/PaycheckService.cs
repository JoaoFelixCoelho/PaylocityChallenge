using Api.Dtos.Employee;
using Api.Dtos.Paycheck;
using Api.Services.Employees;
using Api.Services.Paychecks;

namespace Api.Services.Dependents;

public class PaycheckService : IPaycheckService
{
    private readonly IEmployeeService _employeeService;

    const int MonthsPerYear = 12;
    const int PaychecksPerYear = 26;
    const int EmployeeBaseCost = 1000;
    const int PerDependentCost = 600;
    const int HighSalaryAdditionalCostLimit = 80000;
    const int HighSalaryAdditionalCostPercentage = 2;
    const int HighAgeLimitInYears = 50;
    const int HighAgeAdditionalCost = 200;

    public PaycheckService(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    public async Task<EmployeePaycheckDto> CalculatePaycheck(int employeeId)
    {
        try
        {
            var employee = await _employeeService.GetEmployeeForPaycheck(employeeId);

            var paycheck = GetMonthlyPaycheckBeforeDeductions(employee)
                    - GetEmployeeBaseCost()
                    - GetDependentsCosts(employee)
                    - GetHighSalaryAdditionalCost(employee)
                    - GetDependetsHighAgeAdditionalCost(employee);

            var finalPaycheck = Decimal.Round(paycheck, 2);

            return new EmployeePaycheckDto { EmployeeId = employeeId, PaycheckValue = finalPaycheck };
        }
        catch
        {
            throw;
        }
    }

    private decimal GetMonthlyPaycheckBeforeDeductions(GetEmployeeForPaycheckDto employee)
    {
        return (decimal)employee.Salary / PaychecksPerYear;
    }

    private decimal GetEmployeeBaseCost()
    {
        return (decimal)EmployeeBaseCost * MonthsPerYear / PaychecksPerYear;
    }

    private decimal GetDependentsCosts(GetEmployeeForPaycheckDto employee)
    {
        return (decimal)(employee.DependentsAge.Count * PerDependentCost) * MonthsPerYear / PaychecksPerYear;
    }

    private decimal GetHighSalaryAdditionalCost(GetEmployeeForPaycheckDto employee)
    {
        decimal costPercentage = (decimal)HighSalaryAdditionalCostPercentage / 100;
        
        return employee.Salary > HighSalaryAdditionalCostLimit 
            ? (decimal)GetMonthlyPaycheckBeforeDeductions(employee) * costPercentage : 0;
    }

    private decimal GetDependetsHighAgeAdditionalCost(GetEmployeeForPaycheckDto employee)
    {
        return (decimal)(employee.DependentsAge.Where(x => x > HighAgeLimitInYears).Count() * HighAgeAdditionalCost ) * MonthsPerYear / PaychecksPerYear;
    }
}

