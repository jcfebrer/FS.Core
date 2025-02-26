using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Xml;
using System.IO;
using System.Threading.Tasks;
using FSException;

namespace FSNetwork
{
    public class UPnP
    {
        public event EventHandler<UPnPDeviceEventArgs> OnDeviceFound;
        private string serviceUrl = null;
        private string externalIp = null;

        public bool Discover()
        {
            return DiscoverAsync().Result;
        }

        /// <summary>
        /// Descubre dispositivos UPnP y emite un evento por cada dispositivo encontrado
        /// </summary>
        public async Task<bool> DiscoverAsync()
        {
            bool result = false;
            string searchMessage = "M-SEARCH * HTTP/1.1\r\n" +
                                   "HOST: 239.255.255.250:1900\r\n" +
                                   "MAN: \"ssdp:discover\"\r\n" +
                                   "MX: 2\r\n" +
                                   "ST: urn:schemas-upnp-org:device:InternetGatewayDevice:1\r\n\r\n";

            using (UdpClient udpClient = new UdpClient())
            {
                udpClient.EnableBroadcast = true;
                IPEndPoint multicastEndPoint = new IPEndPoint(IPAddress.Parse("239.255.255.250"), 1900);
                byte[] requestData = Encoding.UTF8.GetBytes(searchMessage);

                await udpClient.SendAsync(requestData, requestData.Length, multicastEndPoint);

                DateTime startTime = DateTime.Now;
                TimeSpan timeout = TimeSpan.FromSeconds(5);

                while ((DateTime.Now - startTime) < timeout)
                {
                    var receiveTask = udpClient.ReceiveAsync();
                    if (await Task.WhenAny(receiveTask, Task.Delay(5000)) != receiveTask) break;

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
                                OnDeviceFound?.Invoke(this, new UPnPDeviceEventArgs(response.RemoteEndPoint.Address.ToString(), locationUrl, serviceUrl));

                                result = true;
                            }
                        }
                    }
                }

                return result;
            }
        }

        public bool DiscoverSync()
        {
            bool result = false;
            string searchMessage = "M-SEARCH * HTTP/1.1\r\n" +
                                   "HOST: 239.255.255.250:1900\r\n" +
                                   "MAN: \"ssdp:discover\"\r\n" +
                                   "MX: 2\r\n" +
                                   "ST: urn:schemas-upnp-org:device:InternetGatewayDevice:1\r\n\r\n";

            using (UdpClient udpClient = new UdpClient())
            {
                udpClient.EnableBroadcast = true;
                IPEndPoint multicastEndPoint = new IPEndPoint(IPAddress.Parse("239.255.255.250"), 1900);
                byte[] requestData = Encoding.UTF8.GetBytes(searchMessage);

                udpClient.Send(requestData, requestData.Length, multicastEndPoint);

                var receiveBuffer = new byte[8192];
                IPEndPoint senderEndPoint = new IPEndPoint(IPAddress.Any, 0);

                DateTime startTime = DateTime.Now;
                TimeSpan timeout = TimeSpan.FromSeconds(5);

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
                                    OnDeviceFound?.Invoke(this, new UPnPDeviceEventArgs(senderEndPoint.Address.ToString(), locationUrl, serviceUrl));

                                    result = true;
                                }
                            }
                        }
                    }
                }

                return result;
            }
        }

        private string ExtractLocationUrl(string response)
        {
            int startIndex = response.ToLower().IndexOf("location:") + 9;
            int endIndex = response.IndexOf("\r", startIndex);
            return response.Substring(startIndex, endIndex - startIndex).Trim();
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
            if (serviceUrl == null)
                throw new ExceptionUtil("La url de servicio es null. ¿Esta activo y disponible tu router con UPnP activado?");

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
            if (serviceUrl == null)
                throw new ExceptionUtil("La url de servicio es null. ¿Esta activo y disponible tu router con UPnP activado?");

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
            if (serviceUrl == null)
                throw new ExceptionUtil("La url de servicio es null. ¿Esta activo y disponible tu router con UPnP activado?");

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

        public UPnPDeviceEventArgs(string ipAddress, string locationUrl, string serviceUrl)
        {
            IPAddress = ipAddress;
            LocationUrl = locationUrl;
            ServiceUrl = serviceUrl;
        }
    }
}