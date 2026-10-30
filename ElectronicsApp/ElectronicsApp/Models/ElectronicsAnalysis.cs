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
        public static double GetSer(List<Electronics> items)
        {
            if (items == null || items.Count == 0) return 0;

            double total = 0;
            foreach (var item in items)
            {
                total += item.Price;
            }

            return total / items.Count;
        }
    }
}