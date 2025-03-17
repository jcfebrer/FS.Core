using FSMail;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSTests.FSMail
{
    [TestClass()]
    public class MimeTests
    {
        [TestMethod()]
        public void Main()
        {
            MimeMessage mail = new MimeMessage();
            mail.SetDate(DateTime.Now);
            mail.SetVersion("1.0");
            mail.SetFrom("sender@local.com");
            mail.SetTo("recipient1@server1.com, Nick Name <recipient2@server1.com>, \"Nick Name\" <recipient3@server3.com>, \"ÑîÌÎ\" <recipient5@server1.com>");
            mail.SetCC("recipient4@server4.com");
            mail.SetSubject("²âÊÔ³ÌÐò");//charset gb2312
            mail.SetFieldValue("X-Priority", "3 (Normal)", null);

            // Initialize header
            mail.SetContentType("multipart/mixed");

            // generate a boundary string automatically
            // if the parameter is NULL
            mail.SetBoundary(null);

            // Add a text body part
            // default Content-Type is "text/plain"
            // default Content-Transfer-Encoding is "7bit"
            MimeBody mBody = mail.CreatePart();
            mBody.SetText("Hi, there");  // set the content of the body part



            // Add a file attachment body part
            //mBody = mail.CreatePart();
            //mBody.SetDescription("enclosed photo");
            //mBody.SetTransferEncoding("base64");
            // if Content-Type is not specified, it'll be
            // set to "image/jpeg" by ReadFromFile()
            
            //mBody.ReadFromFile(".\\00.jpg");



            // Generate a simple message
            MimeMessage mail2 = new MimeMessage();
            mail2.SetFrom("abc@abc.com");
            mail2.SetTo("abc@abc.com");
            mail2.SetSubject("This is an attached message");
            mail2.SetText("Content of attached message.\r\n");


            // Attach the message
            mBody = mail.CreatePart();
            mBody.SetDescription("enclosed message");
            mBody.SetTransferEncoding("7bit");
            // if Content-Type is not specified, it'll be
            // set to "message/rfc822" by SetMessage()
            mBody.SetMessage(mail2);


            // Add an embeded multipart
            mBody = mail.CreatePart();
            mBody.SetContentType("multipart/alternative");
            mBody.SetBoundary("embeded_multipart_boundary");
            MimeBody mBodyChild = mBody.CreatePart();
            mBodyChild.SetText("Content of Part 1\r\n");
            mBodyChild = mBody.CreatePart();
            mBodyChild.SetText("Content of Part 2\r\n");


            //store content to a string buffer
            StringBuilder sb = new StringBuilder();
            mail.StoreBody(sb);

            StreamWriter sw = new StreamWriter(".\\mimetest.txt");
            sw.Write(sb.ToString());
            sw.Close();


            StreamReader sr = new StreamReader(".\\mimetest.txt");
            string message = sr.ReadToEnd();
            MimeMessage aMimeMessage = new MimeMessage();
            aMimeMessage.LoadBody(message);

            ArrayList bodylist = new ArrayList();
            aMimeMessage.GetBodyPartList(bodylist);
            for (int i = 0; i < bodylist.Count; i++)
            {
                MimeBody ab = (MimeBody)bodylist[i];
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
    }
}
