// <fileheader>
// <copyright file="edit_post.aspx.cs" company="Febrer Software">
//     Fecha: 30/11/2007
//     Path: forum\edit_post.aspx.cs
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
    public class edit_post : FSPortal.BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            contenido = Inicio();
        }

        public string Inicio()
        {
            StringBuilder sb = new StringBuilder();
            // Dimension variables
            string strMode;
            // Holds the mode of the page
            long lngTopicID;
            // Holds the Topic ID number
            string strTopicSubject = "";
            int intTopicPriority = 0;
            // Holds the priority of the topic
            long lngMessageID = 0;
            // Holds the message ID to be edited
            string strQuoteusuario;
            // Holds the quoters usuario
            string strQuoteMessage = "";
            long lngPostUserID = 0;
            // Holds the user ID of the user to post the message
            bool blnForumLocked = false;
            // Set to true if the forum is locked
            int intRecordPositionPageNum;
            // Holds the recorset page number to show the Threads for
            string strMessage = "";
            string strForumName = "";
            bool blnTopicLocked = false;
            // Holds if the topic is locked or not
            // If the user is user is using a banned IP redirect to an error page
            if (FSForum.Includes.common.bannedIP())
            {
                // Clean up
                // rsCommon = Nothing
                // adoCon.Close()
                // adoCon = Nothing
                // Redirect
                Response.Redirect("insufficient_permission.aspx?M=IP");
            }

            // Read in the message ID number to edit
            Variables.Forum.intForumID = int.Parse(Request.QueryString["FID"]);
            lngMessageID = long.Parse(Request.QueryString["PID"]);
            intRecordPositionPageNum = int.Parse(Request.QueryString["TPN"]);
            strMode = Request.QueryString["M"].Substring(0, 2).Trim();
            // Set the page mode
            if ((strMode == "Q"))
            {
                strMode = "quote";
            }
            else if ((strMode == "R"))
            {
                strMode = "reply";
            }
            else
            {
                strMode = "edit";
            }

            // Get the message from the database
            string strSQL;
            DataTable rsCommon;
            FSDatabase.BdUtils db = new FSDatabase.BdUtils("FSForum");
            if ((strMode != "reply"))
            {
                // Initalise the strSQL variable with an SQL statement to query the database get the message details
                strSQL = ("SELECT "
                            + (Variables.Forum.strDbTable + ("Topic.Locked, "
                            + (Variables.Forum.strDbTable + ("Topic.Topic_ID, "
                            + (Variables.Forum.strDbTable + ("Topic.Forum_ID, "
                            + (Variables.Forum.strDbTable + ("Topic.Subject, "
                            + (Variables.Forum.strDbTable + ("Topic.Priority, "
                            + (Variables.Forum.strDbTable + ("Topic.Start_date, "
                            + (Variables.Forum.strDbTable + ("Topic.Locked, "
                            + (Variables.Forum.strDbTable + ("Thread.UsuarioID, "
                            + (Variables.Forum.strDbTable + ("Thread.Message, "
                            + (Variables.Forum.strDbTable + ("Thread.Message_date, " + "Usuarios.usuario ")))))))))))))))))))));
                strSQL = (strSQL + ("FROM "
                            + (Variables.Forum.strDbTable + ("Topic, "
                            + (Variables.Forum.strDbTable + ("Thread, " + "Usuarios "))))));
                strSQL = (strSQL + ("WHERE ("
                            + (Variables.Forum.strDbTable + ("Topic.Topic_ID = "
                            + (Variables.Forum.strDbTable + ("Thread.Topic_ID AND "
                            + (Variables.Forum.strDbTable + ("Thread.UsuarioID = " + ("Usuarios.UsuarioID) AND "
                            + (Variables.Forum.strDbTable + ("Thread.Thread_ID="
                            + (lngMessageID + ";"))))))))))));
                rsCommon = db.Execute(strSQL);
                if ((rsCommon.Rows.Count == 0))
                {
                    // Read in the details from the recordset
                    blnTopicLocked = Functions.ValorBool(rsCommon.Rows[0]["Locked"].ToString());
                    lngTopicID = long.Parse(rsCommon.Rows[0]["Topic_ID"].ToString());
                    Variables.Forum.intForumID = int.Parse(rsCommon.Rows[0]["Forum_ID"].ToString());
                    lngPostUserID = long.Parse(rsCommon.Rows[0]["UsuarioID"].ToString());
                    strQuoteusuario = rsCommon.Rows[0]["usuario"].ToString();
                    strTopicSubject = rsCommon.Rows[0]["Subject"].ToString();
                    intTopicPriority = int.Parse(rsCommon.Rows[0]["Priority"].ToString());
                    if ((blnForumLocked == false))
                    {
                        blnForumLocked = Functions.ValorBool(rsCommon.Rows[0]["Locked"].ToString());
                    }

                    // If this is a post being edited format the post and check dates
                    if ((strMode == "edit"))
                    {
                        // Read in message from rs
                        strMessage = rsCommon.Rows[0]["Message"].ToString();
                        // Convert message back to forum codes and text
                        strMessage = FuncionesForum.EditPostConvertion(strMessage);
                        // If the start topic date and the message date are the same then the user can edit the topic title
                        if ((rsCommon.Rows[0]["Start_date"].ToString() == rsCommon.Rows[0]["Message_date"].ToString()))
                        {
                            strMode = "editTopic";
                        }
                        else if ((strMode == "quote"))
                        {
                            // Read in the message from rs
                            strQuoteMessage = rsCommon.Rows[0]["Message"].ToString();
                        }

                        // If this is a quoted message read in any further details and format the post
                        if ((strMode == "quote"))
                        {
                            // If the post being quoted is written by a guest see if they have a name
                            if ((lngPostUserID == 2))
                            {
                                // Initalise the strSQL variable with an SQL statement to query the database
                                if ((FSDatabase.Utils.BDType == FSDatabase.Utils.TypeBd.SQLServer))
                                {
                                    strSQL = ("Execute "
                                                + (Variables.Forum.strDbProc + ("GuestPoster @lngThreadID = " + lngMessageID)));
                                }
                                else
                                {
                                    strSQL = ("SELECT "
                                                + (Variables.Forum.strDbTable + ("GuestName.Name FROM "
                                                + (Variables.Forum.strDbTable + ("GuestName WHERE "
                                                + (Variables.Forum.strDbTable + ("GuestName.Thread_ID = "
                                                + (lngMessageID + ";"))))))));
                                }

                                // Query the database
                                rsCommon = db.Execute(strSQL);
                                // Read in the quoters name    
                                if ((rsCommon.Rows.Count > 0))
                                {
                                    strQuoteusuario = rsCommon.Rows[0]["Name"].ToString();
                                }

                            }

                            // Build up the quoted thread post
                            strMessage = (" [QUOTE="
                                        + (strQuoteusuario + "] "));
                            strMessage = (strMessage + strQuoteMessage);
                            // Convert the signature back to original format
                            strMessage = FuncionesForum.EditPostConvertion(strMessage);
                            // Place the forum code for closing quote at the end    
                            strMessage = (strMessage + "[/QUOTE] ");
                        }

                    }

                    // Else change the mode from reply to quote
                }
                else
                {
                    strMode = "quote";
                }

                // Read in the forum permissions from the database
                // Initalise the strSQL variable with an SQL statement to query the database
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
                rsCommon = db.Execute(strSQL);
                // If there is a record returned by the recordset then check to see if you need a clave to enter it
                if ((rsCommon.Rows.Count > 0))
                {
                    // Read in forum details from the database
                    strForumName = rsCommon.Rows[0]["Forum_name"].ToString();
                    // Read in wether the forum is locked or not
                    blnForumLocked = bool.Parse(rsCommon.Rows[0]["Locked"].ToString());
                    FuncionesForum.forumPermisisons(Variables.Forum.intForumID, FSPortal.Variables.User.GroupId, int.Parse(rsCommon.Rows[0]["Read"].ToString()), int.Parse(rsCommon.Rows[0]["Post"].ToString()), int.Parse(rsCommon.Rows[0]["Reply_posts"].ToString()), int.Parse(rsCommon.Rows[0]["Edit_posts"].ToString()), int.Parse(rsCommon.Rows[0]["Delete_posts"].ToString()), int.Parse(rsCommon.Rows[0]["Priority_posts"].ToString()), 0, 0, int.Parse(rsCommon.Rows[0]["Attachments"].ToString()), int.Parse(rsCommon.Rows[0]["Image_upload"].ToString()));
                    // If the forum requires a clave and a logged in forum code is not found on the users machine then send them to a login page
                    if (((rsCommon.Rows[0]["clave"].ToString() != "")
                                && !(Web.Cookie(Request.Cookies[FSPortal.Variables.App.strCookieName], ("Forum" + Variables.Forum.intForumID)) == rsCommon.Rows[0]["Forum_code"].ToString())))
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

            }
            //sb.AppendLine("<html>");
            //sb.AppendLine("<head>");
            //sb.AppendLine("<title>");
            if ((strMode == "quote"))
            {
                tituloPagina = Variables.Forum.strTxtPostReply;
            }
            else
            {
                tituloPagina = Variables.Forum.strTxtEditPost;
            }
            //sb.AppendLine("</title>");
            sb.AppendLine("<!-- Check the from is filled in correctly before submitting -->");
            sb.AppendLine(@"<script  language=""javascript"">");
            sb.AppendLine("//Function to check form is filled in correctly before submitting");
            sb.AppendLine("function CheckForm () {");
            sb.AppendLine(@"var errorMsg = """";");
            // If Gecko Madis API (RTE) need to strip default input from the API
            if (FuncionesForum.RTEenabled() == "Gecko")
            {
                sb.AppendLine(("\t//For Gecko Madis API (RTE)" + ("\r\n" + ("\tif (document.frmAddMessage.message.value.indexOf(\'<br />\') > -1 && document.frmAddMessage.message.va" +
                    "lue.length == 8) document.frmAddMessage.message.value = \'\';" + "\r\n"))));
            }

            // If we are editing the first topic then  check for a subject
            if ((strMode == "editTopic"))
            {

            }
            sb.AppendLine("//Check for a subject");
            sb.AppendLine(@"if (document.frmAddMessage.subject.value==""""){");
            sb.AppendLine(@"errorMsg += ""\n\t");
            sb.AppendLine(Variables.Forum.strTxtErrorTopicSubject);
sb.AppendLine(@""";");
            sb.AppendLine("}");
            sb.AppendLine("//Check for a name");
            sb.AppendLine(@"if (document.frmAddMessage.Gname.value==""""){");
            sb.AppendLine(@"errorMsg += ""\n\t");
            sb.AppendLine(Variables.Forum.strTxtNoNameError);
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
            sb.AppendLine("//Reset the submition action");
            sb.AppendLine(@"document.frmAddMessage.action = ""post_message.aspx?PN=");
            int.Parse(Request.QueryString["PN"]);
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
            string txtMode;
            if ((strMode == "quote"))
            {
                txtMode = Variables.Forum.strTxtPostReply;
            }
            else
            {
                txtMode = Variables.Forum.strTxtEditPost;
            }

            string sTituloBarra = ("/ :: <a href=\"../default.aspx\">inicio</a> :: <a href=\'default.aspx\'>foro</a> :: " + ("<a href=\'forum_topics.aspx?FID="
                        + (Variables.Forum.intForumID + ("\'>"
                        + (strForumName + ("</a>" + (" :: " + txtMode)))))));
            sb.AppendLine(@"<navigation:navigation ID=""navigation"" runat=""server"" />");
            // Clean up
            rsCommon = null;
            if ((((((FSPortal.Variables.User.UsuarioId == lngPostUserID)
                        || (FSPortal.Variables.User.Administrador || Variables.Forum.blnModerator))
                        && ((Variables.Forum.blnEdit || FSPortal.Variables.User.Administrador)
                        && ((strMode == "edit")
                        || (strMode == "editTopic"))))
                        || ((Variables.Forum.blnReply == true)
                        && (strMode == "quote")))
                        && (Variables.Forum.blnActiveMember
                        && (((blnForumLocked == false)
                        || FSPortal.Variables.User.Administrador)
                        && ((blnTopicLocked == false)
                        || FSPortal.Variables.User.Administrador)))))
            {
                // See if the users browser is RTE enabled
                if (((FuncionesForum.RTEenabled() != "false")
                            && ((Variables.Forum.blnRTEEditor == true)
                            && (Variables.Forum.blnWYSIWYGEditor == true))))
                {
                    // Open the message form for RTE enabled browsers
                    RTE_message_form_inc.strMode = strMode;
                    RTE_message_form_inc.strBuddyName = "";
                    RTE_message_form_inc.intTopicPriority = intTopicPriority;
                    RTE_message_form_inc.strTopicSubject = strTopicSubject;
                    RTE_message_form_inc.RTEenabled = FuncionesForum.RTEenabled();
                    RTE_message_form_inc.lngMessageID = lngMessageID;
                    RTE_message_form_inc.intTotalNumOfThreads = 0;
                }
                sb.AppendLine(RTE_message_form_inc.Render(Request.Form["PN"]));
                //sb.AppendLine(@"<RTE_message_form:RTE_message_form ID=""RTE_message_form"" runat=""server"" />");
                // Open up the mesage form for non RTE enabled browsers
                message_form_inc.strMessage = strMessage;
                message_form_inc.strMode = strMode;
                message_form_inc.strBuddyName = "";
                message_form_inc.intTopicPriority = intTopicPriority;
                message_form_inc.strTopicSubject = strTopicSubject;
                message_form_inc.RTEenabled = FuncionesForum.RTEenabled();
                message_form_inc.lngMessageID = lngMessageID;
                message_form_inc.intTotalNumOfThreads = 0;
                sb.AppendLine(message_form_inc.Render(Request.QueryString["M"], Request.Form["PN"]));
                //sb.AppendLine(@"<message_form:message_form ID=""message_form"" runat=""server"" />");
            }

            sb.AppendLine(("\r\n" + "  <br />"));
            // Else if the forum is locked display a message telling the user so
            blnForumLocked = true;
            sb.AppendLine(("\r\n" + ("<div align=\"center\"><br /><br /><span class=\"text\">"
                            + (Variables.Forum.strTxtForumLockedByAdmim + "</span><br /><br /><br /><br /><br /></div>"))));
            // Display message if the topic is locked and the message is quoted
            if (blnTopicLocked & strMode == "quote")
            {
                sb.AppendLine(("\r\n" + ("<div align=\"center\"><br /><br /><span class=\"text\">"
                                + (Variables.Forum.strTxtSorryNoReply + ("<br />"
                                + (Variables.Forum.strTxtThisTopicIsLocked + "</span><br /><br /><br /><br /><br /></div>"))))));
            }
            // Else if the user does not have permision to reply in this forum
            if ((FSPortal.Variables.User.GroupId != 2)
                        & strMode == "quote")
            {
                Variables.Forum.blnReply = false;
            }

            sb.AppendLine(("\r\n" + ("<div align=\"center\"><br /><br /><span class=\"text\">"
                            + (Variables.Forum.strTxtSorryYouDoNotHavePerimssionToReplyToPostsInThisForum + "</span><br /><br />"))));
            sb.AppendLine(("\r\n" + ("<a href=\"javascript:history.back(1)\" target=\"_self\">"
                            + (Variables.Forum.strTxtReturnForumTopic + "</a><br /><br /><br /><br /></div>"))));
            // Else the user is not logged in so let them know to login before they can post a message
            strMode = "quote";
            sb.AppendLine(("\r\n" + ("<div align=\"center\"><br /><br /><span class=\"text\">"
                            + (Variables.Forum.strTxtSorryYouDoNotHavePerimssionToReplyToPostsInThisForum + "</span><br /><br />"))));
            sb.AppendLine(("\r\n" + ("<a href=\"registration_rules.aspx?FID="
                            + (Variables.Forum.intForumID + ("\" target=\"_self\"><img src=\""
                            + (Variables.Forum.strImagePath + ("register.gif\"  alt=\""
                            + (Variables.Forum.strTxtRegister + ("\" border=\"0\" align=\"middle\" /></a>  <a href=\"login_user.aspx?FID="
                            + (Variables.Forum.intForumID + ("\" target=\"_self\"><img src=\""
                            + (Variables.Forum.strImagePath + ("login.gif\"  alt=\""
                            + (Variables.Forum.strTxtLogin + "\" border=\"0\" align=\"middle\" /></a><br /><br /><br /><br /></div>"))))))))))))));
            // Else the user is not the person who posted the message so display an error message
            sb.AppendLine(("\r\n" + ("<div align=\"center\"><br /><br /><span class=\"text\">"
                            + (Variables.Forum.strTxtNoPermissionToEditPost + "</span><br /><br />"))));
            sb.AppendLine(("\r\n" + ("<a href=\"javascript:history.back(1)\" target=\"_self\">"
                            + (Variables.Forum.strTxtReturnForumTopic + "</a><br /><br /><br /><br /></div>"))));
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            return sb.ToString();
        }

    }

}
