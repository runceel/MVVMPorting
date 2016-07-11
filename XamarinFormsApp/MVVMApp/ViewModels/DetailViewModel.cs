using MVVMApp.Models;
using Prism.Mvvm;
using Prism.Regions;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Linq;
using System.Reactive.Linq;
using System;
using System.Reactive.Disposables;

namespace MVVMApp.ViewModels
{
    public class DetailViewModel : BindableBase, INavigationAware
    {
        private CompositeDisposable Disposable { get; set; }

        private IHotpepperApp Model { get; }

        private ReadOnlyReactiveProperty<string> name;

        public ReadOnlyReactiveProperty<string> Name
        {
            get { return this.name; }
            private set { this.SetProperty(ref this.name, value); }
        }

        private ReadOnlyReactiveProperty<string> kana;

        public ReadOnlyReactiveProperty<string> Kana
        {
            get { return this.kana; }
            private set { this.SetProperty(ref this.kana, value); }
        }

        private ReadOnlyReactiveProperty<string> image;

        public ReadOnlyReactiveProperty<string> Image
        {
            get { return this.image; }
            private set { this.SetProperty(ref this.image, value); }
        }

        public DetailViewModel(IHotpepperApp model)
        {
            this.Model = model;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            this.Disposable = new CompositeDisposable();

            this.Name = this.Model
                .ObserveProperty(x => x.SelectedShop)
                .Select(x => x?.name)
                .ToReadOnlyReactiveProperty()
                .AddTo(this.Disposable);

            this.Kana = this.Model
                .ObserveProperty(x => x.SelectedShop)
                .Select(x => x?.name_kana)
                .ToReadOnlyReactiveProperty()
                .AddTo(this.Disposable);

            this.Image = this.Model
                .ObserveProperty(x => x.SelectedShop)
                .Select(x => x?.photo?.pc?.l)
                .ToReadOnlyReactiveProperty()
                .AddTo(this.Disposable);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext) => this.Disposable.Dispose();
    }
}
