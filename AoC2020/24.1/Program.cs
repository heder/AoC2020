using System;
using System.IO;
using System.Linq;

namespace _24._1
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadLines("in.txt").ToArray();
            bool[,] grid = new bool[100, 100];
            int currentX;
            int currentY;

            foreach (var line in lines)
            {
                currentX = currentY = 50;
                var l = line.ToCharArray();
                int i = 0;
                while (i < line.Length)
                {
                    switch (l[i])
                    {
                        case 'e':
                            currentX += 2;
                            break;

                        case 'w':
                            currentX -= 2;
                            break;

                        case 's':
                            currentY -= 2;
                            if (l[i + 1] == 'e') currentX++;
                            if (l[i + 1] == 'w') currentX--;
                            i++;
                            break;

                        case 'n':
                            currentY += 2;
                            if (l[i + 1] == 'e') currentX++;
                            if (l[i + 1] == 'w') currentX--;
                            i++;
                            break;
                    }

                    i++;
                }

                grid[currentX, currentY] = !grid[currentX, currentY];
            }

            int sum = 0;
            foreach (var item in grid)
            {
                if (item == true) sum++;
            }

            Console.WriteLine(sum);
            Console.ReadKey();
        }
    }
}


