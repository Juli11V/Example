using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WebAPI.Application.Interfaces;
using WebAPI.Domain.Constants;
using WebAPI.Domain.Entities;
using WebAPI.Domain.Exceptions;

namespace WebAPI.Application.Services
{
    public class CurrencyApiClient : ICurrencyApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly CurrencyServiceOptions _options;

        public CurrencyApiClient(HttpClient httpClient, IOptions<CurrencyServiceOptions> options)
        {
            _httpClient = httpClient;
            _options = options.Value;
        }

        public async Task<decimal> ConvertCurrencyAsync(string fromCurrency, string toCurrency, decimal amount)
        {
            var response = await _httpClient.GetAsync($"{_options.BaseUrl}/convert/latest?from={fromCurrency}&to={toCurrency}&amount={amount}&apikey={_options.ApiKey}");

            if (!response.IsSuccessStatusCode)
            {
                throw new CurrencyConversionException("An error occurred when receiving the exchange rate");
            }

            string content = await response.Content.ReadAsStringAsync();
            var exchangeRates = JsonSerializer.Deserialize<ExchangeRates>(content);
            if (exchangeRates?.Rates != null && exchangeRates.Rates.ContainsKey(toCurrency))
            {
                decimal rate = exchangeRates.Rates[toCurrency];
                return amount * rate;
            }
            throw new CurrencyConversionException("An error occurred when receiving the exchange rate");
        }
    }
}
