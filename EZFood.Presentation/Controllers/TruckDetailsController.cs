
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EZFood.Application.Interfaces;
using EZFood.Domain.Entities.Models;
using EZFood.Shared.Dtos.CuisineType;
using EZFood.Shared.Dtos.TruckDetail;
using EZFood.Domain.Entities.Enums;
using EZFood.Shared.Dtos.Response;
using EZFood.Shared.Dtos.TruckDetail.Steps;

namespace MLM.Presentation.Controllers;

[ApiController]
[Authorize(Roles = "Admin, Seller")]
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
    public async Task<ActionResult<StepResponse<StepOne>>> CreateStepOne([FromBody] CreateStepOneDto detailDto)
    {
        try
        {
            StepResponse<StepOne> response = await _serviceManager.TruckDetailService.CreateStepOneAsync(detailDto);
            return Ok(response);
        }
        catch (Exception ex) {
            _logger.LogError(ex, "Error creating/updating truck detail step 1");
            return Ok(StepResponse<StepOne>.ErrorResult(OnboardingStatus.Step1, ex.Message));
        }            
    }


    [HttpPost("step-two-data")]
    public async Task<ActionResult<StepResponse<StepTwo>>> CreateStepTwo([FromBody] CreateStepTwoDto detailDto)
    {
        try
        {
            StepResponse<StepTwo> response = await _serviceManager.TruckDetailService.CreateStepTwoAsync(detailDto);
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating/updating truck detail step 2");
            return Ok(StepResponse<StepTwo>.ErrorResult(OnboardingStatus.Step2, ex.Message));            
        }
    }

    [HttpPost("step-three-data")]
    public async Task<ActionResult<StepResponse<StepThree>>> CreateStepThree([FromForm] CreateStepThreeDto detailDto)
    {
        try
        {
            StepResponse<StepThree> response = await _serviceManager.TruckDetailService.CreateStepThreeAsync(detailDto);
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating/updating truck detail step 3");
            return Ok(StepResponse<StepThree>.ErrorResult(OnboardingStatus.Step3, ex.Message));           
        }
    }

    [HttpPost("step-three-file")]
    public async Task<ActionResult<StepResponse<string>>> UpdateStepThreeFile([FromForm] CreateStepThreeFileDto detailDto)
    {
        try
        {
            StepResponse<string> response = await _serviceManager.TruckDetailService.UpdateStepThreeFileAsync(detailDto);
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating/updating truck detail step 3");
            return Ok(StepResponse<string>.ErrorResult(OnboardingStatus.Step3, ex.Message));
        }
    }

    [HttpPost("step-four-data")]
    public async Task<ActionResult<StepResponse<StepFour>>> CreateStepFour([FromForm] CreateStepFourDto detailDto)
    {
        try
        {
            StepResponse<StepFour> response = await _serviceManager.TruckDetailService.CreateStepFourAsync(detailDto);
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating/updating truck detail step 4 data");
            return Ok(new StepResponseDto
            {
                Result = false,
                OnboardingStatus = OnboardingStatus.Step1,
                Message = ex.Message
            });
        }
    }

    [HttpPost("step-five-data")]
    public async Task<ActionResult<StepResponse<StepFive>>> CreateStepFive([FromForm] CreateStepFourDto detailDto)
    {
        try
        {
            StepResponse<StepFive> response = await _serviceManager.TruckDetailService.CreateStepFiveAsync(detailDto);
            return Ok(response);
        }
        catch (Exception ex)    
        {
            _logger.LogError(ex, "Error creating/updating truck detail step 5 data");
            return Ok(new StepResponseDto
            {
                Result = false,
                OnboardingStatus = OnboardingStatus.Step5,
                Message = ex.Message
            });
        }
    }

    [HttpGet("submit-for-review")]
    public async Task<ActionResult<ResponseDto>> SubmitForReview()
    {
        try
        {
            ResponseDto response = await _serviceManager.TruckDetailService.SubmitForReviewAsync();
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating/updating truck detail step 5 data");
            return Ok(new ResponseDto
            {
                Result = false,
                Message = ex.Message
            });
        }
    }


    [HttpDelete("delete-step-four-image/{id}")]
    public async Task<IActionResult> DeleteStepFourImage(int id)
    {
        var stepFourResponse = await _serviceManager.TruckDetailService.DeleteStepFourImage(id);       
        return Ok(stepFourResponse);
    }




    [HttpDelete("delete-step-five-file/{id}")]
    public async Task<IActionResult> DeleteStepFiveFile(int id)
    {
        var stepFiveResponse = await _serviceManager.TruckDetailService.DeleteStepFiveFile(id);
        return Ok(stepFiveResponse);
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

