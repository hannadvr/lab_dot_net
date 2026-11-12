namespace StockExchangeApp
{
    //  Нафтова компанія
    public class OilCompany : IMarketParticipant, IProfitCalculator
    {
        public string Name { get; }
        public double LastCalculatedProfit { get; private set; }
        private const double ProductionCost = 45.0; // Собівартість видобутку
        private const double DailyBarrels = 10000;

        //  Відправка повідомлень
        public event Action<string>? MessageGenerated;

        public OilCompany(string name = "UkrNafta Corp")
        {
            Name = name;
        }

        public string CalculateProfit(double oilPrice)
        {
            LastCalculatedProfit = (oilPrice - ProductionCost) * DailyBarrels;
            string status = LastCalculatedProfit >= 0 ? "Прибуток" : "Збиток";
            return $"[{Name}] {status}: {LastCalculatedProfit:N0}$/день (при ціні {oilPrice:F2}$)";
        }

        public string OnMarketClosed()
        {
            return $"[{Name}] Фінансовий звіт за день сформовано. Підсумок: {LastCalculatedProfit:N0}$.";
        }

        // Обробники подій біржі
        public void HandlePriceChanged(double newPrice, double oldPrice)
        {
            MessageGenerated?.Invoke(CalculateProfit(newPrice));
        }

        public void HandleMarketClosed(string info)
        {
            MessageGenerated?.Invoke(OnMarketClosed());
        }
    }
}