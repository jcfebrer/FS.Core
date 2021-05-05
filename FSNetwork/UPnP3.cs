using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSNetwork
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class UPnP3
    {
        /// <summary>
        /// Device search request
        /// </summary>
        private const string searchRequest = "M-SEARCH * HTTP/1.1\r\nHOST: {0}:{1}\r\nMAN: \"ssdp:discover\"\r\nMX: {2}\r\nST: {3}\r\n";

        /// <summary>
        /// Advertisement multicast address
        /// </summary>
        private const string multicastIP = "239.255.255.250";

        /// <summary>
        /// Advertisement multicast port
        /// </summary>
        private const int multicastPort = 1900;

        /// <summary>
        ///  Time to Live (TTL) for multicast messages
        /// </summary>
        private const int multicastTTL = 4;

        private const int unicastPort = 1901;

        private const int MaxResultSize = 8096;

        private const string DefaultDeviceType = "ssdp:all";

        private string deviceType;

        private Action<UPnP2.DeviceUPnP> onDeviceFound;

        private int searchTimeOut;

        private Socket socket;

        private Timer timer;

        private int sendCount;

        private SocketAsyncEventArgs sendEvent;

        private bool socketClosed;

        private List<Task> taskList = new List<Task>();

        public void Initialize(string deviceType, int searchTimeOut, Action<UPnP2.DeviceUPnP> onDeviceFound)
        {
            if (searchTimeOut < 1 || searchTimeOut > 4)
            {
                this.searchTimeOut = multicastTTL;
            }
            else
            {
                this.searchTimeOut = searchTimeOut;
            }

            if (string.IsNullOrWhiteSpace(deviceType))
            {
                this.deviceType = DefaultDeviceType;
            }
            else
            {
                this.deviceType = deviceType;
            }

            this.onDeviceFound = onDeviceFound;
        }

        public void FindDevices()
        {
            string request = string.Format(searchRequest, multicastIP, multicastPort, this.searchTimeOut, this.deviceType);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            byte[] multiCastData = Encoding.UTF8.GetBytes(request);
            socket.SendBufferSize = multiCastData.Length;
            sendEvent = new SocketAsyncEventArgs();
            sendEvent.RemoteEndPoint = new IPEndPoint(IPAddress.Parse(multicastIP), multicastPort);
            sendEvent.SetBuffer(multiCastData, 0, multiCastData.Length);
            sendEvent.Completed += OnSocketSendEventCompleted;

            // Set a one-shot timer for the Search time plus a second
            TimerCallback cb = new TimerCallback((state) =>
            {
                this.socketClosed = true;
                socket.Close();
            });

            timer = new Timer(cb, null, TimeSpan.FromSeconds(this.searchTimeOut + 1), new TimeSpan(-1));

            // Kick off the initial Send
            this.sendCount = 3;
            this.socketClosed = false;
            socket.SendToAsync(sendEvent);
            //while (!this.socketClosed)
            //{
            //    Thread.Sleep(200);
            //}

            //Task.WaitAll(this.taskList.ToArray());
            //this.taskList.Clear();
        }

        private void OnSocketSendEventCompleted(object sender, SocketAsyncEventArgs e)
        {
            if (e.SocketError != SocketError.Success)
            {
                this.AddDevice(null);
            }
            else
            {
                if (e.LastOperation == SocketAsyncOperation.SendTo)
                {
                    if (--this.sendCount != 0)
                    {
                        if (!this.socketClosed)
                        {
                            socket.SendToAsync(sendEvent);
                        }
                    }
                    else
                    {
                        // When the initial multicast is done, get ready to receive responses
                        e.RemoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
                        socket.ReceiveBufferSize = MaxResultSize;
                        byte[] receiveBuffer = new byte[MaxResultSize];
                        e.SetBuffer(receiveBuffer, 0, MaxResultSize);
                        socket.ReceiveFromAsync(e);
                    }
                }
                else if (e.LastOperation == SocketAsyncOperation.ReceiveFrom)
                {
                    // Got a response, so decode it
                    string result = Encoding.UTF8.GetString(e.Buffer, 0, e.BytesTransferred);
                    if (result.StartsWith("HTTP/1.1 200 OK", StringComparison.InvariantCultureIgnoreCase))
                    {
                        //parse device and invoke callback
                        AddDevice(result);
                    }
                    else
                    {
                        //Debug.WriteLine("INVALID SEARCH RESPONSE");
                    }

                    if (!this.socketClosed)
                    {
                        // and kick off another read
                        socket.ReceiveFromAsync(e);
                    }
                    else
                    {
                        // unless socket was closed, when declare the scan is complete
                        //AddDevice(result);
                    }
                }
            }
        }

        private void AddDevice(string response)
        {
            Console.WriteLine(response);
            //Task addDeviceTask = Task.Run(() =>
            //{
            //    // parse the result and download the device description
            //    if (this.onDeviceFound != null && response != null)
            //    {
            //        Dictionary<string, string> ssdpResponse = ParseSSDPResponse(response);
            //        HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(ssdpResponse["location"]);
            //        WebResponse webResponse = webRequest.GetResponse();
            //        using (DeviceXml deviceXml = new DeviceXml(webResponse.GetResponseStream()))
            //        {
            //            this.onDeviceFound(deviceXml.GetObject());
            //        }
            //    }
            //});

            //this.taskList.Add(addDeviceTask);
        }

    //    // Probably not exactly compliant with RFC 2616 but good enough for now
    //    private Dictionary<string, string> ParseSSDPResponse(string response)
    //    {
    //        StringReader reader = new StringReader(response);

    //        string line = reader.ReadLine();
    //        if (line != "HTTP/1.1 200 OK")
    //            return null;

    //        Dictionary<string, string> result = new Dictionary<string, string>();

    //        for (;;)
    //        {
    //            line = reader.ReadLine();
    //            if (line == null)
    //                break;
    //            if (line != "")
    //            {
    //                int colon = line.IndexOf(':');
    //                if (colon < 1)
    //                {
    //                    return null;
    //                }
    //                string name = line.Substring(0, colon).Trim();
    //                string value = line.Substring(colon + 1).Trim();
    //                if (string.IsNullOrEmpty(name))
    //                {
    //                    return null;
    //                }
    //                result[name.ToLowerInvariant()] = value;
    //            }
    //        }
    //        return result;
    //    }
    }
}