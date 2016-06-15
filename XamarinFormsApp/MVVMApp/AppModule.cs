using Microsoft.Practices.Unity;
using MVVMApp.Models;
using MVVMApp.Views;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMApp
{
    class AppModule : IModule
    {
        private IUnityContainer Container { get; }

        private IRegionManager RegionManager { get; }

        public AppModule(IUnityContainer container, IRegionManager regionManager)
        {
            this.Container = container;
            this.RegionManager = regionManager;
        }

        public void Initialize()
        {
            this.Container.RegisterType<IGeoProvider, GeoProvider>(new ContainerControlledLifetimeManager());
            this.Container.RegisterType<HotpepperApp>(new ContainerControlledLifetimeManager());
            this.Container.RegisterType<object, GeoInfoView>(nameof(GeoInfoView));
            this.Container.RegisterType<object, DetailView>(nameof(DetailView));

            this.RegionManager.RequestNavigate("Master", nameof(GeoInfoView));
            this.RegionManager.RequestNavigate("Detail", nameof(DetailView));

        }
    }
}
