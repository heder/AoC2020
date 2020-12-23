using System;
using System.IO;

namespace _18._1
{
    class Program
    {
        public static char[] ex;
        public static ulong pos;

        public static void Main(string[] args)
        {
            ulong sum = 0;
            var lines = File.ReadLines("in.txt");
            foreach (var line in lines)
            {
                ex = line.Replace(" ", "").Trim().ToCharArray();

                //ex = new char[ex2.Length + 2];
                //ex[0] = '0';
                //ex[1] = '+';

                //Array.Copy(ex2, 0, ex, 2, ex2.Length);

                ulong s = CalcExpression();

                Console.WriteLine(s);

                pos = 0;
                sum += s;

            }

            Console.WriteLine(sum);

            Console.ReadKey();
        }

        public static ulong CalcExpression()
        {
            ulong sum = 0;

            //if (ex[pos] == '(')
            //{
            //    sum = 0;
            //    pos++;
            //    sum += CalcExpression();
            //}
            //else
            //{ 
            //    sum = Convert.ToUInt64(ex[pos].ToString());
            //    pos++;
            //}

            while (pos < (uint)ex.Length)
            {

                switch (ex[pos])
                {
                    case '(':
                        pos++;
                        sum += CalcExpression();
                        break;

                    case ')':
                        return sum;

                    case '+':
                        pos++;
                        if (ex[pos] == '(')
                        {
                            pos++;
                            sum += CalcExpression();
                        }
                        else
                        {
                            sum += Convert.ToUInt64(ex[pos].ToString());
                        }

                        break;

                    case '*':
                        pos++;
                        if (ex[pos] == '(')
                        {
                            pos++;
                            sum *= CalcExpression();
                        }
                        else
                        {
                            sum *= Convert.ToUInt64(ex[pos].ToString());
                        }

                        break;

                    default:
                        sum = Convert.ToUInt64(ex[pos].ToString());
                        break;
                }

                pos++;
            }

            return sum;
        }
    }
}
