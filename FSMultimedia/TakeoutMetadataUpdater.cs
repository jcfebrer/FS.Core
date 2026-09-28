#if NET48_OR_GREATER || NETCOREAPP

using System;
using System.Globalization;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FSMultimedia
{
    // Modelos serializables con System.Text.Json
    public class TakeoutTime
    {
        [JsonPropertyName("timestamp")]
        public string Timestamp { get; set; }
    }

    public class TakeoutGeoData
    {
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("altitude")]
        public double Altitude { get; set; }
    }

    public class TakeoutJsonModel
    {
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("photoTakenTime")]
        public TakeoutTime PhotoTakenTime { get; set; }

        [JsonPropertyName("creationTime")]
        public TakeoutTime CreationTime { get; set; }

        [JsonPropertyName("geoData")]
        public TakeoutGeoData GeoData { get; set; }
    }

    public class TakeoutMetadataUpdater
    {
        public string FilePath { get; }
        public string JsonPath { get; }

        public TakeoutMetadataUpdater(string filePath, string jsonPath = null)
        {
            FilePath = filePath;
            JsonPath = jsonPath ?? $"{filePath}.json";

            // Si no existe con .json, probar el sufijo de Takeout de Google Photos (.supplemental-metadata.json)
            if (!File.Exists(JsonPath) && File.Exists($"{filePath}.supplemental-metadata.json"))
            {
                JsonPath = $"{filePath}.supplemental-metadata.json";
            }
        }

        public bool UpdateMetadata()
        {
            if (!File.Exists(FilePath) || !File.Exists(JsonPath))
                return false;

            try
            {
                // 1. Leer y deserializar el JSON de Google Takeout
                string jsonContent = File.ReadAllText(JsonPath);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var metadata = JsonSerializer.Deserialize<TakeoutJsonModel>(jsonContent, options);
                if (metadata == null) return false;

                // 2. Obtener fecha
                DateTimeOffset? takenDate = GetDateFromTakeout(metadata);

                // 3. Escribir metadatos internos (Fotos y Vídeos)
                try
                {
                    using (var tagFile = TagLib.File.Create(FilePath))
                    {
                        // Mapear Descripción
                        if (!string.IsNullOrEmpty(metadata.Description))
                        {
                            tagFile.Tag.Comment = metadata.Description;
                            tagFile.Tag.Description = metadata.Description;
                        }

                        // Mapear Fecha y Hora completas
                        if (takenDate.HasValue)
                        {
                            ApplyDateTime(tagFile, takenDate.Value);
                        }

                        // Mapear Coordenadas GPS
                        if (metadata.GeoData != null && (metadata.GeoData.Latitude != 0.0 || metadata.GeoData.Longitude != 0.0))
                        {
                            ApplyGpsCoordinates(tagFile, metadata.GeoData);
                        }

                        // Guardar cambios en el archivo
                        tagFile.Save();
                    }
                }
                catch (TagLib.UnsupportedFormatException)
                {
                    Console.WriteLine($"Formato no soportado directamente por TagLibSharp: {FilePath}");
                }

                // 4. Actualizar fechas del sistema de archivos
                if (takenDate.HasValue)
                {
                    DateTime utcTime = takenDate.Value.UtcDateTime;
                    File.SetCreationTimeUtc(FilePath, utcTime);
                    File.SetLastWriteTimeUtc(FilePath, utcTime);
                }

                Console.WriteLine($"Metadatos y GPS procesados con éxito en {FilePath}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error procesando {FilePath}: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Aplica la fecha y hora completas (año, mes, día, hora, minuto, segundo) 
        /// según el tipo de archivo multimedia.
        /// </summary>
        private void ApplyDateTime(TagLib.File tagFile, DateTimeOffset takenDate)
        {
            // 1. Tag genérico (asigna al menos el año)
            tagFile.Tag.Year = (uint)takenDate.Year;

            // 2. Para Imágenes (JPG, PNG, TIFF, etc.): Escribe EXIF DateTimeOriginal completo
            if (tagFile is TagLib.Image.File imageFile)
            {
                imageFile.ImageTag.DateTime = takenDate.LocalDateTime;
            }

            // 3. Para Vídeos MP4 / MOV / QuickTime
            if (tagFile.GetTag(TagLib.TagTypes.Apple) is TagLib.Mpeg4.AppleTag appleTag)
            {
                // MP4/QuickTime guarda las fechas de captura/creación en UTC
                appleTag.SetDashBox("com.apple.quicktime.creationdate", "mdta", takenDate.ToString("yyyy-MM-ddTHH:mm:ssK"));
            }
        }

        /// <summary>
        /// Aplica las coordenadas GPS tanto para imágenes (EXIF/XMP) como para vídeos (Atómos de QuickTime/MP4).
        /// </summary>
        private void ApplyGpsCoordinates(TagLib.File tagFile, TakeoutGeoData geo)
        {
            // 1. Para imágenes (JPEG, PNG, TIFF, etc.)
            if (tagFile is TagLib.Image.File imageFile)
            {
                imageFile.ImageTag.Latitude = geo.Latitude;
                imageFile.ImageTag.Longitude = geo.Longitude;
                imageFile.ImageTag.Altitude = geo.Altitude;
            }

            // 2. Para contenedores MP4 / MOV (Apple QuickTime Meta / ISO 6709)
            if (tagFile.GetTag(TagLib.TagTypes.Apple) is TagLib.Mpeg4.AppleTag appleTag)
            {
                // Formato ISO 6709 estándar para MP4: "+37.3860-122.0839/"
                string iso6709Location = string.Format(
                    CultureInfo.InvariantCulture,
                    "{0:+00.0000;-00.0000}{1:+000.0000;-000.0000}/",
                    geo.Latitude,
                    geo.Longitude
                );

                // Escribir en la etiqueta de localización de MP4/QuickTime
                appleTag.SetDashBox("com.apple.quicktime.location.ISO6709", "mdta", iso6709Location);
            }
        }

        private DateTimeOffset? GetDateFromTakeout(TakeoutJsonModel metadata)
        {
            string rawTimestamp = metadata.PhotoTakenTime?.Timestamp ?? metadata.CreationTime?.Timestamp;

            if (long.TryParse(rawTimestamp, out long unixTimestamp))
            {
                return DateTimeOffset.FromUnixTimeSeconds(unixTimestamp);
            }

            return null;
        }
    }
}

#endif