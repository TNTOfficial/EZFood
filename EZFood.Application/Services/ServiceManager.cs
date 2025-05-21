using AutoMapper;
using EZFood.Application.Interfaces;
using EZFood.Application.Services;
using EZFood.Infrastructure.Identity;
using EZFood.Infrastructure.Persistence.DbContext;
using EZFood.Infrastructure.Persistence.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MLM.Application.Services;

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
    private readonly Lazy<IFileStorageService> _fileStorageService;
    private readonly Lazy<IOnboardingActionService> _onboardingActionService;
    private readonly Lazy<IUserEventService> _userEventService;

    public ServiceManager(EZFoodContext context,IRepositoryManager repositoryManager, IHttpContextAccessor httpContextAccessor,
        UserManager<ApplicationUser> userManager,RoleManager<IdentityRole<Guid>> roleManager, IConfiguration configuration, HttpClient httpClient, IWebHostEnvironment environment)
    {
        _userService = new Lazy<IUserService>(() => new UserService(repositoryManager, userManager));
        _emailService = new Lazy<IEmailService>(() => new EmailService(configuration));
        _tokenService = new Lazy<ITokenService>(() => new TokenService(userManager, configuration));
        _authService = new Lazy<IAuthService>(() => new AuthService(repositoryManager, userManager, _tokenService.Value, _emailService.Value)); 
        _dataSeedService = new Lazy<IDataSeedService>(() => new DataSeedService(context, userManager, roleManager,
            repositoryManager));
        _cuisineTypeService = new Lazy<ICuisineTypeService>(() => new CuisineTypeService(repositoryManager));
        _truckDetailService = new Lazy<ITruckDetailService>(() => new TruckDetailService(repositoryManager, httpContextAccessor, _fileStorageService.Value)); 
        _fileStorageService = new Lazy<IFileStorageService>(() => new FileStorageService(environment, configuration));
        _onboardingActionService = new Lazy<IOnboardingActionService>(() => new OnboardingActionService(repositoryManager));
        _userEventService = new Lazy<IUserEventService>(() => new UserEventService(repositoryManager));
    }
    public IUserService UserService => _userService.Value;
    public IEmailService EmailService => _emailService.Value;
    public ITokenService TokenService => _tokenService.Value;
    public IAuthService AuthService => _authService.Value;
    public IDataSeedService DataSeedService => _dataSeedService.Value;
    public ICuisineTypeService CuisineTypeService => _cuisineTypeService.Value;
    public ITruckDetailService TruckDetailService => _truckDetailService.Value;
    public IFileStorageService FileStorageService => _fileStorageService.Value;
    public IOnboardingActionService OnboardingActionService => _onboardingActionService.Value;
    public IUserEventService UserEventService => _userEventService.Value;


}
