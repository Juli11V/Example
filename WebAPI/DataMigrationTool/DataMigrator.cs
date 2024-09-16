using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebAPI.Domain;
using WebAPI.Domain.Entities;
using WebAPI.Infrastructure.Data;

namespace DataMigrationTool
{
    public class DataMigrator
    {
        private readonly ILogger<DataMigrator> _logger;
        private readonly AppDbContext _dbContext;
        private readonly string _passwordHash;

        public DataMigrator(ILogger<DataMigrator> logger, AppDbContext dbContext, string passwordHash)
        {
            _logger = logger;
            _dbContext = dbContext;
            _passwordHash = passwordHash;
        }

        public async Task ExecuteMigration(string jsonFilePath, string xmlFilePath)
        {
            try
            {
                var jsonAccountReader = new JsonAccountReader();
                var jsonAccounts = await jsonAccountReader.ReadAccountsFromFile(jsonFilePath);
                await MigrateAccounts(jsonAccounts);

                var xmlAccountReader = new XmlAccountReader();
                var xmlAccounts = await xmlAccountReader.ReadAccountsFromFile(xmlFilePath);
                await MigrateAccounts(xmlAccounts);

                _logger.LogInformation("Data migration completed successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while migrating data.");
            }
        }

        private async Task MigrateAccounts(List<Account> accounts)
        {
            using var transactionSql = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                foreach (var account in accounts)
                {
                    var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.ExternalUserId == account.UserId);
                    if (existingUser == null)
                    {
                        existingUser = new ApplicationUser
                        {
                            Id = Guid.NewGuid(),
                            PasswordHash = _passwordHash,
                            NormalizedUserName = account.UserId.ToUpper(),
                            ExternalUserId = account.UserId,
                            UserName = account.UserId
                        };
                        _dbContext.Users.Add(existingUser);
                        await _dbContext.SaveChangesAsync();
                    }

                    var existingAccount = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.UserId == existingUser.Id.ToString());
                    if (existingAccount == null)
                    {
                        existingAccount = new Account
                        {
                            Id = Guid.NewGuid(),
                            UserId = existingUser.Id.ToString(),
                            Balance = account.Balance,
                            Currency = account.Currency
                        };
                        _dbContext.Accounts.Add(existingAccount);
                    }
                    else
                    {
                        existingAccount.Balance = account.Balance;
                        _dbContext.Accounts.Update(existingAccount);
                    }
                    if (account.Transactions != null && account.Transactions.Any())
                    {
                        foreach (var transactionDto in account.Transactions)
                        {
                            if (existingUser != null && existingAccount != null)
                            {
                                IExecutableTransaction transaction = transactionDto.TransactionType switch
                                {
                                    "Deposit" => new DepositTransaction
                                    {
                                        Id = Guid.NewGuid(),
                                        UserId = existingUser.Id.ToString(),
                                        Amount = transactionDto.Amount,
                                        ExternalTransactionId = transactionDto.ExternalTransactionId,
                                        AccountId = existingAccount.Id
                                    },
                                    "Withdraw" => new WithdrawTransaction
                                    {
                                        Id = Guid.NewGuid(),
                                        UserId = existingUser.Id.ToString(),
                                        Amount = transactionDto.Amount,
                                        ExternalTransactionId = transactionDto.ExternalTransactionId,
                                        AccountId = existingAccount.Id
                                    },
                                    _ => throw new NotSupportedException($"Transaction type '{transactionDto.TransactionType}' is not supported.")
                                };

                                transaction.Execute(account);
                                _dbContext.Transactions.Add((Transaction)transaction);
                            }
                        }
                    }
                }

                await _dbContext.SaveChangesAsync();
                await transactionSql.CommitAsync();
            }
            catch (Exception ex)
            {
                await transactionSql.RollbackAsync();
                _logger.LogError(ex, "An error occurred during data migration.");
                throw;
            }
        }
    }
}
