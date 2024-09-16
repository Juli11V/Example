namespace WebAPI.Domain.Entities;
public class ExchangeRates
{
    public string Base { get; set; }
    public DateTime Date { get; set; }
    public Dictionary<string, decimal> Rates { get; set; }
}

