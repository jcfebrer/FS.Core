#region

using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using System.Windows.Forms;

#endregion

namespace WiworBrowser.Controls
{
    public class DBHttp
    {
        public string GetHTTP(string url, ref CookieContainer Cookies)
        {
            string getHTTPReturn = null;

            WiworBrowser.Controls.DBFunctions.Replace(url, "//", "/");
            HttpWebRequest myRequest = ((HttpWebRequest) (WebRequest.Create(url)));
            myRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.0.3705)";
            myRequest.Headers.Add("XXXXXXXXXXXXXXX", "XXXXXXXXXXXXXXX");
            myRequest.Headers.Add("Cache-control", "no-cache");

            if (!(Cookies == null))
            {
                myRequest.CookieContainer = Cookies;
            }

            HttpWebResponse MyResponse = ((HttpWebResponse) (myRequest.GetResponse()));
            Encoding enc = Encoding.GetEncoding("iso-8859-1");
            StreamReader sr = new StreamReader(MyResponse.GetResponseStream(), enc);
            getHTTPReturn = sr.ReadToEnd();
            sr.Close();

            if (!(Cookies == null))
            {
                if (!String.IsNullOrEmpty(MyResponse.Headers["set-cookie"]))
                {
                    Cookies.Add(ParseCookies(MyResponse.Headers["set-cookie"]));
                }
            }
            return getHTTPReturn;
        }


        public string GetHTTP(string url)
        {
            CookieContainer cookie = null;
            return GetHTTP(url, ref cookie);
        }


        public string PostHttp(string url, string postdata, ref CookieContainer Cookies)
        {
            string postHttpReturn = null;

            WiworBrowser.Controls.DBFunctions.Replace(url, "//", "/");
            HttpWebRequest myRequest = ((HttpWebRequest) (WebRequest.Create(url)));
            myRequest.ContentType = "application/x-www-form-urlencoded";
            myRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.0.3705)";
            myRequest.Headers.Add("XXXXXXXXXXXXXXX", "XXXXXXXXXXXXXXX");
            myRequest.Headers.Add("Cache-control", "no-cache");
            myRequest.AllowAutoRedirect = true;
            myRequest.Method = "POST";
            myRequest.CookieContainer = Cookies;
            myRequest.ContentLength = postdata.Length;
            Stream s = null;
            s = myRequest.GetRequestStream();
            s.Write(Encoding.ASCII.GetBytes(postdata), 0, postdata.Length);
            s.Close();
            HttpWebResponse MyResponse = ((HttpWebResponse) (myRequest.GetResponse()));
            Encoding enc = Encoding.GetEncoding("iso-8859-1");
            StreamReader sr = new StreamReader(MyResponse.GetResponseStream(), enc);
            postHttpReturn = sr.ReadToEnd();
            if (!String.IsNullOrEmpty(MyResponse.Headers["set-cookie"]))
            {
                Cookies.Add(ParseCookies(MyResponse.Headers["set-cookie"]));
            }
            sr.Close();
            return postHttpReturn;
        }


        public string PostHttp(string url, string postdata)
        {
            string postHttpReturn = null;

            WiworBrowser.Controls.DBFunctions.Replace(url, "//", "/");
            HttpWebRequest myRequest = ((HttpWebRequest) (WebRequest.Create(url)));
            myRequest.ContentType = "application/x-www-form-urlencoded";
            myRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.0.3705)";
            myRequest.Headers.Add("XXXXXXXXXXXXXXX", "XXXXXXXXXXXXXXX");
            myRequest.Headers.Add("Cache-control", "no-cache");
            myRequest.AllowAutoRedirect = true;
            myRequest.Method = "POST";
            myRequest.ContentLength = postdata.Length;
            Stream s = null;
            s = myRequest.GetRequestStream();
            s.Write(Encoding.ASCII.GetBytes(postdata), 0, postdata.Length);
            s.Close();
            HttpWebResponse MyResponse = ((HttpWebResponse) (myRequest.GetResponse()));
            Encoding enc = Encoding.GetEncoding("iso-8859-1");
            StreamReader sr = new StreamReader(MyResponse.GetResponseStream(), enc);
            postHttpReturn = sr.ReadToEnd();
            sr.Close();
            return postHttpReturn;
        }


        public string LoginHttp(string url, string postdata, ref CookieContainer Cookies)
        {
            string loginHttpReturn = null;

            WiworBrowser.Controls.DBFunctions.Replace(url, "//", "/");
            HttpWebRequest myRequest = ((HttpWebRequest) (WebRequest.Create(url)));
            myRequest.ContentType = "application/x-www-form-urlencoded";
            myRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.0.3705)";
            myRequest.Headers.Add("XXXXXXXXXXXXXXX", "XXXXXXXXXXXXXXX");
            myRequest.Headers.Add("Cache-control", "no-cache");
            myRequest.SendChunked = false;
            myRequest.AllowAutoRedirect = false;
            myRequest.Method = "POST";
            myRequest.CookieContainer = Cookies;
            myRequest.ContentLength = postdata.Length;
            Stream s = null;
            s = myRequest.GetRequestStream();
            s.Write(Encoding.ASCII.GetBytes(postdata), 0, postdata.Length);
            s.Close();
            HttpWebResponse MyResponse = ((HttpWebResponse) (myRequest.GetResponse()));
            Encoding enc = Encoding.GetEncoding("iso-8859-1");
            StreamReader sr = new StreamReader(MyResponse.GetResponseStream(), enc);
            loginHttpReturn = sr.ReadToEnd();
            sr.Close();
            if (!String.IsNullOrEmpty(MyResponse.Headers["set-cookie"]))
            {
                Cookies.Add(ParseCookies(MyResponse.Headers["set-cookie"]));
            }
            loginHttpReturn = GetHTTP(MyResponse.Headers["Location"], ref Cookies);
            return loginHttpReturn;
        }


        public CookieCollection ParseCookies(string cs)
        {
            CookieCollection parseCookiesReturn = null;
            Regex r = new Regex("(?<!Sun|Mon|Tue|Wed|Thu|Fri|Sat),");
            parseCookiesReturn = new CookieCollection();
            foreach (string s in r.Split(cs))
            {
                Cookie c = new Cookie();
                string transTemp12 = s.Split(';')[0];
                c.Name = transTemp12.Split("=".Split("".ToCharArray()), StringSplitOptions.None)[0];
                string transTemp15 = s.Split(';')[0];
                string transTemp16 = "=";
                c.Value = transTemp15.Split(transTemp16.Split("".ToCharArray()), StringSplitOptions.None)[1];
                c.Path = "/";
                c.Domain = "untoldoblivion.com";
                string transTemp18 = s.Substring(WiworBrowser.Controls.DBFunctions.InStr(s, ";") + 1);
                string transTemp19 = ";";
                foreach (string s2 in transTemp18.Split(transTemp19.Split("".ToCharArray()), StringSplitOptions.None))
                {
                    string transTemp21 = s2.ToLower().Trim();
                    if (transTemp21.Substring(0, 5) == "path=")
                    {
                        c.Path = s2.Substring(6);
                    }
                    string transTemp23 = s2.ToLower();
                    if (transTemp23.Substring(0, 8) == "expires=")
                    {
                        c.Expires = DateTime.Parse(s2.Substring(9));
                    }
                    string transTemp25 = s2.ToLower();
                    if (transTemp25.Substring(0, 7) == "domain=")
                    {
                        c.Domain = s2.Substring(8);
                    }
                    if (s2.ToLower() == "secure")
                    {
                        c.Secure = true;
                    }
                    string transTemp27 = s2.ToLower();
                    if (transTemp27.Substring(0, 8) == "comment=")
                    {
                        c.Comment = s2.Substring(9);
                    }
                    string transTemp29 = s2.ToLower();
                    if (transTemp29.Substring(0, 5) == "port=")
                    {
                        c.Port = s2.Substring(6);
                    }
                }
                parseCookiesReturn.Add(c);
            }
            return parseCookiesReturn;
        }


        public string GetHttpCamera(string url)
        {
            string getHTTPReturn = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);

                request.KeepAlive = true;
                request.UserAgent =
                    "Mozilla/5.0 (Windows; U; Windows NT 6.0; en-US) AppleWebKit/533.4 (KHTML, like Gecko) Chrome/5.0.375.99 Safari/533.4";
                request.Headers.Set(HttpRequestHeader.Authorization, "Basic YWRtaW46");
                request.Accept = "*/*";
                request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip,deflate,sdch");
                request.Headers.Set(HttpRequestHeader.AcceptLanguage, "es-ES,es;q=0.8");
                request.Headers.Set(HttpRequestHeader.AcceptCharset, "ISO-8859-1,utf-8;q=0.7,*;q=0.3");

                HttpWebResponse MyResponse = ((HttpWebResponse) (request.GetResponse()));

                Encoding enc = Encoding.GetEncoding("iso-8859-1");
                StreamReader sr = new StreamReader(MyResponse.GetResponseStream(), enc);
                getHTTPReturn = sr.ReadToEnd();
                sr.Close();
            }
            catch (WebException e)
            {
                return e.ToString();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return getHTTPReturn;
        }



        public string SendXML(string url, string xmlFile)
        {
            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.Load(xmlFile);

            string result = "";
            StreamWriter myWriter = null;
            string sURL = url;
            string sXML = xmlDoc.InnerXml;
            HttpWebRequest objRequest = ((HttpWebRequest)(WebRequest.Create(sURL)));

            if (sXML.Length > 1024)
            {
                MessageBox.Show("El envio de ficheros XML con SendXML esta limitado a 1024 carácteres.");
                return null;
            }

            objRequest.Method = "POST";
            objRequest.ContentLength = sXML.Length;
            objRequest.ContentType = @"application/x-www-form-urlencoded; charset=""utf-8""";


            try
            {
                myWriter = new StreamWriter(objRequest.GetRequestStream());
                myWriter.Write(sXML);
                myWriter.Flush();
                myWriter.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            try
            {
                HttpWebResponse objResponse = ((HttpWebResponse)(objRequest.GetResponse()));
                StreamReader sr = null;
                sr = new StreamReader(objResponse.GetResponseStream());
                result = sr.ReadToEnd();
                sr.Close();
            }
            catch (WebException ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return result;
        }


        public XmlDocument GetXMLDocument(string sourceFile)
        {
            try
            {
                WebRequest myRequest = WebRequest.Create(sourceFile);
                WebResponse myResponse = myRequest.GetResponse();
                XmlTextReader myReader = new XmlTextReader(myResponse.GetResponseStream());
                XmlDocument doc = new XmlDocument();
                doc.Load(myReader);
                return doc;
            }
            catch (XmlException e)
            {
                MessageBox.Show( "No se puede tener acceso al fichero XML: " + e.ToString());
                return null;
            }
        }


        public XmlTextReader GetXMLTextReader(string sourceFile)
        {
            try
            {
                WebRequest myRequest = WebRequest.Create(sourceFile);
                WebResponse myResponse = myRequest.GetResponse();
                XmlTextReader myReader = new XmlTextReader(myResponse.GetResponseStream());

                return myReader;
            }
            catch (XmlException e)
            {
                MessageBox.Show(e.ToString());
                return null;
            }
        }


        public string PostData(string url, string data)
        {
            HttpWebRequest loHttp = ((HttpWebRequest)(WebRequest.Create(url)));
            string lcPostData = HttpUtility.UrlEncode(data);

            loHttp.Method = "POST";
            byte[] lbPostBuffer = Encoding.GetEncoding(1252).GetBytes(lcPostData);
            loHttp.ContentLength = lbPostBuffer.Length;
            Stream loPostData = loHttp.GetRequestStream();
            loPostData.Write(lbPostBuffer, 0, lbPostBuffer.Length);
            loPostData.Close();

            HttpWebResponse loWebResponse = ((HttpWebResponse)(loHttp.GetResponse()));
            Encoding enc = Encoding.GetEncoding(1252);
            StreamReader loResponseStream = new StreamReader(loWebResponse.GetResponseStream(), enc);
            string lcHtml = loResponseStream.ReadToEnd();
            loWebResponse.Close();
            loResponseStream.Close();

            return lcHtml;
        }


        public void Post(string url, string postData)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] data = encoding.GetBytes(postData);

            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
            myRequest.Method = "POST";
            myRequest.ContentType = "application/x-www-form-urlencoded";
            myRequest.ContentLength = data.Length;
            Stream newStream = myRequest.GetRequestStream();
            // Send the data.
            newStream.Write(data, 0, data.Length);
            newStream.Close();
        }
    }
}