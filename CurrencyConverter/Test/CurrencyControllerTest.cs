using CurrencyConverter.Controllers;
using CurrencyConverter.Models;
using CurrencyConverter.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CurrencyConverter.Test
{
    public class ExchangeRateControllerTests
    {

        private readonly ExchangeRateService _exchangeRateService;
        private readonly CurrencyController _controller;

        public ExchangeRateControllerTests()
        {
            _exchangeRateService = new Mock<ExchangeRateService>().Object; 
            _controller = new Mock<CurrencyController>().Object;
        }

        [Fact]
        public async Task GetExchangeRates_ReturnsOk_WhenRatesFound()
        {

            var currency = "USD"; 
            var result = await _controller.GetExchangeRates(currency);
            var okResult =Assert.IsType<ExchangeRateDto>(result);
            Assert.Equal(okResult.Base , currency);
        }




        [Fact]
        public async Task GetCurrencyConversion_ReturnsOk_WhenConversionSuccess()
        { 
            var amount = 100M;
            var currencyFrom = "USD";
            var currencyTo = "EUR";
          
            var result = await _controller.GetCurrencyConversion(amount, currencyFrom, currencyTo);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(90.28M, ((dynamic)okResult.Value).Amount);
        }

        [Fact]
        public async Task GetExchangeRateHistory_ReturnsOk_WhenHistoryFound()
        {
            // Arrange
            var fromDate = DateTime.UtcNow.AddDays(-7);
            var toDate = DateTime.UtcNow;
            // Act
            var result = await _controller.GetExchangeRateHistory(fromDate, toDate, 1, 10);

            // Assert
            var okResult = Assert.IsType<ExchangeRateHistoryDto>(result);
            Assert.Equal(okResult.Start_date, fromDate);
            Assert.Equal(okResult.End_date, toDate);
        }

    }
}
