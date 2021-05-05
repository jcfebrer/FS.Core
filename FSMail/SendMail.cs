// // <fileheader>
// // <copyright file="FuncionesMail.cs" company="Febrer Software">
// //     Fecha: 03/07/2015
// //     Project: FSLibrary
// //     Solution: FSLibraryNET2008
// //     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
// //     http://www.febrersoftware.com
// // </copyright>
// // </fileheader>

#region

using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using FSCrypto;
using System.Security.Cryptography.Pkcs;
using System.Collections.Generic;
using System.Net.Mime;
using System.IO;

#endregion

namespace FSMail
{
	public class SendMail
	{
		public SendMail()
		{
			smd = new SendMailDelegate(SendMailMessage);
		}
		
		private delegate Boolean SendMailDelegate(string sTo, string sCC, string sCCO, string sSubject, string sBody, string sFrom, string sFromName, string plantilla);
		private SendMailDelegate smd;
		private static List<string> attachments;

		public static string Server;
		public static string User;
		public static string Password;
		public static int Port;
		public static bool EnableSSL;
		public static string UserPortal;
		public static string UserFullName;

		
		public void SendMailAsync(string sTo, string sCC, string sCCO, string sSubject, string sBody, string sFrom, string sFromName, string plantilla)
		{
			smd.BeginInvoke(sTo, sCC, sCCO, sSubject, sBody, sFrom, sFromName, plantilla, null, null);
		}

        public static bool SendMailMessage(string sTo, string sCC, string sCCO, string sSubject, string sBody, string sFrom, string sFromName, string plantilla)
        {
            return SendMailMessage(sTo, sCC, sCCO, sSubject, sBody, sFrom, sFromName, plantilla, false, null);
        }

		public static void AddAttachment(string pathToAttachment)
        {
			if(attachments == null)
				attachments = new List<string>();

			if (!attachments.Contains(pathToAttachment))
				attachments.Add(pathToAttachment);
        }

        public static bool SendMailMessage(string sTo, string sCC, string sCCO, string sSubject, string sBody, string sFrom, string sFromName, string plantilla, bool Firmar, System.Security.Cryptography.X509Certificates.X509Certificate2 Certificado)
		{
            if (System.Diagnostics.Debugger.IsAttached) return false;

            MailMessage Mail = new MailMessage();

			Mail.BodyEncoding = System.Text.Encoding.UTF8;
			Mail.From = new MailAddress(sFrom, sFromName);
			Mail.To.Add(new MailAddress(sTo));

			if (sCCO != "") {
				string[] toMail = sCCO.Split(';');

				foreach (string s in toMail) {
					if (s != sTo)
						Mail.Bcc.Add(new MailAddress(s));
				}
			}

			if (sCC != "") {
				string[] toMail = sCC.Split(';');

				foreach (string s in toMail) {
					if (s != sTo)
						Mail.CC.Add(new MailAddress(s));
				}
			}

			Mail.Priority = MailPriority.Normal;
			Mail.Subject = sSubject;

			if (sBody.IndexOf("<") > 0 && sBody.IndexOf(">") > 0) {
				Mail.IsBodyHtml = true;				
			} else {
				Mail.IsBodyHtml = false;
			}

			if (plantilla != "") {
				if (plantilla.IndexOf("{body}", StringComparison.CurrentCultureIgnoreCase) > -1)
					sBody = plantilla.Replace("{body}", sBody);
			}


			if (Mail.IsBodyHtml) {
				sBody = sBody.Replace("\r\n", "<br />");
			}

            if (Firmar)
            {
                Mail.Body = FSCertificate.Certificate.SignMessage(sBody, Certificado);
            }
            else
            {
                Mail.Body = sBody;
            }

			if (attachments != null)
			{
				foreach (string attachmentPath in attachments)
				{
					Attachment attachment = new Attachment(attachmentPath);
					attachment.ContentDisposition.Inline = true;
					attachment.ContentDisposition.DispositionType = DispositionTypeNames.Inline;
					attachment.ContentId = Guid.NewGuid().ToString();
					attachment.ContentType.MediaType = FSLibrary.MimeType.GetMimeType(attachmentPath);
					attachment.ContentType.Name = Path.GetFileName(attachmentPath);

					Mail.Attachments.Add(attachment);
				}
			}

			SmtpClient smtpClient = new SmtpClient();
			smtpClient.Port = Port;
			smtpClient.EnableSsl = EnableSSL;
			smtpClient.Host = Server;
			smtpClient.UseDefaultCredentials = false;
			smtpClient.Credentials = new NetworkCredential(User, Password);
			smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

			try {
				if (Mail.To.Count > 0)
					smtpClient.Send(Mail);
			} catch {
				throw;
			}


            return true;
		}

		public static bool SendErrorMail(string message)
		{
			return SendErrorMail(message, null);
		}

		public static bool SendErrorMail(string message, System.Exception ex)
        {
            return SendErrorMail(message, ex, false, null);
        }

		public static bool SendErrorMail(string message, System.Exception ex, bool Firmar, System.Security.Cryptography.X509Certificates.X509Certificate2 Certificado)
		{
			if (System.Diagnostics.Debugger.IsAttached)
				return false;

			string sFrom = ConfigurationManager.AppSettings["DebugFromEmail"];
			string sTo = ConfigurationManager.AppSettings["DebugToEmail"];

			if (String.IsNullOrEmpty(sFrom) || String.IsNullOrEmpty(sTo))
				return false;

			MailMessage Mail = new MailMessage();

			string sSubject = "Error: Excepción no controlada";
			string sBody = "";

			if (ex == null && HttpContext.Current != null && HttpContext.Current.Server != null )
			{
				ex = HttpContext.Current.Server.GetLastError();

				sSubject = "Error en: " + HttpContext.Current.Request.Url + " - " +
									   HttpContext.Current.Request.FilePath;
				sBody = "Se ha registrado un error en (Global.asax): " + HttpContext.Current.Request.Url + "\n" +
										"\n";
				sBody += "Fichero: " + HttpContext.Current.Request.FilePath + "\n";
				sBody += "QueryString: " +
			HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.QueryString.ToString()) + "\n";
				string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
				sBody = sBody + "IP: <a href='https://dig.whois.com.au/ip/" + ip + "'>" + ip + "</a>\r\n";
			}
	

			if (ex != null) {
				sBody += "Error: " + ex;

				if (ex.InnerException != null) {
					sBody += "\r\nErrorInner: " + ex.InnerException;
				}
			}

			sBody += "\n\n";
			sBody += "Mensaje: " + message + "\n";
			
			if (UserPortal != null) {
				sBody = sBody + "Usuario: " + UserPortal + "\n";
				sBody = sBody + "Nombre completo: " + UserFullName + "\n";
			}

			sBody = sBody + "\r\n" + "\r\nFecha: " + System.DateTime.Now + "\r\n";

			Mail.From = new MailAddress(sFrom, "Error 500");
			Mail.To.Add(new MailAddress(sTo));
			Mail.Subject = sSubject;

            if (Firmar)
            {
                Mail.Body = FSCertificate.Certificate.SignMessage(sBody, Certificado);
            }
            else
            {
                Mail.Body = sBody;
            }

			Crypto crypt = new Crypto();

			SmtpClient client = new SmtpClient();
			client.Host = ConfigurationManager.AppSettings["DebugServer"];
			client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["DebugUser"],
				crypt.Decryp(ConfigurationManager.AppSettings["DebugPassword"]));

			try {
				client.Send(Mail);
			} catch {
                return false;
			}

            return true;
		}
    }
}