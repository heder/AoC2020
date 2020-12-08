using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _8._2
{
    class Program
    {
        struct Instruction
        {
            public string opcode { get; set; }
            public int arg { get; set; }
        }


        static void Main(string[] args)
        {
            var lines = File.ReadLines("in.txt");
            var program = lines.Select(f => { var a = f.Split(" "); return new Instruction() { opcode = a[0], arg = Convert.ToInt32(a[1]) }; }).ToArray();

            Instruction[] originalProgram = new Instruction[program.Length];
            program.CopyTo(originalProgram, 0);

            int latestReplacedOp = -1;
            while (true)
            {
                originalProgram.CopyTo(program, 0);

                int accumulator = 0;
                Instruction currentInstruction;
                int pc = 0;
                
                List<int> executionHistory = new List<int>();

                for (int i = latestReplacedOp + 1; i < program.Length; i++)
                {
                    // Search for "nop"
                    if (program[i].opcode == "nop")
                    {
                        Console.WriteLine($"Replacing nop to jmp @ {i}");
                        latestReplacedOp = i;
                        program[i].opcode = "jmp";
                        break;
                    }

                    // Search for "jmp"
                    if (program[i].opcode == "jmp")
                    {
                        Console.WriteLine($"Replacing jmp to nop @ {i}");
                        latestReplacedOp = i;
                        program[i].opcode = "nop";
                        break;
                    }
                }

                while (pc < program.Length)
                {
                    currentInstruction = program[pc];

                    if (executionHistory.Contains(pc))
                    {
                        Console.WriteLine("ERROR: Loop detected");
                        break;
                    }

                    executionHistory.Add(pc);

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


                    if (pc >= program.Length)
                    {
                        Console.WriteLine("Normal termination");
                        Console.WriteLine($"Acc: {accumulator}");
                        Console.ReadKey();
                        return;
                    }
                }
            }
        }
    }
}
