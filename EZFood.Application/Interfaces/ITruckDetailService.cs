
using EZFood.Domain.Entities.Enums;
using EZFood.Domain.Entities.Models;
using EZFood.Shared.Dtos.CuisineType;
using EZFood.Shared.Dtos.Response;
using EZFood.Shared.Dtos.TruckDetail;
using EZFood.Shared.Dtos.TruckDetail.Steps;

namespace EZFood.Application.Interfaces;

public interface ITruckDetailService
{
    Task<IEnumerable<TruckDetail>> GetAllTruckDetailsAsync();
    Task<StepsResponseDto> GetTruckDetailStepsAsync();
    Task<IEnumerable<TruckDetail>> GetPendingTruckDetailsAsync();
    Task<IEnumerable<TruckDetail>> GetApprovedTruckDetailsAsync();
    Task<TruckDetail?> GetTruckDetailByIdAsync(Guid id);
    Task<TruckDetail> CreateTruckDetailAsync(CreateTruckDetailDto createPackTypeDto);
    Task<StepResponse<StepOne>> CreateStepOneAsync(CreateStepOneDto detailDto);
    Task<StepResponse<StepTwo>> CreateStepTwoAsync(CreateStepTwoDto detailDto);
    Task<StepResponse<StepThree>> CreateStepThreeAsync(CreateStepThreeDto detailDto);
    Task<StepResponseDto> CreateStepFourAsync(CreateStepFourDto detailDto);
    Task<TruckDetail?> UpdateTruckDetailAsync(Guid id, UpdateTruckDetailDto updatePackTypeDto);
    Task<bool> DeleteTruckDetailAsync(Guid id);
    Task<bool> UpdateOnboardingStatusAsync(Guid id, OnboardingStatus onboardingStatus);
    Task<bool> UpdateTruckDetailStatusAsync(Guid id, bool status);
}
