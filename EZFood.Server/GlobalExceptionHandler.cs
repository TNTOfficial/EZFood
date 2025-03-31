using Microsoft.AspNetCore.Diagnostics;
using EZFood.Shared.Exceptions;
using EZFood.Shared.Dtos.Common;
using System.Text.Json;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger = logger;

    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "An error occurred");

        context.Response.ContentType = "application/json";

        var response = exception switch
        {
            EZFoodException mlmEx => new ApiResponse<object>
            {
                Success = false,
                Message = mlmEx.Message,
                Errors = mlmEx.Details
            },
            _ => new ApiResponse<object>
            {
                Success = false,
                Message = "An unexpected error occurred",
                Errors = new[] { "Please try again later" }
            }
        };

        context.Response.StatusCode = exception switch
        {
            EZFoodException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };

        var json = JsonSerializer.Serialize(response);
        await context.Response.WriteAsync(json, cancellationToken);

        return true; // Indicates that the exception was handled
    }
}
