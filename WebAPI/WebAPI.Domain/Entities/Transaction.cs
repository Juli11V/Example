using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Domain.Entities;
public abstract class Transaction : BaseEntity
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime Timestamp { get; set; }
    public string UserId { get; set; }
    public string TransactionType { get; set; }
    public string? ExternalTransactionId { get; set; } = null;
    public Guid AccountId { get; set; }
    public virtual Account Account { get; set; }
}

