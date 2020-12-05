using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _5._2
{
    class Seat
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public int Id { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadLines("in.txt");
            List<Seat> seats = new List<Seat>();

            foreach (var line in lines)
            {
                var binaryRow = line.Substring(0, 7).Replace("F", "0").Replace("B", "1");
                int row = Convert.ToInt32(binaryRow, 2);

                var binaryColumn = line.Substring(7, 3).Replace("L", "0").Replace("R", "1");
                int col = Convert.ToInt32(binaryColumn, 2);

                int id = (row * 8) + col;

                seats.Add(new Seat() { Col = col, Row = row, Id = id });
            }

            var rowsWithLessThan8Cols = seats
                .GroupBy(f => f.Row)
                .Select(group => new { Row = group.Key, Count = group.Count() })
                .Where(f => f.Count < 8);

            foreach (var item in rowsWithLessThan8Cols)
            {
                Console.WriteLine($"{item.Row}:{item.Count}");

                var s = seats
                    .Where(f => f.Row == item.Row)
                    .OrderBy(f => f.Col);

                foreach (var col in s)
                {
                    Console.WriteLine($"{col.Row}:{col.Col}:{col.Id}");
                }
            }

            Console.ReadKey();
        }
    }
}
