using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankDepositorsApp;

namespace BankDepositorsTests
{
    [TestClass]
    public class DepositorAmountComparerTests
    {
        private const string Iban1 = "UA111111111111111111111111111";
        private const string Iban2 = "UA222222222222222222222222222";

        [TestMethod]
        public void Compare_WhenAmountsAreDifferent_ReturnsCorrectOrder()
        {
            var d1 = new BankDepositor("A", Iban1, 1000m, 2021);
            var d2 = new BankDepositor("B", Iban2, 5000m, 2021);
            var comparer = new DepositorAmountComparer();

            Assert.IsTrue(comparer.Compare(d1, d2) < 0);
            Assert.IsTrue(comparer.Compare(d2, d1) > 0);
        }

        [TestMethod]
        public void Compare_WhenAmountsAreIdentical_ReturnsZero()
        {
            var d1 = new BankDepositor("A", Iban1, 2000m, 2020);
            var d2 = new BankDepositor("B", Iban2, 2000m, 2022);
            var comparer = new DepositorAmountComparer();

            Assert.AreEqual(0, comparer.Compare(d1, d2));
        }

        [TestMethod]
        public void Compare_WhenFirstDepositorIsNull_ReturnsNegative()
        {
            var d = new BankDepositor("B", Iban2, 2000m, 2022);
            var comparer = new DepositorAmountComparer();

            Assert.IsTrue(comparer.Compare(null, d) < 0);
        }

        [TestMethod]
        public void Compare_WhenBothAreNull_ReturnsZero()
        {
            var comparer = new DepositorAmountComparer();
            Assert.AreEqual(0, comparer.Compare(null, null));
        }
    }
}