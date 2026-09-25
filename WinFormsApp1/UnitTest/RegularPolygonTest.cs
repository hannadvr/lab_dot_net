using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WinFormsApp1.lab2;

namespace UnitTest
{
    [TestClass]
    public class RegularPolygonTest
    {
        // Розрахунок площі
        [TestMethod]
        public void Test_CalculateArea_Square()
        {
            var polygon = new RegularPolygon(4, 2.0);
            var testRes = 4.0;

            var res = polygon.CalculateArea();

            Assert.AreEqual(testRes, res, 0.001);
        }

        // Розрахунок радіуса описаного кола
        [TestMethod]
        public void Test_GetCircumscribedRadius_Square()
        {
            var polygon = new RegularPolygon(4, 2.0);
            var testRes = Math.Sqrt(2);

            var res = polygon.GetCircumscribedRadius();

            Assert.AreEqual(testRes, res, 0.001);
        }

        // Радіус вписаного кола
        [TestMethod]
        public void Test_GetInscribedRadius_Square()
        {
            var polygon = new RegularPolygon(4, 2.0);
            var testRes = 1.0;

            var res = polygon.GetInscribedRadius();

            Assert.AreEqual(testRes, res, 0.001);
        }

        // Неправильна кількість сторін
        [TestMethod]
        public void Test_InvalidSidesCount_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => new RegularPolygon(2, 5.0));
        }

        // Неправильна довжина сторін
        [TestMethod]
        public void Test_NegativeSideLength_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => new RegularPolygon(4, -2));
        }
    }
}
