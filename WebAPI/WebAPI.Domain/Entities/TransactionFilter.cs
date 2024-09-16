namespace WebAPI.Domain.Entities;
public class TransactionFilter
{
    public List<string> TransactionTypes { get; set; }
    public decimal? MinAmount { get; set; }
    public decimal? MaxAmount { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public List<string> UserIds { get; set; }
}

