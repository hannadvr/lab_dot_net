using System;
using System.Collections.Generic;
using System.Text;

namespace WinFormsApp1.lab2
{
    public class RegularPolygon
    {
        int n;
        double a;

        public RegularPolygon()
        {
            n = 3;
            a = 1.0;
        }

        public RegularPolygon(int n)
        {
            if (n < 3)
                throw new ArgumentException("Кількість сторін має бути не менше 3.");

            this.n = n;
            a = 1.0;
        }

        public RegularPolygon(int n, double a)
        {
            if (n < 3)
                throw new ArgumentException("Кількість сторін має бути не менше 3.");
            if (a <= 0)
                throw new ArgumentOutOfRangeException(nameof(a), "Довжина сторони має бути більшою за 0.");

            this.n = n;
            this.a = a;
        }

        public int N
        {
            get { return n; }
            set
            {
                if (value < 3)
                    throw new ArgumentException("Кількість сторін має бути не менше 3.");
                n = value;
            }
        }

        public double A
        {
            get { return a; }
        }

        public double CalculateArea()
        {
            return (n * Math.Pow(a, 2)) / (4 * Math.Tan(Math.PI / n));
        }

        public double GetCircumscribedRadius()
        {
            return a / (2 * Math.Sin(Math.PI / n));
        }

        public double GetInscribedRadius()
        {
            return a / (2 * Math.Tan(Math.PI / n));
        }
    }
}
