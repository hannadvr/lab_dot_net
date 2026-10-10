using System;
using System.Collections.Generic;
using System.Text;

namespace WinFormsApp1.lab2
{
    public class Calculation_abc
    {
        public Calculation_abc()
        {
            a = 0;
            b = 0;
            c = 0;
        }

        public Calculation_abc(int a)
        {
            this.a = a;
            b = 0;
            c = 0;
        }

        public Calculation_abc(int a, int b, int c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }

        // Приховані поля
        int a, b, c;

        // Відкриті властивості
        public int A { get; set; }
        public int B { get; set; }
        public int C { get; set; }

        // Метод для розрахунку
        public double D()
        {
            if (a % 2 == 0 && b % 2 == 0 && c % 2 == 0)
                return a * b * c;
            else if (a % 2 == 1 || b % 2 == 1 || c % 2 == 1)
                return (a + b + c) / 3;
            else
                return (a + b + c) * (a + b + c);
        }
    }
}
