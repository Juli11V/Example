using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using WebAPI.Application.Interfaces;
using WebAPI.Application.Services;
using WebAPI.Domain.Entities;
using Xunit;

namespace WebAPI.Tests
{
    public class AccountServiceTests
    {
        private readonly Mock<IRepository<Account>> _mockAccountRepository;
        private readonly Mock<IRepository<Transaction>> _mockTransactionRepository;
        private readonly IAccountService _accountService;

        public AccountServiceTests()
        {
            _mockAccountRepository = new Mock<IRepository<Account>>();
            _mockTransactionRepository = new Mock<IRepository<Transaction>>();
            _accountService = new AccountService(_mockAccountRepository.Object, _mockTransactionRepository.Object);
        }

        [Fact]
        public async Task CreateAccount_Should_Create_Account()
        {
            var userId = "usertest";
            var currency = "USD";
            var expectedAccount = new Account { UserId = userId, Currency = currency, Balance = 0 };
            _mockAccountRepository.Setup(repo => repo.AddAsync(It.IsAny<Account>())).ReturnsAsync(expectedAccount);

            var createdAccount = await _accountService.CreateAccountAsync(userId, currency);

            createdAccount.Should().BeEquivalentTo(expectedAccount);
        }

        [Fact]
        public async Task DepositAsync_Should_Increase_Balance()
        {
            var userId = "usertest";
            var accountId = Guid.NewGuid();
            var amount = 100m;
            var account = new Account { Id = accountId, UserId = userId, Balance = 0, Currency = "USD" };
            _mockAccountRepository.Setup(repo => repo.SingleOrDefaultAsync(It.IsAny<Expression<Func<Account, bool>>>()))
                                  .ReturnsAsync(account);

            await _accountService.DepositAsync(userId, amount, accountId);

            account.Balance.Should().Be(amount);
        }

        [Fact]
        public async Task WithdrawAsync_Should_Decrease_Balance()
        {
            var userId = "usertest";
            var accountId = Guid.NewGuid();
            var amount = 50m;
            var account = new Account { Id = accountId, UserId = userId, Balance = 100, Currency = "USD" };
            _mockAccountRepository.Setup(repo => repo.SingleOrDefaultAsync(It.IsAny<Expression<Func<Account, bool>>>()))
                                  .ReturnsAsync(account);

            await _accountService.WithdrawAsync(userId, amount, accountId);

            account.Balance.Should().Be(50);
        }
    }
}
