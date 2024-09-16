using System.Linq.Expressions;
using WebAPI.Application.Interfaces;
using WebAPI.Domain.Entities;

namespace WebAPI.Application.Services.Filters;
public class StartDateSpecification : ISpecification<Transaction>
{
    private readonly DateTime _startDate;
    public StartDateSpecification(DateTime startDate) => _startDate = startDate;

    public Expression<Func<Transaction, bool>> ToExpression()
    {
        return transaction => transaction.Timestamp >= _startDate;
    }
}

