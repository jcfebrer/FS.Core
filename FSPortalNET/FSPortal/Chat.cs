// // <fileheader>
// // <copyright file="Chat.cs" company="Febrer Software">
// //     Fecha: 03/07/2015
// //     Project: FSPortal
// //     Solution: FSPortalNET2008
// //     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
// //     http://www.febrersoftware.com
// // </copyright>
// // </fileheader>

#region

using System.Collections;
using System.Text;
using FSLibrary;

#endregion

namespace FSPortal
{
    public class Chat
    {
        protected static ArrayList pArray = new ArrayList();


        public void AddMessage(string sDealer, string sUser, string sMsg)
        {
            string sAddText = sDealer + "~" + sUser + "~" + sMsg;
            pArray.Add(sAddText);

            if (pArray.Count > 200)
            {
                pArray.RemoveRange(0, 10);
            }
        }


        public string GetAllMessages(string sDealer)
        {
            StringBuilder sResponse = new StringBuilder("");

            for (int i = 0; i <= pArray.Count - 1; i += i + 1)
            {
                sResponse.Append(FormatChat(pArray[i].ToString(), sDealer));
            }

            return (sResponse.ToString());
        }


        private string FormatChat(string sLine, string sDealer)
        {
            int iFirst = sLine.IndexOf("~");
            int iLast = sLine.LastIndexOf("~");

            string sDeal = TextUtil.Substring(sLine, 0, iFirst);
            if (sDeal != sDealer)
            {
                return ("");
            }

            string sUser = TextUtil.Substring(sLine, iFirst + 1, iLast - (iFirst + 1));

            string sMsg = TextUtil.Substring(sLine, iLast + 1);

            string sRet = "<STRONG>" + sUser + ": </STRONG>" + sMsg + Ui.Lf();

            return (sRet);
        }
    }
}