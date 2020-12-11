using System;
using System.IO;
using System.Linq;

namespace _11._2
{
    class Program
    {
        static char[,] seats;
        static char[,] compseats;

        static void Main(string[] args)
        {
            var lines = File.ReadLines("in.txt").ToArray();
            seats = new char[lines[0].Length, lines.Length];
            compseats = new char[lines[0].Length, lines.Length];


            int surroundCount;

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[0].Length; j++)
                {
                    seats[j, i] = lines[i][j];
                }
            }

            compseats = seats.Clone() as char[,];

            // DumpMatrix();
            int changes = 0;

            while (true)
            {
                changes = 0;
                for (int y = 0; y < seats.GetLength(1); y++)
                {
                    for (int x = 0; x < seats.GetLength(0); x++)
                    {
                        surroundCount = GetVisible(x, y);

                        if (seats[x, y] == 'L')
                        {
                            if (surroundCount == 0)
                            {
                                seats[x, y] = '#';
                                changes++;
                            }
                        }

                        if (seats[x, y] == '#')
                        {
                            if (surroundCount >= 5)
                            {
                                seats[x, y] = 'L';
                                changes++;
                            }
                        }
                    }
                }

                compseats = seats.Clone() as char[,];

                if (changes == 0)
                {
                    int n = CountOccupied();
                    Console.WriteLine(n);
                    Console.ReadKey();
                }
            }
        }


        static void DumpMatrix()
        {
            for (int y = 0; y < seats.GetLength(1); y++)
            {
                for (int x = 0; x < seats.GetLength(0); x++)
                {
                    Console.Write(seats[x, y]);
                }
                Console.Write(Environment.NewLine);
            }
            Console.Write(Environment.NewLine);
        }

        static int CountOccupied()
        {
            int n = 0;
            foreach (var item in seats)
            {
                if (item == '#') n++;
            }

            return n;
        }


        static int GetVisible(int x, int y)
        {
            int c = 0;
            int x1;
            int y1;

            // UP
            x1 = x; y1 = y;
            while (y1 > 0)
            {
                y1--;
                if (compseats[x1, y1] == 'L') { break; }
                if (compseats[x1, y1] == '#') { c++; break; }
            }

            // DOWN
            x1 = x; y1 = y;
            while (y1 < compseats.GetUpperBound(1))
            {
                y1++;
                if (compseats[x1, y1] == 'L') { break; }
                if (compseats[x1, y1] == '#') { c++; break; }
            }

            // LEFT
            x1 = x; y1 = y;
            while (x1 > 0)
            {
                x1--;
                if (compseats[x1, y1] == 'L') { break; }
                if (compseats[x1, y1] == '#') { c++; break; }
            }

            // RIGHT
            x1 = x; y1 = y;
            while (x1 < compseats.GetUpperBound(0))
            {
                x1++;
                if (compseats[x1, y1] == 'L') { break; }
                if (compseats[x1, y1] == '#') { c++; break; }
            }

            // UPLEFT
            x1 = x; y1 = y;
            while (x1 > 0 && y1 > 0)
            {
                x1--;
                y1--;
                if (compseats[x1, y1] == 'L') { break; }
                if (compseats[x1, y1] == '#') { c++; break; }
            }

            // UPRIGHT
            x1 = x; y1 = y;
            while (x1 < compseats.GetUpperBound(0) && y1 > 0)
            {
                x1++;
                y1--;
                if (compseats[x1, y1] == 'L') { break; }
                if (compseats[x1, y1] == '#') { c++; break; }
            }

            // DOWNRIGHT
            x1 = x; y1 = y;
            while (x1 < compseats.GetUpperBound(0) && y1 < compseats.GetUpperBound(1))
            {
                x1++;
                y1++;
                if (compseats[x1, y1] == 'L') { break; }
                if (compseats[x1, y1] == '#') { c++; break; }
            }

            // DOWNLEFT
            x1 = x; y1 = y;
            while (x1 > 0 && y1 < compseats.GetUpperBound(1))
            {
                x1--;
                y1++;
                if (compseats[x1, y1] == 'L') { break; }
                if (compseats[x1, y1] == '#') { c++; break; }
            }

            return c;
        }
    }
}
