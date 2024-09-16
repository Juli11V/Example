using Microsoft.AspNetCore.Identity;

namespace WebAPI.Application.Interfaces;
public interface IUserManagerDecorator<TUser> where TUser : class
{
    Task<IdentityResult> CreateAsync(TUser user, string password);
    Task<IdentityResult> AddToRoleAsync(TUser user, string role);
    Task<TUser> FindByNameAsync(string userName);
    Task<IList<string>> GetRolesAsync(TUser user);
}