using CurrencyConverter.Models;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace CurrencyConverter.Services
{
    public class ExchangeRateService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration; 
        public ExchangeRateService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
  
        public async Task<ExchangeRateDto> GetExchangeRatesAsync( string currency)
        {
            string apiBaseURL = _configuration["AppSettings:ApiBaseURL"];
            string apiUrl = apiBaseURL + "latest?from=" + currency;

            try
            {
                var option =new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var exchangeRateDto = JsonSerializer.Deserialize<ExchangeRateDto>(responseBody, option);

                return exchangeRateDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ExchangeRateHistoryDto> GetExchangeRateHistoryAsync(DateTime fromDate, DateTime toDate)
        {
            string apiBaseURL = _configuration["AppSettings:ApiBaseURL"];
            string apiUrl = apiBaseURL +  fromDate.ToString("yyyy-MM-dd")+ ".." + toDate.ToString("yyyy-MM-dd");

            try
            {
                var option = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var exchangeRateHistoryDto = JsonSerializer.Deserialize<ExchangeRateHistoryDto>(responseBody, option);
                
                return exchangeRateHistoryDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ExchangeRateDto> GetCurrencyConversionAsync( decimal amount, string currencyFrom, string currencyTo )
        {
            string apiBaseURL = _configuration["AppSettings:ApiBaseURL"]; // amount
            string apiUrl = apiBaseURL + "latest?amount=" + amount +"&from="+currencyFrom+ "&to="+currencyTo;

            try
            {
                var option = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var exchangeRateDto = JsonSerializer.Deserialize<ExchangeRateDto>(responseBody, option);

                return exchangeRateDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
