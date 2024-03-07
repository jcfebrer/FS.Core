#region

using FSExceptionCore;
using FSLibraryCore;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;

#endregion

namespace FSNetworkCore
{
    public static class Http
    {
        #region "DBHttp Web"

        public static string GetFromUrl(string url)
        {
            return GetFromUrl(url, Encoding.Default, "", "");
        }

        public static string GetFromUrl(string url, string user, string password)
        {
            return GetFromUrl(url, Encoding.Default, user, password);
        }

        public static string GetFromUrl(string url, Encoding enc)
        {
            return GetFromUrl(url, enc, "", "");
        }

        public static string GetFromUrl(string url, Encoding enc, string user, string password)
        {
            HttpWebRequest myRequest = ((HttpWebRequest)(WebRequest.Create(url)));
            myRequest.Method = "GET";

            if (!String.IsNullOrEmpty(user))
                myRequest.Credentials = new NetworkCredential(user, password);

            HttpWebResponse myResponse = ((HttpWebResponse)(myRequest.GetResponse()));
            Stream receiveStream = myResponse.GetResponseStream();
            StreamReader readStream = new StreamReader(receiveStream, enc);
            string responseText = readStream.ReadToEnd();

            myResponse.Close();
            readStream.Close();

            return responseText;
        }

        public static string PostHttp(string url, string postdata)
        {
            string postHttpReturn = null;

            FSLibrary.TextUtil.Replace(url, "//", "/");
            HttpWebRequest myRequest = ((HttpWebRequest)(WebRequest.Create(url)));
            myRequest.ContentType = "application/x-www-form-urlencoded";
            myRequest.Headers["User-Agent"] = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.0.3705)";
            myRequest.Headers.Add("XXXXXXXXXXXXXXX", "XXXXXXXXXXXXXXX");
            myRequest.Headers.Add("Cache-control", "no-cache");
            myRequest.AllowAutoRedirect = true;
            myRequest.Method = "POST";
            myRequest.ContentLength = postdata.Length;
            Stream s = null;
            s = myRequest.GetRequestStream();
            s.Write(Encoding.ASCII.GetBytes(postdata), 0, postdata.Length);
            s.Close();
            HttpWebResponse MyResponse = ((HttpWebResponse)(myRequest.GetResponse()));
            Encoding enc = Encoding.GetEncoding("iso-8859-1");
            StreamReader sr = new StreamReader(MyResponse.GetResponseStream(), enc);
            postHttpReturn = sr.ReadToEnd();
            sr.Close();
            return postHttpReturn;
        }

        #endregion

        public static string GetHTTP(string url, ref CookieContainer Cookies, Encoding enc)
        {
            string getHTTPReturn = null;

            FSLibrary.TextUtil.Replace(url, "//", "/");
            HttpWebRequest myRequest = (HttpWebRequest)(WebRequest.Create(url));
            myRequest.Headers["User-Agent"] = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.0.3705)";
            myRequest.Headers.Add("XXXXXXXXXXXXXXX", "XXXXXXXXXXXXXXX");
            myRequest.Headers.Add("Cache-control", "no-cache");

            if (!(Cookies == null))
            {
                myRequest.CookieContainer = Cookies;
            }

            HttpWebResponse MyResponse = ((HttpWebResponse)(myRequest.GetResponse()));

            StreamReader sr = new StreamReader(MyResponse.GetResponseStream(), enc);
            getHTTPReturn = sr.ReadToEnd();
            sr.Close();

            if (Cookies != null)
            {
                if (!String.IsNullOrEmpty(MyResponse.Headers["set-cookie"]))
                {
                    Cookies.Add(ParseCookies(MyResponse.Headers["set-cookie"]));
                }
            }
            return getHTTPReturn;
        }


        public static string GetHTTP(string url)
        {
            CookieContainer cookie = null;
            return GetHTTP(url, ref cookie, Encoding.UTF8);
        }

        public static string GetHTTP(string url, Encoding enc)
        {
            CookieContainer cookie = null;
            return GetHTTP(url, ref cookie, enc);
        }


        public static string PostHttp(string url, string postdata, ref CookieContainer Cookies)
        {
            string postHttpReturn = null;

            FSLibrary.TextUtil.Replace(url, "//", "/");
            HttpWebRequest myRequest = ((HttpWebRequest)(WebRequest.Create(url)));
            myRequest.ContentType = "application/x-www-form-urlencoded";
            myRequest.Headers["User-Agent"] = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.0.3705)";
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
            HttpWebResponse MyResponse = ((HttpWebResponse)(myRequest.GetResponse()));
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


        public static string LoginHttp(string url, string postdata, ref CookieContainer Cookies, Encoding enc)
        {
            string loginHttpReturn = null;

            FSLibrary.TextUtil.Replace(url, "//", "/");
            HttpWebRequest myRequest = ((HttpWebRequest)(WebRequest.Create(url)));
            myRequest.ContentType = "application/x-www-form-urlencoded";
            myRequest.Headers["User-Agent"] = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.0.3705)";
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
            HttpWebResponse MyResponse = ((HttpWebResponse)(myRequest.GetResponse()));

            StreamReader sr = new StreamReader(MyResponse.GetResponseStream(), enc);
            loginHttpReturn = sr.ReadToEnd();
            sr.Close();
            if (!String.IsNullOrEmpty(MyResponse.Headers["set-cookie"]))
            {
                Cookies.Add(ParseCookies(MyResponse.Headers["set-cookie"]));
            }
            loginHttpReturn = GetHTTP(MyResponse.Headers["Location"], ref Cookies, enc);
            return loginHttpReturn;
        }


        public static CookieCollection ParseCookies(string cs)
        {
            CookieCollection parseCookiesReturn = null;
            Regex r = new Regex("(?<!Sun|Mon|Tue|Wed|Thu|Fri|Sat),");
            parseCookiesReturn = new CookieCollection();
            foreach (string s in r.Split(cs))
            {
                Cookie c = new Cookie();
                string transTemp12 = s.Split(';')[0];
                c.Name = transTemp12.Split('=')[0];
                string transTemp15 = s.Split(';')[0];
                c.Value = transTemp15.Split('=')[1];
                c.Path = "/";
                c.Domain = "untoldoblivion.com";
                string transTemp18 = s.Substring(FSLibrary.TextUtil.IndexOf(s, ";") + 1);
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
                        c.Expires = System.DateTime.Parse(s2.Substring(9));
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


        public static string GetHttpCamera(string url)
        {
            string getHTTPReturn = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                request.KeepAlive = true;
                request.Headers["User-Agent"] =
                    "Mozilla/5.0 (Windows; U; Windows NT 6.0; en-US) AppleWebKit/533.4 (KHTML, like Gecko) Chrome/5.0.375.99 Safari/533.4";
                request.Headers.Set(HttpRequestHeader.Authorization, "Basic YWRtaW46");
                request.Accept = "*/*";
                request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip,deflate,sdch");
                request.Headers.Set(HttpRequestHeader.AcceptLanguage, "es-ES,es;q=0.8");
                request.Headers.Set(HttpRequestHeader.AcceptCharset, "ISO-8859-1,utf-8;q=0.7,*;q=0.3");

                HttpWebResponse MyResponse = ((HttpWebResponse)(request.GetResponse()));

                Encoding enc = Encoding.GetEncoding("iso-8859-1");
                StreamReader sr = new StreamReader(MyResponse.GetResponseStream(), enc);
                getHTTPReturn = sr.ReadToEnd();
                sr.Close();
            }
            catch (WebException e)
            {
                using (WebResponse response2 = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response2;
                    if (httpResponse != null)
                    {
                        Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
                        using (Stream data = response2.GetResponseStream())
                        using (var reader2 = new StreamReader(data))
                        {
                            string text = reader2.ReadToEnd();
                            return text;
                        }
                    }
                    else
                        e.ToString();
                }
            }
            catch (System.Exception ex)
            {
                return ex.ToString();
            }

            return getHTTPReturn;
        }



        public static string SendXML(string url, string xmlFile)
        {
            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.Load(xmlFile);

            string result = "";
            StreamWriter myWriter = null;
            string sURL = url;
            string sXML = xmlDoc.InnerXml;
            HttpWebRequest objRequest = ((HttpWebRequest)(WebRequest.Create(sURL)));

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
            catch (System.Exception e)
            {
                throw new ExceptionUtil(e);
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
                using (WebResponse response2 = ex.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response2;
                    if (httpResponse != null)
                    {
                        Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
                        using (Stream data = response2.GetResponseStream())
                        using (var reader2 = new StreamReader(data))
                        {
                            string text = reader2.ReadToEnd();
                            throw new ExceptionUtil(text);
                        }
                    }
                    else
                        throw new ExceptionUtil(ex);
                }
            }

            return result;
        }


        public static XmlDocument GetXMLDocument(string sourceFile)
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
                throw new ExceptionUtil("No se puede tener acceso al fichero XML: " + sourceFile, e);
            }
        }


        public static XmlTextReader GetXMLTextReader(string sourceFile)
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
                throw new ExceptionUtil("No se puede tener acceso al fichero XML: " + sourceFile, e);
            }
        }


        public static string PostData(string url, string data)
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


        public static void Post(string url, string data)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] bytes = encoding.GetBytes(data);

            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
            myRequest.Method = "POST";
            myRequest.ContentType = "application/x-www-form-urlencoded";
            myRequest.ContentLength = bytes.Length;
            Stream newStream = myRequest.GetRequestStream();
            // Send the data.
            newStream.Write(bytes, 0, bytes.Length);
            newStream.Close();
        }

        public static string SendXMLFile(string xmlFilepath, string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);

            request.KeepAlive = false;
            request.ProtocolVersion = HttpVersion.Version10;
            request.ContentType = "application/xml";
            request.Method = "POST";

            StringBuilder sb = new StringBuilder();
            using (StreamReader sr = new StreamReader(xmlFilepath))
            {
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    sb.AppendLine(line);
                }
                byte[] postBytes = Encoding.UTF8.GetBytes(sb.ToString());

                request.ReadWriteTimeout = 500;
                request.Timeout = 500;

                request.ContentLength = postBytes.Length;

                try
                {
                    Stream requestStream = request.GetRequestStream();

                    requestStream.Write(postBytes, 0, postBytes.Length);
                    requestStream.Close();

                    using (var response = (HttpWebResponse)request.GetResponse())
                    {
                        return response.ToString();
                    }
                }
                catch (ExceptionUtil ex)
                {
                    request.Abort();
                    return "Error: " + ex.Message;
                }
            }
        }


        public static void HttpUploadFile(string url, string file, NameValueCollection formValues, X509Certificate2 certificate)
        {
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            wr.ContentType = "multipart/form-data; boundary=" + boundary;
            wr.Method = "POST";
            wr.KeepAlive = true;
            wr.Credentials = System.Net.CredentialCache.DefaultCredentials;

            if (certificate != null)
                wr.ClientCertificates.Add(certificate);

            Stream rs = wr.GetRequestStream();

            if (formValues != null)
            {
                foreach (string key in formValues.Keys)
                {
                    rs.Write(boundarybytes, 0, boundarybytes.Length);
                    string formitem = string.Format("Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}", key, formValues[key]);
                    byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                    rs.Write(formitembytes, 0, formitembytes.Length);
                }
            }
            rs.Write(boundarybytes, 0, boundarybytes.Length);

            string header = string.Format("Content-Disposition: form-data; name=\"file\"; filename=\"{0}\"\r\nContent-Type: {1}\r\n\r\n", Path.GetFileName(file), Path.GetExtension(file));
            byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
            rs.Write(headerbytes, 0, headerbytes.Length);

            FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[4096];
            int bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                rs.Write(buffer, 0, bytesRead);
            }
            fileStream.Close();

            byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            rs.Write(trailer, 0, trailer.Length);
            rs.Close();

            WebResponse wresp = null;
            try
            {
                wresp = wr.GetResponse();
                Stream stream2 = wresp.GetResponseStream();
                StreamReader reader2 = new StreamReader(stream2);
                //"File uploaded, server response is: {0}", reader2.ReadToEnd()));
            }
            catch (Exception)
            {
                //"Error uploading file", ex);
                if (wresp != null)
                {
                    wresp.Close();
                    wresp = null;
                }
            }
            finally
            {
                wr = null;
            }
        }

        public static string GetRequest(string url)
        {
            Encoding enc = Encoding.GetEncoding("UTF-8");

            WebRequest req = WebRequest.Create(url);
            WebResponse res = req.GetResponse();

            Stream st = res.GetResponseStream();
            StreamReader sr = new StreamReader(st, enc);
            string html = sr.ReadToEnd();
            sr.Close();
            st.Close();

            return html;
        }

        //public static bool IsLocalhost()
        //{
        //    string host = HttpContext.Current.Request.Url.Host;
        //    return (host == null || FSLibrary.TextUtil.Substring(host, 0, 9) == "localhost" ||
        //        FSLibrary.TextUtil.Substring(host, 0, 9) == "127.0.0.1" || FSLibrary.TextUtil.Substring(host, 0, 3) == "10." ||
        //        FSLibrary.TextUtil.Substring(host, 0, 4) == "192.");
        //}

        //public static string IpAddress()
        //{
        //    string strIp = null;
        //    HttpContext context = new HttpContext();
        //    strIp = FSLibrary.Functions.Valor(context.GetServerVariable("HTTP_X_FORWARDED_FOR"));
        //    if (strIp == "")
        //    {
        //        strIp = FSLibrary.Functions.Valor(context.GetServerVariable("REMOTE_ADDR"));
        //    }
        //    if (strIp == "::1") strIp = "127.0.0.1";
        //    return strIp;
        //}

        //public static string ToAbsoluteUrl(string relativeUrl)
        //{
        //    if (string.IsNullOrEmpty(relativeUrl))
        //        return relativeUrl;

        //    //otra manera
        //    //"src=\"" + context.Request.Url.Scheme + "://" +
        //    //context.Request.Url.Authority + src + "\"";

        //    if (HttpContext.Current == null)
        //        return relativeUrl;

        //    if (relativeUrl.StartsWith("/"))
        //        relativeUrl = relativeUrl.Insert(0, "~");
        //    if (!relativeUrl.StartsWith("~/"))
        //        relativeUrl = relativeUrl.Insert(0, "~/");

        //    Uri url = HttpContext.Current.Request.Url;
        //    string port = url.Port != 80 ? (":" + url.Port) : String.Empty;

        //    return String.Format("{0}://{1}{2}{3}",
        //        url.Scheme, url.Host, port, VirtualPathUtility.ToAbsolute(relativeUrl));
        //}

        public static string ReadHeaders(WebHeaderCollection headers)
        {
            string headerText = "";
            for (int i = 0; i < headers.Count; ++i)
            {
                string headerName = headers.GetKey(i);
                foreach (string value in headers.GetValues(i))
                {
                    headerText += String.Format("{0}: {1}" + Environment.NewLine, headerName, value);
                }
            }
            return headerText;
        }
    }
}