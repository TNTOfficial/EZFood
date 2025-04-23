
using EZFood.Shared.Exceptions;
using EZFood.Application.Interfaces;
using EZFood.Domain.Entities.Models;
using EZFood.Infrastructure.Persistence.Interfaces;
using EZFood.Shared.Dtos.CuisineType;
using EZFood.Domain.Entities.Enums;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using EZFood.Shared.Dtos.TruckDetail;
using MLM.Application.Services;

namespace EZFood.Application.Services;

public class TruckDetailService : ITruckDetailService
{

    private readonly IRepositoryManager _repositoryManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private Guid _userId = Guid.Empty;
    private readonly IFileStorageService _fileStorageService;

    public TruckDetailService(IRepositoryManager repositoryManager, IHttpContextAccessor httpContextAccessor, IFileStorageService fileStorageService)
    {
        _repositoryManager = repositoryManager;
        _httpContextAccessor = httpContextAccessor;
        _fileStorageService = fileStorageService;
        string? userId = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == "userId")?.Value;
        if (userId != null)
        {
            _userId = Guid.Parse(userId);
        }
    }
    public async Task<IEnumerable<TruckDetail>> GetAllTruckDetailsAsync()
    {
        return await _repositoryManager.TruckDetail.GetAllTruckDetailsAsync();
    }

    public async Task<IEnumerable<TruckDetail>> GetPendingTruckDetailsAsync()
    {
        return await _repositoryManager.TruckDetail.GetPendingTruckDetaisAsync();
    }

    public async Task<IEnumerable<TruckDetail>> GetApprovedTruckDetailsAsync()
    {
        return await _repositoryManager.TruckDetail.GetAllTruckDetailsAsync();
    }

    public async  Task<CuisineType?> GetCuisineTypeByIdAsync(Guid id)
    {
        CuisineType? cuisineType = await _repositoryManager.CuisineType.GetCuisineTypeByIdAsync(id);
        if (cuisineType == null)
        {
            throw new EZFoodException("Cuisine type id not found.");
        }
        return cuisineType;
    }

    public async Task<StepResponseDto> CreateStepOneAsync(CreateStepOneDto detailDto)
    {
            TruckDetail? existingTruck = await _repositoryManager.TruckDetail.getTruckDetailByUserAsync(_userId);
            if (existingTruck != null)
            {
                existingTruck.TruckName = detailDto.TruckName;
                existingTruck.TruckOwnerName = detailDto.TruckOwnerName;
                existingTruck.PhoneNumber = detailDto.PhoneNumber;
                existingTruck.Address = detailDto.Address;
                existingTruck.BusinessEmail = detailDto.BusinessEmail;
                existingTruck.OnboardingStatus = OnboardingStatus.Step2;
                existingTruck.UpdatedAt = DateTime.UtcNow;
                _repositoryManager.TruckDetail.Update(existingTruck);
                await _repositoryManager.SaveAsync();
            return new StepResponseDto
                {
                    OnboardingStatus = OnboardingStatus.Step2,
                    Message = "Step 1 details updated successfully."
                };
            }
            else
            {
                TruckDetail truckDetail = new TruckDetail
                {
                    Id = Guid.NewGuid(),
                    TruckName = detailDto.TruckName,
                    TruckOwnerName = detailDto.TruckOwnerName,
                    PhoneNumber = detailDto.PhoneNumber,
                    Address = detailDto.Address,
                    BusinessEmail = detailDto.BusinessEmail,
                    OnboardingStatus = OnboardingStatus.Step2,
                    UserId = _userId,
                    UpdatedAt = DateTime.UtcNow
                };
                _repositoryManager.TruckDetail.CreateTruckDetailAsync(truckDetail);
                await _repositoryManager.SaveAsync();
                return new StepResponseDto
                {
                    OnboardingStatus = OnboardingStatus.Step2,
                    Message = "Step 1 details updated successfully."
                };
            }
        
    }

    public async Task<StepResponseDto> CreateStepTwoAsync(CreateStepTwoDto detailDto)
    {
        TruckDetail? existingTruck = await _repositoryManager.TruckDetail.getTruckDetailByUserAsync(_userId);
        if (existingTruck != null)
        {
            List<CuisineType> cuisines = [.. _repositoryManager.CuisineType.FindByCondition(x => detailDto.Cuisines.Contains(x.Id), false)];
             
            existingTruck.IsOtherCuisine = detailDto.IsOtherCuisine;
            existingTruck.CuisineNote = detailDto.IsOtherCuisine ? detailDto.CuisineNote : null;

            existingTruck.OnboardingStatus = OnboardingStatus.Step3;
            existingTruck.UpdatedAt = DateTime.UtcNow;
            if(cuisines != null)
            {
                await _repositoryManager.CuisineTypeTruckDetail.DeleteRecordsAsync(existingTruck.Id);
                await _repositoryManager.SaveAsync();
                existingTruck.CuisineTypes = cuisines;
            }
            _repositoryManager.TruckDetail.Update(existingTruck);
            await _repositoryManager.SaveAsync();
            return new StepResponseDto
            {
                OnboardingStatus = OnboardingStatus.Step3,
                Message = "Step 2 details updated successfully."
            };
        }
        else
        {           
            return new StepResponseDto
            {
                Result = false,
                OnboardingStatus = OnboardingStatus.Step2,
                Message = "Step 2 details could not be updated."
            };
        }

    }

    public async Task<StepResponseDto> CreateStepThreeAsync(CreateStepThreeDto detailDto)
    {
        TruckDetail? existingTruck = await _repositoryManager.TruckDetail.getTruckDetailByUserAsync(_userId);
        if (existingTruck != null)
        {
            if (detailDto.COI is not null)
            {
                if (!string.IsNullOrEmpty(existingTruck.COI))
                {
                    _fileStorageService.DeleteFile(existingTruck.COI);
                }
                string subDirectory = $"documents/COI";
                existingTruck.COI = await _fileStorageService.SaveFileAsync(detailDto.COI!, subDirectory, "coi");
            }

            if (detailDto.W9 is not null)
            {
                if (!string.IsNullOrEmpty(existingTruck.W9))
                {
                    _fileStorageService.DeleteFile(existingTruck.W9);
                }
                string subDirectory = $"documents/W9";
                existingTruck.W9 = await _fileStorageService.SaveFileAsync(detailDto.W9!, subDirectory, "W9");
            }

            if (detailDto.DCHCertificate is not null)
            {
                if (!string.IsNullOrEmpty(existingTruck.DCHCertificate))
                {
                    _fileStorageService.DeleteFile(existingTruck.DCHCertificate);
                }
                string subDirectory = $"documents/DCHCertificate";
                existingTruck.DCHCertificate = await _fileStorageService.SaveFileAsync(detailDto.DCHCertificate!, subDirectory, "DCHCertificate");
            }

            if (detailDto.ServeSafeCertificate is not null)
            {
                if (!string.IsNullOrEmpty(existingTruck.ServeSafeCertificate))
                {
                    _fileStorageService.DeleteFile(existingTruck.ServeSafeCertificate);
                }
                string subDirectory = $"documents/ServeSafeCertificate";
                existingTruck.ServeSafeCertificate = await _fileStorageService.SaveFileAsync(detailDto.ServeSafeCertificate!, subDirectory, "ServeSafeCertificate");
            }

            existingTruck.BusinessDescription = detailDto.BusinessDescription;
            existingTruck.BussinessStartYear = detailDto.BussinessStartYear;
            existingTruck.EIN = detailDto.EIN;
            existingTruck.IsBreakfast = detailDto.IsBreakfast;
            existingTruck.IsLunch = detailDto.IsLunch;
            existingTruck.IsDinner = detailDto.IsDinner;
            existingTruck.MinimumGuaranteeAmount = detailDto.MinimumGuaranteeAmount;
            existingTruck.OnboardingStatus = OnboardingStatus.Step4;
            existingTruck.UpdatedAt = DateTime.UtcNow;
            
            _repositoryManager.TruckDetail.Update(existingTruck);
            await _repositoryManager.SaveAsync();
            return new StepResponseDto
            {
                OnboardingStatus = OnboardingStatus.Step4,
                Message = "Step 3 details updated successfully."
            };
        }
        else
        {
            return new StepResponseDto
            {
                Result = false,
                OnboardingStatus = OnboardingStatus.Step3,
                Message = "Step 3 details could not be updated."
            };
        }
    }



    public async Task<CuisineType?> UpdateCuisineTypeAsync(Guid id, UpdateCuisineTypeDto updateCuisineTypeDto)
    {
        CuisineType? existingType = await _repositoryManager.CuisineType.GetCuisineTypeByIdAsync(id);
        if(existingType == null)
        {
            return null;
        }

        CuisineType cuisineType = new()
        {
            Id = id,
            Name = updateCuisineTypeDto.Name,
            Description = updateCuisineTypeDto.Description,
            Status = updateCuisineTypeDto.Status,
        };
        _repositoryManager.CuisineType.Update(cuisineType);
        await _repositoryManager.SaveAsync();
        return cuisineType;
    }

   

    public async Task<bool> DeleteCuisineTypeAsync(Guid id)
    {
        CuisineType? type = await _repositoryManager.CuisineType.GetCuisineTypeByIdAsync(id);
        if (type == null)
            return false;
        return await _repositoryManager.CuisineType.DeleteCuisineTypeAsync(id);
    }
   


    public async Task<IEnumerable<CuisineType>> GetActiveCuisineTypesAsync()
    {
        return await _repositoryManager.CuisineType.GetActiveCuisineTypesAsync();
    }

    public async Task<bool> UpdateCuisineTypeStatusAsync(Guid id, bool status)
    {
        CuisineType? type = await _repositoryManager.CuisineType.GetCuisineTypeByIdAsync(id);
        if (type == null)
        {
            return false;
        }
        type.Status = status;
        _repositoryManager.CuisineType.UpdateCuisineTypeAsync(type);
        await _repositoryManager.SaveAsync();
        return true;
    }


    

    public Task<TruckDetail?> GetTruckDetailByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<TruckDetail> CreateTruckDetailAsync(CreateTruckDetailDto createPackTypeDto)
    {
        throw new NotImplementedException();
    }

    public Task<TruckDetail?> UpdateTruckDetailAsync(Guid id, UpdateTruckDetailDto updatePackTypeDto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteTruckDetailAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateOnboardingStatusAsync(Guid id, OnboardingStatus onboardingStatus)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateTruckDetailStatusAsync(Guid id, bool status)
    {
        throw new NotImplementedException();
    }
};
