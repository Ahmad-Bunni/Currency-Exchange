using Acr.UserDialogs;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Exchange.ViewModels
{
    public class BasePageViewModel : BindableBase, INavigationAware
    {
        protected bool IsConnectd => Connectivity.NetworkAccess == NetworkAccess.Internet;
        protected IUserDialogs Dialogs;
        protected INavigationService NavigationService;
        public BasePageViewModel(IUserDialogs userDialogs, INavigationService navigationService)
        {
            Dialogs = userDialogs;
            NavigationService = navigationService;
        }

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                if (value)
                    Dialogs.ShowLoading("Loading");
                else
                    Dialogs.HideLoading();

                SetProperty(ref isBusy, value, () => RaisePropertyChanged(nameof(IsBusy)));

            }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set
            {
                SetProperty(ref title, value);
            }
        }

        public virtual async void OnNavigatedFrom(INavigationParameters parameters)
        {
            await Task.FromResult(0);
        }

        public virtual async void OnNavigatedTo(INavigationParameters parameters)
        {
            await Task.FromResult(0);
        }

        public virtual async void OnNavigatingTo(INavigationParameters parameters)
        {
            await Task.FromResult(0);
        }
    }
}
