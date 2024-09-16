using WebAPI.Domain.Entities;

namespace WebAPI.Application.Interfaces;
public interface ITokenService
{
    Task<string> GenerateJwtToken(ApplicationUser user);
}