using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _4._1
{
    class Passport
    {
        public Passport()
        {
            byr = null;
            iyr = null;
            eyr = null;
            hgt = null;
            hcl = null;
            ecl = null;
            pid = null;
            cid = null;
        }

        public string byr { get; set; }
        public string iyr { get; set; }
        public string eyr { get; set; }
        public string hgt { get; set; }
        public string hcl { get; set; }
        public string ecl { get; set; }
        public string pid { get; set; }
        public string cid { get; set; }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadLines("in.txt").ToArray();
            List<Passport> passports = new List<Passport>();


            int i = 0;
            while (i < lines.Length)
            {
                var pp = new Passport();

                while (i < lines.Length && lines[i].Trim() != "")
                {
                    var fields = lines[i].Split(" ");
                    foreach (var field in fields)
                    {
                        var data = field.Split(":");

                        switch (data[0])
                        {
                            case "byr": pp.byr = data[1]; break;
                            case "iyr": pp.iyr = data[1]; break;
                            case "eyr": pp.eyr = data[1]; break;
                            case "hgt": pp.hgt = data[1]; break;
                            case "hcl": pp.hcl = data[1]; break;
                            case "ecl": pp.ecl = data[1]; break;
                            case "pid": pp.pid = data[1]; break;
                            case "cid": pp.cid = data[1]; break;
                        }
                    }

                    i++;
                }
                passports.Add(pp);

                i++;
            }

            int noOfValid = 0;

            foreach (var passport in passports)
            {
                if (passport.byr != null && passport.iyr != null && passport.eyr != null && passport.hgt != null && passport.hcl != null && passport.ecl != null && passport.pid != null)
                {
                    noOfValid++;
                }
            }

            Console.WriteLine(noOfValid);
            Console.ReadKey();
        }
    }
}
