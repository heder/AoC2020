using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _8._1
{
    class Program
    {
        class Instruction
        {
            public string opcode { get; set; }
            public int arg { get; set; }
        }


        static void Main(string[] args)
        {
            var lines = File.ReadLines("in.txt");

            var program = lines.Select(f => { var a = f.Split(" "); return new Instruction() { opcode = a[0], arg = Convert.ToInt32(a[1]) }; }).ToArray();

            int accumulator = 0;
            Instruction currentInstruction;
            int pc = 0;
            List<int> executionHistory = new List<int>();

            while (pc < program.Length)
            {
                currentInstruction = program[pc];

                switch (currentInstruction.opcode)
                {
                    case "nop":
                        pc++;
                        break;

                    case "acc":
                        accumulator += currentInstruction.arg;
                        pc++;
                        break;

                    case "jmp":
                        pc += currentInstruction.arg;
                        break;
                }

                if (executionHistory.Contains(pc))
                {
                    // Loop
                    Console.WriteLine(accumulator);
                    Console.ReadKey();
                    return;
                }

                executionHistory.Add(pc);
            }
        }
    }
}
