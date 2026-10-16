using System;
using System.Text;

namespace ElectronicsApp.Models
{
    public class Tablet : Electronics, IPerformanceEvaluated, IConnectable
    {
        public bool HasStylus { get; private set; }
        public bool IsConnected { get; private set; }

        public Tablet(string brand, double price, int year, bool hasStylus)
            : base(brand, price, year)
        {
            this.HasStylus = hasStylus;
            this.IsConnected = false;
        }

        // Реалізація інтерфейсу IConnectable
        public string ConnectToWifi(string networkName)
        {
            IsConnected = true;
            return $"Планшет {Brand} підключено до {networkName}";
        }

        // Реалізація інтерфейсу IPerformanceEvaluated
        public virtual int GetBenchmarkScore()
        {
            int baseScore = (int)(Price / 12) + (Year * 2);
            return HasStylus ? baseScore + 200 : baseScore;
        }

        public override double CalculateDepreciation()
        {
            int age = DateTime.Now.Year - Year;
            if (age <= 0) return 0;

            double depreciation = Price * 0.18 * age;
            double maxDepreciation = Price * 0.85;
            return depreciation > maxDepreciation ? maxDepreciation : depreciation;
        }

        public override string GetDescription()
        {
            var protocol = new StringBuilder();
            protocol.AppendLine($"Планшет {Brand}");
            protocol.AppendLine($"Рік: {Year}");
            protocol.AppendLine($"Стилус: {(HasStylus ? "Так" : "Ні")}");
            protocol.AppendLine($"Wi-Fi: {(IsConnected ? "Підключено" : "Не підключено")}");
            protocol.AppendLine($"Бенчмарк: {GetBenchmarkScore()}");
            protocol.AppendLine($"Ціна: {Price:C}");
            return protocol.ToString();
        }
    }
}