using CurrencyConverter.Models;
using CurrencyConverter.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace CurrencyConverter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ExchangeRateService _exchangeRateService;

        public CurrencyController(ExchangeRateService exchangeRateService)
        {
            _exchangeRateService = exchangeRateService;
        }

        [HttpGet("ExchangeRates")]
        public async Task<IActionResult> GetExchangeRates([Required] string currency)
        {
            try
            {
                var exchangeRates = await _exchangeRateService.GetExchangeRatesAsync(currency);
                if (exchangeRates == null )
                {
                    return StatusCode(404, "No record found!");
                }
                return Ok(exchangeRates);
            }
            catch (Exception ex)
            {
                if(ex.ToString().Contains("404 (Not Found)"))
                    return StatusCode(400, "Invalid curency!");
                else
                return StatusCode(500, $"Error calling API: {ex.Message}");
            }
        }

        [HttpGet("CurrencyConversion")]
        public async Task<IActionResult> GetCurrencyConversion([Required] decimal amount, [Required]  string currencyFrom, [Required] string currencyTo)
        {
            try
            {
                var exchangeRates = await _exchangeRateService.GetCurrencyConversionAsync(amount, currencyFrom, currencyTo);
                if (exchangeRates == null)
                {
                    return StatusCode(404, "No record found!");
                }
                else
                {
                    
                    return Ok(new { Amount= exchangeRates?.Rates?.First().Value});
                }
                
            }
            catch (Exception ex)
            {
                if (ex.ToString().Contains("404 (Not Found)"))
                    return StatusCode(400, "Invalid curency!");
                else
                    return StatusCode(500, $"Error calling API: {ex.Message}");
            }
        }

        [HttpGet("ExchangeRateHistory")]
        public async Task<IActionResult> GetExchangeRateHistory([Required] DateTime fromDate, [Required] DateTime toDate, int page=1 , int count=10)
        {
            try
            {
                DateTime endDate = toDate >= fromDate.AddDays(page * count) ? fromDate.AddDays(page * count) : toDate;
                var exchangeRates = await _exchangeRateService.GetExchangeRateHistoryAsync(fromDate, endDate);
                if (exchangeRates == null)
                {
                    return StatusCode(404, "No record found!");
                }

                return Ok(exchangeRates);
            }
            catch (Exception ex)
            {
                if (ex.ToString().Contains("404 (Not Found)"))
                    return StatusCode(400, "Invalid curency!");
                else
                    return StatusCode(500, $"Error calling API: {ex.Message}");
            }
        }

    }
}
