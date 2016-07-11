using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVVMApp.Models;
using MVVMApp.Models.Raw;
using Prism.Mvvm;
using MVVMApp.ViewModels;

namespace MVVMApp.Test.ViewModels
{
    [TestClass]
    public class DetailViewModelTest
    {
        private HotpepperAppMock HotpepperAppMock { get; set; }

        private DetailViewModel Target { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            this.HotpepperAppMock = new HotpepperAppMock();
            this.Target = new DetailViewModel(this.HotpepperAppMock);
        }

        [TestCleanup]
        public void Cleanup()
        {
            this.HotpepperAppMock = null;
            this.Target = null;
        }

        [TestMethod]
        public void InitialStateCheck()
        {
            Assert.IsNull(this.Target.Name);
            Assert.IsNull(this.Target.Kana);
            Assert.IsNull(this.Target.Image);
        }

        [TestMethod]
        public void InitialStateCheckWhenCalledOnNavigatedTo()
        {
            this.Target.OnNavigatedTo(null);
            Assert.IsNull(this.Target.Name.Value);
            Assert.IsNull(this.Target.Kana.Value);
            Assert.IsNull(this.Target.Image.Value);
        }

        [TestMethod]
        public void PropertyValueChangedWhenSelectedValueChangeed()
        {
            this.Target.OnNavigatedTo(null);
            this.HotpepperAppMock.SelectedShop = new Shop
            {
                name = "name",
                name_kana = "kana",
                photo = new Photo
                {
                    pc = new Pc { l = "l" }
                }
            };
            Assert.AreEqual("name", this.Target.Name.Value);
            Assert.AreEqual("kana", this.Target.Kana.Value);
            Assert.AreEqual("l", this.Target.Image.Value);
        }
    }

    class HotpepperAppMock : BindableBase, IHotpepperApp
    {
        public GeoInfo GeoInfo => null;

        private Shop selectedShop;

        public Shop SelectedShop
        {
            get { return this.selectedShop; }
            set { this.SetProperty(ref this.selectedShop, value); }
        }


        public ObservableCollection<Shop> Shops => null;

        public Task LoadGeoInfoAsync() => Task.CompletedTask;

        public Task LoadShopsAsync() => Task.CompletedTask;
    }
}
