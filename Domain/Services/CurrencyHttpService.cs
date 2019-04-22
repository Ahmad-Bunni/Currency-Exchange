using System;
using System.Threading.Tasks;
using Domain.Interface;
using Domain.Model;
using Refit;

namespace Domain.Services
{
    public class CurrencyHttpService : ICurrencyHttpService
    {
        private readonly ICurrencyHttpService _currencyHttpService;
        public CurrencyHttpService()
        {
            _currencyHttpService = RestService.For<ICurrencyHttpService>("https://api.exchangeratesapi.io/latest");
        }
        public async Task<Currencies> GetAsync([AliasAs("base")] string baseCurrency)
        {
            try
            {
                return await _currencyHttpService.GetAsync(baseCurrency);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
