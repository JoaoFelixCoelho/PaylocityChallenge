using Api.Dtos.Paycheck;
using Api.Models;
using Api.Services.Paychecks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PaycheckController : ControllerBase
{
    private readonly IPaycheckService _paycheckService;

    public PaycheckController(IPaycheckService paycheckService)
    {
        _paycheckService = paycheckService;
    }

    [SwaggerOperation(Summary = "Get paycheck by employeeId")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<EmployeePaycheckDto>>> Get(int id)
    {
        try
        {
            var paycheck = await _paycheckService.CalculatePaycheck(id);

            return new ApiResponse<EmployeePaycheckDto>
            {
                Data = paycheck,
                Success = true
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<EmployeePaycheckDto>
            {
                Message = "An error occurred getting the paycheck",
                Success = false,
                Error = ex.Message
            };
        }
    }
}
