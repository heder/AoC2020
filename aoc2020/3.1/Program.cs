using System;
using System.IO;
using System.Linq;

namespace _3._1
{

    struct Coordinate
    {
        public int x;
        public int y;
    }

    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadLines("in.txt").ToArray();
            char[,] forest = new char[lines[0].Length, lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[0].Length; j++)
                {
                    forest[j, i] = lines[i][j];
                }
            }

            Coordinate currentPosition = new Coordinate() { x = 0, y = 0 };

            int NoOfTrees = 0;

            while (currentPosition.y < lines.Length)
            {
                if (forest[currentPosition.x, currentPosition.y] == '#')
                {
                    NoOfTrees++;
                }

                currentPosition.x += 3;
                if (currentPosition.x >= forest.GetLength(0))
                {
                    currentPosition.x = currentPosition.x - forest.GetLength(0);
                }


                currentPosition.y += 1;
            }

            Console.WriteLine(NoOfTrees);
            Console.ReadKey();



        }
    }
}
