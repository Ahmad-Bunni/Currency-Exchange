using Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IExchangeService
    {
        Task<IEnumerable<Currency>> GetCurrenciesList(string baseCurrency);
    }
}
