using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankDepositorsApp;

namespace BankDepositorsTests
{
    [TestClass]
    public sealed class BankDepositorTests
    {
        private const string ValidIban1 = "UA111111111111111111111111111";
        private const string ValidIban2 = "UA222222222222222222222222222";

        [TestMethod]
        public void Constructor_WithValidArguments_CreatesDepositorObject()
        {
            string name = "Шевченко Тарас Григорович";
            decimal amount = 25000.50m;
            int year = 2024;

            var depositor = new BankDepositor(name, ValidIban1, amount, year);

            Assert.AreEqual(name, depositor.FullName);
            Assert.AreEqual(ValidIban1, depositor.AccountNumber);
            Assert.AreEqual(amount, depositor.Amount);
            Assert.AreEqual(year, depositor.OpeningYear);
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        public void Constructor_WithInvalidFullName_ThrowsArgumentException(string invalidName)
        {
            Assert.Throws<ArgumentException>(() => new BankDepositor(invalidName, ValidIban1, 1000m, 2023));
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        [DataRow("12345")]
        [DataRow("UA123")]
        [DataRow("PL111111111111111111111111111")]
        [DataRow("UA11111111111111111111111111X")]
        public void Constructor_WithInvalidAccountNumber_ThrowsArgumentException(string invalidAcc)
        {
            Assert.Throws<ArgumentException>(() => new BankDepositor("Іванов І.І.", invalidAcc, 1000m, 2023));
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-50)]
        public void Constructor_WithZeroOrNegativeAmount_ThrowsArgumentException(double invalidAmount)
        {
            Assert.Throws<ArgumentException>(() => new BankDepositor("Іванов І.І.", ValidIban1, (decimal)invalidAmount, 2023));
        }

        [TestMethod]
        public void Constructor_WithFutureYear_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new BankDepositor("Іванов І.І.", ValidIban1, 1000m, DateTime.Now.Year + 1));
        }

        [TestMethod]
        public void Constructor_WithEarlyYear_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new BankDepositor("Іванов І.І.", ValidIban1, 1000m, 1899));
        }

        [TestMethod]
        public void CompareTo_WithOtherDepositor_ReturnsCorrectValue()
        {
            var d1 = new BankDepositor("А", ValidIban1, 100m, 2020);
            var d2 = new BankDepositor("Б", ValidIban2, 200m, 2021);
            var d3 = new BankDepositor("В", ValidIban1, 300m, 2022);

            Assert.IsTrue(d1.CompareTo(d2) < 0);
            Assert.IsTrue(d2.CompareTo(d1) > 0);
            Assert.AreEqual(0, d1.CompareTo(d3));
        }

        [TestMethod]
        public void CompareTo_WithNull_ReturnsPositiveValue()
        {
            var d = new BankDepositor("А", ValidIban1, 100m, 2020);
            Assert.IsTrue(d.CompareTo(null) > 0);
        }
    }
}