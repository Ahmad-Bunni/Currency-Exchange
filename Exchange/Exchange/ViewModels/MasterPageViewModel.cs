using Acr.UserDialogs;
using Common.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Threading.Tasks;

namespace Exchange.ViewModels
{
    public class MasterPageViewModel : BasePageViewModel
    {
        public DelegateCommand<string> NavigateCommand { get; set; }
        private readonly INavigationService _navigationService;
        private readonly IEventAggregator _ea;

        public MasterPageViewModel(INavigationService navigationService, IUserDialogs userDialogs, IEventAggregator eventAggregator) : base(userDialogs, navigationService)
        {
            _navigationService = navigationService;
            _ea = eventAggregator;

            NavigateCommand = new DelegateCommand<string>(NavigateAsync);
        }
        private async void NavigateAsync(string page)
        {
            _ea.GetEvent<PersistNavigationEvent>().Publish(true);

            await _navigationService.NavigateAsync(new Uri(page, UriKind.Relative));

        }
    }
}
