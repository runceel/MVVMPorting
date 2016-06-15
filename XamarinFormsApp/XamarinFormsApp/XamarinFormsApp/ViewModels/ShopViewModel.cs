using MVVMApp.Models.Raw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamarinFormsApp.ViewModels
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
