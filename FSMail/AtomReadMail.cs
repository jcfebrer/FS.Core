using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FSMail
{
    public class MailData
    {
        public MailData(string title, string summary)
        {
            Title = title;
            Summary = summary;
        }

        public string Title { set; get; }
        public string Summary { set; get; }
    }
    public class AtomReadMail
    {
        public static List<MailData> Read(string email, string password)
        {
            System.Net.WebClient objClient = new System.Net.WebClient();
            string response;
            string title;
            string summary;

            //Creating a new xml document
            XmlDocument doc = new XmlDocument();

            //Logging in Gmail server to get data
            objClient.Credentials = new System.Net.NetworkCredential(email, password);
            //reading data and converting to string
            response = Encoding.UTF8.GetString(
                       objClient.DownloadData(@"https://mail.google.com/mail/feed/atom"));

            //loading into an XML so we can get information easily
            doc.LoadXml(response);

            //nr of emails
            var nr = doc.SelectSingleNode(@"/feed/fullcount").InnerText;

            List<MailData> result = new List<MailData>();

            //Reading the title and the summary for every email
            foreach (XmlNode node in doc.SelectNodes(@"/feed/entry"))
            {
                title = node.SelectSingleNode("title").InnerText;
                summary = node.SelectSingleNode("summary").InnerText;

                result.Add(new MailData(title, summary));
            }

            return result;
        }
    }
}
