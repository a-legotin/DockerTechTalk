using System.Drawing;
using System.IO;
using System.Net;

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
                outputWriter.Write($"Got request from {request.RemoteEndPoint.ToString()}", Color.Gray);
                requestRepo.Add(request);
                response.StatusCode = (int)HttpStatusCode.OK;
                using (Stream stream = response.OutputStream) { }
            }
        }
    }
}
