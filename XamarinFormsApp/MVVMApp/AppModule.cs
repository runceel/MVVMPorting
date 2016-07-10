using Microsoft.Practices.Unity;
using MVVMApp.Models;
using MVVMApp.Views;
using Prism.Modularity;
using Prism.Regions;

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
            // register models
            this.Container.RegisterType<IGeoProvider, GeoProvider>(new ContainerControlledLifetimeManager());
            this.Container.RegisterType<IHotpepperApp, HotpepperApp>(new ContainerControlledLifetimeManager());
            this.Container.RegisterType<IHotpepperClient, HotpepperClient>(new ContainerControlledLifetimeManager());
            // register pages
            this.Container.RegisterType<object, GeoInfoView>(nameof(GeoInfoView));
            this.Container.RegisterType<object, DetailView>(nameof(DetailView));

            this.RegionManager.RequestNavigate("Master", nameof(GeoInfoView));
            this.RegionManager.RequestNavigate("Detail", nameof(DetailView));

        }
    }
}
