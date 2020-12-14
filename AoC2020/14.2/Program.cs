using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _14._2
{
    class Program
    {
        static string currentmask;
        static Dictionary<long, long> ram = new Dictionary<long, long>();

        static void Main(string[] args)
        {
            var lines = File.ReadLines("in.txt").ToArray();

            foreach (var line in lines)
            {
                var splitted = line.Split("=");

                if (splitted[0].Trim() == "mask")
                {
                    currentmask = splitted[1].Trim();
                }

                if (splitted[0].StartsWith("mem"))
                {
                    long val = Convert.ToInt64(splitted[1].Trim());
                    long addr = Convert.ToInt64(splitted[0].Trim().Substring(4, splitted[0].Trim().Length - 5));

                    char[] cmask = currentmask.ToCharArray();
                    int noFloatingBits = cmask.Count(f => f == 'X');
                    int n = Convert.ToInt32(new String('1', noFloatingBits), 2);

                    // Prepare address
                    char[] saddr = Convert.ToString(addr, 2).PadLeft(36, '0').ToCharArray();
                    for (int i = 0; i < 36; i++)
                    {
                        if (cmask[i] == 'X')  saddr[i] = 'X';
                        if (cmask[i] == '1') saddr[i] = '1';
                    }

                    // Handle floating bits
                    for (int i = 0; i <= n; i++)
                    {
                        string binary = Convert.ToString(i, 2).PadLeft(noFloatingBits, '0');
                        int binposcounter = 0;
                        char[] result = new char[36];
                        saddr.CopyTo(result, 0);

                        // Distribute binary digits in mask
                        for (int j = 0; j < 36; j++)
                        {
                            if (result[j] == 'X')
                            {
                                result[j] = binary[binposcounter];
                                binposcounter++;
                            }
                        }

                        var astring = new string(result);
                        addr = Convert.ToInt64(astring, 2);

                        // Process
                        if (ram.ContainsKey(addr))
                        {
                            ram[addr] = val;
                        }
                        else
                        {
                            ram.Add(addr, val);
                        }
                    }
                }
            }

            long runningSum = 0;
            foreach (var item in ram)
            {
                runningSum += item.Value;
            }

            Console.WriteLine(runningSum);
            Console.ReadKey();
        }
    }
}
