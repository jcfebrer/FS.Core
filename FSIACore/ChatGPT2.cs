using Newtonsoft.Json;
using OpenAI.ObjectModels.RequestModels;
using System;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace FSIACore
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

        public class ChatResponseError : ChatResponse
        {
            public string Message { get; set; }
            public string Type { get; set; }
            public object Param { get; set; }
            public string Code { get; set; }
        }

        [DataContract]
        public class ChatResponseSuccess : ChatResponse
        {
            [DataMember(Name = "id")]
            [JsonPropertyName("id")]
            public string? Id { get; set; }

            [DataMember(Name = "object")]
            [JsonPropertyName("object")]
            public string? Object { get; set; }

            [DataMember(Name = "created")]
            [JsonPropertyName("created")]
            public int Created { get; set; }

            [DataMember(Name = "model")]
            [JsonPropertyName("model")]
            public string? Model { get; set; }

            [DataMember(Name = "choices")]
            [JsonPropertyName("choices")]
            public ChatChoice[]? Choices { get; set; }

            [DataMember(Name = "usage")]
            [JsonPropertyName("usage")]
            public ChatUsage? Usage { get; set; }
        }

        [DataContract]
        public class ChatChoice
        {
            [DataMember(Name = "message")]
            [JsonPropertyName("message")]
            public ChatMessage? Message { get; set; }

            [DataMember(Name = "index")]
            [JsonPropertyName("index")]
            public int Index { get; set; }

            [DataMember(Name = "logprobs")]
            [JsonPropertyName("logprobs")]
            public object? Logprobs { get; set; }

            [DataMember(Name = "finish_reason")]
            [JsonPropertyName("finish_reason")]
            public string? FinishReason { get; set; }
        }

        [DataContract]
        public class ChatUsage
        {
            [DataMember(Name = "prompt_tokens")]
            [JsonPropertyName("prompt_tokens")]
            public int PromptTokens { get; set; }

            [DataMember(Name = "completion_tokens")]
            [JsonPropertyName("completion_tokens")]
            public int CompletionTokens { get; set; }

            [DataMember(Name = "total_tokens")]
            [JsonPropertyName("total_tokens")]
            public int TotalTokens { get; set; }
        }
        public abstract class ChatResponse
        {
        }

        public ChatGPT2(string key, string organization)
        {
            Key = key;
            Organization = organization;
        }

        async public Task<ChatResponse> Question(string prompt, string system)
        {
            GptMessage message = new GptMessage();
            message.content = prompt;
            message.role = User;
            GptMessage systemMsg = new GptMessage();
            systemMsg.content = system;
            systemMsg.role = System;

            return await Question(new GptMessage[] { systemMsg, message });
        }

        async public Task<ChatResponse> Question(string prompt)
        { 
            GptMessage message = new GptMessage();
            message.content = prompt;
            message.role = User;
            return await Question(new GptMessage[] { message });
        }

        async public Task<ChatResponse> Question(GptMessage[] messages)
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
                    throw new Exception($"Error en la llamada a la API: {response.StatusCode}, Reason: {response.ReasonPhrase}");
                }

                var responseString = await response.Content.ReadAsStringAsync();

                switch (response.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                    //case HttpStatusCode.TooManyRequests:
                    case HttpStatusCode.InternalServerError:
                    case HttpStatusCode.NotFound:
                    case HttpStatusCode.BadRequest:
                        {
                            return JsonConvert.DeserializeObject<ChatResponseError>(responseString);
                        }
                }

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return null;
                }

                // Return the response data
                return  JsonConvert.DeserializeObject<ChatResponseSuccess>(responseString);
            }
        }
    }
}