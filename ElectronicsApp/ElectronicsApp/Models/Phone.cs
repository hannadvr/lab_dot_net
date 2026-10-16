using System;
using System.Text;

namespace ElectronicsApp.Models
{
    public class Phone : Electronics, IBatteryOperated, IConnectable
    {
        public int RamMemory { get; private set; }
        public bool IsConnected { get; private set; }

        public Phone(string brand, double price, int year, int ramMemory)
            : base(brand, price, year)
        {
            if (ramMemory <= 0)
            {
                throw new ArgumentException("Об'єм пам'яті повинен бути більшим за 0.", nameof(ramMemory));
            }

            this.RamMemory = ramMemory;
            this.IsConnected = false;
        }

        // Реалізація інтерфейсу IConnectable
        public string ConnectToWifi(string networkName)
        {
            IsConnected = true;
            return $"Телефон {Brand} підключено до {networkName}";
        }

        // Реалізація інтерфейсу IBatteryOperated
        public virtual double CalculateBatteryLife()
        {
            return 10.0 + (RamMemory * 0.5);
        }

        public override double CalculateDepreciation()
        {
            int age = DateTime.Now.Year - Year;
            if (age <= 0) return 0;

            double depreciation = Price * 0.20 * age;
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
            protocol.AppendLine($"Wi-Fi: {(IsConnected ? "Підключено" : "Не підключено")}");
            protocol.AppendLine($"Робота батареї: {CalculateBatteryLife()} год.");
            protocol.AppendLine($"Ціна: {Price:C}");
            return protocol.ToString();
        }
    }
}