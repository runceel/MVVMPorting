using Microsoft.Practices.Unity;
using MVVMApp.Views;
using Prism.Modularity;
using Prism.Unity;
using System.Windows;

namespace MVVMApp
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return this.Container.Resolve<MainWindow>();
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();
            var catalog = this.ModuleCatalog as ModuleCatalog;
            catalog.AddModule(typeof(AppModule));
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }
    }
}
