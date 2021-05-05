using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FSSyscat
{
    public class MailParameters
    {
        public bool EnableSsl;
        public string Logstr;
        public string Mailaddress;
        public string Mailpassword;
        public string SmtpHost;
        public int SmtpPort;

        public MailParameters(string logstr, string mailaddress, string mailpassword, string smtpHost, int smtpPort,
                      bool enablessl)
        {
            Logstr = logstr;
            Mailaddress = mailaddress;
            Mailpassword = mailpassword;
            SmtpHost = smtpHost;
            SmtpPort = smtpPort;
            EnableSsl = enablessl;
        }
    }
}
