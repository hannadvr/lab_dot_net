using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WinFormsApp1.lab2;

namespace UnitTest
{
    [TestClass]
    public class Calculation_abTest
    {
        // Неправильні межі
        [TestMethod]
        public void Calculation_a_great_b()
        {
            var calc = new Calculation_ab(10, 2);

            Assert.Throws<ArgumentOutOfRangeException>(() => calc.Product());
        }

        // Немає чисел які піходять за умовою
        [TestMethod]
        public void Calculation_no_multiples_returns_zero()
        {

            var calc = new Calculation_ab(1, 7);
            var testRes = 0;

            var res = calc.Product();

            Assert.AreEqual(testRes, res);
        }

        // Є числа які відповідають умові
        [TestMethod]
        public void Calculation_valid_range_returns_product()
        {
            var calc = new Calculation_ab(5, 20);
            var testRes = 128;

            var res = calc.Product();

            Assert.AreEqual(testRes, res);
        }
    }
}