using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankDepositorsApp;

namespace BankDepositorsTests
{
    public class FakeFileService : IFileSaver, IFileLoader
    {
        public Task SaveAsync<T>(string filePath, List<T> data) => Task.CompletedTask;
        public Task<List<T>> LoadAsync<T>(string filePath) => Task.FromResult(new List<T>());
    }

    [TestClass]
    public class BankManagerTests
    {
        private BankManager _manager;
        private const string Iban1 = "UA111111111111111111111111111";
        private const string Iban2 = "UA222222222222222222222222222";
        private const string Iban3 = "UA333333333333333333333333333";

        [TestInitialize]
        public void Setup()
        {
            var fakeService = new FakeFileService();
            _manager = new BankManager(fakeService, fakeService, "test.json");
        }

        [TestMethod]
        public void AddDepositor_IncreasesListCount()
        {
            var dep = new BankDepositor("Іваненко І.І.", Iban1, 1000m, 2024);
            _manager.AddDepositor(dep);

            Assert.AreEqual(1, _manager.Depositors.Count);
            Assert.AreEqual(dep, _manager.Depositors[0]);
        }

        [TestMethod]
        public void DeleteDepositor_RemovesItemFromList()
        {
            var dep = new BankDepositor("Іваненко І.І.", Iban1, 1000m, 2024);
            _manager.AddDepositor(dep);
            _manager.DeleteDepositor(dep);

            Assert.AreEqual(0, _manager.Depositors.Count);
        }

        [TestMethod]
        public void SortByAmount_SortsDepositorsAscending()
        {
            var d1 = new BankDepositor("A", Iban1, 12000m, 2023);
            var d2 = new BankDepositor("B", Iban2, 3000m, 2023);
            _manager.AddDepositor(d1);
            _manager.AddDepositor(d2);

            _manager.SortByAmount();

            Assert.AreEqual(3000m, _manager.Depositors[0].Amount);
            Assert.AreEqual(12000m, _manager.Depositors[1].Amount);
        }

        [TestMethod]
        public void FilterByCurrentYear_FiltersOnlyCurrentYearDepositors()
        {
            int currentYear = DateTime.Now.Year;
            var past = new BankDepositor("Старий", Iban1, 1000m, currentYear - 1);
            var cur = new BankDepositor("Новий", Iban2, 2000m, currentYear);

            _manager.AddDepositor(past);
            _manager.AddDepositor(cur);

            _manager.FilterByCurrentYear();

            Assert.AreEqual(1, _manager.Depositors.Count);
            Assert.AreEqual("Новий", _manager.Depositors[0].FullName);
        }

        [TestMethod]
        public void FilterByCurrentYear_ThenSortByAmount_MatchesVariant5()
        {
            int currentYear = DateTime.Now.Year;
            var past = new BankDepositor("Минулий", Iban1, 50000m, currentYear - 1);
            var curHigh = new BankDepositor("Поточний дорогий", Iban2, 20000m, currentYear);
            var curLow = new BankDepositor("Поточний дешевий", Iban3, 5000m, currentYear);

            _manager.AddDepositor(past);
            _manager.AddDepositor(curHigh);
            _manager.AddDepositor(curLow);

            // Фільтрація за поточним роком
            _manager.FilterByCurrentYear();
            Assert.AreEqual(2, _manager.Depositors.Count);

            // Сортування за сумою
            _manager.SortByAmount();

            Assert.AreEqual(5000m, _manager.Depositors[0].Amount);
            Assert.AreEqual(20000m, _manager.Depositors[1].Amount);
        }

        [TestMethod]
        public void ResetFilter_RestoresAllRecords()
        {
            int currentYear = DateTime.Now.Year;
            _manager.AddDepositor(new BankDepositor("A", Iban1, 1000m, currentYear - 1));
            _manager.AddDepositor(new BankDepositor("B", Iban2, 2000m, currentYear));

            _manager.FilterByCurrentYear();
            Assert.AreEqual(1, _manager.Depositors.Count);

            _manager.ResetFilter();
            Assert.AreEqual(2, _manager.Depositors.Count);
        }

        [TestMethod]
        public void AddDepositor_DuplicateAccountNumber_ThrowsInvalidOperationException()
        {
            var dep1 = new BankDepositor("Іванов І.І.", Iban1, 1000m, 2024);
            var dep2 = new BankDepositor("Петров П.П.", Iban1, 5000m, 2024); // той самий Iban1

            _manager.AddDepositor(dep1);

            Assert.Throws<InvalidOperationException>(() => _manager.AddDepositor(dep2));
        }
    }
}