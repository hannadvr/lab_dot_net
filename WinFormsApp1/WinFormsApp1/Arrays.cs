using System;
using System.Collections.Generic;
using System.Text;

namespace WinFormsApp1
{
    internal class Arrays
    {
        public bool error = false; // відкрите поле помилки
        int[] a;                  // закритий масив
        int length = 7;           // розмірність

        public int Length
        {
            get { return length; }
            set { length = value; }
        }

        public int this[int i]
        {
            get
            {
                if (i >= 0 && i < length)
                {
                    error = false;
                    return a[i];
                }
                else
                {
                    error = true;
                    return 0;
                }
            }
            set
            {
                if (i >= 0 && i < length)
                {
                    a[i] = value;
                    error = false;
                }
                else
                {
                    error = true;
                }
            }
        }

        public Arrays()
        {
            a = new int[length];
            Random rnd = new Random();
            for (int i = 0; i < length; i++)
            {
                a[i] = rnd.Next(-50, 50);
            }
        }

        public Arrays(int size)
        {
            length = size;
            a = new int[length];
            Random rnd = new Random();
            for (int i = 0; i < length; i++)
            {
                a[i] = rnd.Next(-50, 50);
            }
        }

        public void MultiplyByTwo()
        {
            for (int i = 0; i < length; i++)
            {
                a[i] *= 2;
            }
        }
    }
}
