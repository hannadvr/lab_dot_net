using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockExchangeApp;

namespace StockExchangeApp.Tests
{
    [TestClass]
    public class OilCompanyTests
    {
        [TestMethod]
        public void CalculateProfit_HighOilPrice_CalculatesPositiveProfit()
        {
            var company = new OilCompany("TestOil");

            // Собівартість = 45$, обсяг = 10 000 барелів  (85 - 45) * 10000 = 400 000$
            string result = company.CalculateProfit(85.0);

            Assert.AreEqual(400000.0, company.LastCalculatedProfit, 0.01);
            Assert.IsTrue(result.Contains("Прибуток"));
        }

        [TestMethod]
        public void CalculateProfit_LowOilPrice_CalculatesLoss()
        {
            var company = new OilCompany("TestOil");

            string result = company.CalculateProfit(35.0);

            Assert.AreEqual(-100000.0, company.LastCalculatedProfit, 0.01);
            Assert.IsTrue(result.Contains("Збиток"));
        }
    }
}