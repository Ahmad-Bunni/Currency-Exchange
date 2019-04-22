using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Interface;
using Domain.Model;
using Xamarin.Forms;

namespace Domain.Services
{
    public class ExchangeService : IExchangeService
    {
        private readonly ICurrencyHttpService _currencyHttpService;
        public ExchangeService(ICurrencyHttpService currencyHttpService)
        {
            _currencyHttpService = currencyHttpService;
        }
        public async Task<IEnumerable<Currency>> GetCurrenciesList(string baseCurrency)
        {
            try
            {
                Currencies currencies = await _currencyHttpService.GetAsync(baseCurrency);

                List<Currency> currencyList = new List<Currency>();

                foreach (var currencyProperty in currencies.Rates.GetType().GetProperties())
                {
                    var value = currencyProperty.GetValue(currencies.Rates);

                    if (value is double rate)
                        currencyList.Add(new Currency
                        {
                            Abbreviation = currencyProperty.Name == "TUR" ? "TRY" : currencyProperty.Name,
                            Amount = "0",
                            IsBase = baseCurrency == (currencyProperty.Name == "TUR" ? "TRY" : currencyProperty.Name),
                            Logo = $"{currencyProperty.Name}.png",
                            Rate = rate == 0 ? 1 : rate
                        });
                }

                return currencyList;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
