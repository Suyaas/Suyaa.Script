using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sychost
{
    public static class SConsole
    {
        // 内存锁
        private static object _lock = new object();

        /// <summary>
        /// 输出内容
        /// </summary>
        /// <param name="content"></param>
        public static void Write(string content)
        {
            lock (_lock)
            {
                Console.Write(content);
            }
        }

        /// <summary>
        /// 输出内容
        /// </summary>
        /// <param name="content"></param>
        /// <param name="color"></param>
        public static void Write(string content, ConsoleColor color)
        {
            lock (_lock)
            {
                ConsoleColor colorBefore = Console.ForegroundColor;
                Console.ForegroundColor = color;
                Console.Write(content);
                Console.ForegroundColor = colorBefore;
            }
        }

        /// <summary>
        /// 换行输出内容
        /// </summary>
        /// <param name="content"></param>
        public static void WriteLine()
        {
            lock (_lock)
            {
                Console.WriteLine();
            }
        }

        /// <summary>
        /// 换行输出内容
        /// </summary>
        /// <param name="content"></param>
        public static void WriteLine(string content)
        {
            lock (_lock)
            {
                Console.WriteLine(content);
            }
        }

        /// <summary>
        /// 输出内容
        /// </summary>
        /// <param name="content"></param>
        /// <param name="color"></param>
        public static void WriteLine(string content, ConsoleColor color)
        {
            lock (_lock)
            {
                ConsoleColor colorBefore = Console.ForegroundColor;
                Console.ForegroundColor = color;
                Console.WriteLine(content);
                Console.ForegroundColor = colorBefore;
            }
        }

        /// <summary>
        /// 输出内容
        /// </summary>
        /// <param name="evt"></param>
        /// <param name="content"></param>
        public static void Log(string evt, string content)
        {
            lock (_lock)
            {
                ConsoleColor colorBefore = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write($"[{evt}] ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(content);
                Console.ForegroundColor = colorBefore;
            }
        }
    }
}
