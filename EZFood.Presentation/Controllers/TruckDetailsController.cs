
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EZFood.Application.Interfaces;
using EZFood.Domain.Entities.Models;
using EZFood.Shared.Dtos.CuisineType;
using EZFood.Shared.Dtos.TruckDetail;
using EZFood.Domain.Entities.Enums;

namespace MLM.Presentation.Controllers;

[ApiController]
[Authorize(Roles = "Seller")]
[Route("api/truck-details")]
public class TruckDetailsController(IServiceManager serviceManager, ILogger<TruckDetailsController> logger) : ControllerBase
{
    private readonly IServiceManager _serviceManager = serviceManager;
    private readonly ILogger<TruckDetailsController> _logger = logger;

    [HttpGet]
    public async Task<ActionResult<TruckDetail>> GetAllTruckDetails()
    {
        try
        {
          IEnumerable<TruckDetail>?  truckDetails = await _serviceManager.TruckDetailService.GetAllTruckDetailsAsync();
            return Ok(truckDetails);
        }
        catch (Exception ex)
        {

            _logger.LogError(ex, " Error retrieving all truck details");
            return StatusCode(500, "An error occurred while retrieving truck details.");
        }

    }

    [HttpGet("truck-detail-steps")]
    public async Task<ActionResult<StepsResponseDto>> GetTruckDetailSteps()
    {
        try
        {
            StepsResponseDto truckDetails = await _serviceManager.TruckDetailService.GetTruckDetailStepsAsync();
            return Ok(truckDetails);
        }
        catch (Exception ex)
        {

            _logger.LogError(ex, " Error retrieving all truck details");
            return StatusCode(500, "An error occurred while retrieving truck details.");
        }

    }
    [HttpGet("{id}")]
    public async Task<ActionResult<TruckDetail>> GetTruckDetailById(Guid id)
    {
        try
        {
            TruckDetail? type = await _serviceManager.TruckDetailService.GetTruckDetailByIdAsync(id);
            if (type == null)
            {
                return NotFound($"Truck details with ID{id} not Found");
            }
            return Ok(type);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving truck details with ID {TruckDetailId}", id);
            return StatusCode(500, "An error occurred while retrieving the truck details.");

        }

    }

    [HttpPost("step-one-data")]
    public async Task<ActionResult<TruckDetail>> CreateStepOne([FromBody] CreateStepOneDto detailDto)
    {
        try
        {
            StepResponseDto? response = await _serviceManager.TruckDetailService.CreateStepOneAsync(detailDto);
            return Ok(response);
        }
        catch (Exception ex) {
            _logger.LogError(ex, "Error creating/updating truck detail step 1");
            return Ok(new StepResponseDto
            {
                Result = false,
                OnboardingStatus = OnboardingStatus.Step1,
                Message = ex.Message
            });
        }            
    }


    [HttpPost("step-two-data")]
    public async Task<ActionResult<TruckDetail>> CreateStepTwo([FromBody] CreateStepTwoDto detailDto)
    {
        try
        {
            StepResponseDto? response = await _serviceManager.TruckDetailService.CreateStepTwoAsync(detailDto);
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating/updating truck detail step 2");
            return Ok(new StepResponseDto
            {
                Result = false,
                OnboardingStatus = OnboardingStatus.Step1,
                Message = ex.Message
            });
        }
    }

    [HttpPost("step-three-data")]
    public async Task<ActionResult<TruckDetail>> CreateStepThree([FromForm] CreateStepThreeDto detailDto)
    {
        try
        {
            StepResponseDto? response = await _serviceManager.TruckDetailService.CreateStepThreeAsync(detailDto);
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating/updating truck detail step 3");
            return Ok(new StepResponseDto
            {
                Result = false,
                OnboardingStatus = OnboardingStatus.Step1,
                Message = ex.Message
            });
        }
    }


    [HttpPut("{id}")]
    public async Task<ActionResult<TruckDetail>> UpdateTruckDetail(Guid id,
       [FromBody] UpdateTruckDetailDto updateDto
      )
    {
        try
        {
            TruckDetail? updatedTruckDetail = await _serviceManager.TruckDetailService.UpdateTruckDetailAsync(id, updateDto);

            if (updatedTruckDetail == null)
            {
                return NotFound($"truck details with ID {id} not found.");
            }

            return Ok(updatedTruckDetail);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating truck details with ID {TruckDetailId}", id);
            return StatusCode(500, "An error occurred while updating the  truck details.");
        }

    }


}

