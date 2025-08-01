namespace FSFtp.Proxy
{
    /// <summary>
    /// Abstraction of an FtpClient with a proxy
    /// </summary>
    public abstract class FtpClientProxy : FtpClient
    {
		private ProxyInfo _proxy;
        /// <summary> The proxy connection info. </summary>
		protected ProxyInfo Proxy { get { return _proxy;  } }

        /// <summary> A FTP client with a HTTP 1.1 proxy implementation </summary>
        /// <param name="proxy">Proxy information</param>
		protected FtpClientProxy(ProxyInfo proxy)
        {
			_proxy = proxy;
        }

	    /// <summary> Redefine connect for FtpClient : authentication on the Proxy  </summary>
        /// <param name="stream">The socket stream.</param>
        protected override void Connect(FtpSocketStream stream)
        {
            stream.Connect(Proxy.Host, Proxy.Port, InternetProtocolVersions);
        }
    }
}