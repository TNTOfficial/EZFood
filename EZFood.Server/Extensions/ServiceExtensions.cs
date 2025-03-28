using EZFood.Infrastructure.Identity;
using EZFood.Infrastructure.Persistance.DbContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace EZFood.Server.Extensions;

public static class ServiceExtensions
{


    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
            {
                builder.WithOrigins(
                    "http://localhost:4200",
                    "http://localhost:4201"
                   )
                .AllowAnyMethod()
                .AllowAnyHeader();
            });
        });
    }

    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EZFoodContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConntection"));
        });
    }


    public static void ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
        {
            options.Password.RequiredLength = 5;
            options.Password.RequireDigit = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            options.Password.RequiredUniqueChars = 0;
        }).AddEntityFrameworkStores<EZFoodContext>()
        .AddDefaultTokenProviders(); ;
    }


    public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
    {
        IConfiguration jwtSettings = configuration.GetSection("JwtSettings");
        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                RequireExpirationTime = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["ValidIssuer"],
                ValidAudiences = configuration.GetSection("JwtSettings:ValidAudiences")
                                      .GetChildren()
                                      .Select(a => a.Value)
                                      .ToArray(),
                IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtSettings["SecurityKey"]!))
            };
        });
    }
}
