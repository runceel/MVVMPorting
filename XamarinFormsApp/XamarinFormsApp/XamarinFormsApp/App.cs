using Microsoft.Practices.Unity;
using MVVMApp.Models;
using Prism.Unity;
using Reactive.Bindings;
using XamarinFormsApp.Views;

namespace XamarinFormsApp
{
    public class App : PrismApplication
    {
        protected override async void OnInitialized()
        {
            UIDispatcherScheduler.Initialize();
            await this.NavigationService.NavigateAsync("MainPage");
        }

        protected override void RegisterTypes()
        {
            // register pages
            this.Container.RegisterTypeForNavigation<MainPage>();
            this.Container.RegisterTypeForNavigation<DetailPage>();
            // register models
            this.Container.RegisterType<IHotpepperApp, HotpepperApp>(new ContainerControlledLifetimeManager());
            this.Container.RegisterType<IHotpepperClient, HotpepperClient>(new ContainerControlledLifetimeManager());
        }
    }
}
