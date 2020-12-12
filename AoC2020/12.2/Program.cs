using System;
using System.IO;
using System.Linq;

namespace _12._2
{
    struct Action
    {
        public char action;
        public int arg;
    }


    class Program
    {
        static int shipx = 0;
        static int shipy = 0;

        static int wpx = 10;
        static int wpy = 1;

        static void Main(string[] args)
        {
            var actions = File.ReadLines("in.txt").Select(f => new Action() { action = f.Substring(0, 1)[0], arg = Convert.ToInt32(f.Substring(1)) });

            foreach (var a in actions)
            {
                switch (a.action)
                {
                    case 'N':
                        wpy += a.arg;
                        break;

                    case 'S':
                        wpy -= a.arg;
                        break;

                    case 'W':
                        wpx -= a.arg;
                        break;

                    case 'E':
                        wpx += a.arg;
                        break;

                    case 'L':
                        RotateWayPointLeft(a.arg);
                        break;

                    case 'R':
                        RotateWayPointRight(a.arg);
                        break;

                    case 'F':
                        MoveForwardRelWpTimes(a.arg);
                        break;

                    default:
                        break;
                }

                Console.WriteLine($"Ship a {shipx}:{shipy} Waypoint at {wpx}:{wpy}");
            }

            int mhdist = Math.Abs(shipx) + Math.Abs(shipy);
            Console.WriteLine(mhdist);
            Console.ReadKey();

        }

        private static void RotateWayPointLeft(int arg)
        {
            int dX;
            int dY;
            int steps = arg / 90;

            for (int i = 0; i < steps; i++)
            {
                dY = shipy - wpy;
                dX = shipx - wpx;

                wpy = shipy - dX;
                wpx = shipx + dY;
            }
        }

        private static void RotateWayPointRight(int arg)
        {
            int dX;
            int dY;
            int steps = arg / 90;

            for (int i = 0; i < steps; i++)
            {
                dY = shipy - wpy;
                dX = shipx - wpx;

                wpy = shipy + dX;
                wpx = shipx - dY;
            }
        }

        private static void MoveForwardRelWpTimes(int n)
        {
            // Calc ship rel waypoint
            int deltax = wpx - shipx;
            int deltay = wpy - shipy;

            // Move ship and waypoint
            for (int i = 0; i < n; i++)
            {
                shipx += deltax;
                shipy += deltay;

                wpx += deltax;
                wpy += deltay;
            }
        }
    }
}
