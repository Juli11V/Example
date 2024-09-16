namespace WebAPI.Application.Interfaces;
public interface ICurrencyApiClient
{
    Task<decimal> ConvertCurrencyAsync(string fromCurrency, string toCurrency, decimal amount);
}

