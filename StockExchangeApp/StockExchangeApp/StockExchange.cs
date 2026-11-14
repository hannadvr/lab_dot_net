using System;

namespace StockExchangeApp
{
    public class StockExchange
    {
        private double _currentOilPrice = 75.0; // Базова ціна нафти ($/барель)
        private bool _isMarketOpen = true;
        public double _max = 75.0;
        public double _min = 75.0;

        public double CurrentOilPrice
        {
            get => _currentOilPrice;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Ціна нафти повинна бути більшою за 0$!");
                _currentOilPrice = value;
            }
        }

        public bool IsMarketOpen => _isMarketOpen;

        // Ціна змінилась (використовує багатоадресний делегат)
        public event OilPriceChangedHandler? PriceChanged;

        // Ринок закрився
        public event Action<string>? MarketClosed;

        // Котирування перевірено (для журналу подій)
        public event Action<double>? PriceChecked;

        public void UpdateOilPrice(double newPrice)
        {
            if (!_isMarketOpen)
                throw new InvalidOperationException("Торги завершено! Ринок закритий.");

            double oldPrice = _currentOilPrice;
            CurrentOilPrice = newPrice; // Валідація
            if (_max < newPrice) _max = newPrice;
            if (_min  > newPrice) _min = newPrice;

            // Сповіщаємо про факт перевірки/оновлення ціни
            PriceChecked?.Invoke(newPrice);

            // Якщо ціна змінилась — викликаємо широкомовну подію для всіх підписників
            if (Math.Abs(newPrice - oldPrice) > 0.001)
            {
                PriceChanged?.Invoke(newPrice, oldPrice);
            }
        }

        public void CloseMarket()
        {
            if (!_isMarketOpen) return;

            _isMarketOpen = false;
            MarketClosed?.Invoke($"Торгову сесію закрито. Фінальна ціна нафти: {_currentOilPrice:F2}$, мінімальна ціна: {_min:F2}$,максимальна ціна:{_max:F2}$");
        }

        public bool OpenMarket()
        {
            if (_isMarketOpen)
                return false; // Біржа вже відкрита

            _isMarketOpen = true;
            return true;
        }
    }
}