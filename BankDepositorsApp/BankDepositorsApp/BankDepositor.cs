using System;
using System.Text.RegularExpressions;

namespace BankDepositorsApp
{
    public class BankDepositor : IComparable<BankDepositor>
    {
        private string _fullName;
        private string _accountNumber;
        private decimal _amount;
        private int _openingYear;

        public string FullName
        {
            get => _fullName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("ПІБ вкладника не може бути пустим.", nameof(FullName));
                _fullName = value.Trim();
            }
        }

        public string AccountNumber
        {
            get => _accountNumber;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Номер рахунку не може бути пустим.", nameof(AccountNumber));

                string cleaned = value.Replace(" ", "").ToUpperInvariant();
                var ibanRegex = new Regex(@"^UA\d{27}$");
                if (!ibanRegex.IsMatch(cleaned))
                {
                    throw new ArgumentException("Номер рахунку має відповідати формату IBAN України (UA та 27 цифр).", nameof(AccountNumber));
                }

                _accountNumber = cleaned;
            }
        }

        public decimal Amount
        {
            get => _amount;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Сума вкладу має бути більшою за нуль.", nameof(Amount));
                _amount = value;
            }
        }

        public int OpeningYear
        {
            get => _openingYear;
            set
            {
                int currentYear = DateTime.Now.Year;
                if (value > currentYear)
                    throw new ArgumentException("Рік відкриття рахунку не може бути в майбутньому.", nameof(OpeningYear));
                if (value < 1900)
                    throw new ArgumentException("Рік відкриття рахунку не може бути менше за 1900.", nameof(OpeningYear));
                _openingYear = value;
            }
        }

        public BankDepositor() { }

        // Конструктор
        public BankDepositor(string fullName, string accountNumber, decimal amount, int openingYear)
        {
            FullName = fullName;
            AccountNumber = accountNumber;
            Amount = amount;
            OpeningYear = openingYear;
        }

        // Порівняння
        public int CompareTo(BankDepositor other)
        {
            if (other == null) return 1;
            return string.Compare(this.AccountNumber, other.AccountNumber, StringComparison.OrdinalIgnoreCase);
        }

        // Перетворення в текстовий рядок
        public override string ToString()
        {
            return $"ПІБ: {FullName}, Рахунок: {AccountNumber}, Сума: {Amount:N2} грн, Рік: {OpeningYear}";
        }
    }
}