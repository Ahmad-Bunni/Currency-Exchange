﻿using Autofac;
using Exchange.Startup;
using Exchange.ViewModels;
using Xamarin.Forms;

namespace Exchange.Views
{
    public abstract class BasePage<T> : ContentPage where T : BasePageViewModel
    {
        public T ViewModel { get; }

        public BasePage()
        {
            ViewModel = AppContainer.Container.Resolve<T>();
            BindingContext = ViewModel;
        }

    }
}