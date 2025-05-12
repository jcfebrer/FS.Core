#if NET45_OR_GREATER || NETCOREAPP

using System;
using System.Net.Http.Headers;
using System.Net.Http;
using FSException;

namespace FSNetwork
{
    public class WebApi
    {
        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                BasicAuth = true;
                BearerAuth = false;
            }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                BasicAuth = true;
                BearerAuth = false;
            }
        }
        private string _token;
        public string Token
        {
            get { return _token; }
            set
            {
                _token = value;
                BasicAuth = false;
                BearerAuth = true;
            }
        }

        public bool BearerAuth { get; set; }
        public bool BasicAuth { get; set; }

        public WebApi()
        {
            BasicAuth = false;
            BearerAuth = false;
        }

        public WebApi(string token)
        {
            Token = token;

            BasicAuth = false;
            BearerAuth = true;
        }

        public WebApi(string userName, string password)
        {
            UserName = userName;
            Password = password;

            BasicAuth = true;
            BearerAuth = false;
        }

        public string CallApi(string url, string urlParameters)
        {
            using (HttpClient client = new HttpClient())
            {
                if (url.EndsWith("\\"))
                    url = url.Substring(0, url.Length - 1);

                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                if (BasicAuth)
                {
                    var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(UserName + ":" + Password);
                    string val = System.Convert.ToBase64String(plainTextBytes);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", val);
                    //client.DefaultRequestHeaders.Add("Authorization", "Basic " + val);
                }

                if (BearerAuth)
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                HttpResponseMessage response = client.GetAsync(urlParameters).Result;
                //response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    return content;
                }
                else
                {
                    throw new ExceptionUtil(String.Format("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase));
                }
            }
        }

        public async System.Threading.Tasks.Task<string> CallApiAsync(string url, string urlParameters)
        {
            using (HttpClient client = new HttpClient())
            {
                if (url.EndsWith("\\"))
                    url = url.Substring(0, url.Length - 1);

                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                if (BasicAuth)
                {
                    var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(UserName + ":" + Password);
                    string val = System.Convert.ToBase64String(plainTextBytes);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", val);
                    //client.DefaultRequestHeaders.Add("Authorization", "Basic " + val);
                }

                if (BearerAuth)
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                HttpResponseMessage response = await client.GetAsync(urlParameters);
                //response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return content;
                }
                else
                {
                    throw new ExceptionUtil(String.Format("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase));
                }
            }
        }

        //public async Task<string> CallApiAsyncAuth(string url, string urlParameters, bool auth = false, string user = "", string pass = "")
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri(url);
        //        client.DefaultRequestHeaders.Clear();
        //        client.DefaultRequestHeaders.ConnectionClose = true;

        //        var authenticationString = $"{user}:{pass}";
        //        var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));

        //        var requestMessage = new HttpRequestMessage(HttpMethod.Post, urlParameters);
        //        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);

        //        var response = await client.SendAsync(requestMessage);
        //        response.EnsureSuccessStatusCode();
        //        string responseBody = await response.Content.ReadAsStringAsync();
        //        return responseBody;
        //    }
        //}
    }
}

#endif