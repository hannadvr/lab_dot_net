using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElectronicsApp.Models;

namespace ElectronicsTests
{
    [TestClass]
    public class LaptopTests
    {
        // Всі дані введено правильно
        [TestMethod]
        public void Constructor_WithValidData_InitializesCorrectly()
        {
            var laptop = new Laptop("Asus", 30000, 2023, 15.6);

            Assert.AreEqual("Asus", laptop.Brand);
            Assert.AreEqual(30000, laptop.Price);
            Assert.AreEqual(2023, laptop.Year);
            Assert.AreEqual(15.6, laptop.ScreenSize);
        }

        // Неправильна ціна
        [TestMethod]
        public void Constructor_WithNegativePrice_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new Laptop("Asus", -100, 2023, 15.6));
        }

        // Результат тесту продуктивності
        [TestMethod]
        public void GetBenchmarkScore_ReturnsCalculatedScore()
        {
            var laptop = new Laptop("Asus", 20000, 2024, 15.6);
            int expected = (int)(20000 / 10) + (2024 * 2);

            Assert.AreEqual(expected, laptop.GetBenchmarkScore());
        }

        // Розрахунок амортизації
        [TestMethod]
        public void CalculateDepreciation_ReturnsCorrectValue()
        {
            int currentYear = DateTime.Now.Year;
            var laptop = new Laptop("Asus", 20000, currentYear - 2, 15.6);
            double expected = 20000 * 0.15 * 2; // 6000

            Assert.AreEqual(expected, laptop.CalculateDepreciation());
        }

        // Некоректний розмір екрану
        [TestMethod]
        public void Constructor_WithZeroOrNegativeScreenSize_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new Laptop("Asus", 30000, 2024, 0));
        }

        // Перевірка формування рядка опису
        [TestMethod]
        public void GetDescription_ReturnsCorrectDescriptionString()
        {
            var laptop = new Laptop("Asus", 30000, 2024, 15.6);
            string description = laptop.GetDescription();

            StringAssert.Contains(description, "Ноутбук Asus");
            StringAssert.Contains(description, "Рік: 2024");
            StringAssert.Contains(description, "Екран: 15,6\"");
        }
    }
}