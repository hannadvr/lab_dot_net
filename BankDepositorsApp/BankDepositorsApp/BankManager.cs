using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        // Додавання елемента з ручною перевіркою дубліката без LINQ (Any)
        public void AddDepositor(BankDepositor depositor)
        {
            if (depositor == null)
                throw new ArgumentNullException(nameof(depositor));

            foreach (var item in _allDepositors)
            {
                if (string.Equals(item.AccountNumber, depositor.AccountNumber, StringComparison.OrdinalIgnoreCase))
                {
                    throw new InvalidOperationException($"Вкладник із номером рахунку '{depositor.AccountNumber}' вже існує в базі.");
                }
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

        // Сортування через стандартний метод List<T>.Sort із компарером (без LINQ OrderBy)
        public void SortByAmount()
        {
            var sorted = new List<BankDepositor>(Depositors);
            sorted.Sort(new DepositorAmountComparer());
            UpdateVisibleCollection(sorted);
        }

        // Фільтрація через звичайний цикл foreach (без LINQ Where)
        public void FilterByCurrentYear()
        {
            int currentYear = DateTime.Now.Year;
            var filtered = new List<BankDepositor>();

            foreach (var depositor in _allDepositors)
            {
                if (depositor.OpeningYear == currentYear)
                {
                    filtered.Add(depositor);
                }
            }

            UpdateVisibleCollection(filtered);
        }

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