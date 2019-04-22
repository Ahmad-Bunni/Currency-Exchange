using Acr.UserDialogs;
using Autofac;
using Domain.Interface;
using Domain.Services;
using Exchange.ViewModels;
using IContainer = Autofac.IContainer;

namespace Exchange.Startup
{
    public class AppSetup
    {
        public IContainer CreateContainer()
        {
            var containerBuilder = new ContainerBuilder();
            RegisterDependencies(containerBuilder);
            return containerBuilder.Build();
        }

        protected virtual void RegisterDependencies(ContainerBuilder cb)
        {
            cb.RegisterType<CurrencyHttpService>().As<ICurrencyHttpService>().SingleInstance();
            cb.RegisterType<ExchangeService>().As<IExchangeService>().SingleInstance();
            cb.RegisterType<RatesPageViewModel>().SingleInstance();
            cb.RegisterInstance(UserDialogs.Instance);

        }
    }
}
