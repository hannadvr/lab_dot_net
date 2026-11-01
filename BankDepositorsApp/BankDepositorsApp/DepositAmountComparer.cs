using System.Collections.Generic;

namespace BankDepositorsApp
{
    public class DepositorAmountComparer : IComparer<BankDepositor>
    {
        // Порівняння вкладу
        public int Compare(BankDepositor x, BankDepositor y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            return x.Amount.CompareTo(y.Amount);
        }
    }
}