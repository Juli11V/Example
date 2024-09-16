using Microsoft.AspNetCore.Identity;

namespace WebAPI.Application.Interfaces
{
    public interface ISignInManagerDecorator<TRole> where TRole : class
    {
        Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure);
    }
}