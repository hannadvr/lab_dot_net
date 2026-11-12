using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockExchangeApp;
using System;

namespace StockExchangeApp.Tests
{
    [TestClass]
    public class StockExchangeTests
    {
        [TestMethod]
        public void UpdateOilPrice_ValidPrice_FiresPriceChangedAndPriceChecked()
        {
            var exchange = new StockExchange();
            double checkedPrice = 0;
            double oldPriceReceived = 0;
            double newPriceReceived = 0;

            exchange.PriceChecked += p => checkedPrice = p;
            exchange.PriceChanged += (n, o) => { newPriceReceived = n; oldPriceReceived = o; };

            exchange.UpdateOilPrice(90.0);

            Assert.AreEqual(90.0, checkedPrice);
            Assert.AreEqual(75.0, oldPriceReceived);
            Assert.AreEqual(90.0, newPriceReceived);
        }

        [TestMethod]
        public void UpdateOilPrice_ZeroOrNegative_ThrowsArgumentException()
        {
            var exchange = new StockExchange();

            Assert.ThrowsExactly<ArgumentException>(() => exchange.UpdateOilPrice(0));
        }

        [TestMethod]
        public void CloseMarket_BlocksFurtherPriceUpdates()
        {
            var exchange = new StockExchange();
            bool closedEventFired = false;
            exchange.MarketClosed += _ => closedEventFired = true;

            exchange.CloseMarket();

            Assert.IsTrue(closedEventFired);
            Assert.IsFalse(exchange.IsMarketOpen);
            Assert.ThrowsExactly<InvalidOperationException>(() => exchange.UpdateOilPrice(80.0));
        }
    }
}