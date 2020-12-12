using System;
using System.IO;
using System.Linq;

namespace _12._1
{
    struct Action
    {
        public char action;
        public int arg;
    }


    class Program
    {
        static int x = 0;
        static int y = 0;
        static int angle = 90;

        static void Main(string[] args)
        {
            var actions = File.ReadLines("in.txt").Select(f => new Action() { action = f.Substring(0, 1)[0], arg = Convert.ToInt32(f.Substring(1)) });

            foreach (var a in actions)
            {
                switch (a.action)
                {
                    case 'N':
                        y += a.arg;
                        break;

                    case 'S':
                        y -= a.arg;
                        break;

                    case 'W':
                        x -= a.arg;
                        break;

                    case 'E':
                        x += a.arg;
                        break;

                    case 'L':
                        angle -= a.arg;
                        angle = (angle % 360 + 360) % 360;
                        break;

                    case 'R':
                        angle += a.arg;
                        angle = angle % 360;
                        break;

                    case 'F':
                        MoveForward(a.arg);
                        break;

                    default:
                        break;
                }

                Console.WriteLine($"Moved to {x}:{y}");
            }

            int mhdist = Math.Abs(x) + Math.Abs(y);
            Console.WriteLine(mhdist);
            Console.ReadKey();

        }

        private static void MoveForward(int distance)
        {
            // Handle straight angles:
            switch (angle)
            {
                case 0:
                    y += distance;
                    break;

                case 180:
                    y -= distance;
                    break;

                case 270:
                    x -= distance;
                    break;

                case 90:
                    x += distance;
                    break;
            }

            //if (angle > 0 && angle < 90)
            //{
            //    int a = angle;

            //    // NE
            //    int deltaY = distance * Math.Sin(a);
            //    int deltaX = distance * Math.Cos(a);

            //    y += deltaY;
            //    x += deltaX;
            //} 
            //else if (angle > 90 && angle < 180)
            //{
            //    // SE
            //    int a = angle - 90;

            //    int deltaY = distance * Math.Sin(a);
            //    int deltaX = distance * Math.Cos(a);

            //    y -= deltaY;
            //    x += deltaX;
            //}
            //else if (angle > 270 && angle < 360)
            //{
            //    int a = 360 - angle;

            //    // NW
            //    int deltaY = distance * Math.Sin(a);
            //    int deltaX = distance * Math.Cos(a);

            //    y += deltaY;
            //    x -= deltaX;
            //}
            //else if (angle > 180 && angle < 270)
            //{
            //    // SW
            //    int a = 270 - angle;

            //    // NW
            //    int deltaY = distance * Math.Sin(a);
            //    int deltaX = distance * Math.Cos(a);

            //    y -= deltaY;
            //    x -= deltaX;
            //}
        }
    }
}
