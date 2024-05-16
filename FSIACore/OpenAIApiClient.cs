using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FSIACore
{
    public class OpenAIApiClient
    {
        private readonly HttpClient httpClient;
        private readonly string apiKey;
        private readonly string organization;

        public OpenAIApiClient(string apiKey, string organization)
        {
            this.apiKey = apiKey;
            this.organization = organization;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://api.openai.com/v1/");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        }

        public async Task<string> SendPrompt(string prompt, string model)
        {
            var requestBody = new
            {
                organization = organization,
                rol = "user",
                prompt = prompt,
                model = model,
                max_tokens = 150,
                temperature = 0.5
            };

            string responseBody;
            var response = await httpClient.PostAsJsonAsync("completions", requestBody);
            if (response.IsSuccessStatusCode)
            {
                response.EnsureSuccessStatusCode();
                responseBody = await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new Exception("Error: " + response.ReasonPhrase + " (" + response.StatusCode.ToString() + ")" );
            }

            return responseBody;
        }
    }
}