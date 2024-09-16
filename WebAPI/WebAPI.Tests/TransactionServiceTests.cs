using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Application.Services;
using WebAPI.Domain.Entities;
using WebAPI.Application.Services.Filters;
using Xunit;
using WebAPI.Application.Interfaces;

namespace WebAPI.Tests
{
    public class TransactionServiceTests
    {
        [Fact]
        public async Task GetTransactionsAsync_ReturnsCorrectTransactions()
        {
            var transactions = new List<Transaction>
            {
                new DepositTransaction { Amount = 50, Timestamp = DateTime.Parse("2022-06-01") },
                new WithdrawTransaction { Amount = 100, Timestamp = DateTime.Parse("2022-06-02") },
                new DepositTransaction { Amount = 200, Timestamp = DateTime.Parse("2022-06-03") }
            };

            var mockRepository = new Mock<IRepository<Transaction>>();

            var service = new TransactionService(mockRepository.Object);
            var filter = new TransactionFilter();

            var result = await service.GetTransactionsAsync(filter, 1, 10);
            var resultTransactions = result.Items;
            var totalCount = result.TotalCount;

            resultTransactions.Should().BeEquivalentTo(transactions);
            totalCount.Should().Be(3);
        }
    }
}
