using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace Exchange.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";

        }

        public ICommand OpenWebCommand { get; }
    }
}