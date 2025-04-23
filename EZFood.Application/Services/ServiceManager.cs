using AutoMapper;
using EZFood.Application.Interfaces;
using EZFood.Application.Services;
using EZFood.Infrastructure.Identity;
using EZFood.Infrastructure.Persistence.DbContext;
using EZFood.Infrastructure.Persistence.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace EZFood.Application.Services;

public sealed class ServiceManager : IServiceManager
{
    private readonly Lazy<IUserService> _userService;
    private readonly Lazy<ITokenService> _tokenService;
    private readonly Lazy<IEmailService> _emailService;
    private readonly Lazy<IAuthService> _authService;
    private readonly Lazy<IDataSeedService> _dataSeedService;
    private readonly Lazy<ICuisineTypeService> _cuisineTypeService;
    private readonly Lazy<ITruckDetailService> _truckDetailService;

    public ServiceManager(EZFoodContext context,IRepositoryManager repositoryManager, IHttpContextAccessor httpContextAccessor,
        UserManager<ApplicationUser> userManager,RoleManager<IdentityRole<Guid>> roleManager, IConfiguration configuration, HttpClient httpClient)
    {
        _userService = new Lazy<IUserService>(() => new UserService(repositoryManager, userManager));
        _emailService = new Lazy<IEmailService>(() => new EmailService(configuration));
        _tokenService = new Lazy<ITokenService>(() => new TokenService(userManager, configuration));
        _authService = new Lazy<IAuthService>(() => new AuthService(repositoryManager, userManager, _tokenService.Value, _emailService.Value)); 
        _dataSeedService = new Lazy<IDataSeedService>(() => new DataSeedService(context, userManager, roleManager,
            repositoryManager));
        _cuisineTypeService = new Lazy<ICuisineTypeService>(() => new CuisineTypeService(repositoryManager));
        _truckDetailService = new Lazy<ITruckDetailService>(() => new TruckDetailService(repositoryManager, httpContextAccessor));
    }
    public IUserService UserService => _userService.Value;
    public IEmailService EmailService => _emailService.Value;
    public ITokenService TokenService => _tokenService.Value;
    public IAuthService AuthService => _authService.Value;
    public IDataSeedService DataSeedService => _dataSeedService.Value;
    public ICuisineTypeService CuisineTypeService => _cuisineTypeService.Value;
    public ITruckDetailService TruckDetailService => _truckDetailService.Value;


}
