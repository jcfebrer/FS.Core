using Newtonsoft.Json;
using System;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FSAi
{
    public class ChatGPT2
    {
        public string Key { get; set; }
        public string Organization { get; set; }

        public string User = "user";
        public string System = "system";

        public class GptMessage
        {
            public string role;
            public string content;
        }

        public ChatGPT2(string key, string organization)
        {
            Key = key;
            Organization = organization;
        }

        async public Task<string> Question(string prompt, string system)
        {
            GptMessage message = new GptMessage();
            message.content = prompt;
            message.role = User;
            GptMessage systemMsg = new GptMessage();
            systemMsg.content = system;
            systemMsg.role = System;

            return await Question(new GptMessage[] { systemMsg, message });
        }

        async public Task<string> Question(string prompt)
        { 
            GptMessage message = new GptMessage();
            message.content = prompt;
            message.role = User;
            return await Question(new GptMessage[] { message });
        }

        async public Task<string> Question(GptMessage[] messages)
        {
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            //ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            using (var httpClient = new HttpClient())
            {
                // Set the API key in the request headers
                if (httpClient.DefaultRequestHeaders.Contains("Authorization"))
                {
                    httpClient.DefaultRequestHeaders.Remove("Authorization");
                }

                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Key}");
                //httpClient.DefaultRequestHeaders.Add("Organization", Organization);

                var requestBody = new
                {
                    frequency_penalty = 0,
                    max_tokens = 2000,
                    messages = messages,
                    model = "gpt-3.5-turbo",
                    n = 1,
                    presence_penalty = 0,
                    stream = false,
                    temperature = 0.7,
                    top_p = 1.0,
                };

                var jsonRequestBody = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(jsonRequestBody, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error en la llamada a la API: {response.StatusCode}");
                }

                var responseString = await response.Content.ReadAsStringAsync();

                var responseObject = JsonConvert.DeserializeObject<dynamic>(responseString);

                return responseObject.choices[0].text.ToString();
            }
        }
    }
}