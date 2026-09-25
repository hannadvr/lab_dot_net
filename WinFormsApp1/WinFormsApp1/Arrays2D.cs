using System;
using System.Collections.Generic;
using System.Text;

namespace WinFormsApp1
{
    internal class Arrays2D
    {
        public bool error = false; // поле помилки
        int[,] a;                 // двовимірний масив
        int x_length = 5;
        int y_length = 3;

        public int X_length
        {
            get { return x_length; }
            set { x_length = value; }
        }

        public int Y_length
        {
            get { return y_length; }
            set { y_length = value; }
        }

        public int this[int i, int j]
        {
            get
            {
                if (i >= 0 && i < x_length && j >= 0 && j < y_length)
                {
                    return a[i, j];
                }
                else
                {
                    error = true;
                    return 0;
                }
            }
            set
            {
                if (i >= 0 && i < x_length && j >= 0 && j < y_length)
                {
                    a[i, j] = value;
                }
                else
                {
                    error = true;
                }
            }
        }

        void generate()
        {
            Random rand = new Random();
            for (int i = 0; i < x_length; i++)
            {
                for (int j = 0; j < y_length; j++)
                {
                    this[i, j] = rand.Next(-50, 50);
                }
            }
        }

        public Arrays2D(int a_length, int b_length)
        {
            x_length = a_length;
            y_length = b_length;
            a = new int[x_length, y_length];
            generate();
        }

        public Arrays2D()
        {
            a = new int[x_length, y_length];
            generate();
        }

        public string FindMultiplesOfFive()
        {
            List<string> results = new List<string>();
            for (int i = 0; i < x_length; i++)
            {
                for (int j = 0; j < y_length; j++)
                {
                    if (a[i, j] % 5 == 0 && a[i, j] != 0)
                    {
                        results.Add($"A[{i},{j}] = {a[i, j]}");
                    }
                }
            }

            if (results.Count == 0)
                return "Елементів, кратних 5, не знайдено";

            return string.Join("\n", results);
        }
        public int SumMain()
        {
            int sum = 0;
            for (int i = 0; i < x_length; i++)
            {
                for (int j = 0; j < y_length; j++)
                {
                    if (i == j)
                    {
                        sum += a[i, j]
                        ;
                    }
                }
            }
            return sum;
        }
    }
    }