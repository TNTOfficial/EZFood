namespace EZFood.Application.Interfaces;

public interface IEmailService
{
    Task SendPasswordResetEmailAsync(string email,string token, string frontendUrl);
}
