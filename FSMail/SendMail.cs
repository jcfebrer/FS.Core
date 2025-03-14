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
using System.Web;
using System.Collections.Generic;
using System.Net.Mime;
using System.IO;
using FSTrace;
using System.Diagnostics;
using System.Reflection;
using FSException;
using FSCertificate;

#endregion

namespace FSMail
{
	public class SendMail
	{
		private List<string> attachments;

		private string server;
        private string user;
        private string password;
        private int port;
        private bool enableSSL;
        private string errorEmail;

		private string projectName;
        private string userPortal;
        private string userFullName;

        public string Server { get => server; set => server = value; }
        public string User { get => user; set => user = value; }
        public string Password { get => password; set => password = value; }
        public int Port { get => port; set => port = value; }
        public bool EnableSSL { get => enableSSL; set => enableSSL = value; }
        public string ErrorEmail { get => errorEmail; set => errorEmail = value; }
		public string ProjectName { get => projectName; set => projectName = value; }
		public string UserPortal { get => userPortal; set => userPortal = value; }
        public string UserFullName { get => userFullName; set => userFullName = value; }

        public SendMail(string server, string user, string password, int port, bool enableSSL, string errorEmail, string projectName)
        {
			Server = server;
			User = user;
			Password = password;
			Port = port;
			EnableSSL = enableSSL;
			ErrorEmail = errorEmail;
			ProjectName = projectName;
        }

		public SendMail(string server, string user, string password, int port, bool enableSSL, string projectName)
		{
			Server = server;
			User = user;
			Password = password;
			Port = port;
			EnableSSL = enableSSL;
			ErrorEmail = user;
			ProjectName = projectName;
		}

		public bool SendMailMessage(string sTo, string sCC, string sCCO, string sSubject, string sBody, string plantilla)
        {
            return SendMailMessage(sTo, sCC, sCCO, sSubject, sBody, plantilla, false, null);
        }

		public void AddAttachment(string pathToAttachment)
        {
			if(attachments == null)
				attachments = new List<string>();

			if (!attachments.Contains(pathToAttachment))
				attachments.Add(pathToAttachment);
        }

        public bool SendMailMessage(string sTo, string sCC, string sCCO, string sSubject, string sBody, string plantilla, bool Firmar, System.Security.Cryptography.X509Certificates.X509Certificate2 Certificado)
		{
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

			//if (System.Diagnostics.Debugger.IsAttached)
			//	return false;

            Log.TraceInfo("Inicio de envio de correo.");

            MailMessage Mail = new MailMessage();

			Mail.BodyEncoding = System.Text.Encoding.UTF8;


			if (!String.IsNullOrEmpty(sTo))
				Mail.To.Add(new MailAddress(sTo));
			else
				throw new ExceptionUtil("Destinatario del mensaje 'sTo', no especificado.");

			
			if (!String.IsNullOrEmpty(User))
				Mail.From = new MailAddress(User);
			else
				throw new ExceptionUtil("Usuario de correo, no especificado.");


			if (!String.IsNullOrEmpty(sCCO)) {
				string[] toMail = sCCO.Split(';');

				foreach (string s in toMail) {
					if (s != sTo)
						Mail.Bcc.Add(new MailAddress(s));
				}
			}

			if (!String.IsNullOrEmpty(sCC)) {
				string[] toMail = sCC.Split(';');

				foreach (string s in toMail) {
					if (s != sTo)
						Mail.CC.Add(new MailAddress(s));
				}
			}

			if (!String.IsNullOrEmpty(UserPortal))
			{
				sBody = sBody + "Usuario: " + UserPortal + "\r\n";
			}
			if (!String.IsNullOrEmpty(UserFullName))
			{
				sBody = sBody + "Nombre completo: " + UserFullName + "\r\n";
			}


			sBody = sBody + "\r\n\r\nFecha: " + System.DateTime.Now + "\r\n";

			if (!String.IsNullOrEmpty(ProjectName))
				sSubject += " - " + projectName;

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
                Mail.Body = Certificate.SignMessage(sBody, Certificado);
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
			} catch(Exception e) {
				Log.TraceError(e);
				return false;
			}

			Log.TraceInfo("FIN de envio de correo.");

			return true;
		}

		public bool SendErrorMail(string message)
		{
			return SendErrorMail(message, null);
		}

		public bool SendErrorMail(string message, System.Exception ex)
        {
            return SendErrorMail(message, ex, false, null);
        }

		public bool SendErrorMail(System.Exception ex)
		{
			return SendErrorMail(ex.Message, ex, false, null);
		}

		public bool SendErrorMail(string message, System.Exception ex, bool Firmar, System.Security.Cryptography.X509Certificates.X509Certificate2 Certificado)
		{
			string sSubject = "Error: Excepción no controlada";
			string sBody = "";

            if (!String.IsNullOrEmpty(message))
            {
                sBody += "Mensaje: " + message + "\r\n";
            }

			//if (ex == null && HttpContext.Current != null && HttpContext.Current.Server != null)
			//{
			//	ex = HttpContext.Current.Server.GetLastError();
			//}

			//if (HttpContext.Current != null && HttpContext.Current.Request != null)
			//{
			//	sSubject = "Error en: " + HttpContext.Current.Request.Url + " - " +
			//							HttpContext.Current.Request.FilePath;
			//	sBody = "Se ha registrado un error en: " + HttpContext.Current.Request.Url + "\r\n" +
			//							"\r\n";
			//	sBody += "Fichero: " + HttpContext.Current.Request.FilePath + "\r\n";
			//	sBody += "RawUrl: " + HttpContext.Current.Request.RawUrl + "\r\n";
			//	sBody += "QueryString: " + HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.QueryString.ToString()) + "\r\n";
			//	string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
			//	if (!String.IsNullOrEmpty(ip))
			//	{
			//		sBody += "IP: <a href='https://www.whois.com/whois/" + ip + "'>" + ip + "</a>\r\n";
			//	}
			//}


			if (ex != null)
			{
				sBody += "--------------------------- EXCEPTION -----------------------------" + "\r\n";
                sBody += "Project Name:   " + Assembly.GetCallingAssembly().GetName().Name + "\r\n" +
					"Error Message:  " + ex.Message + "\r\n" +
					"Error:  " + ex + "\r\n";

				if (ex.InnerException != null)
				{
                    sBody += "--------------------------- INNER EXCEPTION -----------------------------" + "\r\n";
                    sBody += "Inner Message : " + ex.InnerException.Message + "\r\n";
					sBody += "Error: " + ex.InnerException + "\r\n";
				}
			}

			return SendMailMessage(ErrorEmail, "", "", sSubject, sBody,
				"", Firmar, Certificado);

		}
    }
}