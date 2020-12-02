using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _2._2
{
    class Password
    {
        public int PosA { get; set; }
        public int PosB { get; set; }
        public char Letter { get; set; }

        public char LetterAtA { get; set; }
        public char LetterAtB { get; set; }
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

                pw.PosA = Convert.ToInt32(c[0]);
                pw.PosB = Convert.ToInt32(c[1]);
                pw.Letter = b[1].Trim()[0];
                pw.PW = a[1].Trim();

                pw.LetterAtA = pw.PW[pw.PosA - 1];
                pw.LetterAtB = pw.PW[pw.PosB - 1];

                if ((pw.LetterAtA == pw.Letter || pw.LetterAtB == pw.Letter) && (pw.LetterAtA != pw.LetterAtB))
                    pw.Valid = true;
                else
                    pw.Valid = false;

                passwords.Add(pw);
            }

            Console.WriteLine(passwords.Count(f => f.Valid == true));
            Console.ReadKey();

        }
    }
}
