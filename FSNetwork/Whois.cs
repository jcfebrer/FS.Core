#region

using FSException;
using System;
using System.Collections;
using System.IO;
using System.Net.Sockets;
using System.Text;

#endregion

namespace FSNetwork
{
    public class Whois
    {
        private const int WHOIS_PORT = 43;
        private Hashtable m_WhoisServers;
        private int m_intTimeout = 1000;

        public Whois()
        {
            m_WhoisServers = new Hashtable();

            m_WhoisServers.Add("com", "whois.register.com");
            m_WhoisServers.Add("org", "whois.register.com");
            m_WhoisServers.Add("net", "whois.register.com");
            m_WhoisServers.Add("ac", "whois.nic.ac");
            m_WhoisServers.Add("be", "whois.dns.be");
            m_WhoisServers.Add("de", "whois.denic.de");
            m_WhoisServers.Add("fr", "whois.nic.fr");
            m_WhoisServers.Add("gov", "whois.nic.gov");
            m_WhoisServers.Add("mil", "whois.nic.mil");
            m_WhoisServers.Add("uk", "whois.nic.uk");
            m_WhoisServers.Add("es", "whois.register.com");
        }

        public int Timeout
        {
            get { return m_intTimeout; }
            set { m_intTimeout = value; }
        }

        public Hashtable WhoisServers
        {
            get { return m_WhoisServers; }
            set { m_WhoisServers = value; }
        }

        public string WhoisQuery;

        public string Query(string Host)
        {
            TcpClient Tcp = new TcpClient();
            string strWhoisServer = GetWhoIsServer(Host);
            StringBuilder builder = new StringBuilder();

            if (strWhoisServer == null)
            {
                return null;
            }

            try
            {
                Tcp.SendTimeout = Timeout;
                Tcp.ReceiveTimeout = Timeout;
                Tcp.Connect(strWhoisServer, WHOIS_PORT);


                NetworkStream NetStream = Tcp.GetStream();
                BufferedStream BaseStream = new BufferedStream(NetStream);
                try
                {
                    StreamWriter OutputStream = new StreamWriter(BaseStream);
                    OutputStream.WriteLine(Host);
                    OutputStream.Flush();
                    try
                    {
                        StreamReader InputStream = new StreamReader(BaseStream);
                        while (InputStream.Peek() != -1)
                        {
                            builder.Append(InputStream.ReadLine() + "\r\n");
                        }
                    }
                    catch (Exception Err)
                    {
                        throw new ExceptionUtil("Error geting whois data from whois server", Err);
                    }
                }
                catch (Exception Err)
                {
                    throw new ExceptionUtil("Error sending whois request to whois server", Err);
                }

                if (builder.Length > 0)
                {
                    return builder.ToString();
                }
                else
                {
                    return null;
                }
            }
            catch (System.Exception e)
            {
                if (e.GetType().Equals(new SocketException().GetType()))
                {
                    SocketException eSocket = ((SocketException) (e));
                }
                else
                {
                    throw new ExceptionUtil("Unexpected error occured pinging the remote host", e);
                }
            }
            finally
            {
                if (!(Tcp == null))
                {
                    Tcp.Close();
                }
            }
            return "";
        }


        private string GetWhoIsServer(string Host)
        {
            string[] arrDomain = Host.Split(Convert.ToChar(Convert.ToInt32(char.Parse("."))));

            if (arrDomain.Length != 2 & arrDomain.Length != 3)
            {
                return null;
            }

            if (WhoisServers.ContainsKey(arrDomain[arrDomain.GetUpperBound(0)]))
            {
                return Convert.ToString(WhoisServers[arrDomain[arrDomain.GetUpperBound(0)]]);
            }
            else
            {
                return "whois.nic." + arrDomain[arrDomain.GetUpperBound(0)];
            }
        }
    }
}