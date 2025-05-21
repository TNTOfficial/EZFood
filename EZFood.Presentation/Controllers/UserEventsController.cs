
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EZFood.Application.Interfaces;
using EZFood.Domain.Entities.Models;
using EZFood.Shared.Dtos.CuisineType;
using EZFood.Shared.Dtos.Response;
using EZFood.Shared.Dtos.UserEvent;

namespace MLM.Presentation.Controllers;
[Authorize]
[ApiController]
[Route("api/user-events")]
public class UserEventsController(IServiceManager serviceManager, ILogger<UserEventsController> logger) : ControllerBase
{
    private readonly IServiceManager _serviceManager = serviceManager;
    private readonly ILogger<UserEventsController> _logger = logger;   

    
    [HttpPost]
    public async Task<ActionResult<ResponseDto>> CreateCuisineType([FromBody] List<CreateUserEventDto> updateDto)
    {
        try
        {
            ResponseDto res = await _serviceManager.UserEventService.UpdateUserEventsAsync(updateDto);
            return Ok(res);
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

