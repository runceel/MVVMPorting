using MVVMApp.Models;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace MVVMApp.UWP.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private CompositeDisposable Disposable { get; set; }

        private IHotpepperApp HotpepperApp { get; }

        private INavigationService NavigationService { get; }

        private ReadOnlyReactiveProperty<double> lat;

        public ReadOnlyReactiveProperty<double> Lat
        {
            get { return this.lat; }
            set { this.SetProperty(ref this.lat, value); }
        }

        private ReadOnlyReactiveProperty<double> lng;

        public ReadOnlyReactiveProperty<double> Lng
        {
            get { return this.lng; }
            set { this.SetProperty(ref this.lng, value); }
        }

        private ReadOnlyReactiveCollection<ShopViewModel> shops;

        public ReadOnlyReactiveCollection<ShopViewModel> Shops
        {
            get { return this.shops; }
            set { this.SetProperty(ref this.shops, value); }
        }

        private ReactiveProperty<ShopViewModel> selectedShop;

        public ReactiveProperty<ShopViewModel> SelectedShop
        {
            get { return this.selectedShop; }
            set { this.SetProperty(ref this.selectedShop, value); }
        }


        private ReactiveCommand loadShopsCommand;

        public ReactiveCommand LoadShopsCommand
        {
            get { return this.loadShopsCommand; }
            set { this.SetProperty(ref this.loadShopsCommand, value); }
        }

        private ReactiveCommand loadGeoInfoCommand;

        public ReactiveCommand LoadGeoInfoCommand
        {
            get { return this.loadGeoInfoCommand; }
            set { this.SetProperty(ref this.loadGeoInfoCommand, value); }
        }

        private ReactiveCommand<ShopViewModel> selectItemCommand;

        public ReactiveCommand<ShopViewModel> SelectItemCommand
        {
            get { return this.selectItemCommand; }
            set { this.SetProperty(ref this.selectItemCommand, value); }
        }

        public MainPageViewModel(IHotpepperApp app, INavigationService navigationService)
        {
            this.HotpepperApp = app;
            this.NavigationService = navigationService;
        }

        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);
            this.Disposable = new CompositeDisposable();
            this.Lat = this.HotpepperApp
                .ObserveProperty(x => x.GeoInfo)
                .Where(x => x != null)
                .Select(x => x.Lat)
                .ToReadOnlyReactiveProperty()
                .AddTo(this.Disposable);

            this.Lng = this.HotpepperApp
                .ObserveProperty(x => x.GeoInfo)
                .Where(x => x != null)
                .Select(x => x.Lng)
                .ToReadOnlyReactiveProperty()
                .AddTo(this.Disposable);

            this.Shops = this.HotpepperApp
                .Shops
                .ToReadOnlyReactiveCollection(x => new ShopViewModel(x))
                .AddTo(this.Disposable);

            this.LoadGeoInfoCommand = new ReactiveCommand();
            this.LoadGeoInfoCommand.Subscribe(async _ => await this.HotpepperApp.LoadGeoInfoAsync())
                .AddTo(this.Disposable);

            this.LoadShopsCommand = new ReactiveCommand();
            this.LoadShopsCommand.Subscribe(async _ => await this.HotpepperApp.LoadShopsAsync())
                .AddTo(this.Disposable);

            this.SelectItemCommand = new ReactiveCommand<ShopViewModel>();
            this.SelectItemCommand.Subscribe(x => this.NavigationService.Navigate("Detail", x.Model.id));
        }

        public override void OnNavigatingFrom(NavigatingFromEventArgs e, Dictionary<string, object> viewModelState, bool suspending)
        {
            base.OnNavigatingFrom(e, viewModelState, suspending);
            if (!suspending)
            {
                this.Disposable.Dispose();
            }
        }
    }
}
