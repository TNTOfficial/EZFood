﻿
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
using EZFood.Shared.Dtos.TruckDetail.Steps;
using static System.Net.Mime.MediaTypeNames;
using EZFood.Shared.Dtos.Response;

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

    public async Task<IEnumerable<TruckDetail>> GetTruckDetailsForStatusAsync(OnboardingStatus status)
    {
        return await _repositoryManager.TruckDetail.GetTruckDetailsForStatusAsync(status);
    }

    public async Task<IEnumerable<TruckDetail>> GetActiveFoodTrucksAsync()
    {
        return await _repositoryManager.TruckDetail.GetActiveTruckDetailsAsync();
    }

    public async Task<IEnumerable<TruckDetail>> GetTruckDetailsForIncompleteStatusAsync()
    {
        return await _repositoryManager.TruckDetail.GetTruckDetailsForIncompleteStatusAsync();
    }

    public async Task<List<int>> GetOnboardingStatsAsync()
    {
        List<int> counts = new();
        counts.Add(await _repositoryManager.TruckDetail.GetTruckDetailsForStatusCountAsync(OnboardingStatus.Submitted));
        counts.Add(await _repositoryManager.TruckDetail.GetTruckDetailsForStatusCountAsync(OnboardingStatus.ReferBack));
        counts.Add(await _repositoryManager.TruckDetail.GetTruckDetailsForStatusIncompleteCountAsync());
        counts.Add(await _repositoryManager.TruckDetail.GetTruckDetailsForStatusCountAsync(OnboardingStatus.Rejected));
        return counts;
    }


    public async Task<ResponseDto> SubmitForReviewAsync()
    {
        TruckDetail? existingTruck = await _repositoryManager.TruckDetail.getTruckDetailByUserAsync(_userId);
        if (existingTruck != null)
        {            
            existingTruck.OnboardingStatus = OnboardingStatus.Submitted;
            existingTruck.UpdatedAt = DateTime.UtcNow;
            _repositoryManager.TruckDetail.Update(existingTruck);
            await _repositoryManager.SaveAsync();
            return new ResponseDto
            {
                Result = true,
                Message = "Application has been submitted for for review. You will be notified through mail for further action."
            };
        }
        return new ResponseDto
        {
            Result = false,
            Message = "Application could not be submitted for review."

        };
    }
    public async Task<StepsResponseDto<TruckDetailStepsDto>> GetTruckDetailStepsAsync()
    {
        try
        {
            TruckDetailStepsDto stepDetails = new TruckDetailStepsDto();
            TruckDetail? truckDetail = await _repositoryManager.TruckDetail.getTruckDetailByUserAsync(_userId);
            if (truckDetail == null)
            {
                return new StepsResponseDto<TruckDetailStepsDto>
                {
                    Result = false,
                    Data = null
                };
            }
            if(truckDetail.IsActive)
            {
                return new StepsResponseDto<TruckDetailStepsDto>
                {
                    Result = true,
                    IsActive = truckDetail.IsActive,
                    Data = null
                };
            }
            else
            {

                stepDetails.Step = truckDetail.OnboardingStatus;
                stepDetails.StepOne = new StepOne
                {
                    TruckName = truckDetail.TruckName,
                    TruckOwnerName = truckDetail.TruckOwnerName,
                    BusinessEmail = truckDetail.BusinessEmail,
                    PhoneNumber = truckDetail.PhoneNumber,
                    Address = truckDetail.Address
                };
                stepDetails.StepTwo = new StepTwo
                {
                    IsOtherCuisine = truckDetail.IsOtherCuisine,
                    CuisineNote = truckDetail.CuisineNote,
                    Cuisines = truckDetail.CuisineTypes.Select(x => x.Id).ToList()
                };
                stepDetails.StepThree = new StepThree
                {
                    BusinessDescription = truckDetail.BusinessDescription,
                    BussinessStartYear = truckDetail.BussinessStartYear,
                    EIN = truckDetail.EIN,
                    IsBreakfast = truckDetail.IsBreakfast,
                    IsLunch = truckDetail.IsLunch,
                    IsDinner = truckDetail.IsDinner,
                    MinimumGuaranteeAmount = truckDetail.MinimumGuaranteeAmount,
                    COI = truckDetail.COI,
                    W9 = truckDetail.W9,
                    DCHCertificate = truckDetail.DCHCertificate,
                    ServeSafeCertificate = truckDetail.ServeSafeCertificate,
                };
                stepDetails.StepFour = new StepFour
                {
                    BannerUrl = truckDetail.BannerUrl,
                    Files = truckDetail.ImageList
                };
                stepDetails.StepFive = new StepFive
                {
                    Files = truckDetail.MenuList
                };
                return new StepsResponseDto<TruckDetailStepsDto>
                {
                    Result = true,
                    Data = stepDetails
                };
            }

        } catch(Exception ex)
        {
            return new StepsResponseDto<TruckDetailStepsDto>
            {
                Result = true,
                Message = ex.Message
            };
        }
        
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

    public async Task<StepResponse<StepOne>> CreateStepOneAsync(CreateStepOneDto detailDto)
    {
        StepOne stepOne = new()
        {
            TruckName = detailDto.TruckName,
            TruckOwnerName = detailDto.TruckOwnerName,
            PhoneNumber = detailDto.PhoneNumber,
            Address = detailDto.Address,
            BusinessEmail = detailDto.BusinessEmail,
        };
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
            }
        return StepResponse<StepOne>.SuccessResult(OnboardingStatus.Step2, stepOne, "Step 1 details updated successfully.");
    }

    public async Task<StepResponse<StepTwo>> CreateStepTwoAsync(CreateStepTwoDto detailDto)
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
            StepTwo stepTwo = new()
            {
                IsOtherCuisine = existingTruck.IsOtherCuisine,
                CuisineNote = existingTruck.CuisineNote,
                Cuisines = existingTruck.CuisineTypes.Select(x => x.Id).ToList()
            };
            _repositoryManager.TruckDetail.Update(existingTruck);
            await _repositoryManager.SaveAsync();
            return StepResponse<StepTwo>.SuccessResult(OnboardingStatus.Step3, stepTwo, "Step 2 details updated successfully.");
            
        }
        else
        {
            return StepResponse<StepTwo>.ErrorResult(OnboardingStatus.Step2, "Step 2 details could not be updated.");            
        }

    }

    public async Task<StepResponse<StepThree>> CreateStepThreeAsync(CreateStepThreeDto detailDto)
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

            StepThree stepThree = new()
            {
                BusinessDescription = existingTruck.BusinessDescription,
                BussinessStartYear = existingTruck.BussinessStartYear,
                EIN = existingTruck.EIN,
                IsBreakfast = existingTruck.IsBreakfast,
                IsLunch = existingTruck.IsLunch,
                IsDinner = existingTruck.IsDinner,
                MinimumGuaranteeAmount = detailDto.MinimumGuaranteeAmount,
                COI = existingTruck.COI,
                W9 = existingTruck.W9,
                DCHCertificate = existingTruck.DCHCertificate,
                ServeSafeCertificate = existingTruck.ServeSafeCertificate
            };
            return StepResponse<StepThree>.SuccessResult(OnboardingStatus.Step4, stepThree, "Step 3 details updated successfully.");            
        }
        else
        {
            return StepResponse<StepThree>.ErrorResult(OnboardingStatus.Step3, "Step 3 details could not be updated.");           
        }
    }









    public async Task<StepResponse<string>> UpdateStepThreeFileAsync(CreateStepThreeFileDto detailDto)
    {
        TruckDetail? existingTruck = await _repositoryManager.TruckDetail.getTruckDetailByUserAsync(_userId);
        if (existingTruck != null)
        {
            string updatedFile = string.Empty;
            if (detailDto.File is not null)
            {
                if (detailDto.Document == "COI")
                {
                    if (!string.IsNullOrEmpty(existingTruck.COI))
                    {
                        _fileStorageService.DeleteFile(existingTruck.COI);
                    }
                    string subDirectory = $"documents/COI";
                    existingTruck.COI = await _fileStorageService.SaveFileAsync(detailDto.File, subDirectory, "coi");
                    updatedFile = existingTruck.COI;
                }

                if (detailDto.Document == "W9")
                {
                    if (!string.IsNullOrEmpty(existingTruck.W9))
                    {
                        _fileStorageService.DeleteFile(existingTruck.W9);
                    }
                    string subDirectory = $"documents/W9";
                    existingTruck.W9 = await _fileStorageService.SaveFileAsync(detailDto.File, subDirectory, "W9");
                    updatedFile = existingTruck.W9;
                }

                if (detailDto.Document == "DCHCertificate")
                {
                    if (!string.IsNullOrEmpty(existingTruck.DCHCertificate))
                    {
                        _fileStorageService.DeleteFile(existingTruck.DCHCertificate);
                    }
                    string subDirectory = $"documents/DCHCertificate";
                    existingTruck.DCHCertificate = await _fileStorageService.SaveFileAsync(detailDto.File, subDirectory, "DCHCertificate");
                    updatedFile = existingTruck.DCHCertificate;
                }

                if (detailDto.Document == "ServeSafeCertificate")
                {
                    if (!string.IsNullOrEmpty(existingTruck.ServeSafeCertificate))
                    {
                        _fileStorageService.DeleteFile(existingTruck.ServeSafeCertificate);
                    }
                    string subDirectory = $"documents/ServeSafeCertificate";
                    existingTruck.ServeSafeCertificate = await _fileStorageService.SaveFileAsync(detailDto.File, subDirectory, "ServeSafeCertificate");
                    updatedFile = existingTruck.ServeSafeCertificate;
                }
            }

            existingTruck.UpdatedAt = DateTime.UtcNow;

            _repositoryManager.TruckDetail.Update(existingTruck);
            await _repositoryManager.SaveAsync();

            return StepResponse<string>.SuccessResult(OnboardingStatus.Step3, updatedFile, "Step 3 file updated successfully.");
        }
        else
        {
            return StepResponse<string>.ErrorResult(OnboardingStatus.Step3, "Step 3 file could not be updated.");
        }
    }
    public async Task<StepResponse<StepFour>> CreateStepFourAsync(CreateStepFourDto detailDto)
    {
        TruckDetail? existingTruck = await _repositoryManager.TruckDetail.getTruckDetailByUserAsync(_userId);
        if (existingTruck != null)
        {
            if (detailDto.Images is not null)
            {
                List<string> images = new();
                string subDirectory = $"images";

                for (int i = 0; i < detailDto.Images.Count; i++)
                {
                    string img = await _fileStorageService.SaveFileAsync(detailDto.Images[i], subDirectory, "truck-gallaery");
                    images.Add(img);
                }
                List<string> existingList = existingTruck.ImageList != null ? existingTruck.ImageList : new();
                existingList.AddRange(images);
                existingTruck.ImageList = existingList;
            }
            StepFour stepFour = new()
            {
                Files = existingTruck.ImageList
            };
            
            existingTruck.OnboardingStatus = OnboardingStatus.Step5;
            existingTruck.UpdatedAt = DateTime.UtcNow;

            _repositoryManager.TruckDetail.Update(existingTruck);
            await _repositoryManager.SaveAsync();
            return StepResponse<StepFour>.SuccessResult(OnboardingStatus.Step5, stepFour, "Step 4 details updated successfully.");
            
        }
        else
        {
            return StepResponse<StepFour>.ErrorResult(OnboardingStatus.Step4, "Step 4 details could not be updated.");
            
        }
    }


    public async Task<StepResponse<StepFour>> DeleteStepFourImage(int id)
    {
        TruckDetail? existingTruck = await _repositoryManager.TruckDetail.getTruckDetailByUserAsync(_userId);
        if (existingTruck != null)
        {
            existingTruck.ImageList = existingTruck.ImageList?.Where(x => x != existingTruck.ImageList[id]).ToList();
            StepFour stepFour = new()
            {
                Files = existingTruck.ImageList
            };

            existingTruck.UpdatedAt = DateTime.UtcNow;

            _repositoryManager.TruckDetail.Update(existingTruck);
            await _repositoryManager.SaveAsync();
            return StepResponse<StepFour>.SuccessResult(OnboardingStatus.Step4, stepFour, "Step 4 details updated successfully.");

        }
        else
        {
            return StepResponse<StepFour>.ErrorResult(OnboardingStatus.Step4, "Step 4 details could not be updated.");

        }
    }




    public async Task<StepResponse<StepFive>> CreateStepFiveAsync(CreateStepFourDto detailDto)
    {
        TruckDetail? existingTruck = await _repositoryManager.TruckDetail.getTruckDetailByUserAsync(_userId);
        if (existingTruck != null)
        {
            if (detailDto.Images is not null)
            {
                List<string> images = new();
                string subDirectory = $"menu";

                for (int i = 0; i < detailDto.Images.Count; i++)
                {
                    string img = await _fileStorageService.SaveFileAsync(detailDto.Images[i], subDirectory, "truck-menu");
                    images.Add(img);
                }
                List<string> existingList = existingTruck.MenuList != null ? existingTruck.MenuList : new();
                existingList.AddRange(images);
                existingTruck.MenuList = existingList;
            }
            StepFive stepFive = new()
            {
                Files = existingTruck.MenuList
            };

            existingTruck.OnboardingStatus = OnboardingStatus.Pending;
            existingTruck.UpdatedAt = DateTime.UtcNow;

            _repositoryManager.TruckDetail.Update(existingTruck);
            await _repositoryManager.SaveAsync();
            return StepResponse<StepFive>.SuccessResult(OnboardingStatus.Pending, stepFive, "Step 5 details updated successfully.");

        }
        else
        {
            return StepResponse<StepFive>.ErrorResult(OnboardingStatus.Step5, "Step 4 details could not be updated.");

        }
    }


    public async Task<StepResponse<StepFive>> DeleteStepFiveFile(int id)
    {
        TruckDetail? existingTruck = await _repositoryManager.TruckDetail.getTruckDetailByUserAsync(_userId);
        if (existingTruck != null)
        {
            existingTruck.MenuList = existingTruck.MenuList?.Where(x => x != existingTruck.MenuList[id]).ToList();
            StepFive stepFive = new()
            {
                Files = existingTruck.MenuList
            };

            existingTruck.UpdatedAt = DateTime.UtcNow;

            _repositoryManager.TruckDetail.Update(existingTruck);
            await _repositoryManager.SaveAsync();
            return StepResponse<StepFive>.SuccessResult(OnboardingStatus.Step5, stepFive, "Step 5 details updated successfully.");

        }
        else
        {
            return StepResponse<StepFive>.ErrorResult(OnboardingStatus.Step5, "Step 5 details could not be updated.");

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


    

    public async Task<TruckDetail?> GetTruckDetailByIdAsync(Guid id)
    {
        TruckDetail? truckDetail = await _repositoryManager.TruckDetail.GetTruckDetailByIdAsync(id);
        if (truckDetail == null)
        {
            return null;
        }
        return truckDetail;
    }

    public async Task<TruckDetail?> GetTruckDetailByUserAsync(Guid id)
    {
        TruckDetail? truckDetail = await _repositoryManager.TruckDetail.getTruckDetailByUserAsync(id);     
        return truckDetail;
    }

    public async Task<StepsResponseDto<TruckDetailDto>> GetTruckDetailStepsByIdAsync(Guid id)
    {
        try
        {
            TruckDetailDto stepDetails = new TruckDetailDto();
            TruckDetail? truckDetail = await _repositoryManager.TruckDetail.GetTruckDetailByIdAsync(id);
            if (truckDetail == null)
            {
                return new StepsResponseDto<TruckDetailDto>
                {
                    Result = false,
                    Data = null
                };
            }

            stepDetails.Step = truckDetail.OnboardingStatus;
            stepDetails.StepOne = new StepOne
            {
                TruckName = truckDetail.TruckName,
                TruckOwnerName = truckDetail.TruckOwnerName,
                BusinessEmail = truckDetail.BusinessEmail,
                PhoneNumber = truckDetail.PhoneNumber,
                Address = truckDetail.Address
            };
            stepDetails.StepTwo = new StepTwoDetail
            {
                IsOtherCuisine = truckDetail.IsOtherCuisine,
                CuisineNote = truckDetail.CuisineNote,
                Cuisines = truckDetail.CuisineTypes.Select(x => x.Name).ToList()
            };
            stepDetails.StepThree = new StepThree
            {
                BusinessDescription = truckDetail.BusinessDescription,
                BussinessStartYear = truckDetail.BussinessStartYear,
                EIN = truckDetail.EIN,
                IsBreakfast = truckDetail.IsBreakfast,
                IsLunch = truckDetail.IsLunch,
                IsDinner = truckDetail.IsDinner,
                MinimumGuaranteeAmount = truckDetail.MinimumGuaranteeAmount,
                COI = truckDetail.COI,
                W9 = truckDetail.W9,
                DCHCertificate = truckDetail.DCHCertificate,
                ServeSafeCertificate = truckDetail.ServeSafeCertificate,
            };
            stepDetails.StepFour = new StepFour
            {
                BannerUrl = truckDetail.BannerUrl,
                Files = truckDetail.ImageList
            };
            stepDetails.StepFive = new StepFive
            {
                Files = truckDetail.MenuList
            };
            return new StepsResponseDto<TruckDetailDto>
            {
                Result = true,
                Data = stepDetails
            };

        }
        catch (Exception ex)
        {
            return new StepsResponseDto<TruckDetailDto>
            {
                Result = true,
                Message = ex.Message
            };
        }

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
