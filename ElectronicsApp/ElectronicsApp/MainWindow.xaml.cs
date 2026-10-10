using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using ElectronicsApp.Models;

namespace ElectronicsApp
{
    public partial class MainWindow : Window
    {
        // Список для зберігання всієї доданої техніки
        private List<Electronics> _electronics = new List<Electronics>();

        public MainWindow()
        {
            InitializeComponent();
            DeviceTypeComboBox.SelectedIndex = 0;
            SetInitialUiState();
        }

        private void DeviceTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetInitialUiState();
        }

        private void SetInitialUiState()
        {
            if (DeviceTypeComboBox == null || PhoneFields == null || LaptopFields == null)
                return;

            if (DeviceTypeComboBox.SelectedIndex == 0) // Телефон
            {
                PhoneFields.Visibility = Visibility.Visible;
                RamMemoryTextBox.Visibility = Visibility.Visible;

                LaptopFields.Visibility = Visibility.Collapsed;
                ScreenSizeTextBox.Visibility = Visibility.Collapsed;
            }
            else // Ноутбук
            {
                PhoneFields.Visibility = Visibility.Collapsed;
                RamMemoryTextBox.Visibility = Visibility.Collapsed;

                LaptopFields.Visibility = Visibility.Visible;
                ScreenSizeTextBox.Visibility = Visibility.Visible;
            }
        }

        /// Обробка натискання кнопки "Додати техніку".
        private void AddDeviceButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Зчитування та валідація ціни
                if (!double.TryParse(PriceTextBox.Text.Replace(',', '.'),
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture, out double price))
                {
                    MessageBox.Show("Будь ласка, введіть коректну ціну.", "Помилка введення", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Зчитування та валідація року
                if (!int.TryParse(YearTextBox.Text, out int year))
                {
                    MessageBox.Show("Будь ласка, введіть коректний рік.", "Помилка введення", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                string brand = BrandTextBox.Text;
                Electronics device;

                // Створення об'єкта залежно від обраного типу
                if (DeviceTypeComboBox.SelectedIndex == 0)
                {
                    if (!int.TryParse(RamMemoryTextBox.Text, out int ram))
                    {
                        MessageBox.Show("Будь ласка, введіть ціле число для об'єму пам'яті.", "Помилка введення", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    device = new Phone(brand, price, year, ram);
                }
                else
                {
                    if (!double.TryParse(ScreenSizeTextBox.Text.Replace(',', '.'),
                        System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.InvariantCulture, out double screen))
                    {
                        MessageBox.Show("Будь ласка, введіть коректний розмір екрану.", "Помилка введення", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    device = new Laptop(brand, price, year, screen);
                }

                // Додаємо техніку до загального списку
                _electronics.Add(device);

                string description = device.GetDescription();
                ElectronicsListBox.Items.Add(description);

                // Оновлюємо аналітику
                UpdateAnalytics();

                // Очищення полів введення
                BrandTextBox.Clear();
                PriceTextBox.Clear();
                YearTextBox.Clear();
                RamMemoryTextBox.Clear();
                ScreenSizeTextBox.Clear();
            }
            catch (ArgumentException ex)
            {
                // Обробка помилок валідації конструктора/властивостей
                MessageBox.Show(ex.Message, "Помилка валідації", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (FormatException ex)
            {
                // Обробка помилок формату
                MessageBox.Show(ex.Message, "Помилка формату", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                // Загальна обробка інших помилок
                MessageBox.Show("Сталася неочікувана помилка: " + ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// Оновлює інформацію про загальні розрахунки на екрані.
        private void UpdateAnalytics()
        {
            // Викликаємо статичний метод аналізу
            double totalDepreciation = ElectronicsAnalysis.GetTotalDepreciation(_electronics);

            // Оновлюємо текст у нижньому блоці
            TotalDepreciationTextBlock.Text = $"{totalDepreciation:C}";
            TotalCountTextBlock.Text = _electronics.Count.ToString();
        }
    }
}