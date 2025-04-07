namespace EZFood.Application.Interfaces;
public interface IServiceManager
{    
    IUserService UserService { get; }
    ITokenService TokenService { get; }
    IAuthService AuthService { get; }
    IEmailService EmailService { get; }
    IDataSeedService DataSeedService { get; }
    ICuisineTypeService CuisineTypeService { get; }
}