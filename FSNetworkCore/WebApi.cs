using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace FSNetworkCore
{
    public class WebApi
    {
        public static string CallApi(string url, string urlParameters)
        {
            using (HttpClient client = new HttpClient())
            {
                if (url.EndsWith("\\"))
                    url = url.Substring(0, url.Length - 1);

                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync(urlParameters).Result;
                //response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    return content;
                }
                else
                {
                    throw new Exception(String.Format("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase));
                }
            }
        }

        public static async Task<string> CallApiAsync(string url, string urlParameters)
        {
            using (HttpClient client = new HttpClient())
            {
                if (url.EndsWith("\\"))
                    url = url.Substring(0, url.Length - 1);

                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(urlParameters);
                //response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return content;
                }
                else
                {
                    throw new Exception(String.Format("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase));
                }
            }
        }
    }
}
