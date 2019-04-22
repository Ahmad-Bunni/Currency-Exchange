using Exchange.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Exchange.Views
{
    [DesignTimeVisible(true)]
    public partial class RatesPage : BasePage<RatesViewModel>
    {
        public RatesPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await ViewModel.Init();
        }

    }
}