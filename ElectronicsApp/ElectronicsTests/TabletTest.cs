using System;
using ElectronicsApp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ElectronicsTests
{
    [TestClass]
    public class TabletTests
    {
        // Всі дані введено правильно
        [TestMethod]
        public void Constructor_WithValidData_InitializesCorrectly()
        {
            var tablet = new Tablet("iPad Air", 25000, 2024, true);

            Assert.AreEqual("iPad Air", tablet.Brand);
            Assert.AreEqual(25000, tablet.Price);
            Assert.AreEqual(2024, tablet.Year);
            Assert.IsTrue(tablet.HasStylus);
            Assert.IsFalse(tablet.IsConnected);
        }

        // Перевірка роботи через інтерфейс IPerformanceEvaluated
        [TestMethod]
        public void GetBenchmarkScore_ViaInterface_ReturnsCorrectScore()
        {
            IPerformanceEvaluated tabletWithStylus = new Tablet("iPad Pro", 36000, 2024, true);
            int expected = (int)(36000 / 12) + (2024 * 2) + 200;

            Assert.AreEqual(expected, tabletWithStylus.GetBenchmarkScore());
        }

        // Перевірка роботи через інтерфейс IConnectable
        [TestMethod]
        public void ConnectToWifi_ViaInterface_SetsIsConnected()
        {
            IConnectable tablet = new Tablet("Xiaomi Pad", 14000, 2023, false);
            string msg = tablet.ConnectToWifi("Campus_Wi-Fi");

            Assert.IsTrue(tablet.IsConnected);
            StringAssert.Contains(msg, "Campus_Wi-Fi");
        }

        // Перевірка розрахунку амортизації
        [TestMethod]
        public void CalculateDepreciation_ReturnsCorrectValue()
        {
            int currentYear = DateTime.Now.Year;
            var tablet = new Tablet("Lenovo Tab", 10000, currentYear - 2, false);
            double expected = 10000 * 0.18 * 2; // 3600

            Assert.AreEqual(expected, tablet.CalculateDepreciation());
        }

        // Перевірка формування опису
        [TestMethod]
        public void GetDescription_ReturnsCorrectDescriptionString()
        {
            var tablet = new Tablet("iPad Air", 25000, 2024, true);
            string description = tablet.GetDescription();

            StringAssert.Contains(description, "Планшет iPad Air");
            StringAssert.Contains(description, "Бенчмарк:");
            StringAssert.Contains(description, "Стилус: Так");
        }
    }
}