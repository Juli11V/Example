using System.Linq.Expressions;
using WebAPI.Application.Interfaces;
using WebAPI.Domain.Entities;

namespace WebAPI.Application.Services.Filters;
public class EndDateSpecification : ISpecification<Transaction>
{
    private readonly DateTime _endDate;
    public EndDateSpecification(DateTime endDate) => _endDate = endDate;

    public Expression<Func<Transaction, bool>> ToExpression()
    {
        return transaction => transaction.Timestamp <= _endDate;
    }

}

