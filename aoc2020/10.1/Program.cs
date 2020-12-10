using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _10._1
{
    class Adapter
    {
        public int Rating;
        public int NoInChain;
        public int acc1 = 0;
        public int acc3 = 0;

        public LinkedList<int> RemainingAdapters;
        public List<Adapter> AdapterConnections = new List<Adapter>();
        public Adapter ParentAdapter;
    }



    class Program
    {
        static int noOfAdapters;
        static int deviceRating;
        static void Main(string[] args)
        {
            var ratings = File.ReadLines("in.txt").Select(f => Convert.ToInt32(f));
            deviceRating = ratings.Max(f => f + 3);
            
            LinkedList<int> initialAdapters = new LinkedList<int>(ratings);

            initialAdapters.AddLast(deviceRating);

            noOfAdapters = initialAdapters.Count();
            
            Adapter startingAdapter = new Adapter() { Rating = 0, NoInChain = 0, RemainingAdapters = initialAdapters };

            BuildAdapterChains(startingAdapter);
            FindCompleteAdapterChain(startingAdapter);
        }

        private static void BuildAdapterChains(Adapter currentAdapter)
        {
            // Find adapters that fits
            var fits = currentAdapter.RemainingAdapters.Where(f => f >= currentAdapter.Rating + 1 && f <= currentAdapter.Rating + 3).OrderBy(f => f);

            foreach (var adapter in fits)
            {
                Console.WriteLine($"Adding {adapter} to {currentAdapter.Rating}");

                var a = new Adapter() { Rating = adapter, NoInChain = currentAdapter.NoInChain + 1, ParentAdapter = currentAdapter };

                if (adapter - currentAdapter.Rating == 3) { a.acc3 = currentAdapter.acc3 + 1; a.acc1 = currentAdapter.acc1; };
                if (adapter - currentAdapter.Rating == 1) { a.acc1 = currentAdapter.acc1 + 1; a.acc3 = currentAdapter.acc3; };

                currentAdapter.AdapterConnections.Add(a);

                a.RemainingAdapters = new LinkedList<int>(currentAdapter.RemainingAdapters);
                a.RemainingAdapters.Remove(adapter);

                if (adapter == deviceRating)
                {
                    // All adapters used
                    Console.WriteLine(a.acc1 * a.acc3);
                    Console.ReadKey();
                }
            }

            foreach (var adapter in currentAdapter.AdapterConnections)
            {
                BuildAdapterChains(adapter);
            }
        }


        private static void FindCompleteAdapterChain(Adapter startingAdapter)
        {
            
        }



    }
}

