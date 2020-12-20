using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _20._1
{
    class Tile
    {
        internal int Id;
        internal List<string> edges = new List<string>();
        internal int matches;
    }

    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadLines("in.txt").ToArray();

            List<Tile> tiles = new List<Tile>();

            int i = 0;
            while (i < lines.Length)
            {
                if (lines[i].StartsWith("Tile"))
                {
                    var t = new Tile();
                    t.Id = Convert.ToInt32(lines[i].Substring(5, 4));
                    i++;
                    t.edges.Add(lines[i]); t.edges.Add(new String(lines[i].Reverse().ToArray()));

                    char[] left = new char[10];
                    char[] right = new char[10];
                    for (int j = 0; j < 10; j++)
                    {
                        left[j] = lines[j + i].Trim().Substring(0, 1).First();
                        right[j] = lines[j + i].Trim().Substring(9, 1).First();
                    }

                    t.edges.Add(new String(left.ToArray())); t.edges.Add(new String(left.Reverse().ToArray()));
                    t.edges.Add(new String(right.ToArray())); t.edges.Add(new String(right.Reverse().ToArray()));

                    i += 9;

                    t.edges.Add(lines[i]); t.edges.Add(new String(lines[i].Reverse().ToArray()));

                    tiles.Add(t);
                }
                i++;
            }

            // Loop tiles
            foreach (var tile in tiles)
            {
                for (int e = 0; e < tile.edges.Count; e++)
                {
                    var otherTiles = tiles.Except(new List<Tile>() { tile });
                    var other = otherTiles.Where(f => f.edges.Contains(tile.edges[e])).ToList();

                    if (other.Any() == true)
                    {
                        tile.matches++;
                    }
                }
            }

            var x = tiles.Where(f => f.matches == 4);

            long p = 1;
            foreach (var item in x)
            {
                p *= item.Id;
            }

            Console.WriteLine(p);
            Console.ReadKey();
        }
    }
}
