using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using RequestSenderSimple;

namespace RequestSenderSimple.Classes
{
    public class HttpHelper
    {
        public static string PostSync(string uri, SimpleRequest request)
        {
            var httpClient = new HttpClient();
            var postData = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = httpClient.PostAsync(uri, postData);
            string content = response.Result.Content.ReadAsStringAsync().Result;
            return content;
        }
    }
}