using Microsoft.AspNetCore.Identity;

namespace WebAPI.Domain.Entities;
public class UserRole
{
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    public string RoleId { get; set; }
    public IdentityRole Role { get; set; }
}

