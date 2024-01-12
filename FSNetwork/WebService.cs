using FSException;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web;
using System.Xml.Linq;

namespace FSNetwork
{
    public class WebService
    {
        public string Url { get; set; }
        public bool IsAspNet { get; set; }
        public string MethodName { get; set; }
        public string NameSpace { get; set; }
        public CookieContainer CookieContainer { get; set; }

        public Dictionary<string, string> Params = new Dictionary<string, string>();
        public XDocument ResultXML;
        public string ResultString;

        public WebService()
        {
        }

        public WebService(string url, string methodName)
        {
            Url = url;
            MethodName = methodName;

            if (url.ToLower().EndsWith(".asmx"))
                IsAspNet = true;
            else
                IsAspNet = false;
        }

        /// <summary>
        /// Invokes service
        /// </summary>
        public void Invoke()
        {
            Invoke(true);
        }

        /// <summary>
        /// Invokes service
        /// </summary>
        /// <param name="encode">Added parameters will encode? (default: true)</param>
        public void Invoke(bool encode)
        {
            if (IsAspNet)
            {
                if (String.IsNullOrEmpty(NameSpace))
                    throw new ExceptionUtil("Debe indicarlse la propiedad NameSpace en llamadas a servicios ASMX de ASP.NET.");
            }

            string soapStr =
                @"<?xml version=""1.0"" encoding=""utf-8""?>
            <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" 
               xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" 
               xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
              <soap:Body>
                <{0} xmlns=""{2}"">
                  {1}
                </{0}>
              </soap:Body>
            </soap:Envelope>";

            if (String.IsNullOrEmpty(NameSpace))
                NameSpace = "http://tempuri.org/";

            soapStr = soapStr.Replace("{2}", NameSpace);

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(Url);

            if (this.CookieContainer != null)  // if (req.SupportsCookieContainer && 
                req.CookieContainer = this.CookieContainer;


            if (IsAspNet)
            {
                req.Headers.Add("SOAPAction", "\"" + NameSpace + "/" + MethodName + "\"");
            }
            else
                req.Headers.Add("SOAPAction", "\"" + "http://tempuri.org/" + MethodName + "\"");

            req.ContentType = "text/xml;charset=\"utf-8\"";
            req.Accept = "text/xml";
            req.Method = "POST";

            using (Stream stm = req.GetRequestStream())
            {
                string postValues = "";
                foreach (var param in Params)
                {
                    if (encode)
                        postValues += string.Format("<{0}>{1}</{0}>", HttpUtility.UrlEncode(param.Key), HttpUtility.UrlEncode(param.Value));
                    else
                        postValues += string.Format("<{0}>{1}</{0}>", param.Key, param.Value);
                }

                soapStr = string.Format(soapStr, MethodName, postValues);
                using (StreamWriter stmw = new StreamWriter(stm))
                {
                    stmw.Write(soapStr);
                }
            }

            try
            {
                using (WebResponse response = req.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader responseReader = new StreamReader(stream))
                        {
                            string result = responseReader.ReadToEnd();
                            ResultXML = XDocument.Parse(result);
                            ResultString = result;
                        }
                    }
                }
            }
            catch(WebException ex)
            {
                using (WebResponse response2 = ex.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response2;
                    if (httpResponse != null)
                    {
                        Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
                        using (Stream data = response2.GetResponseStream())
                        using (var reader2 = new StreamReader(data))
                        {
                            string result = reader2.ReadToEnd();
                            ResultXML = XDocument.Parse(result);
                            ResultString = result;
                        }
                    }
                    else
                        throw new ExceptionUtil(ex);
                }
            }
        }
    }
}