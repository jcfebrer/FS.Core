using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FSGoogleFirebase
{
    public class Push
    {
        string serverApiKey = "";
        string senderId = "";

        public Push(string apiKey, string senderId)
        {
            this.serverApiKey = apiKey;
            this.senderId = senderId;
        }
        /// <summary>
        /// Envio de mensajes prush a través de la plataforma Firebase de Google
        /// </summary>
        /// <param name="regFB"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string SendMenssage(string regFB, string data)
        {
            try
            {
                string result = "";
                string webAddr = "https://fcm.googleapis.com/fcm/send";
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Headers.Add(string.Format("Authorization: key={0}", serverApiKey));
                httpWebRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                httpWebRequest.Method = "POST";

                using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    // Dim json As String = "{""to"": """ & regFB & """,""content_available"": true,""priority"": ""high"",""notification"": {""body"": """ & mensaje & """, ""title"": """ & mensaje & """}}"
                    string json = "{\"to\": \"" + regFB + "\",\"content_available\": true,\"priority\": \"high\",\"data\": {\"body\": \"" + data + "\"}}";
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }

                HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }

                if (result.IndexOf("\"success\":1") >= 0)
                    return "OK";
                else
                    return result;
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
    }
}
