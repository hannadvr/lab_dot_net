using System;
using System.Collections.Generic;
using System.Text;

namespace WinFormsApp1.lab2
{
    public class Calculation_ab
    {
        // Приховані поля
        int a, b;

        public Calculation_ab()
        {
            a = 0;
            b = 0;
        }

        public Calculation_ab(int a)
        {
            this.a = a;
            b = 0;
        }

        public Calculation_ab(int a, int b)
        {
            this.a = a;
            this.b = b;
        }

        // Відкриті властивості
        public int A { get { return a; } }
        public int B { get { return b; } }

        public long Product()
        {
            if (a > b)
            {
                throw new ArgumentOutOfRangeException("A should be less than or equal to B");
            }

            long product = 1;
            bool found = false;

            for (int i = a; i <= b; i++)
            {
                if (i % 8 == 0)
                {
                    product *= i;
                    found = true;
                }
            }

            if (!found)
            {
                return 0;
            }

            return product;
        }
    }
}