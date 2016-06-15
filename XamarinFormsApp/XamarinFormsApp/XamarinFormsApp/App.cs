using Microsoft.Practices.Unity;
using MVVMApp.Models;
using Prism.Unity;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using XamarinFormsApp.Views;

namespace XamarinFormsApp
{
    public class App : PrismApplication
    {
        protected override void OnInitialized()
        {
            UIDispatcherScheduler.Initialize();
            this.NavigationService.NavigateAsync("MainPage");
        }

        protected override void RegisterTypes()
        {
            this.Container.RegisterTypeForNavigation<MainPage>();
            this.Container.RegisterTypeForNavigation<DetailPage>();
            this.Container.RegisterType<HotpepperApp>(new ContainerControlledLifetimeManager());
        }
    }
}
