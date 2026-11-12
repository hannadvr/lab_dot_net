using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockExchangeApp;

namespace StockExchangeApp.Tests
{
    [TestClass]
    public class InsuranceCompanyTests
    {
        [TestMethod]
        public void AssessRisk_ExtremePrice_ReturnsCriticalRisk()
        {
            var insurance = new InsuranceCompany("TestInsurance");

            insurance.AssessRisk(110.0);

            Assert.IsTrue(insurance.CurrentRiskLevel.Contains("КРИТИЧНИЙ"));
        }

        [TestMethod]
        public void AssessRisk_NormalPrice_ReturnsStableRisk()
        {
            var insurance = new InsuranceCompany("TestInsurance");

            insurance.AssessRisk(75.0);

            Assert.AreEqual("Стабільний", insurance.CurrentRiskLevel);
        }
    }
}