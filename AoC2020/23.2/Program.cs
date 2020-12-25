using System;
using System.Collections.Generic;

namespace _23._1
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = "586439172";

            var map = new Dictionary<int, Cup>();
            var chars = input.ToCharArray();
            var referenceCup = new Cup { value = int.Parse(chars[0].ToString()) };
            var start = referenceCup;
            map.Add(referenceCup.value, referenceCup);

            //var current = start;
            Cup oneCup = null;
            
            for (int i = 1; i < chars.Length; i++)
            {
                var cup = new Cup { value = int.Parse(chars[i].ToString()) };
                map.Add(cup.value, cup);

                if (cup.value == 1) oneCup = cup;
                referenceCup.NextCup = cup;
                referenceCup = cup;
            }

            var value = 10;
            for (int i = 0; i < 1000000 - input.Length; i++)
            {
                var cup = new Cup { value = value };
                map.Add(cup.value, cup);
                referenceCup.NextCup = cup;
                referenceCup = cup;
                value++;
            }
            referenceCup.NextCup = start;
            referenceCup = start;

            for (int i = 0; i < 10000000; i++)
            {
                var removed = referenceCup.NextCup;
                var removedEnd = removed.NextCup.NextCup;

                var removedValues = new HashSet<int>
                {
                    removed.value,
                    removed.NextCup.value,
                    removed.NextCup.NextCup.value
                };
                referenceCup.NextCup = removedEnd.NextCup;

                var refValue = referenceCup.value;
                do
                {
                    refValue--;
                    refValue = refValue == 0 ? 1000000 : refValue;
                }
                while (removedValues.Contains(refValue));

                var destination = map[refValue];

                var tempNext = destination.NextCup;
                destination.NextCup = removed;
                removedEnd.NextCup = tempNext;

                referenceCup = referenceCup.NextCup;
            }

            Console.WriteLine(Convert.ToInt64(oneCup.NextCup.value) * Convert.ToInt64(oneCup.NextCup.NextCup.value));
            Console.ReadLine();
        }
    }

    class Cup
    {
        public int value { get; set; }
        public Cup NextCup { get; set; }
    }
}