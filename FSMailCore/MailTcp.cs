#region

using System;
using System.Net.Sockets;
using System.Text;
using FSLibrary;
using DateTime = System.DateTime;

#endregion

namespace FSMail
{
    public class MailTcp
    {
        private readonly TcpClient client = new TcpClient();

        public int errNumber;
        private NetworkStream ns;

        public MailTcp()
        {
        }

        public MailTcp(string server)
        {
            SMTPServer = server;
            Port = 25;
        }

        public MailTcp(string server, int port)
        {
            SMTPServer = server;
            Port = port;
        }

        public MailTcp(string server, string from, string to, string subject, string message)
        {
            SMTPServer = server;
            Port = 25;
            From = from;
            To = to;
            Subject = subject;
            Message = message;
        }

        public MailTcp(string server, string from, string to, string subject, string message, int port)
        {
            SMTPServer = server;
            Port = port;
            From = from;
            To = to;
            Subject = subject;
            Message = message;
        }

        public string SMTPServer { get; set; }

        public string To { get; set; }

        public string From { get; set; }

        public string CarbonCopy { get; set; }

        public string BlindCarbonCopy { get; set; }


        public int Port { get; set; } = 25;

        public string Message { get; set; }

        public DateTime SendDate { get; set; } = DateTime.Now;

        public string Subject { get; set; }

        public void Open()
        {
            client.Connect(SMTPServer, Port);

            ns = client.GetStream();

            var strMessage = "HELO TEST" + "\n";

            var sendBytes = Encoding.ASCII.GetBytes(strMessage);
            ns.Write(sendBytes, 0, sendBytes.Length);
        }


        public void Close()
        {
            if (errNumber != 0) return;
            try
            {
                var strMessage = "QUIT" + "\n";

                var sendBytes = Encoding.ASCII.GetBytes(strMessage);
                ns.Write(sendBytes, 0, sendBytes.Length);
            }
            catch (Exception)
            {
                errNumber = -1;
            }

            client.Close();
        }


        public bool Send()
        {
            if (errNumber != 0) return false;
            try
            {
                string strMessage = null;

                strMessage = "MAIL FROM:" + From + "\n" + "RCPT TO:" + To + "\n" + "DATA" + "\n" + "date:" + SendDate +
                             "\n" + "from:" + From + "\n" + "to:" + To + "\n" + "cc:" + CarbonCopy + "\n" + "bcc:" +
                             BlindCarbonCopy +
                             "\n" + "subject:" + Subject + "\n" + Message + "\n" + "." + "\n";

                var sendBytes = Encoding.ASCII.GetBytes(strMessage);
                ns.Write(sendBytes, 0, sendBytes.Length);
            }
            finally
            {
                client.Close();
            }

            return true;
        }
    }
}