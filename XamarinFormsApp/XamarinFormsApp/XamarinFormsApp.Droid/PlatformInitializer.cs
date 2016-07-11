using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Practices.Unity;
using Prism;
using Prism.Unity;
using MVVMApp.Models;

namespace XamarinFormsApp.Droid
{
    class PlatformInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IGeoProvider, GeoProvider>();
        }
    }
}