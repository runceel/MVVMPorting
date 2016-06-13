using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMApp.Models
{
    public interface IGeoProvider
    {
        Task<GeoInfo> GetGeoInfoAsync();
    }

    public class GeoInfo
    {
        /// <summary>
        /// 緯度
        /// </summary>
        public double Lat { get; set; }
        
        /// <summary>
        /// 経度
        /// </summary>
        public double Lng { get; set; }
    }
}
