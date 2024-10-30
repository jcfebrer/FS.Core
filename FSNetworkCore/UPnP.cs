using FSExceptionCore;
using FSTraceCore;
using NATUPNPLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FSNetworkCore
{
    public class UPnP
    {
        public enum InternetProtocol
        {
            UDP, TCP, All
        }

        //https://pietschsoft.com/post/2009/02/06/net-framework-communicate-through-nat-router-via-upnp
        private static UPnPNAT UPnPNAT = new UPnPNAT();
        private static IStaticPortMappingCollection UPnPMappings;
        private static string UPnPEntryName = "UDP Hole Punching Client";

        private static IPAddress InternetAccessAdapter;

        public static void CreateRule(int port, InternetProtocol protocolType)
        {
            InternetAccessAdapter = FSNetworkCore.Net.GetInternetIPAddress();

            Log.TraceInfo("Adapter with Internet Access: " + InternetAccessAdapter);

            UPnPMappings = UPnPNAT.StaticPortMappingCollection;

            if (UPnPMappings == null)
            {
                throw new ExceptionUtil("UPnP is disabled in your router or port TCP:2869 and UDP:1900 is closed in windows firewall.");
            }
            else
            {
                ClearUpUPnP();

                Log.TraceInfo("Listening on Port " + port);

                if (protocolType == InternetProtocol.UDP || protocolType == InternetProtocol.All)
                {
                    if (AttemptUPnP(port, "UDP"))
                    {
                        Log.TraceInfo("UPnP UDP Map Added");
                    }
                    else
                    {
                        Log.TraceError("UPnP UDP Mapping Not Possible");
                        throw new ExceptionUtil("UPnP UDP Mapping Not Possible");
                    }
                }

                System.Threading.Thread.Sleep(500);

                if (protocolType == InternetProtocol.TCP || protocolType == InternetProtocol.All)
                {
                    if (AttemptUPnP(port, "TCP"))
                    {
                        Log.TraceInfo("UPnP TCP Map Added");
                    }
                    else
                    {
                        Log.TraceError("UPnP TCP Mapping Not Possible");
                        throw new ExceptionUtil("UPnP TCP Mapping Not Possible");
                    }
                }
            }
        }

        private static bool AttemptUPnP(int port, string type)
        {
            if (UPnPMappings == null)
                return false;
            else
                try
                {
                    UPnPMappings.Add(port, type, port, InternetAccessAdapter.ToString(), true, UPnPEntryName);
                    return true;
                }
                catch
                {
                    return false;
                }
        }

        public static void ClearUpUPnP()
        {
            if (UPnPMappings != null)
            {
                List<int> PortMappingsToDelete = new List<int>();

                foreach (IStaticPortMapping map in UPnPMappings)
                {
                    try
                    {
                        if (map.Description == UPnPEntryName)
                            PortMappingsToDelete.Add(map.ExternalPort);
                    }
                    catch
                    {

                    }
                }

                foreach (int port in PortMappingsToDelete)
                    try
                    {
                        UPnPMappings.Remove(port, "UDP");
                        System.Threading.Thread.Sleep(500);
                        UPnPMappings.Remove(port, "TCP");

                        Log.TraceInfo("UPnP Map " + port + " Removed");
                    }
                    catch (Exception ex)
                    {
                        Log.TraceError("Failed to remove UPnP Map " + port + ": " + ex.ToString());
                    }
            }
        }
    }
}
