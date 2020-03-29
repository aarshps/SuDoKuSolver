using System;
using System.Threading;

namespace SuDoKu.Managers
{
    public static class ConsoleManager
    {
        public static bool isDebug;

        public static void LogLine(string text)
        {
            if (isDebug)
            {
                Thread.Sleep(3000);
                Console.WriteLine(text);
            }
        }
    }
}
