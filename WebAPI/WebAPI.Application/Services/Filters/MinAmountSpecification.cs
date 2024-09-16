using System.Linq.Expressions;
using WebAPI.Application.Interfaces;
using WebAPI.Domain.Entities;

namespace WebAPI.Application.Services.Filters;
public class MinAmountSpecification : ISpecification<Transaction>
{
    private readonly decimal _minAmount;

    public MinAmountSpecification(decimal minAmount) => _minAmount = minAmount;

    public Expression<Func<Transaction, bool>> ToExpression()
    {
        return transaction => transaction.Amount >= _minAmount;
    }
}

