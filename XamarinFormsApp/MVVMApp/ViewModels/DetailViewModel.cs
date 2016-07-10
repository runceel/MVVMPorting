using MVVMApp.Models;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMApp.ViewModels
{
    public class DetailViewModel : BindableBase
    {
        private HotpepperApp Model { get; }

        public ReadOnlyReactiveProperty<string> Name { get; }

        public ReadOnlyReactiveProperty<string> Kana { get; }

        public ReadOnlyReactiveProperty<string> Image { get; }

        public DetailViewModel(HotpepperApp model)
        {
            this.Model = model;
            this.Name = this.Model
                .ObserveProperty(x => x.SelectedShop)
                .Select(x => x?.name)
                .ToReadOnlyReactiveProperty();

            this.Kana = this.Model
                .ObserveProperty(x => x.SelectedShop)
                .Select(x => x?.name_kana)
                .ToReadOnlyReactiveProperty();

            this.Image = this.Model
                .ObserveProperty(x => x.SelectedShop)
                .Select(x => x?.photo?.pc?.l)
                .ToReadOnlyReactiveProperty();
        }
    }
}
