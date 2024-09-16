using Microsoft.AspNetCore.Identity;
using WebAPI.Application.Interfaces;

namespace WebAPI.Application.Services;
public class UserManagerDecorator<TUser> : IUserManagerDecorator<TUser> where TUser : class
{
    private readonly UserManager<TUser> _userManager;

    public UserManagerDecorator(UserManager<TUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IdentityResult> CreateAsync(TUser user, string password)
    {
        return await _userManager.CreateAsync(user, password);
    }

    public async Task<IdentityResult> AddToRoleAsync(TUser user, string role)
    {
        return await _userManager.AddToRoleAsync(user, role);
    }
    public async Task<TUser> FindByNameAsync(string userName)
    {
        return await _userManager.FindByNameAsync(userName);
    }
    public async Task<IList<string>> GetRolesAsync(TUser user)
    {
        return await _userManager.GetRolesAsync(user);
    }

}