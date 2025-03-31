using EZFood.Infrastructure.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EZFood.Server.ContextFactory;

public class EZFoodContextFactory:IDesignTimeDbContextFactory<EZFoodContext>
{

    public  EZFoodContext CreateDbContext(string[] args)
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        var builder = new DbContextOptionsBuilder<EZFoodContext>()
           .UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
           b => b.MigrationsAssembly("EZFood.Server"));

        return new EZFoodContext(builder.Options);
    }
}
