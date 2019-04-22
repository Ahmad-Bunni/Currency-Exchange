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
    public class RatesViewModel : BasePageViewModel
    {
        private IExchangeService ExchangeService => DependencyService.Get<IExchangeService>();
        public ICommand NavigateChild { get; }
        public ICommand SetBaseCurrency { get; }
        public ICommand RefreshCurrencies { get; }
        public RatesViewModel()
        {
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

        private double _baseAmount { get; set; } = 0.0;
        public double BaseAmount
        {
            get { return _baseAmount; }
            set
            {
                if (_baseAmount != value)
                {
                    if (value > 100000)
                        value = 100000;

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
            List<Currency> updatedCurrencies = _currencies.Select(x => new Currency
            {
                Abbreviation = x.Abbreviation,
                Amount = (x.Rate * BaseAmount).ToString("N4"),
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
                var currencies = await ExchangeService.GetCurrenciesList(BaseCurrency);
                _currencies = currencies.ToList();
                UpdateAmounts();
                LastUpdate = DateTimeOffset.UtcNow.ToString("MMM, dd, yyyy HH:mm");
            }
            else
            {

            }
        }
    }
}
