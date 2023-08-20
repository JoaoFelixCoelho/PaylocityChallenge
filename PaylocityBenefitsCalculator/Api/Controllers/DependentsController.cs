using Api.Dtos.Dependent;
using Api.Models;
using Api.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DependentsController : ControllerBase
{
    private readonly IDBBasicOperations<InsertDependentDto, UpdateDependentDto, RemoveDependentDto, ReadDependentDto> _dbBasicOperations;
    private readonly IGetOperations<GetDependentDto> _getOperations;

    public DependentsController(IDBBasicOperations<InsertDependentDto, UpdateDependentDto, RemoveDependentDto, ReadDependentDto> dbBasicOperations, IGetOperations<GetDependentDto> getOperations)
    {
        _dbBasicOperations = dbBasicOperations;
        _getOperations = getOperations;
    }

    [SwaggerOperation(Summary = "Insert dependent")]
    [HttpPost("Insert")]
    public async Task<ActionResult<ApiResponse<ReadDependentDto>>> Insert(InsertDependentDto dependentDto)
    {
        try
        {
            var dependent = await _dbBasicOperations.Insert(dependentDto);
            return new ApiResponse<ReadDependentDto> 
            { 
                Data = dependent
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<ReadDependentDto>
            {
                Message = "An error occurred when executing the - Insert dependent",
                Success = false,
                Error = ex.Message
            };
        }
    }

    [SwaggerOperation(Summary = "Update dependent")]
    [HttpPost("Update")]
    public async Task<ActionResult<ApiResponse<ReadDependentDto>>> Update(UpdateDependentDto dependentDto)
    {
        try
        {
            var dependent = await _dbBasicOperations.Update(dependentDto);
            return new ApiResponse<ReadDependentDto> 
            {
                Data = dependent
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<ReadDependentDto>
            {
                Message = "An error occurred when executing the - Update dependent",
                Success = false,
                Error = ex.Message
            };
        }
    }

    [SwaggerOperation(Summary = "Delete dependent")]
    [HttpPost("Delete")]
    public async Task<ActionResult<ApiResponse<bool>>> Delete(RemoveDependentDto dependentDto)
    {
        try
        {
            await _dbBasicOperations.Delete(dependentDto);
            return new ApiResponse<bool> { Data = true };
        }
        catch (Exception ex)
        {
            return new ApiResponse<bool>
            {
                Message = "An error occurred when executing the - Delete dependent",
                Success = false,
                Error = ex.Message
            };
        }
    }

    [SwaggerOperation(Summary = "Get dependent by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetDependentDto>>> Get(int id)
    {
        try
        {
            var dependent = await _getOperations.Get(id);

            if (dependent == null)
                return NotFound();

            return new ApiResponse<GetDependentDto>
            {
                Data = dependent,
            };
        }
        catch (Exception ex)
        {
            var result = new ApiResponse<GetDependentDto>
            {
                Message = "An error occurred when executing the - Get dependent by id",
                Success = false,
                Error = ex.Message
            };

            return result;
        }
    }

    [SwaggerOperation(Summary = "Get all dependents")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetDependentDto>>>> GetAll()
    {
        try
        {
            var allDependents = await _getOperations.GetAll();
            return new ApiResponse<List<GetDependentDto>>
            {
                Data = (List<GetDependentDto>)allDependents,
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<GetDependentDto>>
            {
                Message = "An error occurred when executing the - Get all dependents",
                Success = false,
                Error = ex.Message
            };
        }
    }
}
