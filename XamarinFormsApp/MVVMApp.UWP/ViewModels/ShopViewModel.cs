using MVVMApp.Models.Raw;

namespace MVVMApp.UWP.ViewModels
{
    public class ShopViewModel
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
