using System.Linq.Expressions;
using System.Transactions;
using WebAPI.Application.Interfaces;
using Transaction = WebAPI.Domain.Entities.Transaction;

namespace WebAPI.Application.Services.Filters;
public class MaxAmountSpecification : ISpecification<Transaction>
{
    private readonly decimal _maxAmount;
    public MaxAmountSpecification(decimal maxAmount) => _maxAmount = maxAmount;

    public Expression<Func<Transaction, bool>> ToExpression()
    {
        return transaction => transaction.Amount <= _maxAmount;
    }
}

