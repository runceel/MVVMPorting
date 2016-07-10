using MVVMApp.Models.Raw;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMApp.Models
{
    public interface IHotpepperApp : INotifyPropertyChanged
    {
        GeoInfo GeoInfo { get; }

        Shop SelectedShop { get; set; }

        ObservableCollection<Shop> Shops { get; }

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
            set { this.SetProperty(ref this.selectedShop, value); }
        }

        public ObservableCollection<Shop> Shops { get; } = new ObservableCollection<Shop>();

        public HotpepperApp(IGeoProvider geoProvider, IHotpepperClient hotpepperClient)
        {
            this.GeoProvider = geoProvider;
            this.HotpepperClient = hotpepperClient;
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
