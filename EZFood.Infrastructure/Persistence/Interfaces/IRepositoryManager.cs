namespace EZFood.Infrastructure.Persistence.Interfaces;
public interface IRepositoryManager
{
    Task<T> ExecuteScalarAsync<T>(string sql);
    IUserRepository User { get; }
    ICuisineTypeRepository CuisineType { get; }
    ITruckDetailRepository TruckDetail { get; }
    ICuisineTypeTruckDetailRepository CuisineTypeTruckDetail { get; }
    IOnboardingActionRepository OnboardingAction { get; }
    Task SaveAsync();
}

