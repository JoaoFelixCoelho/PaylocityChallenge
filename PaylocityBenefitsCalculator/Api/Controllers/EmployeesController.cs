using Api.Dtos.Employee;
using Api.Models;
using Api.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IDBBasicOperations<InsertEmployeeDto, UpdateEmployeeDto, RemoveEmployeeDto, ReadEmployeeDto> _dbBasicOperations;
    private readonly IGetOperations<GetEmployeeDto> _getOperations;

    public EmployeesController(IDBBasicOperations<InsertEmployeeDto, UpdateEmployeeDto, RemoveEmployeeDto, ReadEmployeeDto> dbBasicOperations, 
        IGetOperations<GetEmployeeDto> getOperations)
    {
        _dbBasicOperations = dbBasicOperations;
        _getOperations = getOperations;
    }

    [SwaggerOperation(Summary = "Insert employee")]
    [HttpPost("Insert")]
    public async Task<ActionResult<ApiResponse<ReadEmployeeDto>>> Insert(InsertEmployeeDto employeeDto)
    {
        try
        {
            var employee = await _dbBasicOperations.Insert(employeeDto);
            return new ApiResponse<ReadEmployeeDto> 
            {
                Data = employee
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<ReadEmployeeDto>
            {
                Message = "An error occurred when executing the - Insert employee",
                Success = false,
                Error = ex.Message
            };
        }
    }

    [SwaggerOperation(Summary = "Update employee")]
    [HttpPost("Update")]
    public async Task<ActionResult<ApiResponse<ReadEmployeeDto>>> Update(UpdateEmployeeDto employeeDto)
    {
        try
        {
            var employee = await _dbBasicOperations.Update(employeeDto);
            return new ApiResponse<ReadEmployeeDto> 
            {
                Data = employee
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<ReadEmployeeDto>
            {
                Message = "An error occurred when executing the - Update employee",
                Success = false,
                Error = ex.Message
            };
        }
    }

    [SwaggerOperation(Summary = "Delete employee")]
    [HttpPost("Delete")]
    public async Task<ActionResult<ApiResponse<bool>>> Delete(RemoveEmployeeDto employeeDto)
    {
        try
        {
            await _dbBasicOperations.Delete(employeeDto);
            return new ApiResponse<bool> { Data = true };
        }
        catch (Exception ex)
        {
            return new ApiResponse<bool>
            {
                Message = "An error occurred when executing the - Delete employee",
                Success = false,
                Error = ex.Message
            };
        }
    }

    [SwaggerOperation(Summary = "Get employee by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> Get(int id)
    {
        try
        {
            var employee = await _getOperations.Get(id);

            if (employee == null)
                return NotFound();

            return new ApiResponse<GetEmployeeDto>
            {
                Data = employee,
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<GetEmployeeDto>
            {
                Message = "An error occurred when executing the - Get employee by id",
                Success = false,
                Error = ex.Message
            };
        }
    }

    [SwaggerOperation(Summary = "Get all employees")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetEmployeeDto>>>> GetAll()
    {
        try
        {
            var allEmployees = await _getOperations.GetAll();

            return new ApiResponse<List<GetEmployeeDto>>
            {
                Data = (List<GetEmployeeDto>)allEmployees,
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<GetEmployeeDto>>
            {
                Message = "An error occurred when executing the - Get all employees",
                Success = false,
                Error = ex.Message
            };
        }
    }
}
