using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace _20._1
{

    internal enum Pos
    {
        Up = 0,
        Down = 1,
        Left = 2,
        Right = 3
    }

    class Edge
    {
        internal Pos OriginalPos;
        internal bool Flipped;
        internal string edge;
    }

    class Tile
    {
        internal int Id;
        internal List<Edge> edges = new List<Edge>();
        internal int matches;
        internal char[,] b = new char[10, 10];



        public void RotateArrayClockwise()
        {
            int width;
            int height;

            width = b.GetUpperBound(0) + 1;
            height = b.GetUpperBound(1) + 1;
            var b2 = new char[10, 10];

            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    int newRow;
                    int newCol;

                    newRow = col;
                    newCol = height - (row + 1);

                    b2[newCol, newRow] = b[col, row];
                }
            }

            Array.Copy(b2, b, 10 * 10);
        }


        public void FlipArrayHorizontally()
        {
            int width;
            int height;

            width = b.GetUpperBound(0) + 1;
            height = b.GetUpperBound(1) + 1;
            var b2 = new char[10, 10];

            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    int newRow;
                    int newCol;

                    newRow = row;
                    newCol = 9 - col;

                    b2[newCol, newRow] = b[col, row];
                }
            }

            Array.Copy(b2, b, 10 * 10);
        }


        public string GetLowerEdge()
        {
            char[] c = new char[10];
            for (int i = 0; i < 10; i++)
            {

                c[i] = b[i, 9];
            }

            return new string(c);
        }

        public string GetRightEdge()
        {
            char[] c = new char[10];
            for (int i = 0; i < 10; i++)
            {
                c[i] = b[9, i];
            }

            return new string(c);
        }
    }



    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadLines("in.txt").ToArray();

            List<Tile> tiles = new List<Tile>();
            Tile[,] puzzle = new Tile[12, 12];

            int i = 0;
            while (i < lines.Length)
            {
                if (lines[i].StartsWith("Tile"))
                {
                    var t = new Tile();

                    t.Id = Convert.ToInt32(lines[i].Substring(5, 4));
                    i++;
                    t.edges.Add(new Edge() { edge = lines[i], OriginalPos = Pos.Up, Flipped = false });
                    t.edges.Add(new Edge() { edge = new String(lines[i].Reverse().ToArray()), OriginalPos = Pos.Up, Flipped = true });

                    char[] left = new char[10];
                    char[] right = new char[10];
                    for (int j = 0; j < 10; j++)
                    {
                        left[j] = lines[j + i].Trim().Substring(0, 1).First();
                        right[j] = lines[j + i].Trim().Substring(9, 1).First();

                        for (int Z = 0; Z < lines[i + j].Length; Z++)
                        {
                            t.b[Z, j] = lines[i + j][Z];
                        }
                    }

                    //DumpTile(t);

                    t.edges.Add(new Edge() { edge = new String(left.ToArray()), OriginalPos = Pos.Left, Flipped = false });
                    t.edges.Add(new Edge() { edge = new String(left.Reverse().ToArray()), OriginalPos = Pos.Left, Flipped = true });


                    t.edges.Add(new Edge() { edge = new String(right.ToArray()), OriginalPos = Pos.Right, Flipped = false });
                    t.edges.Add(new Edge() { edge = new String(right.Reverse().ToArray()), OriginalPos = Pos.Right, Flipped = true });

                    i += 9;

                    t.edges.Add(new Edge() { edge = lines[i], OriginalPos = Pos.Down, Flipped = false });
                    t.edges.Add(new Edge() { edge = new String(lines[i].Reverse().ToArray()), OriginalPos = Pos.Down, Flipped = true });

                    tiles.Add(t);
                }
                i++;
            }


            List<Tile> edges = new List<Tile>();

            // Loop tiles
            foreach (var tile in tiles)
            {
                for (int e = 0; e < tile.edges.Count; e++)
                {
                    // Find tile with matching edge
                    var other = tiles.Except(new List<Tile>() { tile }).Where(f => f.edges.Select(g => g.edge).ToList().Contains(tile.edges[e].edge)).ToList();

                    if (other.Any() == true)
                    {
                        // Edge found with no match, tile is edge
                        tile.matches++;

                        if (edges.Contains(tile) == false)
                            edges.Add(tile);
                    }

                }
            }

            var corners = tiles.Where(f => f.matches == 4);

            var x = corners.First();

            // Place random tile upper left
            puzzle[0, 0] = x;

            while (true)
            {
                var s = x.GetLowerEdge();
                var r = x.GetRightEdge();

                var t = tiles.Except(new List<Tile>() { x }).FirstOrDefault(f => f.edges.Select(g => g.edge).ToList().Contains(s));
                var t1 = tiles.Except(new List<Tile>() { x }).FirstOrDefault(f => f.edges.Select(g => g.edge).ToList().Contains(r));

                if (t == null || t1 == null)
                {
                    x.RotateArrayClockwise();
                }
                else
                {
                    break;
                }

            }

            DumpTile(x);

            // Loop, find and flip tiles for left edge
            for (int k = 0; k < puzzle.GetLength(0) - 1; k++)
            {
                // Get lower edge
                var searchedge = puzzle[0, k].GetLowerEdge();

                // Find tile with matching edge
                var t = tiles.Except(new List<Tile>() { puzzle[0, k] }).Single(f => f.edges.Select(g => g.edge).ToList().Contains(searchedge));

                // Find which edge type that fits
                var e = t.edges.Single(e => e.edge == searchedge);

                switch (e.OriginalPos)
                {
                    case Pos.Up:
                        if (e.Flipped == true)
                            t.FlipArrayHorizontally();

                        break;

                    case Pos.Down:
                        // Rotate 180
                        t.RotateArrayClockwise();
                        t.RotateArrayClockwise();

                        if (e.Flipped != true)
                            t.FlipArrayHorizontally();
                        break;
                    case Pos.Left:
                        // Rotate 90
                        t.RotateArrayClockwise();

                        if (e.Flipped != true)
                            t.FlipArrayHorizontally();

                        break;

                    case Pos.Right:
                        // Rotate 270
                        t.RotateArrayClockwise();
                        t.RotateArrayClockwise();
                        t.RotateArrayClockwise();

                        if (e.Flipped == true)
                            t.FlipArrayHorizontally();
                        break;
                    default:
                        break;
                }

                DumpTile(t);


                puzzle[0, k + 1] = t;
            }


            // Construct line by line
            for (int yy = 0; yy < puzzle.GetLength(0); yy++)
            {

                for (int xx = 0; xx < puzzle.GetLength(0) - 1; xx++)
                {
                    // Get right edge
                    var searchedge = puzzle[xx, yy].GetRightEdge();

                    // Find tile with matching edge
                    var t = tiles.Except(new List<Tile>() { puzzle[xx, yy] }).Single(f => f.edges.Select(g => g.edge).ToList().Contains(searchedge));

                    // Find which edge type that fits
                    var e = t.edges.Single(e => e.edge == searchedge);

                    switch (e.OriginalPos)
                    {
                        case Pos.Up:

                            if (e.Flipped != true)
                                t.FlipArrayHorizontally();

                            t.RotateArrayClockwise();
                            t.RotateArrayClockwise();
                            t.RotateArrayClockwise();

                            break;

                        case Pos.Down:
                            // Rotate 180
                            if (e.Flipped == true)
                                t.FlipArrayHorizontally();

                            t.RotateArrayClockwise();


                            break;

                        case Pos.Left:

                            t.RotateArrayClockwise();

                            if (e.Flipped == true)
                                t.FlipArrayHorizontally();

                            t.RotateArrayClockwise();
                            t.RotateArrayClockwise();
                            t.RotateArrayClockwise();

                            break;
                        case Pos.Right:

                            t.RotateArrayClockwise();

                            if (e.Flipped != true)
                                t.FlipArrayHorizontally();

                            t.RotateArrayClockwise();

                            break;
                        default:
                            break;
                    }

                    DumpTile(t);


                    puzzle[xx + 1, yy] = t;

                }

            }


            // Construct matrix without borders
            char[,] sea = new char[puzzle.GetLength(0) * 8, puzzle.GetLength(1) * 8];

            int strideX = 0;
            int strideY = 0;

            for (int tileY = 0; tileY < puzzle.GetLength(0); tileY++)
            {
                for (int bitMapY = 1; bitMapY <= 8; bitMapY++)
                {
                    for (int tileX = 0; tileX < puzzle.GetLength(1); tileX++)
                    {
                        for (int bitMapX = 1; bitMapX <= 8; bitMapX++)
                        {
                            sea[strideX, strideY] = puzzle[tileX, tileY].b[bitMapX, bitMapY];
                            strideX++;
                        }
                    }

                    strideY++;
                    strideX = 0;
                }
            }


            DumpArray(sea);

            // Trial and horror
            sea = FlipArrayHorizontally(sea);
            sea = RotateArrayClockwise(sea);


            // Search
            int found = 0;
            for (int offY = 0; offY < sea.GetLength(0) - 3; offY++)
            {
                for (int offX = 0; offX < sea.GetLength(0) - 20; offX++)
                {
                    //"                  # "
                    //"#    ##    ##    ###"
                    //" #  #  #  #  #  #   "

                    if
                        (
                            sea[offX + 18, offY + 0] == '#' &&
                            sea[offX + 0, offY + 1] == '#' &&
                            sea[offX + 5, offY + 1] == '#' &&
                            sea[offX + 6, offY + 1] == '#' &&
                            sea[offX + 11, offY + 1] == '#' &&
                            sea[offX + 12, offY + 1] == '#' &&
                            sea[offX + 17, offY + 1] == '#' &&
                            sea[offX + 18, offY + 1] == '#' &&
                            sea[offX + 19, offY + 1] == '#' &&
                            sea[offX + 1, offY + 2] == '#' &&
                            sea[offX + 4, offY + 2] == '#' &&
                            sea[offX + 7, offY + 2] == '#' &&
                            sea[offX + 10, offY + 2] == '#' &&
                            sea[offX + 13, offY + 2] == '#' &&
                            sea[offX + 16, offY + 2] == '#'
                        )
                    {
                        // Mark
                        sea[offX + 18, offY + 0] = 'O';
                        sea[offX + 0, offY + 1] = 'O';
                        sea[offX + 5, offY + 1] = 'O';
                        sea[offX + 6, offY + 1] = 'O';
                        sea[offX + 11, offY + 1] = 'O';
                        sea[offX + 12, offY + 1] = 'O';
                        sea[offX + 17, offY + 1] = 'O';
                        sea[offX + 18, offY + 1] = 'O';
                        sea[offX + 19, offY + 1] = 'O';
                        sea[offX + 1, offY + 2] = 'O';
                        sea[offX + 4, offY + 2] = 'O';
                        sea[offX + 7, offY + 2] = 'O';
                        sea[offX + 10, offY + 2] = 'O';
                        sea[offX + 13, offY + 2] = 'O';
                        sea[offX + 16, offY + 2] = 'O';


                        found++;
                    }
                }
            }


            DumpArray(sea);


            int water = 0;
            foreach (var item in sea)
            {
                if (item == '#') water++;
            }

            Console.WriteLine(water);
            Console.ReadKey();
        }



        internal static void DumpTile(Tile t)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.Write(t.b[j, i]);
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }


        internal static void DumpArray(char[,] c)
        {
            for (int i = 0; i < c.GetLength(0); i++)
            {
                for (int j = 0; j < c.GetLength(1); j++)
                {
                    Console.Write(c[j, i]);
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }



        public static char[,] RotateArrayClockwise(char[,] b)
        {
            int width;
            int height;

            width = b.GetUpperBound(0) + 1;
            height = b.GetUpperBound(1) + 1;
            var b2 = new char[width, height];

            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    int newRow;
                    int newCol;

                    newRow = col;
                    newCol = height - (row + 1);

                    b2[newCol, newRow] = b[col, row];
                }
            }

            return b2;
        }


        public static char[,] FlipArrayHorizontally(char[,] b)
        {
            int width;
            int height;

            width = b.GetUpperBound(0) + 1;
            height = b.GetUpperBound(1) + 1;
            var b2 = new char[96, 96];

            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    int newRow;
                    int newCol;

                    newRow = row;
                    newCol = 95 - col;

                    b2[newCol, newRow] = b[col, row];
                }
            }

            return b2;
        }
    }
}
