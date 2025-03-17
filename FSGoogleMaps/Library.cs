using System.Collections;
using System.Collections.Specialized;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Download;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

#if !NETFRAMEWORK
    using System.Text.Json;
#endif

using FSNetwork;
using FSLibrary;
using System.Net.Http;
using FSException;
using System.Web.Script.Serialization;

namespace FSGoogleMaps
{

    public class Library
    {
        public string ApiKey { get; set; }

        public Library(string apiKey)
        {
            ApiKey = apiKey;
        }

        //public static int GetDistance(string origin, string destination)
        //{
        //    int distance = 0;
        //    //string from = origin.Text;
        //    //string to = destination.Text;
        //    string url = "https://maps.googleapis.com/maps/api/directions/json?origin=" + origin + "&destination=" + destination + "&sensor=false&key=" + googleMapsKey;
        //    //string requesturl = @"https://maps.googleapis.com/maps/api/directions/json?origin=" + from + "&alternatives=false&units=imperial&destination=" + to + "&sensor=false";
        //    string content = Http.GetFileContents(url);
        //    JObject o = JObject.Parse(content);
        //    try
        //    {
        //        distance = (int)o.SelectToken("routes[0].legs[0].distance.value");
        //        return distance / 1000;
        //    }
        //    catch
        //    {
        //        return -1;
        //    }
        //}

        public async Task<double> GetDistanceAsync(string origin, string destination)
        {
#if NETFRAMEWORK
                JavaScriptSerializer JsonSerializer = new JavaScriptSerializer();
#endif

            using (HttpClient client = new HttpClient())
            {
                string url = $"https://maps.googleapis.com/maps/api/distancematrix/json?origins={origin}&destinations={destination}&key={ApiKey}";

                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var distanceMatrixResponse = JsonSerializer.Deserialize<DistanceMatrixResponse>(responseBody);

                if (distanceMatrixResponse.rows.Length > 0 && distanceMatrixResponse.rows[0].elements.Length > 0)
                {
                    var element = distanceMatrixResponse.rows[0].elements[0];
                    if (element.status == "OK")
                    {
                        return element.distance.value; // Distancia en metros
                    }
                    else
                    {
                        throw new ExceptionUtil($"Error en la respuesta de la API: {element.status}");
                    }
                }
                else
                {
                    throw new ExceptionUtil("No se encontraron resultados en la respuesta de la API.");
                }
            }
        }
        private class DistanceMatrixResponse
        {
            public string status { get; set; }
            public Row[] rows { get; set; }
        }

        private class Row
        {
            public Element[] elements { get; set; }
        }

        private class Element
        {
            public Distance distance { get; set; }
            public string status { get; set; }
        }

        private class Distance
        {
            public double value { get; set; }
        }
    }
}