using WebAPI.Application.DTOs;
namespace WebAPI.Application.Interfaces;
public interface IAuthService
{
    Task<object> RegisterAsync(RegisterRequest request);
    Task<string> LoginAsync(LoginRequest request);
}

