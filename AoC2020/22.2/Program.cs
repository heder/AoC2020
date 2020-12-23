using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace _22._1
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadLines("in.txt").ToArray();
            LinkedList<int> p1 = new LinkedList<int>();
            LinkedList<int> p2 = new LinkedList<int>();

            int i = 1;
            while (lines[i].Trim() != "")
            {
                p1.AddLast(Convert.ToInt32(lines[i]));
                i++;
            }
            i += 2;
            while (i < lines.Length)
            {
                p2.AddLast(Convert.ToInt32(lines[i]));
                i++;
            }

            int winner = new P().Play(p1, p2);

            int j = 1;
            int sum = 0;
            if (winner == 1)
            {
                var res = p1.Reverse();
                foreach (var c in res)
                {
                    sum += (c * j);
                    j++;
                }
            }
            else
            {
                var res = p2.Reverse();
                foreach (var c in res)
                {
                    sum += (c * j);
                    j++;
                }
            }



            Console.WriteLine(sum);
            Console.ReadKey();
        }
    }



    class P
    {
        internal static int game = 1;

        public int Play(LinkedList<int> p1, LinkedList<int> p2)
        {
            HashSet<string> rounds = new HashSet<string>();
            StringBuilder sb = new StringBuilder();

            int round = 1;

            // Play
            while (p1.Count > 0 && p2.Count > 0)
            {
                int pc1 = p1.First();
                int pc2 = p2.First();

                p1.RemoveFirst();
                p2.RemoveFirst();

                sb.Clear();
                sb.Append(pc1.ToString());
                sb.Append(";");
                sb.Append(pc2.ToString());
                sb.Append("|");

                foreach (var item in p1)
                {
                    sb.Append(item.ToString());
                    sb.Append(":");
                }

                foreach (var item in p2)
                {
                    sb.Append(item.ToString());
                    sb.Append(">");
                }

                if (rounds.Contains(sb.ToString()) == true)
                {
                    p1.AddLast(pc1);
                    p1.AddLast(pc2);

                    // p1 wins
                    return 1;
                }
                else
                {
                    rounds.Add(sb.ToString());
                }

                if (p1.Count >= pc1 && p2.Count >= pc2)
                {
                    // Determine winner by new round

                    game++;
                    int w = Play(new LinkedList<int>(p1.Take(pc1)), new LinkedList<int>(p2.Take(pc2)));
                    if (w == 1)
                    {
                        p1.AddLast(pc1);
                        p1.AddLast(pc2);
                    }
                    else
                    {
                        p2.AddLast(pc2);
                        p2.AddLast(pc1);
                    }
                }
                else
                {
                    // Card with highest value wins
                    if (pc1 > pc2)
                    {
                        p1.AddLast(pc1);
                        p1.AddLast(pc2);
                    }
                    else
                    {
                        p2.AddLast(pc2);
                        p2.AddLast(pc1);
                    }

                }

                round++;
            }

            if (p1.Count > 0)
            {
                return 1;
            }
            else
            {
                return 2;
            }

        }
    }
}

