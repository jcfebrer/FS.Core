// <fileheader>
// <copyright file="FuncionesForum.cs" company="Febrer Software">
//     Fecha: 30/11/2007
//     Path: clsFuncionesForum.cs
//     Copyright (c) 2003-2007 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>


using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using FSLibrary;
using FSPortal;

namespace FSForum
{
    public static class FuncionesForum
    {

        public static long getThreadID(long topicID)
        {

            long getThreadIDReturn = 0;
            topicID = System.Convert.ToInt64(NumberUtils.NumberDouble(topicID));
            if (topicID == 0)
            {
                getThreadIDReturn = 0;
                return getThreadIDReturn;
            }

            FSDatabase.BdUtils db = new FSDatabase.BdUtils("FSForum");
            string ssql = null;

            if (FSDatabase.Utils.BDType == FSDatabase.Utils.TypeBd.MySQL)
            {
                ssql = "SELECT ForumThread.Thread_ID " + "FROM ForumThread " + "WHERE ForumThread.Topic_ID = " + topicID + " ORDER BY ForumThread.Message_date DESC LIMIT 1;";

            }
            else
            {
                ssql = "SELECT Top 1 ForumThread.Thread_ID " + "FROM ForumThread " + "WHERE ForumThread.Topic_ID = " + topicID + " ORDER BY ForumThread.Message_date DESC;";

            }


            DataTable dtT = db.Execute(ssql);
            if (dtT.Rows.Count == 0)
            {
                getThreadIDReturn = 0;
            }
            else
            {
                getThreadIDReturn = System.Convert.ToInt64(dtT.Rows[0]["Thread_ID"]);
            }

            return getThreadIDReturn;
        }



        public static string getUserIDbyTopicID(long topicID)
        {

            string getUserIDbyTopicIDReturn = null;
            topicID = System.Convert.ToInt64(NumberUtils.NumberDouble(topicID));
            if (topicID == 0)
            {
                getUserIDbyTopicIDReturn = "No encontrado.";
                return getUserIDbyTopicIDReturn;
            }

            FSDatabase.BdUtils db = new FSDatabase.BdUtils("FSForum");
            string ssql = null;

            if (FSDatabase.Utils.BDType == FSDatabase.Utils.TypeBd.MySQL)
            {
                ssql = "SELECT UsuarioID FROM " + FSPortal.Variables.App.prefijoTablas + "Usuarios INNER JOIN ForumThread ON " + "UsuarioID = ForumThread.UsuarioID WHERE ForumThread.Topic_ID=" + topicID + " ORDER BY ForumThread.Message_date DESC LIMIT 1;";
            }
            else
            {
                ssql = "SELECT Top 1 Usuarios.UsuarioID FROM " + FSPortal.Variables.App.prefijoTablas + "Usuarios INNER JOIN ForumThread ON " + "Usuarios.UsuarioID = ForumThread.UsuarioID WHERE ForumThread.Topic_ID=" + topicID + " ORDER BY ForumThread.Message_date DESC;";
            }

            DataTable dtU = db.Execute(ssql);

            if (dtU.Rows.Count == 0)
            {
                getUserIDbyTopicIDReturn = "No encontrado.";
            }
            else
            {
                getUserIDbyTopicIDReturn = Functions.Valor(dtU.Rows[0]["UsuarioId"]);
            }

            return getUserIDbyTopicIDReturn;
        }


        public static string getUserNamebyTopicID(long topicID)
        {

            string getUserNamebyTopicIDReturn = null;
            topicID = System.Convert.ToInt64(NumberUtils.NumberDouble(topicID));
            if (topicID == 0)
            {
                getUserNamebyTopicIDReturn = "No encontrado.";
                return getUserNamebyTopicIDReturn;
            }

            FSDatabase.BdUtils db = new FSDatabase.BdUtils("FSForum");
            string ssql = null;

            if (FSDatabase.Utils.BDType == FSDatabase.Utils.TypeBd.MySQL)
            {
                ssql = "SELECT " + FSPortal.Variables.App.prefijoTablas + "Usuarios.Usuario FROM " + FSPortal.Variables.App.prefijoTablas + "Usuarios INNER JOIN ForumThread ON " + FSPortal.Variables.App.prefijoTablas + "Usuarios.UsuarioID = ForumThread.UsuarioID WHERE ForumThread.Topic_ID=" + topicID + " ORDER BY ForumThread.Message_date DESC LIMIT 1;";
            }
            else
            {
                ssql = "SELECT Top 1 " + FSPortal.Variables.App.prefijoTablas + "Usuarios.Usuario FROM " + FSPortal.Variables.App.prefijoTablas + "Usuarios INNER JOIN ForumThread ON " + FSPortal.Variables.App.prefijoTablas + "Usuarios.UsuarioID = ForumThread.UsuarioID WHERE ForumThread.Topic_ID=" + topicID + " ORDER BY ForumThread.Message_date DESC;";
            }

            DataTable dtU = db.Execute(ssql);

            if (dtU.Rows.Count == 0)
            {
                getUserNamebyTopicIDReturn = "No encontrado.";
            }
            else
            {
                getUserNamebyTopicIDReturn = Functions.Valor(dtU.Rows[0]["Usuario"]);
            }

            return getUserNamebyTopicIDReturn;
        }


        public static string removeAllTags(string strInputEntry)
        {
            strInputEntry = strInputEntry.Replace("&", "&amp;");
            strInputEntry = strInputEntry.Replace("<", "&lt;");
            strInputEntry = strInputEntry.Replace(">", "&gt;");
            strInputEntry = strInputEntry.Replace("'", "&#146;");
            strInputEntry = strInputEntry.Replace(@"""", "&amp;quot;");

            return strInputEntry;
        }


        public static string WYSIWYGFormatPost(string strMessage)
        {
            string wYSIWYGFormatPostReturn = null;

            strMessage = strMessage.Replace(@" border=""0"">", ">");
            strMessage = strMessage.Replace(" target=_blank>", ">");
            strMessage = strMessage.Replace(" target=_top>", ">");
            strMessage = strMessage.Replace(" target=_self>", ">");
            strMessage = strMessage.Replace(" target=_parent>", ">");
            strMessage = strMessage.Replace(@" style=""CURSOR: hand""", "");



            strMessage = strMessage.Replace("<SCRIPT> window.open=NS_ActualOpen; </SCRIPT>", "");
            strMessage = strMessage.Replace("<SCRIPT language=javascript>postamble();</SCRIPT>", "");
            strMessage = strMessage.Replace(@"<SCRIPT language=""javascript"">postamble();</SCRIPT>", "");

            if (TextUtil.IndexOf(strMessage, "<!-- ZoneLabs Popup Blocking Insertion -->") > 0)
            {
                strMessage = strMessage.Replace("<!-- ZoneLabas Popup Blocking Insertion -->", "");
                strMessage = strMessage.Replace("<SCRIPT>" + "\r\n" + "window.open=NS_ActualOpen;" + "\r\n" + "orig_onload = window.onload;" + "\r\n" + "orig_onunload = window.onunload;" + "\r\n" + "window.onload = noopen_load;" + "\r\n" + "window.onunload = noopen_unload;" + "\r\n" + "</SCRIPT>", "");
                strMessage = strMessage.Replace("window.open=NS_ActualOpen; orig_onload = window.onload; orig_onunload = window.onunload; window.onload = noopen_load; window.onunload = noopen_unload;", "");
            }

            strMessage = strMessage.Replace("<!--" + "\r\n" + "\r\n" + "window.open = SymRealWinOpen;" + "\r\n" + "\r\n" + "//-->", "");
            strMessage = strMessage.Replace("<!--" + "\r\n" + "\r\n" + "function SymError()" + "\r\n" + "{" + "\r\n" + "  return true;" + "\r\n" + "}" + "\r\n" + "\r\n" + "window.onerror = SymError;" + "\r\n" + "\r\n" + "//-->", "");
            strMessage = strMessage.Replace("<!--" + "\r\n" + "\r\n" + "function SymError()" + "\r\n" + "{" + "\r\n" + "  return true;" + "\r\n" + "}" + "\r\n" + "\r\n" + "window.onerror = SymError;" + "\r\n" + "\r\n" + "var SymRealWinOpen = window.open;" + "\r\n" + "\r\n" + "function SymWinOpen(url, name, attributes)" + "\r\n" + "{" + "\r\n" + "  return (new Object());" + "\r\n" + "}" + "\r\n" + "\r\n" + "window.open = SymWinOpen;" + "\r\n" +
            "\r\n" + "//-->", "");


            wYSIWYGFormatPostReturn = strMessage;

            return wYSIWYGFormatPostReturn;
        }





        public static string FormatPost(string strMessage)
        {
            string formatPostReturn = null;

            strMessage = strMessage.Replace("<", "&lt;");
            strMessage = strMessage.Replace(">", "&gt;");
            strMessage = strMessage.Replace("       ", "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
            strMessage = strMessage.Replace("      ", "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
            strMessage = strMessage.Replace("     ", "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
            strMessage = strMessage.Replace("    ", "&nbsp;&nbsp;&nbsp;&nbsp;");
            strMessage = strMessage.Replace("   ", "&nbsp;&nbsp;&nbsp;");
            strMessage = strMessage.Replace(Convert.ToChar(9).ToString(), "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
            strMessage = strMessage.Replace(Convert.ToChar(10).ToString(), "<br />");

            formatPostReturn = strMessage;

            return formatPostReturn;
        }


        public static string FormatForumCodes(string strMessage)
        {
            string formatForumCodesReturn = null;


            string strTempMessage = null;
            string strMessageLink = null;
            long lngStartPos = 0;
            long lngEndPos = 0;
            int intLoop = 0;



            if (FSForum.Variables.Forum.blnEmoticons == true)
            {
                for (intLoop = 1; intLoop <= FSForum.Variables.Forum.saryEmoticons.GetUpperBound(0); intLoop++)
                {
                    strMessage = strMessage.Replace(FSForum.Variables.Forum.saryEmoticons[intLoop, 2], @"<img border=""0"" src=""" + FSForum.Variables.Forum.saryEmoticons[intLoop, 3] + @""">");
                }
            }

            strMessage = strMessage.Replace("[B]", "<strong>");
            strMessage = strMessage.Replace("[/B]", "</strong>");
            strMessage = strMessage.Replace("[STRONG]", "<strong>");
            strMessage = strMessage.Replace("[/STRONG]", "</strong>");
            strMessage = strMessage.Replace("[I]", "<em>");
            strMessage = strMessage.Replace("[/I]", "</em>");
            strMessage = strMessage.Replace("[EM]", "<em>");
            strMessage = strMessage.Replace("[/EM]", "</em>");
            strMessage = strMessage.Replace("[U]", "<u>");
            strMessage = strMessage.Replace("[/U]", "</u>");

            strMessage = strMessage.Replace("[HR]", "<hr />");
            strMessage = strMessage.Replace("[LIST=1]", "<ol>");
            strMessage = strMessage.Replace("[/LIST=1]", "</ol>");
            strMessage = strMessage.Replace("[LIST]", "<ul>");
            strMessage = strMessage.Replace("[/LIST]", "</ul>");
            strMessage = strMessage.Replace("[LI]", "<li>");
            strMessage = strMessage.Replace("[/LI]", "</li>");
            strMessage = strMessage.Replace("[CENTER]", "<center>");
            strMessage = strMessage.Replace("[/CENTER]", "</center>");


            strMessage = strMessage.Replace("[BR]", "<br />");

            strMessage = strMessage.Replace("[P]", "<p>");
            strMessage = strMessage.Replace("[/P]", "</p>");
            strMessage = strMessage.Replace("[P ALIGN=CENTER]", "<p align=center>");
            strMessage = strMessage.Replace("[P ALIGN=LEFT]", "<p align=left>");
            strMessage = strMessage.Replace("[P ALIGN=RIGHT]", "<p align=right>");

            strMessage = strMessage.Replace("[DIV]", "<div>");
            strMessage = strMessage.Replace("[/DIV]", "</div>");
            strMessage = strMessage.Replace("[DIV ALIGN=CENTER]", "<div align=center>");
            strMessage = strMessage.Replace("[DIV ALIGN=LEFT]", "<div align=left>");
            strMessage = strMessage.Replace("[DIV ALIGN=RIGHT]", "<div align=right>");

            strMessage = strMessage.Replace("[BLOCKQUOTE]", "<blockquote>");
            strMessage = strMessage.Replace("[/BLOCKQUOTE]", "</blockquote<");

            strMessage = strMessage.Replace("[SIZE=1]", @"<font size=""1"">");
            strMessage = strMessage.Replace("[SIZE=2]", @"<font size=""2"">");
            strMessage = strMessage.Replace("[SIZE=3]", @"<font size=""3"">");
            strMessage = strMessage.Replace("[SIZE=4]", @"<font size=""4"">");
            strMessage = strMessage.Replace("[SIZE=5]", @"<font size=""5"">");
            strMessage = strMessage.Replace("[SIZE=6]", @"<font size=""6"">");
            strMessage = strMessage.Replace("[/SIZE]", "</font>");

            strMessage = strMessage.Replace("[FONT=Arial]", @"<font face=""Arial, Helvetica, sans-serif"">");
            strMessage = strMessage.Replace("[FONT=Courier]", @"<font face=""Courier New, Courier, mono"">");
            strMessage = strMessage.Replace("[FONT=Times]", @"<font face=""Times New Roman, Times, serif"">");
            strMessage = strMessage.Replace("[FONT=Verdana]", @"<font face=""Verdana, Arial, Helvetica, sans-serif"">");
            strMessage = strMessage.Replace("[/FONT]", "</font>");

            strMessage = strMessage.Replace("[BLACK]", @"<font color=""black"">");
            strMessage = strMessage.Replace("[WHITE]", @"<font color=""white"">");
            strMessage = strMessage.Replace("[BLUE]", @"<font color=""blue"">");
            strMessage = strMessage.Replace("[RED]", @"<font color=""red"">");
            strMessage = strMessage.Replace("[GREEN]", @"<font color=""green"">");
            strMessage = strMessage.Replace("[YELLOW]", @"<font color=""yellow"">");
            strMessage = strMessage.Replace("[ORANGE]", @"<font color=""orange"">");
            strMessage = strMessage.Replace("[BROWN]", @"<font color=""brown"">");
            strMessage = strMessage.Replace("[MAGENTA]", @"<font color=""magenta"">");
            strMessage = strMessage.Replace("[CYAN]", @"<font color=""cyan"">");
            strMessage = strMessage.Replace("[LIME GREEN]", @"<font color=""limegreen"">");


            while (TextUtil.IndexOf(strMessage, "[IMG]") > 0 & TextUtil.IndexOf(strMessage, "[/IMG]") > 0)
            {

                lngStartPos = TextUtil.IndexOf(strMessage, "[IMG]");

                lngEndPos = TextUtil.IndexOf(strMessage, (int)lngStartPos, "[/IMG]") + 6;

                if (lngEndPos < lngStartPos)
                {
                    lngEndPos = lngStartPos + 6;
                }

                strMessageLink = TextUtil.Substring(strMessage, NumberUtils.NumberInt(lngStartPos), NumberUtils.NumberInt((lngEndPos - lngStartPos))).Trim();

                strTempMessage = strMessageLink;

                strTempMessage = strTempMessage.Replace("[IMG]", @"<img src=""");
                if (TextUtil.IndexOf(strTempMessage, "[/IMG]") > 0)
                {
                    strTempMessage = strTempMessage.Replace("[/IMG]", @""">");
                }
                else
                {
                    strTempMessage = strTempMessage + ">";
                }

                strMessage = strMessage.Replace(strMessageLink, strTempMessage);
            }




            while (TextUtil.IndexOf(strMessage, "[URL=") > 0 & TextUtil.IndexOf(strMessage, "[/URL]") > 0)
            {

                lngStartPos = TextUtil.IndexOf(strMessage, "[URL=");

                lngEndPos = TextUtil.IndexOf(strMessage, (int)lngStartPos, "[/URL]") + 6;

                if (lngEndPos < lngStartPos)
                {
                    lngEndPos = lngStartPos + 7;
                }

                strMessageLink = TextUtil.Substring(strMessage, NumberUtils.NumberInt(lngStartPos), NumberUtils.NumberInt((lngEndPos - lngStartPos))).Trim();

                strTempMessage = strMessageLink;

                strTempMessage = strTempMessage.Replace("[URL=", @"<a href=""");

                if (TextUtil.IndexOf(strTempMessage, "[/URL]") > 0)
                {
                    strTempMessage = strTempMessage.Replace("[/URL]", "</a>");
                    strTempMessage = strTempMessage.Replace("]", @""">");
                }
                else
                {
                    strTempMessage = strTempMessage + ">";
                }

                strMessage = strMessage.Replace(strMessageLink, strTempMessage);
            }




            while (TextUtil.IndexOf(strMessage, "[URL]") > 0 & TextUtil.IndexOf(strMessage, "[/URL]") > 0)
            {

                lngStartPos = TextUtil.IndexOf(strMessage, "[URL]");

                lngEndPos = TextUtil.IndexOf(strMessage, (int)lngStartPos, "[/URL]") + 6;

                if (lngEndPos < lngStartPos)
                {
                    lngEndPos = lngStartPos + 6;
                }

                strMessageLink = TextUtil.Substring(strMessage, NumberUtils.NumberInt(lngStartPos), NumberUtils.NumberInt((lngEndPos - lngStartPos))).Trim();

                strTempMessage = strMessageLink;

                strTempMessage = strTempMessage.Replace("[URL]", "");
                strTempMessage = strTempMessage.Replace("[/URL]", "");

                strTempMessage = @"<a href=""" + strTempMessage + @""">" + strTempMessage + "</a>";

                strMessage = strMessage.Replace(strMessageLink, strTempMessage);
            }




            while (TextUtil.IndexOf(strMessage, "[EMAIL=") > 0 & TextUtil.IndexOf(strMessage, "[/EMAIL]") > 0)
            {

                lngStartPos = TextUtil.IndexOf(strMessage, "[EMAIL=");

                lngEndPos = TextUtil.IndexOf(strMessage, (int)lngStartPos, "[/EMAIL]") + 8;

                if (lngEndPos < lngStartPos)
                {
                    lngEndPos = lngStartPos + 9;
                }

                strMessageLink = TextUtil.Substring(strMessage, NumberUtils.NumberInt(lngStartPos), NumberUtils.NumberInt((lngEndPos - lngStartPos))).Trim();

                strTempMessage = strMessageLink;

                strTempMessage = strTempMessage.Replace("[EMAIL=", @"<a href=""mailto:");
                if (TextUtil.IndexOf(strTempMessage, "[/EMAIL]") > 0)
                {
                    strTempMessage = strTempMessage.Replace("[/EMAIL]", "</a>");
                    strTempMessage = strTempMessage.Replace("]", @""">");
                }
                else
                {
                    strTempMessage = strTempMessage + ">";
                }


                strMessage = strMessage.Replace(strMessageLink, strTempMessage);
            }




            while (TextUtil.IndexOf(strMessage, "[FILE=") > 0 & TextUtil.IndexOf(strMessage, "[/FILE]") > 0)
            {

                lngStartPos = TextUtil.IndexOf(strMessage, "[FILE=");

                lngEndPos = TextUtil.IndexOf(strMessage, (int)lngStartPos, "[/FILE]") + 7;

                if (lngEndPos < lngStartPos)
                {
                    lngEndPos = lngStartPos + 8;
                }

                strMessageLink = TextUtil.Substring(strMessage, NumberUtils.NumberInt(lngStartPos), NumberUtils.NumberInt((lngEndPos - lngStartPos))).Trim();

                strTempMessage = strMessageLink;

                strTempMessage = strTempMessage.Replace("[FILE=", @"<a target=""_blank"" href=""");
                if (TextUtil.IndexOf(strTempMessage, "[/FILE]") > 0)
                {
                    strTempMessage = strTempMessage.Replace("[/FILE]", "</a>");
                    strTempMessage = strTempMessage.Replace("]", @""">");
                }
                else
                {
                    strTempMessage = strTempMessage + ">";
                }

                strMessage = strMessage.Replace(strMessageLink, strTempMessage);
            }



            while (TextUtil.IndexOf(strMessage, "[COLOR=") > 0 & TextUtil.IndexOf(strMessage, "[/COLOR]") > 0)
            {

                lngStartPos = TextUtil.IndexOf(strMessage, "[COLOR=");
                lngEndPos = TextUtil.IndexOf(strMessage, (int)lngStartPos, "[/COLOR]") + 8;

                if (lngEndPos < lngStartPos)
                {
                    lngEndPos = lngStartPos + 9;
                }

                strMessageLink = TextUtil.Substring(strMessage, NumberUtils.NumberInt(lngStartPos), NumberUtils.NumberInt((lngEndPos - lngStartPos))).Trim();

                strTempMessage = strMessageLink;

                strTempMessage = strTempMessage.Replace("[COLOR=", "<font color=");
                if (TextUtil.IndexOf(strTempMessage, "[/COLOR]") > 0)
                {
                    strTempMessage = strTempMessage.Replace("[/COLOR]", "</font>");
                    strTempMessage = strTempMessage.Replace("]", ">");
                }
                else
                {
                    strTempMessage = strTempMessage + ">";
                }

                strMessage = strMessage.Replace(strMessageLink, strTempMessage);
            }

            strMessage = strMessage.Replace("[/COLOR]", "</font>");


            formatForumCodesReturn = strMessage;
            return formatForumCodesReturn;
        }







        public static string formatUserQuote(string strMessage)
        {
            string formatUserQuoteReturn = null;


            string strQuotedUsuarios = "";
            string strQuotedMessage = "";
            long lngStartPos = 0;
            long lngEndPos = 0;
            string strBuildQuote = "";
            string strOriginalQuote = null;

            while (TextUtil.IndexOf(strMessage, "[QUOTE=") > 0 & TextUtil.IndexOf(strMessage, "[/QUOTE]") > 0)
            {

                lngStartPos = TextUtil.IndexOf(strMessage, "[QUOTE=") + 7;
                lngEndPos = TextUtil.IndexOf(strMessage, (int)lngStartPos, "]");

                if (lngStartPos > 6 & lngEndPos > 0)
                {
                    strQuotedUsuarios = TextUtil.Substring(strMessage, NumberUtils.NumberInt(lngStartPos), NumberUtils.NumberInt(lngEndPos - lngStartPos)).Trim();
                }



                lngStartPos = lngStartPos + strQuotedUsuarios.Length + 1;
                lngEndPos = TextUtil.IndexOf(strMessage, (int)lngStartPos, "[/QUOTE]");

                if (lngEndPos - lngStartPos <= 0)
                {
                    lngEndPos = lngStartPos + strQuotedUsuarios.Length;
                }

                if (lngEndPos > lngStartPos)
                {

                    strQuotedMessage = TextUtil.Substring(strMessage, NumberUtils.NumberInt(lngStartPos), NumberUtils.NumberInt(lngEndPos - lngStartPos)).Trim();

                    strQuotedUsuarios = strQuotedUsuarios.Replace(@"""", "");

                    strBuildQuote = @"<table width=""95%"" border=""0"" align=""center"" cellpadding=""0"" cellspacing=""0"">";
                    strBuildQuote = strBuildQuote + "\r\n" + @"<tr><td class=""bold"">" + strQuotedUsuarios + " " + FSForum.Variables.Forum.strTxtWrote + ":<br />";
                    strBuildQuote = strBuildQuote + "\r\n" + @"   <table width=""100%"" border=""0"" cellpadding=""1"" cellspacing=""0"" bgcolor=""" + FSForum.Variables.Forum.strTableQuoteBorderColour + @""">";
                    strBuildQuote = strBuildQuote + "\r\n" + "    <tr>";
                    strBuildQuote = strBuildQuote + "\r\n" + @"    <td><table width=""100%"" border=""0"" cellpadding=""2"" cellspacing=""0"" bgcolor=""" + FSForum.Variables.Forum.strTableQuoteColour + @""">";
                    strBuildQuote = strBuildQuote + "\r\n" + "      <tr>";
                    strBuildQuote = strBuildQuote + "\r\n" + @"       <td class=""text"">" + strQuotedMessage + "</td>";
                    strBuildQuote = strBuildQuote + "\r\n" + "      </tr>";
                    strBuildQuote = strBuildQuote + "\r\n" + "     </table></td>";
                    strBuildQuote = strBuildQuote + "\r\n" + "   </tr>";
                    strBuildQuote = strBuildQuote + "\r\n" + "  </table></td>";
                    strBuildQuote = strBuildQuote + "\r\n" + "</tr>";
                    strBuildQuote = strBuildQuote + "\r\n" + "</table>";
                }



                lngStartPos = TextUtil.IndexOf(strMessage, "[QUOTE=");
                lngEndPos = TextUtil.IndexOf(strMessage, (int)lngStartPos, "[/QUOTE]") + 8;

                if (lngEndPos - lngStartPos <= 7)
                {
                    lngEndPos = lngStartPos + strQuotedUsuarios.Length + 8;
                }

                strOriginalQuote = TextUtil.Substring(strMessage, NumberUtils.NumberInt(lngStartPos), NumberUtils.NumberInt(lngEndPos - lngStartPos)).Trim();

                if (strBuildQuote != "")
                {
                    strMessage = strMessage.Replace(strOriginalQuote, strBuildQuote);
                }
                else
                {
                    string reemp = TextUtil.Replace(strOriginalQuote, "[", "&#91;");
                    strMessage = strMessage.Replace(strOriginalQuote, reemp);
                }
            }

            formatUserQuoteReturn = strMessage;

            return formatUserQuoteReturn;
        }






        public static string formatQuote(string strMessage)
        {
            string formatQuoteReturn = null;


            string strQuotedMessage = null;
            long lngStartPos = 0;
            long lngEndPos = 0;
            string strBuildQuote = "";
            string strOriginalQuote = null;

            while (TextUtil.IndexOf(strMessage, "[QUOTE]") > 0 & TextUtil.IndexOf(strMessage, "[/QUOTE]") > 0)
            {

                lngStartPos = TextUtil.IndexOf(strMessage, "[QUOTE]") + 7;
                lngEndPos = TextUtil.IndexOf(strMessage, (int)lngStartPos, "[/QUOTE]");

                if (lngEndPos < lngStartPos)
                {
                    lngEndPos = lngStartPos + 7;
                }

                if (lngEndPos > lngStartPos)
                {

                    strQuotedMessage = TextUtil.Substring(strMessage, NumberUtils.NumberInt(lngStartPos), NumberUtils.NumberInt(lngEndPos - lngStartPos)).Trim();


                    strBuildQuote = @"<table width=""95%"" border=""0"" align=""center"" cellpadding=""0"" cellspacing=""0"">";
                    strBuildQuote = strBuildQuote + "\r\n" + @"<tr><td class=""bold"">" + FSForum.Variables.Forum.strTxtQuote + ":<br />";
                    strBuildQuote = strBuildQuote + "\r\n" + @"   <table width=""100%"" border=""0"" cellpadding=""1"" cellspacing=""0"" bgcolor=""" + FSForum.Variables.Forum.strTableQuoteBorderColour + @""">";
                    strBuildQuote = strBuildQuote + "\r\n" + "    <tr>";
                    strBuildQuote = strBuildQuote + "\r\n" + @"    <td><table width=""100%"" border=""0"" cellpadding=""2"" cellspacing=""0"" bgcolor=""" + FSForum.Variables.Forum.strTableQuoteBorderColour + @""">";
                    strBuildQuote = strBuildQuote + "\r\n" + "      <tr>";
                    strBuildQuote = strBuildQuote + "\r\n" + @"       <td class=""text"">" + strQuotedMessage + "</td>";
                    strBuildQuote = strBuildQuote + "\r\n" + "      </tr>";
                    strBuildQuote = strBuildQuote + "\r\n" + "     </table></td>";
                    strBuildQuote = strBuildQuote + "\r\n" + "   </tr>";
                    strBuildQuote = strBuildQuote + "\r\n" + "  </table></td>";
                    strBuildQuote = strBuildQuote + "\r\n" + "</tr>";
                    strBuildQuote = strBuildQuote + "\r\n" + "</table>";
                }


                lngStartPos = TextUtil.IndexOf(strMessage, "[QUOTE]");
                lngEndPos = TextUtil.IndexOf(strMessage, (int)lngStartPos, "[/QUOTE]") + 8;

                if (lngEndPos < lngStartPos)
                {
                    lngEndPos = lngStartPos + 7;
                }

                strOriginalQuote = TextUtil.Substring(strMessage, NumberUtils.NumberInt(lngStartPos), NumberUtils.NumberInt(lngEndPos - lngStartPos)).Trim();

                if (strBuildQuote != "")
                {
                    strMessage = strMessage.Replace(strOriginalQuote, strBuildQuote);
                }
                else
                {
                    string reemp = TextUtil.Replace(strOriginalQuote, "[", "&#91;");
                    strMessage = strMessage.Replace(strOriginalQuote, reemp);
                }
            }

            formatQuoteReturn = strMessage;

            return formatQuoteReturn;
        }







        public static string formatCode(string strMessage)
        {
            string formatCodeReturn = null;


            string strCodeMessage = null;
            long lngStartPos = 0;
            long lngEndPos = 0;
            string strBuildCodeBlock = "";
            string strOriginalCodeBlock = null;

            while (TextUtil.IndexOf(strMessage, "[CODE]") > 0 & TextUtil.IndexOf(strMessage, "[/CODE]") > 0)
            {

                lngStartPos = TextUtil.IndexOf(strMessage, "[CODE]") + 6;
                lngEndPos = TextUtil.IndexOf(strMessage, (int)lngStartPos, "[/CODE]");

                if (lngEndPos < lngStartPos)
                {
                    lngEndPos = lngStartPos + 6;
                }

                if (lngEndPos > lngStartPos)
                {
                    strCodeMessage = TextUtil.Substring(strMessage, NumberUtils.NumberInt(lngStartPos), NumberUtils.NumberInt(lngEndPos - lngStartPos)).Trim();

                    strCodeMessage = strCodeMessage.Replace("       ", "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                    strCodeMessage = strCodeMessage.Replace("      ", "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                    strCodeMessage = strCodeMessage.Replace("     ", "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                    strCodeMessage = strCodeMessage.Replace("    ", "&nbsp;&nbsp;&nbsp;&nbsp;");
                    strCodeMessage = strCodeMessage.Replace("   ", "&nbsp;&nbsp;&nbsp;");
                    strCodeMessage = strCodeMessage.Replace(Convert.ToChar(9).ToString(), "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");


                    strBuildCodeBlock = @"<table width=""95%"" border=""0"" align=""center"" cellpadding=""0"" cellspacing=""0"">";
                    strBuildCodeBlock = strBuildCodeBlock + "\r\n" + @"<tr><td class=""bold"">" + FSForum.Variables.Forum.strTxtCode + ":<br />";
                    strBuildCodeBlock = strBuildCodeBlock + "\r\n" + @"   <table width=""100%"" border=""0"" cellpadding=""1"" cellspacing=""0"" bgcolor=""" + FSForum.Variables.Forum.strTableQuoteBorderColour + @""">";
                    strBuildCodeBlock = strBuildCodeBlock + "\r\n" + "    <tr>";
                    strBuildCodeBlock = strBuildCodeBlock + "\r\n" + @"    <td><table width=""100%"" border=""0"" cellpadding=""2"" cellspacing=""0"" bgcolor=""" + FSForum.Variables.Forum.strTableQuoteBorderColour + @""">";
                    strBuildCodeBlock = strBuildCodeBlock + "\r\n" + "      <tr>";
                    strBuildCodeBlock = strBuildCodeBlock + "\r\n" + @"       <td class=""text"" style=""font-family: Courier New, Courier, mono;"">" + strCodeMessage + "</td>";
                    strBuildCodeBlock = strBuildCodeBlock + "\r\n" + "      </tr>";
                    strBuildCodeBlock = strBuildCodeBlock + "\r\n" + "     </table></td>";
                    strBuildCodeBlock = strBuildCodeBlock + "\r\n" + "   </tr>";
                    strBuildCodeBlock = strBuildCodeBlock + "\r\n" + "  </table></td>";
                    strBuildCodeBlock = strBuildCodeBlock + "\r\n" + "</tr>";
                    strBuildCodeBlock = strBuildCodeBlock + "\r\n" + "</table>";
                }


                lngStartPos = TextUtil.IndexOf(strMessage, "[CODE]");
                lngEndPos = TextUtil.IndexOf(strMessage, (int)lngStartPos, "[/CODE]") + 7;

                if (lngEndPos < lngStartPos)
                {
                    lngEndPos = lngStartPos + 6;
                }

                strOriginalCodeBlock = TextUtil.Substring(strMessage, NumberUtils.NumberInt(lngStartPos), NumberUtils.NumberInt(lngEndPos - lngStartPos)).Trim();

                if (strBuildCodeBlock != "")
                {
                    strMessage = strMessage.Replace(strOriginalCodeBlock, strBuildCodeBlock);
                }
                else
                {
                    string reemp = TextUtil.Replace(strOriginalCodeBlock, "[", "&#91;");
                    strMessage = strMessage.Replace(strOriginalCodeBlock, reemp);
                }
            }

            formatCodeReturn = strMessage;

            return formatCodeReturn;
        }







        public static string formatFlash(string strMessage)
        {
            string formatFlashReturn = null;


            long lngStartPos = 0;
            long lngEndPos = 0;
            string[] saryFlashAttributes = null;
            int intAttrbuteLoop = 0;
            string strFlashWidth = null;
            int intFlashWidth = 0;
            string strFlashHeight = null;
            int intFlashHeight = 0;
            string strBuildFlashLink = null;
            string strTempFlashMsg = null;
            string strFlashLink = null;



            while (TextUtil.IndexOf(strMessage, "[FLASH") > 0 & TextUtil.IndexOf(strMessage, "[/FLASH]") > 0)
            {

                intFlashWidth = 50;
                intFlashHeight = 50;
                strFlashLink = "";
                strBuildFlashLink = "";
                strTempFlashMsg = "";

                lngStartPos = TextUtil.IndexOf(strMessage, "[FLASH");
                lngEndPos = TextUtil.IndexOf(strMessage, (int)lngStartPos, "[/FLASH]") + 8;

                if (lngEndPos < lngStartPos)
                {
                    lngEndPos = lngStartPos + 6;
                }

                strTempFlashMsg = TextUtil.Substring(strMessage, NumberUtils.NumberInt(lngStartPos), NumberUtils.NumberInt(lngEndPos - lngStartPos)).Trim();



                lngStartPos = TextUtil.IndexOf(strTempFlashMsg, "[FLASH") + 6;
                lngEndPos = TextUtil.IndexOf(strTempFlashMsg, (int)lngStartPos, "]");

                if (lngEndPos < lngStartPos)
                {
                    lngEndPos = lngStartPos;
                }

                if (strTempFlashMsg != "")
                {

                    saryFlashAttributes = TextUtil.Trim(TextUtil.Substring(strTempFlashMsg, NumberUtils.NumberInt(lngStartPos), NumberUtils.NumberInt(lngEndPos - lngStartPos))).Split(" ".Split("".ToCharArray()), StringSplitOptions.None);

                    for (intAttrbuteLoop = 0; intAttrbuteLoop <= saryFlashAttributes.GetUpperBound(0); intAttrbuteLoop++)
                    {

                        if (TextUtil.IndexOf(saryFlashAttributes[intAttrbuteLoop], "WIDTH=") > 0)
                        {

                            strFlashWidth = saryFlashAttributes[intAttrbuteLoop].Replace("WIDTH=", "");

                            if (NumberUtils.IsNumeric(strFlashWidth))
                            {
                                intFlashWidth = int.Parse(strFlashWidth);
                            }
                        }

                        if (TextUtil.IndexOf(saryFlashAttributes[intAttrbuteLoop], "HEIGHT=") > 0)
                        {

                            strFlashHeight = saryFlashAttributes[intAttrbuteLoop].Replace("HEIGHT=", "");

                            if (NumberUtils.IsNumeric(strFlashHeight))
                            {
                                intFlashHeight = int.Parse(strFlashHeight);
                            }
                        }
                    }



                    lngStartPos = TextUtil.IndexOf(strTempFlashMsg, "]") + 1;
                    lngEndPos = TextUtil.IndexOf(strTempFlashMsg, (int)lngStartPos, "[/FLASH]");

                    if (lngEndPos < lngStartPos)
                    {
                        lngEndPos = lngStartPos + 8;
                    }

                    strFlashLink = TextUtil.Substring(strTempFlashMsg, NumberUtils.NumberInt(lngStartPos), NumberUtils.NumberInt((lngEndPos - lngStartPos))).Trim();


                    if (strFlashLink != "")
                    {
                        strBuildFlashLink = @"<embed src=""" + strFlashLink + @"""";
                        strBuildFlashLink = strBuildFlashLink + " quality=high width=" + intFlashWidth + " height=" + intFlashHeight + @" type=""application/x-shockwave-flash"" pluginspage=""http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash""></embed>";
                    }
                }



                if (strBuildFlashLink != "")
                {
                    strMessage = strMessage.Replace(strTempFlashMsg, strBuildFlashLink);
                }
                else
                {
                    string reemp = TextUtil.Replace(strTempFlashMsg, "[", "&#91;");
                    strMessage = strMessage.Replace(strTempFlashMsg, reemp);
                }
            }

            formatFlashReturn = strMessage;

            return formatFlashReturn;
        }






        public static string editedXMLParser(string strMessage)
        {
            string editedXMLParserReturn = null;

            string strEditedUsuarios = null;
            System.DateTime dtmEditedDate = System.DateTime.MinValue;
            long lngStartPos = 0;
            long lngEndPos = 0;


            lngStartPos = TextUtil.IndexOf(strMessage, "<editID>") + 8;
            lngEndPos = TextUtil.IndexOf(strMessage, "</editID>");
            if (lngEndPos < lngStartPos)
            {
                lngEndPos = lngStartPos;
            }


            string le = TextUtil.Substring(strMessage, NumberUtils.NumberInt(lngStartPos), NumberUtils.NumberInt(lngEndPos - lngStartPos));
            strEditedUsuarios = le.Trim();

            lngStartPos = TextUtil.IndexOf(strMessage, "<editDate>") + 10;
            lngEndPos = TextUtil.IndexOf(strMessage, "</editDate>");
            if (lngEndPos < lngStartPos)
            {
                lngEndPos = lngStartPos;
            }

            double ticks = 0;
            string ti = TextUtil.Substring(strMessage, NumberUtils.NumberInt(lngStartPos), NumberUtils.NumberInt(lngEndPos - lngStartPos));
            ticks = Convert.ToDouble(ti.Trim());
            dtmEditedDate = new System.DateTime(System.Convert.ToInt64(ticks));

            if (!FSLibrary.DateTimeUtil.IsDate(dtmEditedDate.ToString()))
            {
                dtmEditedDate = System.DateTime.Now;
            }


            lngStartPos = TextUtil.IndexOf(strMessage, "<edited>");
            lngEndPos = TextUtil.IndexOf(strMessage, "</edited>") + 9;
            if (lngEndPos < lngStartPos)
            {
                lngEndPos = lngStartPos;
            }

            string po = TextUtil.Trim(TextUtil.Substring(strMessage, NumberUtils.NumberInt(lngStartPos), NumberUtils.NumberInt(lngEndPos - lngStartPos)));
            strMessage = strMessage.Replace(po, "");


            if (strEditedUsuarios != "")
            {
                editedXMLParserReturn = strMessage + @"<span class=""smText""><br /><br />" + FSForum.Variables.Forum.strTxtEditBy + " " + strEditedUsuarios + " " + FSForum.Variables.Forum.strTxtOn + " " + FSForum.FuncionesFecha.DateFormat(dtmEditedDate, FSForum.FuncionesFecha.saryDateTimeData) + " " + FSForum.Variables.Forum.strTxtAt + " " + FSForum.FuncionesFecha.TimeFormat(dtmEditedDate, FSForum.FuncionesFecha.saryDateTimeData) + "</span>";
            }
            else
            {
                return "";
            }
            return editedXMLParserReturn;
        }







        public static string ConvertToText(string strMessage)
        {
            string convertToTextReturn = null;

            string strTempMessage = null;
            string strMessageLink = null;
            long lngStartPos = 0;
            long lngEndPos = 0;

            strMessage = strMessage.Replace(@" target=""_blank""", "");


            while (TextUtil.IndexOf(strMessage, @"<a href=""") > 0 & TextUtil.IndexOf(strMessage, "</a>") > 0)
            {

                lngStartPos = TextUtil.IndexOf(strMessage, @"<a href=""");

                lngEndPos = TextUtil.IndexOf(strMessage, (int)lngStartPos, "</a>") + 4;

                if (lngEndPos - lngStartPos <= 9)
                {
                    lngEndPos = lngStartPos + 9;
                }

                string tt = TextUtil.Substring(strMessage, NumberUtils.NumberInt(lngStartPos), NumberUtils.NumberInt((lngEndPos - lngStartPos)));
                strMessageLink = tt.Trim();

                strTempMessage = strMessageLink;

                if (TextUtil.IndexOf(strTempMessage, @"src=""") > 0)
                {
                    strTempMessage = strTempMessage.Replace(@"<a href=""", "");
                    strTempMessage = strTempMessage.Replace("</a>", " ");
                }
                else
                {
                    strTempMessage = strTempMessage.Replace(@"<a href=""", " <font color='#0000FF'>");
                    strTempMessage = strTempMessage.Replace("</a>", " ");
                    strTempMessage = strTempMessage.Replace(@""">", "</font> - ");
                }

                strMessage = strMessage.Replace(strMessageLink, strTempMessage);
            }

            strMessage = strMessage.Replace(@"<a href= """, "");
            strMessage = strMessage.Replace("<a href='", "");
            strMessage = strMessage.Replace("</a>", "");

            convertToTextReturn = strMessage;

            return convertToTextReturn;
        }




        public static string EditPostConvertion(string strMessage)
        {

            string strTempMessage = null;
            string strMessageLink = null;
            long lngStartPos = 0;
            long lngEndPos = 0;
            int intLoop = 0;


            strMessage = strMessage.Replace(@" target=""_blank""", "");
            strMessage = strMessage.Replace(@" border=""0""", "");
            strMessage = strMessage.Replace(@"<img src= """, @"<img src=""");
            strMessage = strMessage.Replace(@"<a href= """, @"<a href=""");



            for (intLoop = 1; intLoop <= FSForum.Variables.Forum.saryEmoticons.GetUpperBound(0); intLoop++)
            {
                strMessage = strMessage.Replace(@"<img src=""" + FSForum.Variables.Forum.saryEmoticons[intLoop, 3] + @""">", FSForum.Variables.Forum.saryEmoticons[intLoop, 2]);
            }



            if (TextUtil.IndexOf(strMessage, "<edited>") > 0)
            {
                strMessage = removeEditorUsuarios(strMessage);
            }


            strMessage = strMessage.Replace("<b>", "[B]");
            strMessage = strMessage.Replace("</b>", "[/B]");
            strMessage = strMessage.Replace("<i>", "[I]");
            strMessage = strMessage.Replace("</i>", "[/I]");
            strMessage = strMessage.Replace("<u>", "[U]");
            strMessage = strMessage.Replace("</u>", "[/U]");

            strMessage = strMessage.Replace("<hr />", "[HR]");
            strMessage = strMessage.Replace("<hr />", "[HR]");
            strMessage = strMessage.Replace("<hr />", "[HR]");
            strMessage = strMessage.Replace("<ol>", "[LIST=1]");
            strMessage = strMessage.Replace("</ol>", "[/LIST=1]");
            strMessage = strMessage.Replace("<ul>", "[LIST]");
            strMessage = strMessage.Replace("</ul>", "[/LIST]");
            strMessage = strMessage.Replace("<li>", "[LI]");
            strMessage = strMessage.Replace("</li>", "[/LI]");
            strMessage = strMessage.Replace("<center>", "[CENTER]");
            strMessage = strMessage.Replace("</center>", "[/CENTER]");

            strMessage = strMessage.Replace("<strong>", "[B]");
            strMessage = strMessage.Replace("</strong>", "[/B]");
            strMessage = strMessage.Replace("<em>", "[I]");
            strMessage = strMessage.Replace("</em>", "[/I]");

            strMessage = strMessage.Replace("<br />", "");
            strMessage = strMessage.Replace("<br />", "");

            strMessage = strMessage.Replace("<P>", "[P]");
            strMessage = strMessage.Replace("</P>", "[/P]");
            strMessage = strMessage.Replace("<P align=center>", "[P ALIGN=CENTER]");
            strMessage = strMessage.Replace("<P align=left>", "[P ALIGN=LEFT]");
            strMessage = strMessage.Replace("<P align=right>", "[P ALIGN=RIGHT]");

            strMessage = strMessage.Replace("<div>", "[DIV]");
            strMessage = strMessage.Replace("</div>", "[/DIV]");
            strMessage = strMessage.Replace(@"<div align=""center"">", "[DIV ALIGN=CENTER]");
            strMessage = strMessage.Replace(@"<div align=""left"">", "[DIV ALIGN=LEFT]");
            strMessage = strMessage.Replace(@"<div align=""right"">", "[DIV ALIGN=RIGHT]");
            strMessage = strMessage.Replace("<div align=center>", "[DIV ALIGN=CENTER]");
            strMessage = strMessage.Replace("<div align=left>", "[DIV ALIGN=LEFT]");
            strMessage = strMessage.Replace("<div align=right>", "[DIV ALIGN=RIGHT]");

            strMessage = strMessage.Replace("<blockquote>", "[BLOCKQUOTE]");
            strMessage = strMessage.Replace("</blockquote>", "[/BLOCKQUOTE]");

            strMessage = strMessage.Replace(@"<font size=""1"">", "[SIZE=1]");
            strMessage = strMessage.Replace(@"<font size=""2"">", "[SIZE=2]");
            strMessage = strMessage.Replace(@"<font size=""3"">", "[SIZE=3]");
            strMessage = strMessage.Replace(@"<font size=""4"">", "[SIZE=4]");
            strMessage = strMessage.Replace(@"<font size=""5"">", "[SIZE=5]");
            strMessage = strMessage.Replace(@"<font size=""6"">", "[SIZE=6]");
            strMessage = strMessage.Replace("<font size=6>", "[SIZE=6]");
            strMessage = strMessage.Replace("<font size=1>", "[SIZE=1]");
            strMessage = strMessage.Replace("<font size=2>", "[SIZE=2]");
            strMessage = strMessage.Replace("<font size=3>", "[SIZE=3]");
            strMessage = strMessage.Replace("<font size=4>", "[SIZE=4]");
            strMessage = strMessage.Replace("<font size=5>", "[SIZE=5]");
            strMessage = strMessage.Replace("<font size=6>", "[SIZE=6]");
            strMessage = strMessage.Replace(@"<font face=""Arial, Helvetica, sans-serif"">", "[FONT=Arial]");
            strMessage = strMessage.Replace(@"<font face=""Courier New, Courier, mono"">", "[FONT=Courier]");
            strMessage = strMessage.Replace(@"<font face=""Times New Roman, Times, serif"">", "[FONT=Times]");
            strMessage = strMessage.Replace(@"<font face=""Verdana, Arial, Helvetica, sans-serif"">", "[FONT=Verdana]");



            while (TextUtil.IndexOf(strMessage, "<img ") > 0)
            {

                lngStartPos = TextUtil.IndexOf(strMessage, "<img ");

                lngEndPos = TextUtil.IndexOf(strMessage, (int)lngStartPos, @""">") + 3;

                if (lngEndPos - lngStartPos <= 10)
                {
                    lngEndPos = lngStartPos + 10;
                }

                strMessageLink = TextUtil.Substring(strMessage, NumberUtils.NumberInt(lngStartPos), NumberUtils.NumberInt((lngEndPos - lngStartPos))).Trim();

                strTempMessage = strMessageLink;

                strTempMessage = strTempMessage.Replace(@"src=""", "");
                strTempMessage = strTempMessage.Replace("<img ", "[IMG]");
                strTempMessage = strTempMessage.Replace(@""">", "[/IMG]");

                strMessage = strMessage.Replace(strMessageLink, strTempMessage);
            }




            while (TextUtil.IndexOf(strMessage, @"<a href=""mailto:") > 0 & TextUtil.IndexOf(strMessage, "</a>") > 0)
            {

                lngStartPos = TextUtil.IndexOf(strMessage, @"<a href=""mailto:");


                lngEndPos = TextUtil.IndexOf(strMessage, (int)lngStartPos, "</a>") + 4;

                if (lngEndPos - lngStartPos <= 16)
                {
                    lngEndPos = lngStartPos + 16;
                }

                strMessageLink = TextUtil.Substring(strMessage, NumberUtils.NumberInt(lngStartPos), NumberUtils.NumberInt((lngEndPos - lngStartPos))).Trim();

                strTempMessage = strMessageLink;

                strTempMessage = strTempMessage.Replace(@"<a href=""mailto:", "[EMAIL=");
                strTempMessage = strTempMessage.Replace("</a>", "[/EMAIL]");
                strTempMessage = strTempMessage.Replace(@""">", "]");

                strMessage = strMessage.Replace(strMessageLink, strTempMessage);
            }




            while (TextUtil.IndexOf(strMessage, @"<a href=""") > 0 & TextUtil.IndexOf(strMessage, "</a>") > 0)
            {

                lngStartPos = TextUtil.IndexOf(strMessage, @"<a href=""");

                lngEndPos = TextUtil.IndexOf(strMessage, (int)lngStartPos, "</a>") + 4;

                if (lngEndPos - lngStartPos <= 9)
                {
                    lngEndPos = lngStartPos + 9;
                }

                strMessageLink = TextUtil.Substring(strMessage, NumberUtils.NumberInt(lngStartPos), NumberUtils.NumberInt((lngEndPos - lngStartPos))).Trim();

                strTempMessage = strMessageLink;

                strTempMessage = strTempMessage.Replace(@"<a href=""", "[URL=");
                strTempMessage = strTempMessage.Replace("</a>", "[/URL]");
                strTempMessage = strTempMessage.Replace(@""">", "]");

                strMessage = strMessage.Replace(strMessageLink, strTempMessage);
            }



            while (TextUtil.IndexOf(strMessage, "<font color=") > 0 & TextUtil.IndexOf(strMessage, "</font>") > 0)
            {

                lngStartPos = TextUtil.IndexOf(strMessage, "<font color=");


                lngEndPos = TextUtil.IndexOf(strMessage, (int)lngStartPos, "</font>") + 8;

                if (lngEndPos - lngStartPos <= 12)
                {
                    lngEndPos = lngStartPos + 12;
                }

                strMessageLink = TextUtil.Substring(strMessage, NumberUtils.NumberInt(lngStartPos), NumberUtils.NumberInt((lngEndPos - lngStartPos))).Trim();

                strTempMessage = strMessageLink;

                strTempMessage = strTempMessage.Replace("<font color=", "[COLOR=");
                strTempMessage = strTempMessage.Replace("</font>", "[/COLOR]");
                strTempMessage = strTempMessage.Replace(">", "]");

                strMessage = strMessage.Replace(strMessageLink, strTempMessage);

            }

            strMessage = strMessage.Replace("</font>", "[/FONT]");


            strMessage = strMessage.Replace("<", "&lt;");
            strMessage = strMessage.Replace(">", "&gt;");
            strMessage = strMessage.Replace("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;", "       ");
            strMessage = strMessage.Replace("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;", "      ");
            strMessage = strMessage.Replace("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;", "     ");
            strMessage = strMessage.Replace("&nbsp;&nbsp;&nbsp;&nbsp;", "    ");
            strMessage = strMessage.Replace("&nbsp;&nbsp;&nbsp;", "   ");
            strMessage = strMessage.Replace("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;", Convert.ToChar(9).ToString());
            strMessage = strMessage.Replace((char)10, (char)' ');



            return strMessage;

        }





        public static string removeEditorUsuarios(string strMessage)
        {

            long lngStartPos = 0;
            long lngEndPos = 0;

            lngStartPos = TextUtil.IndexOf(strMessage, "<edited>");
            lngEndPos = TextUtil.IndexOf(strMessage, "</edited>") + 9;
            if (lngEndPos - lngStartPos <= 8)
            {
                lngEndPos = lngStartPos + 9;
            }

            return strMessage.Replace(TextUtil.Trim(TextUtil.Substring(strMessage, NumberUtils.NumberInt(lngStartPos), NumberUtils.NumberInt(lngEndPos - lngStartPos))), "");
        }



        public static void forumPermisisons(int intForumID, int intGroupID, int intRead, int intPost, int intReply, int intEdit, int intDelete, int intPriority, int intPollCreate, int intVote, int intAttachments, int intImageUpload)
        {

            string strSQL = null;

            FSForum.Variables.Forum.blnRead = false;
            FSForum.Variables.Forum.blnPost = false;
            FSForum.Variables.Forum.blnReply = false;
            FSForum.Variables.Forum.blnEdit = false;
            FSForum.Variables.Forum.blnDelete = false;
            FSForum.Variables.Forum.blnPriority = false;
            FSForum.Variables.Forum.blnPollCreate = false;
            FSForum.Variables.Forum.blnVote = false;
            FSForum.Variables.Forum.blnAttachments = false;
            FSForum.Variables.Forum.blnImageUpload = false;
            FSForum.Variables.Forum.blnModerator = false;

        
            FSDatabase.BdUtils db = new FSDatabase.BdUtils("FSForum");


            if (FSDatabase.Utils.BDType == FSDatabase.Utils.TypeBd.SQLServer)
            {
                strSQL = "Execute " + FSForum.Variables.Forum.strDbProc + "ForumPermissions @intForumID = " + intForumID + ", @intGroupID = " + intGroupID + ", @intUsuariosID = " + FSPortal.Variables.User.UsuarioId;
            }
            else
            {
                strSQL = "SELECT " + FSForum.Variables.Forum.strDbTable + "Permissions.* ";
                strSQL = strSQL + "FROM " + FSForum.Variables.Forum.strDbTable + "Permissions ";
                strSQL = strSQL + "WHERE (" + FSForum.Variables.Forum.strDbTable + "Permissions.Group_ID = " + intGroupID + " OR " + FSForum.Variables.Forum.strDbTable + "Permissions.UsuarioID = " + FSPortal.Variables.User.UsuarioId + ") AND " + FSForum.Variables.Forum.strDbTable + "Permissions.Forum_ID = " + intForumID + " ";
                strSQL = strSQL + "ORDER BY " + FSForum.Variables.Forum.strDbTable + "Permissions.UsuarioID DESC;";
            }
         
            DataTable dtPermissions = db.Execute(strSQL);

            if (dtPermissions.Rows.Count > 0)
            {

                FSForum.Variables.Forum.blnRead = System.Convert.ToBoolean(dtPermissions.Rows[0]["Read"]);
                FSForum.Variables.Forum.blnPost = System.Convert.ToBoolean(dtPermissions.Rows[0]["Post"]);
                FSForum.Variables.Forum.blnReply = System.Convert.ToBoolean(dtPermissions.Rows[0]["Reply_posts"]);
                FSForum.Variables.Forum.blnEdit = System.Convert.ToBoolean(dtPermissions.Rows[0]["Edit_posts"]);
                FSForum.Variables.Forum.blnDelete = System.Convert.ToBoolean(dtPermissions.Rows[0]["Delete_posts"]);
                FSForum.Variables.Forum.blnPriority = System.Convert.ToBoolean(dtPermissions.Rows[0]["Priority_posts"]);
                FSForum.Variables.Forum.blnPollCreate = System.Convert.ToBoolean(dtPermissions.Rows[0]["Poll_create"]);
                FSForum.Variables.Forum.blnVote = System.Convert.ToBoolean(dtPermissions.Rows[0]["Vote"]);
                FSForum.Variables.Forum.blnAttachments = System.Convert.ToBoolean(dtPermissions.Rows[0]["Attachments"]);
                FSForum.Variables.Forum.blnImageUpload = System.Convert.ToBoolean(dtPermissions.Rows[0]["Image_upload"]);
                FSForum.Variables.Forum.blnModerator = System.Convert.ToBoolean(dtPermissions.Rows[0]["Moderate"]);

            }
            else
            {

                if (intRead == 1 | (intRead == 2 & FSPortal.Variables.User.GroupId != 2) | (FSPortal.Variables.User.Administrador))
                {
                    FSForum.Variables.Forum.blnRead = true;
                }
                if (intPost == 1 | (intPost == 2 & FSPortal.Variables.User.GroupId != 2) | (FSPortal.Variables.User.Administrador))
                {
                    FSForum.Variables.Forum.blnPost = true;
                }
                if (intReply == 1 | (intReply == 2 & FSPortal.Variables.User.GroupId != 2) | (FSPortal.Variables.User.Administrador))
                {
                    FSForum.Variables.Forum.blnReply = true;
                }
                if (intEdit == 1 | (intEdit == 2 & FSPortal.Variables.User.GroupId != 2) | (FSPortal.Variables.User.Administrador))
                {
                    FSForum.Variables.Forum.blnEdit = true;
                }
                if (intDelete == 1 | (intDelete == 2 & FSPortal.Variables.User.GroupId != 2) | (FSPortal.Variables.User.Administrador))
                {
                    FSForum.Variables.Forum.blnDelete = true;
                }
                if (intPriority == 1 | (intPriority == 2 & FSPortal.Variables.User.GroupId != 2) | (FSPortal.Variables.User.Administrador))
                {
                    FSForum.Variables.Forum.blnPriority = true;
                }
                if ((intPollCreate == 1 | (intPollCreate == 2 & FSPortal.Variables.User.GroupId != 2) | (FSPortal.Variables.User.Administrador)) & intPollCreate != 0)
                {
                    FSForum.Variables.Forum.blnPollCreate = true;
                }
                if ((intVote == 1 | (intVote == 2 & FSPortal.Variables.User.GroupId != 2) | (FSPortal.Variables.User.Administrador)) & intVote != 0)
                {
                    FSForum.Variables.Forum.blnVote = true;
                }
                if ((intAttachments == 1 | (intAttachments == 2 & FSPortal.Variables.User.GroupId != 2) | (FSPortal.Variables.User.Administrador)) & intAttachments != 0)
                {
                    FSForum.Variables.Forum.blnAttachments = true;
                }
                if ((intImageUpload == 1 | (intImageUpload == 2 & FSPortal.Variables.User.GroupId != 2) | (FSPortal.Variables.User.Administrador)) & intImageUpload != 0)
                {
                    FSForum.Variables.Forum.blnImageUpload = true;
                }
            }
        }


        public static string BuildSQL(string strTable, string[] sarySearchWord, string strSearchMode)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder("");
            int intSQLLoopCounter = 0;

            intSQLLoopCounter = 0;

            sb.AppendLine("(" + strTable + " LIKE '%" + sarySearchWord[0] + "%'");

            for (intSQLLoopCounter = 1; intSQLLoopCounter <= sarySearchWord.GetUpperBound(0); intSQLLoopCounter++)
            {

                if (strSearchMode == "1")
                {
                    sb.AppendLine(" AND ");

                }
                else
                {
                    sb.AppendLine(" OR ");
                }

                sb.AppendLine(strTable + " LIKE '%" + sarySearchWord[intSQLLoopCounter] + "%'");
            }

            return sb.ToString() + ")";
        }


        public static string RTEenabled()
        {
            string rTEenabledReturn = null;

            string strUserAgent = null;

            strUserAgent = FSPortal.Variables.User.HTTP_USER_AGENT;

            if (TextUtil.IndexOf(strUserAgent, "MSIE") > 0 & TextUtil.IndexOf(strUserAgent, "Win") > 0 & TextUtil.IndexOf(strUserAgent, "Opera") == 0)
            {
                if (TextUtil.Substring(strUserAgent, TextUtil.IndexOf(strUserAgent, "MSIE") + 5, 1).Trim() == "5")
                {
                    rTEenabledReturn = "winIE5";
                }
                else if (Convert.ToInt16(TextUtil.Substring(strUserAgent, TextUtil.IndexOf(strUserAgent, "MSIE") + 5, 1).Trim()) >= 6)
                {
                    rTEenabledReturn = "winIE";
                }
                else
                {
                    rTEenabledReturn = "false";
                }


            }
            else if (TextUtil.IndexOf(strUserAgent, "Firebird") > 0)
            {
                if ((Convert.ToInt64(TextUtil.Substring(strUserAgent, TextUtil.IndexOf(strUserAgent, "Gecko/") + 6, 8).Trim())) >= 20030728)
                {
                    rTEenabledReturn = "Gecko";
                }
                else
                {
                    rTEenabledReturn = "false";
                }


            }
            else if (TextUtil.IndexOf(strUserAgent, "Gecko") > 0 & TextUtil.IndexOf(strUserAgent, "Firebird") == 0 & NumberUtils.IsNumeric(TextUtil.Substring(strUserAgent, TextUtil.IndexOf(strUserAgent, "Gecko/") + 6, 8).Trim()))
            {

                if ((Convert.ToInt64(TextUtil.Substring(strUserAgent, TextUtil.IndexOf(strUserAgent, "Gecko/") + 6, 8).Trim())) >= 20030312)
                {
                    rTEenabledReturn = "Gecko";
                }
                else
                {
                    rTEenabledReturn = "false";
                }


            }
            else
            {
                rTEenabledReturn = "false";
            }
            return rTEenabledReturn;
        }
    }
}
