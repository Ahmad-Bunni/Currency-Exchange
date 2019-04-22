using Xamarin.Forms;
using Exchange.Views;
using Exchange.Startup;

namespace Exchange
{
    public partial class App : Application
    {
        public static bool UseMockDataStore = false;

        public App(AppSetup setup)
        {
            InitializeComponent();

            AppContainer.Container = setup.CreateContainer();

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
