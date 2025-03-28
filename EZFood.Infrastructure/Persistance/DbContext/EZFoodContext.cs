using EZFood.Domain.Entities.Models;
using EZFood.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EZFood.Infrastructure.Persistance.DbContext;

public class EZFoodContext(DbContextOptions<EZFoodContext> options):IdentityDbContext<ApplicationUser,IdentityRole<Guid>,Guid>(options)
{
    public DbSet<User> UserProfile { get; set; }



    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<ApplicationUser>()
        .HasOne(a => a.Profile)
        .WithOne()
        .HasForeignKey<User>(u => u.IdentityUserId)
        .OnDelete(DeleteBehavior.Cascade);
    }
}
