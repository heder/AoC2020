using System;
using System.IO;
using System.Linq;

namespace _13._1
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadLines("in.txt").ToArray();
            int time = Convert.ToInt32(lines[0]);
            var schedules = lines[1].Split(",").Where(f => f != "x").Select(f => Convert.ToInt32(f)).ToArray();


            int starttime = time;
            while (true)
            {
                for (int i = 0; i < schedules.Length; i++)
                {
                    if (time % schedules[i] == 0)
                    {
                        Console.WriteLine(schedules[i]);
                        Console.WriteLine(schedules[i] * (time - starttime));
                        Console.ReadKey();
                    }

                }

                time++;

            }
        }
    }
}
