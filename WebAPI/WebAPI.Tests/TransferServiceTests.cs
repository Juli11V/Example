using FluentAssertions;
using Moq;
using WebAPI.Application.DTOs;
using WebAPI.Application.Interfaces;
using WebAPI.Application.Kafka;
using WebAPI.Application.Services;
using WebAPI.Domain.Entities;
using Xunit;

namespace WebAPI.Tests
{
    public class TransferServiceTests
    {
        [Fact]
        public async Task TransferAsync_InsufficientFunds_ReturnsFailureResult()
        {
            var transferRequest = new TransferRequest
            {
                FromAccountId = Guid.NewGuid(),
                ToAccountId = Guid.NewGuid(),
                Amount = 1000m,
                Currency = "USD",
                UserId = "user123",
                PaymentPurpose = "Test transfer"
            };

            var fromAccount = new Account
            {
                Id = transferRequest.FromAccountId,
                Balance = 500m
            };

            var accountRepositoryMock = new Mock<IRepository<Account>>();
            accountRepositoryMock.Setup(repo => repo.GetByIdAsync(transferRequest.FromAccountId)).ReturnsAsync(fromAccount);
            accountRepositoryMock.Setup(repo => repo.GetByIdAsync(transferRequest.ToAccountId)).ReturnsAsync(new Account());

            var currencyApiClientMock = new Mock<ICurrencyApiClient>();
            var transferRepositoryMock = new Mock<IRepository<Transaction>>();

            var unitOfWorkMock = new Mock<IUnitOfWork>();

            var kafkaMock = new Mock<IKafkaProducer>();

            var transferService = new TransferService(unitOfWorkMock.Object, currencyApiClientMock.Object, kafkaMock.Object);

            var result = await transferService.TransferAsync(transferRequest);

            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be("Insufficient funds");
        }

    }
}
