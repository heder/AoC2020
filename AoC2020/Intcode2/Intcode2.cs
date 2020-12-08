using System;
using System.Collections.Generic;

namespace Intcode
{
    public struct Instruction
    {
        public string opcode { get; set; }
        public int arg { get; set; }
    }


    public class IntCode2
    {
        int accumulator = 0;
        Instruction currentInstruction;
        int pc = 0;
        Instruction[] program;
        Instruction[] originalProgram;
        List<int> executionHistory = new List<int>();

        public IntCode2(Instruction[] code)
        {
            program = new Instruction[code.Length];
            originalProgram = new Instruction[code.Length];

            code.CopyTo(program, 0);
            code.CopyTo(originalProgram, 0);
        }

        int Run()
        {

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
                    break;
                }
            }

            return accumulator;
        }
    }
}



            //public enum CpuState
            //{
            //    INIT = 0,
            //    RUNNING = 1,
            //    OUTPUT_READY = 2,
            //    STOPPED = 3,
            //    WAITING_FOR_INPUT = 4
            //}

            //public class CPU
            //{
            //    public long Output { get; set; }
            //    public CpuState State { get; set; }
            //    private long[] ram = new long[5000];
            //    public long pc { get; set; }
            //    private long relativeBase = 0;
            //    private long[] _input;
            //    private long inputPos = 0;

            //    public long[] Input
            //    { 
            //        set
            //        {
            //            _input = value;
            //            inputPos = 0;
            //        }
            //    }

            //    public CPU(long[] ram_in)
            //    {
            //        ram_in.CopyTo(ram, 0);
            //        pc = 0;
            //        State = CpuState.INIT;
            //    }


            //    public void Run()
            //    {
            //        State = CpuState.RUNNING;

            //        long opcode;
            //        long p1mode;
            //        long p2mode;
            //        long p3mode;
            //        long p1addr = 0;
            //        long p2addr = 0;
            //        long p3addr = 0;

            //        while (State == CpuState.RUNNING)
            //        {
            //            opcode = ram[pc] % 100;
            //            p1mode = ram[pc] / 100 % 10;
            //            p2mode = ram[pc] / 1000 % 10;
            //            p3mode = ram[pc] / 10000 % 10;

            //            switch (p1mode)
            //            {
            //                case 0:
            //                    p1addr = ram[pc + 1];
            //                    break;

            //                case 1:
            //                    p1addr = pc + 1;
            //                    break;

            //                case 2:
            //                    p1addr = relativeBase + ram[pc + 1];
            //                    break;
            //            }


            //            switch (p2mode)
            //            {
            //                case 0:
            //                    p2addr = ram[pc + 2];
            //                    break;

            //                case 1:
            //                    p2addr = pc + 2;
            //                    break;

            //                case 2:
            //                    p2addr = relativeBase + ram[pc + 2];
            //                    break;
            //            }


            //            switch (p3mode)
            //            {
            //                case 0:
            //                    p3addr = ram[pc + 3];
            //                    break;

            //                case 1:
            //                    p3addr = pc + 3;
            //                    break;

            //                case 2:
            //                    p3addr = relativeBase + ram[pc + 3];
            //                    break;
            //            }


            //            switch (opcode)
            //            {
            //                case 1: // ADD
            //                    ram[p3addr] = ram[p1addr] + ram[p2addr];
            //                    pc += 4;
            //                    break;

            //                case 2: // MUL
            //                    ram[p3addr] = ram[p1addr] * ram[p2addr];
            //                    pc += 4;
            //                    break;

            //                case 3: // INP
            //                    //Console.WriteLine($"Input at pc {pc}:{input[inputPos]}");
            //                    ram[p1addr] = _input[inputPos];
            //                    inputPos++;
            //                    pc += 2;
            //                    break;

            //                case 4: // OUT
            //                    //Console.WriteLine($"Output at pc {pc}:{ram[p1addr]}");
            //                    Output = ram[p1addr];
            //                    State = CpuState.OUTPUT_READY;
            //                    pc += 2;
            //                    break;

            //                case 5: // JMP if not Z
            //                    if (ram[p1addr] != 0)
            //                    {
            //                        pc = ram[p2addr];
            //                    }
            //                    else
            //                    {
            //                        pc += 3;
            //                    }
            //                    break;

            //                case 6: // JMP if Z
            //                    if (ram[p1addr] == 0)
            //                    {
            //                        pc = ram[p2addr];
            //                    }
            //                    else
            //                    {
            //                        pc += 3;
            //                    }
            //                    break;

            //                case 7: // IFLESS
            //                    if (ram[p1addr] < ram[p2addr])
            //                    {
            //                        ram[p3addr] = 1;
            //                    }
            //                    else
            //                    {
            //                        ram[p3addr] = 0;
            //                    }
            //                    pc += 4;
            //                    break;

            //                case 8: // IFEQ
            //                    if (ram[p1addr] == ram[p2addr])
            //                    {
            //                        ram[p3addr] = 1;
            //                    }
            //                    else
            //                    {
            //                        ram[p3addr] = 0;
            //                    }
            //                    pc += 4;
            //                    break;


            //                case 9: // MOVEBASE
            //                    relativeBase += ram[p1addr];
            //                    pc += 2;
            //                    break;

            //                case 99:
            //                    State = CpuState.STOPPED;
            //                    break;

            //                default:
            //                    Console.WriteLine($"Invalid instruction {ram[pc]} at pc {pc}");
            //                    Console.ReadKey();
            //                    break;
            //            }
            //        }

            //        return;
            //    }
            //}
        //}
