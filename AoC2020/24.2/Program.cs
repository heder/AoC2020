using System;
using System.IO;
using System.Linq;

namespace _24._2
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadLines("in.txt").ToArray();

            bool[,] grid = new bool[1000, 1000];
            int currentX;
            int currentY;

            foreach (var line in lines)
            {
                currentX = currentY = 500;

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
                            currentY -= 1;
                            if (l[i + 1] == 'e') currentX++;
                            if (l[i + 1] == 'w') currentX--;
                            i++;
                            break;

                        case 'n':
                            currentY += 1;
                            if (l[i + 1] == 'e') currentX++;
                            if (l[i + 1] == 'w') currentX--;
                            i++;
                            break;
                    }

                    i++;
                }

                grid[currentX, currentY] = !grid[currentX, currentY];
            }

            for (int i = 0; i < 100; i++)
            {
                bool[,] compareArray = new bool[grid.GetLength(0), grid.GetLength(0)];
                Array.Copy(grid, compareArray, grid.GetLength(0) * grid.GetLength(0));

                for (int y = 0; y < grid.GetLength(0); y++)
                {
                    for (int x = (y % 2 == 0) ? 0 : 1; x < grid.GetLength(0); x += 2)
                    {
                        int no = 0;

                        if (x - 2 >= 0) { if (compareArray[x - 2, y]) no++; } // west
                        if (x + 2 <= grid.GetUpperBound(0)) { if (compareArray[x + 2, y]) no++; } // right
                        if (x - 1 >= 0 && y - 1 >= 0) { if (compareArray[x - 1, y - 1]) no++; } // sw
                        if (x + 1 <= grid.GetUpperBound(0) && y - 1 >= 0) { if (compareArray[x + 1, y - 1]) no++; } // se
                        if (x - 1 >= 0 && y + 1 <= grid.GetUpperBound(0)) { if (compareArray[x - 1, y + 1]) no++; } // nw
                        if (x + 1 <= grid.GetUpperBound(0) && y + 1 <= grid.GetUpperBound(0)) { if (compareArray[x + 1, y + 1]) no++; } // ne

                        if (grid[x, y] == true && (no == 0 || no > 2))
                        {
                            grid[x, y] = false;
                        }
                        else if (grid[x, y] == false && no == 2)
                        {
                            grid[x, y] = true;
                        }
                    }

                }
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


