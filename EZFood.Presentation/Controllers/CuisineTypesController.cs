
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EZFood.Application.Interfaces;
using EZFood.Domain.Entities.Models;
using EZFood.Shared.Dtos.CuisineType;

namespace MLM.Presentation.Controllers;

[ApiController]
[Route("api/cuisine-types")]
public class CuisineTypesController(IServiceManager serviceManager, ILogger<CuisineTypesController> logger) : ControllerBase
{
    private readonly IServiceManager _serviceManager = serviceManager;
    private readonly ILogger<CuisineTypesController> _logger = logger;

    [HttpGet]
    public async Task<ActionResult<CuisineType>> GetAllCuisineTypes()
    {
        try
        {
          IEnumerable<CuisineType>?  types = await _serviceManager.CuisineTypeService.GetAllCuisineTypesAsync();
            return Ok(types);
        }
        catch (Exception ex)
        {

            _logger.LogError(ex, " Error retrieving all pack types");
            return StatusCode(500, "An error occurred while retrieving pack types.");
        }

    }
    [HttpGet("{id}")]
    public async Task<ActionResult<CuisineType>> GetCuisineTypeById(Guid id)
    {
        try
        {
            CuisineType? type = await _serviceManager.CuisineTypeService.GetCuisineTypeByIdAsync(id);
            if (type == null)
            {
                return NotFound($"Cuisine Type with ID{id} not Found");
            }
            return Ok(type);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving cuisine type with ID {cuisineTypeId}", id);
            return StatusCode(500, "An error occurred while retrieving the cuisine type.");

        }

    }

    [HttpPost]
    public async Task<ActionResult<CuisineType>> CreateCuisineType([FromBody] CreateCuisineTypeDto typeDto)
    {
        try
        {
            CuisineType? createType = await _serviceManager.CuisineTypeService.CreateCuisineTypeAsync(typeDto);
            return Ok(createType);
        }
        catch (Exception ex) {
            _logger.LogError(ex, "Error retrieving cuisine type");
            return StatusCode(500, "An error occurred while retrieving the cuisine type.");

        }
            
    }
       

    [HttpPut("{id}")]
    public async Task<ActionResult<CuisineType>> UpdateCuisineType(Guid id,
       [FromBody] UpdateCuisineTypeDto updateDto
      )
    {
        try
        {
            CuisineType? updatedPackType = await _serviceManager.CuisineTypeService.UpdateCuisineTypeAsync(id, updateDto);

            if (updatedPackType == null)
            {
                return NotFound($"Cuisine type with ID {id} not found.");
            }

            return Ok(updatedPackType);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating cuisine type with ID {CuisineTypeId}", id);
            return StatusCode(500, "An error occurred while updating the cuisine type.");
        }

    }

    [HttpPatch("{id}/status")]
    //[Authorize]
    public async Task<ActionResult> UpdateCuisineTypeStatus(Guid id, [FromBody] CuisineTypeStatusDto cuisineTypeStatusDto)
    {
        try
        {
            bool result = await _serviceManager.CuisineTypeService.UpdateCuisineTypeStatusAsync(id, cuisineTypeStatusDto.Status);
            if (!result)
            {
                return NotFound($"Cuisine Type with ID {id} not found.");
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating status for cuisine type with ID {CuisineTypeId}", id);
            return StatusCode(500, "An error occurred while updating the cuisine type status.");
        }

    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePackType(Guid id)
    {

        try
        {
            bool result = await _serviceManager.CuisineTypeService.DeleteCuisineTypeAsync(id);

            if (!result)
            {
                return NotFound($"Cuisine Type with ID {id} not found.");
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting cuisine type with ID {CuisineTypeId}", id);
            return StatusCode(500, "An error occurred while deleting the cuisine type.");
        }

    }
    [HttpGet("active")]
    [AllowAnonymous]
    public async Task<ActionResult<CuisineType>> GetActivePackType()
    {
        try
        {
           IEnumerable<CuisineType?> packTypes = await _serviceManager.CuisineTypeService.GetActiveCuisineTypesAsync();
            return Ok(packTypes);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving active cuisine types");
            return StatusCode(500, "An error occurred while retrieving active cuisine types.");
        }
    }




}

