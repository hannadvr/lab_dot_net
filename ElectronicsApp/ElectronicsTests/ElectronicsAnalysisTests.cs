using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElectronicsApp.Models;

namespace ElectronicsTests
{
    [TestClass]
    public class ElectronicsAnalysisTests
    {
        [TestMethod]
        public void GetTotalDepreciation_WithItems_ReturnsSum()
        {
            int currentYear = DateTime.Now.Year;
            var list = new List<Electronics>
            {
                new Phone("Apple", 10000, currentYear - 2, 8),   // 4000
                new Laptop("Asus", 20000, currentYear - 2, 15.6) // 6000
            };

            double total = ElectronicsAnalysis.GetTotalDepreciation(list);

            Assert.AreEqual(10000, total);
        }

        [TestMethod]
        public void GetTotalDepreciation_WithEmptyList_ReturnsZero()
        {
            var emptyList = new List<Electronics>();
            double total = ElectronicsAnalysis.GetTotalDepreciation(emptyList);

            Assert.AreEqual(0, total);
        }

        [TestMethod]
        public void PolymorphicInterfaceCollection_Demonstration()
        {
            var networkDevices = new List<IConnectable>
            {
                new Phone("Apple", 20000, 2024, 8),
                new Tablet("iPad", 25000, 2024, true)
                };

            foreach (var device in networkDevices)
            {
                string result = device.ConnectToWifi("Main_Network");
                Assert.IsTrue(device.IsConnected);
                StringAssert.Contains(result, "Main_Network");
            }
        }
    }
}