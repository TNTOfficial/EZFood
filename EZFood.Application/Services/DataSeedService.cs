using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using EZFood.Shared.Exceptions;
using EZFood.Application.Interfaces;
using EZFood.Domain.Constants;
using EZFood.Domain.Entities.Models;
using EZFood.Infrastructure.Identity;
using EZFood.Infrastructure.Persistence.DbContext;
using EZFood.Infrastructure.Persistence.Interfaces;

namespace EZFood.Application.Services;

public class DataSeedService(
    EZFoodContext context,
    UserManager<ApplicationUser>userManager,
    RoleManager<IdentityRole<Guid>> roleManager,
    IRepositoryManager repositoryManager
   ) : IDataSeedService
{
    private readonly IRepositoryManager _repository = repositoryManager;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager = roleManager;
    private readonly EZFoodContext _context = context;

    public async Task<User?> SeedAdminUserAsync()
    {
        await SeedRolesAsync(_roleManager);
        var existingAdminUser = await _userManager.FindByEmailAsync("itsupport@ezfoodtrucks.com");
        // Check if admin already exists
        if (existingAdminUser != null)
        {
            await _userManager.DeleteAsync(existingAdminUser);
            await _context.SaveChangesAsync();
        }


            // Create Identity User
            ApplicationUser adminIdentityUser = new ApplicationUser()
            {
                Id = Guid.NewGuid(),
                UserName = "Admin",
                Email = "itsupport@ezfoodtrucks.com",
                PhoneNumber = "9876543210",
                PhoneNumberConfirmed = true,
                EmailConfirmed = true
            };

        IdentityResult? result = await _userManager.CreateAsync(adminIdentityUser, "Root1234@");
        if (!result.Succeeded)
        {
            throw new EZFoodException($"Failed to create admin user: {string.Join(", ", result.Errors)}");
        }
      
        await _userManager.AddToRoleAsync(adminIdentityUser, Roles.Admin);

        // create admin profile 
        User adminUser = new()
        {
            Id = Guid.NewGuid(),
            IdentityUserId = adminIdentityUser.Id,
            Name = "Admin",
            Email = "itsupport@ezfoodtrucks.com",
            PhoneNumber = "9876543210",
            Status = true,
            CreatedAt = DateTime.UtcNow
        };        

        _context.UserProfile.Add(adminUser);
        await _context.SaveChangesAsync();
        return adminUser;
    }
   
    private async Task SeedRolesAsync(RoleManager<IdentityRole<Guid>> roleManager)
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

}
