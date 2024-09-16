using System;
using System.Threading;
using System.Threading.Tasks;
using WebAPI.Application.Interfaces;
using WebAPI.Domain;
using WebAPI.Domain.Entities;

namespace WebAPI.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IRepository<Account> _accountRepository;
        private readonly IRepository<Transaction> _transactionRepository;
        private readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);

        public AccountService(IRepository<Account> accountRepository, IRepository<Transaction> transactionRepository)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task<Account> CreateAccountAsync(string userId, string currency)
        {
            var account = new Account
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Currency = currency,
                Balance = 0,
            };

            return await _accountRepository.AddAsync(account);
        }

        public async Task DepositAsync(string userId, decimal amount, Guid accountId)
        {
            await ExecuteTransactionAsync<DepositTransaction>(userId, amount, accountId);
        }

        public async Task<Account> GetAccountAsync(string userId, Guid accountId)
        {
            await _semaphoreSlim.WaitAsync();
            try
            {
                var account = await _accountRepository.SingleOrDefaultAsync(account => account.UserId == userId && account.Id == accountId);
                if (account != null)
                    return account;
                else
                    throw new InvalidOperationException("Account not found.");
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        public async Task WithdrawAsync(string userId, decimal amount, Guid accountId)
        {
            await ExecuteTransactionAsync<WithdrawTransaction>(userId, amount, accountId);
        }

        private async Task ExecuteTransactionAsync<TTransaction>(string userId, decimal amount, Guid accountId)
    where TTransaction : Transaction, IExecutableTransaction, new()
        {
            var account = await GetAccountAsync(userId, accountId);
            var transaction = new TTransaction
            {
                UserId = userId,
                Amount = amount,
                Timestamp = DateTime.UtcNow,
                AccountId = accountId
            };
            transaction.Execute(account);
            await _transactionRepository.AddAsync(transaction);
        }

    }
}
