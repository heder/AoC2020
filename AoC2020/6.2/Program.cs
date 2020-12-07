using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _6._1
{
    class Group
    {
        public Dictionary<char, bool> uniqueYes = new Dictionary<char, bool>();
        public List<string> answers = new List<string>();
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
                    g.answers.Add(lines[i]);

                    foreach (var c in lines[i])
                    {
                        if (g.uniqueYes.ContainsKey(c) == false)
                        {
                            g.uniqueYes.Add(c, true);
                        }
                    }

                    i++;
                }

                groups.Add(g);
                i++;
            }


            int runningTotal = 0;
            int noExistInAll = 0;
            foreach (var g in groups)
            {
                noExistInAll = 0;
                foreach (var item in g.uniqueYes)
                {
                    if (g.answers.All(f => f.Contains(item.Key)))
                    {
                        noExistInAll++;
                    }
                }

                runningTotal += noExistInAll;
            }

            Console.WriteLine(runningTotal);
            Console.ReadKey();
        }
    }
}
