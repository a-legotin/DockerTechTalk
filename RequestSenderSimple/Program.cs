using System;
using System.Threading;
using RequestSenderSimple.Classes;

namespace RequestSenderSimple
{
  public class Program
  {
    private static int failCount;

    public static void Main(string[] args)
    {
      var uri = "http://httplistenersimple:777/";
      var name = string.Format("{0:X}", Guid.NewGuid().GetHashCode()).ToLower();
      ;
      var nameFromEnvVar = Environment.GetEnvironmentVariable("CONNAME");
      if (!string.IsNullOrEmpty(nameFromEnvVar)) name = nameFromEnvVar;
      Console.WriteLine($"Starting...Sending to {uri}, my name is {name}");
      while (failCount < 20)
        try
        {
          var random = new Random(DateTime.Now.Millisecond);
          var howLongToSleep = random.Next(500, 5000);
          Thread.Sleep(howLongToSleep);
          var response = HttpHelper.PostSync(uri, new SimpleRequest {Name = name, Slept = howLongToSleep});
          Console.WriteLine(string.IsNullOrWhiteSpace(response) ? "No response" : response);
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.Message);
          failCount++;
        }
    }
  }
}