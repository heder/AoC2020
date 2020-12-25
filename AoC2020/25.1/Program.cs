using System;

namespace _25._1
{
    class Program
    {
        static void Main(string[] args)
        {
            long cardKey = 8335663;
            long doorKey = 8614349;

            //long cKeyCalc = 7;
            long i = 0;
            long value = 1;
            while (value != cardKey)
            {
                value *= 7;
                value = value % 20201227;
                i++;
            }
            long cardLoop = i;


            i = 0;
            value = 1;
            while (value != doorKey)
            {
                value *= 7;
                value = value % 20201227;
                i++;
            }
            long doorLoop = i;


            value = 1;
            for (i = 0; i < doorLoop; i++)
            {
                value *= cardKey;
                value = value % 20201227;
            }
            long key = value;

            value = 1;
            for (i = 0; i < cardLoop; i++)
            {
                value *= doorKey;
                value = value % 20201227;
            }
            long key2 = value;
        }
    }
}
