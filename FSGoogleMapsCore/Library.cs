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
using Newtonsoft.Json.Linq;

namespace FSGoogleMapsCore
{

	public static class Library
	{
        public static string googleMapsKey = "AIzaSyBv6ixS_tpSfxCRlIAiU7IhQ8UtS8RbyA0";

        public static int GetDistance(string origin, string destination)
        {
            int distance = 0;
            //string from = origin.Text;
            //string to = destination.Text;
            string url = "https://maps.googleapis.com/maps/api/directions/json?origin=" + origin + "&destination=" + destination + "&sensor=false&key=" + googleMapsKey;
            //string requesturl = @"https://maps.googleapis.com/maps/api/directions/json?origin=" + from + "&alternatives=false&units=imperial&destination=" + to + "&sensor=false";
            string content = FileGetContents(url);
            JObject o = JObject.Parse(content);
            try
            {
                distance = (int)o.SelectToken("routes[0].legs[0].distance.value");
                return distance / 1000;
            }
            catch
            {
                return -1;
            }
        }

        public static string FileGetContents(string fileName)
        {
            string sContents = string.Empty;
            string me = string.Empty;
            try
            {
                if (fileName.ToLower().IndexOf("http:") > -1 || fileName.ToLower().IndexOf("https:") > -1)
                {
                    System.Net.WebClient wc = new System.Net.WebClient();
                    byte[] response = wc.DownloadData(fileName);
                    sContents = System.Text.Encoding.ASCII.GetString(response);

                }
                else
                {
                    System.IO.StreamReader sr = new System.IO.StreamReader(fileName);
                    sContents = sr.ReadToEnd();
                    sr.Close();
                }
            }
            catch { sContents = "Unable to connect to server"; }
            return sContents;
        }


        public static decimal CalcAirDistance(double latA, double longA, double latB, double longB)
        {

            double theDistance = (Math.Sin(DegreesToRadians(latA)) *
                    Math.Sin(DegreesToRadians(latB)) +
                    Math.Cos(DegreesToRadians(latA)) *
                    Math.Cos(DegreesToRadians(latB)) *
                    Math.Cos(DegreesToRadians(longA - longB)));

            return Convert.ToDecimal((RadiansToDegrees(Math.Acos(theDistance)))) * 69.09M * 1.6093M;
        }

        public static double DegreesToRadians(double degrees)
        {
            return (degrees * Math.PI) / 180;
        }

        public static double RadiansToDegrees(double radians)
        {
            return radians * (180 / Math.PI);
        }
    }
}