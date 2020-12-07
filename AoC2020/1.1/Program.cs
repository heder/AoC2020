using System;
using System.IO;
using System.Linq;

namespace _1_1
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] lines = File.ReadLines("in.txt").Select(f => Convert.ToInt32(f)).ToArray();

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines.Length; j++)
                {
                    if (lines[i] + lines[j] == 2020)
                    {
                        Console.WriteLine(lines[i] * lines[j]);
                        Console.ReadKey();
                        return;
                    }
                }
            }
        }
    }
}
