using FluentAssertions;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using WebAPI.Application.Interfaces;
using WebAPI.Application.Services;
using WebAPI.Domain.Constants;
using Xunit;

namespace WebAPI.Tests
{
    public class CurrencyApiClientIntegrationTests
    {
        private readonly IConfiguration _configuration;

        public CurrencyApiClientIntegrationTests()
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }

        [Fact]
        public async Task ConvertCurrencyAsync_SuccessfulApiCall_ReturnsConvertedAmount()
        {
            var httpClient = new HttpClient();
            var currencyServiceOptions = new CurrencyServiceOptions
            {
                ApiKey = _configuration["CurrencyService:ApiKey"]
            };
            var currencyApiClient = new CurrencyApiClient(httpClient, Microsoft.Extensions.Options.Options.Create(currencyServiceOptions));

            
            var convertedAmount = await currencyApiClient.ConvertCurrencyAsync("USD", "EUR", 100m);

            convertedAmount.Should().BeGreaterThan(0, "because a positive value is expected after currency conversion");
        }
    }
}
