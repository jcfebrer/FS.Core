using System;
using System.Configuration;
using System.Net;

namespace FSAutomatizeWeb.DownloadManager
{
    public class BaseProtocolProvider
    {
        static BaseProtocolProvider()
        {
            ServicePointManager.DefaultConnectionLimit = int.MaxValue;
        }

        protected WebRequest GetRequest(ResourceLocation location)
        {
            WebRequest request = WebRequest.Create(location.URL);
            request.Timeout = 30000;
            SetProxy(request);
            return request;
        }

        protected void SetProxy(WebRequest request)
        {
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["UseProxy"]))
            {
                WebProxy proxy = new WebProxy(ConfigurationManager.AppSettings["ProxyAddress"], Convert.ToInt32(ConfigurationManager.AppSettings["ProxyPort"]));
                proxy.BypassProxyOnLocal = Convert.ToBoolean(ConfigurationManager.AppSettings["ProxyByPassOnLocal"]);
                request.Proxy = proxy;

                if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["ProxyUserName"]))
                {
                    request.Proxy.Credentials = new NetworkCredential(
                        ConfigurationManager.AppSettings["ProxyUserName"],
                        ConfigurationManager.AppSettings["ProxyPassword"],
                        ConfigurationManager.AppSettings["ProxyDomain"]);
                }
            }
        }
    }
}
