using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Domain.Entities;
public class TransferTransaction : Transaction
{
    public Guid FromAccountId { get; set; }
    public Guid ToAccountId { get; set; }
    public string PaymentPurpose { get; set; }

    [ForeignKey("FromAccountId")]
    public Account FromAccount { get; set; }

    [ForeignKey("ToAccountId")]
    public Account ToAccount { get; set; }
}

