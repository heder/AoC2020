using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _15._1
{
    class Program
    {
        static void Main(string[] args)
        {
            var start = File.ReadLines("in.txt").First().Split(",").Select(f => Convert.ToInt32(f)).ToArray();
            int[] numbers = new int[2020];

            for (int j = 0; j < numbers.Length; j++)
            {
                numbers[j] = -1;
            }

            int i;
            for (i = 0; i < start.Length; i++)
            {
                numbers[i] = start[i];
            }

            bool isLastUnique = true;
            int lastNum = numbers[i - 1];
            while (i < 2020)
            {
                if (isLastUnique == true)
                {
                    if (numbers.Contains(0))
                    {
                        isLastUnique = false;
                    }
                    else
                    {
                        isLastUnique = true;
                    }

                    numbers[i] = lastNum = 0;
                }
                else
                {
                    var last2 = numbers
                        .Select((v, i) => new { v, i })
                        .Where(f => f.v == lastNum)
                        .TakeLast(2)
                        .ToArray();

                    var diff = last2[1].i - last2[0].i;

                    if (numbers.Contains(diff))
                    {
                        isLastUnique = false;
                    }
                    else
                    {
                        isLastUnique = true;
                    }
                    
                    numbers[i] = lastNum = diff;
                }

                i++;
            }

            Console.WriteLine(numbers[2019]);
            Console.ReadKey();
        }
    }
}
