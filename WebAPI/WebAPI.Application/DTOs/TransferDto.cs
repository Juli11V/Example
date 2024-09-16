namespace WebAPI.Application.DTOs;
public class TransferDto
{
    public Guid FromAccountId { get; set; }
    public Guid ToAccountId { get; set; }
    public decimal Amount { get; set; }
    public string PaymentPurpose { get; set; }
    public string UserId { get; set; }
    public string Currency { get; set; }
}

