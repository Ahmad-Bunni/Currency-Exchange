using Acr.UserDialogs;
using Common.Events;
using Domain.Interface;
using Domain.Model;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Exchange.ViewModels
{
    public class RatesPageViewModel : BasePageViewModel
    {
        private readonly IExchangeService _exchangeService;
        public DelegateCommand NavigateChild { get; }
        public DelegateCommand<string> RefreshCurrenciesCommand { get; }
        private readonly IEventAggregator _ea;
        public RatesPageViewModel(IExchangeService exchangeService, IUserDialogs userDialogs, INavigationService navigationService, IEventAggregator eventAggregator)
            : base(userDialogs, navigationService)
        {
            _exchangeService = exchangeService;
            _ea = eventAggregator;

            RefreshCurrenciesCommand = new DelegateCommand<string>(async (string param) => await RefreshCurrencies(param));
        }

        private List<Currency> _currencies { get; set; }
        private ObservableCollection<Currency> _currenciesCollection { get; set; }
        public ObservableCollection<Currency> Currencies
        {
            get { return _currenciesCollection; }
            set
            {
                if (_currenciesCollection != value)
                    _currenciesCollection = value;
                RaisePropertyChanged(nameof(Currencies));
            }
        }

        private string _baseAmount { get; set; } = "0";
        public string BaseAmount
        {
            get { return _baseAmount; }
            set
            {
                if (_baseAmount != value)
                {
                    if (!string.IsNullOrWhiteSpace(value) && Convert.ToDouble(value) > 100000)
                        value = "100000";

                    _baseAmount = value;
                    RaisePropertyChanged(nameof(BaseAmount));
                    UpdateAmounts();
                }
            }
        }

        private string _baseCurrency { get; set; }
        public string BaseCurrency
        {
            get { return _baseCurrency; }
            set
            {
                if (_baseCurrency != value)
                {
                    _baseCurrency = value;

                    RaisePropertyChanged(nameof(BaseCurrency));
                }

            }
        }
        private string _lastUpdate { get; set; }
        public string LastUpdate
        {
            get { return _lastUpdate; }
            set
            {

                if (_lastUpdate != value)
                {
                    _lastUpdate = value;
                    RaisePropertyChanged(nameof(LastUpdate));
                }
            }
        }

        //update currencies / base amount if present
        private async Task RefreshCurrencies(string baseCurrency)
        {
            if (!string.IsNullOrWhiteSpace(baseCurrency))
                BaseCurrency = baseCurrency;

            await UpdateCurrencies();

        }

        //re-calculate amounts and update in memory
        private void UpdateAmounts()
        {
            double currentAmount = string.IsNullOrWhiteSpace(BaseAmount) ? 0.0 : Convert.ToDouble(BaseAmount);

            List<Currency> updatedCurrencies = _currencies.Select(x => new Currency
            {
                Abbreviation = x.Abbreviation,
                Amount = (x.Rate * currentAmount).ToString("N4"),
                Rate = x.Rate,
                IsBase = x.IsBase,
                Logo = x.Logo
            }).ToList();

            Currencies = new ObservableCollection<Currency>(updatedCurrencies);
        }

        //update through api
        private async Task UpdateCurrencies()
        {
            try
            {
                IsBusy = true;

                if (IsConnectd)
                {
                    var currencies = await _exchangeService.GetCurrenciesList(BaseCurrency);
                    _currencies = currencies.ToList();
                    UpdateAmounts();
                    LastUpdate = DateTime.Now.ToString("MMM, dd, yyyy HH:mm");
                }
                else
                {
                    if (await Dialogs.ConfirmAsync("You are currently offline. Please connect to the internet and try again.", "Offline", "Retry", "Cancel"))
                    {
                        await UpdateCurrencies();
                    };
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                IsBusy = false;
            }

        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            await UpdateCurrencies();

            // close navigation event
            _ea.GetEvent<PersistNavigationEvent>().Publish(false);
        }
    }
}
