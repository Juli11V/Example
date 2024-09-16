using System.Transactions;
using WebAPI.Domain.Entities;
using Transaction = WebAPI.Domain.Entities.Transaction;

namespace WebAPI.Application.Interfaces;
public interface ITransactionService
{
    Task<PagedResult<Transaction>> GetTransactionsAsync(TransactionFilter filter, int page, int pageSize);
}

