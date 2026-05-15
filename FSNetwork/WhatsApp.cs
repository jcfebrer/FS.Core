#if NET48_OR_GREATER || NETCOREAPP
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
#endif

#if NETCOREAPP
using System.Net.Http.Json;
#endif

namespace FSNetwork
{
    public class WhatsApp
    {
        private readonly string _phoneNumberId;
        private readonly string _accessToken;

        public WhatsApp(string phoneNumberId, string accessToken)
        {
            _phoneNumberId = phoneNumberId;
            _accessToken = accessToken;
        }

#if NET48_OR_GREATER
        public async Task<string> SendMessage(string toNumber, string message)
        {
            // Configuración de seguridad (Crucial en Framework para TLS 1.2)
            System.Net.ServicePointManager.SecurityProtocol =
                System.Net.SecurityProtocolType.Tls12 |
                System.Net.SecurityProtocolType.Tls11 |
                System.Net.SecurityProtocolType.Tls;

            using (var client = new HttpClient())
            {
                // Configurar el Header de Autorización
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _accessToken);

                // Crear el objeto de datos siguiendo la estructura de Meta
                var payload = new
                {
                    messaging_product = "whatsapp",
                    to = toNumber,
                    type = "text",
                    text = new { body = message }
                };

                // Serializar a JSON manualmente
                string jsonPayload = JsonSerializer.Serialize(payload);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                try
                {
                    // Ejecutar la petición POST
                    var response = await client.PostAsync(
                        $"https://graph.facebook.com/v25.0/{_phoneNumberId}/messages",
                        content);

                    if (response.IsSuccessStatusCode)
                    {
                        return "Mensaje enviado correctamente.";
                    }
                    else
                    {
                        string error = await response.Content.ReadAsStringAsync();
                        return $"Error de API WhatsApp: {response.StatusCode} - {error}";
                    }
                }
                catch (Exception ex)
                {
                    return $"Error de conexión: {ex.Message}";
                }
            }
        }
#endif
#if NETCOREAPP
        public async Task<string> SendMessage(string toNumber, string message)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _accessToken);

                var payload = new
                {
                    messaging_product = "whatsapp",
                    to = toNumber,
                    type = "text",
                    text = new { body = message }
                };

                var response = await client.PostAsJsonAsync(
                    $"https://graph.facebook.com/v25.0/{_phoneNumberId}/messages",
                    payload);

                string error = await response.Content.ReadAsStringAsync();
                return response.IsSuccessStatusCode ? "Mensaje enviado correctamente." : $"Error al enviar: {error}";
            }
        }
#endif
    }
}