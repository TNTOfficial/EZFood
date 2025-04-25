

using EZFood.Domain.Entities.Enums;
using EZFood.Shared.Dtos.TruckDetail.Steps;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EZFood.Shared.Dtos.CuisineType;

public class TruckDetailStepsDto
{
    public OnboardingStatus Step { get; set; } = OnboardingStatus.Step1;
    public StepOne? StepOne { get; set; } = new();
    public StepTwo? StepTwo { get; set; } = new();
    public StepThree? StepThree { get; set; } = new();
    public StepFour? StepFour { get; set; } = new();
    public StepFive? StepFive { get; set; } = new();

}
