using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection.Emit;
using System.Text;

#if NETCOREAPP
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
#endif

namespace FSNetwork
{
    public class Telegram
    {
        private string token;

        public Telegram(string token)
        {
            this.token = token;
        }

        public void SendMessage(string chatId, string message)
        {
#if NET45_OR_GREATER || NETCOREAPP
            message = WebUtility.UrlEncode(message);
#else
            message = Uri.EscapeDataString(message);
#endif

            using (WebClient webClient = new WebClient())
            {
                string urlString = $"https://api.telegram.org/bot{token}/sendMessage?chat_id={chatId}&text={message}";
                webClient.DownloadString(urlString);
            }
        }
#if NETCOREAPP
        public async Task SendMessageAsync(string chatId, string message)
        {
            message = WebUtility.UrlEncode(message);

            using (HttpClient client = new HttpClient())
            {
                string urlString = $"https://api.telegram.org/bot{token}/sendMessage?chat_id={chatId}&text={message}";
                var response = await client.GetAsync(urlString);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Mensaje enviado con éxito");
                }
            }
        }

        public async Task SendMessageJson(string chatId, string message)
        {
            using (HttpClient client = new HttpClient())
            {
                var payload = new
                {
                    chat_id = chatId,
                    text = message,
                    parse_mode = "HTML" // Markdown	/ MarkdownV2 / HTML
                };

                string json = JsonSerializer.Serialize(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                string url = $"https://api.telegram.org/bot{token}/sendMessage";
                HttpResponseMessage response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("¡Mensaje enviado con éxito!");
                }
                else
                {
                    string error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al enviar: {response.StatusCode} - {error}");
                }
            }
        }

        public async Task ReadMessages()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    // Usamos un offset para no leer siempre los mismos mensajes
                    // Al pasarle un ID, Telegram marcará los anteriores como leídos
                    string url = $"https://api.telegram.org/bot{token}/getUpdates?timeout=30";

                    var response = await client.GetStringAsync(url);

                    // Analizamos el JSON
                    using (JsonDocument doc = JsonDocument.Parse(response))
                    {
                        JsonElement root = doc.RootElement;
                        if (root.GetProperty("ok").GetBoolean())
                        {
                            var result = root.GetProperty("result");

                            foreach (var update in result.EnumerateArray())
                            {
                                var message = update.GetProperty("message");
                                string text = message.GetProperty("text").GetString();
                                long fromId = message.GetProperty("from").GetProperty("id").GetInt64();
                                string username = message.GetProperty("from").GetProperty("username").GetString();

                                Console.WriteLine($"Mensaje de {username} (ID: {fromId}): {text}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error leyendo: " + ex.Message);
                }
            }
        }

        public async Task<string> GetLatestChatId()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    string url = $"https://api.telegram.org/bot{token}/getUpdates";
                    string response = await client.GetStringAsync(url);

                    using (JsonDocument doc = JsonDocument.Parse(response))
                    {
                        JsonElement root = doc.RootElement;
                        JsonElement result = root.GetProperty("result");

                        if (result.GetArrayLength() > 0)
                        {
                            // Obtenemos el último update del array
                            JsonElement lastUpdate = result[result.GetArrayLength() - 1];

                            // Navegamos hasta el ID del chat
                            long chatId = lastUpdate.GetProperty("message")
                                                   .GetProperty("chat")
                                                   .GetProperty("id")
                                                   .GetInt64();

                            return chatId.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener ChatID: " + ex.Message);
                }

                return null; // O string.Empty si no hay mensajes
            }
        }
#endif
    }
}
