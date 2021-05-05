// <fileheader>
// <copyright file="RTE_message_form_inc.ascx.cs" company="Febrer Software">
//     Fecha: 30/11/2007
//     Path: forum\includes\RTE_message_form_inc.ascx.cs
//     Copyright (c) 2003-2007 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>
using FSPortal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;

namespace FSForum
{
    namespace Includes
    {
        public class RTE_message_form_inc
        {

            public static string strBuddyName;
            public static int intTopicPriority;
            public static string strTopicSubject;
            public static string RTEenabled;
            public static long lngMessageID;
            public static int intTotalNumOfThreads;

            public static string strMode;


            public static string Render(string postPage)
            {
                StringBuilder sb = new StringBuilder();
                
                bool blnAbout;
                // Initiliase variables
                blnAbout = Variables.Forum.blnLCode;
                // The following enables and disables functions of the Rich Text Editor
                // To disable or enable functions change the following to true of false
                bool blnBold = true;
                bool blnUnderline = true;
                bool blnItalic = true;
                bool blnFontStyle = false;
                bool blnFontType = true;
                bool blnFontSize = true;
                bool blnTextColour = true;
                bool blnTextBackgroundColour = false;
                bool blnCut = true;
                bool blnCopy = true;
                bool blnPaste = true;
                bool blnUndo = true;
                bool blnRedo = true;
                bool blnLeftJustify = true;
                bool blnCentre = true;
                bool blnRightJustify = true;
                bool blnFullJustify = false;
                bool blnOrderList = true;
                bool blnUnOrderList = true;
                bool blnOutdent = true;
                bool blnIndent = true;
                bool blnAddHyperlink = true;
                bool blnAddImage = true;
                bool blnInsertTable = false;
                bool blnHTMLView = false;
                bool blnSpellCheck = true;
                bool blnEmoticonPopUp = false;
                FSDatabase.BdUtils db = new FSDatabase.BdUtils("FSForum");
                DataTable rsCommon;
                // If a private message go to pm post message page otherwise goto post message page
                string strPostPage;
                if ((strMode == "PM"))
                {
                    strPostPage = "pm_post_message.aspx";
                }
                else
                {
                    strPostPage = ("post_message.aspx?PN=" + FSLibrary.TextUtil.Substring(postPage, 0, 8).Trim());
                }

                sb.AppendLine(("\r\n" + ("\r\n" + ("<form method=\"post\" name=\"frmAddMessage\" action=\""
                                + (strPostPage + ("\" onSubmit=\"return CheckForm();\" onReset=\"return ResetForm();\">" + ("\r\n" + (" <table width=\"610\" border=\"0\" cellspacing=\"0\" cellpadding=\"1\" bgcolor=\""
                                + (Variables.Forum.strTableBorderColour + ("\" height=\"230\" align=\"center\">" + ("\r\n" + ("  <tr>" + ("\r\n" + ("   <td>" + ("\r\n" + ("    <table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" align=\"center\" bgcolor=\""
                                + (Variables.Forum.strTableColour + ("\" background=\""
                                + (Variables.Forum.strTableBgImage + ("\" height=\"201\">" + ("\r\n" + ("     <tr>" + ("\r\n" + ("      <td>" + ("\r\n" + ("       <table width=\"100%\" border=\"0\" align=\"center\" height=\"233\" cellpadding=\"2\" cellspacing=\"0\">" + ("\r\n" + ("        <tr>" + ("\r\n" + ("         <td colspan=\"2\" height=\"30\" class=\"text\" align=\"left\">*"
                                + (Variables.Forum.strTxtRequiredFields + ("</td>" + ("\r\n" + "        </tr>")))))))))))))))))))))))))))))))));
                // If the poster is in a guest then get them to enter a name
                if (((FSPortal.Variables.User.UsuarioId == 2)
                            && ((strMode != "edit")
                            && (strMode != "editTopic"))))
                {
                    sb.AppendLine(("\r\n" + ("        <tr>" + ("\r\n" + ("\t <td align=\"right\" width=\"15%\" class=\"text\">"
                                    + (Variables.Forum.strTxtName + ("*:</td>" + ("\r\n" + ("\t <td width=\"70%\">" + ("\r\n" + ("\t  <input type=\"text\" name=\"Gname\" size=\"20\" maxlength=\"20\" />" + ("\r\n" + ("\t </td>" + ("\r\n" + "\t</tr>"))))))))))))));
                }

                // If this is a private message display the usuario box
                if ((strMode == "PM"))
                {
                    sb.AppendLine(("\r\n" + ("        <tr>" + ("\r\n" + ("         <td align=\"right\" width=\"15%\" class=\"text\">"
                                    + (Variables.Forum.strTxtTousuario + ("*:</td>" + ("\r\n" + "         <td width=\"70%\" class=\"text\">"))))))));
                    // Get the users buddy list if they have one
                    // Initlise the sql statement
                    string strSQL;
                    strSQL = ("SELECT " + "Usuarios.usuario ");
                    strSQL = (strSQL + ("FROM " + ("Usuarios INNER JOIN "
                                + (Variables.Forum.strDbTable + ("BuddyList ON " + ("Usuarios.UsuarioID = "
                                + (Variables.Forum.strDbTable + "BuddyList.Buddy_ID ")))))));
                    strSQL = (strSQL + ("WHERE "
                                + (Variables.Forum.strDbTable + ("BuddyList.UsuarioID="
                                + (FSPortal.Variables.User.UsuarioId + (" AND "
                                + (Variables.Forum.strDbTable + "BuddyList.Buddy_ID <> 2 ")))))));
                    strSQL = (strSQL + ("ORDER By " + "Usuarios.usuario ASC;"));
                    rsCommon = db.Execute(strSQL);
                    sb.AppendLine(("\r\n" + ("          <input type=\"text\" name=\"member\" size=\"15\" maxlength=\"15\" value=\""
                                    + (System.Web.HttpUtility.HtmlEncode(strBuddyName) + "\""))));
                    if ((rsCommon.Rows.Count == 0))
                    {
                        sb.AppendLine(" onChange=\"document.frmAddMessage.selectMember.options[0].selected = true;\"");
                    }

                    sb.AppendLine(" />");
                    sb.AppendLine(("\r\n" + ("          <a href=\"JavaScript:openWin(\'pop_up_member_search.aspx\',\'profile\',\'toolbar=0,location=0,sta" +
                        "tus=0,menubar=0,scrollbars=0,resizable=1,width=440,height=255\')\"><img src=\""
                                    + (Variables.Forum.strImagePath + ("search.gif\" alt=\""
                                    + (Variables.Forum.strTxtMemberSearch + "\" border=\"0\" align=\"middle\" /></a>"))))));
                    // If there are records returned then display the users buddy list
                    if ((rsCommon.Rows.Count == 0))
                    {
                        sb.AppendLine(("\r\n" + ("          <span class=\"text\">"
                                        + (Variables.Forum.strSelectFormBuddyList + (":</span>" + ("\r\n" + ("          <select name=\"selectMember\" onChange=\"member.value=\'\'\">" + ("\r\n" + ("            <option value=\"\">-- "
                                        + (Variables.Forum.strTxtNoneSelected + " --</option>"))))))))));
                        // Loop throuhgn and display the buddy list
                        foreach (DataRow row in rsCommon.Rows)
                        {
                            sb.AppendLine(("<option value=\""
                                            + (row["usuario"].ToString() + ("\">"
                                            + (row["usuario"].ToString() + "</option>")))));
                        }

                        sb.AppendLine(("\r\n" + "          </select>"));
                    }
                    else
                    {
                        sb.AppendLine(("\r\n" + "\t<input type=\"hidden\" name=\"selectMember\" value=\"\" />"));
                    }

                    sb.AppendLine(("\r\n" + ("       </td>" + ("\r\n" + "        </tr>"))));
                }

                // If this is a new post or editing the first thread then display the subject text box
                if (((strMode == "new")
                            || ((strMode == "editTopic")
                            || ((strMode == "PM")
                            || (strMode == "poll")))))
                {
                    sb.AppendLine(("        <tr>" + ("\r\n" + ("         <td align=\"right\" class=\"text\">"
                                    + (Variables.Forum.strTxtSubjectFolder + ("*:</td>" + ("\r\n" + ("         <td width=\"70%\">" + ("\r\n" + "          <input type=\"text\" name=\"subject\" size=\"30\" maxlength=\"41\"")))))))));
                    if (((strMode == "editTopic")
                                || (strMode == "PM")))
                    {
                        sb.AppendLine((" value=\""
                                        + (strTopicSubject + "\"")));
                    }

                    sb.AppendLine(" />");
                    // If this is the forums moderator or forum admim then let them slect the priority level of the post
                    if ((((FSPortal.Variables.User.Administrador == true)
                                || (Variables.Forum.blnPriority == true))
                                && ((strMode == "new")
                                || ((strMode == "editTopic")
                                || (strMode == "poll")))))
                    {
                        sb.AppendLine(("          <span class=\"text\"> "
                                        + (Variables.Forum.strTxtPriority + (":" + ("\r\n" + ("          <select name=\"priority\">" + ("\r\n" + "           <option value=\"0\"")))))));
                        if ((intTopicPriority == 0))
                        {
                            sb.AppendLine(" selected");
                        }

                        sb.AppendLine((">"
                                        + (Variables.Forum.strTxtNormal + ("</option>" + ("\r\n" + "           <option value=\"1\"")))));
                        if ((intTopicPriority == 1))
                        {
                            sb.AppendLine(" selected");
                        }

                        sb.AppendLine((">"
                                        + (Variables.Forum.strTxtPinnedTopic + "</option>")));
                        // If this is the forum admin or moderator let them post an annoucment to this forum
                        if (((FSPortal.Variables.User.Administrador == true)
                                    || Variables.Forum.blnModerator))
                        {
                            sb.AppendLine(("\r\n" + "           <option value=\"2\""));
                            if ((intTopicPriority == 2))
                            {
                                sb.AppendLine(" selected");
                            }

                            sb.AppendLine(("\">"
                                            + (Variables.Forum.strTopThisForum + "</option>")));
                        }

                        // If this is the forum admin let them post a priority post to all forums
                        if ((FSPortal.Variables.User.Administrador == true))
                        {
                            sb.AppendLine(("\r\n" + "           <option value=\"3\""));
                            if ((intTopicPriority == 3))
                            {
                                sb.AppendLine(" selected");
                            }

                            sb.AppendLine(("\">"
                                            + (Variables.Forum.strTxtTopAllForums + "</option>")));
                        }

                        sb.AppendLine(("          </select>" + ("\r\n" + "          </span>")));
                    }

                    sb.AppendLine(("         </td>" + ("\r\n" + "        </tr>")));
                }

                // If this is a new poll then display space to enter the poll
                if ((strMode == "poll"))
                {
                    sb.AppendLine(@"<poll_form:poll_form ID=""poll_form"" runat=""server"" />");
                }
                sb.AppendLine(RTE_toolbar_1.Render());
                sb.AppendLine(("\t<tr>" + ("\r\n" + ("         <td valign=\"bottom\" align=\"right\" width=\"15%\"> </td>" + ("\r\n" + ("         <td width=\"70%\" valign=\"bottom\">" + ("\r\n" + ("          <table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">" + ("\r\n" + ("           <tr>" + ("\r\n" + ("            <td>" + ("\r\n" + ("             <table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"2\">" + ("\r\n" + ("              <tr>" + ("\r\n" + "               <td>")))))))))))))))));
                // RTE Tool Bar 1
                // ---------------------------------------------------------------------------
                RTE_toolbar_1.blnBold = blnBold;
                RTE_toolbar_1.blnUnderline = blnUnderline;
                RTE_toolbar_1.blnItalic = blnItalic;
                RTE_toolbar_1.blnFontStyle = blnFontStyle;
                RTE_toolbar_1.blnFontType = blnFontType;
                RTE_toolbar_1.blnFontSize = blnFontSize;
                RTE_toolbar_1.blnTextColour = blnTextColour;
                RTE_toolbar_1.blnTextBackgroundColour = blnTextBackgroundColour;
                RTE_toolbar_1.blnCut = blnCut;
                RTE_toolbar_1.blnCopy = blnCopy;
                RTE_toolbar_1.blnPaste = blnPaste;
                RTE_toolbar_1.blnUndo = blnUndo;
                RTE_toolbar_1.blnRedo = blnRedo;
                RTE_toolbar_1.blnLeftJustify = blnLeftJustify;
                RTE_toolbar_1.blnCentre = blnCentre;
                RTE_toolbar_1.blnRightJustify = blnRightJustify;
                RTE_toolbar_1.blnFullJustify = blnFullJustify;
                RTE_toolbar_1.blnOrderList = blnOrderList;
                RTE_toolbar_1.blnUnOrderList = blnUnOrderList;
                RTE_toolbar_1.blnOutdent = blnOutdent;
                RTE_toolbar_1.blnIndent = blnIndent;
                RTE_toolbar_1.blnAddHyperlink = blnAddHyperlink;
                RTE_toolbar_1.blnAddImage = blnAddImage;
                RTE_toolbar_1.blnInsertTable = blnInsertTable;
                RTE_toolbar_1.blnHTMLView = blnHTMLView;
                RTE_toolbar_1.blnSpellCheck = blnSpellCheck;
                RTE_toolbar_1.blnEmoticonPopUp = blnEmoticonPopUp;
                RTE_toolbar_1.RTEenabled = RTEenabled;
                sb.AppendLine(RTE_toolbar_1.Render());
                // ---------------------------------------------------------------------------
                sb.AppendLine(("               </td>" + ("\r\n" + ("              </tr>" + ("\r\n" + ("              <tr>" + ("\r\n" + "               <td>")))))));
                // RTE Tool Bar 2
                // ---------------------------------------------------------------------------
                RTE_toolbar_2.blnBold = blnBold;
                RTE_toolbar_2.blnUnderline = blnUnderline;
                RTE_toolbar_2.blnItalic = blnItalic;
                RTE_toolbar_2.blnFontStyle = blnFontStyle;
                RTE_toolbar_2.blnFontType = blnFontType;
                RTE_toolbar_2.blnFontSize = blnFontSize;
                RTE_toolbar_2.blnTextColour = blnTextColour;
                RTE_toolbar_2.blnTextBackgroundColour = blnTextBackgroundColour;
                RTE_toolbar_2.blnCut = blnCut;
                RTE_toolbar_2.blnCopy = blnCopy;
                RTE_toolbar_2.blnPaste = blnPaste;
                RTE_toolbar_2.blnUndo = blnUndo;
                RTE_toolbar_2.blnRedo = blnRedo;
                RTE_toolbar_2.blnLeftJustify = blnLeftJustify;
                RTE_toolbar_2.blnCentre = blnCentre;
                RTE_toolbar_2.blnRightJustify = blnRightJustify;
                RTE_toolbar_2.blnFullJustify = blnFullJustify;
                RTE_toolbar_2.blnOrderList = blnOrderList;
                RTE_toolbar_2.blnUnOrderList = blnUnOrderList;
                RTE_toolbar_2.blnOutdent = blnOutdent;
                RTE_toolbar_2.blnIndent = blnIndent;
                RTE_toolbar_2.blnAddHyperlink = blnAddHyperlink;
                RTE_toolbar_2.blnAddImage = blnAddImage;
                RTE_toolbar_2.blnInsertTable = blnInsertTable;
                RTE_toolbar_2.blnHTMLView = blnHTMLView;
                RTE_toolbar_2.blnSpellCheck = blnSpellCheck;
                RTE_toolbar_2.blnEmoticonPopUp = blnEmoticonPopUp;
                RTE_toolbar_2.RTEenabled = RTEenabled;
                sb.AppendLine(RTE_toolbar_2.Render());
                // ---------------------------------------------------------------------------
                sb.AppendLine(("\r\n" + ("\t\t  <iframe width=\"260\" height=\"165\" id=\"colourPalette\" src=\"RTE_colour_palette.ascx\" style=\"visibili" +
                    "ty:hidden; position: absolute; left: 0px; top: 0px;\" frameborder=\"0\" scrolling=\"no\"></iframe>" + ("\r\n" + ("\t       </td>" + ("\r\n" + ("\t      </tr>" + ("\r\n" + ("             </table>" + ("\r\n" + ("            </td>" + ("\r\n" + ("           </tr>" + ("\r\n" + ("          </table>" + ("\r\n" + ("         </td>" + ("\r\n" + ("        </tr>" + ("\r\n" + ("        <tr>" + ("\r\n" + ("         <td valign=\"top\" align=\"right\" height=\"61\" width=\"15%\" class=\"text\">"
                                + (Variables.Forum.strTxtMessage + "*:"))))))))))))))))))))))));
                // *************** Emoticons *******************
                // If emoticons are enabled show them next to the post window
                if (Variables.Forum.blnEmoticons)
                {
                    sb.AppendLine(("\r\n" + ("         <table border=\"0\" cellspacing=\"0\" cellpadding=\"4\" align=\"center\">" + ("\r\n" + ("          <tr><td class=\"smText\" colspan=\"3\" align=\"center\"><br />"
                                    + (Variables.Forum.strTxtEmoticons + "</td></tr>"))))));
                    // Intilise the index position (we are starting at 1 instead of position 0 in the array for simpler calculations)
                    int intIndexPosition = 1;
                    // Calcultae the number of outer loops to do
                    int intNumberOfOuterLoops = 4;
                    // If there is a remainder add 1 to the number of loops
                    if ((Variables.Forum.saryEmoticons.Length % 2) > 0)
                    {
                        intNumberOfOuterLoops = (intNumberOfOuterLoops + 1);
                    }

                    // Loop throgh th list of emoticons
                    int intLoop;
                    for (intLoop = 1; (intLoop <= intNumberOfOuterLoops); intLoop++)
                    {
                        sb.AppendLine("<tr>");
                        // Loop throgh th list of emoticons
                        int intInnerLoop;
                        for (intInnerLoop = 1; (intInnerLoop <= 3); intInnerLoop++)
                        {
                            // If there is nothing to display show an empty box
                            if ((intIndexPosition > Variables.Forum.saryEmoticons.Length))
                            {
                                sb.AppendLine(("\r\n" + "\t\t<td class=\"text\"> </td>"));
                                // Else show the emoticon
                            }
                            else
                            {
                                sb.AppendLine(("\r\n" + ("\t\t<td><img src=\""
                                                + (Variables.Forum.saryEmoticons[intIndexPosition, 3] + ("\" border=\"0\" alt=\""
                                                + (Variables.Forum.saryEmoticons[intIndexPosition, 1] + ("\" OnClick=\"AddSmileyIcon(\'"
                                                + (Variables.Forum.saryEmoticons[intIndexPosition, 3] + "\')\" style=\"cursor: pointer;\"></td>"))))))));
                            }

                            // Minus one form the index position
                            intIndexPosition = (intIndexPosition + 1);
                        }

                        sb.AppendLine("</tr>");
                    }

                    sb.AppendLine(("\r\n" + ("         <tr><td colspan=\"3\" align=\"center\"><a href=\"javascript:openWin(\'RTE_emoticons_smilies.aspx\'," +
                        "\'emot\',\'toolbar=0,location=0,status=0,menubar=0,scrollbars=1,resizable=1,width=400,height=400\')\" cla" +
                        "ss=\"smLink\">"
                                    + (Variables.Forum.strTxtMore + "</a></td></tr>"))));
                    sb.AppendLine(("\r\n" + "         </table></td>"));
                }

                // ******************************************
                sb.AppendLine("         <td height=\"61\" width=\"70%\" valign=\"top\">");
                // This bit creates a random number to add to the end of the Iframe link as IE will cache the page
                // Randomise the system timer
                //Randomize(DateTime.Now.Ticks);
                Random rnd = new Random();
                sb.AppendLine(("\r\n" + ("          <script language=\"javascript\">" + ("\r\n" + ("\t\t  //Create an iframe" + ("\r\n" + ("\t\t  document.write (\'<iframe id=\"message\" src=\"RTE_textbox.aspx?mode="
                                + (strMode + ("&pOID="
                                + (lngMessageID + ("&iD="
                                + (rnd.Next(0, 2000) + "\" width=\"490\" height=\"200\" onMouseOver=\"hideColourPallete()\"></iframe>\')"))))))))))));
                if (((RTEenabled == "winIE")
                            || (RTEenabled == "winIE5")))
                {
                    sb.AppendLine(("\r\n" + "\t\t  frames.message.document.designMode = \'On\';"));
                }

                sb.AppendLine(("\r\n" + ("          </script>" + ("\r\n" + ("          <!-- Display a message for RTE users with JavaScript turned off -->" + ("\r\n" + ("          <noscript><span class=\"bold\"><br /><br />"
                                + (Variables.Forum.strTxtJavaScriptEnabled + ("</span></noscript></td>" + ("\r\n" + ("        </tr>" + ("\r\n" + ("        <tr>" + ("\r\n" + ("\t <td align=\"right\" width=\"92\"> </td>" + ("\r\n" + ("\t <td width=\"508\" valign=\"bottom\" class=\"text\"> <input type=\"checkbox\" name=\"forumCodes\" value=\"" +
                                "True\" checked />"
                                + (Variables.Forum.strTxtEnable + (" <a href=\"JavaScript:openWin(\'forum_codes.aspx\',\'codes\',\'toolbar=0,location=0,status=0,menubar=0,scro" +
                                "llbars=1,resizable=1,width=550,height=400\')\">"
                                + (Variables.Forum.strTxtForumCodes + ("</a> "
                                + (Variables.Forum.strTxtToFormatPosts + ("\r\n" + "\t </td></tr>")))))))))))))))))))))));
                // If not PM then display another row
                if ((strMode != "PM"))
                {
                    // If signature of e-mail notify then display row to show
                    if ((((Variables.Forum.blnLoggedInUserEmail == true)
                                && (Variables.Forum.blnEmail == true))
                                || (Variables.Forum.blnLoggedInUserSignature == true)))
                    {
                        sb.AppendLine(("\r\n" + ("        <tr>" + ("\r\n" + ("         <td align=\"right\" height=\"7\" width=\"92\"> </td>" + ("\r\n" + "         <td height=\"7\" width=\"508\" valign=\"bottom\" class=\"text\">"))))));
                        // If the user has a signature offer them the chance to show it
                        if (Variables.Forum.blnLoggedInUserSignature)
                        {
                            sb.AppendLine(("\r\n" + "\t\t <input type=\"checkbox\" name=\"signature\" value=\"True\""));
                            if ((Variables.Forum.blnAttachSignature == true))
                            {
                                sb.AppendLine(" checked");
                            }

                            sb.AppendLine((" />"
                                            + (Variables.Forum.strTxtShowSignature + " ")));
                        }

                        // Display e-mail notify of replies option
                        if ((Variables.Forum.blnEmail && Variables.Forum.blnLoggedInUserEmail))
                        {
                            sb.AppendLine(("\r\n" + "\t\t <input type=\"checkbox\" name=\"email\" value=\"True\""));
                            if (Variables.Forum.blnReplyNotify)
                            {
                                sb.AppendLine(" checked");
                            }

                            sb.AppendLine((" />"
                                            + (Variables.Forum.strTxtEmailNotify + " ")));
                        }

                        sb.AppendLine(("\r\n" + ("         </td>" + ("\r\n" + "        </tr>"))));
                    }

                    // If this is a private e-mail and e-mail is on and the user gave an e-mail address let them choose to be notified when pm msg is read
                }
                else if (((strMode == "PM")
                            && (Variables.Forum.blnEmail && Variables.Forum.blnLoggedInUserEmail)))
                {
                    sb.AppendLine(("\r\n" + ("\t<tr>" + ("\r\n" + ("         <td align=\"right\" width=\"92\"> </td>" + ("\r\n" + ("         <td width=\"508\" valign=\"bottom\" class=\"text\"> <input type=\"checkbox\" name=\"email\" value" +
                        "=\"True\"><span class=\"text\">"
                                    + (Variables.Forum.strTxtEmailNotifyWhenPMIsRead + ("</span></td>" + ("\r\n" + "        </tr>"))))))))));
                }

                sb.AppendLine(("\r\n" + ("        <td>" + ("\r\n" + "         <input type=\"hidden\" name=\"message\" value=\"\">"))));
                if ((strMode != "PM"))
                {
                    sb.AppendLine(("\r\n" + ("\t<input type=\"hidden\" name=\"mode\" value=\""
                                    + (strMode + ("\" />" + ("\r\n" + ("\t<input type=\"hidden\" name=\"FID\" value=\""
                                    + (Variables.Forum.intForumID + ("\" />" + ("\r\n" + ("\t<input type=\"hidden\" name=\"TID\" value=\""
                                    + (Variables.Forum.lngTopicID + ("\" />" + ("\r\n" + ("\t<input type=\"hidden\" name=\"PID\" value=\""
                                    + (lngMessageID + ("\" />" + ("\r\n" + ("\t<input type=\"hidden\" name=\"TPN\" value=\""
                                    + (Variables.Forum.intRecordPositionPageNum + "\" />"))))))))))))))))))));
                    // If reply get the thread position number in the topic
                    if ((strMode == "reply"))
                    {
                        sb.AppendLine(("\r\n" + ("\t\t<input type=\"hidden\" name=\"ThreadPos\" value=\""
                                        + ((intTotalNumOfThreads + 1)
                                        + "\" />"))));
                    }

                }

                sb.AppendLine(("\r\n" + ("\t<input type=\"hidden\" name=\"browser\" value=\"RTE\" />" + ("\r\n" + ("\t<input type=\"hidden\" name=\"sessionID\" value=\""
                                + (FSPortal.Variables.User.sessionID + ("\" /> " + ("\r\n" + ("    </td>" + ("\r\n" + ("    <td height=\"2\" width=\"70%\" align=\"center\">" + ("\r\n" + "\t<p>"))))))))))));
                string strGetMessageBoxHTML;
                // Set how to get the HTML form the message box for Win IE5 and then for other RTE browsers
                if ((RTEenabled == "winIE5"))
                {
                    strGetMessageBoxHTML = "frames.message.document.body.innerHTML;";
                }
                else
                {
                    strGetMessageBoxHTML = "document.getElementById(\'message\').contentWindow.document.body.innerHTML;";
                }

                if (((strMode == "edit")
                            || (strMode == "editTopic")))
                {
                    sb.AppendLine(("\r\n" + ("          <input type=\"submit\" name=\"Submit\" value=\""
                                    + (Variables.Forum.strTxtUpdatePost + ("\" OnClick=\"document.frmAddMessage.message.value = "
                                    + (strGetMessageBoxHTML + "\" tabindex=\"30\" />"))))));
                }
                else if (((strMode == "new")
                            || (strMode == "poll")))
                {
                    sb.AppendLine(("\r\n" + ("          <input type=\"submit\" name=\"Submit\" value=\""
                                    + (Variables.Forum.strTxtNewTopic + ("\" OnClick=\"document.frmAddMessage.message.value = "
                                    + (strGetMessageBoxHTML + "\" tabindex=\"30\" />"))))));
                }
                else if ((strMode == "PM"))
                {
                    sb.AppendLine(("\r\n" + ("          <input type=\"submit\" name=\"Submit\" value=\""
                                    + (Variables.Forum.strTxtPostMessage + ("\" OnClick=\"document.frmAddMessage.message.value = "
                                    + (strGetMessageBoxHTML + "\" tabindex=\"30\" />"))))));
                }
                else
                {
                    sb.AppendLine(("\r\n" + ("          <input type=\"submit\" name=\"Submit\" value=\""
                                    + (Variables.Forum.strTxtPostReply + ("\" OnClick=\"document.frmAddMessage.message.value = "
                                    + (strGetMessageBoxHTML + "\" tabindex=\"30\" />"))))));
                }

                // sb.AppendLine(vbCrLf & "          <input type=""button"" name=""Preview"" value=""" & Variables.Forum.strTxtPreviewPost & """ onClick=""document.frmAddMessage.message.value = " & strGetMessageBoxHTML & " OpenPreviewWindow('post_preview.aspx', document.frmAddMessage);"" />" & _
                sb.AppendLine(("\r\n" + ("          <input type=\"reset\" name=\"Reset\" value=\""
                                + (Variables.Forum.strTxtClearForm + ("\" />" + ("\r\n" + ("         </p>" + ("\r\n" + ("        </td>" + ("\r\n" + ("        </tr>" + ("\r\n" + ("       </table>" + ("\r\n" + ("      </td>" + ("\r\n" + ("     </tr>" + ("\r\n" + ("    </table>" + ("\r\n" + ("   </td>" + ("\r\n" + ("  </tr>" + ("\r\n" + (" </table>" + ("\r\n" + "</form>"))))))))))))))))))))))))));
                return sb.ToString();
            }

        }
    }
}