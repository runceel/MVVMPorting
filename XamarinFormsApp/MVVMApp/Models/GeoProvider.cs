﻿using System;
using System.Device.Location;
using System.Threading.Tasks;

namespace MVVMApp.Models
{
    class GeoProvider : IGeoProvider
    {
        public Task<GeoInfo> GetGeoInfoAsync()
        {
            var geo = new GeoCoordinateWatcher();
            var result = geo.TryStart(false, TimeSpan.FromSeconds(2));
            if (result)
            {
                var taskSource = new TaskCompletionSource<GeoInfo>();
                Task.Run(async () =>
                {
                    while(geo.Position.Location.IsUnknown)
                    {
                        await Task.Delay(10);
                    }
                    taskSource.SetResult(new GeoInfo
                    {
                        Lat = geo.Position.Location.Latitude,
                        Lng = geo.Position.Location.Longitude,
                    });
                });
                return taskSource.Task;
            }
            else
            {
                return Task.FromResult(new GeoInfo
                {
                    Lat = double.NaN,
                    Lng = double.NaN,
                });
            }
        }
    }
}
