using FSNetwork;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FSTests.FSNetwork
{
    [TestClass]
    public class FSUPnPTests
    {
        [TestMethod]
        public async Task TestUPnP()
        {
            UPnP upnp = new UPnP();

            upnp.Gateway = Net.DefaultGateway();
            upnp.OnDeviceFound += (sender, e) =>
            {
                Debug.WriteLine($"🎯 Dispositivo encontrado: {e.IPAddress}");
                Debug.WriteLine($"📍 URL de servicio: {e.ServiceUrl}");
            };

            Debug.WriteLine("🔎 Buscando dispositivos UPnP de forma sincrónica...");
            bool foundSync = upnp.Discover();
            Debug.WriteLine(foundSync ? "✅ Dispositivo encontrado." : "❌ No se encontró ningún dispositivo.");

#if NET45_OR_GREATER || NETCOREAPP
            Debug.WriteLine("\n🔎 Buscando dispositivos UPnP de forma asincrónica...");
            bool foundAsync = await upnp.DiscoverAsync();
            Debug.WriteLine(foundAsync ? "✅ Dispositivo encontrado." : "❌ No se encontró ningún dispositivo.");
#endif
        }
    }
}
