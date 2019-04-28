
using System;
using System.Threading.Tasks;
using Common.Events;
using Prism.Events;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Exchange.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPage : MasterDetailPage, IMasterDetailPageOptions
    {
        public MasterPage(IEventAggregator eventAggregator)
        {
            InitializeComponent();

            eventAggregator.GetEvent<PersistNavigationEvent>().Subscribe(SetPersist);

        }

        public bool IsPresentedAfterNavigation => true;

        private async void SetPersist(bool present)
        {
            await Task.Delay(200);

            this.IsPresented = present;

        }
    }
}