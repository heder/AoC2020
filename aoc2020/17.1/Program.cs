using System;
using System.IO;
using System.Linq;

namespace a17
{
    class Program
    {
        static bool[,,] world = new bool[100, 100, 100];
        static bool[,,] compWorld = new bool[100, 100, 100];

        static int[] xbound = new int[2];
        static int[] ybound = new int[2];
        static int[] zbound = new int[2];

        static void Main(string[] args)
        {
            var lines = File.ReadLines("in.txt").ToArray();

            int z = 50;

            xbound[0] = 49; xbound[1] = 58;
            ybound[0] = 49; ybound[1] = 58;
            zbound[0] = 49; zbound[1] = 51;

            int l = 50;
            foreach (var line in lines)
            {
                var ca = line.ToCharArray();
                for (int i = 0; i < ca.Length; i++)
                {
                    if (ca[i] == '#') world[50 + i, l, z] = true;
                }

                l++;
            }

            int iterations = 6;
            int iter = 0;

            while (iter < iterations)
            {
                DumpWorld();

                Array.Copy(world, compWorld, 100 * 100 * 100);

                for (int i = xbound[0]; i <= xbound[1]; i++)
                {
                    for (int j = ybound[0]; j <= ybound[1]; j++)
                    {
                        for (int k = zbound[0]; k <= zbound[1]; k++)
                        {
                            int nb = GetNoOfNeighbours(i, j, k);

                            if (world[i, j, k] == true && (nb == 2 || nb == 3))
                            {
                                // Cube remains active
                            }
                            else
                            {
                                // Cube becomes inactive
                                world[i, j, k] = false;
                            }

                            if (world[i, j, k] == false && nb == 3)
                            {
                                world[i, j, k] = true;
                            }
                        }
                    }
                }

                // Expand world
                xbound[0]--; xbound[1]++;
                ybound[0]--; ybound[1]++;
                zbound[0]--; zbound[1]++;

                iter++;
            }

            int c = GetActive();
            Console.WriteLine(c);
            Console.ReadKey();
        }

        static void DumpWorld()
        {
            for (int z = zbound[0]; z <= zbound[1]; z++)
            {
                for (int y = ybound[0]; y <= ybound[1]; y++)
                {
                    for (int x = xbound[0]; x <= xbound[1]; x++)
                    {
                        if (world[x, y, z] == true)
                            Console.Write("#");
                        else
                            Console.Write(".");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }

        static int GetNoOfNeighbours(int x, int y, int z)
        {
            // Check neighbours
            int sum = 0;

            // Front plane
            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if (compWorld[i, j, z - 1] == true) sum++; // Front plane

                    if ((i == x && j == y) == false) // Skip "me"
                    {
                        if (compWorld[i, j, z] == true) sum++; // Current plane
                    }

                    if (compWorld[i, j, z + 1] == true) sum++; // Back plane
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
