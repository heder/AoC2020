using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

            // Play
            while (p1.Count > 0 && p2.Count > 0)
            {
                int pc1 = p1.First();
                int pc2 = p2.First();

                p1.RemoveFirst();
                p2.RemoveFirst();

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


            int j = 1;
            int sum = 0;
            if (p1.Count > 0)
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
}
