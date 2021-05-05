// <fileheader>
// <copyright file="forum_posts.aspx.cs" company="Febrer Software">
//     Fecha: 30/11/2007
//     Path: forum\forum_posts.aspx.cs
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
using System.Text;
using FSPortal;
using FSForum.Includes;
using FSNetwork;

namespace FSForum
{
    public class forum_posts : FSPortal.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            contenido = Inicio();
        }
        public string Inicio()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"<common:common ID=""common"" runat=""server"" />");
            sb.AppendLine(@"<emoticons:emoticons ID=""emoticons"" runat=""server"" />");
            // Response.Buffer = True
            // Make sure this page is not cached
            // Response.Expires = -1
            // Response.ExpiresAbsolute = Now() - 2
            // Response.AddHeader "pragma","no-cache"
            // Response.AddHeader "cache-control","private"
            // Response.CacheControl = "No-Store"
            // Dimension variables
            FSDatabase.BdUtils db = new FSDatabase.BdUtils("FSForum");
            DataTable rsTopic;
            // Holds the Database Recordset Variable for the topic details
            DataTable rsPost;
            // Holds the database recordset variable for the thread
            string strForumName = "";
            string strMode = "";
            string strSubject;
            // Holds the topic subject
            string strusuario;
            // Holds the usuario of the thread
            long lngUserID;
            // Holds the ID number of the user
            System.DateTime dtmTopicDate;
            // Holds the date the thread was made
            string strMessage;
            // Holds the message body of the thread
            long lngMessageID;
            // Holds the message ID number
            string strUsuariosHomepage;
            // Holds the homepage of the usuario if it is given
            string strUsuariosLocation;
            // Holds the location of the user if given
            string strUsuariosAvatar;
            // Holds the Usuarioss avatar
            string strUsuariosSignature;
            // Holds the Usuarioss signature
            long lngUsuariosNumOfPosts;
            // Holds the number of posts the user has made to the forum
            System.DateTime dtmUsuariosRegistration;
            // Holds the registration date of the user
            long lngNumberOfViews;
            // Holds the number of times the topic has been viewed to save back to the database
            //bool blnNoThread = false;
            // Set to true if there is no thread to view
            //bool blnIsModerator = false;
            // Set to true if the user who posted the message is a forum moderator
            int intThreadNo;
            // Holds the number of threads in the topic
            int intPriority;
            // Holds the priority level of the topic
            int intTotalNumOfPages;
            // Holds the number of pages
            int intRecordLoopCounter;
            // Holds the loop counter numeber
            int intTopicPageLoopCounter;
            // Loop counter for other thread page link
            int intTotalNumOfThreads;
            // Holds the total number of therads in this topic
            string strUsuariosIP;
            // Holds the Usuarioss IP
            string strSearchKeywords;
            // Holds the keywords to search for
            string[] sarySearchWord;
            // Array to hold the search words
            string strGroupName;
            // Holds the Usuarioss group name
            int intRankStars;
            // Holds the number of stars for the group
            string strRankCustomStars;
            // Holds custom stars for the user group
            bool blnPollNoReply;
            // Set to true if users can't reply to a poll
            bool blnBannedIP;
            // Set to true if the user is using a banned IP
            System.DateTime dtmLastEntryDate;
            // Holds the date of the last post entry to the topic
            string strMemberTitle;
            // Holds the members title
            bool blnTopicWatched;
            // Set to true if this topic is being watched
            // Initialise variables
            strMode = "reply";
            lngMessageID = 0;
            Variables.Forum.intForumID = 0;
            Variables.Forum.lngTopicID = 0;
            intThreadNo = 0;
            //blnNoThread = false;
            //blnIsModerator = false;
            blnPollNoReply = false;
            blnBannedIP = false;
            blnTopicWatched = false;
            if (common.bannedIP())
            {
                // If the user is using a banned IP then set the banned IP variable to true and active member variable to false
                blnBannedIP = true;
            }

            // If this is the first time the page is displayed then the Forum Thread record position is set to page 1
            if ((Request.QueryString["TPN"] == null))
            {
                Variables.Forum.intRecordPositionPageNum = 1;
                // Else the page has been displayed before so the Forum Thread record postion is set to the Record Position number
            }
            else
            {
                Variables.Forum.intRecordPositionPageNum = int.Parse(Request.QueryString["TPN"]);
            }

            // Read in the Topic ID for the topic to display and page number
            if ((Request.QueryString["TID"] == null))
            {
                Variables.Forum.lngTopicID = 0;
            }
            else
            {
                Variables.Forum.lngTopicID = long.Parse(Request.QueryString["TID"]);
            }

            if ((Request.QueryString["PN"] == null))
            {
                Variables.Forum.intTopicPageNumber = 0;
            }
            else
            {
                Variables.Forum.intTopicPageNumber = int.Parse(Request.QueryString["PN"]);
            }

            if ((Variables.Forum.lngTopicID == 0))
            {
                // Clean up
                // rsCommon = Nothing
                // adoCon.Close()
                // adoCon = Nothing
                // Redirect
                Response.Redirect("default.aspx");
            }

            // Read in the keywords if comming from a search
            strSearchKeywords = TextUtil.Substring(Request.QueryString["KW"], 0, 35).Trim();
            // Split up the keywords to be searched
            sarySearchWord = strSearchKeywords.Trim().Split(' ');
            // Get the posts from the database
            // Create a record set object to the Threads held in the database
            string strSQL;
            // Initalise the strSQL variable with an SQL statement to query the database get the thread details
            if (FSDatabase.Utils.BDType == FSDatabase.Utils.TypeBd.SQLServer)
            {
                strSQL = "Execute "
                            + Variables.Forum.strDbProc + "ThreadDetails @lngTopicID = " + Variables.Forum.lngTopicID;
            }
            else
            {
                strSQL = "SELECT  "
                            + Variables.Forum.strDbTable + "Topic.*, "
                            + Variables.Forum.strDbTable + "Thread.*";
                strSQL = strSQL + " FROM "
                            + Variables.Forum.strDbTable + "Topic, "
                            + Variables.Forum.strDbTable + "Thread";
                strSQL = strSQL + " WHERE "
                            + Variables.Forum.strDbTable + "Topic.Topic_ID = "
                            + Variables.Forum.strDbTable + "Thread.Topic_ID AND "
                            + Variables.Forum.strDbTable + "Topic.Topic_ID = "
                            + Variables.Forum.lngTopicID;
                strSQL = strSQL + " ORDER BY "
                            + Variables.Forum.strDbTable + "Thread.Message_date ASC;";
            }

            // Query the database
            rsPost = db.Execute(strSQL, Variables.Forum.intRecordPositionPageNum, Variables.Forum.intThreadsPerPage);
            // Set the number of records to display on each page
            // rsPost.PageSize = Variables.Forum.intThreadsPerPage
            intTotalNumOfThreads = rsPost.Rows.Count;
            // Count the number of pages there are in the recordset calculated by the PageSize attribute set by admin
            intTotalNumOfPages = intTotalNumOfThreads % Variables.Forum.intThreadsPerPage;
            // rsPost.PageCount
            if (((intTotalNumOfThreads % Variables.Forum.intThreadsPerPage)
                        > 0))
            {
                intTotalNumOfPages = (intTotalNumOfPages + 1);
            }

            // If there is no topic in the database then display the appropraite mesasage
            bool blnExit;
            if ((rsPost.Rows.Count == 0))
            {
                // If there are no thread's to display then display the appropriate error message
                strSubject = Variables.Forum.strTxtNoThreads;
                //blnNoThread = true;
            }
            else
            {
                blnExit = (rsPost.Rows.Count == 0);
                if (((Variables.Forum.intRecordPositionPageNum > intTotalNumOfPages)
                            || (Request.QueryString["get"] == "last")))
                {
                    // Set the page number to show from
                    // rsPost.AbsolutePage = intTotalNumOfPages
                    // Set the page position number to the highest page number
                    Variables.Forum.intRecordPositionPageNum = intTotalNumOfPages;
                    // Else the page number to show from is the requested page number
                }
                else
                {
                    // rsPost.AbsolutePage = intRecordPositionPageNum
                }

                // Read in the number of views for the page form the database
                lngNumberOfViews = long.Parse(rsPost.Rows[0]["No_of_views"].ToString());
                lngNumberOfViews = (lngNumberOfViews + 1);
                // Write the number of times the Topic has been viewed back to the database
                // Initalise the strSQL variable with the SQL string
                if ((FSDatabase.Utils.BDType == FSDatabase.Utils.TypeBd.SQLServer))
                {
                    strSQL = ("Execute "
                                + (Variables.Forum.strDbProc + ("UpdateViewPostCount @lngNumberOfViews = "
                                + (lngNumberOfViews + (", @lngTopicID= " + Variables.Forum.lngTopicID)))));
                }
                else
                {
                    strSQL = ("UPDATE "
                                + (Variables.Forum.strDbTable + "Topic SET "));
                    strSQL = (strSQL + (""
                                + (Variables.Forum.strDbTable + ("Topic.No_of_views=" + lngNumberOfViews))));
                    strSQL = (strSQL + (" WHERE ((("
                                + (Variables.Forum.strDbTable + ("Topic.Topic_ID)="
                                + (Variables.Forum.lngTopicID + "));")))));
                }

                // Write to the database
                db.Execute(strSQL);
                // Read in the thread subject forum ID and where the topic is locked
                Variables.Forum.intForumID = int.Parse(rsPost.Rows[0]["Forum_ID"].ToString());
                Variables.Forum.lngPollID = long.Parse(rsPost.Rows[0]["Poll_ID"].ToString());
                strSubject = rsPost.Rows[0]["Subject"].ToString();
                Variables.Forum.blnTopicLocked = bool.Parse(rsPost.Rows[0]["Locked"].ToString());
                intPriority = int.Parse(rsPost.Rows[0]["Priority"].ToString());
                dtmLastEntryDate = System.DateTime.Parse(rsPost.Rows[0]["Last_entry_date"].ToString());
                if (((System.DateTime.Parse(Session["dtmLastVisit"].ToString()) < dtmLastEntryDate)
                            && (Request.Cookies[("RT" + Variables.Forum.lngTopicID)] == null)))
                {
                    Response.Cookies["RT"][("TID" + Variables.Forum.lngTopicID)] = 1.ToString();
                }

            }

            // If this is a top priority post across all forums then read in teh forum ID form the querystring and ingnore the real topic forum ID
            if ((Request.QueryString["PR"] == "3"))
            {
                Variables.Forum.intForumID = int.Parse(Request.QueryString["FID"]);
            }

            if ((FSDatabase.Utils.BDType == FSDatabase.Utils.TypeBd.SQLServer))
            {
                strSQL = ("Execute "
                            + (Variables.Forum.strDbProc + ("ForumsAllWhereForumIs @Variables.Forum.intForumID = " + Variables.Forum.intForumID)));
            }
            else
            {
                strSQL = ("SELECT "
                            + (Variables.Forum.strDbTable + ("Forum.* FROM "
                            + (Variables.Forum.strDbTable + ("Forum WHERE "
                            + (Variables.Forum.strDbTable + ("Forum.Forum_ID = "
                            + (Variables.Forum.intForumID + ";"))))))));
            }

            // Query the database
            rsTopic = db.Execute(strSQL);
            // If there is a record returned by the recordset then check to see if you need a clave to enter it
            if ((rsTopic.Rows.Count > 0))
            {
                // Read in forum details from the database
                strForumName = rsTopic.Rows[0]["Forum_name"].ToString();
                // Read in wether the forum is locked or not
                Variables.Forum.blnForumLocked = bool.Parse(rsTopic.Rows[0]["Locked"].ToString());
                FuncionesForum.forumPermisisons(Variables.Forum.intForumID, FSPortal.Variables.User.GroupId, int.Parse(rsTopic.Rows[0]["Read"].ToString()), int.Parse(rsTopic.Rows[0]["Post"].ToString()), int.Parse(rsTopic.Rows[0]["Reply_posts"].ToString()), int.Parse(rsTopic.Rows[0]["Edit_posts"].ToString()), int.Parse(rsTopic.Rows[0]["Delete_posts"].ToString()), 0, int.Parse(rsTopic.Rows[0]["Poll_create"].ToString()), int.Parse(rsTopic.Rows[0]["Vote"].ToString()), int.Parse(rsTopic.Rows[0]["Attachments"].ToString()), int.Parse(rsTopic.Rows[0]["Image_upload"].ToString()));
                // If the user has no read writes then kick them
                if ((Variables.Forum.blnRead == false))
                {
                    // Redirect to a page asking for the user to enter the forum clave
                    Response.Redirect("insufficient_permission.aspx");
                }

                // If the forum requires a clave and a logged in forum code is not found on the users machine then send them to a login page
                if (((rsTopic.Rows[0]["clave"].ToString() != "")
                            && (Web.Cookie(Request.Cookies[FSPortal.Variables.App.strCookieName], ("Forum" + Variables.Forum.intForumID)) != rsTopic.Rows[0]["Forum_code"].ToString())))
                {
                    // Redirect to a page asking for the user to enter the forum clave
                    Response.Redirect(("forum_clave_form.aspx?RP=PT&FID="
                                    + (Variables.Forum.intForumID + ("&tID=" + Variables.Forum.lngTopicID))));
                }

            }

            tituloPagina = Variables.Forum.strMainForumName + ": " + strSubject;
            // Write the HTML head of the page
            sb.AppendLine("<script language=\"JavaScript\">" + "\r\n" + "function CheckForm() {" + "\r\n" + "\tvar errorMsg = \'\';");
            // If Gecko Madis API (RTE) need to strip default input from the API
            if ((FuncionesForum.RTEenabled() == "Gecko"))
            {
                sb.AppendLine(("\r\n" + ("\t//For Gecko Madis API (RTE)" + ("\r\n" + ("\tif (document.frmAddMessage.message.value.indexOf(\'<br />\') > -1 && document.frmAddMessage.message.va" +
                    "lue.length == 8) document.frmAddMessage.message.value = \'\';" + "\r\n")))));
            }

            // If this is a guest posting check that they have entered their name
            if ((Variables.Forum.blnPost
                        && (FSPortal.Variables.User.UsuarioId == 2)))
            {
                sb.AppendLine(("\r\n" + ("\t//Check for a name" + ("\r\n" + ("\tif (document.frmAddMessage.Gname.value==\'\'){" + ("\r\n" + ("\t\terrorMsg += \'\\n\\t"
                                + (Variables.Forum.strTxtNoNameError + ("\';" + ("\r\n" + "\t}"))))))))));
            }

            sb.AppendLine(("\r\n" + ("\r\n" + ("\t//Check for message" + ("\r\n" + ("\tif (document.frmAddMessage.message.value==\'\'){" + ("\r\n" + ("\t\terrorMsg += \'\\n\\t"
                            + (Variables.Forum.strTxtNoMessageError + ("\';" + ("\r\n" + ("\t}" + ("\r\n" + ("" + ("\r\n" + ("\t//If there is aproblem with the form then display an error" + ("\r\n" + ("\tif (errorMsg != \'\'){" + ("\r\n" + ("\t\tmsg = \'"
                            + (Variables.Forum.strTxtErrorDisplayLine + ("\\n\\n\';" + ("\r\n" + ("\t\tmsg += \'"
                            + (Variables.Forum.strTxtErrorDisplayLine1 + ("\\n\';" + ("\r\n" + ("\t\tmsg += \'"
                            + (Variables.Forum.strTxtErrorDisplayLine2 + ("\\n\';" + ("\r\n" + ("\t\tmsg += \'"
                            + (Variables.Forum.strTxtErrorDisplayLine + ("\\n\\n\';" + ("\r\n" + ("\t\tmsg += \'"
                            + (Variables.Forum.strTxtErrorDisplayLine3 + ("\\n\';" + ("\r\n" + ("" + ("\r\n" + ("\t\terrorMsg += alert(msg + errorMsg + \'\\n\\n\');" + ("\r\n" + ("\t\treturn false;" + ("\r\n" + ("\t}" + ("\r\n" + ("" + ("\r\n" + ("\t//Reset the submition action" + ("\r\n" + ("\tdocument.frmAddMessage.action = \'post_message.aspx?PN="
                            + (Variables.Forum.intTopicPageNumber + ("\'" + ("\r\n" + "\tdocument.frmAddMessage.target = \'_self\';")))))))))))))))))))))))))))))))))))))))))))))))))))))));
            if (((FuncionesForum.RTEenabled() != "false")
                        && (Variables.Forum.blnRTEEditor && Variables.Forum.blnWYSIWYGEditor)))
            {
                sb.AppendLine(("\r\n" + "\tdocument.frmAddMessage.Submit.disabled=true;"));
            }

            sb.AppendLine(("\r\n" + ("\treturn true;" + ("\r\n" + ("}" + ("\r\n" + "</script>"))))));
            string sTituloBarra;
            sTituloBarra = ("/ :: <a href=\"../default.aspx\">inicio</a> :: <a href=\'default.aspx\'>foro</a> :: " + ("<a href=\"forum_topics.aspx?FID="
                        + (Variables.Forum.intForumID + ("&PN="
                        + (Variables.Forum.intTopicPageNumber + ("\">"
                        + (strForumName + ("</a>" + (" :: "
                        + (Variables.Forum.strTxtTopic + (": " + strSubject)))))))))));
            sb.AppendLine(@"<navigation:navigation ID=""common1"" runat=""server"" />");
            sb.AppendLine(("\r\n"
                                    + (sTituloBarra + ("\r\n" + ("<table width=\""
                                    + (Variables.Forum.strTableVariableWidth + ("\" border=\"0\" cellspacing=\"0\" cellpadding=\"3\" align=\"center\"><tr>" + ("\r\n" + "  <td align=\"left\" class=\"heading\">"))))))));
            // Display the forum name
            // sb.AppendLine(strForumName)
            // If the forum is locked show a locked pad lock icon
            if ((Variables.Forum.blnForumLocked == true))
            {
                sb.AppendLine(("  <span class=\"smText\">(<img src=\""
                                + (Variables.Forum.strImagePath + ("forum_locked_icon.gif\" align=\"baseline\" alt=\""
                                + (Variables.Forum.strTxtForumLocked + ("\"> "
                                + (Variables.Forum.strTxtForumLocked + ")</span>")))))));
            }

            sb.AppendLine(("</td>" + ("\r\n" + ("</tr>" + ("\r\n" + (" <tr>" + ("\r\n" + "  <td align=\"left\" width=\"95%\" class=\"bold\">")))))));
            // <img src=""" & Variables.Forum.strImagePath & "open_folder_icon.gif"" border=""0"" align=""middle"" /> <a href=""default.aspx"" target=""_self"" class=""boldLink"">" & strMainForumName & "</a>")
            // sb.AppendLine(strNavSpacer)
            // Check there are forum's to display
            if ((rsTopic.Rows.Count == 0))
            {
                // If there are no forum's to display then display the appropriate error message
                sb.AppendLine(("<span class=\"bold\">"
                                + (Variables.Forum.strTxtNoForums + "</span>")));
                // Else there the are forum's to write the HTML to display it the forum names and a discription
            }
            else
            {
                // Write the HTML of the forum descriptions as hyperlinks to the forums
                // sb.Append ("<a href=""forum_topics.aspx?FID=" & Variables.Forum.intForumID & "&PN=" & intTopicPageNumber & """ target=""_self"" class=""boldLink"">" & strForumName & "</a>")
            }

            sb.AppendLine(("\r\n" + ("</td>" + ("\r\n" + "  <td align=\"right\" width=\"5%\">"))));
            // If the user is the forum admin or a moderator then give them admin functions on this topic
            if ((FSPortal.Variables.User.Administrador || Variables.Forum.blnModerator))
            {
                sb.AppendLine(("\r\n" + ("     <a href=\"javascript:openWin(\'pop_up_topic_admin.aspx?TID="
                                + (Variables.Forum.lngTopicID + ("\',\'admin\',\'toolbar=0,location=0,status=0,menubar=0,scrollbars=1,resizable=1,width=590,height=425\')\">[" +
                                ""
                                + (Variables.Forum.strTxtTopicAdmin + "]</a>"))))));
            }

            sb.AppendLine(("\r\n" + ("</td>" + ("\r\n" + (" </tr>" + ("\r\n" + ("</table>" + ("\r\n" + ("      <table width=\""
                            + (Variables.Forum.strTableVariableWidth + ("\" border=\"0\" cellspacing=\"0\" cellpadding=\"4\" align=\"center\">" + ("\r\n" + ("        <tr>" + ("\r\n" + ("         <td width=\"70%\"><span class=\"lgText\"><img src=\""
                            + (Variables.Forum.strImagePath + ("subject_folder.gif\" alt=\""
                            + (Variables.Forum.strTxtSubjectFolder + ("\" align=\"middle\" /> "
                            + (Variables.Forum.strTxtTopic + (": "
                            + (strSubject + "</span>"))))))))))))))))))))));
            // If the topic is locked then have a locked icon
            if ((Variables.Forum.blnTopicLocked == true))
            {
                sb.AppendLine(("  <span class=\"smText\">(<img src=\""
                                + (Variables.Forum.strImagePath + ("forum_locked_icon.gif\" align=\"baseline\" alt=\""
                                + (Variables.Forum.strTxtTopicLocked + ("\"> "
                                + (Variables.Forum.strTxtTopicLocked + ")</span>")))))));
            }

            sb.AppendLine(("\r\n" + "\t</td>"));
            // If the user can reply and they are not suspened then display real reply links
            if (((Variables.Forum.blnReply == true)
                        && (Variables.Forum.blnActiveMember == true)))
            {
                // If the reply box is on the same page shorten the reply link
                if ((Variables.Forum.intRecordPositionPageNum == intTotalNumOfPages))
                {
                    // Display images with links to reply to post or post a new topic
                    sb.AppendLine(("\r\n" + ("<td align=\"right\"><a href=\"#reply\" target=\"_self\">"
                                    + (Variables.Forum.strTxtPostReply + "</a>"))));
                }
                else
                {
                    // Display images with links to reply to post or post a new topic
                    sb.AppendLine(("\r\n" + ("<td align=\"right\"><a href=\"forum_posts.aspx?TID="
                                    + (Variables.Forum.lngTopicID + ("&PN="
                                    + (Variables.Forum.intTopicPageNumber + ("&TPN="
                                    + (intTotalNumOfPages + ("#reply\" target=\"_self\">"
                                    + (Variables.Forum.strTxtPostReply + "</a>"))))))))));
                }

                // Else the user can not reply
            }
            else
            {
                // Display images with links to reply to post or post a new topic but get redirected to a login screen if user is not logged in
                sb.AppendLine(("\r\n" + ("<td align=\"right\"><a href=\"edit_post.aspx?M=R&FID="
                                + (Variables.Forum.intForumID + ("\"  target=\"_self\">["
                                + (Variables.Forum.strTxtPostReply + "]</a>"))))));
            }

            // Display new topic link
            sb.AppendLine(("<a href=\"post_message_form.aspx?FID="
                            + (Variables.Forum.intForumID + ("\"  target=\"_self\">["
                            + (Variables.Forum.strTxtNewTopic + "]</a>")))));
            // If the user can create a poll disply a create poll link
            if ((Variables.Forum.blnPollCreate == true))
            {
                sb.AppendLine(("<a href=\"poll_create_form.aspx?FID="
                                + (Variables.Forum.intForumID + ("\"  target=\"_self\">["
                                + (Variables.Forum.strTxtCreateNewPoll + "]</a>")))));
            }

            sb.AppendLine(("\r\n" + ("\t</td>" + ("\r\n" + ("        </tr>" + ("\r\n" + "      </table>"))))));
            // If there is a poll then display the poll include
            if ((Variables.Forum.lngPollID > 0))
            {
                sb.AppendLine(@"<poll_display:poll_display ID=""common2"" runat=""server"" />");
            }


            //If there are threads display them

            if (rsPost.Rows.Count > 0)
            {
                //Check to see if user is watching this topic or not
                if (Variables.Forum.blnEmail && FSPortal.Variables.User.GroupId != 2)
                {

                    //Initalise the SQL string with a query to see if this person is watching the topic
                    if (FSDatabase.Utils.BDType == FSDatabase.Utils.TypeBd.SQLServer)
                    {
                        strSQL = "Execute " + Variables.Forum.strDbProc + "TopicEmailNotify @lngUsuariosID = " + FSPortal.Variables.User.UsuarioId + ", @lngTopicID= " + Variables.Forum.lngTopicID;
                    }
                    else
                    {
                        strSQL = "SELECT " + Variables.Forum.strDbTable + "EmailNotify.*  ";
                        strSQL = strSQL + "FROM " + Variables.Forum.strDbTable + "EmailNotify ";
                        strSQL = strSQL + "WHERE " + Variables.Forum.strDbTable + "EmailNotify.UsuarioID=" + FSPortal.Variables.User.UsuarioId + " AND " + Variables.Forum.strDbTable + "EmailNotify.Topic_ID=" + Variables.Forum.lngTopicID + ";";
                    }

                    // Query the database
                    rsTopic = db.Execute(strSQL);
                    // If a record is return the user is watching this topic
                    if ((rsTopic.Rows.Count == 0))
                    {
                        blnTopicWatched = true;
                        Variables.Forum.blnReplyNotify = true;
                    }

                }


                sb.AppendLine("<table width=\""
                                    + Variables.Forum.strTableVariableWidth + "\" border=\"0\" cellspacing=\"0\" cellpadding=\"1\" bgcolor=\""
                                    + Variables.Forum.strTablePostsBorderColour + "\" align=\"center\">" + "\r\n" + " <tr>" + "\r\n" + "  <td>" + "\r\n" + "  <table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" bgcolor=\""
                                    + Variables.Forum.strTablePostsBgColour + "\">" + "\r\n" + "    <tr>" + "\r\n" + "     <td bgcolor=\""
                                    + Variables.Forum.strTablePostsBgColour + "\">" + "\r\n" + "      <table width=\"100%\" border=\"0\" cellspacing=\"1\" cellpadding=\"3\">" + "\r\n" + "       <tr>" + "\r\n" + "        <td bgcolor=\""
                                    + Variables.Forum.strTablePostsTitleColour + "\" width=\"140\" class=\"tHeading\" background=\""
                                    + Variables.Forum.strTablePostsTitleBgImage + "\" nowrap=\"nowrap\">"
                                    + Variables.Forum.strTxtUsuarios + "</td>" + "\r\n" + "        <td bgcolor=\""
                                    + Variables.Forum.strTablePostsTitleColour + "\" width=\"82%\" class=\"tHeading\" background=\""
                                    + Variables.Forum.strTablePostsTitleBgImage + "\" nowrap=\"nowrap\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">" + "\r\n" + "          <tr>" + "\r\n" + "           <td width=\"20%\" class=\"tHeading\">"
                                    + Variables.Forum.strTxtMessage + "</td>" + "\r\n" + "           <td width=\"80%\" align=\"right\" nowrap=\"nowrap\"><a href=\"get_topic.aspx?FID="
                                    + Variables.Forum.intForumID + "&tID="
                                    + Variables.Forum.lngTopicID + "&dIR=P\" target=\"_self\" class=\"npLink\"><< "
                                    + Variables.Forum.strTxtPrevTopic + "</a> | <a href=\"get_topic.aspx?FID="
                                    + Variables.Forum.intForumID + "&tID="
                                    + Variables.Forum.lngTopicID + "&dIR=N\" target=\"_self\" class=\"npLink\">"
                                    + Variables.Forum.strTxtNextTopic + " >></a></td>" + "\r\n" + "          </tr>" + "\r\n" + "         </table></td>" + "\r\n" + "         </tr>");

                // Calculate the post number
                intThreadNo = ((Variables.Forum.intRecordPositionPageNum - 1)
                            * Variables.Forum.intThreadsPerPage);
                // Loop round to read in all the thread's in the database
                // For intRecordLoopCounter = 1 To Variables.Forum.intThreadsPerPage
                intRecordLoopCounter = 1;
                foreach (DataRow row in rsPost.Rows)
                {
                    // Initilise moderator variable
                    //blnIsModerator = false;
                    intThreadNo = (intThreadNo + 1);
                    // Read in threads details for the topic from the database
                    lngMessageID = long.Parse(row["Thread_ID"].ToString());
                    strMessage = row["Message"].ToString();
                    strusuario = row["UsuarioID"].ToString();
                    lngUserID = long.Parse(row["UsuarioID"].ToString());
                    dtmTopicDate = System.DateTime.Parse(row["Message_date"].ToString());
                    strUsuariosHomepage = ""; //row["Homepage"].ToString();
                    strUsuariosLocation = ""; //row["Location"].ToString();
                    dtmUsuariosRegistration = System.DateTime.Now; //System.DateTime.Parse(row["FechaCreacion"].ToString());
                    lngUsuariosNumOfPosts = 0;  //long.Parse(row["No_of_posts"].ToString());
                    strUsuariosAvatar = ""; //row["Avatar"].ToString();
                    strMemberTitle = ""; //row["Avatar_title"].ToString();
                    strUsuariosSignature = ""; //row["Signature"].ToString();
                    strUsuariosIP = ""; //row["IP_addr"].ToString();
                    strGroupName = ""; //row["Name"].ToString();
                    intRankStars = 0; //int.Parse(row["Stars"].ToString());
                    strRankCustomStars = ""; //row["Custom_stars"].ToString();
                                             // If the poster is a guest see if they have entered their name in the GuestName table and get it
                    if ((lngUserID == 2))
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
                        rsTopic = db.Execute(strSQL);
                        // Read in the guest posters name
                        if ((rsTopic.Rows.Count > 0))
                        {
                            strusuario = rsTopic.Rows[0]["Name"].ToString();
                        }
                    }

                    // If the post contains a quote or code block then format it
                    if ((((strMessage.IndexOf("[QUOTE=") + 1)
                                > 0)
                                && ((strMessage.IndexOf("[/QUOTE]") + 1)
                                > 0)))
                    {
                        strMessage = FuncionesForum.formatUserQuote(strMessage);
                    }

                    if ((((strMessage.IndexOf("[QUOTE]") + 1)
                                > 0)
                                && ((strMessage.IndexOf("[/QUOTE]") + 1)
                                > 0)))
                    {
                        strMessage = FuncionesForum.formatQuote(strMessage);
                    }

                    if ((((strMessage.IndexOf("[CODE]") + 1)
                                > 0)
                                && ((strMessage.IndexOf("[/CODE]") + 1)
                                > 0)))
                    {
                        strMessage = FuncionesForum.formatCode(strMessage);
                    }

                    // If the post contains a flash link then format it
                    if (Variables.Forum.blnFlashFiles)
                    {
                        if ((((strMessage.IndexOf("[FLASH") + 1)
                                    > 0)
                                    && ((strMessage.IndexOf("[/FLASH]") + 1)
                                    > 0)))
                        {
                            strMessage = FuncionesForum.formatFlash(strMessage);
                        }

                        if ((((strUsuariosSignature.IndexOf("[FLASH") + 1)
                                    > 0)
                                    && ((strUsuariosSignature.IndexOf("[/FLASH]") + 1)
                                    > 0)))
                        {
                            strUsuariosSignature = FuncionesForum.formatFlash(strUsuariosSignature);
                        }
                    }

                    // If the message has been edited parse the 'edited by' XML into HTML for the post
                    if (((strMessage.IndexOf("<edited>") + 1)
                                > 0))
                    {
                        strMessage = FuncionesForum.editedXMLParser(strMessage);
                    }

                    // Call the function to highlight search words if coming froma search page
                    if ((strSearchKeywords != ""))
                    {
                        strMessage = FuncionesFilter.searchHighlighter(strMessage, sarySearchWord);
                    }

                    // If the user wants there signature shown then attach it to the message
                    if ((Functions.ValorBool(row["Show_signature"])
                                && (strUsuariosSignature != "")))
                    {
                        strMessage = (strMessage + ("<!-- Signature --><br /><br />__________________<br />"
                                    + (strUsuariosSignature + "<!-- Signature -->")));
                    }

                    sb.AppendLine(("\r\n" + ("      <tr>" + ("\r\n" + ("        <td valign=\"top\" background=\""
                                    + (Variables.Forum.strTablePostsBgImage + "\" bgcolor=\""))))));
                    if (((intRecordLoopCounter % 2)
                                == 0))
                    {
                        sb.AppendLine(Variables.Forum.strTablePostsSideEvenRowColour);
                    }
                    else
                    {
                        sb.AppendLine(Variables.Forum.strTablePostsSideOddRowColour);
                    }

                    sb.AppendLine(("\" class=\"smText\">" + ("\r\n" + ("         <a name=\""
                                    + (lngMessageID + ("\"></a>" + ("\r\n" + ("         <span class=\"bold\">"
                                    + (strusuario + ("</span><br />"
                                    + (strGroupName + ("<br />" + ("\r\n" + "         <img src=\"")))))))))))));
                    if ((strRankCustomStars != ""))
                    {
                        sb.AppendLine(strRankCustomStars);
                    }
                    else
                    {
                        sb.AppendLine((Variables.Forum.strImagePath
                                        + (intRankStars + "_star_rating.gif")));
                    }

                    sb.AppendLine(("\" alt=\""
                                    + (strGroupName + "\"><br />")));
                    // If the user has an avatar then display it
                    if (((Variables.Forum.blnAvatar == true)
                                && (strUsuariosAvatar != "")))
                    {
                        sb.AppendLine(("<img src=\""
                                        + (strUsuariosAvatar + ("\" width=\""
                                        + (Variables.Forum.intAvatarWidth + ("\" height=\""
                                        + (Variables.Forum.intAvatarHeight + ("\" alt=\""
                                        + (Variables.Forum.strTxtAvatar + ("\" vspace=\"5\" OnError=\"this.src=\'"
                                        + (Variables.Forum.strImagePath + "blank.gif\', height=\'0\';\">")))))))))));
                    }

                    // If there is a title for this member then display it
                    if ((strMemberTitle != ""))
                    {
                        sb.AppendLine(("\r\n" + ("<br />" + strMemberTitle)));
                    }

                    sb.AppendLine(("<br /><br />"
                                    + (Variables.Forum.strTxtJoined + (": " + FuncionesFecha.DateFormat(dtmUsuariosRegistration, FuncionesFecha.saryDateTimeData)))));
                    // If the is a location display it
                    if ((strUsuariosLocation != ""))
                    {
                        sb.AppendLine(("<br />"
                                        + (Variables.Forum.strTxtLocation + (": " + strUsuariosLocation))));
                    }

                    // If active users is enabled see if the user is online
                    //  ''If Variables.Forum.blnActiveUsers Then
                    //  ''    'Display if the user is online
                    //  ''    blnIsUserOnline = False
                    //  ''    'Loop through the active users array
                    //  ''    For intArrayPass = 1 To UBound(saryActiveUsers, 2)
                    //  ''        If saryActiveUsers(1, intArrayPass) = lngUserID Then blnIsUserOnline = True
                    //  ''    Next
                    //  ''    'Display if the user is online
                    //  ''    If blnIsUserOnline Then sb.AppendLine(vbCrLf & "<br />" & Variables.Forum.strTxtOnlineStatus & ": " & Variables.Forum.strTxtOnLine2) Else sb.AppendLine(vbCrLf & "<br />" & Variables.Forum.strTxtOnlineStatus & ": " & Variables.Forum.strTxtOffLine)
                    //  ''End If
                    // Display the num of posts
                    sb.AppendLine(("<br />"
                                    + (Variables.Forum.strTxtPosts + (": " + lngUsuariosNumOfPosts))));
                    // Create the table for the main post
                    sb.AppendLine(("\r\n" + "         </td>"));
                    sb.AppendLine(("\r\n" + ("        <td valign=\"top\" background=\""
                                    + (Variables.Forum.strTablePostsBgImage + "\" bgcolor=\""))));
                    if (((intRecordLoopCounter % 2)
                                == 0))
                    {
                        sb.AppendLine(Variables.Forum.strTablePostsEvenRowColour);
                    }
                    else
                    {
                        sb.AppendLine(Variables.Forum.strTablePostsOddRowColour);
                    }

                    sb.AppendLine("\" class=\"text\">");
                    sb.AppendLine(("\r\n" + "         <table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">"));
                    sb.AppendLine(("\r\n" + "          <tr>"));
                    sb.AppendLine(("\r\n" + ("           <td width=\"80%\" class=\"smText\">"
                                    + (Variables.Forum.strTxtPosted + (" "
                                    + (FuncionesFecha.DateFormat(dtmTopicDate, FuncionesFecha.saryDateTimeData) + (" "
                                    + (Variables.Forum.strTxtAt + (" " + FuncionesFecha.TimeFormat(dtmTopicDate, FuncionesFecha.saryDateTimeData))))))))));
                    // If the user is the admin or moderatir then display the Usuarioss IP
                    if (((FSPortal.Variables.User.Administrador || Variables.Forum.blnModerator)
                                && (strUsuariosIP != "")))
                    {
                        sb.AppendLine((" | "
                                        + (Variables.Forum.strTxtIP + (" <a href=\"javascript:openWin(\'pop_up_IP_blocking.aspx?IP="
                                        + (strUsuariosIP + ("&tID="
                                        + (Variables.Forum.lngTopicID + ("\',\'move\',\'toolbar=0,location=0,status=0,menubar=0,scrollbars=1,resizable=1,width=425,height=425\')\" cl" +
                                        "ass=\"smLink\">"
                                        + (strUsuariosIP + "</a>")))))))));
                    }
                    else
                    {
                        sb.AppendLine((" | " + Variables.Forum.strTxtIPLogged));
                    }

                    sb.AppendLine(("\r\n" + "\t\t    </td>"));
                    sb.AppendLine(("\r\n" + "                    <td width=\"20%\" align=\"right\" nowrap=\"nowrap\">"));
                    // Display the report post feature if email is enabled.
                    if ((Variables.Forum.blnEmail
                                && ((FSPortal.Variables.User.GroupId != 2)
                                && (lngUserID != FSPortal.Variables.User.UsuarioId))))
                    {
                        sb.AppendLine(("\r\n" + ("<a href=\"report_post.aspx?PID="
                                        + (lngMessageID + ("&FID="
                                        + (Variables.Forum.intForumID + ("&tID="
                                        + (Variables.Forum.lngTopicID + ("&TPN="
                                        + (Variables.Forum.intRecordPositionPageNum + ("\" target=\"_self\" class=\"smLink\">["
                                        + (Variables.Forum.strTxtReportPost + "]</a>"))))))))))));
                    }

                    // If the topic is not locked put in a link for someone to quote this message
                    if (((Variables.Forum.blnTopicLocked == false)
                                && (blnPollNoReply == false)))
                    {
                        sb.AppendLine(("\r\n" + ("            \t\t <a href=\"edit_post.aspx?M=Q&pID="
                                        + (lngMessageID + ("&TPN="
                                        + (intTotalNumOfPages + ("\">["
                                        + (Variables.Forum.strTxtQuote + (" "
                                        + (strusuario + "]</a>"))))))))));
                        // Else put in a non breakin space for netscape 4 bug
                    }
                    else
                    {
                        sb.AppendLine(" ");
                    }

                    sb.AppendLine(("\r\n" + ("          </td>" + ("\r\n" + ("          </tr>" + ("\r\n" + ("          <tr>" + ("\r\n" + ("           <td colspan=\"2\"><hr /></td>" + ("\r\n" + ("          </tr>" + ("\r\n" + ("         </table>" + ("\r\n" + ("<!-- Message body -->" + ("\r\n"
                                    + (strMessage + ("\r\n" + ("<!-- Message body \'\'\"\" -->" + ("\r\n" + ("        </td>" + ("\r\n" + ("       </tr>" + ("\r\n" + ("       <tr>" + ("\r\n" + "        <td bgcolor=\""))))))))))))))))))))))))));
                    if (((intRecordLoopCounter % 2)
                                == 0))
                    {
                        sb.AppendLine(Variables.Forum.strTablePostsSideEvenRowColour);
                    }
                    else
                    {
                        sb.AppendLine(Variables.Forum.strTablePostsSideOddRowColour);
                    }

                    sb.AppendLine(("\" background=\""
                                    + (Variables.Forum.strTablePostsBgImage + ("\"><a href=\"#top\" target=\"_self\" class=\"npLink\">"
                                    + (Variables.Forum.strTxtBackToTop + "</a></td>")))));
                    sb.AppendLine(("\r\n" + "        <td bgcolor=\""));
                    if (((intRecordLoopCounter % 2)
                                == 0))
                    {
                        sb.AppendLine(Variables.Forum.strTablePostsEvenRowColour);
                    }
                    else
                    {
                        sb.AppendLine(Variables.Forum.strTablePostsOddRowColour);
                    }

                    sb.AppendLine(("\" background=\""
                                    + (Variables.Forum.strTablePostsBgImage + ("\" class=\"text\"><a href=\"JavaScript:openWin(\'pop_up_profile.aspx?PF="
                                    + (lngUserID + ("&FID="
                                    + (Variables.Forum.intForumID + ("\',\'profile\',\'toolbar=0,location=0,status=0,menubar=0,scrollbars=1,resizable=1,width=590,height=425\')\"" +
                                    ">["
                                    + (Variables.Forum.strTxtView + (" "
                                    + (strusuario + (" "
                                    + (Variables.Forum.strTxtProfile + "]</a>")))))))))))));
                    sb.AppendLine(("\r\n" + ("         <a href=\"search_form.aspx?KW="
                                    + (Server.UrlEncode(strusuario) + ("&sI=AR&fM="
                                    + (Variables.Forum.intForumID + ("&FID="
                                    + (Variables.Forum.intForumID + ("&tID="
                                    + (Variables.Forum.lngTopicID + ("&PN="
                                    + (Variables.Forum.intTopicPageNumber + ("&TPN="
                                    + (Variables.Forum.intRecordPositionPageNum + ("\">["
                                    + (Variables.Forum.strTxtSearchForPosts + (" "
                                    + (strusuario + "]</a>"))))))))))))))))));
                    // If the user has a hompeage put in a link button
                    if ((strUsuariosHomepage != ""))
                    {
                        sb.AppendLine(("\r\n" + ("         <a href=\""
                                        + (strUsuariosHomepage + ("\" target=\"_blank\">["
                                        + (Variables.Forum.strTxtVisit + (" "
                                        + (Variables.Forum.strTxtHomepage + (" de "
                                        + (strusuario + "]</a>"))))))))));
                    }

                    // If the private msg's are on then display a link to enable use to send them a msg
                    if ((Variables.Forum.blnPrivateMessages
                                && (FSPortal.Variables.User.GroupId != 2)))
                    {
                        sb.AppendLine(("\r\n" + ("         <a href=\"pm_new_message_form.aspx?name="
                                        + (Server.UrlEncode(strusuario) + ("\" target=\"_self\">["
                                        + (Variables.Forum.strTxtSendPrivateMessage + "]</a>"))))));
                        sb.AppendLine(("\r\n" + ("         <a href=\"pm_buddy_list.aspx?name="
                                        + (Server.UrlEncode(strusuario) + ("\" target=\"_self\">["
                                        + (Variables.Forum.strTxtAddToBuddyList + "]</a>"))))));
                    }

                    // If the logged in user is the person who posted the message or the forum administrator/moderator then allow them to edit or delete the message
                    if ((((FSPortal.Variables.User.UsuarioId == lngUserID)
                                && ((Variables.Forum.blnForumLocked == false)
                                && (Variables.Forum.blnActiveMember
                                && (Variables.Forum.blnTopicLocked == false))))
                                || (FSPortal.Variables.User.Administrador || Variables.Forum.blnModerator)))
                    {
                        // Only let the user edit the post if they have edit rights
                        if ((Variables.Forum.blnEdit || FSPortal.Variables.User.Administrador))
                        {
                            sb.AppendLine(("\r\n" + ("         <a href=\"edit_post.aspx?PID="
                                            + (lngMessageID + ("&PN="
                                            + (Variables.Forum.intTopicPageNumber + ("&TPN="
                                            + (Variables.Forum.intRecordPositionPageNum + ("\" target=\"_self\">["
                                            + (Variables.Forum.strTxtEditPost + "]</a>"))))))))));
                        }

                        // Only let a normal user delete there post if someone hasn't posted a reply
                        if (((intTotalNumOfThreads == intThreadNo)
                                    || (FSPortal.Variables.User.Administrador || Variables.Forum.blnModerator)))
                        {
                            // Only let the user delete the post if they have delete rights
                            if ((Variables.Forum.blnDelete || FSPortal.Variables.User.Administrador))
                            {
                                sb.AppendLine(("\r\n" + ("         <a href=\"delete_post.aspx?PID="
                                                + (lngMessageID + ("&PN="
                                                + (Variables.Forum.intTopicPageNumber + ("&TPN="
                                                + (Variables.Forum.intRecordPositionPageNum + ("\" target=\"_self\" OnClick=\"return confirm(\'"
                                                + (Variables.Forum.strTxtDeletePostAlert + ("\')\">["
                                                + (Variables.Forum.strTxtDeletePost + "]</a>"))))))))))));
                            }

                        }
                    }

                    // If the user is the forum admin or a moderator then let them move the topic to another forum
                    if ((FSPortal.Variables.User.Administrador || Variables.Forum.blnModerator))
                    {
                        sb.AppendLine(("\r\n" + ("         <a href=\"javascript:openWin(\'move_post_form.aspx?PID="
                                        + (lngMessageID + ("\',\'move\',\'toolbar=0,location=0,status=0,menubar=0,scrollbars=1,resizable=1,width=590,height=425\')\">["
                                        + (Variables.Forum.strTxtMovePost + "]</a>"))))));
                    }

                    sb.AppendLine(("\r\n" + ("        </td>" + ("\r\n" + ("       </tr>" + ("\r\n" + ("       </tr>" + ("\r\n" + "       <tr>"))))))));
                    sb.AppendLine(("\r\n" + ("        <td colspan=\"2\" align=\"center\" background=\""
                                    + (Variables.Forum.strTablePostsBgImage + ("\" bgcolor=\""
                                    + (Variables.Forum.strTablePostsSeporatorColour + "\"><span style=\"font-size: 2px;\"> </span></td>"))))));
                    // Move to the next database record
                    // rsPost.MoveNext()
                    // blnExit = Not rsPost.Read()
                    intRecordLoopCounter = (intRecordLoopCounter + 1);
                }
            }

            sb.AppendLine(("     </tr>" + ("\r\n" + ("    </table></td>" + ("\r\n" + ("   </tr>" + ("\r\n" + ("  </table>" + ("\r\n" + (" </td></tr>" + ("\r\n" + ("</table>" + ("\r\n" + ("<br />" + ("\r\n" + "<div align=\"center\">")))))))))))))));
            // Set up an achor
            sb.AppendLine("<a name=\"reply\"></a>");
            // Display a message if the users IP is banned
            if (blnBannedIP)
            {
                sb.AppendLine(("\r\n" + ("\t<p class=\"text\">"
                                + (Variables.Forum.strTxtSorryYouDoNotHavePerimssionToReplyIPBanned + "</p>"))));
                // Display message if the users forum membership is suspended
            }
            else if (((Variables.Forum.blnActiveMember == false)
                        && (Variables.Forum.intRecordPositionPageNum == intTotalNumOfPages)))
            {
                sb.AppendLine(("\r\n" + ("\t<p class=\"text\">"
                                + (Variables.Forum.strTxtSorryNoReply + "<br />"))));
                // If mem suspended display message
                if (((Variables.Forum.strLoggedInUserCode.IndexOf("N0act") + 1)
                            > 0))
                {
                    sb.AppendLine(Variables.Forum.strTxtForumMemberSuspended);
                    // Else account not yet active
                }
                else
                {
                    sb.AppendLine(Variables.Forum.strTxtForumMembershipNotAct);
                }

                // If email is on then place a re-send activation email link
                if ((((Variables.Forum.strLoggedInUserCode.IndexOf("N0act", 0, System.StringComparison.OrdinalIgnoreCase) + 1)
                            == 0)
                            && (Variables.Forum.blnEmailActivation && Variables.Forum.blnLoggedInUserEmail)))
                {
                    sb.AppendLine(("<br /><a href=\"JavaScript:openWin(\'resend_email_activation.aspx\',\'actMail\',\'toolbar=0,location=0,stat" +
                        "us=0,menubar=0,scrollbars=1,resizable=1,width=475,height=200\')\">"
                                    + (Variables.Forum.strTxtResendActivationEmail + "</a>")));
                }

                sb.AppendLine("</p>");
                // Display message if the forum is locked
            }
            else if ((Variables.Forum.blnForumLocked
                        && (Variables.Forum.intRecordPositionPageNum == intTotalNumOfPages)))
            {
                sb.AppendLine(("\r\n" + ("\t<p class=\"text\">"
                                + (Variables.Forum.strTxtSorryNoReply + ("<br />"
                                + (Variables.Forum.strTxtThisForumIsLocked + "</p>"))))));
                // Display message if the user does not have permisison to post in this forum
            }
            else if (((Variables.Forum.blnReply == false)
                        && (FSPortal.Variables.User.GroupId != 2)))
            {
                sb.AppendLine(("\r\n" + ("\t<p class=\"text\">"
                                + (Variables.Forum.strTxtSorryYouDoNotHavePerimssionToReplyToPostsInThisForum + "</p>"))));
                // Display message if the topic is locked
            }
            else if (((Variables.Forum.blnTopicLocked == true)
                        && (Variables.Forum.intRecordPositionPageNum == intTotalNumOfPages)))
            {
                sb.AppendLine(("\r\n" + ("\t<p class=\"text\">"
                                + (Variables.Forum.strTxtSorryNoReply + ("<br />"
                                + (Variables.Forum.strTxtThisTopicIsLocked + "</p>"))))));
                // Display message if this is a poll only
            }
            else if (blnPollNoReply)
            {
                sb.AppendLine(("\r\n" + ("\t<p class=\"text\">"
                                + (Variables.Forum.strTxtThisIsAPollOnlyYouCanNotReply + "</p>"))));
                // Display message if the user is a guest or not logged in
            }
            else if (((Variables.Forum.blnReply == false)
                        && (Variables.Forum.intRecordPositionPageNum == intTotalNumOfPages)))
            {
                // sb.AppendLine(vbCrLf & "    <p class=""text"">" &  Variables.Forum.strTxtPostAReplyRegister & " <a href=""login_user.aspx?FID=" & Variables.Forum.intForumID & """ target=""_self"">" & Variables.Forum.strTxtLoginSm & "</a><br />" & Variables.Forum.strTxtNeedToRegister & " <a href=""registration_rules.aspx?FID=" & Variables.Forum.intForumID & """ target=""_self"">" & Variables.Forum.strTxtSmRegister & "</a></p>")
                // Else disply the reply post box
            }
            else if ((Variables.Forum.intRecordPositionPageNum == intTotalNumOfPages))
            {
                sb.AppendLine(("\r\n" + ("\t<span class=\"heading\">"
                                + (Variables.Forum.strTxtPostReply + "</span><br />"))));
                // See if the users browser is RTE enabled
                if (((FuncionesForum.RTEenabled() != "false")
                            && (Variables.Forum.blnRTEEditor && Variables.Forum.blnWYSIWYGEditor)))
                {
                    // Open the message form for RTE enabled browsers
                    RTE_message_form_inc.strMode = strMode;
                    RTE_message_form_inc.strBuddyName = "";
                    RTE_message_form_inc.intTopicPriority = 0;
                    RTE_message_form_inc.strTopicSubject = "";
                    RTE_message_form_inc.RTEenabled = FuncionesForum.RTEenabled();
                    RTE_message_form_inc.lngMessageID = lngMessageID;
                    RTE_message_form_inc.intTotalNumOfThreads = intTotalNumOfThreads;
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
                    message_form_inc.intTotalNumOfThreads = intTotalNumOfThreads;
                    message_form_inc.strMode = strMode;
                    message_form_inc.strMessage = "";
                    //sb.AppendLine(@"<message_form:message_form ID=""message_form"" runat=""server"" />");
                    sb.AppendLine(message_form_inc.Render(Request.QueryString["M"], Request.Form["PN"]));
                }

            }

            sb.AppendLine(("\r\n" + ("      <table width=\""
                                    + (Variables.Forum.strTableVariableWidth + ("\" border=\"0\" cellspacing=\"0\" cellpadding=\"3\">" + ("\r\n" + "            <tr><form>"))))));
            // If there is more than 1 page of topics then dispaly drop down list to the other threads
            if ((intTotalNumOfPages > 1))
            {
                // Display an image link to the last topic
                sb.AppendLine(("\r\n" + "        <td colspan=\"3\" align=\"right\" class=\"text\" nowrap=\"nowrap\">"));
                // Display a prev link if previous pages are available
                if ((Variables.Forum.intRecordPositionPageNum > 1))
                {
                    if ((Request.QueryString["KW"] != ""))
                    {
                        sb.AppendLine(("<a href=\"forum_posts.aspx?TID="
                                        + (Variables.Forum.lngTopicID + ("&kW="
                                        + (Server.UrlEncode(Request.QueryString["KW"]) + ("&PN="
                                        + (Variables.Forum.intTopicPageNumber + ("&TPN="
                                        + ((Variables.Forum.intRecordPositionPageNum - 1) + ("\"><< "
                                        + (Variables.Forum.strTxtPrevious + "</a> ")))))))))));
                    }
                    else
                    {
                        sb.AppendLine(("<a href=\"forum_posts.aspx?TID="
                                        + (Variables.Forum.lngTopicID + ("&PN="
                                        + (Variables.Forum.intTopicPageNumber + ("&TPN="
                                        + ((Variables.Forum.intRecordPositionPageNum - 1) + ("\"><< "
                                        + (Variables.Forum.strTxtPrevious + "</a> ")))))))));
                    }

                }

                sb.AppendLine((Variables.Forum.strTxtPage + (" " + ("\r\n" + "         <select onChange=\"ForumJump(this)\" name=\"SelectTopicPage\">"))));
                // Loop round to display links to all the other pages
                for (intTopicPageLoopCounter = 1; (intTopicPageLoopCounter <= intTotalNumOfPages); intTopicPageLoopCounter++)
                {
                    // Display a link in the link list to the another threads pages
                    // If it's a search result page then place the highlighted keywords into the link
                    if ((Request.QueryString["KW"] != ""))
                    {
                        sb.AppendLine(("\r\n" + ("          <option value=\"forum_posts.aspx?TID="
                                        + (Variables.Forum.lngTopicID + ("&kW="
                                        + (Server.UrlEncode(Request.QueryString["KW"]) + ("&PN="
                                        + (Variables.Forum.intTopicPageNumber + ("&TPN="
                                        + (intTopicPageLoopCounter + "\""))))))))));
                    }
                    else
                    {
                        sb.AppendLine(("\r\n" + ("          <option value=\"forum_posts.aspx?TID="
                                        + (Variables.Forum.lngTopicID + ("&PN="
                                        + (Variables.Forum.intTopicPageNumber + ("&TPN="
                                        + (intTopicPageLoopCounter + "\""))))))));
                    }

                    // If this page number to display is the same as the page being displayed then make sure it's selected
                    if ((intTopicPageLoopCounter == Variables.Forum.intRecordPositionPageNum))
                    {
                        sb.AppendLine(" selected");
                    }

                    // Display the link page number
                    sb.AppendLine((">"
                                    + (intTopicPageLoopCounter + "</option>")));
                }

                // End the drop down list
                sb.AppendLine(("\r\n" + ("        </select> "
                                + (Variables.Forum.strTxtOf + (" " + intTotalNumOfPages)))));
                // Display a next link if needed
                if ((Variables.Forum.intRecordPositionPageNum != intTotalNumOfPages))
                {
                    if ((Request.QueryString["KW"] != ""))
                    {
                        sb.AppendLine((" <a href=\"forum_posts.aspx?TID="
                                        + (Variables.Forum.lngTopicID + ("&kW="
                                        + (Server.UrlEncode(Request.QueryString["KW"]) + ("&PN="
                                        + (Variables.Forum.intTopicPageNumber + ("&TPN="
                                        + ((Variables.Forum.intRecordPositionPageNum + 1) + ("\">"
                                        + (Variables.Forum.strTxtNext + " >></a>")))))))))));
                    }
                    else
                    {
                        sb.AppendLine((" <a href=\"forum_posts.aspx?TID="
                                        + (Variables.Forum.lngTopicID + ("&PN="
                                        + (Variables.Forum.intTopicPageNumber + ("&TPN="
                                        + ((Variables.Forum.intRecordPositionPageNum + 1) + ("\">"
                                        + (Variables.Forum.strTxtNext + " >></a>")))))))));
                    }

                }

                sb.AppendLine("</td>");
            }

            sb.AppendLine(("</form></tr>" + ("\r\n" + ("             <tr>" + ("\r\n" + "              <td>")))));
            // Display a link to watch or un-watch this topic if email notification is enabled
            if ((Variables.Forum.blnEmail
                        && (FSPortal.Variables.User.GroupId != 2)))
            {
                // Create link
                sb.AppendLine(("<a href=\"email_notify.aspx?FID="
                                + (Variables.Forum.intForumID + ("&tID="
                                + (Variables.Forum.lngTopicID + ("&PN="
                                + (Variables.Forum.intTopicPageNumber + ("&TPN="
                                + (intTotalNumOfPages + "\" target=\"_self\">")))))))));
                // If topic is watched allow unwatch link display
                if (blnTopicWatched)
                {
                    sb.AppendLine(Variables.Forum.strTxtUn);
                }

                // Display link to watch the topic
                sb.AppendLine((Variables.Forum.strTxtWatchThisTopic + "</a>"));
                // Display a non breaking space for Netscrape 4 bug
            }
            else
            {
                sb.AppendLine(" ");
            }

            sb.AppendLine(("\r\n" + ("             </td>" + ("\r\n" + "              <td align=\"right\">"))));
            // If the user is not suspened and can reply then have links to reply etc.
            if ((Variables.Forum.blnReply && Variables.Forum.blnActiveMember))
            {
                // Only show the post reply link button on pages without the reply box as some people seem to think this button should actually post the forum!!!
                if ((Variables.Forum.intRecordPositionPageNum != intTotalNumOfPages))
                {
                    sb.AppendLine(("\r\n" + ("<a href=\"forum_posts.aspx?TID="
                                    + (Variables.Forum.lngTopicID + ("&PN="
                                    + (Variables.Forum.intTopicPageNumber + ("&TPN="
                                    + (intTotalNumOfPages + ("#reply\" target=\"_self\">["
                                    + (Variables.Forum.strTxtPostReply + "]</a>"))))))))));
                }

                // Else the user is not logged
            }
            else
            {
                // Display images with links to reply to post or post a new topic but get redirected to a login screen if user is not logged in
                sb.AppendLine(("\r\n" + ("<a href=\"edit_post.aspx?M=R&FID="
                                + (Variables.Forum.intForumID + ("\"  target=\"_self\">["
                                + (Variables.Forum.strTxtPostReply + "]</a>"))))));
            }

            // Display new topic link
            sb.AppendLine(("<a href=\"post_message_form.aspx?FID="
                            + (Variables.Forum.intForumID + ("\"  target=\"_self\">["
                            + (Variables.Forum.strTxtNewTopic + "]</a>")))));
            // If the user can create a poll disply a create poll link
            if ((Variables.Forum.blnPollCreate == true))
            {
                sb.AppendLine(("<a href=\"poll_create_form.aspx?FID="
                                + (Variables.Forum.intForumID + ("\"  target=\"_self\">["
                                + (Variables.Forum.strTxtCreateNewPoll + "]</a>")))));
            }

            sb.AppendLine(("\r\n" + ("        </td>" + ("\r\n" + ("        </tr>" + ("\r\n" + ("         </table>" + ("\r\n" + (" <table width=\""
                            + (Variables.Forum.strTableVariableWidth + ("\" border=\"0\" cellspacing=\"0\" cellpadding=\"1\">" + ("\r\n" + ("  <tr>" + ("\r\n" + ("   <td width=\"30%\">" + ("\r\n" + ("    <table width=\"380\" border=\"0\" cellspacing=\"0\" cellpadding=\"2\">" + ("\r\n" + ("     <tr>" + ("\r\n" + ("      <td>" + ("\r\n" + ("      <table width=\"300\" border=\"0\" cellspacing=\"0\" cellpadding=\"1\" align=\"center\" bgcolor=\""
                            + (Variables.Forum.strTableBorderColour + ("\">" + ("\r\n" + ("       <tr>" + ("\r\n" + ("        <td>" + ("\r\n" + ("         <table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"4\" align=\"center\" bgcolor=\""
                            + (Variables.Forum.strTableColour + ("\" background=\""
                            + (Variables.Forum.strTableBgImage + ("\">" + ("\r\n" + ("          <tr>" + ("\r\n" + ("           <td align=\"center\"> <a href=\"JavaScript:openWin(\'printer_friendly_posts.aspx?TID="
                            + (Variables.Forum.lngTopicID + ("\',\'printer_friendly\',\'toolbar=0,location=0,status=0,menubar=1,scrollbars=1,resizable=1,width=640,heig" +
                            "ht=390\')\"><img src=\""
                            + (Variables.Forum.strImagePath + ("print_version.gif\" align=\"middle\" border=\"0\" alt=\""
                            + (Variables.Forum.strTxtPrintVersion + ("\"></a>" + ("\r\n" + ("           <a href=\"JavaScript:openWin(\'printer_friendly_posts.aspx?FID="
                            + (Variables.Forum.intForumID + ("&tID="
                            + (Variables.Forum.lngTopicID + ("\',\'printer_friendly\',\'toolbar=0,location=0,status=0,menubar=1,scrollbars=1,resizable=1,width=640,heig" +
                            "ht=390\')\" class=\"smLink\">"
                            + (Variables.Forum.strTxtPrintVersion + "</a>"))))))))))))))))))))))))))))))))))))))))))))))))))));
            // If the user has logged in then the Logged In and the e-mail is on then display a link to allow the user to e-mail topic to a friend
            if (((FSPortal.Variables.User.GroupId != 2)
                        && (Variables.Forum.blnEmail && Variables.Forum.blnActiveMember)))
            {
                sb.AppendLine(("          <a href=\"JavaScript:openWin(\'email_topic.aspx?TID="
                                + (Variables.Forum.lngTopicID + ("\',\'email_friend\',\'toolbar=0,location=0,status=0,menubar=0,scrollbars=0,resizable=1,width=440,height=4" +
                                "50\')\"><img src=\""
                                + (Variables.Forum.strImagePath + ("e-mail_topic.gif\" align=\"middle\" border=\"0\" alt=\""
                                + (Variables.Forum.strTxtEmailTopic + ("\"></a>" + ("\r\n" + ("         <a href=\"JavaScript:openWin(\'email_topic.aspx?TID="
                                + (Variables.Forum.lngTopicID + ("\',\'email_friend\',\'toolbar=0,location=0,status=0,menubar=0,scrollbars=0,resizable=1,width=440,height=4" +
                                "50\')\" class=\"smLink\">"
                                + (Variables.Forum.strTxtEmailTopic + "</a>")))))))))))));
            }

            sb.AppendLine(("     </td>" + ("\r\n" + ("        </tr>" + ("\r\n" + ("       </table>" + ("\r\n" + ("      </tr>" + ("\r\n" + "     </table><br /><form>")))))))));
            sb.AppendLine(@"<forum_jump:forum_jump ID=""forum_jump"" runat=""server"" />");
            sb.AppendLine(("</form></td>" + ("\r\n" + ("    </table></td>" + ("\r\n" + "   <td width=\"70%\" align=\"right\" class=\"smText\" nowrap=\"nowrap\">")))));
            sb.AppendLine(@"<forum_permissions:forum_permissions ID=""forum_permissions"" runat=""server"" />");
            sb.AppendLine(("</td>" + ("\r\n" + ("  </tr>" + ("\r\n" + (" </table>" + ("\r\n" + "  <br />")))))));
            // Clear server objects
            // Set rsTopic = Nothing
            // Set rsCommon = Nothing
            // adoCon.Close
            // Set adoCon = Nothing
            // Display the process time
            // If Variables.Forum.blnShowProcessTime Then sb.AppendLine("<span class=""smText""><br /><br />" & Variables.Forum.strTxtThisPageWasGeneratedIn & " " & FormatNumber(Timer() - Variables.Forum.dblStartTime, 4) & " " & Variables.Forum.strTxtSeconds & "</span>")
            sb.AppendLine("</div>");
            // Display an alert message if the user is watching this topic for email notification
            if ((Request.QueryString["EN"] == "TS"))
            {
                sb.AppendLine("<script language=\"JavaScript\">");
                sb.AppendLine(("alert(\'"
                                + (Variables.Forum.strTxtYouWillNowBeNotifiedOfAllReplies + "\');")));
                sb.AppendLine("</script>");
            }

            // Display an alert message if the user is not watching this topic for email notification
            if ((Request.QueryString["EN"] == "TU"))
            {
                sb.AppendLine("<script language=\"JavaScript\">");
                sb.AppendLine(("alert(\'"
                                + (Variables.Forum.strTxtYouWillNowNOTBeNotifiedOfAllReplies + "\');")));
                sb.AppendLine("</script>");
            }
            return sb.ToString();
        }

    }
}
