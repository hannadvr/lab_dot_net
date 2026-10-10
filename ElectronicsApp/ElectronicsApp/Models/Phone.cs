using System;
using System.Text;

namespace ElectronicsApp.Models
{
    public class Phone : Electronics
    {
        public int RamMemory;

        public Phone(string brand, double price, int year, int ramMemory)
            : base(brand, price, year)
        {
            if (ramMemory <= 0)
            {
                throw new ArgumentException("Об'єм пам'яті повинен бути більшим за 0.", nameof(ramMemory));
            }

            this.RamMemory = ramMemory;
        }

        public virtual double CalculateBatteryLife()
        {
            return 10.0 + (RamMemory * 0.5);
        }

        public override double CalculateDepreciation()
        {
            int age = DateTime.Now.Year - Year;
            if (age <= 0) return 0;

            double depreciation = Price * 0.20 * age;

            // Максимальна амортизація не перевищує 90% від початкової вартості
            double maxDepreciation = Price * 0.90;
            if (depreciation > maxDepreciation)
            {
                depreciation = maxDepreciation;
            }

            return depreciation;
        }

        public override string GetDescription()
        {
            var protocol = new StringBuilder();
            protocol.AppendLine($"Телефон {Brand}");
            protocol.AppendLine($"Рік: {Year}");
            protocol.AppendLine($"ОЗП: {RamMemory} ГБ");
            protocol.AppendLine($"Робота батареї: {CalculateBatteryLife()} год.");
            protocol.AppendLine($"Ціна: {Price:C}");
            return protocol.ToString();
        }
    }
}