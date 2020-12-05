using System;
using System.IO;
using System.Linq;

namespace _5._1
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadLines("in.txt").ToArray();

            int highest = 0;
            foreach (var line in lines)
            {
                var binaryRow = line.Substring(0, 7).Replace("F", "0").Replace("B", "1");
                int row = Convert.ToInt32(binaryRow, 2);

                var binaryColumn = line.Substring(7, 3).Replace("L", "0").Replace("R", "1");
                int col = Convert.ToInt32(binaryColumn, 2);

                int id = (row * 8) + col;

                highest = Math.Max(highest, id);
            }

            Console.WriteLine(highest);
            Console.ReadKey();
        }
    }
}
