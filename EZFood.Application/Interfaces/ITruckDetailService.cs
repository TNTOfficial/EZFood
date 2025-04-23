
using EZFood.Domain.Entities.Enums;
using EZFood.Domain.Entities.Models;
using EZFood.Shared.Dtos.CuisineType;
using EZFood.Shared.Dtos.TruckDetail;

namespace EZFood.Application.Interfaces;

public interface ITruckDetailService
{
    Task<IEnumerable<TruckDetail>> GetAllTruckDetailsAsync();
    Task<IEnumerable<TruckDetail>> GetPendingTruckDetailsAsync();
    Task<IEnumerable<TruckDetail>> GetApprovedTruckDetailsAsync();
    Task<TruckDetail?> GetTruckDetailByIdAsync(Guid id);
    Task<TruckDetail> CreateTruckDetailAsync(CreateTruckDetailDto createPackTypeDto);
    Task<StepResponseDto> CreateStepOneAsync(CreateStepOneDto detailDto);
    Task<StepResponseDto> CreateStepTwoAsync(CreateStepTwoDto detailDto);
    Task<StepResponseDto> CreateStepThreeAsync(CreateStepThreeDto detailDto);
    Task<TruckDetail?> UpdateTruckDetailAsync(Guid id, UpdateTruckDetailDto updatePackTypeDto);
    Task<bool> DeleteTruckDetailAsync(Guid id);
    Task<bool> UpdateOnboardingStatusAsync(Guid id, OnboardingStatus onboardingStatus);
    Task<bool> UpdateTruckDetailStatusAsync(Guid id, bool status);
}
