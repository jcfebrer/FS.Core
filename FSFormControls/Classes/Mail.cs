#region

using System;
using System.Net;
using System.Net.Mail;
using FSLibrary;
using FSException;

#endregion

namespace FSFormControls
{
    public class Mail
    {
        public string[] m_AttachmentPath;
        public string m_Body = "";
        public string m_CC = "";
        public string m_CCO = "";
        public string m_From = "";
        public string m_Password = "";
        public string m_SmtpServer = "";
        public string m_Subject = "";
        public string m_To = "";
        public string m_UserName = "";

        public Mail(string strFrom, string strTo, string strSubject, string strBody)
        {
            m_From = strFrom;
            m_To = strTo;
            m_Subject = strSubject;
            m_Body = strBody;
        }

        public string UserName
        {
            get { return m_UserName; }
            set { m_UserName = value; }
        }

        public string Password
        {
            get { return m_Password; }
            set { m_Password = value; }
        }

        public string From
        {
            get { return m_From; }
            set { m_From = value; }
        }

        public string To
        {
            get { return m_To; }
            set { m_To = value; }
        }

        public string Subject
        {
            get { return m_Subject; }
            set { m_Subject = value; }
        }

        public string Body
        {
            get { return m_Body; }
            set { m_Body = value; }
        }

        public string[] AttachmentPath
        {
            get { return m_AttachmentPath; }
            set { m_AttachmentPath = value; }
        }

        public string CC
        {
            get { return m_CC; }
            set { m_CC = value; }
        }

        public string CCO
        {
            get { return m_CCO; }
            set { m_CCO = value; }
        }

        public string SmtpServer
        {
            get { return m_SmtpServer; }
            set { m_SmtpServer = value; }
        }

        public void Send()
        {
            var f = 0;
            try
            {
                var insMail = new MailMessage();

                if (string.IsNullOrEmpty(m_From)) throw new ExceptionUtil("Debe indicar la propiedad 'FROM'.");
                if (string.IsNullOrEmpty(m_To))
                {
                    throw new ExceptionUtil("Debe indicar la propiedad 'TO'.");
                }

                var destTo = m_To.Split(';');
                foreach (var s in destTo) insMail.To.Add(s);


                if (!string.IsNullOrEmpty(m_CC))
                {
                    var destCC = m_CC.Split(';');
                    foreach (var s in destCC) insMail.CC.Add(s);
                }

                if (!string.IsNullOrEmpty(m_CCO))
                {
                    var destCCO = m_CCO.Split(';');
                    foreach (var s in destCCO) insMail.Bcc.Add(s);
                }

                insMail.From = new MailAddress(m_From);
                insMail.Subject = m_Subject;
                insMail.Body = m_Body;
                if (m_AttachmentPath != null)
                    for (f = 0; f <= m_AttachmentPath.Length - 1; f++)
                        insMail.Attachments.Add(new Attachment(m_AttachmentPath[f]));


                var smtp = new SmtpClient();
                if (!string.IsNullOrEmpty(m_SmtpServer))
                {
                    smtp.Host = m_SmtpServer;
                    if (!string.IsNullOrEmpty(m_UserName))
                        smtp.Credentials = new NetworkCredential(m_UserName, m_Password);
                }

                smtp.Send(insMail);
            }
            catch (ExceptionUtil ex)
            {
                throw new ExceptionUtil(ex);
            }
        }
    }
}