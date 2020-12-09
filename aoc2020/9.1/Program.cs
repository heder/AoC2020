using System;
using System.IO;
using System.Linq;

namespace _8._2
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadLines("in.txt");
            var numbers = lines.Select(f => Convert.ToInt64(f)).ToArray();
            int preambleSize = 25;

            for (int i = preambleSize; i < numbers.Length; i++)
            {
                if (FindSum(numbers, numbers[i], i - preambleSize, i) == false)
                {
                    Console.WriteLine(numbers[i]);
                    Console.ReadKey();
                }
            }
        }

        static bool FindSum(long[] a, long s, long from, long to)
        {
            for (long i = from; i < to; i++)
            {
                for (long j = from; j < to; j++)
                {
                    if (i != j)
                    {
                        if (a[i] != a[j])
                        {
                            if (a[i] + a[j] == s)
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }
    }
}
