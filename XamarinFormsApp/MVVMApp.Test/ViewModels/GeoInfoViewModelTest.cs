using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVVMApp.Models;
using Moq;
using MVVMApp.ViewModels;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MVVMApp.Models.Raw;

namespace MVVMApp.Test.ViewModels
{
    [TestClass]
    public class GeoInfoViewModelTest
    {
        [TestMethod]
        public void LoadGeoInfoCommandInvokeLoadGeoInfoAsync()
        {
            var hotpepperApp = new Mock<IHotpepperApp>();
            var task = Task.CompletedTask;
            hotpepperApp.Setup(x => x.LoadGeoInfoAsync())
                .Returns(task)
                .Verifiable();
            hotpepperApp.Setup(x => x.Shops)
                .Returns(new ObservableCollection<Shop>());

            var target = new GeoInfoViewModel(hotpepperApp.Object);
            target.OnNavigatedTo(null);
            target.LoadGeoInfoCommand.Execute();
            hotpepperApp.Verify();
        }

        [TestMethod]
        public void LoadShopsCommandInvokeLoadShopsAsync()
        {
            var hotpepperApp = new Mock<IHotpepperApp>();
            var task = Task.CompletedTask;
            hotpepperApp.Setup(x => x.LoadShopsAsync())
                .Returns(task)
                .Verifiable();
            hotpepperApp.Setup(x => x.Shops)
                .Returns(new ObservableCollection<Shop>());

            var target = new GeoInfoViewModel(hotpepperApp.Object);
            target.OnNavigatedTo(null);
            target.LoadShopsCommand.Execute();
            hotpepperApp.Verify();
        }
    }
}
