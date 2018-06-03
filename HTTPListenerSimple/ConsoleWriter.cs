using System;
using System.Drawing;

namespace HTTPListenerSimple
{
  public class ConsoleOutputWriter : IOutputWriter
  {
    public void Write(string output, Color color)
    {
      var consoleColor = (ConsoleColor) Enum.Parse(typeof(ConsoleColor), color.Name);
      Console.ForegroundColor = consoleColor;
      Console.WriteLine(output);
      Console.ResetColor();
    }

    internal static IOutputWriter GetOutputWriter()
    {
      return new ConsoleOutputWriter();
    }
  }
}