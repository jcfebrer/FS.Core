using System;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Xml;
using System.IO;
using System.Threading;
using FSExceptionCore;

namespace FSNetworkCore
{
    public class UPnP2
    {
        static TimeSpan _timeout = new TimeSpan(0, 0, 0, 3);
        public delegate void MessageLogTextEventHandler(object source, DeviceUPnP e);
        public static event MessageLogTextEventHandler OnDeviceSearch;
        public static ConnectionTypeEnum ConnectionType = UPnP2.ConnectionTypeEnum.WANIPConnection;

        public enum ConnectionTypeEnum
        {
            WANIPConnection,
            WANPPPConnection
        }

        public static TimeSpan TimeOut
        {
            get { return _timeout; }
            set { _timeout = value; }
        }

        public class DeviceUPnP
        {
            private string m_Ip;
            private string m_ServiceUrl;
            private string m_Data;
            private bool m_UPnp;

            public DeviceUPnP(string Ip, string ServiceUrl, string Data, bool UPnp)
            {
                m_Ip = Ip;
                m_ServiceUrl = ServiceUrl;
                m_Data = Data;
                m_UPnp = UPnp;
            }

            public string Ip
            {
                get { return m_Ip; }
                set { m_Ip = value; }
            }

            public string ServiceUrl
            {
                get { return m_ServiceUrl; }
                set { m_ServiceUrl = value; }
            }

            public string Data
            {
                get { return m_Data; }
                set { m_Data = value; }
            }

            public bool UPnp
            {
                get { return m_UPnp; }
                set { m_UPnp = value; }
            }
        }

        public static bool Discover()
        {
            return Discover(null);
        }
        public static bool Discover(IPAddress defaultGateway)
        {
            string host = "239.255.255.250";
            int port = 1900;
            IPEndPoint LocalEndPoint = new IPEndPoint(Net.GetInternetIPAddress(), 0);
            IPEndPoint MulticastEndPoint;

            if (defaultGateway != null)
                MulticastEndPoint = new IPEndPoint(defaultGateway, port);
            else
                MulticastEndPoint = new IPEndPoint(IPAddress.Parse(host), port);

            Socket UdpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            UdpSocket.Bind(LocalEndPoint);

            UdpSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

            if (defaultGateway == null)
            {
                UdpSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(MulticastEndPoint.Address, IPAddress.Any));
                UdpSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, 2);
                UdpSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastLoopback, true);
            }

            UdpSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 1500);

            // todos los dispositivos UPnP = ST: ssdp:all
            // dispositivos de tipo WAN = ST: service:WANIPConnection:1
            string SearchString = "M-SEARCH * HTTP/1.1\r\n" + 
                "HOST: {host}:{port}\r\n" + 
                "MAN: \"ssdp:discover\"\r\n" + 
                "ST: upnp:rootdevice\r\n" +
                "MX: 3\r\n\r\n";

            
            if (defaultGateway != null)
                host = defaultGateway.ToString();

            SearchString = SearchString.Replace("{host}", host);
            SearchString = SearchString.Replace("{port}", port.ToString());

            //ST: urn:schemas-upnp-org:device:MediaRenderer:1
            UdpSocket.SendTo(Encoding.UTF8.GetBytes(SearchString), SocketFlags.None, MulticastEndPoint);
            
            byte[] ReceiveBuffer = new byte[64000];
            int ReceivedBytes = 0;

            while (true)
            {
                if (UdpSocket.Available > 0)
                {
                    ReceivedBytes = UdpSocket.Receive(ReceiveBuffer, SocketFlags.None);

                    if (ReceivedBytes > 0)
                    {
                        string data = Encoding.UTF8.GetString(ReceiveBuffer, 0, ReceivedBytes);
                        data = data.ToLower();

                        if (data.Contains("upnp:rootdevice"))
                        {
                            string ip = data.Substring(data.IndexOf("location:") + 9);
                            ip = ip.Substring(0, ip.IndexOf("\r")).Trim();

                            if (OnDeviceSearch != null)
                                OnDeviceSearch.Invoke(null, new DeviceUPnP(ip, null, data, false));

                            string _serviceUrl = GetServiceUrl(ip);

                            if (!string.IsNullOrEmpty(_serviceUrl))
                            {
                                if (OnDeviceSearch != null)
                                    OnDeviceSearch.Invoke(null, new DeviceUPnP(ip, _serviceUrl, data, true));

                                return true;
                            }
                        }
                    }
                    //else
                    //{
                    //    return false;
                    //}
                }
                //else
                //{
                //    return false;
                //}
            }
        }

        private static string GetServiceUrl(string resp)
        {
            try
            {
                XmlDocument desc = new XmlDocument();
                try
                {
                    desc.Load(WebRequest.Create(resp).GetResponse().GetResponseStream());
                }
                catch
                {
                    return null;
                }
                XmlNamespaceManager nsMgr = new XmlNamespaceManager(desc.NameTable);
                nsMgr.AddNamespace("tns", "urn:schemas-upnp-org:device-1-0");
                XmlNode typen = desc.SelectSingleNode("//tns:device/tns:deviceType/text()", nsMgr);
                if (typen == null) return null;
                if (!typen.Value.Contains("InternetGatewayDevice"))
                    return null;
                XmlNode node = desc.SelectSingleNode("//tns:service[tns:serviceType=\"urn:schemas-upnp-org:service:" + ConnectionType.ToString() + ":1\"]/tns:controlURL/text()", nsMgr);
                if (node == null)
                {
                    ConnectionType = ConnectionTypeEnum.WANPPPConnection;
                    node = desc.SelectSingleNode("//tns:service[tns:serviceType=\"urn:schemas-upnp-org:service:" + ConnectionType.ToString() + ":1\"]/tns:controlURL/text()", nsMgr);
                }
                if (node == null)
                    return null;
                //XmlNode eventnode = desc.SelectSingleNode("//tns:service[tns:serviceType=\"urn:schemas-upnp-org:service:" + ConnectionType.ToString() + ":1\"]/tns:eventSubURL/text()", nsMgr);
                //string eventUrl = CombineUrls(resp, eventnode.Value);
                return CombineUrls(resp, node.Value);
            }
            catch { return null; }
        }

        private static string CombineUrls(string resp, string p)
        {
            int n = resp.IndexOf("://");
            n = resp.IndexOf('/', n + 3);
            string url = resp.Substring(0, n) + p;
            if (!url.ToLower().EndsWith(".xml"))
                url += ".xml";
            return url;
        }

        public static void ForwardPort(DeviceUPnP device, int port, ProtocolType protocol, string description)
        {
            if (device == null || string.IsNullOrEmpty(device.ServiceUrl))
                throw new ExceptionUtil("No UPnP service available or Discover() has not been called");
            XmlDocument xdoc = SOAPRequest(device.ServiceUrl, "<u:AddPortMapping xmlns:u=\"urn:schemas-upnp-org:service:" + ConnectionType.ToString() + ":1\">" +
                "<NewRemoteHost></NewRemoteHost><NewExternalPort>" + port.ToString() + "</NewExternalPort><NewProtocol>" + protocol.ToString().ToUpper() + "</NewProtocol>" +
                "<NewInternalPort>" + port.ToString() + "</NewInternalPort><NewInternalClient>" + Dns.GetHostAddresses(Dns.GetHostName())[0].ToString() +
                "</NewInternalClient><NewEnabled>1</NewEnabled><NewPortMappingDescription>" + description +
            "</NewPortMappingDescription><NewLeaseDuration>0</NewLeaseDuration></u:AddPortMapping>", "AddPortMapping");
        }

        public static void DeleteForwardingRule(DeviceUPnP device, int port, ProtocolType protocol)
        {
            if (device == null || string.IsNullOrEmpty(device.ServiceUrl))
                throw new ExceptionUtil("No UPnP service available or Discover() has not been called");
            XmlDocument xdoc = SOAPRequest(device.ServiceUrl,
            "<u:DeletePortMapping xmlns:u=\"urn:schemas-upnp-org:service:" + ConnectionType.ToString() + ":1\">" +
            "<NewRemoteHost>" +
            "</NewRemoteHost>" +
            "<NewExternalPort>" + port + "</NewExternalPort>" +
            "<NewProtocol>" + protocol.ToString().ToUpper() + "</NewProtocol>" +
            "</u:DeletePortMapping>", "DeletePortMapping");
        }

        public static IPAddress GetExternalIP(DeviceUPnP device)
        {
            if (device == null || string.IsNullOrEmpty(device.ServiceUrl))
                throw new ExceptionUtil("No UPnP service available or Discover() has not been called");
            XmlDocument xdoc = SOAPRequest(device.ServiceUrl, "<u:GetExternalIPAddress xmlns:u=\"urn:schemas-upnp-org:service:" + ConnectionType.ToString() + ":1\">" + "\r\n" +
            "</u:GetExternalIPAddress>", "GetExternalIPAddress");
            XmlNamespaceManager nsMgr = new XmlNamespaceManager(xdoc.NameTable);
            nsMgr.AddNamespace("tns", "urn:schemas-upnp-org:device-1-0");
            string IP = xdoc.SelectSingleNode("//NewExternalIPAddress/text()", nsMgr).Value;
            return IPAddress.Parse(IP);
        }

        private static XmlDocument SOAPRequest(string url, string soap, string function)
        {
            string req = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + "\r\n" +
            "<s:Envelope xmlns:s=\"http://schemas.xmlsoap.org/soap/envelope/\" s:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\">" + "\r\n" +
            "<s:Body>" + "\r\n" +
            soap + "\r\n" +
            "</s:Body>" + "\r\n" +
            "</s:Envelope>" + "\r\n" + "\r\n";
            HttpWebRequest r = (HttpWebRequest)HttpWebRequest.Create(url);
            r.ServicePoint.Expect100Continue = false;
            r.Method = "POST";
            byte[] b = Encoding.UTF8.GetBytes(req);
            r.Headers.Add("SOAPAction", "\"urn:schemas-upnp-org:service:" + ConnectionType.ToString() + ":1#" + function + "\"");
            r.ContentType = "text/xml; charset=\"utf-8\"";
            r.ContentLength = b.Length;
            r.GetRequestStream().Write(b, 0, b.Length);
            XmlDocument resp = new XmlDocument();
            WebResponse wres = r.GetResponse();
            Stream ress = wres.GetResponseStream();
            resp.Load(ress);
            return resp;
        }
    }
}
