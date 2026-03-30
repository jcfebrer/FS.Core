using Google.GenAI;
using Google.GenAI.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSGoogleGemini
{
    public class Library
    {
        private readonly string apiKey;
        private readonly string model;

        public Library(string apiKey, string model = "models/gemini-2.5-flash")
        {
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new ArgumentNullException(nameof(apiKey), "La API Key no puede estar vacía.");

            this.apiKey = apiKey;
            this.model = model;
        }

        public string Question(string prompt)
        {
            return QuestionAsync(prompt).Result;
        }

        public async Task<string> QuestionAsync(string prompt)
        {
            var client = new Client(apiKey: apiKey);

            try
            {
                // 3. Genera contenido
                var response = await client.Models.GenerateContentAsync(
                    model: model,
                    contents: prompt
                );

                // 4. Muestra el resultado
                Console.WriteLine("Gemini dice: " + response.Text);
                return response.Text;
            }
            catch (Exception ex)
            {
                // Logueamos el error y relanzamos o manejamos según necesites
                throw new Exception($"Error al comunicarse con Gemini: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// https://generativelanguage.googleapis.com/v1beta/models?key=TU_API_KEY
        /// </summary>
        /// <returns></returns>
        public async Task<string> ListModelsAsync()
        {
            var client = new Client(apiKey: apiKey);
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.AppendLine("--- Listando modelos disponibles ---");

                // Llamada al método ListModelsAsync
                var models = await client.Models.ListAsync();

                await foreach (var model in models)
                {
                    sb.AppendLine($"ID: {model.Name}");
                    sb.AppendLine($"   Descripción: {model.Description}");
                    sb.AppendLine($"   Versión: {model.Version}");
                    // Comprobamos si soporta la generación de contenido
                    sb.AppendLine($"   Soporta GenerateContent: {model.SupportedActions.Contains("generateContent")}");
                    sb.AppendLine(new string('-', 30));
                }
            }
            catch (Exception ex)
            {
                sb.AppendLine($"Error al listar modelos: {ex.Message}");
            }

            return sb.ToString();
        }
    }
}
