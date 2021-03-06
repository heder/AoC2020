﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _7._1
{
    class Bag
    {
        public Bag()
        {
            ContainedIn = new List<Bag>();
        }
        public string Id { get; set; }
        public List<Bag> ContainedIn { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadLines("in.txt");
            Dictionary<string, Bag> bags = new Dictionary<string, Bag>();

            foreach (var line in lines)
            {
                var split = line.Replace("bags", "bag").Split("contain");

                Bag bag;
                string bagId = split[0].Trim();
                if (bags.ContainsKey(bagId) == false)
                {
                    bag = new Bag() { Id = bagId };
                    bags.Add(bagId, bag);
                }
                else
                {
                    bag = bags[bagId];
                }

                var splittedcontains = split[1].Split(",");
                foreach (var b in splittedcontains)
                {
                    Bag bag2;
                    if (b.Trim('.', ' ') == "no other bag")
                    {
                    }
                    else
                    {
                        string bag2Id = b.Trim('.', ' ').Substring(2).Trim('.', ' ');

                        int no = Convert.ToInt32(b.Trim('.', ' ').Substring(0, 1));

                        if (bags.ContainsKey(bag2Id) == false)
                        {
                            bag2 = new Bag() { Id = bag2Id };
                            bags.Add(bag2Id, bag2);
                        }
                        else
                        {
                            bag2 = bags[bag2Id];
                        }

                        bag2.ContainedIn.Add(bag);
                    }
                }
            }

            var gold = bags["shiny gold bag"];
            Backtrack(gold);

            Console.WriteLine(colors.Distinct().Count());
            Console.ReadKey();
        }

        internal static List<string> colors = new List<string>();

        static void Backtrack(Bag b)
        {
            foreach (var bag in b.ContainedIn)
            {
                colors.Add(bag.Id);
                Backtrack(bag);
            }
        }
    }
}
