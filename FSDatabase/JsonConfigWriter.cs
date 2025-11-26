#if NETCOREAPP

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

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
            // Leer JSON actual
            string json = File.ReadAllText(configPath);
            var doc = JsonSerializer.Deserialize<Dictionary<string, object>>(json);

            // Asegurar que existe la sección
            var sectionDict = doc.ContainsKey(section)
                ? JsonSerializer.Deserialize<Dictionary<string, object>>(doc[section].ToString())
                : new Dictionary<string, object>();

            // Modificar valor
            sectionDict[key] = value;

            // Reinsertar sección
            doc[section] = sectionDict;

            // Guardar
            string output = JsonSerializer.Serialize(doc, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(configPath, output);
        }
    }
}
#endif