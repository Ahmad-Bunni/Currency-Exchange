using Domain.Interface;
using Domain.Model;
using Exchange.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Exchange.ViewModels
{
    public class RatesPageViewModel : BasePageViewModel
    {
        private readonly IExchangeService _exchangeService;
        public ICommand NavigateChild { get; }
        public ICommand SetBaseCurrency { get; }
        public ICommand RefreshCurrencies { get; }
        public RatesPageViewModel(IExchangeService exchangeService)
        {
            _exchangeService = exchangeService;

            SetBaseCurrency = new Command(async (selectedCurrency) =>
            {
                try
                {
                    IsBusy = true;
                    BaseCurrency = selectedCurrency as string;
                    await UpdateCurrencies();
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    IsBusy = false;
                }

            });

            RefreshCurrencies = new Command(async () =>
            {
                try
                {
                    IsBusy = true;
                    await UpdateCurrencies();

                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    IsBusy = false;

                }

            });
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
                OnPropertyChanged(nameof(Currencies));
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
                    OnPropertyChanged(nameof(BaseAmount));
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

                    OnPropertyChanged(nameof(BaseCurrency));
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
                    OnPropertyChanged(nameof(LastUpdate));
                }
            }
        }


        public async override Task Init()
        {
            await base.Init();

            try
            {
                IsBusy = true;

                if (Currencies == null)
                {
                    BaseCurrency = "EUR";
                    await UpdateCurrencies();
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

        private async Task UpdateCurrencies()
        {
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
    }
}
