using WebAPI.Application.Interfaces;
using WebAPI.Application.Services.Filters;
using WebAPI.Domain.Entities;

namespace WebAPI.Application.Extensions;
public static  class TransactionFilterExtensions
{
    public static List<ISpecification<Transaction>> ToSpecifications(this TransactionFilter filter)
    {
        var specifications = new List<ISpecification<Transaction>>();

        if (filter.TransactionTypes != null && filter.TransactionTypes.Any())
        {
            specifications.Add(new TransactionTypeSpecification(filter.TransactionTypes));
        }

        if (filter.MinAmount.HasValue)
        {
            specifications.Add(new MinAmountSpecification(filter.MinAmount.Value));
        }

        if (filter.MaxAmount.HasValue)
        {
            specifications.Add(new MaxAmountSpecification(filter.MaxAmount.Value));
        }

        if (filter.StartDate.HasValue)
        {
            specifications.Add(new StartDateSpecification(filter.StartDate.Value));
        }

        if (filter.EndDate.HasValue)
        {
            specifications.Add(new EndDateSpecification(filter.EndDate.Value));
        }

        if (filter.UserIds != null && filter.UserIds.Any())
        {
            specifications.Add(new UserIdSpecification(filter.UserIds));
        }

        return specifications;
    }
}

