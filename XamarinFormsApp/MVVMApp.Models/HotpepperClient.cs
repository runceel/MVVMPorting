using MVVMApp.Models.Raw;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace MVVMApp.Models
{
    public interface IHotpepperClient
    {
        Task<Rootobject> SearchAsync(double lat, double lng);
    }

    public class HotpepperClient : IHotpepperClient
    {
        public async Task<Rootobject> SearchAsync(double lat, double lng)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue("sample", "1.0"));
            var json = await client.GetStringAsync($"http://webservice.recruit.co.jp/hotpepper/gourmet/v1/?key={ApiKey.Key}&{nameof(lat)}={lat}&{nameof(lng)}={lng}&format=json");
            return JsonConvert.DeserializeObject<Rootobject>(json);
        }
    }
}
