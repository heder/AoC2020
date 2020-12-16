using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _16._2
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
            List<int[]> invalidTickets = new List<int[]>();
            List<int> invalidFields = new List<int>();

            string[] fields;

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
                    var valid = rules.Where(f => field >= f.RangeA[0] && field <= f.RangeA[1] ||
                            field >= f.RangeB[0] && field <= f.RangeB[1]).Count();

                    if (valid == 0)
                    {
                        invalidFields.Add(field);
                        invalidTickets.Add(item);
                    }
                }
            }

            var validTickets = tickets.Except(invalidTickets);

            fields = new string[myTicket.Length];

            while (rules.Count > 0)
            {
                // Loop each field
                for (int f = 0; f < myTicket.Length; f++)
                {
                    List<int> fieldNos = new List<int>();

                    // Create list of all field x
                    foreach (var t in validTickets)
                    {
                        fieldNos.Add(t[f]);
                    }

                    List<Rule> matchedRules = new List<Rule>();
                    foreach (var r in rules)
                    {
                        // Check which rule that satisfies this specific tickets field on all tickets
                        if (fieldNos.All(f => f >= r.RangeA[0] && f <= r.RangeA[1] || f >= r.RangeB[0] && f <= r.RangeB[1]) == true)
                        {
                            // Field i matches rule r
                            matchedRules.Add(r);
                        }
                    }

                    if (matchedRules.Count() == 1)
                    {
                        fields[f] = matchedRules.First().Name;
                        rules.Remove(matchedRules.First());
                    }
                }
            }

            long prod = 1;
            for (int ff = 0; ff < myTicket.Length; ff++)
            {
                if (fields[ff].StartsWith("departure"))
                {
                    prod *= myTicket[ff];
                }
            }

            Console.WriteLine(prod);
            Console.ReadKey();
        }
    }
}
