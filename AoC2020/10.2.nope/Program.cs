using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _10._1
{
    struct Adapter
    {
        public byte Rating;
        //public int NoInChain;
        //public int acc1 = 0;
        //public int acc3 = 0;
        public byte SearchPointer;

        //public int[] RemainingAdapters;
        public Adapter[] AdapterConnections;
        //public Adapter ParentAdapter;
    }


    class Program
    {
        static byte[] adapters;

        static byte deviceRating;
        static int paths = 0;
        static void Main(string[] args)
        {
            var a = File.ReadLines("in.txt").Select(f => Convert.ToByte(f)).OrderBy(f => f).ToList();
            deviceRating = Convert.ToByte(a.Max(f => f + 3));
            a.Add(deviceRating);
            adapters = a.ToArray();

            Adapter startingAdapter = new Adapter() { SearchPointer = 0, Rating = 0 };

            BuildAdapterChains(startingAdapter);

            Console.WriteLine(paths);
            Console.ReadKey();
        }

        private static byte[] xxx = new byte[3];
        private static byte xxx_i;

        private static void BuildAdapterChains(Adapter curr)
        {
            // Check first three adapters
            if (adapters[curr.SearchPointer + 0] <= curr.Rating + 3)
            {
                xxx[0] = adapters[curr.SearchPointer + 0];
                xxx_i = 1;
            }
            if (curr.SearchPointer + 1 < adapters.Length && adapters[curr.SearchPointer + 1] <= curr.Rating + 3)
            {
                xxx[1] = adapters[curr.SearchPointer + 1];
                xxx_i = 2;
            }
            if (curr.SearchPointer + 2 < adapters.Length && adapters[curr.SearchPointer + 2] <= curr.Rating + 3)
            {
                xxx[2] = adapters[curr.SearchPointer + 2];
                xxx_i = 3;
            }

            //var fits = currentAdapter.RemainingAdapters.Where(f => f >= currentAdapter.Rating + 1 && f <= currentAdapter.Rating + 3);

            curr.AdapterConnections = new Adapter[xxx_i];

            for (byte i = 0; i < xxx_i; i++)
            {
                // Console.WriteLine($"Adding {xxx[i]} to {curr.Rating}");

                var a = new Adapter() { Rating = xxx[i] }; // /*NoInChain = currentAdapter.NoInChain + 1,  ParentAdapter = currentAdapter */};


                //if (adapter - currentAdapter.Rating == 3) { a.acc3 = currentAdapter.acc3 + 1; a.acc1 = currentAdapter.acc1; };
                //if (adapter - currentAdapter.Rating == 1) { a.acc1 = currentAdapter.acc1 + 1; a.acc3 = currentAdapter.acc3; };
                
                a.SearchPointer += Convert.ToByte(curr.SearchPointer + i + Convert.ToByte(1));
                //curr.AdapterConnections.Add(a);

                curr.AdapterConnections[i] = a;

                //a.RemainingAdapters = new int[((curr.RemainingAdapters.Length - i) - 1)];
                //Array.Copy(curr.RemainingAdapters, i + 1, a.RemainingAdapters, 0, (curr.RemainingAdapters.Length - i) - 1);

                if (xxx[i] == deviceRating)
                {
                    //Console.WriteLine(a.RemainingAdapters.Count());

                    // All adapters used
                    //Console.WriteLine(a.acc1 * a.acc3);
                    paths++;
                    //if (paths % 1000000 == 1)
                    //{
                    //    Console.WriteLine(paths);
                    //}
                }
            }

            foreach (var adapter in curr.AdapterConnections)
            {
                if (adapter.Rating != deviceRating)
                {
                    BuildAdapterChains(adapter);
                }
            }
        }
    }
}

