using System;
using System.IO;
using System.Windows;

namespace BankDepositorsApp
{
    public partial class MainWindow : Window
    {
        private readonly BankManager _manager;

        public MainWindow()
        {
            InitializeComponent();

            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string filePath = Path.Combine(documentsPath, "depositors.json");

            _manager = new BankManager(new JsonFileSaver(), new JsonFileReader(), filePath);
            DepositorsDataGrid.ItemsSource = _manager.Depositors;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!decimal.TryParse(AmountTextBox.Text, out decimal amount))
                    throw new ArgumentException("Введіть коректне числове значення для суми.");

                if (!int.TryParse(OpeningYearTextBox.Text, out int year))
                    throw new ArgumentException("Введіть коректний рік відкриття.");

                var newDepositor = new BankDepositor(
                    FullNameTextBox.Text,
                    AccountNumberTextBox.Text,
                    amount,
                    year
                );

                _manager.AddDepositor(newDepositor);

                FullNameTextBox.Clear();
                AccountNumberTextBox.Clear();
                AmountTextBox.Clear();
                OpeningYearTextBox.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (DepositorsDataGrid.SelectedItem is BankDepositor selectedDepositor)
            {
                _manager.DeleteDepositor(selectedDepositor);
            }
            else
            {
                MessageBox.Show("Оберіть вкладника для видалення зі списку.", "Увага", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void CurrentYearFilterButton_Click(object sender, RoutedEventArgs e)
        {
            _manager.FilterByCurrentYear();
        }

        private void ResetFilterButton_Click(object sender, RoutedEventArgs e)
        {
            _manager.ResetFilter();
        }

        private void SortButton_Click(object sender, RoutedEventArgs e)
        {
            _manager.SortByAmount();
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await _manager.SaveAsync();
                MessageBox.Show("Список вкладників успішно збережено.", "Збереження", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при збереженні: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await _manager.LoadAsync();
                MessageBox.Show("Список вкладників успішно завантажено.", "Завантаження", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при завантаженні: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Пошук за ПІБ або номером рахунку
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SearchTextBox.Text))
            {
                _manager.ResetFilter();
            }
            else
            {
                _manager.SearchDepositors(SearchTextBox.Text);
            }
        }

        // Список унікальних вкладників
        private void UniqueDepositorsButton_Click(object sender, RoutedEventArgs e)
        {
            _manager.ShowUniqueDepositors();
        }

        // Топ-3 вклади
        private void TopThreeButton_Click(object sender, RoutedEventArgs e)
        {
            _manager.FindTopThreeDeposits();
        }

        // Розрахунок середнього вкладу
        private void AverageAmountButton_Click(object sender, RoutedEventArgs e)
        {
            decimal avg = _manager.CalculateAverageAmount();
            MessageBox.Show($"Середня сума вкладу = {avg:N2} грн", "Розрахунки", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Групування за роком відкриття
        private void GroupByYearButton_Click(object sender, RoutedEventArgs e)
        {
            _manager.GroupDepositorsByYear();
        }
    }
}