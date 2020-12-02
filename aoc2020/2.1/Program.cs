using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _2._1
{


    class Password
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public char Letter { get; set; }
        public string PW { get; set; }
        public int Occurences { get; set; }
        public bool Valid { get; set; }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadLines("in.txt");
            var passwords = new List<Password>();

            foreach (var line in lines)
            {
                var pw = new Password();

                var a = line.Split(":");
                var b = a[0].Split(" ");
                var c = b[0].Split("-");

                pw.Min = Convert.ToInt32(c[0]);
                pw.Max = Convert.ToInt32(c[1]);
                pw.Letter = b[1].Trim()[0];
                pw.PW = a[1].Trim();

                pw.Occurences = pw.PW.Count(f => f == pw.Letter);

                //if (pw.Occurences >= pw.Min && pw.Occurences <= pw.Max)
                //    pw.Valid = true;
                //else
                //    pw.Valid = false;

                passwords.Add(pw);
            }

            Console.WriteLine(passwords.Count(f => f.Valid == true));
            Console.ReadKey();

        }
    }
}
