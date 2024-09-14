namespace CurrencyConverter.Models
{
    public class CurrencyConversionDto
    {
        public decimal Amount { get; set; }
        public string Base { get; set; }
        public DateTime Start_date { get; set; }
        public DateTime End_date { get; set; }
        public Dictionary<DateTime, Dictionary<string, decimal>> Rates { get; set; }
    }
}
