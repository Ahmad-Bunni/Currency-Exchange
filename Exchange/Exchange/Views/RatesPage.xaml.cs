using Exchange.ViewModels;
using System.ComponentModel;

using Xamarin.Forms;

namespace Exchange.Views
{
    [DesignTimeVisible(true)]
    public partial class RatesPage : ContentPage
    {
        RatesViewModel viewModel;
        public RatesPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new RatesViewModel(Navigation);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await viewModel.Init();
        }
    }
}