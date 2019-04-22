using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace Exchange.ViewModels
{
    public class AboutViewModel : BasePageViewModel
    {
        public AboutViewModel()
        {
            Title = "About";

        }

        public ICommand OpenWebCommand { get; }
    }
}