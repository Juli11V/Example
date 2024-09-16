using System.Linq.Expressions;

namespace WebAPI.Application.Interfaces;

public interface ISpecification<T>
{
    Expression<Func<T, bool>> ToExpression();
}

