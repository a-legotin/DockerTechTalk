using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HTTPListenerSimple
{
    internal class Program
    {
        private const int statisticIntervalMs = 5000;
        private const string urlSettingKey = "StartUpURL";

        private static void Main(string[] args)
        {
            var outputWriter = ConsoleOutputWriter.GetOutputWriter();
            var requestRepo = RequestRepository.GetRepository();
            var url = ConfigurationManager.AppSettings[urlSettingKey];
            if (string.IsNullOrEmpty(url)) return;
            var listener = new HttpListener();
            listener.Prefixes.Add(url);
            listener.Start();
            outputWriter.Write("Welcome to simple HttpListener", Color.DarkGray);
            outputWriter.Write($"Listening on {url}...", Color.DarkGray);
            var perfTaskCancelToken = new CancellationTokenSource();
            var perfTask = new Task(() => { new PerfMeter(statisticIntervalMs, requestRepo, perfTaskCancelToken); }, TaskCreationOptions.LongRunning);
            perfTask.Start();

            try
            {
                while (true)
                {
                    var context = listener.GetContext();
                    var request = context.Request;
                    var response = context.Response;
                    var requestContent = JsonConvert.DeserializeObject<SimpleRequest>(GetRequestPostData(request));
                    outputWriter.Write($"Got request from {requestContent.Name}, which slept {requestContent.Slept} ms right before sending request", Color.Green);
                    requestRepo.Add(request);
                    string responseString = $"Hey, {requestContent.Name}, your request is {requestRepo.CountItems()} in queue.";
                    var buffer = Encoding.UTF8.GetBytes(responseString);
                    response.ContentLength64 = buffer.Length;
                    response.StatusCode = (int)HttpStatusCode.OK;
                    using (var stream = response.OutputStream)
                    {
                        stream.Write(buffer, 0, buffer.Length);
                    }
                }
            }
            finally
            {
                perfTaskCancelToken.Cancel();
            }
        }

        public static string GetRequestPostData(HttpListenerRequest request)
        {
            if (!request.HasEntityBody)
                return null;
            using (var body = request.InputStream) // here we have data
            {
                using (var reader = new StreamReader(body, request.ContentEncoding))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}