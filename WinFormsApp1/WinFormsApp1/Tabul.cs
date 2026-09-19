using System;
using System.Collections.Generic;
using System.Text;

namespace WinFormsApp1
{
    internal class Tabul
    {
        public double[,] xy = new double[1000, 2];
        public int n = 0;
        private double f1(double x)
        {
            double a = Math.Pow(Math.Sin(Math.Pow(x + 5, 2)), 2);
            double b = Math.Exp(-x) + Math.Cbrt(3 * Math.Pow(x, 2) + 1);
            return a / b;
        }
        private double f2(double x)
        {
            double a = Math.Pow(x, 4) + 2 * Math.Pow(x, 3) - x;
            double b = Math.Cos(Math.Pow(x + 3, 2));

            return a / b;
        }
        private double f3(double x)
        {
            double a = Math.Tan(Math.Pow(x, 2) + 4 * x - 1);
            double b = 2 * Math.Sqrt(Math.Pow(x, 3)) * Math.Sin(Math.Pow(x, 3));

            return a / b;
        }
        public void tab(double xn=2.45, double xk=25.2, double h=0.1, double a = 5.0)
        {
            double x = xn, y;
            int i = 0;
            while (x <= xk)
            {
                if (x <= 0)
                {
                    y = f1(x);
                }
                else
                {
                    if (x>0 && x <= a)
                    {
                        y=f2(x);
                    }
                    else
                    {
                        y = f3(x);
                    }
                }
                xy[i, 0] = x;
                xy[i, 1] = y;
                x = x + h;
                i++;
            }
            n = i;
        }
    }
}
