using EZFood.Domain.Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace EZFood.Infrastructure.Identity;
public class ApplicationUser : IdentityUser<Guid>
{
    public virtual User? Profile { get; set; }
}
