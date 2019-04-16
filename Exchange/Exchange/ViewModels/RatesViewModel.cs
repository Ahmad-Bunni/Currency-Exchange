using Exchange.Models;
using Exchange.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Exchange.ViewModels
{
    public class RatesViewModel : BaseViewModel
    {
        private readonly INavigation _navigation;
        private ObservableCollection<Currency> _currencies { get; set; }
        public ObservableCollection<Currency> Currencies
        {
            get { return _currencies; }
            set
            {
                if (_currencies != value)
                    _currencies = value;

                OnPropertyChanged(nameof(Currencies));
            }
        }
        public RatesViewModel(INavigation navigation)
        {
            _navigation = navigation;

            Title = "Rates";

            NavigateChild = new Command(async () =>
          {
              await _navigation.PushAsync(new ChildPage());

          });
        }

        public ICommand NavigateChild { get; }


        public async override Task Init()
        {
            await base.Init();

            try
            {
                IsBusy = true;

                var currencies = await ExchangeService.GetCurrenciesList();
                Currencies = new ObservableCollection<Currency>(currencies);

            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                IsBusy = false;

            }


        }
    }
}
