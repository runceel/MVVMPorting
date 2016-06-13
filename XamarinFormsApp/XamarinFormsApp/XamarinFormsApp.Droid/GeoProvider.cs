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
using MVVMApp.Models;
using Android.Locations;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(XamarinFormsApp.Droid.GeoProvider))]

namespace XamarinFormsApp.Droid
{
    public class GeoProvider : IGeoProvider
    {
        public static Context CurrentContext { get; set; }

        public Task<GeoInfo> GetGeoInfoAsync()
        {
            var ls = (LocationManager)CurrentContext.GetSystemService(Context.LocationService);
            var criteria = new Criteria
            {
                Accuracy = Accuracy.Coarse,
                PowerRequirement = Power.Low,
            };
            var provider = ls.GetBestProvider(criteria, true);
            var taskSource = new TaskCompletionSource<GeoInfo>();

            ls.RequestLocationUpdates(provider, 0, 0, new LocationListener(ls, taskSource));

            return taskSource.Task;
        }

        class LocationListener : Java.Lang.Object, ILocationListener
        {
            private TaskCompletionSource<GeoInfo> TaskSource { get; }
            private LocationManager LocationManager { get; }

            public LocationListener(LocationManager ls, TaskCompletionSource<GeoInfo> taskSource)
            {
                this.LocationManager = ls;
                this.TaskSource = taskSource;
            }

            public void OnLocationChanged(Location location)
            {
                this.TaskSource.SetResult(new GeoInfo
                {
                    Lat = location.Latitude,
                    Lng = location.Longitude,
                });
                this.LocationManager.RemoveUpdates(this);
            }

            public void OnProviderDisabled(string provider)
            {
            }

            public void OnProviderEnabled(string provider)
            {
            }

            public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
            {
            }
        }
    }
}