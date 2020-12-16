using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _16._1
{
    class Rule
    {
        public string Name;
        public int[] RangeA = new int[2];
        public int[] RangeB = new int[2];
    }

    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadLines("in.txt").ToArray();

            List<Rule> rules = new List<Rule>();
            int[] myTicket;
            List<int[]> tickets = new List<int[]>();

            List<int> invalidFields = new List<int>();

            int i = 0;
            while (lines[i] != "")
            {
                // Rules
                var rule = new Rule();

                var splittedA = lines[i].Split(":");
                rule.Name = splittedA[0].Trim();

                var splittedB = splittedA[1].Split("or");
                var splittedB1 = splittedB[0].Split("-");
                var splittedB2 = splittedB[1].Split("-");

                rule.RangeA[0] = Convert.ToInt32(splittedB1[0].Trim());
                rule.RangeA[1] = Convert.ToInt32(splittedB1[1].Trim());

                rule.RangeB[0] = Convert.ToInt32(splittedB2[0].Trim());
                rule.RangeB[1] = Convert.ToInt32(splittedB2[1].Trim());

                rules.Add(rule);

                i++;
            }

            i += 2;

            // My ticket
            myTicket = lines[i].Split(",").Select(f => Convert.ToInt32(f)).ToArray();

            i += 3;

            while (i < lines.Length)
            {
                // Tickets

                tickets.Add(lines[i].Split(",").Select(f => Convert.ToInt32(f)).ToArray());
                i++;
            }



            foreach (var item in tickets)
            {
                foreach (var field in item)
                {
                    int noValid = 0;
                    int noInvalid = 0;
                    foreach (var rule in rules)
                    {
                        if (field >= rule.RangeA[0] && field <= rule.RangeA[1] ||
                            field >= rule.RangeB[0] && field <= rule.RangeB[1])
                        {
                            // Valid
                            noValid++;
                        }
                        else
                        {
                            // Not valid
                            noInvalid++;
                        }
                    }

                    if (noValid > 0)
                    {
                        // We're fine
                    }
                    else
                    {
                        invalidFields.Add(field);
                    }

                }
            }


            Console.WriteLine(invalidFields.Sum());
            Console.ReadKey();
        }
    }
}
