using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _3._1
{
    struct Coordinate
    {
        public int x;
        public int y;
    }

    class Result
    {
        public int deltaX;
        public int deltaY;
        public int result;
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Result> runs = new List<Result>();
            runs.Add(new Result() { deltaX = 1, deltaY = 1 });
            runs.Add(new Result() { deltaX = 3, deltaY = 1 });
            runs.Add(new Result() { deltaX = 5, deltaY = 1 });
            runs.Add(new Result() { deltaX = 7, deltaY = 1 });
            runs.Add(new Result() { deltaX = 1, deltaY = 2 });

            var lines = File.ReadLines("in.txt").ToArray();
            char[,] forest = new char[lines[0].Length, lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[0].Length; j++)
                {
                    forest[j, i] = lines[i][j];
                }
            }

            foreach (var run in runs)
            {
                Coordinate currentPosition = new Coordinate() { x = 0, y = 0 };

                int noOfTrees = 0;

                while (currentPosition.y < lines.Length)
                {
                    if (forest[currentPosition.x, currentPosition.y] == '#')
                    {
                        noOfTrees++;
                    }

                    currentPosition.x += run.deltaX;
                    if (currentPosition.x >= forest.GetLength(0))
                    {
                        currentPosition.x = currentPosition.x - forest.GetLength(0);
                    }

                    currentPosition.y += run.deltaY;
                }

                Console.WriteLine(noOfTrees);
                run.result = noOfTrees;
            }

            long multiplier = 1;
            foreach (var run in runs)
            {
                multiplier *= run.result;
            }

            Console.WriteLine(multiplier);
            Console.ReadKey();
        }
    }
}