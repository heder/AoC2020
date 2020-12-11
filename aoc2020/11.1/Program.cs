using System;
using System.IO;
using System.Linq;

namespace _11._1
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

            //DumpMatrix();
            int changes = 0;

            while (true)
            {
                changes = 0;
                for (int y = 0; y < seats.GetLength(1); y++)
                {
                    for (int x = 0; x < seats.GetLength(0); x++)
                    {
                        surroundCount = GetSurrounding(x, y);

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
                            if (surroundCount >= 4)
                            {
                                seats[x, y] = 'L';
                                changes++;
                            }
                        }
                    }
                }

                //DumpMatrix();
                //int n = CountSeats();

                compseats = seats.Clone() as char[,];

                if (changes == 0)
                {
                    int n = CountOccupied();
                    Console.WriteLine(n);
                    Console.ReadKey();
                }

                //Console.ReadKey();
            }

            //Coordinate currentPosition = new Coordinate() { x = 0, y = 0 };

            //int NoOfTrees = 0;

            //while (currentPosition.y < lines.Length)
            //{
            //    if (seats[currentPosition.x, currentPosition.y] == '#')
            //    {
            //        NoOfTrees++;
            //    }

            //    currentPosition.x += 3;
            //    if (currentPosition.x >= seats.GetLength(0))
            //    {
            //        currentPosition.x = currentPosition.x - seats.GetLength(0);
            //    }


            //    currentPosition.y += 1;
            //}

            //Console.WriteLine(NoOfTrees);
            Console.ReadKey();
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
            for (int y = 0; y < seats.GetLength(1); y++)
            {
                for (int x = 0; x < seats.GetLength(0); x++)
                {
                    if (seats[x, y] == '#')
                    {
                        n++;
                    }
                }
            }

            return n;
        }


        static int GetSurrounding(int x, int y)
        {
            int c = 0;

            if (x >= 1 && y >= 1 && compseats[x - 1, y - 1] == '#') c++; // TOPLEFT
            if (y >= 1 && compseats[x, y - 1] == '#') c++; // TOP
            if (y >= 1 && x < compseats.GetUpperBound(0) && compseats[x + 1, y - 1] == '#') c++; // TOPRIGHT

            if (x >= 1 && compseats[x -1 , y] == '#') c++; // LEFT
            if (x < compseats.GetUpperBound(0) && compseats[x + 1, y] == '#') c++; // RIGHT

            if (y < compseats.GetUpperBound(1) && x >= 1 && compseats[x - 1, y + 1] == '#') c++; // BOTTOMLEFT
            if (y < compseats.GetUpperBound(1) && compseats[x, y + 1] == '#') c++; // BOTTOM
            if (y < compseats.GetUpperBound(1) && x < compseats.GetUpperBound(0) &&  compseats[x + 1, y + 1] == '#') c++; // BOTTOMRIGHT



            return c;
        }


        static int GetSafeX(int x)
        {
            if (x < 0) return 0;
            else if (x > compseats.GetUpperBound(0)) return compseats.GetUpperBound(0);
            else return x;
        }

        static int GetSafeY(int y)
        {
            if (y < 0) return 0;
            else if (y > compseats.GetUpperBound(1)) return compseats.GetUpperBound(1);
            else return y;
        }
    }
}
