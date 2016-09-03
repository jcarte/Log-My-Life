using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogMyLife.Domain.Test
{
    public static class Logging
    {

        private static object _lock = new object();
        private static Stopwatch sw = new Stopwatch();

        static Logging()
        {
            sw.Start();
        }

        public static void LogTitle(string msg, int colourCode = 0)
        {
            Log($"\n\n\n==========================", colourCode);
            Log($"=========={msg}===========",colourCode);
            Log($"==========================", colourCode);
        }

        public static void Log(string msg, int colourCode = 0)
        {
            ConsoleColor[] cols = new ConsoleColor[]{
            ConsoleColor.White,
            ConsoleColor.Green,
            ConsoleColor.Blue,
            ConsoleColor.Red,
            ConsoleColor.Cyan,
            ConsoleColor.Yellow
            };

            lock(_lock)
            {
                ConsoleColor col = cols[colourCode];
                Console.ForegroundColor = col;
                Console.WriteLine(msg + "\t" + sw.ElapsedMilliseconds + "ms");
            }
        }
    }
}
