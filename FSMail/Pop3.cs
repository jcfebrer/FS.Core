using FSException;
using FSLibrary;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;

namespace FSMail
{
	/// <summary>
	/// Librería para acceso a la cuenta de correo mediante pop3
	/// RFC: https://www.rfc-es.org/rfc/rfc1939-es.txt
	/// </summary>
	/// <seealso cref="System.IDisposable" />
	public class Pop3 : IDisposable
	{
		public string Host { get; set; }
		public int Port { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public bool IsSecure { get; set; }
		public TcpClient Client { get; set; }
		public Stream ClientStream { get; set; }
		public StreamWriter Writer { get; set; }
		public StreamReader Reader { get; set; }
		public bool Recent { get; set; }
		private bool disposed = false;
		
		public Pop3(string host, int port, string email, string password) : this(host, port, email, password, false, false) { }
		public Pop3(string host, int port, string email, string password, bool secure, bool recent)
		{
			Host = host;
			Port = port;
			Email = email;
			Password = password;
			IsSecure = secure;
			Recent = recent;
		}

		public void Connect()
		{
			if (Client == null)
				Client = new TcpClient();
			if (!Client.Connected)
				Client.Connect(Host, Port);
			if (IsSecure)
			{
				SslStream secureStream =
				new SslStream(Client.GetStream());
				secureStream.AuthenticateAsClient(Host);
				ClientStream = secureStream;
				secureStream = null;
			}
			else
				ClientStream = Client.GetStream();
			Writer = new StreamWriter(ClientStream);
			Reader = new StreamReader(ClientStream);
			ReadLine();
			Login();
		}

		public int GetEmailCount()
		{
			int count = 0;
			string response = SendCommand("STAT");
			if (IsResponseOk(response))
			{
				string[] arr = response.Substring(4).Split(' ');
				count = Convert.ToInt32(arr[0]);
			}
			else
				count = -1;
			return count;
		}

		public Pop3Email FetchEmail(int emailId, int lines)
		{
			if (IsResponseOk(SendCommand("TOP " + emailId + " " + lines)))
			{
				Pop3Email email = new Pop3Email(ReadLines());
				email.Id = emailId;
				return email;
			}
			else
				return null;
		}

		public bool DeleteEmail(int emailId)
		{
			if (IsResponseOk(SendCommand("DELE " + emailId)))
			{
				return true;
			}
			else
				return false;
		}

		public string[] GetMessagesList()
		{
			string[] list = new string[] { };
			string response = SendCommand("LIST");
			if (IsResponseOk(response))
			{
				string data = ReadLines();
				list = data.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
			}

			return list;
		}

		public int GetMessageSize(int emailId)
		{
			int size = 0;
			string response = SendCommand("LIST " + emailId);
			if (IsResponseOk(response))
			{
				size = Convert.ToInt32(response.Split(' ')[2]);
			}

			return size;
		}


		public List<Pop3MessagePart> FetchMessagePartList(int count, int maxSize = 0)
		{
			List<Pop3MessagePart> emailParts = new List<Pop3MessagePart>();
			string[] list = GetMessagesList();
			int listCount = list.Length;

			for (int i = listCount - 1; i >= (listCount - count); i--)
			{
				int emailId = Convert.ToInt32(list[i].Split(' ')[0]);
				int size = Convert.ToInt32(list[i].Split(' ')[1]);

				if (maxSize != 0 && size < maxSize)
				{
					Pop3MessagePart emailPart = FetchMessageParts(emailId);

					if (emailPart != null)
						emailParts.Add(emailPart);
				}
			}
			return emailParts;
		}


		public List<Pop3Email> FetchEmailList(int count, int lines = 0, int maxSize = 0)
		{
			List<Pop3Email> emails = new List<Pop3Email>();
			string[] list = GetMessagesList();
			int listCount = list.Length;

			for (int i = listCount - 1; i >= (listCount - count); i--)
			{
				int emailId = Convert.ToInt32(list[i].Split(' ')[0]);
				int size = Convert.ToInt32(list[i].Split(' ')[1]);

				if (maxSize != 0 && size < maxSize)
				{
					Pop3Email email = FetchEmail(emailId, lines);
					email.Size = size;

					if (email != null)
						emails.Add(email);
				}
			}
			return emails;
		}


		public Pop3MessagePart FetchMessageParts(int emailId)
		{
			if (IsResponseOk(SendCommand("RETR " + emailId)))
			{
				string data = ReadLines();
				return Pop3Util.ParseMessageParts(data);
			}
			return null;
		}


		public void Close()
		{
			if (Client != null)
			{
				if (Client.Connected)
					Logout();
				Client.Close();
				Client = null;
			}
			if (ClientStream != null)
			{
				ClientStream.Close();
				ClientStream = null;
			}
			if (Writer != null)
			{
				Writer.Close();
				Writer = null;
			}
			if (Reader != null)
			{
				Reader.Close();
				Reader = null;
			}
			disposed = true;
		}

		public void Dispose()
		{
			if (!disposed)
				Close();
		}

		protected void Login()
		{
			string recent = "";
			if (Recent)
				recent = "recent:";

			if (!IsResponseOk(SendCommand("USER " + recent + Email))
			|| !IsResponseOk(SendCommand("PASS " + Password)))
				throw new ExceptionUtil("User/password not accepted");
		}

		protected void Logout()
		{
			SendCommand("RSET");
		}

		protected string SendCommand(string cmdtext)
		{
			Writer.WriteLine(cmdtext);
			Writer.Flush();
			return ReadLine();
		}

		protected string ReadLine()
		{
			return Reader.ReadLine() + "\r\n";
		}

		protected string ReadLines()
		{
			StringBuilder b = new StringBuilder();
			while (true)
			{
				string temp = ReadLine();
				if (temp == ".\r\n" || temp.IndexOf("-ERR") != -1)
					break;
				b.Append(temp);
			}
			return b.ToString();
		}

		protected static bool IsResponseOk(string response)
		{
			if (response.StartsWith("+OK"))
				return true;
			if (response.StartsWith("-ERR"))
				return false;
			throw new ExceptionUtil("Cannot understand server response: " +
						response);
		}
	}
	public class Pop3Email
	{
		public NameValueCollection Headers { get; set; }
		public string ContentType { get; set; }
		public DateTime Date { get; set; }
		public string From { get; set; }
		public string To { get; set; }
		public string Subject { get; set; }
		public int Id { get; set; }
		public int Size { get; set; }
		public Pop3Email(string emailText)
		{
			Headers = Pop3Util.ParseHeaders(emailText);
			ContentType = Headers["Content-Type"];
			From = Headers["From"];
			To = Headers["To"];
			Subject = Headers["Subject"];
			if (Headers["Date"] != null)
				try
				{
					Date = DateTimeUtil.UtcToDateTime(Headers["Date"]);
				}
				catch (FormatException)
				{
					Date = DateTime.MinValue;
				}
			else
				Date = DateTime.MinValue;
		}
	}
	public class Pop3MessagePart
	{
		public NameValueCollection Headers { get; protected set; }
		public string ContentType { get; protected set; }
		public string MessageText { get; protected set; }
		public Pop3MessagePart(NameValueCollection headers, string messageText)
		{
			Headers = headers;
			ContentType = Headers["Content-Type"];
			MessageText = messageText;
		}
	}
	public class Pop3Util
	{
		public static NameValueCollection ParseHeaders(string headerText)
		{
			NameValueCollection headers = new NameValueCollection();
			StringReader reader = new StringReader(headerText);
			string line;
			string headerName = null, headerValue;
			int colonIndx;
			while ((line = reader.ReadLine()) != null)
			{
				if (line == "")
					break;
				if (Char.IsLetterOrDigit(line[0]) &&
					(colonIndx = line.IndexOf(':')) != -1)
				{
					headerName = line.Substring(0, colonIndx);
					headerValue = line.Substring
						(colonIndx + 1).Trim();
					headers.Add(headerName, headerValue);
				}
				else if (headerName != null)
					headers[headerName] += " " + line.Trim();
				else
					throw new FormatException
					("Could not parse headers");
			}
			return headers;
		}

		public static Pop3MessagePart ParseMessageParts(string emailText)
		{
			Regex BoundaryRegex = new Regex("Content-Type:multipart(?:/\\S +;)" + "\\s+[^\r\n]* boundary =\"?(?<boundary>" + "[^\"\r\n]+)\"?\r\n", RegexOptions.IgnoreCase | RegexOptions.Compiled);

			Pop3MessagePart messageParts = null;
			int newLinesIndx = emailText.IndexOf("\r\n\r\n");
			Match m = BoundaryRegex.Match(emailText);
			if (m.Index < emailText.IndexOf("\r\n\r\n") && m.Success)
			{
				string boundary = m.Groups["boundary"].Value;
				string startingBoundary = "\r\n--" + boundary;
				int startingBoundaryIndx = -1;
				while (true)
				{
					if (startingBoundaryIndx == -1)
						startingBoundaryIndx = emailText.IndexOf(startingBoundary);
					if (startingBoundaryIndx != -1)
					{
						int nextBoundaryIndx = emailText.IndexOf(startingBoundary, startingBoundaryIndx + startingBoundary.Length);
						if (nextBoundaryIndx != -1 && nextBoundaryIndx != startingBoundaryIndx)
						{
							string multipartMsg = emailText.Substring(startingBoundaryIndx + startingBoundary.Length, (nextBoundaryIndx - startingBoundaryIndx - startingBoundary.Length));
							int headersIndx = multipartMsg.IndexOf("\r\n\r\n");
							if (headersIndx == -1)
								throw new FormatException("Incompatible multipart message format");

							string bodyText = multipartMsg.Substring(headersIndx).Trim();
							NameValueCollection headers = Pop3Util.ParseHeaders(multipartMsg.Trim());
							messageParts = new Pop3MessagePart(headers, bodyText);
						}
						else
							break;
						startingBoundaryIndx = nextBoundaryIndx;
					}
					else
						break;
				}
				if (newLinesIndx != -1)
				{
					string emailBodyText = emailText.Substring(newLinesIndx + 1);
				}
			}
			else
			{
				int headersIndx = emailText.IndexOf("\r\n\r\n");
				if (headersIndx == -1)
					throw new FormatException("Incompatible multipart message format");

				string bodyText = emailText.Substring(headersIndx).Trim();
				NameValueCollection headers = Pop3Util.ParseHeaders(emailText);
				messageParts = new Pop3MessagePart(headers, bodyText);
			}
			return messageParts;
		}
	}
}