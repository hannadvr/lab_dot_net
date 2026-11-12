namespace StockExchangeApp
{
    // Страхова компанія
    public class InsuranceCompany : IMarketParticipant, IRiskAssessor
    {
        public string Name { get; }
        public string CurrentRiskLevel { get; private set; } = "Низький";

        public event Action<string>? MessageGenerated;

        public InsuranceCompany(string name = "SafeEnergy Insurance")
        {
            Name = name;
        }

        public string AssessRisk(double oilPrice)
        {
            if (oilPrice > 100 || oilPrice < 50)
                CurrentRiskLevel = "КРИТИЧНИЙ (підвищення тарифів на 25%)";
            else if (oilPrice > 85 || oilPrice < 60)
                CurrentRiskLevel = "Помірний (корекція резервів)";
            else
                CurrentRiskLevel = "Стабільний";

            return $"[{Name}] Оцінка ризиків: {CurrentRiskLevel}";
        }

        public string OnMarketClosed()
        {
            return $"[{Name}] Страхові поліси зафіксовано. Статус ризику: {CurrentRiskLevel}.";
        }

        public void HandlePriceChanged(double newPrice, double oldPrice)
        {
            MessageGenerated?.Invoke(AssessRisk(newPrice));
        }

        public void HandleMarketClosed(string info)
        {
            MessageGenerated?.Invoke(OnMarketClosed());
        }
    }
}