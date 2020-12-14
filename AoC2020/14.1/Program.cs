using System;
using System.IO;
using System.Linq;

namespace _14._1
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadLines("in.txt").ToArray();

            long setMask = long.MaxValue;
            long clearMask = long.MaxValue;
            long[] ram = new long[100000];

            foreach (var line in lines)
            {
                var splitted = line.Split("=");

                if (splitted[0].Trim() == "mask")
                {
                    setMask = Convert.ToInt64(splitted[1].Trim().Replace("X", "0"), 2);
                    clearMask = Convert.ToInt64(splitted[1].Trim().Replace("X", "1"), 2);
                }

                if (splitted[0].StartsWith("mem"))
                {
                    long addr = Convert.ToInt64(splitted[0].Trim().Substring(4, splitted[0].Trim().Length - 5));
                    long val = Convert.ToInt64(splitted[1].Trim());

                    val |= setMask;
                    val &= clearMask;
                    

                    ram[addr] = val;

                }
            }

            long runningSum = 0;
            foreach (var item in ram)
            {
                runningSum += item;
            }

            Console.WriteLine(runningSum);
            Console.ReadKey();
        }
    }
}
