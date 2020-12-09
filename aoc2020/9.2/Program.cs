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

            long min = long.MaxValue;
            long max = long.MinValue;

            long at;
            for (int i = 0; i < numbers.Length; i++)
            {
                Console.WriteLine($"Summing from {i}");
                at = SearchSum(numbers, i, 167829540);
                if (at > 0)
                {
                    Console.WriteLine($"Sum matched from {i} ({numbers[i]}) to {at} ({numbers[at]}) sum {numbers[i] + numbers[at]})");

                    for (int j = i; j < at; j++)
                    {
                        min = Math.Min(numbers[j], min);
                        max = Math.Max(numbers[j], max);

                        Console.WriteLine($"min {min} max {max} sum {min + max}");
                    }

                    Console.ReadKey();
                }
            }
        }

        static long SearchSum(long[] a, long startpos, long searchee)
        {
            long sum = 0;
            for (long i = startpos; i < a.Length; i++)
            {
                sum += a[i];

                if (sum == searchee)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}

