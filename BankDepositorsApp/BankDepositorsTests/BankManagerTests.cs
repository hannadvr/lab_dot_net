using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankDepositorsApp;

namespace BankDepositorsTests
{
    // Тестова заглушка
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

        // Для незалежності тестів
        [TestInitialize]
        public void Setup()
        {
            var fakeService = new FakeFileService();
            _manager = new BankManager(fakeService, fakeService, "test.json");
        }

        // Додавання вкладника
        [TestMethod]
        public void AddDepositor_IncreasesListCount()
        {
            var dep = new BankDepositor("Іваненко І.І.", Iban1, 1000m, 2024);
            _manager.AddDepositor(dep);

            Assert.AreEqual(1, _manager.Depositors.Count);
            Assert.AreEqual(dep, _manager.Depositors[0]);
        }

        // Видалення вкладника
        [TestMethod]
        public void DeleteDepositor_RemovesItemFromList()
        {
            var dep = new BankDepositor("Іваненко І.І.", Iban1, 1000m, 2024);
            _manager.AddDepositor(dep);
            _manager.DeleteDepositor(dep);

            Assert.AreEqual(0, _manager.Depositors.Count);
        }

        // Сортування за сумою вкладення
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

        // Фільтрація за поточним роком
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

        // Фільтр і сортування
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

        // Скидання фільтра
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

        // Перевірка при однакових номерах рахунків
        [TestMethod]
        public void AddDepositor_DuplicateAccountNumber_ThrowsInvalidOperationException()
        {
            var dep1 = new BankDepositor("Іванов І.І.", Iban1, 1000m, 2024);
            var dep2 = new BankDepositor("Петров П.П.", Iban1, 5000m, 2024); // той самий Iban1

            _manager.AddDepositor(dep1);

            Assert.Throws<InvalidOperationException>(() => _manager.AddDepositor(dep2));
        }

        // Перевірка рядка пошуку коли даних немає
        [TestMethod]
        public void SearchDepositors_WithNullOrWhiteSpace_RestoresAllDepositors()
        {
            var d1 = new BankDepositor("Іваненко І.І.", Iban1, 1000m, 2024);
            var d2 = new BankDepositor("Петренко П.П.", Iban2, 2000m, 2023);
            _manager.AddDepositor(d1);
            _manager.AddDepositor(d2);

            _manager.SearchDepositors(null);
            Assert.AreEqual(2, _manager.Depositors.Count);

            _manager.SearchDepositors("   ");
            Assert.AreEqual(2, _manager.Depositors.Count);
        }

        // Пошук за введеним фрагментом
        [TestMethod]
        public void SearchDepositors_WithFragment_ReturnsMatchingDepositors()
        {
            var d1 = new BankDepositor("Тарас Шевченко", Iban1, 1000m, 2024);
            var d2 = new BankDepositor("Леся Українка", Iban2, 2000m, 2023);
            _manager.AddDepositor(d1);
            _manager.AddDepositor(d2);

            _manager.SearchDepositors("шевч");
            Assert.AreEqual(1, _manager.Depositors.Count);
            Assert.AreEqual(d1.FullName, _manager.Depositors[0].FullName);

            _manager.SearchDepositors("22222");
            Assert.AreEqual(1, _manager.Depositors.Count);
            Assert.AreEqual(d2.AccountNumber, _manager.Depositors[0].AccountNumber);
        }

        // Унікальні імена
        [TestMethod]
        public void ShowUniqueDepositors_ReturnsUniqueNamesAlphabetically()
        {
            var d1 = new BankDepositor("Шевченко Т.", Iban1, 1000m, 2024);
            var d2 = new BankDepositor("Франко І.", Iban2, 2000m, 2023);
            var d3 = new BankDepositor("Франко І.", Iban3, 3000m, 2022); // дублікат ПІБ
            _manager.AddDepositor(d1);
            _manager.AddDepositor(d2);
            _manager.AddDepositor(d3);

            _manager.ShowUniqueDepositors();

            Assert.AreEqual(2, _manager.Depositors.Count);
            Assert.AreEqual("Франко І.", _manager.Depositors[0].FullName);
            Assert.AreEqual("Шевченко Т.", _manager.Depositors[1].FullName);
        }

        // Топ три вкладники
        [TestMethod]
        public void FindTopThreeDeposits_ReturnsThreeHighestDeposits()
        {
            _manager.AddDepositor(new BankDepositor("A", Iban1, 1000m, 2021));
            _manager.AddDepositor(new BankDepositor("B", Iban2, 5000m, 2022));
            _manager.AddDepositor(new BankDepositor("C", Iban3, 2000m, 2023));
            _manager.AddDepositor(new BankDepositor("D", "UA444444444444444444444444444", 8000m, 2024));

            _manager.FindTopThreeDeposits();

            Assert.AreEqual(3, _manager.Depositors.Count);
            Assert.AreEqual(8000m, _manager.Depositors[0].Amount);
            Assert.AreEqual(5000m, _manager.Depositors[1].Amount);
            Assert.AreEqual(2000m, _manager.Depositors[2].Amount);
        }

        // Середня сума вкладу
        [TestMethod]
        public void CalculateAverageAmount_ReturnsCorrectAverage()
        {
            _manager.AddDepositor(new BankDepositor("A", Iban1, 10000m, 2022));
            _manager.AddDepositor(new BankDepositor("B", Iban2, 20000m, 2023));
            _manager.AddDepositor(new BankDepositor("C", Iban3, 30000m, 2024));

            decimal avg = _manager.CalculateAverageAmount();

            Assert.AreEqual(20000m, avg);
        }

        // Середній вклад коли порожній список
        [TestMethod]
        public void CalculateAverageAmount_WhenListIsEmpty_ReturnsZero()
        {
            decimal avg = _manager.CalculateAverageAmount();
            Assert.AreEqual(0m, avg);
        }

        //  Групування за роком
        [TestMethod]
        public void FilterAboveAverage()
        {
            var d1 = new BankDepositor("A", Iban1, 1000m, 2020);
            var d2 = new BankDepositor("B", Iban2, 2000m, 2024);
            var d3 = new BankDepositor("C", Iban3, 3000m, 2022);
            _manager.AddDepositor(d1);
            _manager.AddDepositor(d2);
            _manager.AddDepositor(d3);

            _manager.FilterAboveAverageAmount();

            Assert.AreEqual(3000m, _manager.Depositors[0].Amount);
        }

        public void GroupDepositorsByYear_OrdersGroupsFromNewestToOldest()
        {
            var d1 = new BankDepositor("A", Iban1, 1000m, 2020);
            var d2 = new BankDepositor("B", Iban2, 2000m, 2024);
            var d3 = new BankDepositor("C", Iban3, 3000m, 2022);
            _manager.AddDepositor(d1);
            _manager.AddDepositor(d2);
            _manager.AddDepositor(d3);

            _manager.GroupDepositorsByYear();

            Assert.AreEqual(2024, _manager.Depositors[0].OpeningYear);
            Assert.AreEqual(2022, _manager.Depositors[1].OpeningYear);
            Assert.AreEqual(2020, _manager.Depositors[2].OpeningYear);
        }
    }
}