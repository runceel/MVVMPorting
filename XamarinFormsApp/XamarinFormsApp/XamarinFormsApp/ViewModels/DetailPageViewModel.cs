using MVVMApp.Models;
using Prism.Mvvm;
using Prism.Navigation;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using XamarinFormsApp.Commons;

namespace XamarinFormsApp.ViewModels
{
    public class DetailPageViewModel : BindableBase, INavigationEventAware
    {
        private CompositeDisposable Disposable { get; set; }

        private HotpepperApp Model { get; }

        private ReadOnlyReactiveProperty<string> name;

        public ReadOnlyReactiveProperty<string> Name
        {
            get { return this.name; }
            set { this.SetProperty(ref this.name, value); }
        }

        private ReadOnlyReactiveProperty<string> kana;

        public ReadOnlyReactiveProperty<string> Kana
        {
            get { return this.kana; }
            set { this.SetProperty(ref this.kana, value); }
        }

        private ReadOnlyReactiveProperty<string> image;

        public ReadOnlyReactiveProperty<string> Image
        {
            get { return this.image; }
            set { this.SetProperty(ref this.image, value); }
        }

        public DetailPageViewModel(HotpepperApp model)
        {
            this.Model = model;
        }

        public void Disappearing()
        {
            this.Disposable.Dispose();
            this.Disposable = null;
        }

        public void Appearing()
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
                .Select(x => x?.photo?.mobile?.l)
                .ToReadOnlyReactiveProperty()
                .AddTo(this.Disposable);
        }
    }
}
