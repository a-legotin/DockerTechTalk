using RequestSenderSimple.Classes;
using System;
using System.Threading;

namespace RequestSenderSimple
{
    public class Program
    {
        private static int failCount;

        public static void Main(string[] args)
        {
            var uri = "http://192.168.1.25:6075/";
            var name = "Unnamed container";
            var nameFromEnvVar = Environment.GetEnvironmentVariable("CONNAME");
            if (!string.IsNullOrEmpty(nameFromEnvVar)) name = nameFromEnvVar;
            Console.WriteLine($"Starting...{uri}, my name is {name}");
            while (failCount < 10)
                try
                {
                    var random = new Random(DateTime.Now.Millisecond);
                    var howLongToSleep = random.Next(10, 500);
                    Thread.Sleep(howLongToSleep);
                    var response = HttpHelper.PostSync(uri, new SimpleRequest { Name = name, Slept = howLongToSleep });
                    Console.WriteLine(response);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    failCount++;
                }
        }
    }
}