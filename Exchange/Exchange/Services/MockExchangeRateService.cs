using Exchange.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Services
{
    public class MockExchangeRateService : IExchangeService
    {
        List<Currency> currencies;
        public MockExchangeRateService()
        {
            currencies = new List<Currency>
            {
                new Currency
                {
                    Name = "USD"
                },
                new Currency
                {
                    Name = "EUR"
                },
                  new Currency
                {
                    Name = "GBP"
                }
            };
        }

        public async Task<IEnumerable<Currency>> GetCurrenciesList()
        {
            return await Task.FromResult(currencies);
        }
    }
}
