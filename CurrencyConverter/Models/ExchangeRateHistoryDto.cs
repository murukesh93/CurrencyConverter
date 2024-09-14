using System.Collections.Generic;

namespace CurrencyConverter.Models
{
    public class ExchangeRateHistoryDto
    {
        public decimal Amount { get; set; }
        public string Base { get; set; }
        public DateTime Start_date { get; set; }
        public DateTime End_date { get; set; }
        public Dictionary<DateTime, Dictionary<string, decimal>> Rates { get; set; }
    }
}
