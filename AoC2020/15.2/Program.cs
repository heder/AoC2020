using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace b15
{
    class Positions
    {
        public long[] q = new long[2];

        public Positions(long v)
        {
            q[0] = v;
            q[1] = -1;
        }

        public void Enqueue(long v)
        {
            q[1] = q[0];
            q[0] = v;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            DateTime s = DateTime.Now;

            var start = File.ReadLines("in.txt").First().Split(",").Select(f => Convert.ToInt64(f)).ToArray();
            long iterations = 30000000;

            Dictionary<long, Positions> numbers = new Dictionary<long, Positions>();

            int i;
            for (i = 0; i < start.Length; i++)
            {
                numbers.Add(start[i], new Positions(i));
            }

            long diff = -1;
            bool isLastUnique = true;
            long lastNum = numbers.Last().Key;

            while (i < iterations)
            {
                if (isLastUnique == true)
                {
                    if (numbers.ContainsKey(0))
                    {
                        isLastUnique = false;
                        numbers[0].Enqueue(i);
                    }
                    else
                    {
                        isLastUnique = true;
                        numbers.Add(0, new Positions(i));
                    }

                    lastNum = 0;
                    
                }
                else
                {
                    diff = numbers[lastNum].q[0] - numbers[lastNum].q[1];

                    if (numbers.ContainsKey(diff))
                    {
                        isLastUnique = false;
                        numbers[diff].Enqueue(i);
                    }
                    else
                    {
                        isLastUnique = true;
                        numbers.Add(diff, new Positions(i));
                    }

                    lastNum = diff;
                }

                i++;

                if (i % 1000000 == 0)
                {
                    Console.WriteLine(i);
                }
            }

            Console.WriteLine(DateTime.Now - s);
            Console.WriteLine(lastNum);
            Console.ReadKey();
        }
    }
}
