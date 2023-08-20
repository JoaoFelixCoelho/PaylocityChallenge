using Api.Dtos.Employee;
using Api.Dtos.Paycheck;
using Api.Services.Dependents;
using Api.Services.Employees;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;


namespace ApiTests.UnitTests
{
    public class PaycheckServiceTest
    {
        private readonly Mock<IEmployeeService> _employeeServiceMock;
        private readonly PaycheckService _paycheckService;

        public PaycheckServiceTest()
        {
            _employeeServiceMock = new Mock<IEmployeeService>();
            _paycheckService = new PaycheckService(_employeeServiceMock.Object);
        }

        [Fact]
        public async Task GetMonthlyPaycheck_NoAddedRestrictions_ReturnCorrectResult()
        {
            var employee = new GetEmployeeForPaycheckDto
            {
                Id = 1,
                Age = 30,
                Salary = 50000
            };

            var expected = new EmployeePaycheckDto
            {
                EmployeeId = employee.Id,
                PaycheckValue = 1461.54M
            };

            _employeeServiceMock.Setup(x => x.GetEmployeeForPaycheck(employee.Id)).ReturnsAsync(employee);
            var result = await _paycheckService.CalculatePaycheck(employee.Id);

            Assert.Equal(expected.PaycheckValue, result.PaycheckValue);
        }

        [Fact]
        public async Task GetMonthlyPaycheck_WithDependentsRestrictions_ReturnCorrectResult()
        {
            var employee = new GetEmployeeForPaycheckDto
            {
                Id = 1,
                Age = 30,
                Salary = 75000,
                DependentsAge = new List<int>() { 31, 15 }
            };

            var expected = new EmployeePaycheckDto
            {
                EmployeeId = employee.Id,
                PaycheckValue = 1869.23M
            };

            _employeeServiceMock.Setup(x => x.GetEmployeeForPaycheck(employee.Id)).ReturnsAsync(employee);
            var result = await _paycheckService.CalculatePaycheck(employee.Id);

            Assert.Equal(expected.PaycheckValue, result.PaycheckValue);
        }

        [Fact]
        public async Task GetMonthlyPaycheck_WithDependentsAndHighSalaryRestrictions_ReturnCorrectResult()
        {
            var employee = new GetEmployeeForPaycheckDto
            {
                Id = 1,
                Age = 30,
                Salary = 85000,
                DependentsAge = new List<int>() { 31 }
            };

            var expected = new EmployeePaycheckDto
            {
                EmployeeId = employee.Id,
                PaycheckValue = 2465.38M
            };

            _employeeServiceMock.Setup(x => x.GetEmployeeForPaycheck(employee.Id)).ReturnsAsync(employee);
            var result = await _paycheckService.CalculatePaycheck(employee.Id);

            Assert.Equal(expected.PaycheckValue, result.PaycheckValue);
        }

        [Fact]
        public async Task GetMonthlyPaycheck_WithAgeDependentsAndHighSalaryRestrictions_ReturnCorrectResult()
        {
            var employee = new GetEmployeeForPaycheckDto
            {
                Id = 1,
                Age = 53,
                Salary = 93000,
                DependentsAge = new List<int>() { 51, 20 }
            };

            var expected = new EmployeePaycheckDto
            {
                EmployeeId = employee.Id,
                PaycheckValue = 2397.69M
            };

            _employeeServiceMock.Setup(x => x.GetEmployeeForPaycheck(employee.Id)).ReturnsAsync(employee);
            var result = await _paycheckService.CalculatePaycheck(employee.Id);

            Assert.Equal(expected.PaycheckValue, result.PaycheckValue);
        }
    }
}
