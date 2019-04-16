using Exchange.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Services
{
    public interface IExchangeService
    {
        Task<IEnumerable<Currency>> GetCurrenciesList();
    }
}
