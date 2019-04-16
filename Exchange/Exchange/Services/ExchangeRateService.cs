using Exchange.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Exchange.Services
{
    public class ExchangeRateService : IExchangeService
    {
        bool IsConnectd => Connectivity.NetworkAccess == NetworkAccess.Internet;


        Task<IEnumerable<Currency>> IExchangeService.GetCurrenciesList()
        {
            throw new NotImplementedException();
        }
    }
}
