using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _6._1
{
    class Group
    {
        public Dictionary<char, bool> yes = new Dictionary<char, bool>();
    }

    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadLines("in.txt").ToArray();
            List<Group> groups = new List<Group>();

            int i = 0;
            while (i < lines.Length)
            {
                var g = new Group();

                while (i < lines.Length && lines[i].Trim() != "")
                {
                    foreach (var c in lines[i])
                    {
                        if (g.yes.ContainsKey(c) == false)
                        {
                            g.yes.Add(c, true);
                        }
                    }

                    i++;
                }

                groups.Add(g);
                i++;
            }

            int runningTotal = 0;
            foreach (var g in groups)
            {
                var n = g.yes.Count();
                runningTotal += n;
            }

            Console.WriteLine(runningTotal);
            Console.ReadKey();
        }
    }
}
