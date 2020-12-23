using System;
using System.IO;
using System.Linq;

namespace _18._1
{
    class Program
    {
        public class Node
        {
            public Node(Node p)
            {
                parent = p;
            }

            public string op;

            public int res = -1;

            public Node p;

            public Node a;
            public Node b;
        }



        static void Main(string[] args)
        {
            var lines = File.ReadLines("in.txt").ToArray();

            foreach (var item in lines)
            {
                var line = item.Replace(" ", "").Trim().Reverse().ToArray();

                //string literal = "";
                int arg = 0;
                Node root = new Node();
                Node currentNode = root;
                int i = 0;
                for (i = 0; i < line.Length; i++)
                {
                    switch (line[i])
                    {
                        case '+':
                            currentNode.op = "+";
                            currentNode.b = new Node();
                            currentNode = currentNode.b;
                            break;

                        case '*':
                            currentNode.op = "*";
                            currentNode.b = new Node();
                            currentNode = currentNode.b;
                            break;

                        case '(':
                            currentNode.b = new Node();
                            currentNode.a = new Node();
                            currentNode = currentNode.a;
                            break;

                        case ')':
                            // Unwind to first with b==null;
                            while ()
                            break;

                        default:
                            currentNode.a = new Node();
                            currentNode.a.res = Convert.ToInt32(line[i].ToString());
                            break;
                    };
                }

                currentNode.b = new Node();
                currentNode.op = "+";
                currentNode.b.res = 0;

                int x = Calc(root);

                Console.WriteLine(x);

            }

            Console.WriteLine();

        }



        public static int Calc(Node n)
        {

            if (n.b.res == -1)
            {
                n.b.res = Calc(n.b);
            }

            if (n.a.res == -1)
            {
                n.a.res = Calc(n.a);
            }



            if (n.op == "+") return n.a.res + n.b.res;
            if (n.op == "*") return n.a.res * n.b.res;

            return 0;

        }
    }
}
