namespace WebAPI.Domain.Entities;
public class DepositTransaction : Transaction, IExecutableTransaction
{
    public void Execute(Account account)
    {
        if (Amount <= 0)
        {
            throw new ArgumentException("Deposit amount must be a positive number.");
        }

        if (account.Balance + Amount < 0)
        {
            throw new InvalidOperationException("Invalid deposit: resulting balance would be negative.");
        }

        account.Balance += Amount;
    }
}

