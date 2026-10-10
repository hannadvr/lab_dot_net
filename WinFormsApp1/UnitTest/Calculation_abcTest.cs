using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WinFormsApp1.lab2;

namespace UnitTest
{
    [TestClass]
    public class Calculation_abcTest
    {
        // Усі числа парні
        [TestMethod]
        public void Calculation_all_even()
        {
            var calc = new Calculation_abc(2, 4, 6);
            var testRes = 48;

            var res = calc.D();

            Assert.AreEqual(testRes, res);
        }

        // Є одне непарне число
        [TestMethod]
        public void Calculation_with_one_odd()
        {
            var calc = new Calculation_abc(2, 3, 4);
            var testRes = 81;

            var res = calc.D();

            Assert.AreEqual(testRes, res);
        }

        // Усі числа не парні
        [TestMethod]
        public void Calculation_all_odd()
        {
            var calc = new Calculation_abc(1, -3, 5);
            var testRes = 9;

            var res = calc.D();

            Assert.AreEqual(testRes, res);
        }

        // Є два непарні числа
        [TestMethod]
        public void Calculation_with_two_odd()
        {
            var calc = new Calculation_abc(3, 7, -6);
            var testRes = 16;

            var res = calc.D();

            Assert.AreEqual(testRes, res);
        }

        // Є нулі
        [TestMethod]
        public void Calculation_with_zero()
        {
            var calc = new Calculation_abc(3);
            var testRes = 9;

            var res = calc.D();

            Assert.AreEqual(testRes, res);
        }

        [TestMethod]
        public void Calculation_test()
        {
            var calc = new Calculation_abc(3, 3, 6);
            var testRes = 4;

            var res = calc.D();

            Assert.AreEqual(testRes, res);
        }
    }
}
