using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RequestSenderSimple;

namespace ConsoleApplication
{
    public class Program
    {
        private static int failCount = 0;
        public static void Main(string[] args)
        {
            string uri = "http://localhost:9999/";
            string name = "Unnamed container";
            while (failCount < 10)
            {
                try
                {
                    var random = new Random(DateTime.Now.Millisecond);
                    int howLongToSleep = random.Next(10, 2000);
                    Thread.Sleep(howLongToSleep);
                    string response = GetAsync(uri, new SimpleRequest() { Name = name, Slept = howLongToSleep });
                    Console.WriteLine(response);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    failCount++;
                }
            }
         }

        public static string GetAsync(string uri, SimpleRequest request)
        {
            var httpClient = new HttpClient();
            var postData = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = httpClient.PostAsync(uri, postData);
            string content = response.Result.Content.ReadAsStringAsync().Result;
            return content;
        }
    }
}
