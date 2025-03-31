using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using EZFood.Application.Interfaces;
using EZFood.Domain.Entities.Models;
using EZFood.Infrastructure.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EZFood.Application.Services;
public class TokenService(
    UserManager<ApplicationUser> userManager,
    IConfiguration configuration) : ITokenService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IConfiguration _configuration = configuration;

    public async Task<string> GenerateJwtTokenAsync(ApplicationUser appUser, User userProfile)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, appUser.UserName!),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.NameIdentifier, appUser.Id.ToString()),
            new("email", userProfile.Email),
            new("userId", userProfile.Id.ToString()),
            new("name", userProfile.Name)
        };
        var roles = await _userManager.GetRolesAsync(appUser);

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        claims.Add(new Claim("roles", string.Join(",", roles)));        

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecurityKey"]!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expires = DateTime.UtcNow.AddDays(
            Convert.ToDouble(_configuration["JwtSettings:ExpirationTime"]));
        var validAudiences = _configuration.GetSection("JwtSettings:ValidAudiences")
                                   .GetChildren()
                                   .Select(a => a.Value)
                                   .Where(a => !string.IsNullOrEmpty(a))
                                   .ToArray();

        var token = new JwtSecurityToken(
            _configuration["JwtSettings:ValidIssuer"],
            null,
            claims,
            expires: expires,
            signingCredentials: creds
        );
        token.Payload["aud"] = validAudiences;
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}