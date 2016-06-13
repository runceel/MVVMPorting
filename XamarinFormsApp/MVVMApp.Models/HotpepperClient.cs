using MVVMApp.Models.Raw;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MVVMApp.Models
{
    public class HotpepperClient
    {
        public async Task<Rootobject> SearchAsync(double lat, double lng)
        {
            var client = new HttpClient();
            Debug.WriteLine($"http://webservice.recruit.co.jp/hotpepper/gourmet/v1/?key={ApiKey.Key}&{nameof(lat)}={lat}&{nameof(lng)}={lng}&format=json");
            client.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue("sample", "1.0"));
            var json = await client.GetStringAsync($"http://webservice.recruit.co.jp/hotpepper/gourmet/v1/?key={ApiKey.Key}&{nameof(lat)}={lat}&{nameof(lng)}={lng}&format=json");
            return JsonConvert.DeserializeObject<Rootobject>(json);
        }
    }
}
