using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Exchange.Services;
using Exchange.Views;

namespace Exchange
{
    public partial class App : Application
    {
        public static bool UseMockDataStore = true;

        public App()
        {
            InitializeComponent();

            if (UseMockDataStore)
                DependencyService.Register<MockExchangeRateService>();
            else
                DependencyService.Register<ExchangeRateService>();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
