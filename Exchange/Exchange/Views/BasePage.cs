using Exchange.ViewModels;
using Xamarin.Forms;

namespace Exchange.Views
{
    public abstract class BasePage<T> : ContentPage where T : BasePageViewModel, new()
    {
        public T ViewModel { get; }

        public BasePage()
        {
            ViewModel = new T();
            BindingContext = ViewModel;
        }

    }
}
