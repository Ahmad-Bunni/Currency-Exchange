using Acr.UserDialogs;
using Common.Events;
using Prism.Events;
using Prism.Navigation;

namespace Exchange.ViewModels
{
    public class AboutViewModel : BasePageViewModel
    {
        private readonly IEventAggregator _ea;
        public AboutViewModel(IUserDialogs userDialogs, INavigationService navigationService, IEventAggregator eventAggregator)
         : base(userDialogs, navigationService)
        {
            _ea = eventAggregator;

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            _ea.GetEvent<PersistNavigationEvent>().Publish(false);
        }
    }
}