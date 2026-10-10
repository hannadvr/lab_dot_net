using System.Collections.Generic;

namespace ElectronicsApp.Models
{
    public class ElectronicsAnalysis
    {
        public static double GetTotalDepreciation(List<Electronics> items)
        {
            if (items == null) return 0;

            double total = 0;
            foreach (var item in items)
            {
                total += item.CalculateDepreciation();
            }
            return total;
        }
    }
}