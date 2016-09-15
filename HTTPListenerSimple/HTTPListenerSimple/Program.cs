using System.Drawing;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace HTTPListenerSimple
{
    class Program
    {
        static void Main(string[] args)
        {
            var outputWriter = ConsoleOutputWriter.GetOutputWriter();
            var requestRepo = RequestRepository.GetRepository();
            string url = "http://127.0.0.1";
            string port = "9999";
            string prefix = $"{url}:{port}/";

            HttpListener listener = new HttpListener();
            listener.Prefixes.Add(prefix);
            listener.Start();
            outputWriter.Write("Welcome to simple HttpListene", Color.DarkGray);
            outputWriter.Write($"Listening on {prefix}...", Color.DarkGray);


            while (true)
            {
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;
                SimpleRequest requestContent = JsonConvert.DeserializeObject<SimpleRequest>(GetRequestPostData(request));
                outputWriter.Write($"Got request from {requestContent.Name}, which slept {requestContent.Slept} ms right befor sending request", Color.Green);
                requestRepo.Add(request);
                string responseString = $"Hey, {requestContent.Name}, your request is {requestRepo.CountItems()} in queue.";
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                response.ContentLength64 = buffer.Length;
                response.StatusCode = (int)HttpStatusCode.OK;
                using (Stream stream = response.OutputStream) { stream.Write(buffer, 0, buffer.Length); }
            }
        }

        public static string GetRequestPostData(HttpListenerRequest request)
        {
            if (!request.HasEntityBody)
            {
                return null;
            }
            using (System.IO.Stream body = request.InputStream) // here we have data
            {
                using (System.IO.StreamReader reader = new System.IO.StreamReader(body, request.ContentEncoding))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
