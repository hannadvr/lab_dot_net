using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockExchangeApp;

namespace StockExchangeApp.Tests
{
    [TestClass]
    public class TradingBotTests
    {
        [TestMethod]
        public void ExecuteTrade_LowPrice_DecidesToBuy()
        {
            var bot = new TradingBot("TestBot");

            bot.ExecuteTrade(55.0);

            Assert.IsTrue(bot.LastDecision.Contains("КУПІВЛЯ"));
        }

        [TestMethod]
        public void ExecuteTrade_HighPrice_DecidesToSell()
        {
            var bot = new TradingBot("TestBot");

            bot.ExecuteTrade(98.0);

            Assert.IsTrue(bot.LastDecision.Contains("ПРОДАЖ"));
        }

        [TestMethod]
        public void TradingBot_ImplementsAllThreeInterfaces()
        {
            var bot = new TradingBot();

            Assert.IsInstanceOfType(bot, typeof(IMarketParticipant));
            Assert.IsInstanceOfType(bot, typeof(IRiskAssessor));
            Assert.IsInstanceOfType(bot, typeof(ITradeExecutor));
        }
    }
}