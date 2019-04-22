using Domain.Model;
using Refit;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface ICurrencyHttpService
    {
        [Get("")]
        Task<Currencies> GetAsync([AliasAs("base")] string baseCurrency);
    }
}
