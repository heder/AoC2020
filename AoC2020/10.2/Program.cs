using System;
using System.IO;
using System.Linq;

namespace _10._2
{
    class Program
    {
        static int[] adapters;
        static long[] runningSum;

        static void Main(string[] args)
        {
            var a = File.ReadLines("in.txt").Select(f => Convert.ToInt32(f)).ToList();
            a.Add(a.Max(f => f + 3));
            a.Add(0);
            adapters = a.OrderBy(f => f).ToArray();

            runningSum = new long[adapters.Length];
            runningSum[0] = 1;

            for (int i = 0; i < adapters.Length; i++)
            {
                // Check which previous adapters are reachable. Accumulate running sum.
                if ((i - 1) >= 0 && adapters[i - 1] >= adapters[i] - 3) runningSum[i] += runningSum[i - 1];
                if ((i - 2) >= 0 && adapters[i - 2] >= adapters[i] - 3) runningSum[i] += runningSum[i - 2];
                if ((i - 3) >= 0 && adapters[i - 3] >= adapters[i] - 3) runningSum[i] += runningSum[i - 3];
            }

            Console.WriteLine(runningSum[runningSum.Length - 1]);
            Console.ReadKey();
        }
    }
}