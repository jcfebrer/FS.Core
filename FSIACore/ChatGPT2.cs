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
        public string Url { get; set; }

        public enum Roles
        {
            user,
            system
        }

        public class GptMessage
        {
            public string role;
            public string content;
        }

        [DataContract]
        public class ChatResponseError : ChatResponse
        {
            [DataMember(Name = "error")]
            [JsonPropertyName("error")]
            public ChatError? Error { get; set; }
        }

        [DataContract]
        public class ChatError
        {
            [DataMember(Name = "message")]
            [JsonPropertyName("message")]
            public string? Message { get; set; }

            [DataMember(Name = "type")]
            [JsonPropertyName("type")]
            public string? Type { get; set; }

            [DataMember(Name = "param")]
            [JsonPropertyName("param")]
            public object? Param { get; set; }

            [DataMember(Name = "code")]
            [JsonPropertyName("code")]
            public string? Code { get; set; }
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
        public class ChatMessage
        {
            [DataMember(Name = "role")]
            [JsonPropertyName("role")]
            public string? Role { get; set; }

            [DataMember(Name = "content")]
            [JsonPropertyName("content")]
            public string? Content { get; set; }

            [DataMember(Name = "name")]
            [JsonPropertyName("name")]
            public string? Name { get; set; }

            [DataMember(Name = "function_call")]
            [JsonPropertyName("function_call")]
            public ChatMessageFunctionCall? FunctionCall { get; set; }
        }

        [DataContract]
        public class ChatMessageFunctionCall
        {
            [DataMember(Name = "name")]
            [JsonPropertyName("name")]
            public string? Name { get; set; }

            [DataMember(Name = "arguments")]
            [JsonPropertyName("arguments")]
            public string? Arguments { get; set; }
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

        public ChatGPT2(string key, string organization, string url)
        {
            Key = key;
            Organization = organization;
            Url = url;
        }

        public async Task<ChatResponse?> Question(string prompt, string system, CancellationToken token)
        {
            GptMessage message = new GptMessage();
            message.content = prompt;
            message.role = nameof(Roles.user);
            GptMessage systemMsg = new GptMessage();
            systemMsg.content = system;
            systemMsg.role = nameof(Roles.system);

            return await Question(new GptMessage[] { systemMsg, message }, token);
        }

        public async Task<ChatResponse?> Question(string prompt, CancellationToken token)
        { 
            GptMessage message = new GptMessage();
            message.content = prompt;
            message.role = nameof(Roles.user);
            return await Question(new GptMessage[] { message }, token);
        }

        public async Task<ChatResponse?> Question(GptMessage[] messages, CancellationToken token)
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

                var jsonRequestBody = System.Text.Json.JsonSerializer.Serialize(requestBody);
                var content = new StringContent(jsonRequestBody, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(Url, content);

#if NETFRAMEWORK
                var responseBody = await response.Content.ReadAsStringAsync();
#else
                var responseBody = await response.Content.ReadAsStringAsync(token);
#endif

                switch (response.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
#if !NETFRAMEWORK
                    case HttpStatusCode.TooManyRequests:
#endif
                    case HttpStatusCode.InternalServerError:
                    case HttpStatusCode.NotFound:
                    case HttpStatusCode.BadRequest:
                        {
                            return System.Text.Json.JsonSerializer.Deserialize<ChatResponseError>(responseBody);
                        }
                }

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return null;
                }

                // Return the response data
                return  System.Text.Json.JsonSerializer.Deserialize<ChatResponseSuccess>(responseBody);
            }
        }
    }
}