using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSGoogleAuthenticatorCore;
using System.Drawing;

namespace FSTestsCore.FSGoogle
{
    [TestClass()]
    public class AuthenticatorTests
    {
        [TestMethod()]
        public void TestAuthenticator()
        {
            Authenticator fSGoogleAuthenticator = new Authenticator();

            //Crea una clave aleatoria (habría que guardar en cada usuario para realizar la validación)
            byte[] key = Authenticator.CreateKey();

            var qrbitmap = fSGoogleAuthenticator.GenerateProvisioningImage("juancarlos@febrersoftware.com", "Febrer Software", key);

            Assert.IsNotNull(qrbitmap);

            var url = fSGoogleAuthenticator.GenerateQRUrl("juancarlos@febrersoftware.com", "Febrer Software", key);

            Assert.IsNotNull(url);

            string keyString = fSGoogleAuthenticator.GenerateKey(key);

            Assert.IsNotNull(url);

            string valor = fSGoogleAuthenticator.GeneratePin(key);

            Assert.IsNotNull(valor);
        }
    }
}
