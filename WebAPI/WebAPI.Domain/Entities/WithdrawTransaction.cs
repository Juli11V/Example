namespace WebAPI.Domain.Entities;
using WebAPI.Domain.Entities;

public class WithdrawTransaction : Transaction, IExecutableTransaction
{
    public void Execute(Account account)
    {
        if(account.Balance - Amount < 0)
        {
            throw new InvalidOperationException("Insufficient funds.");
        }

        account.Balance -= Amount;
    }
}

