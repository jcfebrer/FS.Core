using FSMail;
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
    public class ImapTest
    {
        [TestMethod()]
        public void TestImap()
        {
            /*
             * 
             * 
             * OJO!!!!
             * Tiene que estar habilitado IMAP en la configuración de la cuenta GMAIL.
             * Configuración -> Reenvio y correo POP3 / IMAP -> Acceso IMAP -> Habilitar
             * 
             * 
             */
            Imap imap = new Imap();
            imap.Login(ConfigurationManager.AppSettings["Pop3Server"],
                Convert.ToInt32(ConfigurationManager.AppSettings["Pop3Port"]),
                ConfigurationManager.AppSettings["Pop3UserName"],
                ConfigurationManager.AppSettings["Pop3Password"],
                Convert.ToBoolean(ConfigurationManager.AppSettings["Pop3Ssl"]));

            if (imap.IsLoggedIn)
            {
                //imap.ExamineFolder(); // modo de solo-lectura
                imap.SelectFolder(); //permite la modificación

                //total messages
                Console.WriteLine("Mensajes totales: " + imap.TotalMessages.ToString());

                //total mensajes recientes
                Console.WriteLine("Mensajes recientes: " + imap.RecentMessages.ToString());

                //primer mensaje sin leer
                Console.WriteLine("Primer mensaje sin leer (UID): " + imap.FirstUnSeenMsgUID.ToString());

                if(imap.FirstUnSeenMsgUID > 0)
                {
                    ImapEmail imapEmailNoRead = imap.FetchMessage(imap.FirstUnSeenMsgUID.ToString(), true);
                }

                ArrayList saArrayNoRead = imap.SearchCommand(@"FROM ""@portuoil.net""");
                ImapEmail imapEmail1 = imap.FetchMessage(saArrayNoRead[0].ToString(), true);


                ArrayList saArrayNoReadRepsol = imap.SearchCommand(@"FROM ""@repsol.com""");

                ArrayList saArrayHeader = imap.FetchPartHeader(saArrayNoReadRepsol[0].ToString());

                string data = imap.FetchPartBody(saArrayNoReadRepsol[0].ToString());

                MimeMessage mimeMessage = new MimeMessage(data);
                ArrayList bodylist = new ArrayList();
                mimeMessage.GetBodyPartList(bodylist);
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


                string[] saSearchData = new String[1];
                saSearchData[0] = "repsol";
                ArrayList saArray = imap.SearchMessage(saSearchData);

                string suid = saArray[0].ToString();

                //Savel XML
                //string sFileName = suid + ".xml";
                //imap.SaveXML(suid, sFileName, true);

                ImapEmail imapEmail = imap.FetchMessage(suid, true);

                //Create folder
                //imap.CreateFolder("PRUEBA");

                //Move
                imap.MoveMessage(suid, "PRUEBA");

                //Delete
                //imap.SetFlag(suid, "\\Deleted");
                //imap.Expunge();

                //Marcar como no leido (no funciona bien)
                imap.SetFlag(suid, "\\Seen", false);
                imap.Expunge();

                //Tamaño buzón
                bool bUnlimitedQuota = false;
                int nUsedKBytes = 0;
                double nTotalKBytes = 0;

                imap.GetQuota("inbox", ref bUnlimitedQuota, ref nUsedKBytes, ref nTotalKBytes);
                Console.WriteLine("Unlimitedquota:{0}, UsedKBytes:{1}, TotalKBytes:{2}",
                    bUnlimitedQuota, nUsedKBytes, nTotalKBytes);

                //Tamaño mensaje
                long size = imap.GetMessageSize(suid);

                //Desconexión
                imap.LogOut();
            }
        }
    }
}
