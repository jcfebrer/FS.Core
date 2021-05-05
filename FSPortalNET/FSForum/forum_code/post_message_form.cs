// <fileheader>
// <copyright file="post_message_form.aspx.cs" company="Febrer Software">
//     Fecha: 30/11/2007
//     Path: forum\post_message_form.aspx.cs
//     Copyright (c) 2003-2007 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>
using FSForum.Includes;
using FSNetwork;
using FSLibrary;
using FSPortal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;

namespace FSForum
{
    public class post_message_form : FSPortal.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            contenido = Inicio();
        }
        public string Inicio()
        {
            StringBuilder sb = new StringBuilder();
            //sb.AppendLine(@"<common:common ID=""common"" runat=""server"" />");
            common.Inicio();
            //sb.AppendLine(@"<emoticons_inc:emoticons_inc ID=""emoticons_inc"" runat=""server"" />");
            emoticons_inc.Load();
            // Set the response buffer to true as we maybe redirecting
            // Response.Buffer = True 
            // Make sure this page is not cached
            // Response.Expires = -1
            // Response.ExpiresAbsolute = Now() - 2
            // Response.AddHeader "pragma","no-cache"
            // Response.AddHeader "cache-control","private"
            // Response.CacheControl = "No-Store"
            // Dimension variables
            string strMode;
            // Holds the mode of the page
            //long lngTopicID = 0;
            // Holds the Topic ID number
            long lngMessageID = 0;
            // Holds the Thread ID of the post
            string strForumName = "";
            bool blnForumLocked;
            // Set to true if the forum is locked
            //int intTopicPriority = 0;
            // Holds the priority of the topic
            //int intRecordPositionPageNum = 1;
            // Holds the recorset page number to show the Threads for
            DataTable rscommon;
            // If the user is user is using a banned IP redirect to an error page
            if (common.bannedIP())
            {
                // Clean up
                rscommon = null;
                Response.Redirect("insufficient_permission.aspx?M=IP");
            }

            // Intialise variables
            //lngTopicID = 0;
            //lngMessageID = 0;
            //intTopicPriority = 0;
            //intRecordPositionPageNum = 1;
            strMode = "new";
            Variables.Forum.intForumID = int.Parse(Request.QueryString["FID"]);
            string strSQL;
            FSDatabase.BdUtils db = new FSDatabase.BdUtils("FSForum");
            if ((FSDatabase.Utils.BDType == FSDatabase.Utils.TypeBd.SQLServer))
            {
                strSQL = ("Execute "
                            + (Variables.Forum.strDbProc + ("ForumsAllWhereForumIs @Variables.Forum.intForumID = " + Variables.Forum.intForumID)));
            }
            else
            {
                strSQL = ("SELECT "
                            + (Variables.Forum.strDbTable + ("Forum.* FROM "
                            + (Variables.Forum.strDbTable + ("Forum WHERE Forum_ID = "
                            + (Variables.Forum.intForumID + ";"))))));
            }

            // Query the database
            rscommon = db.Execute(strSQL);
            // If there is a record returned by the recordset then check to see if you need a clave to enter it
            if ((rscommon.Rows.Count > 0))
            {
                // Read in forum details from the database
                strForumName = rscommon.Rows[0]["Forum_name"].ToString();
                // Read in wether the forum is locked or not
                blnForumLocked = bool.Parse(rscommon.Rows[0]["Locked"].ToString());
                FuncionesForum.forumPermisisons(Variables.Forum.intForumID, FSPortal.Variables.User.GroupId, int.Parse(rscommon.Rows[0]["Read"].ToString()), int.Parse(rscommon.Rows[0]["Post"].ToString()), int.Parse(rscommon.Rows[0]["Reply_posts"].ToString()), int.Parse(rscommon.Rows[0]["Edit_posts"].ToString()), int.Parse(rscommon.Rows[0]["Delete_posts"].ToString()), int.Parse(rscommon.Rows[0]["Priority_posts"].ToString()), 0, 0, int.Parse(rscommon.Rows[0]["Attachments"].ToString()), int.Parse(rscommon.Rows[0]["Image_upload"].ToString()));
                // If the forum requires a clave and a logged in forum code is not found on the users machine then send them to a login page
                if ((!(rscommon.Rows[0]["clave"].ToString() == "")
                            && !(Web.Cookie(Request.Cookies[FSPortal.Variables.App.strCookieName], ("Forum" + Variables.Forum.intForumID)) == rscommon.Rows[0]["Forum_code"].ToString())))
                {
                    // Redirect to a page asking for the user to enter the forum clave
                    Response.Redirect(("forum_clave_form.aspx?FID=" + Variables.Forum.intForumID));
                }

            }

            // If the forum level for the user on this forum is read only set the forum to be locked
            if (((Variables.Forum.blnRead == false)
                        && ((Variables.Forum.blnModerator == false)
                        && (FSPortal.Variables.User.Administrador == false))))
            {
                blnForumLocked = true;
            }
            //sb.AppendLine("<html>");
            //sb.AppendLine("<head>");
            //sb.AppendLine("<title>Post New Topic</title>");
            tituloPagina = "Post New Topic";
            sb.AppendLine("<!-- Check the from is filled in correctly before submitting -->");
            sb.AppendLine(@"<script language=""javascript"">");
            sb.AppendLine("//Function to check form is filled in correctly before submitting");
            sb.AppendLine("function CheckForm () {");
            sb.AppendLine(@"var errorMsg = """";");
            // If Gecko Madis API (RTE) need to strip default input from the API
            if ((FuncionesForum.RTEenabled() == "Gecko"))
            {
                sb.AppendLine(("\t//For Gecko Madis API (RTE)" + ("\r\n" + ("\tif (document.frmAddMessage.message.value.indexOf(\'<br />\') > -1 && document.frmAddMessage.message.va" +
                    "lue.length == 8) document.frmAddMessage.message.value = \'\';" + "\r\n"))));
            }

            // If this is a guest posting check that they have entered their name
            if ((Variables.Forum.blnPost
                        && (FSPortal.Variables.User.UsuarioId == 2)))
            {

            }
            sb.AppendLine("//Check for a name");
            sb.AppendLine(@"if (document.frmAddMessage.Gname.value==""""){");
            sb.AppendLine(@"errorMsg += ""\n\t");
            sb.AppendLine(Variables.Forum.strTxtNoNameError);
            sb.AppendLine(@""";");
            sb.AppendLine("}");
            sb.AppendLine("//Check for a subject");
            sb.AppendLine(@"if (document.frmAddMessage.subject.value==""""){");
            sb.AppendLine(@"errorMsg += ""\n\t");
            sb.AppendLine(Variables.Forum.strTxtErrorTopicSubject);
            sb.AppendLine(@""";");
            sb.AppendLine("}");
            sb.AppendLine("//Check for message");
            sb.AppendLine(@"if (document.frmAddMessage.message.value==""""){");
            sb.AppendLine(@"errorMsg += ""\n\t");
            sb.AppendLine(Variables.Forum.strTxtNoMessageError);
            sb.AppendLine(@""";");
            sb.AppendLine("}");
            sb.AppendLine("//If there is aproblem with the form then display an error");
            sb.AppendLine(@"if (errorMsg != """"){");
            sb.AppendLine(@"msg = """);
            sb.AppendLine(Variables.Forum.strTxtErrorDisplayLine);
            sb.AppendLine(@"\n\n"";");
            sb.AppendLine(@"msg += """);
            sb.AppendLine(Variables.Forum.strTxtErrorDisplayLine1);
            sb.AppendLine(@"\n"";");
            sb.AppendLine(@"msg += """);
            sb.AppendLine(Variables.Forum.strTxtErrorDisplayLine2);
            sb.AppendLine(@"\n"";");
            sb.AppendLine(@"msg += """);
            sb.AppendLine(Variables.Forum.strTxtErrorDisplayLine);
            sb.AppendLine(@"\n\n"";");
            sb.AppendLine(@"msg += """);
            sb.AppendLine(Variables.Forum.strTxtErrorDisplayLine3);
            sb.AppendLine(@"\n"";");
            sb.AppendLine(@"errorMsg += alert(msg + errorMsg + ""\n\n"");");
            sb.AppendLine("return false;");
            sb.AppendLine("}");
            sb.AppendLine("//Reset the submition page back to it's original place");
            sb.AppendLine(@"document.frmAddMessage.action = ""post_message.aspx?PN=");
            sb.AppendLine(Variables.Forum.intTopicPageNumber.ToString());
            sb.AppendLine(@"""");
            sb.AppendLine(@"document.frmAddMessage.target = ""_self"";");
            if (((FuncionesForum.RTEenabled() != "false")
                                && (Variables.Forum.blnRTEEditor && Variables.Forum.blnWYSIWYGEditor)))
            {
                sb.AppendLine(("\r\n" + "\tdocument.frmAddMessage.Submit.disabled=true;"));
            }
            sb.AppendLine("return true;");
            sb.AppendLine("}");
            sb.AppendLine("</script>");
            string sTituloBarra;
            sTituloBarra = ("/ :: <a href=\"../default.aspx\">inicio</a> :: <a href=\'default.aspx\'>foro</a> :: " + ("<a href=\'forum_topics.aspx?FID="
                        + (Variables.Forum.intForumID + ("\'>"
                        + (strForumName + ("</a>" + (" :: " + Variables.Forum.strTxtPostNewTopic)))))));
            sb.AppendLine(@"<navigation:navigation ID=""common1"" runat=""server"" />");
            sb.AppendLine(@"<table width=""");
            sb.AppendLine(Variables.Forum.strTableVariableWidth);
            sb.AppendLine(@""" border=""0"" cellspacing=""0"" cellpadding=""3"" align=""center"">");
            sb.AppendLine("<tr>");
            sb.AppendLine(@"<td align=""left"" class=""heading"">");
            sb.AppendLine(sTituloBarra);
            // sb.AppendLine(Variables.Forum.strTxtPostNewTopic)}
            sb.AppendLine("</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("<tr>");
            sb.AppendLine(@"<!--<td align=""left"" width=""71%"" class=""bold""><img src=""");
            sb.AppendLine(Variables.Forum.strImagePath);
            sb.AppendLine(@"open_folder_icon.gif"" border=""0"" align=""middle""> <a href=""default.aspx"" target=""_self"" class=""boldLink"">");
            sb.AppendLine(Variables.Forum.strMainForumName);
            sb.AppendLine("</a>");
            sb.AppendLine(("<a href=\"forum_topics.aspx?FID="
                                    + (Variables.Forum.intForumID + ("\" target=\"_self\" class=\"boldLink\">"
                                    + (strForumName + "</a>")))));
            sb.AppendLine(Variables.Forum.strTxtPostNewTopic);
            sb.AppendLine("<br /></td>-->");
            sb.AppendLine("</tr>");
            sb.AppendLine("</table><br />");
            // Clean up
            rscommon = null;
            if (((Variables.Forum.blnPost == true)
                        && ((Variables.Forum.blnActiveMember == true)
                        && ((Variables.Forum.blnForumLocked == false)
                        || (FSPortal.Variables.User.Administrador == true)))))
            {
                // See if the users browser is RTE enabled
                if (((FuncionesForum.RTEenabled() != "false")
                            && ((Variables.Forum.blnRTEEditor == true)
                            && (Variables.Forum.blnWYSIWYGEditor == true))))
                {
                    // Open the message form for RTE enabled browsers
                    RTE_message_form_inc.strMode = strMode;
                    RTE_message_form_inc.strBuddyName = "";
                    RTE_message_form_inc.intTopicPriority = 0;
                    RTE_message_form_inc.strTopicSubject = "";
                    RTE_message_form_inc.RTEenabled = FuncionesForum.RTEenabled();
                    RTE_message_form_inc.lngMessageID = lngMessageID;
                    RTE_message_form_inc.intTotalNumOfThreads = 0;
                }

                //sb.AppendLine(@"<RTE_message_form:RTE_message_form ID=""RTE_message_form"" runat=""server"" />");
                sb.AppendLine(RTE_message_form_inc.Render(Request.Form["PN"]));
            }
            else
            {
                // Open up the mesage form for non RTE enabled browsers
                message_form_inc.strBuddyName = "";
                message_form_inc.intTopicPriority = 0;
                message_form_inc.strTopicSubject = "";
                message_form_inc.RTEenabled = FuncionesForum.RTEenabled();
                message_form_inc.lngMessageID = lngMessageID;
                message_form_inc.intTotalNumOfThreads = 0;
                message_form_inc.strMode = strMode;
                message_form_inc.strMessage = "";
                //sb.AppendLine(@"<message_form:message_form ID=""message_form"" runat=""server"" />");
                sb.AppendLine(message_form_inc.Render(Request.QueryString["M"], Request.Form["PN"]));
            }

            // If the users account is suspended then let them know
            Variables.Forum.blnActiveMember = false;
            sb.AppendLine(("\r\n" + "<div align=\"center\"><br /><br /><span class=\"text\">"));
            // If mem suspended display message
            if (((Variables.Forum.strLoggedInUserCode.IndexOf("N0act") + 1)
                        > 0))
            {
                sb.AppendLine(Variables.Forum.strTxtForumMemberSuspended);
                // Else account not yet active
            }
            else
            {
                sb.AppendLine(("<span class=\"lgText\">"
                                + (Variables.Forum.strTxtForumMembershipNotAct + ("</span><br /><br />" + Variables.Forum.strTxtToActivateYourForumMem))));
            }

            // If email is on then place a re-send activation email link
            if ((((Variables.Forum.strLoggedInUserCode.IndexOf("N0act") + 1)
                        == 0)
                        && (Variables.Forum.blnEmailActivation && Variables.Forum.blnLoggedInUserEmail)))
            {
                sb.AppendLine(("<br /><br /><a href=\"JavaScript:openWin(\'resend_email_activation.aspx\',\'actMail\',\'toolbar=0,location=" +
                    "0,status=0,menubar=0,scrollbars=1,resizable=1,width=475,height=200\')\">"
                                + (Variables.Forum.strTxtResendActivationEmail + "</a>")));
            }

            sb.AppendLine("</span><br /><br /><br /><br /></div>");
            // Else if the forum is locked display a message telling the user so
            blnForumLocked = true;
            sb.AppendLine(("\r\n" + ("<div align=\"center\"><br /><br /><span class=\"text\">"
                            + (Variables.Forum.strTxtForumLockedByAdmim + "</span><br /><br /><br /><br /><br /></div>"))));
            // Else if the user does not have permision to post in this forum
            Variables.Forum.blnPost = (false
                        & (FSPortal.Variables.User.GroupId != 2));
            sb.AppendLine(("\r\n" + ("<div align=\"center\"><br /><br /><span class=\"text\">"
                            + (Variables.Forum.strTxtSorryYouDoNotHavePermissionToPostInTisForum + "</span><br /><br />"))));
            sb.AppendLine(("\r\n" + ("<a href=\"javascript:history.back(1)\" target=\"_self\">"
                            + (Variables.Forum.strTxtReturnToDiscussionForum + "</a><br /><br /><br /><br /></div>"))));
            // Else the user is not logged in so let them know to login before they can post a message
            sb.AppendLine(("\r\n" + ("<div align=\"center\"><br /><br /><span class=\"text\">"
                            + (Variables.Forum.strTxtMustBeRegisteredToPost + "</span><br /><br />"))));
            sb.AppendLine(("\r\n" + ("<a href=\"registration_rules.aspx?FID="
                            + (Variables.Forum.intForumID + ("\" target=\"_self\"><img src=\""
                            + (Variables.Forum.strImagePath + ("register.gif\"  alt=\""
                            + (Variables.Forum.strTxtRegister + ("\" border=\"0\" align=\"middle\" /></a>  <a href=\"login_user.aspx?FID="
                            + (Variables.Forum.intForumID + ("\" target=\"_self\"><img src=\""
                            + (Variables.Forum.strImagePath + ("login.gif\"  alt=\""
                            + (Variables.Forum.strTxtLogin + "\" border=\"0\" align=\"middle\" /></a><br /><br /><br /><br /></div>"))))))))))))));
            sb.AppendLine("</div>");
            return sb.ToString();
        }

    }
}
