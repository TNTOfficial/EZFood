using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using EZFood.Domain.Constants;
using EZFood.Domain.Entities.Models;
using EZFood.Infrastructure.Identity;
using EZFood.Infrastructure.Persistence.DbContext;

namespace EZFood.Infrastructure.Persistence.Seed;

public static class DbInitializer
{
    public static async Task SeedDatabase(IServiceProvider serviceProvider)
    {
        using IServiceScope scope = serviceProvider.CreateScope();
        EZFoodContext context = scope.ServiceProvider.GetRequiredService<EZFoodContext>();
        UserManager<ApplicationUser> userManager = scope.ServiceProvider
            .GetRequiredService<UserManager<ApplicationUser>>();
        RoleManager<IdentityRole<Guid>> roleManager = scope.ServiceProvider
                        .GetRequiredService<RoleManager<IdentityRole<Guid>>>();
        await context.Database.MigrateAsync();
        await SeedRoles(roleManager);
        await SeedAdminUser(context, userManager);
    }

    private static async Task SeedRoles(RoleManager<IdentityRole<Guid>> roleManager)
    {
        if (!await roleManager.RoleExistsAsync(Roles.Admin))
        {
            await roleManager.CreateAsync(new IdentityRole<Guid>(Roles.Admin));
        }
        if (!await roleManager.RoleExistsAsync(Roles.Seller))
        {
            await roleManager.CreateAsync(new IdentityRole<Guid>(Roles.Seller));
        }
        if (!await roleManager.RoleExistsAsync(Roles.User))
        {
            await roleManager.CreateAsync(new IdentityRole<Guid>(Roles.User));
        }
    }

    public static async Task SeedAdminUser(EZFoodContext context, UserManager<ApplicationUser> userManager)
    {
        // Check if admin already exists
        if (await userManager.FindByEmailAsync("admin.vedaant@gmail.com") != null)
            return;


        //creating Idenity User

        ApplicationUser adminIdenityUser = new ApplicationUser()
        {
            Id = Guid.NewGuid(),
            UserName = "Admin",
            Email = "admin.vedaant@gmail.com",
            PhoneNumber = "0987654321",
            PhoneNumberConfirmed = true,
            EmailConfirmed = true
        };
        IdentityResult? result = await userManager.CreateAsync(adminIdenityUser, "Root1234@");
        if (!result.Succeeded)
        {
            throw new Exception($"Failed to create admin user: {string.Join(", ", result.Errors)}");
        }
        await userManager.AddToRoleAsync(adminIdenityUser, Roles.Admin);

        // create admin profile 
        User adminUser = new User
        {
            Id = Guid.NewGuid(),
            IdentityUserId = adminIdenityUser.Id,
            Name ="Admin",
            Email = "admin.vedaant@gmail.com",
            PhoneNumber = "0987654321",
            Status = true,
            CreatedAt = DateTime.UtcNow,            
        };

        context.UserProfile.Add(adminUser);
        await context.SaveChangesAsync();

        await context.SaveChangesAsync();
    }
}
