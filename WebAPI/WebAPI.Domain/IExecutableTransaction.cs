using WebAPI.Domain.Entities;

namespace WebAPI.Domain;
public interface IExecutableTransaction
{
    void Execute(Account account);
}

