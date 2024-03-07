#region

using System.Net.NetworkInformation;
using System.Text;
using System.Threading;

#endregion

namespace FSNetworkCore
{
    public class PingAsync
    {
        #region Delegates

        public delegate void PingCompleted(object sender, PingCompletedEventArgs e, string message);

        #endregion

        private readonly AutoResetEvent resetEvent = new AutoResetEvent(false);

        public event PingCompleted CompleteCallback;

        public void Ping(string ip4address)
        {
			System.Net.NetworkInformation.Ping pingSender = new System.Net.NetworkInformation.Ping();

            // Create an event handler for ping complete
            pingSender.PingCompleted += pingSender_Complete;

            // Create a buffer of 32 bytes of data to be transmitted.
            byte[] packetData = Encoding.ASCII.GetBytes("................................");

            // Jump though 50 routing nodes tops, and don't fragment the packet
            PingOptions packetOptions = new PingOptions(50, true);

            // Send the ping asynchronously
            pingSender.SendAsync(ip4address, 5000, packetData, packetOptions, resetEvent);
        }

        private void pingSender_Complete(object sender, PingCompletedEventArgs e)
        {
            string message;

            if (e.Cancelled)
            {
                message = "Ping cancelado...";

                // The main thread can resume
                ((AutoResetEvent) e.UserState).Set();
            }
            else if (e.Error != null)
            {
                message = "Ha ocurrido un error: " + e.Error;

                // The main thread can resume
                ((AutoResetEvent) e.UserState).Set();
            }
            else
            {
                PingReply pingResponse = e.Reply;
                // Call the method that displays the ping results, and pass the information with it
                if (pingResponse == null)
                {
                    // We got no response
                    message = "Sin respuesta.";
                }
                else if (pingResponse.Status == IPStatus.Success)
                {
                    // We got a response, let's see the statistics
                    message = "Respuesta desde " + pingResponse.Address + ": bytes=" + pingResponse.Buffer.Length +
                              " time=" + pingResponse.RoundtripTime + " TTL=" + pingResponse.Options.Ttl;
                }
                else
                {
                    message = "Ping was unsuccessful: " + pingResponse.Status;
                }
            }

            if (null != CompleteCallback) CompleteCallback(sender, e, message);
        }
    }
}