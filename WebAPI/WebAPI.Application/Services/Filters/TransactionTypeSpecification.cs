using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Application.Interfaces;
using WebAPI.Domain.Entities;

namespace WebAPI.Application.Services.Filters
{
    public class TransactionTypeSpecification : ISpecification<Transaction>
    {
        private readonly List<string> _transactionTypes;

        public TransactionTypeSpecification(List<string> transactionTypes)
        {
            _transactionTypes = transactionTypes;
        }

        public Expression<Func<Transaction, bool>> ToExpression()
        {
            return transaction => _transactionTypes.Contains(transaction.TransactionType);
        }

    }

}
