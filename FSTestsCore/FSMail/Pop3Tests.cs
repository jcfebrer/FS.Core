using FSMailCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSTestsCore.FSMail
{
    [TestClass()]
    public class Pop3Tests
    {
        [TestMethod()]
        public void TestPop3()
        {
            int totalEmails = 0;
            List<Pop3Email> emails;

            using (Pop3 client = new Pop3(ConfigurationManager.AppSettings["Pop3Server"], 
                Convert.ToInt32(ConfigurationManager.AppSettings["Pop3Port"]), 
                ConfigurationManager.AppSettings["Pop3UserName"], 
                ConfigurationManager.AppSettings["Pop3Password"], 
                Convert.ToBoolean(ConfigurationManager.AppSettings["Pop3Ssl"]), 
                Convert.ToBoolean(ConfigurationManager.AppSettings["Pop3Recent"])))
            {
                client.Connect();

                totalEmails = client.GetEmailCount();

                emails = client.FetchEmailList(totalEmails, 0, 20000);

                //List<Pop3MessagePart> emailParts = new List<Pop3MessagePart>();
                //emailParts = client.FetchMessagePartList(totalEmails, 20000);

                string text = "";
                foreach (Pop3Email email in emails)
                {
                    text += email.Date.ToString() + " From:" + email.From + " To:" + email.To + " " + email.Subject + "\n";
                    Pop3MessagePart detalle = client.FetchMessageParts(email.Id);
                    string full = detalle.MessageText;

                    MimeMessage mimeMessage = new MimeMessage(full);
                    ArrayList bodylist = new ArrayList();
                    mimeMessage.GetBodyPartList(bodylist);
                    for (int i = 0; i < bodylist.Count; i++)
                    {
                        MimeBody? ab = (MimeBody?)bodylist[i];
                        if (ab.IsText())
                        {
                            string m = ab.GetText();
                            Console.WriteLine(m);
                        }
                        else if (ab.IsAttachment())
                        {
                            ab.WriteToFile("new" + ab.GetName());
                        }
                    }
                }

                Console.Write(text);

                client.Close();
            }
        }
    }
}
