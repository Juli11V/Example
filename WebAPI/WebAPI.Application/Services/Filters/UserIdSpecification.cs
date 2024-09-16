using System.Linq;
using System.Linq.Expressions;
using WebAPI.Application.Interfaces;
using WebAPI.Domain.Entities;
namespace WebAPI.Application.Services.Filters;
public class UserIdSpecification : ISpecification<Transaction>
{
    private readonly List<string> _userIds;

    public UserIdSpecification(List<string> userIds) => _userIds = userIds;

    public Expression<Func<Transaction, bool>> ToExpression()
    {
        return transaction => _userIds.Contains(transaction.UserId);
    }
}

