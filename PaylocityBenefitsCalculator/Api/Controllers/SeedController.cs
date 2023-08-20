using Api.Models;
using Api.Services.SeedHelper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class SeedController : ControllerBase
{
    private readonly ISeedHelper _seedHelper;

    public SeedController(ISeedHelper seedHelper)
    {
        _seedHelper = seedHelper;
    }

    [SwaggerOperation(Summary = "Seed all BD tables")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<bool>>> Seed()
    {
        try
        {
            await _seedHelper.SeedDataBase();
            return new ApiResponse<bool>
            {
                Data = true,
                Message = "DB seed executed with success",
                Success = true
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<bool>
            {
                Data = false,
                Message = "An error occurred when executing the DB seed",
                Success = false,
                Error = ex.Message
            };
        }
    }
}
