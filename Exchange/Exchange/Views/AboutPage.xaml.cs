using Exchange.ViewModels;
using System;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Exchange.Views
{
    [DesignTimeVisible(true)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
            BindingContext = new AboutViewModel();
        }
    }
}