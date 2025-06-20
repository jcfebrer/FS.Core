﻿using System;
using System.IO;

#if NET35_OR_GREATER || NETCOREAPP
    using System.Linq;
#endif

using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace FSNetwork
{
    public static class WOL
    {
#if NET45_OR_GREATER || NETCOREAPP
        public static async System.Threading.Tasks.Task WakeOnLanAsync(string macAddress)
        {
            byte[] magicPacket = BuildMagicPacket(macAddress);
            foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces().Where((n) =>
                n.NetworkInterfaceType != NetworkInterfaceType.Loopback && n.OperationalStatus == OperationalStatus.Up))
            {
                IPInterfaceProperties iPInterfaceProperties = networkInterface.GetIPProperties();
                foreach (MulticastIPAddressInformation multicastIPAddressInformation in iPInterfaceProperties.MulticastAddresses)
                {
                    IPAddress multicastIpAddress = multicastIPAddressInformation.Address;
                    if (multicastIpAddress.ToString().StartsWith("ff02::1%", StringComparison.OrdinalIgnoreCase)) // Ipv6: All hosts on LAN (with zone index)
                    {
                        UnicastIPAddressInformation unicastIPAddressInformation = iPInterfaceProperties.UnicastAddresses.Where((u) =>
                            u.Address.AddressFamily == AddressFamily.InterNetworkV6 && !u.Address.IsIPv6LinkLocal).FirstOrDefault();
                        if (unicastIPAddressInformation != null)
                        {
                            await SendWakeOnLanAsync(unicastIPAddressInformation.Address, multicastIpAddress, magicPacket);
                            break;
                        }
                    }
                    else if (multicastIpAddress.ToString().Equals("224.0.0.1")) // Ipv4: All hosts on LAN
                    {
                        UnicastIPAddressInformation unicastIPAddressInformation = iPInterfaceProperties.UnicastAddresses.Where((u) =>
                            u.Address.AddressFamily == AddressFamily.InterNetwork && !iPInterfaceProperties.GetIPv4Properties().IsAutomaticPrivateAddressingActive).FirstOrDefault();
                        if (unicastIPAddressInformation != null)
                        {
                            await SendWakeOnLanAsync(unicastIPAddressInformation.Address, multicastIpAddress, magicPacket);
                            break;
                        }
                    }
                }
            }
        }
#endif

#if NET35_OR_GREATER || NETCOREAPP
        public static void WakeOnLan(string macAddress)
        {
            byte[] magicPacket = BuildMagicPacket(macAddress);
            foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces().Where((n) =>
                n.NetworkInterfaceType != NetworkInterfaceType.Loopback && n.OperationalStatus == OperationalStatus.Up))
            {
                IPInterfaceProperties iPInterfaceProperties = networkInterface.GetIPProperties();
                foreach (MulticastIPAddressInformation multicastIPAddressInformation in iPInterfaceProperties.MulticastAddresses)
                {
                    IPAddress multicastIpAddress = multicastIPAddressInformation.Address;
                    if (multicastIpAddress.ToString().StartsWith("ff02::1%", StringComparison.OrdinalIgnoreCase)) // Ipv6: All hosts on LAN (with zone index)
                    {
                        UnicastIPAddressInformation unicastIPAddressInformation = iPInterfaceProperties.UnicastAddresses.Where((u) =>
                            u.Address.AddressFamily == AddressFamily.InterNetworkV6 && !u.Address.IsIPv6LinkLocal).FirstOrDefault();
                        if (unicastIPAddressInformation != null)
                        {
                            SendWakeOnLan(unicastIPAddressInformation.Address, multicastIpAddress, magicPacket);
                            break;
                        }
                    }
                    else if (multicastIpAddress.ToString().Equals("224.0.0.1")) // Ipv4: All hosts on LAN
                    {
                        UnicastIPAddressInformation unicastIPAddressInformation = iPInterfaceProperties.UnicastAddresses.Where((u) =>
                            u.Address.AddressFamily == AddressFamily.InterNetwork && !iPInterfaceProperties.GetIPv4Properties().IsAutomaticPrivateAddressingActive).FirstOrDefault();
                        if (unicastIPAddressInformation != null)
                        {
                            SendWakeOnLan(unicastIPAddressInformation.Address, multicastIpAddress, magicPacket);
                            break;
                        }
                    }
                }
            }
        }
#endif

        static byte[] BuildMagicPacket(string macAddress) // MacAddress in any standard HEX format
        {
            macAddress = Regex.Replace(macAddress, "[: -]", "");
            byte[] macBytes = new byte[6];
            for (int i = 0; i < 6; i++)
            {
                macBytes[i] = Convert.ToByte(macAddress.Substring(i * 2, 2), 16);
            }

            using (MemoryStream ms = new MemoryStream())
            {
                using (BinaryWriter bw = new BinaryWriter(ms))
                {
                    for (int i = 0; i < 6; i++)  //First 6 times 0xff
                    {
                        bw.Write((byte)0xff);
                    }
                    for (int i = 0; i < 16; i++) // then 16 times MacAddress
                    {
                        bw.Write(macBytes);
                    }
                }
                return ms.ToArray(); // 102 bytes magic packet
            }
        }

#if NET45_OR_GREATER || NETCOREAPP
        static async System.Threading.Tasks.Task SendWakeOnLanAsync(IPAddress localIpAddress, IPAddress multicastIpAddress, byte[] magicPacket)
        {
            using (UdpClient client = new UdpClient(new IPEndPoint(localIpAddress, 0)))
            {
                await client.SendAsync(magicPacket, magicPacket.Length, multicastIpAddress.ToString(), 9);
            }
        }
#endif

#if NET30 || NET20
        public static void WakeOnLan(string macAddress)
        {
            byte[] magicPacket = BuildMagicPacket(macAddress);

            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface networkInterface in networkInterfaces)
            {
                if (networkInterface.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                    networkInterface.OperationalStatus == OperationalStatus.Up)
                {
                    IPInterfaceProperties iPInterfaceProperties = networkInterface.GetIPProperties();
                    MulticastIPAddressInformationCollection multicastAddresses = iPInterfaceProperties.MulticastAddresses;

                    foreach (MulticastIPAddressInformation multicastIPAddressInformation in multicastAddresses)
                    {
                        IPAddress multicastIpAddress = multicastIPAddressInformation.Address;
                        string multicastIpAddressString = multicastIpAddress.ToString();

                        if (multicastIpAddressString.StartsWith("ff02::1%", StringComparison.OrdinalIgnoreCase)) // Ipv6: All hosts on LAN (with zone index)
                        {
                            UnicastIPAddressInformation unicastIPAddressInformation = GetIPv6UnicastAddress(iPInterfaceProperties);
                            if (unicastIPAddressInformation != null)
                            {
                                SendWakeOnLan(unicastIPAddressInformation.Address, multicastIpAddress, magicPacket);
                                break;
                            }
                        }
                        else if (multicastIpAddressString.Equals("224.0.0.1")) // Ipv4: All hosts on LAN
                        {
                            UnicastIPAddressInformation unicastIPAddressInformation = GetIPv4UnicastAddress(iPInterfaceProperties);
                            if (unicastIPAddressInformation != null)
                            {
                                SendWakeOnLan(unicastIPAddressInformation.Address, multicastIpAddress, magicPacket);
                                break;
                            }
                        }
                    }
                }
            }
        }

        private static UnicastIPAddressInformation GetIPv6UnicastAddress(IPInterfaceProperties iPInterfaceProperties)
        {
            UnicastIPAddressInformationCollection unicastAddresses = iPInterfaceProperties.UnicastAddresses;
            foreach (UnicastIPAddressInformation unicastIPAddressInformation in unicastAddresses)
            {
                if (unicastIPAddressInformation.Address.AddressFamily == AddressFamily.InterNetworkV6 &&
                    !unicastIPAddressInformation.Address.IsIPv6LinkLocal)
                {
                    return unicastIPAddressInformation;
                }
            }
            return null;
        }

        private static UnicastIPAddressInformation GetIPv4UnicastAddress(IPInterfaceProperties iPInterfaceProperties)
        {
            UnicastIPAddressInformationCollection unicastAddresses = iPInterfaceProperties.UnicastAddresses;
            IPv4InterfaceProperties ipv4Properties = iPInterfaceProperties.GetIPv4Properties();
            if (ipv4Properties == null) return null;
            bool isAutomaticPrivateAddressingActive = ipv4Properties.IsAutomaticPrivateAddressingActive;
            foreach (UnicastIPAddressInformation unicastIPAddressInformation in unicastAddresses)
            {
                if (unicastIPAddressInformation.Address.AddressFamily == AddressFamily.InterNetwork &&
                    !isAutomaticPrivateAddressingActive)
                {
                    return unicastIPAddressInformation;
                }
            }
            return null;
        }
#endif

        static void SendWakeOnLan(IPAddress localIpAddress, IPAddress multicastIpAddress, byte[] magicPacket)
        {
            using (UdpClient client = new UdpClient(new IPEndPoint(localIpAddress, 0)))
            {
                client.Send(magicPacket, magicPacket.Length, multicastIpAddress.ToString(), 9);
            }
        }
    }
}