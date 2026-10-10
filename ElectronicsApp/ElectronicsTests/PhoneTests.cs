using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElectronicsApp.Models;

namespace ElectronicsTests
{
    [TestClass]
    public class PhoneTests
    {
        // Всі дані введено правильно
        [TestMethod]
        public void Constructor_WithValidData_InitializesCorrectly()
        {
            var phone = new Phone("Apple", 12000, 2024, 8);

            Assert.AreEqual("Apple", phone.Brand);
            Assert.AreEqual(12000, phone.Price);
            Assert.AreEqual(2024, phone.Year);
            Assert.AreEqual(8, phone.RamMemory);
        }

        // Неправильна ціна
        [TestMethod]
        public void Constructor_WithZeroPrice_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new Phone("Apple", 0, 2024, 8));
        }

        // Обрахунок тривалості роботи батареї
        [TestMethod]
        public void CalculateBatteryLife_ReturnsCorrectHours()
        {
            var phone = new Phone("Apple", 10000, 2024, 8);
            double expected = 10.0 + (8 * 0.5);

            Assert.AreEqual(expected, phone.CalculateBatteryLife());
        }

        // Обрахунок амортизації
        [TestMethod]
        public void CalculateDepreciation_ReturnsCorrectValue()
        {
            int currentYear = DateTime.Now.Year;
            var phone = new Phone("Apple", 10000, currentYear - 2, 8);
            double expected = 10000 * 0.20 * 2; // 4000

            Assert.AreEqual(expected, phone.CalculateDepreciation());
        }

        // Не вказано бренд
        [TestMethod]
        public void Constructor_WithEmptyBrand_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new Phone("", 10000, 2024, 8));
        }

        // Некоректний рік
        [TestMethod]
        public void Constructor_WithInvalidYear_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new Phone("Apple", 10000, 1980, 8));
        }

        // Некоректний об'єм пам'яті
        [TestMethod]
        public void Constructor_WithZeroOrNegativeRamMemory_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new Phone("Apple", 10000, 2024, 0));
        }

        // Перевірка формування рядка опису
        [TestMethod]
        public void GetDescription_ReturnsCorrectDescriptionString()
        {
            var phone = new Phone("Apple", 12000, 2024, 8);
            string description = phone.GetDescription();

            StringAssert.Contains(description, "Телефон Apple");
            StringAssert.Contains(description, "Рік: 2024");
            StringAssert.Contains(description, "ОЗП: 8 ГБ");
        }
    }
}