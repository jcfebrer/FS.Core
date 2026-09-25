#if NET45_OR_GREATER || NETCOREAPP

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Text;
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

        [JsonPropertyName("geoData")]
        public TakeoutGeoData GeoData { get; set; }
    }

    public class TakeoutMetadataUpdater
    {
        public string ImagePath { get; }
        public string JsonPath { get; }

        // IDs estándar de propiedades EXIF
        private const int EXIF_IMAGE_DESCRIPTION = 0x010E;
        private const int EXIF_DATE_TIME_ORIGINAL = 0x9003;
        private const int EXIF_GPS_LATITUDE_REF = 0x0001;
        private const int EXIF_GPS_LATITUDE = 0x0002;
        private const int EXIF_GPS_LONGITUDE_REF = 0x0003;
        private const int EXIF_GPS_LONGITUDE = 0x0004;

        public TakeoutMetadataUpdater(string imagePath, string jsonPath = null)
        {
            ImagePath = imagePath;
            JsonPath = jsonPath ?? $"{imagePath}.supplemental-metadata.json";
        }

        public bool UpdateMetadata()
        {
            if (!File.Exists(ImagePath) || !File.Exists(JsonPath))
                return false;

            try
            {
                // 1. Leer y deserializar el JSON usando System.Text.Json
                string jsonContent = File.ReadAllText(JsonPath);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var metadata = JsonSerializer.Deserialize<TakeoutJsonModel>(jsonContent, options);

                if (metadata == null) return false;

                // 2. Cargar la imagen
                byte[] imageBytes = File.ReadAllBytes(ImagePath);
                using (var ms = new MemoryStream(imageBytes))
                using (var image = Image.FromStream(ms))
                {
                    // 3. Mapear Fecha de Toma (PhotoTakenTime)
                    if (long.TryParse(metadata.PhotoTakenTime?.Timestamp, out long unixTimestamp))
                    {
                        DateTime dt = FSLibrary.DateTimeUtil.FromUnixTimeSeconds(unixTimestamp).LocalDateTime;
                        string formattedDate = dt.ToString("yyyy:MM:dd HH:mm:ss\0"); // Formato EXIF ASCII
                        byte[] dateBytes = Encoding.ASCII.GetBytes(formattedDate);

                        SetPropertyItem(image, EXIF_DATE_TIME_ORIGINAL, 2, dateBytes); // Type 2 = ASCII String
                    }

                    // 4. Mapear Descripción
                    if (!string.IsNullOrWhiteSpace(metadata.Description))
                    {
                        byte[] descBytes = Encoding.UTF8.GetBytes(metadata.Description + "\0");
                        SetPropertyItem(image, EXIF_IMAGE_DESCRIPTION, 2, descBytes);
                    }

                    // 5. Mapear GPS (geoData)
                    if (metadata.GeoData != null && (metadata.GeoData.Latitude != 0.0 || metadata.GeoData.Longitude != 0.0))
                    {
                        // Latitud
                        string latRef = metadata.GeoData.Latitude >= 0 ? "N\0" : "S\0";
                        SetPropertyItem(image, EXIF_GPS_LATITUDE_REF, 2, Encoding.ASCII.GetBytes(latRef));
                        SetPropertyItem(image, EXIF_GPS_LATITUDE, 5, DegreesToExifRational(metadata.GeoData.Latitude)); // Type 5 = Rational

                        // Longitud
                        string lonRef = metadata.GeoData.Longitude >= 0 ? "E\0" : "W\0";
                        SetPropertyItem(image, EXIF_GPS_LONGITUDE_REF, 2, Encoding.ASCII.GetBytes(lonRef));
                        SetPropertyItem(image, EXIF_GPS_LONGITUDE, 5, DegreesToExifRational(metadata.GeoData.Longitude));
                    }

                    // 6. Guardar la imagen de forma segura reemplazando el original
                    string tempPath = ImagePath + ".tmp";
                    image.Save(tempPath, ImageFormat.Jpeg);

                    image.Dispose();
                    ms.Dispose();

                    File.Delete(ImagePath);
                    File.Move(tempPath, ImagePath);
                }

                Console.WriteLine($"Metadatos incrustados con éxito en {ImagePath}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error procesando {ImagePath}: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Crea o reemplaza un PropertyItem mediante Reflection.
        /// </summary>
        private void SetPropertyItem(Image image, int propId, short type, byte[] value)
        {
            PropertyItem prop = null;
            try
            {
                prop = image.GetPropertyItem(propId);
            }
            catch
            {
                ConstructorInfo ctor = typeof(PropertyItem).GetConstructor(
                    BindingFlags.NonPublic | BindingFlags.Instance, null, Type.EmptyTypes, null);

                if (ctor != null)
                {
                    prop = (PropertyItem)ctor.Invoke(null);
                    prop.Id = propId;
                }
            }

            if (prop != null)
            {
                prop.Type = type;
                prop.Len = value.Length;
                prop.Value = value;

                image.SetPropertyItem(prop);
            }
        }

        /// <summary>
        /// Convierte coordenadas decimales en 3 pares de Racionales EXIF (Grados, Minutos, Segundos).
        /// </summary>
        private byte[] DegreesToExifRational(double degrees)
        {
            degrees = Math.Abs(degrees);
            uint d = (uint)Math.Floor(degrees);
            double mDouble = (degrees - d) * 60.0;
            uint m = (uint)Math.Floor(mDouble);
            uint s = (uint)Math.Round((mDouble - m) * 60.0 * 100.0);

            byte[] result = new byte[24];

            // Grados (d/1)
            Array.Copy(BitConverter.GetBytes(d), 0, result, 0, 4);
            Array.Copy(BitConverter.GetBytes((uint)1), 0, result, 4, 4);

            // Minutos (m/1)
            Array.Copy(BitConverter.GetBytes(m), 0, result, 8, 4);
            Array.Copy(BitConverter.GetBytes((uint)1), 0, result, 12, 4);

            // Segundos (s/100)
            Array.Copy(BitConverter.GetBytes(s), 0, result, 16, 4);
            Array.Copy(BitConverter.GetBytes((uint)100), 0, result, 20, 4);

            return result;
        }
    }
}

#endif