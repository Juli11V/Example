using Microsoft.AspNetCore.Identity;
using WebAPI.Application.Interfaces;

namespace WebAPI.Application.Services
{
    public class SignInManagerDecorator<TUser> : ISignInManagerDecorator<TUser> where TUser : class
    {
        private readonly SignInManager<TUser> _signInManager;

        public SignInManagerDecorator(SignInManager<TUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
        {
            return await _signInManager.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure);
        }
    }
}