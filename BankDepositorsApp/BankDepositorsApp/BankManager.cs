using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace BankDepositorsApp
{
    public class BankManager
    {
        private readonly IFileSaver _fileSaver;
        private readonly IFileLoader _fileLoader;
        private readonly string _filePath;

        public ObservableCollection<BankDepositor> Depositors { get; }
        private List<BankDepositor> _allDepositors;

        public BankManager(IFileSaver fileSaver, IFileLoader fileLoader, string filePath)
        {
            _fileSaver = fileSaver;
            _fileLoader = fileLoader;
            _filePath = filePath;

            Depositors = new ObservableCollection<BankDepositor>();
            _allDepositors = new List<BankDepositor>();
        }

        public void AddDepositor(BankDepositor depositor)
        {
            if (depositor == null)
                throw new ArgumentNullException(nameof(depositor));

            if (_allDepositors.Any(item => string.Equals(item.AccountNumber, depositor.AccountNumber, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException($"Вкладник із номером рахунку '{depositor.AccountNumber}' вже існує в базі.");
            }

            Depositors.Add(depositor);
            _allDepositors.Add(depositor);
        }

        public void DeleteDepositor(BankDepositor depositor)
        {
            if (depositor == null) return;
            Depositors.Remove(depositor);
            _allDepositors.Remove(depositor);
        }

        // Сортування за сумою
        public void SortByAmount()
        {
            var sorted = Depositors
                .OrderBy(d => d.Amount)
                .ToList();

            UpdateVisibleCollection(sorted);
        }

        // Фільтрація за поточним роком через
        public void FilterByCurrentYear()
        {
            int currentYear = DateTime.Now.Year;
            var filtered = _allDepositors
                .Where(d => d.OpeningYear == currentYear)
                .ToList();

            UpdateVisibleCollection(filtered);
        }

        //5 нових методів

        // Пошук за частиною ПІБ або номера рахунку
        public void SearchDepositors(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                ResetFilter();
                return;
            }

            string lowerTerm = searchTerm.Trim().ToLowerInvariant();
            var searchResult = _allDepositors
                .Where(d => d.FullName.ToLowerInvariant().Contains(lowerTerm) ||
                            d.AccountNumber.ToLowerInvariant().Contains(lowerTerm))
                .ToList();

            UpdateVisibleCollection(searchResult);
        }

        // Отримання списку унікальних імен вкладників
        public void ShowUniqueDepositors()
        {
            var uniqueNames = _allDepositors
                .Select(d => d.FullName)
                .Distinct()
                .OrderBy(name => name)
                .ToList();

            var uniqueDepositors = uniqueNames
                .Select(name => new BankDepositor(name, "UA000000000000000000000000000", 1m, DateTime.Now.Year))
                .ToList();

            UpdateVisibleCollection(uniqueDepositors);
        }

        // Знайти 3 найбільші вклади
        public void FindTopThreeDeposits()
        {
            var topDeposits = _allDepositors
                .OrderByDescending(d => d.Amount)
                .Take(3)
                .ToList();

            UpdateVisibleCollection(topDeposits);
        }

        // Розрахунок середньої суми вкладу
        public decimal CalculateAverageAmount()
        {
            if (_allDepositors.Count == 0)
                return 0;

            return _allDepositors.Average(d => d.Amount);
        }

        // Згрупувати вкладників за роком відкриття (від нових до старих)
        public void GroupDepositorsByYear()
        {
            var grouped = _allDepositors
                .GroupBy(d => d.OpeningYear)
                .OrderByDescending(g => g.Key)
                .SelectMany(g => g)
                .ToList();

            UpdateVisibleCollection(grouped);
        }

        // Скидання фільтру
        public void ResetFilter()
        {
            UpdateVisibleCollection(_allDepositors);
        }

        private void UpdateVisibleCollection(List<BankDepositor> newList)
        {
            Depositors.Clear();
            foreach (var item in newList)
            {
                Depositors.Add(item);
            }
        }

        public async Task SaveAsync()
        {
            await _fileSaver.SaveAsync(_filePath, _allDepositors);
        }

        public async Task LoadAsync()
        {
            var loaded = await _fileLoader.LoadAsync<BankDepositor>(_filePath);
            if (loaded != null)
            {
                _allDepositors = loaded;
                UpdateVisibleCollection(_allDepositors);
            }
        }
    }
}