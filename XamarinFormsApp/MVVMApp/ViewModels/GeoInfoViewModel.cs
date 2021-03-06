﻿using MVVMApp.Models;
using Prism.Mvvm;
using Prism.Regions;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace MVVMApp.ViewModels
{
    public class GeoInfoViewModel : BindableBase, INavigationAware
    {
        private CompositeDisposable Disposable { get; set; }

        private IHotpepperApp HotpepperApp { get; }

        private ReadOnlyReactiveProperty<double> lat;

        public ReadOnlyReactiveProperty<double> Lat
        {
            get { return this.lat; }
            private set { this.SetProperty(ref this.lat, value); }
        }

        private ReadOnlyReactiveProperty<double> lng;

        public ReadOnlyReactiveProperty<double> Lng
        {
            get { return this.lng; }
            private set { this.SetProperty(ref this.lng, value); }
        }

        private ReadOnlyReactiveCollection<ShopViewModel> shops;

        public ReadOnlyReactiveCollection<ShopViewModel> Shops
        {
            get { return this.shops; }
            private set { this.SetProperty(ref this.shops, value); }
        }

        private ReactiveProperty<ShopViewModel> selectedShop;

        public ReactiveProperty<ShopViewModel> SelectedShop
        {
            get { return this.selectedShop; }
            private set { this.SetProperty(ref this.selectedShop, value); }
        }

        private ReactiveCommand loadShopsCommand;

        public ReactiveCommand LoadShopsCommand
        {
            get { return this.loadShopsCommand; }
            private set { this.SetProperty(ref this.loadShopsCommand, value); }
        }

        private ReactiveCommand loadGeoInfoCommand;

        public ReactiveCommand LoadGeoInfoCommand
        {
            get { return this.loadGeoInfoCommand; }
            private set { this.SetProperty(ref this.loadGeoInfoCommand, value); }
        }

        public GeoInfoViewModel(IHotpepperApp app)
        {
            this.HotpepperApp = app;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
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

            this.SelectedShop = new ReactiveProperty<ShopViewModel>();
            this.SelectedShop
                .Subscribe(async x =>
                {
                    await this.HotpepperApp.SetSelectedShopByIdAsync(x?.Model?.id);
                })
                .AddTo(this.Disposable);

            this.LoadGeoInfoCommand = new ReactiveCommand();
            this.LoadGeoInfoCommand.Subscribe(async _ => await this.HotpepperApp.LoadGeoInfoAsync())
                .AddTo(this.Disposable);

            this.LoadShopsCommand = new ReactiveCommand();
            this.LoadShopsCommand.Subscribe(async _ => await this.HotpepperApp.LoadShopsAsync())
                .AddTo(this.Disposable);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext) => this.Disposable.Dispose();
    }
}
