using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace RequestSenderSimple.Classes
{
  public class HttpHelper
  {
    public static string PostSync(string uri, SimpleRequest request)
    {
      string content;
      using (var httpClient = new HttpClient())
      {
        httpClient.BaseAddress = new Uri(uri);
        var postData = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = httpClient.PostAsync(uri, postData);
        content = response.Result.Content.ReadAsStringAsync().Result;
      }

      return content;
    }
  }
}