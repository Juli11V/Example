using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebAPI.Application.Interfaces;
using WebAPI.Domain.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAPI.Application.Services.Filters;
using Moq;
using Xunit;
using WebAPI.Application.Extensions;

namespace WebAPI.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IRepository<Transaction> _transactionRepository;

        public TransactionService(IRepository<Transaction> transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<PagedResult<Transaction>> GetTransactionsAsync(TransactionFilter filter, int page, int pageSize)
        {
            var specifications = filter.ToSpecifications();

            var totalCount = await _transactionRepository.GetTotalCountWithSpecificationsAsync(specifications);

            var transactions = (await _transactionRepository.GetPageWithSpecificationsAsync(specifications, page, pageSize)).ToList();
            return new PagedResult<Transaction>
            {
                Items = transactions,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                CurrentPage = page
            };
        }
    }
}
