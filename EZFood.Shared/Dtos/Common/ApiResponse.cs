namespace EZFood.Shared.Dtos.Common;

public class ApiResponse<T>
{
    public bool Success {  get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public IEnumerable<string>? Errors { get; set; }

    public static ApiResponse<T> SuccessResult(T data, string message= "Operation Successful")=>
        new() { Success = true, Data=data,Message = message };
    public static ApiResponse<T> ErrorResult(string message, IEnumerable<string>? errors = null) =>
        new() { Success = false, Message = message, Errors = errors };
}
