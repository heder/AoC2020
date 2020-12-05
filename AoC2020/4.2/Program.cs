using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace _4._2
{
    class Passport
    {
        public Passport()
        {
        }


        public string _byr = null;
        public string _iyr = null;
        public string _eyr = null;
        public string _hgt = null;
        public string _hcl = null;
        public string _ecl = null;
        public string _pid = null;
        public string _cid = null;

        public string byr
        {
            get { return _byr; }
            set
            {
                if (value.Length == 4 &&
                    Convert.ToInt32(value) >= 1920 &&
                    Convert.ToInt32(value) <= 2002)
                {
                    _byr = value;
                }
            }
        }
        public string iyr
        {
            get { return _iyr; }
            set
            {
                if (value.Length == 4 &&
                    Convert.ToInt32(value) >= 2010 &&
                    Convert.ToInt32(value) <= 2020)
                {
                    _iyr = value;
                }
            }
        }
        public string eyr
        {
            get { return _eyr; }
            set
            {
                if (value.Length == 4 &&
                    Convert.ToInt32(value) >= 2020 &&
                    Convert.ToInt32(value) <= 2030)
                {
                    _eyr = value;
                }
            }
        }
        public string hgt
        {
            get { return _hgt; }
            set
            {
                int l = value.Length;
                string type = value.Substring(value.Length - 2, 2);
                int res;
                if
                    (
                (type == "in" || type == "cm") &&
                int.TryParse(value.Substring(0, value.Length - 2), out res) == true
                )
                {
                    if (
                            (type == "cm" && res >= 150 && res <= 193) ||
                            (type == "in" && res >= 59 && res <= 76)
                        )
                    {
                        _hgt = value;
                    }
                }
            }
        }
        public string hcl
        {
            get { return _hcl; }
            set
            {
                int l = value.Length;

                if
                    (
                    value[0] == '#' &&
                    new Regex("^[0-9a-f]").IsMatch(value.Substring(1)) == true
                    )
                {
                    _hcl = value;
                }
            }
        }
        public string ecl
        {
            get { return _ecl; }
            set
            {
                if (new List<string>() { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" }.Contains(value))
                {
                    _ecl = value;
                }
            }
        }
        public string pid
        {
            get { return _pid; }
            set
            {
                int result;

                if (value.Length == 9 &&
                    int.TryParse(value, out result))
                {
                    _pid = value;
                }
            }
        }

        public string cid
        {
            get { return _cid; }
            set { _cid = value; }
        }
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
