using MVVMApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace MVVMApp.UWP.Models
{
    public class GeoProvider : IGeoProvider
    {
        public async Task<GeoInfo> GetGeoInfoAsync()
        {
            var status = await Geolocator.RequestAccessAsync();
            if (status != GeolocationAccessStatus.Allowed)
            {
                return new GeoInfo();
            }

            var geo = new Geolocator();
            var pos = await geo.GetGeopositionAsync();
            return new GeoInfo
            {
                Lat = pos.Coordinate.Point.Position.Latitude,
                Lng = pos.Coordinate.Point.Position.Longitude,
            };
        }
    }
}
