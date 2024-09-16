using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using WebAPI.Application.DTOs;
using WebAPI.Domain.Constants;
using WebAPI.Domain.Entities;

namespace WebAPI.Infrastructure.Data;
public static class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        IdentityResult roleResult;

        foreach (var roleName in RoleConstants.AllRoles)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }
}

