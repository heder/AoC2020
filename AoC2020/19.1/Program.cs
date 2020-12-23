using System;
using System.IO;
using System.Linq;

namespace _11._1
{

    class Rule
    {
        public int ruleid;
    }


    class Program
    {

        public static void Main(string[] args)
        {
            var lines = File.ReadLines("in.txt").ToArray();

            int i = 0;
            while (lines[i] != "")
            {
                var split = lines[i].Split(":");
                int id = Convert.ToInt32(split[0]);
 
            }

        }

    }
}
