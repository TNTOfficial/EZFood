using EZFood.Domain.Entities.Enums;

namespace EZFood.Shared.Dtos.Response;

public class StepResponse<T>
{
    public bool Result {  get; set; }
    public OnboardingStatus OnboardingStatus { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }

    public static StepResponse<T> SuccessResult(OnboardingStatus onboardingStatus,T data, string message)=>
        new() { Result = true, Data=data,Message = message, OnboardingStatus = onboardingStatus };
    public static StepResponse<T> ErrorResult(OnboardingStatus onboardingStatus, string message) =>
        new() { Result = false, Message = message, OnboardingStatus = onboardingStatus };
}
