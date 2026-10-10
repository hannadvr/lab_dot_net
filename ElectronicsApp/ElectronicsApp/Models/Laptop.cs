using System;
using System.Text;

namespace ElectronicsApp.Models
{
    public class Laptop : Electronics
    {
        public double ScreenSize;

        public Laptop(string brand, double price, int year, double screenSize)
            : base(brand, price, year)
        {
            if (screenSize <= 0)
            {
                throw new ArgumentException("Розмір екрану повинен бути більшим за 0.", nameof(screenSize));
            }

            this.ScreenSize = screenSize;
        }

        public virtual int GetBenchmarkScore()
        {
            return (int)(Price / 10) + (Year * 2);
        }

        public override double CalculateDepreciation()
        {
            int age = DateTime.Now.Year - Year;
            if (age <= 0) return 0;

            double depreciation = Price * 0.15 * age;

            // Максимальна амортизація для ноутбука не більше 85% від вартості
            double maxDepreciation = Price * 0.85;
            if (depreciation > maxDepreciation)
            {
                depreciation = maxDepreciation;
            }

            return depreciation;
        }

        public override string GetDescription()
        {
            var protocol = new StringBuilder();
            protocol.AppendLine($"Ноутбук {Brand}");
            protocol.AppendLine($"Рік: {Year}");
            protocol.AppendLine($"Екран: {ScreenSize}\"");
            protocol.AppendLine($"Бенчмарк: {GetBenchmarkScore()}");
            protocol.AppendLine($"Ціна: {Price:C}");
            return protocol.ToString();
        }
    }
}