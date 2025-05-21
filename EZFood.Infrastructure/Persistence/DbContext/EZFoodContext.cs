using System.Reflection.Emit;
using EZFood.Domain.Entities.Models;
using EZFood.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EZFood.Infrastructure.Persistence.DbContext;

public class EZFoodContext(DbContextOptions<EZFoodContext> options) :
    IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>(options)
{
    public DbSet<User> UserProfile { get; set; }
    public DbSet<CuisineType> CuisineTypes { get; set; }
    public DbSet<TruckDetail> TruckDetails { get; set; }
    public DbSet<CuisineTypeTruckDetail> TruckDetailCuisineTypes { get; set; }
    public DbSet<OnboardingAction> OnboardingActions { get; set; }
    public DbSet<UserEvent> UserEvents { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Configuring identity relations
        builder.Entity<ApplicationUser>()
            .HasOne(a => a.Profile)
            .WithOne()
            .HasForeignKey<User>(u => u.IdentityUserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.HasIndex(u => u.Email).IsUnique(true);
            entity.HasIndex(e => e.PhoneNumber).IsUnique(true);            
        });

        builder.Entity<CuisineTypeTruckDetail>().HasKey(table => new { table.CuisineTypesId, table.TruckDetailsId });
        builder.Entity<TruckDetail>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.HasIndex(u => u.BusinessEmail).IsUnique(true);
            entity.HasIndex(e => e.PhoneNumber).IsUnique(true);
            entity.HasMany(s => s.CuisineTypes) // Student can enroll in many Courses
                .WithMany(c => c.TruckDetails) // Course can have many Students
                .UsingEntity<CuisineTypeTruckDetail>();
        });
    }
}
