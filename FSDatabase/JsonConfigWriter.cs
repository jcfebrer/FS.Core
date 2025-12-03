#if NETCOREAPP

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace FSDatabase
{
    /// <summary>
    /// Clase para modificar archivos JSON de configuración
    /// </summary>
    public static class JsonConfigWriter
    {
        private static readonly string configPath =
            Path.Combine(AppContext.BaseDirectory, "appsettings.json");

        public static void SetValue(string section, string key, string value)
        {
            // 1. Leer JSON actual y parsear como JsonNode
            string json = File.ReadAllText(configPath);

            // JsonNode.Parse lo convierte en un árbol manipulable (JsonObject en este caso)
            if (JsonNode.Parse(json) is not JsonObject root)
            {
                // Manejo de error si el archivo está vacío o es inválido
                root = new JsonObject();
            }

            // 2. Obtener o crear la sección (ej. "AppSettings")
            if (root[section] is not JsonObject sectionNode)
            {
                // Si la sección no existe o no es un objeto, la creamos
                sectionNode = new JsonObject();
                root[section] = sectionNode;
            }

            // 3. Modificar/Establecer el valor de la clave (ej. "GamesPath")
            sectionNode[key] = value;

            // 4. Guardar
            var options = new JsonSerializerOptions { WriteIndented = true };
            string output = root.ToJsonString(options);

            // Escribir el nuevo JSON en el archivo
            File.WriteAllText(configPath, output);
        }
    }
}
#endif