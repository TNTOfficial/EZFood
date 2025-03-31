using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using EZFood.Application.Interfaces;
using System.Web;

namespace EZFood.Application.Services;
public class EmailService(IConfiguration configuration) : IEmailService
{
    private readonly IConfiguration _configuration = configuration;
    public async Task SendPasswordResetEmailAsync(string email, string token, string frontendUrl)
    {
        MimeMessage message = new();

        // Getting email settings from the configuration
        string? fromEmail = _configuration["EmailSettings:FromEmail"];
        string? fromName = _configuration["EmailSettings:FromName"];
        string? smtpServer = _configuration["EmailSettings:SmtpServer"];
        int smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"]!);
        string? smtpUsername = _configuration["EmailSettings:SmtpUsername"];
        string? smtpPassword = _configuration["EmailSettings:SmtpPassword"];

        message.From.Add(new MailboxAddress(fromName, fromEmail));
        message.To.Add(new MailboxAddress("", email));
        message.Subject = "Reset your password";

        // Encoding token for url safety

        string encodedToken = HttpUtility.UrlEncode(token);

        // Consturct the reset URL with query params
        string resetUrl = $"{frontendUrl}?token={encodedToken}";

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = $@"
                     <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto;'>
                        <h2>Password Reset Request</h2>
                        <p>You recently requested to reset your password. Click the button below to reset it:</p>
                        <div style='margin: 30px 0;'>
                            <a href='{resetUrl}' style='background-color: #4CAF50; color: white; padding: 12px 24px; text-decoration: none; border-radius: 4px; display: inline-block;'>Reset Password</a>
                        </div>
                        <p>If you did not request a password reset, you can ignore this email.</p>
                        <p>This link will expire in 24 hours.</p>
                     </div>",
            TextBody = $"Reset your password by visiting: {resetUrl}"
        };
        message.Body = bodyBuilder.ToMessageBody();

        using var client = new SmtpClient();
        await client.ConnectAsync(smtpServer, smtpPort, SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(smtpUsername, smtpPassword);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }

}

