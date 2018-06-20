using System;
using System.Drawing;

namespace HTTPListenerSimple
{
    public class ConsoleOutputWriter : IOutputWriter
    {
        internal static IOutputWriter GetOutputWriter()
        {
            return new ConsoleOutputWriter();
        }

        public void Write(string output, Color color)
        {
            ConsoleColor consoleColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), color.Name);
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(output);
            Console.ResetColor();
        }
    }
}
