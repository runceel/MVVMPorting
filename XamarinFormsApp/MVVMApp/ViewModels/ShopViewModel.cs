using MVVMApp.Models.Raw;
using Prism.Mvvm;

namespace MVVMApp.ViewModels
{
    public class ShopViewModel : BindableBase
    {
        public Shop Model { get; }

        public string Name => this.Model.name;

        public string NameKana => this.Model.name_kana;

        public ShopViewModel(Shop model)
        {
            this.Model = model;
        }
    }
}
