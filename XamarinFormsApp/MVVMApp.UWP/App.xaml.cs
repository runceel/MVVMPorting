using Prism.Unity.Windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Threading.Tasks;
using MVVMApp.Models;
using MVVMApp.UWP.Models;
using Microsoft.Practices.Unity;
using System.Reflection;

namespace MVVMApp.UWP
{
    sealed partial class App : PrismUnityApplication
    {
        public App()
        {
            this.InitializeComponent();
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            this.Container.RegisterType<IGeoProvider, GeoProvider>(new ContainerControlledLifetimeManager());
            this.Container.RegisterTypes(
                AllClasses.FromApplication()
                    .Where(x => x.Namespace == "MVVMApp.Models")
                    .Where(x => !x.GetTypeInfo().IsAbstract),
                WithMappings.FromAllInterfaces,
                WithName.Default,
                WithLifetime.ContainerControlled);
        }

        protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
            this.NavigationService.Navigate("Main", args.Arguments);
            return Task.CompletedTask;
        }
    }
}
