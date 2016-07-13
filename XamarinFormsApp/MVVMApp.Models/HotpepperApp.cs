using MVVMApp.Models.Raw;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MVVMApp.Models
{
    public interface IHotpepperApp : INotifyPropertyChanged
    {
        GeoInfo GeoInfo { get; }

        Shop SelectedShop { get; }

        ObservableCollection<Shop> Shops { get; }

        Task SetSelectedShopByIdAsync(string id);

        Task LoadGeoInfoAsync();

        Task LoadShopsAsync();
    }

    public class HotpepperApp : BindableBase, IHotpepperApp
    {
        private IGeoProvider GeoProvider { get; }

        private IHotpepperClient HotpepperClient { get; }

        private GeoInfo geoInfo;

        public GeoInfo GeoInfo
        {
            get { return this.geoInfo; }
            private set { this.SetProperty(ref this.geoInfo, value); }
        }

        private Shop selectedShop;

        public Shop SelectedShop
        {
            get { return this.selectedShop; }
            private set { this.SetProperty(ref this.selectedShop, value); }
        }

        public ObservableCollection<Shop> Shops { get; } = new ObservableCollection<Shop>();

        public HotpepperApp(IGeoProvider geoProvider, IHotpepperClient hotpepperClient)
        {
            this.GeoProvider = geoProvider;
            this.HotpepperClient = hotpepperClient;
        }

        public async Task SetSelectedShopByIdAsync(string id)
        {
            if (id == this.SelectedShop?.id)
            {
                return;
            }

            if (!this.Shops.Any())
            {
                await this.LoadShopsAsync();
            }

            this.SelectedShop = this.Shops.FirstOrDefault(x => x.id == id);
        }

        public async Task LoadGeoInfoAsync()
        {
            this.GeoInfo = await this.GeoProvider.GetGeoInfoAsync();
        }

        public async Task LoadShopsAsync()
        {
            var results = await this.HotpepperClient.SearchAsync(this.GeoInfo.Lat, this.GeoInfo.Lng);
            this.Shops.Clear();
            foreach (var s in results.results.shop)
            {
                this.Shops.Add(s);
            }
        }

    }
}
