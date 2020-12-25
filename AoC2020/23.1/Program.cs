using System;
using System.Collections.Generic;
using System.Linq;

namespace _23._1
{
    class Program
    {
        static void Main(string[] args)
        {

            var line = "586439172".ToCharArray();

            Cup referenceCup = null;

            int cupsAdded = 0;
            Cup lastAddedCup = null;
            Cup newCup;
            for (int i = 0; i < line.Length; i++)
            {
                newCup = new Cup() { value = Convert.ToInt32(line[i].ToString()) };

                if (i == 0) 
                    referenceCup = newCup;
                else 
                    lastAddedCup.NextCup = newCup;

                lastAddedCup = newCup;
                cupsAdded++;
            }

            for (int i = 10; i <= 1000000; i++)
            {
                newCup = new Cup() { value = Convert.ToInt32(line[i].ToString()) };
                lastAddedCup = newCup;
            }
            lastAddedCup.NextCup = referenceCup;


            for (int i = 0; i <= 10000000; i++)
            {
                var n = referenceCup;
                //for (int j = 0; j < 9; j++)
                //{
                //    Console.Write($"{n.value} ");
                //    n = n.NextCup;
                //}
                //Console.WriteLine();

                // reference to removed cups
                Cup removed = referenceCup.NextCup;

                // reconnect reference cup
                referenceCup.NextCup = referenceCup.NextCup.NextCup.NextCup.NextCup;

                // find destination for removed.
                // find highest number of all lower than reference cup
                List<Tuple<int, Cup>> candidates = new List<Tuple<int, Cup>>();
                n = referenceCup.NextCup;
                for (int j = 0; j < 6; j++)
                {
                    if (n.value < referenceCup.value) candidates.Add(new Tuple<int, Cup>(n.value, n));
                    n = n.NextCup;
                }

                Cup destination;

                if (candidates.Count == 0)
                {
                    n = referenceCup.NextCup;
                    for (int j = 0; j < 6; j++)
                    {
                        candidates.Add(new Tuple<int, Cup>(n.value, n));
                        n = n.NextCup;
                    }
                    destination = candidates.First(g => g.Item1 == candidates.Max(f => f.Item1)).Item2;
                }
                else
                {
                    destination = candidates.First(g => g.Item1 == candidates.Max(f => f.Item1)).Item2;
                }

                Console.WriteLine($"Reconnecting to {destination}");

                // Reconnect at destination
                var nextatdest = destination.NextCup;
                destination.NextCup = removed;
                removed.NextCup.NextCup.NextCup = nextatdest;

                referenceCup = referenceCup.NextCup;

                if (i % 1000 == 0) Console.WriteLine(i);
            }
        }
    }







    public class Cup
    {
        public int value;
        public Cup NextCup;
    }
}
