// <fileheader>
// <copyright file="message_form_inc.ascx.cs" company="Febrer Software">
//     Fecha: 30/11/2007
//     Path: forum\includes\message_form_inc.ascx.cs
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
        public class message_form_inc
        {

            public static string strBuddyName;
            public static int intTopicPriority;
            public static string strTopicSubject;
            public static string RTEenabled;
            public static long lngMessageID;
            public static int intTotalNumOfThreads;

            public static string strMessage;
            public static string strMode;


            public static string Render(string mode, string postPage)
            {
                StringBuilder sb = new StringBuilder();
                // If a private message go to pm post message page otherwise goto    post message page
                string strPostPage;
                string strMode = FSLibrary.TextUtil.Substring(mode, 0, 2).Trim();
                if ((strMode == "PM"))
                {
                    strPostPage = "pm_post_message.aspx";
                }
                else
                {
                    strPostPage = ("post_message.aspx?PN=" + FSLibrary.TextUtil.Substring(postPage, 0, 8).Trim());
                }
                //sb.AppendLine(@"<message_form_js:message_form_js ID=""message_form_js"" runat=""server"" />");
                sb.AppendLine(message_form_js.Render());
                sb.AppendLine(@"<form method=""post"" name=""frmAddMessage"" action=""");
                sb.AppendLine(strPostPage);
                sb.AppendLine(@""" onReset=""return confirm('");
                sb.AppendLine(Variables.Forum.strResetFormConfirm);
                sb.AppendLine(@"');"">");
                sb.AppendLine(@"<table    width=""610"" border=""0"" cellspacing=""0"" cellpadding=""1"" bgcolor=""");
                sb.AppendLine(Variables.Forum.strTableBorderColour);
                sb.AppendLine(@""" height=""230"" align=""center"">");
                sb.AppendLine("<tr>");
                sb.AppendLine("<td>");
                sb.AppendLine(@"<table width=""100%""    border=""0"" cellspacing=""0"" cellpadding=""0"" align=""center"" bgcolor=""");
                sb.AppendLine(Variables.Forum.strTableColour);
                sb.AppendLine(@""" background=""");
                sb.AppendLine(Variables.Forum.strTableBgImage);
                sb.AppendLine(@"""    height=""201"">");
                sb.AppendLine("<tr>");
                sb.AppendLine("<td>");
                sb.AppendLine(@"<table width=""100%"" border=""0"" align=""center"" cellpadding=""2"" cellspacing=""0"">");
                sb.AppendLine(@"<tr align=""left"">");
                sb.AppendLine(@"<td colspan=""2"" height=""31"" class=""text"">*");
                sb.AppendLine(Variables.Forum.strTxtRequiredFields);
                sb.AppendLine("</td>");
                sb.AppendLine("</tr>");
                // If the poster is in a guest then get them to enter a name
                if (((FSPortal.Variables.User.UsuarioId == 2)
                            && ((strMode != "edit")
                            && (strMode != "editTopic"))))
                {

                }
                sb.AppendLine("<tr>");
                sb.AppendLine(@"<td align=""right"" width=""15%"" class=""text"">");
                sb.AppendLine(Variables.Forum.strTxtName);
                sb.AppendLine("*:</td>");
                sb.AppendLine(@"<td width=""70%""><input type='text' name=""Gname"" size=""20"" maxlength=""20"" /></td>");
                sb.AppendLine("</tr>");
                sb.AppendLine("<tr>");
                sb.AppendLine(@"<td align=""right"" width=""15%"" class=""text"">");
                sb.AppendLine(Variables.Forum.strTxtTousuario);
                sb.AppendLine(":</td>");
                sb.AppendLine(@"<td width=""70%"">");
                // Get the users    buddy list if they have    one
                // Initlise the sql statement
                string strSQL;
                strSQL = "SELECT " + Variables.Forum.strDbTable + "BuddyList.UsuarioID ";
                strSQL = strSQL + "FROM\t" + Variables.Forum.strDbTable + "BuddyList ";
                strSQL = strSQL + "WHERE "
                            + Variables.Forum.strDbTable + "BuddyList.UsuarioID="
                            + FSPortal.Variables.User.UsuarioId + " AND "
                            + Variables.Forum.strDbTable + "BuddyList.Buddy_ID <> 2 ";
                strSQL = strSQL + "ORDER By " + Variables.Forum.strDbTable + "BuddyList.UsuarioID ASC;";
                FSDatabase.BdUtils db = new FSDatabase.BdUtils("FSForum");
                DataTable rsCommon = db.Execute(strSQL);
                sb.AppendLine(@"<input type='text' name=""member"" size=""15"" maxlength=""15"" value=""");
                sb.AppendLine(System.Web.HttpUtility.HtmlEncode(strBuddyName));
                sb.AppendLine(@"""");
                if ((rsCommon.Rows.Count > 0))
                {
                    sb.AppendLine(" onChange=\"document.frmAddMessage.selectMember.options[0].selected = true;\"");
                }
                sb.AppendLine("/>");
                sb.AppendLine(@"<a href=""JavaScript:openWin('pop_up_member_search.aspx','profile','toolbar=0,location=0,status=0,menubar=0,scrollbars=0,resizable=1,width=440,height=255')""><img src=""");
                sb.AppendLine(Variables.Forum.strImagePath);
                sb.AppendLine(@"search.gif"" alt=""");
                sb.AppendLine(Variables.Forum.strTxtMemberSearch);
                sb.AppendLine(@"""    border=""0"" align=""middle""></a>");
                // If there are records returned then display the    users buddy list
                if ((rsCommon.Rows.Count > 0))
                {
                    sb.AppendLine(@"<span	class=""text"">");
                    sb.AppendLine(Variables.Forum.strSelectFormBuddyList);
                    sb.AppendLine(":</span>");
                    sb.AppendLine(@"<select name=""selectMember"" onChange=""member.value=''"" />");
                    sb.AppendLine(@"<option value="""">--");
                    sb.AppendLine(Variables.Forum.strTxtNoneSelected);
                    sb.AppendLine("--</option>");
                    // Loop throuhgn and display the buddy list
                    foreach (DataRow row in rsCommon.Rows)
                    {
                        sb.AppendLine(("<option\tvalue=\""
                                        + (row["usuario"].ToString() + ("\">"
                                        + (row["usuario"].ToString() + "</option>")))));
                    }
                    sb.AppendLine("</select>");
                    sb.AppendLine("<input type=\"hidden\" name=\"selectMember\" value=\"\" />");
                    sb.AppendLine("</td>");
                    sb.AppendLine("</tr>");
                }

                //If this is a new post or editing the first thread then    display    the subject text box
                if (strMode == "new" || strMode == "editTopic" || strMode == "PM" || strMode == "poll")
                {
                    sb.AppendLine("<tr>");
                    sb.AppendLine(@"<td align=""right"" width=""15%"" class=""text"">");
                    sb.AppendLine(Variables.Forum.strTxtSubjectFolder);
                    sb.AppendLine("*:</td>");
                    sb.AppendLine(@"<td width=""70%"">");
                    sb.AppendLine(@"<input type='text' name=""subject"" size=""30"" maxlength=""41""");
                    if ((strMode == "editTopic") || (strMode == "PM"))
                    {
                        sb.AppendLine((" value=\""
                                        + (strTopicSubject + "\"")));
                    }
                    sb.AppendLine("/>");
                    // If this is the    forums moderator or forum admim    then let them select the    priority level of the post
                    if ((((FSPortal.Variables.User.Administrador == true)
                                || (Variables.Forum.blnPriority == true))
                                && ((strMode == "new")
                                || ((strMode == "editTopic")
                                || (strMode == "poll")))))
                    {

                    }
                    sb.AppendLine(@"<span    class=""text"">");
                    sb.AppendLine(Variables.Forum.strTxtPriority);
                    sb.AppendLine(@": <select name=""priority"">");
                    sb.AppendLine(@"<option value=""0""");
                    if ((intTopicPriority == 0))
                    {
                        sb.AppendLine(" selected");
                    }
                    sb.AppendLine(">");
                    sb.AppendLine(Variables.Forum.strTxtNormal);
                    sb.AppendLine("</option>");
                    sb.AppendLine(@"<option value=""1""");
                    if ((intTopicPriority == 1))
                    {
                        sb.AppendLine(" selected");
                    }
                    sb.AppendLine(">");
                    sb.AppendLine(Variables.Forum.strTxtPinnedTopic);
                    sb.AppendLine("</option>");
                    // If this is the    forum admin or moderator let them post an annoucment to    this forum
                    if (((FSPortal.Variables.User.Administrador == true)
                                || Variables.Forum.blnModerator))
                    {

                    }
                    sb.AppendLine(@"<option value=""2""");
                    if ((intTopicPriority == 2))
                    {
                        sb.AppendLine(" selected");
                    }
                    sb.AppendLine(">");
                    sb.AppendLine(Variables.Forum.strTopThisForum);
                    sb.AppendLine("</option>");
                    sb.AppendLine(@"<option value=""3""");
                    if (intTopicPriority == 3) sb.AppendLine(" selected");
                    sb.AppendLine(">");
                    sb.AppendLine(Variables.Forum.strTxtTopAllForums);
                    sb.AppendLine("</option>");
                    sb.AppendLine("</select>");
                    sb.AppendLine("</span>");
                    // Else the priority of the post is normal
                    sb.AppendLine("<input type=\"hidden\" name=\"priority\" value=\"0\" />");
                    sb.AppendLine("</td>");
                    sb.AppendLine("</tr>");
                    sb.AppendLine(@"<poll_form:poll_form ID=""poll_form"" runat=""server"" />");
                }
                sb.AppendLine("<tr>");
                sb.AppendLine(@"<td align=""right"" width=""15%""><span class=""text""> </span></td>");
                sb.AppendLine(@"<td width=""70%"" valign=""bottom"">");
                sb.AppendLine(@"<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"">");
                sb.AppendLine("<tr>");
                sb.AppendLine("<td>");
                sb.AppendLine(@"<table width=""477""    border=""0"" cellspacing=""0"" cellpadding=""1"">");
                sb.AppendLine("<tr>");
                sb.AppendLine(@"<td width=""346"">");
                sb.AppendLine(@"<select    name=""selectFont"" onChange=""FontCode(selectFont.options[selectFont.selectedIndex].value, 'FONT');document.frmAddMessage.selectFont.options[0].selected = true;"">");
                sb.AppendLine("<option selected>--");
                sb.AppendLine(Variables.Forum.strTxtFont);
                sb.AppendLine("--</option>");
                sb.AppendLine(@"<option value=""FONT=Arial"">Arial</option>");
                sb.AppendLine(@"<option value=""FONT=Courier"">Courier New</option>");
                sb.AppendLine(@"<option value=""FONT=Times"">Times New Roman</option>");
                sb.AppendLine(@"<option value=""FONT=Verdana"">Verdana</option>");
                sb.AppendLine("</select>");
                sb.AppendLine(@"<select    name=""selectSize"" onChange=""FontCode(selectSize.options[selectSize.selectedIndex].value, 'SIZE');document.frmAddMessage.selectSize.options[0].selected = true;"">");
                sb.AppendLine("<option selected>--");
                sb.AppendLine(Variables.Forum.strTxtSize);
                sb.AppendLine("--</option>");
                sb.AppendLine(@"<option value=""SIZE=1"">1</option>");
                sb.AppendLine(@"<option value=""SIZE=2"">2</option>");
                sb.AppendLine(@"<option value=""SIZE=3"">3</option>");
                sb.AppendLine(@"<option value=""SIZE=4"">4</option>");
                sb.AppendLine(@"<option value=""SIZE=5"">5</option>");
                sb.AppendLine(@"<option value=""SIZE=6"">6</option>");
                sb.AppendLine("</select>");
                sb.AppendLine(@"<select    name=""selectColour"" onChange=""FontCode(selectColour.options[selectColour.selectedIndex].value, 'COLOR');document.frmAddMessage.selectColour.options[0].selected = true;"">");
                sb.AppendLine(@"<option value=""0"" selected>--");
                sb.AppendLine(Variables.Forum.strTxtFontColour);
                sb.AppendLine("--</option>");
                sb.AppendLine(@"<option value=""COLOR=BLACK"">");
                sb.AppendLine(Variables.Forum.strTxtBlack);
                sb.AppendLine("</option>");
                sb.AppendLine(@"<option value=""COLOR=WHITE"">");
                sb.AppendLine(Variables.Forum.strTxtWhite);
                sb.AppendLine("</option>");
                sb.AppendLine(@"<option value=""COLOR=BLUE"">");
                sb.AppendLine(Variables.Forum.strTxtBlue);
                sb.AppendLine("</option>");
                sb.AppendLine(@"<option value=""COLOR=RED"">");
                sb.AppendLine(Variables.Forum.strTxtRed);
                sb.AppendLine("</option>");
                sb.AppendLine(@"<option value=""COLOR=GREEN"">");
                sb.AppendLine(Variables.Forum.strTxtGreen);
                sb.AppendLine("</option>");
                sb.AppendLine(@"<option value=""COLOR=YELLOW"">");
                sb.AppendLine(Variables.Forum.strTxtYellow);
                sb.AppendLine("</option>");
                sb.AppendLine(@"<option value=""COLOR=ORANGE"">");
                sb.AppendLine(Variables.Forum.strTxtOrange);
                sb.AppendLine("</option>");
                sb.AppendLine(@"<option value=""COLOR=BROWN"">");
                sb.AppendLine(Variables.Forum.strTxtBrown);
                sb.AppendLine("</option>");
                sb.AppendLine(@"<option value=""COLOR=MAGENTA"">");
                sb.AppendLine(Variables.Forum.strTxtMagenta);
                sb.AppendLine("</option>");
                sb.AppendLine(@"<option value=""COLOR=CYAN"">");
                sb.AppendLine(Variables.Forum.strTxtCyan);
                sb.AppendLine("</option>");
                sb.AppendLine(@"<option value=""COLOR=LIME GREEN"">");
                sb.AppendLine(Variables.Forum.strTxtLimeGreen);
                sb.AppendLine("</option>");
                sb.AppendLine("</select>");
                sb.AppendLine("</table>");
                sb.AppendLine(@"<table width=""477""    border=""0"" cellspacing=""0"" cellpadding=""1"">");
                sb.AppendLine("<tr>");
                sb.AppendLine(@"<td width=""357"" nowrap=""nowrap""><a href=""JavaScript:AddMessageCode('B','");
                sb.AppendLine(Variables.Forum.strTxtEnterBoldText);
                sb.AppendLine(@"', '')""><img src=""");
                sb.AppendLine(Variables.Forum.strImagePath);
                sb.AppendLine(@"post_button_bold.gif"" align=""middle""    border=""0"" alt=""");
                sb.AppendLine(Variables.Forum.strTxtBold);
                sb.AppendLine(@"""></a>");
                sb.AppendLine(@"<a href=""JavaScript:AddMessageCode('I','");
                sb.AppendLine(Variables.Forum.strTxtEnterItalicText);
                sb.AppendLine(@"',    '')""><img src=""");
                sb.AppendLine(Variables.Forum.strImagePath);
                sb.AppendLine(@"post_button_italic.gif"" align=""middle"" border=""0""    alt=""");
                sb.AppendLine(Variables.Forum.strTxtItalic);
                sb.AppendLine(@"""></a>");
                sb.AppendLine(@"<a href=""JavaScript:AddMessageCode('U','");
                sb.AppendLine(Variables.Forum.strTxtEnterUnderlineText);
                sb.AppendLine(@"', '')""><img src=""");
                sb.AppendLine(Variables.Forum.strImagePath);
                sb.AppendLine(@"post_button_underline.gif"" align=""middle"" border=""0"" alt=""");
                sb.AppendLine(Variables.Forum.strTxtUnderline);
                sb.AppendLine(@"""></a>");
                sb.AppendLine(@"<a href=""JavaScript:AddCode('URL')""><img src=""");
                sb.AppendLine(Variables.Forum.strImagePath);
                sb.AppendLine(@"post_button_hyperlink.gif"" align=""middle"" border=""0"" alt=""");
                sb.AppendLine(Variables.Forum.strTxtAddHyperlink);
                sb.AppendLine(@"""></a>");
                sb.AppendLine(@"<a href=""JavaScript:AddCode('EMAIL')""><img src=""");
                sb.AppendLine(Variables.Forum.strImagePath);
                sb.AppendLine(@"post_button_email.gif"" align=""middle"" border=""0""    alt=""");
                sb.AppendLine(Variables.Forum.strTxtAddEmailLink);
                sb.AppendLine(@"""></a>");
                sb.AppendLine(@"<a href=""JavaScript:AddMessageCode('CENTER','");
                sb.AppendLine(Variables.Forum.strTxtEnterCentredText);
                sb.AppendLine(@"', '')""><img    src=""");
                sb.AppendLine(Variables.Forum.strImagePath);
                sb.AppendLine(@"post_button_centre.gif"" align=""middle"" border=""0"" alt=""");
                sb.AppendLine(Variables.Forum.strTxtCentre);
                sb.AppendLine(@"""></a>");
                sb.AppendLine(@"<a href=""JavaScript:AddCode('LIST', '')""><img src=""");
                sb.AppendLine(Variables.Forum.strImagePath);
                sb.AppendLine(@"post_button_list.gif"" align=""middle"" border=""0"" alt=""");
                sb.AppendLine(Variables.Forum.strTxtList);
                sb.AppendLine(@"""></a>");
                sb.AppendLine(@"<a href=""JavaScript:AddCode('INDENT', '')""><img    src=""");
                sb.AppendLine(Variables.Forum.strImagePath);
                sb.AppendLine(@"post_button_indent.gif"" align=""middle"" border=""0"" alt=""");
                sb.AppendLine(Variables.Forum.strTxtIndent);
                sb.AppendLine(@"""></a>");
                sb.AppendLine(@"<a href=""JavaScript:AddCode('IMG')""><img src=""");
                sb.AppendLine(Variables.Forum.strImagePath);
                sb.AppendLine(@"post_button_image.gif"" align=""middle"" border=""0"" alt=""");
                sb.AppendLine(Variables.Forum.strTxtAddImage);
                sb.AppendLine(@"""></a>");
                // If image uploading is allowed have an image upload button
                if (Variables.Forum.blnImageUpload)
                {

                }
                sb.AppendLine(@"<a href=""javascript:openWin('upload_images.aspx?MSG=BSC&FID=");
                sb.AppendLine(Variables.Forum.intForumID.ToString());
                sb.AppendLine(@"','images','toolbar=0,location=0,status=0,menubar=0,scrollbars=0,resizable=1,width=400,height=150')""><img src=""");
                sb.AppendLine(Variables.Forum.strImagePath);
                sb.AppendLine(@"post_button_image_upload.gif"" align=""middle""    alt=""");
                sb.AppendLine(Variables.Forum.strTxtImageUpload);
                sb.AppendLine(@"""    border=""0""></a>");
                sb.AppendLine(@"<a href=""javascript:openWin('upload_files.aspx?MSG=BSC&FID=");
                sb.AppendLine(Variables.Forum.intForumID.ToString());
                sb.AppendLine(@"','files','toolbar=0,location=0,status=0,menubar=0,scrollbars=0,resizable=1,width=400,height=150')""><img src=""");
                sb.AppendLine(Variables.Forum.strImagePath);
                sb.AppendLine(@"post_button_file_upload.gif"" align=""middle"" alt=""");
                sb.AppendLine(Variables.Forum.strTxtFileUpload);
                sb.AppendLine(@""" border=""0""></a>");
                sb.AppendLine("</td>");
                sb.AppendLine(@"<td width=""126"" align=""right"" class=""text"" nowrap=""nowrap"">");
                sb.AppendLine(Variables.Forum.strTxtMode);
                sb.AppendLine(":");
                sb.AppendLine(@"<select    name=""selectMode"" onChange=PromptMode(this)>");
                sb.AppendLine(@"<option value=""1"" selected>");
                sb.AppendLine(Variables.Forum.strTxtPrompt);
                sb.AppendLine("</option>");
                sb.AppendLine(@"<option value=""0"">");
                sb.AppendLine(Variables.Forum.strTxtBasic);
                sb.AppendLine("</option>");
                sb.AppendLine("</select>");
                sb.AppendLine("</td>");
                sb.AppendLine("</tr>");
                sb.AppendLine("</table>");
                sb.AppendLine("</td>");
                sb.AppendLine("</tr>");
                sb.AppendLine("</table>");
                sb.AppendLine("</td>");
                sb.AppendLine("</tr>");
                sb.AppendLine("<tr>");
                sb.AppendLine(@"<td valign=""top"" align=""right""    width=""15%"" class=""text"">");
                sb.AppendLine(Variables.Forum.strTxtMessage);
                sb.AppendLine("*:");
                // If emoticons are enabled show them next to the post window
                if (Variables.Forum.blnEmoticons)
                {
                    sb.AppendLine(@"<table border=""0"" cellspacing=""0"" cellpadding=""4"" align=""center"">");
                    sb.AppendLine(@"<tr><td class=""smText"" colspan=""3"" align=""center""><br />");
                    sb.AppendLine(Variables.Forum.strTxtEmoticons);
                    sb.AppendLine("</td></tr>");
                    // Intilise the index position (we are starting at 1 instead of position 0 in the array for simpler calculations)
                    int intIndexPosition = 1;
                    // Calcultae the number of outer loops to do
                    int intNumberOfOuterLoops = 4;
                    // If there is a remainder add 1 to the number of loops
                    if (((Variables.Forum.saryEmoticons.Length % 2)
                                > 0))
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
                                sb.AppendLine(("\r\n" + "\t<td class=\"text\"> </td>"));
                                // Else show the emoticon
                            }
                            else
                            {
                                sb.AppendLine(("\r\n" + ("\t<td class=\"text\"><a href=\"JavaScript:AddSmileyIcon(\'"
                                                + (Variables.Forum.saryEmoticons[intIndexPosition, 2] + ("\')\"><img src=\""
                                                + (Variables.Forum.saryEmoticons[intIndexPosition, 3] + ("\" border=\"0\" alt=\""
                                                + (Variables.Forum.saryEmoticons[intIndexPosition, 2] + "\"></a></td>"))))))));
                            }

                            // Minus one form the index position
                            intIndexPosition = (intIndexPosition + 1);
                        }

                        sb.AppendLine("</tr>");
                    }
                    sb.AppendLine(@"<tr><td colspan=""3"" align=""center""><a href=""javascript:openWin('emoticons_smilies.aspx','emot','toolbar=0,location=0,status=0,menubar=0,scrollbars=1,resizable=1,width=400,height=400')"" class=""smLink"">");
                    sb.AppendLine(Variables.Forum.strTxtMore);
                    sb.AppendLine("</a></td></tr>");
                    sb.AppendLine("</table></td>");
                }
                sb.AppendLine("</td>");
                sb.AppendLine(@"<td width=""70%"" valign=""top"">");
                sb.AppendLine(@"<textarea name=""message"" cols=""57"" rows=""12"" wrap=""PHYSICAL"" onSelect=""storeCaret(this);"" onClick=""storeCaret(this);"" onKeyup=""storeCaret(this);"">");
                if (strMode == "edit" || strMode == "quote" || strMode == "editTopic" || strMode == "PM") sb.AppendLine(strMessage);
                sb.AppendLine("</textarea>");
                sb.AppendLine("</td>");
                sb.AppendLine("</tr>");
                sb.AppendLine("<tr>");
                sb.AppendLine(@"<td align=""right"" width=""92"">&nbsp;</td>");
                sb.AppendLine(@"<td width=""508"" valign=""bottom"" class=""text"">&nbsp;<input type='checkbox' name=""forumCodes"" value=""True"" checked />");
                sb.AppendLine(Variables.Forum.strTxtEnable + @" <a href=""JavaScript:openWin('forum_codes.aspx','codes','toolbar=0,location=0,status=0,menubar=0,scrollbars=1,resizable=1,width=550,height=400')"">" + Variables.Forum.strTxtForumCodes + "</a> " + Variables.Forum.strTxtToFormatPosts);
                sb.AppendLine("</td></tr>");
                //If not	PM then	display	another	row
                if (strMode != "PM")
                {
                    //If signature of e-mail notify then display row	to show
                    if ((Variables.Forum.blnLoggedInUserEmail == true && Variables.Forum.blnEmail == true) || Variables.Forum.blnLoggedInUserSignature == true)
                    {
                        sb.AppendLine("<tr>");
                        sb.AppendLine(@"<td align=""right"" width=""92""> </td>");
                        sb.AppendLine(@"<td width=""508"" valign=""bottom"" class=""text"">");
                        // If the    user has a signature offer them    the chance to show it
                        if ((Variables.Forum.blnLoggedInUserSignature == true))
                        {
                            sb.AppendLine(@"<input type='checkbox' name=""signature"" value=""True""");
                            if ((Variables.Forum.blnAttachSignature == true)) sb.AppendLine("\tchecked");
                            sb.AppendLine("/>");
                            sb.AppendLine(Variables.Forum.strTxtShowSignature);
                            sb.AppendLine(@"<input type='checkbox' name=""email"" value=""True""");
                            if (Variables.Forum.blnReplyNotify == true) sb.AppendLine("    checked");
                            sb.AppendLine("/>");
                            sb.AppendLine(Variables.Forum.strTxtEmailNotify);
                        }
                        sb.AppendLine("</td>");
                        sb.AppendLine("</tr>");
                        sb.AppendLine("</tr>");
                    }
                    sb.AppendLine("<tr>");
                    sb.AppendLine(@"<td align=""right"" width=""92""> </td>");
                    sb.AppendLine(@"<td width=""508"" valign=""bottom"" class=""text""> <input type='checkbox' name=""email"" value=""True"" />");
                    sb.AppendLine(Variables.Forum.strTxtEmailNotifyWhenPMIsRead);
                    sb.AppendLine("</td>");
                    sb.AppendLine("</tr>");
                }
                sb.AppendLine("<td>");
                if (strMode != "PM")
                {
                    sb.AppendLine(@"<input    type=""hidden"" name=""mode"" value=""");
                    sb.AppendLine(strMode);
                    sb.AppendLine(@""" />");
                    sb.AppendLine(@"<input    type=""hidden"" name=""FID"" value=""");
                    sb.AppendLine(Variables.Forum.intForumID.ToString());
                    sb.AppendLine(@""" />");
                    sb.AppendLine(@"<input    type=""hidden"" name=""TID"" value=""");
                    sb.AppendLine(Variables.Forum.lngTopicID.ToString());
                    sb.AppendLine(@""" />");
                    sb.AppendLine(@"<input    type=""hidden"" name=""PID"" value=""");
                    sb.AppendLine(lngMessageID.ToString());
                    sb.AppendLine(@""" />");
                    sb.AppendLine(@"<input    type=""hidden"" name=""TPN"" value=""");
                    sb.AppendLine(Variables.Forum.intRecordPositionPageNum.ToString());
                    sb.AppendLine(@""" />");
                    // If reply get the thread position number in the topic
                    if ((strMode == "reply"))
                    {
                        sb.AppendLine(@"<input	type=""hidden"" name=""ThreadPos"" value=""");
                        sb.AppendLine((intTotalNumOfThreads + 1).ToString());
                        sb.AppendLine(@""" />");
                    }
                }
                sb.AppendLine(@"<input type=""hidden"" name=""sessionID"" value=""");
                sb.AppendLine(FSPortal.Variables.User.sessionID);
                sb.AppendLine(@""" />");
                sb.AppendLine("</td>");
                sb.AppendLine(@"<td width=""70%""    align=""center"">");
                sb.AppendLine("<p>");
                // Select    the button for this page
                if (((strMode == "edit")
                            || (strMode == "editTopic")))
                {
                    sb.AppendLine(("<input type=\"submit\" name=\"Submit\" value=\""
                                    + (Variables.Forum.strTxtUpdatePost + "\" onClick=\"return CheckForm();\" />")));
                }
                else if (((strMode == "new")
                            || (strMode == "poll")))
                {
                    sb.AppendLine(("<input type=\"submit\" name=\"Submit\" value=\""
                                    + (Variables.Forum.strTxtNewTopic + "\" onClick=\"return CheckForm();\" />")));
                }
                else if ((strMode == "PM"))
                {
                    sb.AppendLine(("<input type=\"submit\" name=\"Submit\" value=\""
                                    + (Variables.Forum.strTxtPostMessage + "\" onClick=\"return CheckForm();\" />")));
                }
                else
                {
                    sb.AppendLine(("<input type=\"submit\" name=\"Submit\" value=\""
                                    + (Variables.Forum.strTxtPostReply + "\" onClick=\"return CheckForm();\" />")));
                }
                sb.AppendLine(@"<!--<input type=""button"" name=""Preview"" value=""");
                sb.AppendLine(Variables.Forum.strTxtPreviewPost);
                sb.AppendLine(@""" onClick=""OpenPreviewWindow('post_preview.aspx', document.frmAddMessage);"" />-->");
                sb.AppendLine(@"<input type=""reset"" name=""Reset"" value=""");
                sb.AppendLine(Variables.Forum.strTxtResetForm);
                sb.AppendLine(@""" />");
                sb.AppendLine("</p>");
                sb.AppendLine("</td>");
                sb.AppendLine("</tr>");
                sb.AppendLine("</table>");
                sb.AppendLine("</td>");
                sb.AppendLine("</tr>");
                sb.AppendLine("</table>");
                sb.AppendLine("</td>");
                sb.AppendLine("</tr>");
                sb.AppendLine("</table>");
                sb.AppendLine("</form>");
                return sb.ToString();
            }

        }
    }
}
