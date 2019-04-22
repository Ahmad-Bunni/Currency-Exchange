using Xamarin.Forms;
using Exchange.Views;
using Acr.UserDialogs;
using Domain.Interface;
using Domain.Services;

namespace Exchange
{
    public partial class App : Application
    {
        public static bool UseMockDataStore = false;

        public App()
        {
            InitializeComponent();

            if (UseMockDataStore)
            {

            }
            else
            {
                DependencyService.Register<ICurrencyHttpService, CurrencyHttpService>();
                DependencyService.Register<IExchangeService, ExchangeService>();
            }

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
