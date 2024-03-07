using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.IO;
using FSExceptionCore;

namespace FSNetworkCore
{
public class Ftp
	{

	public string User;
	public string Pass;

	public string Site;
	public Ftp(string Site, string User, string Pass)
		{
			this.Site = Site;
			this.User = User;
			this.Pass = Pass;
		}
	public bool UploadFile(string fileName)
		{
			try {
				string absoluteFileName = Path.GetFileName (fileName);

				FtpWebRequest request = (FtpWebRequest)WebRequest.Create (new Uri (string.Format ("ftp://{0}/{1}", Site, absoluteFileName))) as FtpWebRequest;
				request.Method = WebRequestMethods.Ftp.UploadFile;
				request.UseBinary = true;
				request.UsePassive = true;
				request.KeepAlive = false;
				request.Credentials = new NetworkCredential (User, Pass);
				request.ConnectionGroupName = "group";

				using (FileStream fs = System.IO.File.OpenRead (fileName)) {
					byte[] buffer = new byte[fs.Length];
					fs.Read (buffer, 0, buffer.Length);
					fs.Close ();
					Stream requestStream = request.GetRequestStream ();
					requestStream.Write (buffer, 0, buffer.Length);
					requestStream.Flush ();
					requestStream.Close ();
				}
			} catch (System.Exception ex) {
				throw new ExceptionUtil("Imposible subir fichero al servidor.", ex);
			}

			return true;
		}


	public string[] GetFtpFileList(string folder)
	{

		try {
			if (folder.StartsWith("/")) {
				folder = folder.Substring(1);
			}
			FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(string.Format("ftp://{0}/{1}", Site, folder)));

			request.Method = WebRequestMethods.Ftp.ListDirectory;
			request.Credentials = new NetworkCredential(User, Pass);

			FtpWebResponse response = (FtpWebResponse)request.GetResponse();
			Stream responseStream = response.GetResponseStream();
			StreamReader reader = new StreamReader(responseStream);
			string line = reader.ReadLine();
			List<string> lines = new List<string>();

			while (line != null) {
				lines.Add(line);
				line = reader.ReadLine();
			}

			return lines.ToArray();
		} catch (System.Exception ex) {
				throw new ExceptionUtil("Imposible recuperar lista de ficheros.", ex);
			}
	}

	public string DownloadFile(string file)
		{

			try {
				if (file.StartsWith ("/")) {
					file = file.Substring (1);
				}
				FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create (new Uri (string.Format ("ftp://{0}/{1}", Site, file)));

				NetworkCredential cr = new NetworkCredential (User, Pass);
				request.Credentials = cr;
				request.Method = WebRequestMethods.Ftp.DownloadFile;

				StreamReader reader = new StreamReader (request.GetResponse ().GetResponseStream ());

				string res = reader.ReadToEnd ();
				reader.Close ();

				//Dim ficLocal As String = Path.Combine(dirLocal, Path.GetFileName(ficFTP))
				//Dim sw As New StreamWriter(ficLocal, False, Encoding.Default)
				//sw.Write(res)
				//sw.Close()

				return res;
			} catch (System.Exception ex) {
				throw new ExceptionUtil("Imposuble descargar fichero.", ex);
			}
		}
	}
}