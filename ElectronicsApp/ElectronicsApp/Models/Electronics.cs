using System;

namespace ElectronicsApp.Models
{
    public abstract class Electronics
    {
        public string Brand;
        public double Price;
        public int Year;

        public Electronics(string brand, double price, int year)
        {
            if (string.IsNullOrWhiteSpace(brand))
            {
                throw new ArgumentException("Бренд повинен бути вказаний.", nameof(brand));
            }

            if (price <= 0)
            {
                throw new ArgumentException("Ціна повинна бути більшою за 0.", nameof(price));
            }

            if (year < 1990 || year > DateTime.Now.Year)
            {
                throw new ArgumentException($"Рік повинен бути в межах від 1990 до {DateTime.Now.Year}.", nameof(year));
            }

            this.Brand = brand;
            this.Price = price;
            this.Year = year;
        }

        public abstract double CalculateDepreciation();
        public abstract string GetDescription();
    }
}