using MVVMApp.Views;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Unity;
using Prism.Modularity;

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
