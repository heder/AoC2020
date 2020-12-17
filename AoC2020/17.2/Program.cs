using System;
using System.IO;
using System.Linq;

namespace b17
{
    class Program
    {
        static bool[,,,] world = new bool[100, 100, 100, 100];
        static bool[,,,] compWorld = new bool[100, 100, 100, 100];

        static int[] xbound = new int[2];
        static int[] ybound = new int[2];
        static int[] zbound = new int[2];
        static int[] wbound = new int[2];

        static void Main(string[] args)
        {
            var lines = File.ReadLines("in.txt").ToArray();

            xbound[0] = 49; xbound[1] = 58;
            ybound[0] = 49; ybound[1] = 58;
            zbound[0] = 49; zbound[1] = 51;
            wbound[0] = 49; wbound[1] = 51;

            int li = 50;
            foreach (var line in lines)
            {
                var ca = line.ToCharArray();
                for (int i = 0; i < ca.Length; i++)
                {
                    if (ca[i] == '#') world[50 + i, li, 50, 50] = true;
                }

                li++;
            }

            int iterations = 6;
            int iter = 0;

            while (iter < iterations)
            {
                DumpWorld();

                Array.Copy(world, compWorld, 100 * 100 * 100 * 100);

                for (int i = xbound[0]; i <= xbound[1]; i++)
                {
                    for (int j = ybound[0]; j <= ybound[1]; j++)
                    {
                        for (int k = zbound[0]; k <= zbound[1]; k++)
                        {
                            for (int l = wbound[0]; l <= wbound[1]; l++)
                            {
                                int nb = GetNoOfNeighbours(i, j, k, l);

                                if (world[i, j, k, l] == true && (nb == 2 || nb == 3))
                                {
                                    // Cube remains active
                                }
                                else
                                {
                                    // Else, cube becomes inactive
                                    world[i, j, k, l] = false;
                                }

                                if (world[i, j, k, l] == false && nb == 3)
                                {
                                    world[i, j, k, l] = true;
                                }
                            }
                        }
                    }
                }

                // Expand world
                xbound[0]--; xbound[1]++;
                ybound[0]--; ybound[1]++;
                zbound[0]--; zbound[1]++;
                wbound[0]--; wbound[1]++;

                iter++;
            }

            int c = GetActive();
            Console.WriteLine(c);
            Console.ReadKey();
        }

        static void DumpWorld()
        {
            for (int z = zbound[0] + 1; z <= zbound[1] - 1; z++)
            {
                for (int w = wbound[0] + 1; w <= wbound[1] - 1; w++)
                {
                    Console.WriteLine($"z={z}, w={w}");

                    for (int y = ybound[0] + 1; y <= ybound[1] - 1; y++)
                    {
                        for (int x = xbound[0] + 1; x <= xbound[1] - 1; x++)
                        {
                            if (world[x, y, z, w] == true)
                                Console.Write("#");
                            else
                                Console.Write(".");
                        }
                        Console.WriteLine();
                    }

                    Console.WriteLine("\n-------\n");
                }

                Console.WriteLine("\n****\n");
            }
        }

        static int GetNoOfNeighbours(int x, int y, int z, int w)
        {
            int sum = 0;

            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    for (int k = z - 1; k <= z + 1; k++)
                    {
                        for (int l = w - 1; l <= w + 1; l++)
                        {
                            if ((i == x && j == y && k == z && l == w) == false) // Skip "me"
                            {
                                if (compWorld[i, j, k, l] == true) sum++;
                            }
                        }
                    }
                }
            }

            return sum;
        }

        static int GetActive()
        {
            int c = 0;
            foreach (var item in world)
            {
                if (item == true) c++;
            }

            return c;
        }
    }
}
