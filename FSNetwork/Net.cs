using FSException;
using FSLibrary;

#if NETCOREAPP
    using Microsoft.AspNetCore.Http;
#endif

using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace FSNetwork
{
    public class Net
    {
        public static IPAddress GetInternetIPAddress()
        {
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                return endPoint.Address;
            }
        }

        public static IPAddress DefaultGateway()
        {
            return DefaultGateway(GetInternetIPAddress());
        }

        public static IPAddress DefaultGateway(IPAddress ip)
        {
            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            foreach (NetworkInterface networkCard in NetworkInterface.GetAllNetworkInterfaces())
            {
                foreach (UnicastIPAddressInformation ipAddr in networkCard.GetIPProperties().UnicastAddresses)
                {
                    if (ipAddr.Address.Equals(ip))
                    {
                        foreach (GatewayIPAddressInformation gatewayAddr in networkCard.GetIPProperties().GatewayAddresses)
                        {
                            return gatewayAddr.Address;
                        }
                    }
                }
            }
            return null;
        }

        public static IPAddress GetAdapterWithInternetAccess()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_IP4RouteTable WHERE Destination=\"0.0.0.0\"");
            int interfaceIndex = -1;

            foreach (var item in searcher.Get())
                interfaceIndex = Convert.ToInt32(item["InterfaceIndex"]);

            searcher = new ManagementObjectSearcher("root\\CIMV2",
                string.Format("SELECT * FROM Win32_NetworkAdapterConfiguration WHERE InterfaceIndex={0}", interfaceIndex));

            foreach (var item in searcher.Get())
            {
                string[] IPAddresses = (string[])item["IPAddress"];

                foreach (string IP in IPAddresses)
                    return IPAddress.Parse(IP);
            }

            return null;
        }

        /// <summary>
        /// System.Net.NetworkInformation.NetworkInterface n = FSNetworkCore.Net.GetBestNetworkInterface(IPAddress.Parse("8.8.8.8"));
        /// </summary>
        /// <param name="remoteAddress"></param>
        /// <returns></returns>
        public static NetworkInterface GetBestNetworkInterface(IPAddress remoteAddress)
        {
            // Get the index of the interface with the best route to the remote address.
            int dwDestAddr = BitConverter.ToInt32(remoteAddress.GetAddressBytes(), 0);
            int dwBestIfIndex = 0;
            int result = Win32API.GetBestInterface(dwDestAddr, ref dwBestIfIndex);
            if (result != 0)
                throw new NetworkInformationException((int)result);

            // Find a matching .NET interface object with the given index.
            foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
                if (networkInterface.GetIPProperties().GetIPv4Properties().Index == dwBestIfIndex)
                    return networkInterface;

            throw new InvalidOperationException($"Could not find best interface for {remoteAddress}.");
        }

        public static IPAddress GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip;
                }
            }
            throw new ExceptionUtil("No network adapters with an IPv4 address in the system!");
        }

        public static bool IsPortOpen(string host, int port, TimeSpan timeout)
        {
            try
            {
                using (var client = new TcpClient())
                {
                    var result = client.BeginConnect(host, port, null, null);
                    var success = result.AsyncWaitHandle.WaitOne(timeout);
                    client.EndConnect(result);
                    return success;
                }
            }
            catch
            {
                return false;
            }
        }

        public static string GetWebIPAddress()
        {
#if NETFRAMEWORK
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
#else
            string ipAddress = HttpContext.Current.GetServerVariable("HTTP_X_FORWARDED_FOR");
#endif

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

#if NETFRAMEWORK
            return context.Request.ServerVariables["REMOTE_ADDR"];
#else
            return HttpContext.Current.GetServerVariable("REMOTE_ADDR");
#endif
        }

        public static string GetPublicAddress2()
        {
            //https://ipinfo.io/ip --> añadir: .Replace("\n","");
            //https://ipv4.icanhazip.com --> añadir: .Replace("\n","");
            //http://bot.whatismyipaddress.com
            //http://www.serport.biz:8002/ip.aspx
            //http://ipecho.net/plain
            //http://checkip.dyndns.org
            string externalip = Http.GetFromUrl("https://ipv4.icanhazip.com");
            return externalip.Replace("\n", "");
        }

        public static string GetPublicAddress()
        {
            string result = Http.GetFromUrl("http://checkip.dyndns.org");
            result = TextUtil.SearchIpValues(result).First();
            return result;
        }

        public static bool IsUrl(string Url)
        {
            string strRegex = "^(https?://)"
                              + "?(([0-9a-z_!~*'().&=+$%-]+: )?[0-9a-z_!~*'().&=+$%-]+@)?" //user@ 
                              + @"(([0-9]{1,3}\.){3}[0-9]{1,3}" // IP- 199.194.52.184 
                              + "|" // allows either IP or domain 
                              + @"([0-9a-z_!~*'()-]+\.)*" // tertiary domain(s)- www. 
                              + @"([0-9a-z][0-9a-z-]{0,61})?[0-9a-z]\." // second level domain 
                              + "[a-z]{2,6})" // first level domain- .com or .museum 
                              + "(:[0-9]{1,4})?" // port number- :80 
                              + "((/?)|" // a slash isn't required if there is no file name 
                              + "(/[0-9a-z_!~*'().;?:@&=+$,%#-]+)+/?)$";
            Regex re = new Regex(strRegex);

            if (re.IsMatch(Url))
                return (true);
            else
                return (false);
        }

        public static string GetIP()
        {
            string strHostName = "";
            strHostName = Dns.GetHostName();

            IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);

            IPAddress[] addr = ipEntry.AddressList;

            return addr[addr.Length - 1].ToString();
        }

        public static bool IsUri(string s)
        {
            try
            {
                new Uri(s);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public static IPAddress[] GetIPAddress()
        {
            string strHostName = "";
            strHostName = Dns.GetHostName();

            IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);

            return ipEntry.AddressList;
        }


        public static double Dot2LongIP(string DottedIP)
        {
            string[] arrDec = null;
            int i = 0;
            long intResult = 0;

            try
            {
                if (DottedIP == "")
                {
                    return 0;
                }
                arrDec = DottedIP.Split(char.Parse("."));
                for (i = arrDec.Length - 1; i >= 0; i += -1)
                {
                    intResult =
                        Convert.ToInt64(intResult +
                            ((System.Math.Floor(Convert.ToDouble(arrDec[i])) % 256) *
                                System.Math.Pow(256, 3 - i)));
                }
                return intResult;
            }
            catch
            {
                return 0;
            }
        }
    }
}
