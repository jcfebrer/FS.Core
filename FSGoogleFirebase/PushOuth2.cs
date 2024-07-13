using Google.Apis.Auth.OAuth2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FSGoogleFirebase.Auth;
using System.Security.Policy;
using System.IO;

namespace FSGoogleFirebase
{
    /// <summary>
    /// Las credenciales se obtienen de:
    /// https://console.cloud.google.com/apis/credentials
    /// </summary>
    public class PushOuth2
    {
        public string ProjectId { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string JsonKeyFilePath { get; set; }
        public string JsonSecretsKeyFilePath { get; set; }

        public PushOuth2(string projectId)
        {
            this.ProjectId = projectId;
        }
        public void LoadJsonKeyFile(string jsonKeyFilePath)
        {
            this.JsonKeyFilePath = jsonKeyFilePath;
        }

        public void LoadClientSecrets(string jsonSecretsKeyFilePath)
        {
            this.JsonSecretsKeyFilePath = jsonSecretsKeyFilePath;
        }

        public void SetClientSecret(string clientId, string clientSecret)
        {
            this.ClientId = clientId;
            this.ClientSecret = clientSecret;
        }

        public async Task SendMessage(string deviceToken, string message)
        {
            await SendMessage(deviceToken, "", message);
        }

        public async Task SendMessage(string deviceToken, string title, string body)
        {
            var fcmUrl = "https://fcm.googleapis.com/v1/projects/" + ProjectId + "/messages:send";

            var scopes = new[] { "https://www.googleapis.com/auth/firebase.messaging" };

            string token;
            ClientSecrets clientSecrets;

            if (String.IsNullOrEmpty(this.JsonKeyFilePath))
            {
                if (String.IsNullOrEmpty(this.JsonSecretsKeyFilePath))
                {
                    clientSecrets = new ClientSecrets
                    {
                        ClientId = this.ClientId,
                        ClientSecret = this.ClientSecret
                    };
                }
                else
                {
                    var stream = new FileStream(this.JsonSecretsKeyFilePath, FileMode.Open, FileAccess.Read);
                    clientSecrets = GoogleClientSecrets.FromStream(stream).Secrets;
                }

                var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    clientSecrets,
                    scopes,
                    "user",
                    CancellationToken.None);

                token = await credential.GetAccessTokenForRequestAsync();
            }
            else
            {
                using (var stream = new FileStream(JsonKeyFilePath, FileMode.Open, FileAccess.Read))
                {
                    token = await GoogleCredential.FromStream(stream)
                        .CreateScoped(scopes)
                        .UnderlyingCredential
                        .GetAccessTokenForRequestAsync();
                }
            }

            var message = new
            {
                message = new
                {
                    token = deviceToken,
                    notification = new
                    {
                        title = title,
                        body = body
                    }
                }
            };

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var jsonMessage = System.Text.Json.JsonSerializer.Serialize(message);
                var httpContent = new StringContent(jsonMessage, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(fcmUrl, httpContent);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    throw new Exception("Error: " + responseString);
                }
            }
        }
    }
}