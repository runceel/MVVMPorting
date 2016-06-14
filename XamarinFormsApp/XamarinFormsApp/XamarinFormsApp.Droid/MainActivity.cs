using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

[assembly:UsesPermission("android.permission.ACCESS_FINE_LOCATION")]
[assembly:UsesPermission("android.permission.ACCESS_COARSE_LOCATION")]

namespace XamarinFormsApp.Droid
{
    [Activity(Label = "XamarinFormsApp", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            CurrentContextHolder.Context = this;

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }

        protected override void OnResume()
        {
            base.OnResume();
            CurrentContextHolder.Context = this;
        }

        protected override void OnPause()
        {
            base.OnPause();
            CurrentContextHolder.Context = null;
        }
    }
}

