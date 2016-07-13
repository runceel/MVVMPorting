using MVVMApp.Models;
using Prism.Windows.Mvvm;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Windows.Navigation;
using System.Reactive.Disposables;
using Reactive.Bindings.Extensions;
using System.Reactive.Linq;

namespace MVVMApp.UWP.ViewModels
{
    public class DetailPageViewModel : ViewModelBase
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

        public DetailPageViewModel(IHotpepperApp model)
        {
            this.Model = model;
        }

        public override async void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);
            await this.Model.SetSelectedShopByIdAsync(e.Parameter as string);
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
