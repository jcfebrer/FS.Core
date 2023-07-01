#region

using System.Net.NetworkInformation;
using System.Text;
using System.Threading;

#endregion

namespace FSNetwork
{
    public class Ping
    {
        public static long DoPing(string ip4address)
        {
            System.Net.NetworkInformation.Ping pingSender = new System.Net.NetworkInformation.Ping();

            // Create a buffer of 32 bytes of data to be transmitted.
            byte[] packetData = Encoding.ASCII.GetBytes("................................");

            // Jump though 50 routing nodes tops, and don't fragment the packet
            PingOptions packetOptions = new PingOptions(50, true);

            // Send the ping asynchronously
            PingReply pingReply = pingSender.Send(ip4address, 5000, packetData, packetOptions);

            if (pingReply.Status == IPStatus.Success)
                return pingReply.RoundtripTime;
            else
                return -1;
        }
    }
}