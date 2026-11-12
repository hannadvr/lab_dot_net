using System;
using System.Threading.Tasks;

namespace StockExchangeApp
{

    // Торговий бот 
    public class TradingBot : IMarketParticipant, IRiskAssessor, ITradeExecutor
    {
        public string Name { get; }
        public string LastDecision { get; private set; } = "Очікування";

        public event Action<string>? MessageGenerated;
        // Завершення алгоритмічної серії ордерів
        public event Action? BotOperationCompleted;

        public TradingBot(string name = "AlphaQuant Bot v2")
        {
            Name = name;
        }

        public string AssessRisk(double oilPrice)
        {
            return oilPrice > 95 ? "Висока волатильність" : "Нормальна волатильність";
        }

        public string ExecuteTrade(double oilPrice)
        {
            if (oilPrice < 65)
                LastDecision = "КУПІВЛЯ (BUY) — ціна нижче ринкової підтримки";
            else if (oilPrice > 90)
                LastDecision = "ПРОДАЖ (SELL) — фіксація пікового прибутку";
            else
                LastDecision = "УТРИМАННЯ (HOLD) — нейтральний коридор";

            return $"[{Name}] Рішення: {LastDecision} | Волатильність: {AssessRisk(oilPrice)}";
        }

        public string OnMarketClosed()
        {
            LastDecision = "Ордери скасовано (ринок закритий)";
            return $"[{Name}] {LastDecision}";
        }

        // Асинхронний обробник зміни ціни
        public async void HandlePriceChanged(double newPrice, double oldPrice)
        {
            MessageGenerated?.Invoke($"[{Name}] Аналіз стрибка ціни ({oldPrice:F2}$ -> {newPrice:F2}$)...");
            await Task.Delay(600); // Імітація обробки ордера
            MessageGenerated?.Invoke(ExecuteTrade(newPrice));
            BotOperationCompleted?.Invoke();
        }

        public void HandleMarketClosed(string info)
        {
            MessageGenerated?.Invoke(OnMarketClosed());
        }
    }
}