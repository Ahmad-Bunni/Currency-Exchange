using Acr.UserDialogs;
using Domain.Interface;
using Domain.Services;
using Exchange.ViewModels;
using Exchange.Views;
using Prism.Autofac;
using Prism.Ioc;
using System;
using Xamarin.Forms;

namespace Exchange
{
    public partial class App : PrismApplication
    {
        public static bool UseMockDataStore = false;

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync(new Uri($"/{nameof(MasterPage)}/{nameof(NavigationPage)}/{nameof(RatesPage)}", UriKind.Absolute));
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MasterPage, MasterPageViewModel>();
            containerRegistry.RegisterForNavigation<RatesPage, RatesPageViewModel>();
            containerRegistry.RegisterForNavigation<AboutPage, AboutViewModel>();
            containerRegistry.RegisterSingleton<ICurrencyHttpService, CurrencyHttpService>();
            containerRegistry.RegisterSingleton<IExchangeService, ExchangeService>();
            containerRegistry.RegisterInstance(UserDialogs.Instance);
        }
    }
}
