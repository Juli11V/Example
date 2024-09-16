using WebAPI.Domain.Entities;

namespace WebAPI.Application.Interfaces;
public interface IAccountService
{
    Task<Account> CreateAccountAsync(string userId, string currency);
    Task DepositAsync(string userId, decimal amount, Guid accountId);
    Task WithdrawAsync(string UserId, decimal amount, Guid accountId);
    Task<Account> GetAccountAsync(string UserId, Guid accountId);
}

