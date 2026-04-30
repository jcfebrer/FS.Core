#if NETCOREAPP
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace FSNetwork
{
    public class TelegramService
    {
        private readonly string _baseUrl;
        private readonly HttpClient _http;
        private readonly Action<string, string> _log;
        private int _lastUpdateId = 0;
        private readonly Action<string> _process;

        public TelegramService(string token, Action<string, string> logAction, Action<string> process)
        {
            _baseUrl = $"https://api.telegram.org/bot{token}/";
            _http = new HttpClient();
            _log = logAction;
            _process = process;
        }

        public async Task StartPollingAsync(CancellationToken ct)
        {
            _log?.Invoke("Telegram", "Iniciando escucha manual de Telegram...");

            while (!ct.IsCancellationRequested)
            {
                try
                {
                    // Importante: El offset confirma a Telegram que ya leímos todo lo anterior
                    var url = $"{_baseUrl}getUpdates?offset={_lastUpdateId + 1}&timeout=30";

                    var response = await _http.GetFromJsonAsync<TelegramResponse>(url, ct);

                    if (response?.Ok == true && response.Result.Count > 0)
                    {
                        foreach (var update in response.Result)
                        {
                            // 1. Actualizamos el ID para no volver a leer este mensaje
                            _lastUpdateId = update.UpdateId;

                            if (update.Message?.Text != null)
                            {
                                await HandleMessage(update.Message);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _log?.Invoke("Telegram", $"Error: {ex.Message}");
                    await Task.Delay(2000, ct);
                }
            }
        }

        public async Task SendChatActionAsync(long chatId, string action = "typing")
        {
            try
            {
                // Tipos de acción: typing, upload_photo, find_location,record_video, upload_video, record_voice, upload_voice, etc.
                var payload = new { chat_id = chatId, action = action };
                await _http.PostAsJsonAsync($"{_baseUrl}sendChatAction", payload);
            }
            catch (Exception ex)
            {
                _log?.Invoke("Error Telegram Action", ex.Message);
            }
        }

        private async Task HandleMessage(TelegramMessage msg)
        {
            _log?.Invoke("Telegram", $"Mensaje de {msg.From.FirstName}: {msg.Text}");

            // Respondemos vía POST
            //var payload = new { chat_id = msg.Chat.Id, text = msg.Text };
            //await _http.PostAsJsonAsync($"{_baseUrl}sendMessage", payload);

            // Mostramos el mensaje escribiendo en el cliente de telegram
            await SendChatActionAsync(msg.Chat.Id, "typing");

            // Procesamos el mensaje recibido
            _process?.Invoke(msg.Text);
        }
    }

    // Clases para deserealizar el JSON de Telegram
    public record TelegramResponse(
    [property: JsonPropertyName("ok")] bool Ok,
    [property: JsonPropertyName("result")] List<TelegramUpdate> Result
);

    public record TelegramUpdate(
        [property: JsonPropertyName("update_id")] int UpdateId,
        [property: JsonPropertyName("message")] TelegramMessage Message
    );

    public record TelegramMessage(
        [property: JsonPropertyName("message_id")] int MessageId,
        [property: JsonPropertyName("chat")] TelegramChat Chat,
        [property: JsonPropertyName("from")] TelegramUser From,
        [property: JsonPropertyName("text")] string Text
    );

    public record TelegramChat([property: JsonPropertyName("id")] long Id);
    public record TelegramUser([property: JsonPropertyName("first_name")] string FirstName);
}
#endif