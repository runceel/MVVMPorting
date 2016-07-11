using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVVMApp.Models;
using Moq;
using System.Threading.Tasks;
using MVVMApp.Models.Raw;

namespace MVVMApp.Test.Models
{
    [TestClass]
    public class HotPepperAppTest
    {
        [TestMethod]
        public async Task LoadGeoInfoAsync()
        {
            var geoProvider = new Mock<IGeoProvider>();
            geoProvider
                .Setup(x => x.GetGeoInfoAsync())
                .Returns(() => Task.FromResult(new GeoInfo { Lat = 10.0, Lng = 15.0 }))
                .Verifiable();

            var target = new HotpepperApp(geoProvider.Object, null);
            Assert.IsNull(target.GeoInfo);

            string propertyName = null;
            target.PropertyChanged += (_, e) => propertyName = e.PropertyName;
            await target.LoadGeoInfoAsync();
            geoProvider.Verify();
            Assert.AreEqual(nameof(IHotpepperApp.GeoInfo), propertyName);
            Assert.AreEqual(10.0, target.GeoInfo.Lat);
            Assert.AreEqual(15.0, target.GeoInfo.Lng);
        }

        [TestMethod]
        public async Task LoadShopsAsync()
        {
            var geoProvider = new Mock<IGeoProvider>();
            geoProvider
                .Setup(x => x.GetGeoInfoAsync())
                .Returns(() => Task.FromResult(new GeoInfo { Lat = 10.0, Lng = 15.0 }))
                .Verifiable();

            var hotpepperClient = new Mock<IHotpepperClient>();
            var searchAsyncResult = Task.FromResult(new Rootobject
            {
                results = new Results
                {
                    shop = new[]
                    {
                        new Shop { name = "shop1" },
                        new Shop { name = "shop2" },
                        new Shop { name = "shop3" },
                    }
                }
            });
            hotpepperClient.Setup(x => x.SearchAsync(10.0, 15.0)).Returns(() => searchAsyncResult)
                .Verifiable();

            var target = new HotpepperApp(geoProvider.Object, hotpepperClient.Object);
            await target.LoadGeoInfoAsync();
            await target.LoadShopsAsync();

            Assert.AreEqual(3, target.Shops.Count);
            Assert.AreEqual("shop1", target.Shops[0].name);
            Assert.AreEqual("shop2", target.Shops[1].name);
            Assert.AreEqual("shop3", target.Shops[2].name);

            await target.LoadShopsAsync();
            Assert.AreEqual(3, target.Shops.Count);
        }
    }
}
