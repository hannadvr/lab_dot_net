using System;
using System.Windows;

namespace StockExchangeApp
{
    public partial class MainWindow : Window
    {
        private readonly StockExchange _exchange;
        private readonly OilCompany _oilCompany;
        private readonly InsuranceCompany _insuranceCompany;
        private readonly TradingBot _tradingBot;

        public MainWindow()
        {
            InitializeComponent();
            btnOpenMarket.IsEnabled = false; // Оскільки при старті біржа вже відкрита

            // 1. Ініціалізація об'єктів
            _exchange = new StockExchange();
            _oilCompany = new OilCompany("UkrNafta Corp");
            _insuranceCompany = new InsuranceCompany("SafeEnergy Insurance");
            _tradingBot = new TradingBot("AlphaQuant Bot");

            // 2. Підписка на ШИРОКОМОВНУ подію 1 (багатоадресний делегат: 3 підписники)
            _exchange.PriceChanged += _oilCompany.HandlePriceChanged;
            _exchange.PriceChanged += _insuranceCompany.HandlePriceChanged;
            _exchange.PriceChanged += _tradingBot.HandlePriceChanged;

            // 3. Підписка на ШИРОКОМОВНУ подію 2 (закриття ринку)
            _exchange.MarketClosed += _oilCompany.HandleMarketClosed;
            _exchange.MarketClosed += _insuranceCompany.HandleMarketClosed;
            _exchange.MarketClosed += _tradingBot.HandleMarketClosed;
            _exchange.MarketClosed += LogMessage;

            // 4. Підписка на подію перевірки ціни (Подія 3)
            _exchange.PriceChecked += price => LogMessage($"[Біржа] Зафіксовано котирування: {price:F2}$/барель");

            // 5. Підписка UI на повідомлення від учасників (Події 4 та 5)
            _oilCompany.MessageGenerated += LogMessage;
            _insuranceCompany.MessageGenerated += LogMessage;
            _tradingBot.MessageGenerated += LogMessage;
            _tradingBot.BotOperationCompleted += () => btnUpdatePrice.IsEnabled = true;

            LogMessage("Торгову сесію розпочато. Поточна ціна нафти: 75.00$");
        }

        private void btnUpdatePrice_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(txtOilPrice.Text.Replace('.', ','), out double newPrice))
            {
                try
                {
                    if (Math.Abs(newPrice - _exchange.CurrentOilPrice) > 0.001)
                        btnUpdatePrice.IsEnabled = false;

                    _exchange.UpdateOilPrice(newPrice);
                }
                catch (Exception ex)
                {
                    btnUpdatePrice.IsEnabled = true;
                    LogMessage($"ПОМИЛКА: {ex.Message}");
                }
            }
            else
            {
                LogMessage("ПОМИЛКА: Введіть коректне числове значення ціни.");
            }
        }

        private void btnCloseMarket_Click(object sender, RoutedEventArgs e)
        {
            _exchange.CloseMarket();
            btnUpdatePrice.IsEnabled = false;
            btnCloseMarket.IsEnabled = false; // Блокуємо "Закрити", бо ринок вже закритий
            btnOpenMarket.IsEnabled = true;    // Розблоковуємо "Відкрити"
        }

        private void btnOpenMarket_Click(object sender, RoutedEventArgs e)
        {
            if (_exchange.OpenMarket())
            {
                btnUpdatePrice.IsEnabled = true;
                btnCloseMarket.IsEnabled = true;   // Знову можна закрити
                btnOpenMarket.IsEnabled = false;   // Блокуємо "Відкрити", бо ринок вже відкритий
                LogMessage("[Біржа] Ринок знову відкрито для торгів!");
            }
        }

        private void LogMessage(string message)
        {
            Dispatcher.Invoke(() =>
            {
                LogListBox.Items.Add($"[{DateTime.Now:HH:mm:ss}] {message}");
                LogListBox.ScrollIntoView(LogListBox.Items[LogListBox.Items.Count - 1]);
            });
        }
    }
}