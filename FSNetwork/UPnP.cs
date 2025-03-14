using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Xml;
using System.IO;
using System.Threading.Tasks;
using FSException;
using System.Threading;
using System.Diagnostics;
using FSTrace;

namespace FSNetwork
{
    public class UPnP
    {
        public event EventHandler<UPnPDeviceEventArgs> OnDeviceFound;
        private string serviceUrl = null;
        private string externalIp = null;
        private int timeoutInSecs = 5;
        private IPAddress gateway = null;
        private bool stopInFirstFind = true;

        private string ssdp_discover = "ssdp:discover";

        // Tipo de dispositivos a descubrir:
        private string st_discover = "upnp:rootdevice";
        //ssdp:all
        //urn:schemas-upnp-org:device:MediaRenderer:1
        //urn:schemas-upnp-org:service:WANCommonInterfaceConfig:1
        //urn:schemas-upnp-org:device:InternetGatewayDevice:1
        //upnp:rootdevice

        public bool StopInFirstFind
        {
            get { return stopInFirstFind; }
            set { stopInFirstFind = value; }
        }

        public string DiscoverType
        {
            get { return st_discover; }
            set { st_discover = value; }
        }

        public IPAddress Gateway
        {
            get { return gateway; }
            set { gateway = value; }
        }

        public int DiscoverTimeoutInSecs
        {
            get { return timeoutInSecs; }
            set { timeoutInSecs = value; }
        }

        // segundos de espera en responder
        private int mx_discover = 3;

        private const int port = 1900;
        private const string multicastAddress = "239.255.255.250";

        private IPEndPoint multicastEndPoint = new IPEndPoint(IPAddress.Parse(multicastAddress), port);

        public bool Discover()
        {
            try
            {
                return DiscoverSync();
            }
            catch (Exception ex)
            {
                Log.TraceError(ex);
                return false;
            }
        }

        /// <summary>
        /// Descubre dispositivos UPnP y emite un evento por cada dispositivo encontrado
        /// </summary>
        public async Task<bool> DiscoverAsync()
        {
            try
            {
                if (gateway != null)
                    multicastEndPoint = new IPEndPoint(gateway, port);
                else
                    multicastEndPoint = new IPEndPoint(IPAddress.Parse(multicastAddress), 1900);

                bool result = false;

            using (UdpClient udpClient = new UdpClient())
            {
                    //udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                    //udpClient.Client.ReceiveTimeout = timeoutInSecs * 1000;
                    //udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, 0));

                udpClient.EnableBroadcast = true;

                    byte[] requestData = Encoding.UTF8.GetBytes(SearchMessage());

                await udpClient.SendAsync(requestData, requestData.Length, multicastEndPoint);

                    //DateTime startTime = DateTime.Now;
                    //TimeSpan timeout = TimeSpan.FromSeconds(timeoutInSecs);

                    while (true) //while ((DateTime.Now - startTime) < timeout)
                {
                var receiveTask = udpClient.ReceiveAsync();

                        if (await Task.WhenAny(receiveTask, Task.Delay(timeoutInSecs * 1000)) != receiveTask)
                            break;

                    UdpReceiveResult response = receiveTask.Result;
                    string responseText = Encoding.UTF8.GetString(response.Buffer);

                    if (responseText.ToLower().Contains("location:"))
                    {
                        string locationUrl = ExtractLocationUrl(responseText);
                        if (locationUrl != null)
                        {
                            bool success = await ParseGatewayAsync(locationUrl);
                            if (success)
                            {
                                // Emitir evento con información del dispositivo
                                    OnDeviceFound?.Invoke(this, new UPnPDeviceEventArgs(response.RemoteEndPoint.Address.ToString(), locationUrl, serviceUrl, responseText));

                                    if (stopInFirstFind)
                                        return true;
                                    else
                                        result = true;
                                }
                            }
                        }
                        else
                        {
                            // Emitir evento con información del dispositivo
                            OnDeviceFound?.Invoke(this, new UPnPDeviceEventArgs(response.RemoteEndPoint.Address.ToString(), null, null, responseText));
                        }
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.TraceError(ex);
                return false;
            }
}

        public bool DiscoverSync()
        {
            try
            {
                if (gateway != null)
                    multicastEndPoint = new IPEndPoint(gateway, port);
                else
                    multicastEndPoint = new IPEndPoint(IPAddress.Parse(multicastAddress), 1900);

                bool result = false;

                using (UdpClient udpClient = new UdpClient())
                {
                    //udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                    //udpClient.Client.ReceiveTimeout = timeoutInSecs * 1000;
                    //udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, 0));

                    udpClient.EnableBroadcast = true;

                    byte[] requestData = Encoding.UTF8.GetBytes(SearchMessage());

                    udpClient.Send(requestData, requestData.Length, multicastEndPoint);

                    var receiveBuffer = new byte[8192];
                    IPEndPoint senderEndPoint = new IPEndPoint(IPAddress.Any, 0);

                    DateTime startTime = DateTime.Now;
                    TimeSpan timeout = TimeSpan.FromSeconds(timeoutInSecs);

                    while ((DateTime.Now - startTime) < timeout)
                    {
                        if (udpClient.Available > 0)
                        {
                            receiveBuffer = udpClient.Receive(ref senderEndPoint);
                            string responseText = Encoding.UTF8.GetString(receiveBuffer);

                            if (responseText.ToLower().Contains("location:"))
                            {
                                string locationUrl = ExtractLocationUrl(responseText);
                                if (locationUrl != null)
                                {
                                    bool success = ParseGateway(locationUrl);
                                    if (success)
                                    {
                                        OnDeviceFound?.Invoke(this, new UPnPDeviceEventArgs(senderEndPoint.Address.ToString(), locationUrl, serviceUrl, responseText));

                                        if (stopInFirstFind)
                                            return true;
                                        else
                                            result = true;
                                    }
                                }
                            }
                            else
                            {
                                // Emitir evento con información del dispositivo
                                OnDeviceFound?.Invoke(this, new UPnPDeviceEventArgs(senderEndPoint.Address.ToString(), null, null, responseText));
                            }
                        }
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.TraceError(ex);
                return false;
            }
}

        private string SearchMessage()
        {
            string searchMessage = $@"M-SEARCH * HTTP/1.1
HOST: {multicastAddress}:{port}
MAN: ""{ssdp_discover}""
MX: {mx_discover}
ST: {st_discover}

";
            return searchMessage;
        }

        /// <summary>
        /// Extrae la URL de la respuesta SSDP.
        /// </summary>
        private string ExtractLocationUrl(string response)
        {
            int startIndex = response.ToLower().IndexOf("location:") + 9;
            int endIndex = response.IndexOfAny(new[] { '\r', '\n' }, startIndex);
            return (startIndex > 8 && endIndex > startIndex) ? response.Substring(startIndex, endIndex - startIndex).Trim() : null;
        }

        private async Task<bool> ParseGatewayAsync(string url)
        {
            return ParseGateway(url);
        }

        private bool ParseGateway(string url)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(url);

                XmlNamespaceManager nsManager = new XmlNamespaceManager(xmlDoc.NameTable);
                nsManager.AddNamespace("tns", "urn:schemas-upnp-org:device-1-0");

                XmlNode serviceNode = xmlDoc.SelectSingleNode("//tns:service[tns:serviceType='urn:schemas-upnp-org:service:WANIPConnection:1']/tns:controlURL", nsManager);
                if (serviceNode != null)
                {
                    serviceUrl = CombineUrls(url, serviceNode.InnerText);
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil($"Error al analizar el XML del router: {ex.Message}");
            }
            return false;
        }

        public bool AddPortMapping(int externalPort, int internalPort, string localIp, string description = "UPnP Port Forward")
        {
            CheckServiceUrl();

            string soapBody = $@"
            <u:AddPortMapping xmlns:u='urn:schemas-upnp-org:service:WANIPConnection:1'>
                <NewRemoteHost></NewRemoteHost>
                <NewExternalPort>{externalPort}</NewExternalPort>
                <NewProtocol>TCP</NewProtocol>
                <NewInternalPort>{internalPort}</NewInternalPort>
                <NewInternalClient>{localIp}</NewInternalClient>
                <NewEnabled>1</NewEnabled>
                <NewPortMappingDescription>{description}</NewPortMappingDescription>
                <NewLeaseDuration>0</NewLeaseDuration>
            </u:AddPortMapping>";

            return SendSoapRequest("AddPortMapping", soapBody);
        }

        public bool DeletePortMapping(int externalPort)
        {
            CheckServiceUrl();

            string soapBody = $@"
            <u:DeletePortMapping xmlns:u='urn:schemas-upnp-org:service:WANIPConnection:1'>
                <NewRemoteHost></NewRemoteHost>
                <NewExternalPort>{externalPort}</NewExternalPort>
                <NewProtocol>TCP</NewProtocol>
            </u:DeletePortMapping>";

            return SendSoapRequest("DeletePortMapping", soapBody);
        }

        public string GetExternalIPAddress()
        {
            CheckServiceUrl();

            string soapBody = "<u:GetExternalIPAddress xmlns:u='urn:schemas-upnp-org:service:WANIPConnection:1'></u:GetExternalIPAddress>";

            XmlDocument responseXml = SendSoapRequestXml("GetExternalIPAddress", soapBody);
            if (responseXml != null)
            {
                XmlNode ipNode = responseXml.SelectSingleNode("//NewExternalIPAddress");
                if (ipNode != null)
                    externalIp = ipNode.InnerText;
            }
            return externalIp;
        }

        private void CheckServiceUrl()
        {
            if (serviceUrl == null)
                throw new ExceptionUtil("La url de servicio es null. ¿Esta activo y disponible tu router con UPnP activado?");
        }

        private bool SendSoapRequest(string action, string body)
        {
            return SendSoapRequestXml(action, body) != null;
        }

        private XmlDocument SendSoapRequestXml(string action, string body)
        {
            try
            {
                if(serviceUrl == null)
                    throw new ExceptionUtil("La url de servicio es null. ¿Esta activo y disponible tu router con UPnP activado?");

                string soapMessage = $@"<?xml version='1.0'?>
                <s:Envelope xmlns:s='http://schemas.xmlsoap.org/soap/envelope/' s:encodingStyle='http://schemas.xmlsoap.org/soap/encoding/'>
                    <s:Body>{body}</s:Body>
                </s:Envelope>";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceUrl);
                request.Method = "POST";
                request.Headers.Add("SOAPAction", $"\"urn:schemas-upnp-org:service:WANIPConnection:1#{action}\"");
                request.ContentType = "text/xml; charset=\"utf-8\"";
                byte[] soapBytes = Encoding.UTF8.GetBytes(soapMessage);
                request.ContentLength = soapBytes.Length;

                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(soapBytes, 0, soapBytes.Length);
                }

                XmlDocument responseXml = new XmlDocument();
                using (WebResponse response = request.GetResponse())
                {
                    responseXml.Load(response.GetResponseStream());
                }
                return responseXml;
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil($"Error en SOAP Request ({action}): {ex.Message}");
            }
        }

        private string CombineUrls(string baseUrl, string relativePath)
        {
            Uri baseUri = new Uri(baseUrl);
            return new Uri(baseUri, relativePath).ToString();
        }
    }

    /// <summary>
    /// Clase para almacenar información de un dispositivo UPnP detectado.
    /// </summary>
    public class UPnPDeviceEventArgs : EventArgs
    {
        public string IPAddress { get; }
        public string LocationUrl { get; }
        public string ServiceUrl { get; }
        public string ResponseText { get; }

        public UPnPDeviceEventArgs(string ipAddress, string locationUrl, string serviceUrl, string responseText)
        {
            IPAddress = ipAddress;
            LocationUrl = locationUrl;
            ServiceUrl = serviceUrl;
            ResponseText = responseText;
        }
}
}