using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace _13._2
{
    class Program
    {
        public static int highestBus;
        public static int[] schedules;
        public static int highestIndex;
        public static DateTime start;

        static void Main(string[] args)
        {
            var data = File.ReadLines("in.txt").ToArray();
            schedules = data[0].Replace("x", "-1").Split(",").Select(f => Convert.ToInt32(f)).ToArray();

            highestBus = schedules.Max();
            highestIndex = schedules.FirstOrDefault(f => f == highestBus);

            int threads = 24;
            start = DateTime.Now;

            for (int t = 1; t <= threads; t++)
            {
                Thread thread = new Thread(new ParameterizedThreadStart(Program.InterleavedSearch));
                thread.Start(new ThreadStart() { threadNo = t, noOfThreads = threads });
            }
        }

        public static void InterleavedSearch(object _t)
        {
            var t = (ThreadStart)_t;
            long delta = Program.highestBus * t.noOfThreads;
            long currentTime = Program.highestBus * t.threadNo;
            long startSearchAtTime;

            Console.WriteLine($"thread {t.threadNo} / {t.noOfThreads} starting at {currentTime}, skipping {delta}");

            while (true)
            {
                startSearchAtTime = currentTime - Program.highestIndex;

                for (int i = 0; i < Program.schedules.Length; i++)
                {
                    if (Program.schedules[i] != -1)
                    {
                        if ((startSearchAtTime + i) % Program.schedules[i] != 0) 
                            break;

                        if (i == Program.schedules.Length - 1)
                        {
                            Console.WriteLine($"thread {t.threadNo} FOUND {startSearchAtTime} in {DateTime.Now - Program.start}");
                            Console.ReadKey();
                        }
                    }
                }

                currentTime += delta;
            }
        }
    }

    public class ThreadStart
    {
        public int threadNo;
        public int noOfThreads;
    }
}
