// <fileheader>
// <copyright file="forum_topics.aspx.cs" company="Febrer Software">
//     Fecha: 30/11/2007
//     Path: forum\forum_topics.aspx.cs
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
    public class forum_topics : FSPortal.BasePage
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
            // Set the response buffer to true as we maybe redirecting
            // Response.Buffer = True
            // Make sure this page is not cached
            // Response.Expires = -1
            // Response.ExpiresAbsolute = Now() - 2
            // Response.AddHeader "pragma","no-cache"
            // Response.AddHeader "cache-control","private"
            // Response.CacheControl = "No-Store"
            // Dimension variables
            FSDatabase.BdUtils db = new FSDatabase.BdUtils("FSForum");
            DataTable rsForum;
            // Holds the Recordset for the forum details
            DataTable rsTopic;
            // Holds the Recordset for the Topic details
            string strForumName = "";
            long lngNumberOfReplies = 0;
            // Holds the number of replies for a topic
            long lngTopicID = 0;
            // Holds the topic ID
            string strSubject;
            // Holds the topic subject
            string strTopicStartusuario = "";
            long lngTopicStartUserID = 0;
            // Holds the users Id number for the user who started the topic
            long lngNumberOfViews = 0;
            // Holds the number of views a topic has had
            long lngLastEntryMessageID = 0;
            // Holds the message ID of the last entry
            string strLastEntryusuario = "";
            long lngLastEntryUserID = 0;
            // Holds the user's ID number of the last person to post a meassge in a topic
            System.DateTime dtmLastEntryDate = new System.DateTime();
            // Holds the date the last person made a post in the topic
            int intRecordPositionPageNum;
            // Holds the recorset page number to show the topics for
            int intTotalNumOfPages = 0;
            // Holds the total number of pages in the recordset
            int intRecordLoopCounter = 0;
            // Holds the loop counter numeber
            int intTopicPageLoopCounter;
            // Holds the number of pages there are in the forum
            int intShowTopicsFrom;
            // Holds when to show the topics from
            string strShowTopicsFrom = "";
            bool blnForumLocked = false;
            // Set to true if the forum is locked
            bool blnTopicLocked;
            // set to true if the topic is locked
            int intPriority = 0;
            // Holds the priority level of the topic
            int intNumberOfTopicPages;
            // Holds the number of topic pages
            int intTopicPagesLoopCounter;
            // Holds the number of loops
            long lngPollID;
            // Holds the Poll ID
            bool blnNewPost;
            // Set to true if the post is a new post since the users last visit
            int intShowTopicsWithin = 0;
            // Holds the amount of time to show topics within
            int intMovedForumID;
            // If the post is moved this holds the moved ID    
            int intNonPriorityTopicNum;
            // Holds the record count for non priority topics
            string strFirstPostMsg = "";
            System.DateTime dtmFirstEntryDate = new System.DateTime();
            // Holds the date of the first message
            // Initlaise variables
            intNonPriorityTopicNum = 0;
            // Read in the Forum ID to display the Topics for
            Variables.Forum.intForumID = int.Parse(Request.QueryString["FID"]);
            if (Variables.Forum.intForumID == 0)
            {
                // Redirect
                Response.Redirect("default.aspx");
            }

            // If this is the first time the page is displayed then the Forum Topic record position is set to page 1
            if (Request.QueryString["PN"] == null || Request.QueryString["PN"] == "")
            {
                intRecordPositionPageNum = 1;
                // Else the page has been displayed before so the Forum Topic record postion is set to the Record Position number
            }
            else
            {
                intRecordPositionPageNum = int.Parse(Request.QueryString["PN"]);
            }

            // Create a recordset to get the forum details
            // Read in the forum name from the database
            // Initalise the strSQL variable with an SQL statement to query the database
            string strSQL;
            if (FSDatabase.Utils.BDType == FSDatabase.Utils.TypeBd.SQLServer)
            {
                strSQL = ("Execute " + (Variables.Forum.strDbProc + ("ForumsAllWhereForumIs @Variables.Forum.intForumID = " + Variables.Forum.intForumID)));
            }
            else
            {
                strSQL = ("SELECT "
                            + (Variables.Forum.strDbTable + ("Forum.* FROM "
                            + (Variables.Forum.strDbTable + ("Forum WHERE Forum_ID = "
                            + (Variables.Forum.intForumID + ";"))))));
            }

            // Query the database
            rsForum = db.Execute(strSQL);
            // If there is a record returned by the recordset then check to see if you need a clave to enter it
            if (rsForum.Rows.Count > 0)
            {
                // Read in forum details from the database
                strForumName = rsForum.Rows[0]["Forum_name"].ToString();
                // Read in whether the forum is locked or not
                blnForumLocked = bool.Parse(rsForum.Rows[0]["Locked"].ToString());
                intShowTopicsWithin = int.Parse(rsForum.Rows[0]["Show_topics"].ToString());
                FuncionesForum.forumPermisisons(Variables.Forum.intForumID, FSPortal.Variables.User.GroupId, int.Parse(rsForum.Rows[0]["Read"].ToString()), int.Parse(rsForum.Rows[0]["Post"].ToString()), int.Parse(rsForum.Rows[0]["Reply_posts"].ToString()), int.Parse(rsForum.Rows[0]["Edit_posts"].ToString()), int.Parse(rsForum.Rows[0]["Delete_posts"].ToString()), 0, int.Parse(rsForum.Rows[0]["Poll_create"].ToString()), int.Parse(rsForum.Rows[0]["Vote"].ToString()), 0, 0);
                // If the user has no read writes then kick them
                if ((Variables.Forum.blnRead == false))
                {
                    // Redirect to a page asking for the user to enter the forum clave
                    Response.Redirect("insufficient_permission.aspx");
                }

                // If the forum requires a clave and a logged in forum code is not found on the users machine then send them to a login page
                if (((rsForum.Rows[0]["clave"].ToString() != "") && (Web.Cookie(Request.Cookies[FSPortal.Variables.App.strCookieName], ("Forum" + Variables.Forum.intForumID)) != rsForum.Rows[0]["Forum_code"].ToString())))
                {
                    // Redirect to a page asking for the user to enter the forum clave
                    Response.Redirect(("forum_clave_form.aspx?FID=" + Variables.Forum.intForumID));
                }

            }

            // Get what date to show topics till from cookie
            if (Web.Cookie(Request.Cookies["TS"]) != "")
            {
                intShowTopicsFrom = int.Parse(Web.Cookie(Request.Cookies["TS"]));
            }
            else
            {
                intShowTopicsFrom = intShowTopicsWithin;
            }

            // Initialse the string to display when the topics are show up till
            switch (intShowTopicsFrom)
            {
                case 0:
                    strShowTopicsFrom = Variables.Forum.strTxtFewYears;
                    break;
                case 7:
                    strShowTopicsFrom = Variables.Forum.strTxtWeek;
                    break;
                case 14:
                    strShowTopicsFrom = Variables.Forum.strTxtTwoWeeks;
                    break;
                case 31:
                    strShowTopicsFrom = Variables.Forum.strTxtMonth;
                    break;
                case 62:
                    strShowTopicsFrom = Variables.Forum.strTxtTwoMonths;
                    break;
                case 182:
                    strShowTopicsFrom = Variables.Forum.strTxtSixMonths;
                    break;
                case 365:
                    strShowTopicsFrom = Variables.Forum.strTxtYear;
                    break;
            }
            //sb.AppendLine("<html>");
            //sb.AppendLine("<head>");
            //sb.AppendLine("<title>");
            tituloPagina = Variables.Forum.strMainForumName + ":" + strForumName;
            //sb.AppendLine("</title>");

            string sTituloBarra;
            sTituloBarra = ("/ :: <a href=\"../default.aspx\">inicio</a> :: <a href=\'default.aspx\'>foro</a> :: " + strForumName);
            //sb.AppendLine(@"<navigation:navigation ID=""common1"" runat=""server"" />");
            sb.AppendLine(navigation_buttons_inc.Render());
            sb.AppendLine(sTituloBarra);
            sb.AppendLine(@"<table width=""");
            sb.AppendLine(Variables.Forum.strTableVariableWidth);
            sb.AppendLine(@""" border=""0"" cellspacing=""0"" cellpadding=""3"" align=""center"">");
            sb.AppendLine("<tr>");
            sb.AppendLine(@"<td align=""left"" class=""heading"">");
            // Display the forum name
            // sb.AppendLine(strForumName)
            // If the forum is locked show a locked pad lock icon
            if ((blnForumLocked == true))
            {
                sb.AppendLine(("  <span class=\"smText\">(<img src=\""
                                + (Variables.Forum.strImagePath + ("forum_locked_icon.gif\" align=\"baseline\" alt=\""
                                + (Variables.Forum.strTxtForumLocked + ("\"> "
                                + (Variables.Forum.strTxtForumLocked + ")</span>")))))));
            }
            sb.AppendLine("</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("<tr>");
            sb.AppendLine(@"<td align=""left"" class=""bold"">");
            // sb.AppendLine(strNavSpacer)
            // Check there are forum's to display
            if (!(rsForum.Rows.Count > 0))
            {
                // If there are no forum's to display then display the appropriate error message
                sb.AppendLine(("<span class=\"bold\">"
                                + (Variables.Forum.strTxtNoForums + "</span>")));
                // Else there the are forum's to write the HTML to display it the forum names and a discription
            }
            else
            {
                // Write the HTML of the forum descriptions as hyperlinks to the forums
                // sb.Append ("<a href=""forum_topics.aspx?FID=" & Variables.Forum.intForumID & """ target=""_self"" class=""boldLink"">" & strForumName & "</a>")
                // rsForum.Read()
            }
            sb.AppendLine("</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</table>");
            sb.AppendLine(@"<table width=""");
            sb.AppendLine(Variables.Forum.strTableVariableWidth);
            sb.AppendLine(@""" border=""0"" cellspacing=""1"" cellpadding=""3"" align=""center"">");
            sb.AppendLine("<tr>");
            sb.AppendLine("<form>");
            sb.AppendLine(@"<td width=""50%""><span class=""text"">");
            sb.AppendLine(Variables.Forum.strTxtShowTopics);
            sb.AppendLine("</span>");
            sb.AppendLine(@"<select name=""show"" onChange=""ShowTopicsFT(this);"">");
            sb.AppendLine(@"<option value=""0""");
            if ((intShowTopicsFrom == 0))
            {
                sb.Append("selected");
            }
            sb.Append(">");
            sb.AppendLine(Variables.Forum.strTxtAll);
            sb.AppendLine("</option>");
            sb.AppendLine(@"<option value=""7""");
            if ((intShowTopicsFrom == 7))
            {
                sb.Append("selected");
            }
            sb.Append(">");
            sb.AppendLine(Variables.Forum.strTxtLastWeek);
            sb.AppendLine("</option>");
            sb.AppendLine(@"<option value=""14""");
            if ((intShowTopicsFrom == 14))
            {
                sb.Append("selected");
            }
            sb.Append(">");
            sb.AppendLine(Variables.Forum.strTxtLastTwoWeeks);
            sb.AppendLine("</option>");
            sb.AppendLine(@"<option value=""31""");
            if ((intShowTopicsFrom == 31))
            {
                sb.Append("selected");
            }
            sb.Append(">");
            sb.AppendLine(Variables.Forum.strTxtLastMonth);
            sb.AppendLine("</option>");
            sb.AppendLine(@"<option value=""62""");
            if ((intShowTopicsFrom == 62))
            {
                sb.Append("selected");
            }
            sb.Append(">");
            sb.AppendLine(Variables.Forum.strTxtLastTwoMonths);
            sb.AppendLine("</option>");
            sb.AppendLine(@"<option value=""182""");
            if ((intShowTopicsFrom == 182))
            {
                sb.Append("selected");
            }
            sb.Append(">");
            sb.AppendLine(Variables.Forum.strTxtLastSixMonths);
            sb.AppendLine("</option>");
            sb.AppendLine(@"<option value=""365""");
            if ((intShowTopicsFrom == 365))
            {
                sb.Append("selected");
            }
            sb.Append(">");
            sb.AppendLine(Variables.Forum.strTxtLastYear);
            sb.AppendLine("</option>");
            sb.AppendLine("</select>");
            sb.AppendLine("</td>");
            sb.AppendLine(@"<td align=""right"" width=""50%""><a href=""post_message_form.aspx?FID=");
            sb.AppendLine(Variables.Forum.intForumID.ToString());
            sb.AppendLine(@""" target=""_self"">[");
            sb.AppendLine(Variables.Forum.strTxtNewTopic);
            sb.AppendLine("]</a>");
            // If the user can create a poll disply a create poll link
            if ((Variables.Forum.blnPollCreate == true))
            {
                sb.AppendLine(("<a href=\"poll_create_form.aspx?FID="
                                + (Variables.Forum.intForumID + ("\"  target=\"_self\">["
                                + (Variables.Forum.strTxtCreateNewPoll + "]</a>")))));
            }

            sb.AppendLine("</td>");
            sb.AppendLine("</form>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</table>");
            sb.AppendLine(@"<table width=""");
            sb.AppendLine(Variables.Forum.strTableVariableWidth);
            sb.AppendLine(@""" border=""0"" cellspacing=""0"" cellpadding=""1"" bgcolor=""");
            sb.AppendLine(Variables.Forum.strTableBorderColour);
            sb.AppendLine(@""" align=""center"">");
            sb.AppendLine("<tr>");
            sb.AppendLine("<td>");
            sb.AppendLine(@"<table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" bgcolor=""");
            sb.AppendLine(Variables.Forum.strTableBgColour);
            sb.AppendLine(@""">");
            sb.AppendLine("<tr>");
            sb.AppendLine(@"<td bgcolor=""");
            sb.AppendLine(Variables.Forum.strTableBgColour);
            sb.AppendLine(@""">");
            sb.AppendLine(@"<table width=""100%"" border=""0"" cellspacing=""1"" cellpadding=""3"" bgcolor=""");
            sb.AppendLine(Variables.Forum.strTableBgColour);
            sb.AppendLine(@""">");
            sb.AppendLine("<tr>");
            sb.AppendLine(@"<td bgcolor=""");
            sb.AppendLine(Variables.Forum.strTableTitleColour);
            sb.AppendLine(@""" width=""3%"" class=""tHeading"" background=""");
            sb.AppendLine(Variables.Forum.strTableTitleBgImage);
            sb.AppendLine(@"""> </td>");
            sb.AppendLine(@"<td bgcolor=""");
            sb.AppendLine(Variables.Forum.strTableTitleColour);
            sb.AppendLine(@""" width=""41%"" class=""tHeading"" background=""");
            sb.AppendLine(Variables.Forum.strTableTitleBgImage);
            sb.AppendLine(@""">");
            sb.AppendLine(Variables.Forum.strTxtTopics);
            sb.AppendLine("</td>");
            sb.AppendLine(@"<td bgcolor=""");
            sb.AppendLine(Variables.Forum.strTableTitleColour);
            sb.AppendLine(@""" width=""15%"" class=""tHeading"" background=""");
            sb.AppendLine(Variables.Forum.strTableTitleBgImage);
            sb.AppendLine(@""">");
            sb.AppendLine(Variables.Forum.strTxtThreadStarter);
            sb.AppendLine("</td>");
            sb.AppendLine(@"<td bgcolor=""");
            sb.AppendLine(Variables.Forum.strTableTitleColour);
            sb.AppendLine(@""" width=""7%"" align=""center"" class=""tHeading"" background=""");
            sb.AppendLine(Variables.Forum.strTableTitleBgImage);
            sb.AppendLine(@""">");
            sb.AppendLine(Variables.Forum.strTxtReplies);
            sb.AppendLine("</td>");
            sb.AppendLine(@"<td bgcolor=""");
            sb.AppendLine(Variables.Forum.strTableTitleColour);
            sb.AppendLine(@""" width=""7%"" align=""center"" class=""tHeading"" background=""");
            sb.AppendLine(Variables.Forum.strTableTitleBgImage);
            sb.AppendLine(@""">");
            sb.AppendLine(Variables.Forum.strTxtViews);
            sb.AppendLine("</td>");
            sb.AppendLine(@"<td bgcolor=""");
            sb.AppendLine(Variables.Forum.strTableTitleColour);
            sb.AppendLine(@""" width=""29%"" align=""center"" class=""tHeading"" background=""");
            sb.AppendLine(Variables.Forum.strTableTitleBgImage);
            sb.AppendLine(@""">");
            sb.AppendLine(Variables.Forum.strTxtLastPost);
            sb.AppendLine("</td>");
            sb.AppendLine("</tr>");
            // Get the Topics for the forum from the database
            // Initalise the strSQL variable with an SQL statement to query the database to count the number of Topics for each Forum
            // Run Stored procedures for SQL Server
            if (FSDatabase.Utils.BDType == FSDatabase.Utils.TypeBd.SQLServer)
            {
                if ((intShowTopicsFrom == 0))
                {
                    strSQL = ("Execute "
                                + (Variables.Forum.strDbProc + ("TopicDetialsAll @Variables.Forum.intForumID = " + Variables.Forum.intForumID)));
                }
                else
                {
                    strSQL = ("Execute "
                                + (Variables.Forum.strDbProc + ("TopicDetialsInTheLastXX @Variables.Forum.intForumID = "
                                + (Variables.Forum.intForumID + (", @intShowTopicsFrom = " + intShowTopicsFrom)))));
                }

                // Run SQL for Access
            }
            else
            {
                strSQL = ("SELECT "
                            + (Variables.Forum.strDbTable + ("Topic.Topic_ID, "
                            + (Variables.Forum.strDbTable + ("Topic.Moved_ID, "
                            + (Variables.Forum.strDbTable + ("Topic.No_of_views, "
                            + (Variables.Forum.strDbTable + ("Topic.Subject, "
                            + (Variables.Forum.strDbTable + ("Topic.Poll_ID, "
                            + (Variables.Forum.strDbTable + ("Topic.Locked, "
                            + (Variables.Forum.strDbTable + ("Topic.Priority, "
                            + (Variables.Forum.strDbTable + "Topic.Last_entry_date "))))))))))))))));
                strSQL = (strSQL + ("FROM "
                            + (Variables.Forum.strDbTable + "Topic ")));
                if ((intShowTopicsFrom == 0))
                {
                    strSQL = (strSQL + ("WHERE ("
                                + (Variables.Forum.strDbTable + ("Topic.Forum_ID = "
                                + (Variables.Forum.intForumID + (") OR ("
                                + (Variables.Forum.strDbTable + ("Topic.Priority = 3) OR ("
                                + (Variables.Forum.strDbTable + ("Topic.Moved_ID = "
                                + (Variables.Forum.intForumID + ") ")))))))))));
                }
                else
                {
                    strSQL = (strSQL + ("WHERE ((("
                                + (Variables.Forum.strDbTable + ("Topic.Forum_ID = "
                                + (Variables.Forum.intForumID + (") OR ("
                                + (Variables.Forum.strDbTable + ("Topic.Moved_ID = "
                                + (Variables.Forum.intForumID + (")) AND (("
                                + (Variables.Forum.strDbTable + ("Topic.Last_entry_date > Now() - "
                                + (intShowTopicsFrom + (") OR ("
                                + (Variables.Forum.strDbTable + ("Topic.Priority > 0))) OR ("
                                + (Variables.Forum.strDbTable + "Topic.Priority = 3) ")))))))))))))))));
                }

                strSQL = (strSQL + ("ORDER BY "
                            + (Variables.Forum.strDbTable + ("Topic.Priority DESC, "
                            + (Variables.Forum.strDbTable + "Topic.Last_entry_date DESC;")))));
            }

            // Query the database
            rsForum = db.Execute(strSQL, intRecordPositionPageNum, Variables.Forum.intTopicPerPage);
            // Set the number of records to display on each page
            // rsForum.PageSize = Variables.Forum.intTopicPerPage
            // Check there are Topic's to display
            if (rsForum.Rows.Count == 0)
            {
                // If there are no Topic's to display then display the appropriate error message
                sb.AppendLine(("\r\n" + ("<td bgcolor=\""
                                + (Variables.Forum.strTableColour + ("\" background=\""
                                + (Variables.Forum.strTableBgImage + ("\" colspan=\"6\" class=\"text\">"
                                + (Variables.Forum.strTxtNoTopicsToDisplay + (" "
                                + (strShowTopicsFrom + "</td>"))))))))));
                // Else there the are topic's so write the HTML to display the topic names and a discription
            }
            else
            {
                int intTotalNumOfThreads = rsForum.Rows.Count;
                // Count the number of pages there are in the recordset calculated by the PageSize attribute set by admin
                intTotalNumOfPages = intTotalNumOfThreads % Variables.Forum.intThreadsPerPage;
                // rsPost.PageCount
                if ((intTotalNumOfThreads % Variables.Forum.intThreadsPerPage) > 0)
                {
                    intTotalNumOfPages = (intTotalNumOfPages + 1);
                }

                // Get the record poistion to display from
                // rsForum.AbsolutePage = intRecordPositionPageNum
                // If there are no records on this page and it's above the frist page then set the page position to 1
                // If rsForum.EOF And intRecordPositionPageNum > 1 Then rsForum.AbsolutePage = 1
                // Count the number of pages there are in the recordset calculated by the PageSize attribute set above
                // intTotalNumOfPages = rsForum.PageCount
                // Craete a Recodset object for the topic details
                // rsTopic = Server.CreateObject("ADODB.Recordset")
                // Loop round to read in all the Topics in the database
                // For intRecordLoopCounter = 1 To Variables.Forum.intTopicPerPage
                intRecordLoopCounter = 1;
                foreach (DataRow row in rsForum.Rows)
                {
                    // Read in Topic details from the database
                    lngTopicID = long.Parse(row["Topic_ID"].ToString());
                    lngPollID = long.Parse(row["Poll_ID"].ToString());
                    intMovedForumID = int.Parse(row["Moved_ID"].ToString());
                    lngNumberOfViews = long.Parse(row["No_of_views"].ToString());
                    strSubject = row["Subject"].ToString();
                    blnTopicLocked = bool.Parse(row["Locked"].ToString());
                    intPriority = int.Parse(row["Priority"].ToString());
                    if ((intPriority <= 1))
                    {
                        intNonPriorityTopicNum = (intNonPriorityTopicNum + 1);
                    }

                    // If this is the first topic that is not importent then display the forum topics bar
                    if ((intNonPriorityTopicNum == 1))
                    {
                        sb.AppendLine(("\r\n" + ("<tr><td bgcolor=\""
                                        + (Variables.Forum.strTableTitleColour2 + ("\" background=\""
                                        + (Variables.Forum.strTableTitleBgImage2 + ("\" colspan=\"6\" class=\"tiHeading\">"
                                        + (Variables.Forum.strTxtForum + (" "
                                        + (Variables.Forum.strTxtTopics + "</td></tr>"))))))))));
                    }

                    // If this is the first topic and it is an important one then display a bar saying so
                    if (((intRecordLoopCounter == 1)
                                && (intPriority >= 2)))
                    {
                        sb.AppendLine(("\r\n" + ("<tr><td bgcolor=\""
                                        + (Variables.Forum.strTableTitleColour2 + ("\" background=\""
                                        + (Variables.Forum.strTableTitleBgImage2 + ("\" colspan=\"6\" class=\"tiHeading\">"
                                        + (Variables.Forum.strTxtImportantTopics + "</td></tr>"))))))));
                    }

                    // Initalise the strSQL variable with an SQL statement to query the database to get the Usuarios and subject from the database for the topic
                    if ((FSDatabase.Utils.BDType == FSDatabase.Utils.TypeBd.SQLServer))
                    {
                        strSQL = ("Execute "
                                    + (Variables.Forum.strDbProc + ("LastAndFirstThreadUsuarios @lngTopicID = " + lngTopicID)));
                    }
                    else
                    {
                        strSQL = ("SELECT "
                                    + (Variables.Forum.strDbTable + ("Thread.Thread_ID, "
                                    + (Variables.Forum.strDbTable + ("Thread.UsuarioID, "
                                    + (Variables.Forum.strDbTable + ("Thread.Message, "
                                    + (Variables.Forum.strDbTable + "Thread.Message_date "))))))));
                        strSQL = (strSQL + ("FROM "
                                    + (Variables.Forum.strDbTable + "Thread ")));
                        strSQL = (strSQL + ("WHERE "
                                    + (Variables.Forum.strDbTable + ("Thread.Topic_ID = "
                                    + (lngTopicID + " ")))));
                        strSQL = (strSQL + ("ORDER BY "
                                    + (Variables.Forum.strDbTable + "Thread.Message_date ASC;")));
                    }

                    // Set the cursor type property of the record set to forward only so we can navigate through the record set
                    // rsTopic.CursorType = 1
                    // Query the database
                    rsTopic = db.Execute(strSQL);
                    // If there is info in the database relating to the topic then get them from the record set
                    if ((rsTopic.Rows.Count > 0))
                    {
                        // Read in the subject and Usuarios and number of replies from the record set
                        strTopicStartusuario = rsTopic.Rows[0]["UsuarioID"].ToString();
                        strFirstPostMsg = TextUtil.Substring(rsTopic.Rows[0]["Message"].ToString(), 0, 275);
                        lngTopicStartUserID = long.Parse(rsTopic.Rows[0]["UsuarioID"].ToString());
                        lngNumberOfReplies = rsTopic.Rows.Count;
                        dtmFirstEntryDate = System.DateTime.Parse(rsTopic.Rows[0]["Message_date"].ToString());
                        lngLastEntryMessageID = long.Parse(rsTopic.Rows[(rsTopic.Rows.Count - 1)]["Thread_ID"].ToString());
                        strLastEntryusuario = rsTopic.Rows[(rsTopic.Rows.Count - 1)]["UsuarioID"].ToString();
                        lngLastEntryUserID = long.Parse(rsTopic.Rows[(rsTopic.Rows.Count - 1)]["UsuarioID"].ToString());
                        dtmLastEntryDate = System.DateTime.Parse(rsTopic.Rows[(rsTopic.Rows.Count - 1)]["Message_date"].ToString());
                        strFirstPostMsg = FuncionesFilter.removeHTML(strFirstPostMsg);
                        // Trim the number of characters down to 150 for the subject link title
                        strFirstPostMsg = TextUtil.Substring(strFirstPostMsg.Trim(), 0, 150);
                        // If the length of the message is over 150 then append ... to it
                        if ((long.Parse(strFirstPostMsg.Length.ToString()) == 150))
                        {
                            strFirstPostMsg = (strFirstPostMsg.Trim() + "...");
                        }

                        // Set the booleon varible if this is a new post since the users last visit and has not been read
                        string rC = "";
                        if (Request.Cookies["RT"] != null)
                        {
                            rC = Request.Cookies["RT"][("TID" + lngTopicID)];
                        }

                        if (((System.DateTime.Parse(Session["dtmLastVisit"].ToString()) < dtmLastEntryDate)
                                    && (rC == "")))
                        {
                            blnNewPost = true;
                        }
                        else
                        {
                            blnNewPost = false;
                        }

                        // Write the HTML of the Topic descriptions as hyperlinks to the Topic details and message
                        sb.AppendLine(("\r\n" + "\t<tr>"));
                        sb.AppendLine(("\r\n" + "\t<td bgcolor=\""));
                        if (((intRecordLoopCounter % 2)
                                    == 0))
                        {
                            sb.AppendLine(Variables.Forum.strTableEvenRowColour);
                        }
                        else
                        {
                            sb.AppendLine(Variables.Forum.strTableOddRowColour);
                        }

                        sb.AppendLine(("\" background=\""
                                        + (Variables.Forum.strTableBgImage + "\" width=\"1%\" align=\"center\">")));
                        // If the topic is pinned then display the pinned icon
                        if ((intPriority == 1))
                        {
                            sb.AppendLine(("<img src=\""
                                            + (Variables.Forum.strImagePath + ("pinned_topic_icon.gif\" border=\"0\" alt=\""
                                            + (Variables.Forum.strTxtPinnedTopic + "\">")))));
                            // If the topic is top priorty and locked then display top priporty locked icon
                        }
                        else if (((blnTopicLocked == true)
                                    && (intPriority > 0)))
                        {
                            sb.AppendLine(("<img src=\""
                                            + (Variables.Forum.strImagePath + ("priority_post_locked_icon.gif\" border=\"0\" alt=\""
                                            + (Variables.Forum.strTxtHighPriorityPostLocked + "\">")))));
                            // If the topic is top priorty then display top priporty icon
                        }
                        else if ((intPriority > 0))
                        {
                            sb.AppendLine(("<img src=\""
                                            + (Variables.Forum.strImagePath + ("priority_post_icon.gif\" border=\"0\" alt=\""
                                            + (Variables.Forum.strTxtHighPriorityPost + "\">")))));
                            // If the topic is closed display a closed topic icon
                        }
                        else if ((blnTopicLocked == true))
                        {
                            sb.AppendLine(("<img src=\""
                                            + (Variables.Forum.strImagePath + ("closed_topic_icon.gif\" border=\"0\" alt=\""
                                            + (Variables.Forum.strTxtLockedTopic + "\">")))));
                            // If the topic is moved to another forum display moved post icon
                        }
                        else if (((intMovedForumID == Variables.Forum.intForumID)
                                    && (intMovedForumID != 0)))
                        {
                            sb.AppendLine(("<img src=\""
                                            + (Variables.Forum.strImagePath + ("moved_icon.gif\" border=\"0\" alt=\""
                                            + (Variables.Forum.strTxtMoved + "\">")))));
                            // If the topic is a hot topic and with new replies then display hot to new replies icon
                        }
                        else if ((((lngNumberOfReplies >= Variables.Forum.intNumHotReplies)
                                    || (lngNumberOfViews >= Variables.Forum.intNumHotViews))
                                    && (blnNewPost == true)))
                        {
                            sb.AppendLine(("<img src=\""
                                            + (Variables.Forum.strImagePath + ("hot_topic_new_posts_icon.gif\" border=\"0\" alt=\""
                                            + (Variables.Forum.strTxtHotTopicNewReplies + "\">")))));
                            // If this is a hot topic that contains a poll then display the hot topic poll icon
                        }
                        else if (((lngPollID > 0)
                                    && ((lngNumberOfReplies >= Variables.Forum.intNumHotReplies)
                                    || (lngNumberOfViews >= Variables.Forum.intNumHotViews))))
                        {
                            sb.AppendLine(("<img src=\""
                                            + (Variables.Forum.strImagePath + ("hot_topic_poll_icon.gif\" border=\"0\" alt=\""
                                            + (Variables.Forum.strTxtHotTopic + "\">")))));
                            // If the topic is a hot topic display hot topic icon
                        }
                        else if (((lngNumberOfReplies >= Variables.Forum.intNumHotReplies)
                                    || (lngNumberOfViews >= Variables.Forum.intNumHotViews)))
                        {
                            sb.AppendLine(("<img src=\""
                                            + (Variables.Forum.strImagePath + ("hot_topic_no_new_posts_icon.gif\" border=\"0\" alt=\""
                                            + (Variables.Forum.strTxtHotTopic + "\">")))));
                            // If the topic is has new replies display new replies icon
                        }
                        else if ((blnNewPost == true))
                        {
                            sb.AppendLine(("<img src=\""
                                            + (Variables.Forum.strImagePath + ("new_posts_icon.gif\" border=\"0\" alt=\""
                                            + (Variables.Forum.strTxtOpenTopicNewReplies + "\">")))));
                            // If there is a poll in the post display the poll post icon
                        }
                        else if ((lngPollID > 0))
                        {
                            sb.AppendLine(("<img src=\""
                                            + (Variables.Forum.strImagePath + ("poll_icon.gif\" border=\"0\" alt=\""
                                            + (Variables.Forum.strTxtHotTopic + "\">")))));
                            // Display topic icon
                        }
                        else
                        {
                            sb.AppendLine(("<img src=\""
                                            + (Variables.Forum.strImagePath + ("no_new_posts_icon.gif\" border=\"0\" alt=\""
                                            + (Variables.Forum.strTxtOpenTopic + "\">")))));
                        }

                        sb.AppendLine(("\r\n" + "\t</td>"));
                        sb.AppendLine(("\r\n" + "\t<td bgcolor=\""));
                        if (((intRecordLoopCounter % 2)
                                    == 0))
                        {
                            sb.AppendLine(Variables.Forum.strTableEvenRowColour);
                        }
                        else
                        {
                            sb.AppendLine(Variables.Forum.strTableOddRowColour);
                        }

                        sb.AppendLine(("\" background=\""
                                        + (Variables.Forum.strTableBgImage + "\" width=\"41%\" class=\"text\">")));
                        // If the user is a forum admin or a moderator then give let them delete the topic
                        if ((FSPortal.Variables.User.Administrador || Variables.Forum.blnModerator))
                        {
                            sb.AppendLine(("\r\n" + ("      <a href=\"javascript:openWin(\'pop_up_topic_admin.aspx?TID="
                                            + (lngTopicID + ("\',\'admin\',\'toolbar=0,location=0,status=0,menubar=0,scrollbars=1,resizable=1,width=590,height=425\')\"><" +
                                            "img src=\""
                                            + (Variables.Forum.strImagePath + ("small_admin_icon.gif\" align=\"middle\" border=\"0\" alt=\""
                                            + (Variables.Forum.strTxtTopicAdmin + "\"></a>"))))))));
                        }

                        // If the topic is moved to another forum display moved next to it
                        if (((intMovedForumID == Variables.Forum.intForumID)
                                    && (intMovedForumID != 0)))
                        {
                            sb.AppendLine(Variables.Forum.strTxtMoved);
                        }

                        // If there is a poll display a poll text
                        if ((lngPollID > 0))
                        {
                            sb.AppendLine(Variables.Forum.strTxtPoll);
                        }

                        // Display the subject of the topic
                        sb.AppendLine(("\r\n" + ("\t<a href=\"forum_posts.aspx?TID="
                                        + (lngTopicID + ("&PN=" + intRecordPositionPageNum)))));
                        if ((intPriority == 3))
                        {
                            sb.AppendLine(("&FID="
                                            + (Variables.Forum.intForumID + "&PR=3")));
                        }

                        sb.AppendLine(("\" target=\"_self\" title=\""
                                        + (strFirstPostMsg + ("\">"
                                        + (strSubject + "</a>")))));
                        // Calculate the number of pages for the topic and display links if there are more than 1 page
                        intNumberOfTopicPages = int.Parse((lngNumberOfReplies + 1).ToString()) % Variables.Forum.intThreadsPerPage;
                        // If there is a remainder from calculating the num of pages add 1 to the number of pages
                        if (((lngNumberOfReplies + 1) % Variables.Forum.intThreadsPerPage) > 0)
                        {
                            intNumberOfTopicPages = (intNumberOfTopicPages + 1);
                        }

                        // If there is more than 1 page for the topic display links to the other pages
                        if ((intNumberOfTopicPages > 1))
                        {
                            sb.AppendLine(("\r\n" + ("<br /><img src=\""
                                            + (Variables.Forum.strImagePath + ("pages_icon.gif\" align=\"middle\" alt=\""
                                            + (Variables.Forum.strTxtPages + "\">"))))));
                            // Loop round to display the links to the other pages
                            for (intTopicPagesLoopCounter = 1; (intTopicPagesLoopCounter <= intNumberOfTopicPages); intTopicPagesLoopCounter++)
                            {
                                // If there is more than 7 pages display ... last page and exit the loop
                                if ((intTopicPagesLoopCounter > 7))
                                {
                                    // If this is position 8 then display just the 8th page
                                    if ((intNumberOfTopicPages == 8))
                                    {
                                        sb.AppendLine(("\r\n" + (" <a href=\"forum_posts.aspx?TID="
                                                        + (lngTopicID + ("&PN="
                                                        + (intRecordPositionPageNum + "&TPN=8"))))));
                                        // If a priority topic need to make sure we don't change forum
                                        if ((intPriority == 3))
                                        {
                                            sb.AppendLine(("&FID="
                                                            + (Variables.Forum.intForumID + "&PR=3")));
                                        }

                                        sb.AppendLine("\" target=\"_self\" class=\"smLink\">8</a>");
                                        // Else display the last 2 pages
                                    }
                                    else
                                    {
                                        sb.AppendLine(" ...");
                                        sb.AppendLine(("\r\n" + (" <a href=\"forum_posts.aspx?TID="
                                                        + (lngTopicID + ("&PN="
                                                        + (intRecordPositionPageNum + ("&TPN="
                                                        + (intNumberOfTopicPages - 1))))))));
                                        // If a priority topic need to make sure we don't change forum
                                        if ((intPriority == 3))
                                        {
                                            sb.AppendLine(("&FID="
                                                            + (Variables.Forum.intForumID + "&PR=3")));
                                        }

                                        sb.AppendLine(("\" target=\"_self\" class=\"smLink\">"
                                                        + ((intNumberOfTopicPages - 1)
                                                        + "</a>")));
                                        sb.AppendLine(("\r\n" + (" <a href=\"forum_posts.aspx?TID="
                                                        + (lngTopicID + ("&PN="
                                                        + (intRecordPositionPageNum + ("&TPN=" + intNumberOfTopicPages)))))));
                                        // If a priority topic need to make sure we don't change forum
                                        if ((intPriority == 3))
                                        {
                                            sb.AppendLine(("&FID="
                                                            + (Variables.Forum.intForumID + "&PR=3")));
                                        }

                                        sb.AppendLine(("\" target=\"_self\" class=\"smLink\">"
                                                        + (intNumberOfTopicPages + "</a>")));
                                    }

                                    break;
                                }

                                // Display the links to the other pages
                                sb.AppendLine(("\r\n" + (" <a href=\"forum_posts.aspx?TID="
                                                + (lngTopicID + ("&PN="
                                                + (intRecordPositionPageNum + ("&TPN=" + intTopicPagesLoopCounter)))))));
                                // If a priority topic need to make sure we don't change forum
                                if ((intPriority == 3))
                                {
                                    sb.AppendLine(("&FID="
                                                    + (Variables.Forum.intForumID + "&PR=3")));
                                }

                                sb.AppendLine(("\" target=\"_self\" class=\"smLink\">"
                                                + (intTopicPagesLoopCounter + "</a>")));
                            }

                        }

                    }

                }

            }
            sb.AppendLine("</td>");
            sb.AppendLine(@"<td bgcolor=""");
            if ((intRecordLoopCounter % 2) == 0)
            {
                sb.AppendLine(Variables.Forum.strTableEvenRowColour);
            }
            else
            {
                sb.AppendLine(Variables.Forum.strTableOddRowColour);
            }
            sb.AppendLine(@""" background=""");
            sb.AppendLine(Variables.Forum.strTableBgImage);
            sb.AppendLine(@""" width=""15%"" class=""text""><a href=""JavaScript:openWin('pop_up_profile.aspx?PF=");
            sb.AppendLine(lngTopicStartUserID.ToString());
            sb.AppendLine("&FID=");
            sb.AppendLine(Variables.Forum.intForumID.ToString());
            sb.AppendLine(@"','profile','toolbar=0,location=0,status=0,menubar=0,scrollbars=1,resizable=1,width=590,height=425')"" title=""");
            sb.AppendLine((Variables.Forum.strTxtThisTopicWasStarted
                                    + (FuncionesFecha.DateFormat(dtmFirstEntryDate, FuncionesFecha.saryDateTimeData) + (" "
                                    + (Variables.Forum.strTxtAt + (" " + FuncionesFecha.TimeFormat(dtmFirstEntryDate, FuncionesFecha.saryDateTimeData)))))));
            sb.AppendLine(@""">");
            sb.AppendLine(strTopicStartusuario);
            sb.AppendLine("</a></td>");
            sb.AppendLine(@"<td bgcolor=""");
            if (((intRecordLoopCounter % 2)
                                == 0))
            {
                sb.AppendLine(Variables.Forum.strTableEvenRowColour);
            }
            else
            {
                sb.AppendLine(Variables.Forum.strTableOddRowColour);
            }
            sb.AppendLine(@""" background=""");
            sb.AppendLine(Variables.Forum.strTableBgImage);
            sb.AppendLine(@""" width=""7%"" align=""center"" class=""text"">");
            sb.AppendLine(lngNumberOfReplies.ToString());
            sb.AppendLine("</td>");
            sb.AppendLine(@"<td bgcolor=""");
            if (((intRecordLoopCounter % 2)
                                == 0))
            {
                sb.AppendLine(Variables.Forum.strTableEvenRowColour);
            }
            else
            {
                sb.AppendLine(Variables.Forum.strTableOddRowColour);
            }
            sb.AppendLine(@""" background=""");
            sb.AppendLine(Variables.Forum.strTableBgImage);
            sb.AppendLine(@""" width=""7%"" align=""center"" class=""text"">");
            sb.AppendLine(lngNumberOfViews.ToString());
            sb.AppendLine("</td>");
            sb.AppendLine(@"<td bgcolor=""");
            if (((intRecordLoopCounter % 2)
                                == 0))
            {
                sb.AppendLine(Variables.Forum.strTableEvenRowColour);
            }
            else
            {
                sb.AppendLine(Variables.Forum.strTableOddRowColour);
            }
            sb.AppendLine(@""" background=""");
            sb.AppendLine(Variables.Forum.strTableBgImage);
            sb.AppendLine(@""" width=""29%"" class=""smText"" align=""right"" nowrap=""nowrap"">");
            sb.AppendLine((FuncionesFecha.DateFormat(dtmLastEntryDate, FuncionesFecha.saryDateTimeData) + (" "
                                    + (Variables.Forum.strTxtAt + (" " + FuncionesFecha.TimeFormat(dtmLastEntryDate, FuncionesFecha.saryDateTimeData))))));
            sb.AppendLine("<br />");
            sb.AppendLine(Variables.Forum.strTxtBy);
            sb.AppendLine(@"<a href=""javascript:openWin('pop_up_profile.aspx?PF=");
            sb.AppendLine(lngLastEntryUserID.ToString());
            sb.AppendLine(@"','profile','toolbar=0,location=0,status=0,menubar=0,scrollbars=1,resizable=1,width=590,height=425')""  class=""smLink"">");
            sb.AppendLine(strLastEntryusuario);
            sb.AppendLine(@"</a> <a href=""forum_posts.aspx?TID=");
            sb.AppendLine(lngTopicID.ToString());
            sb.AppendLine("&PN=");
            sb.AppendLine(intRecordPositionPageNum.ToString());
            if (intPriority == 3) sb.AppendLine("&FID=" + Variables.Forum.intForumID + "&PR=3");
            sb.AppendLine("&get=last#");
            sb.AppendLine(lngLastEntryMessageID.ToString());
            sb.AppendLine(@""" target=""_self""><img src=""");
            sb.AppendLine(Variables.Forum.strImagePath);
            sb.AppendLine(@"right_arrow.gif"" align=""middle"" border=""0"" alt=""");
            sb.AppendLine(Variables.Forum.strTxtViewLastPost);
            sb.AppendLine(@"""></a></td>");
            sb.AppendLine("</tr>");
            intRecordLoopCounter = (intRecordLoopCounter + 1);
            sb.AppendLine("</table>");
            sb.AppendLine("</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</table>");
            sb.AppendLine("</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</table>");
            sb.AppendLine(@"<table width=""");
            sb.AppendLine(Variables.Forum.strTableVariableWidth);
            sb.AppendLine(@""" border=""0"" cellspacing=""0"" cellpadding=""4"" align=""center"">");
            sb.AppendLine("<tr><form><td>");
            // Display a link to watch or un-watch this topic if email notification is enabled
            if (((Variables.Forum.blnEmail == true)
                        && (FSPortal.Variables.User.GroupId != 2)))
            {
                // Initalise the SQL string with a query to get the poll details
                if ((FSDatabase.Utils.BDType == FSDatabase.Utils.TypeBd.SQLServer))
                {
                    strSQL = ("Execute "
                                + (Variables.Forum.strDbProc + ("ForumEmailNotify @lngUsuariosID = "
                                + (FSPortal.Variables.User.UsuarioId + (", @Variables.Forum.intForumID= " + Variables.Forum.intForumID)))));
                }
                else
                {
                    strSQL = ("SELECT "
                                + (Variables.Forum.strDbTable + "EmailNotify.*  "));
                    strSQL = (strSQL + ("FROM "
                                + (Variables.Forum.strDbTable + "EmailNotify ")));
                    strSQL = (strSQL + ("WHERE "
                                + (Variables.Forum.strDbTable + ("EmailNotify.UsuarioID="
                                + (FSPortal.Variables.User.UsuarioId + (" AND "
                                + (Variables.Forum.strDbTable + ("EmailNotify.Forum_ID="
                                + (Variables.Forum.intForumID + ";")))))))));
                }

                // Query the database
                rsForum = db.Execute(strSQL);
                // Create link
                sb.AppendLine(("<a href=\"email_notify.aspx?FID="
                                + (Variables.Forum.intForumID + ("&PN="
                                + (intRecordPositionPageNum + "\" target=\"_self\">")))));
                // If a record is return the user is watching this topic
                if ((rsForum.Rows.Count > 0))
                {
                    sb.AppendLine(Variables.Forum.strTxtUn);
                }

                // Display link to watch the topic
                sb.AppendLine((Variables.Forum.strTxtWatchThisForum + "</a>"));
                // Disply a non breaking space for Netscrape 4 bug
            }
            else
            {
                sb.AppendLine(" ");
            }
            sb.AppendLine("</td>");
            // If there is more than 1 page of topics then dispaly drop down list to the other topics
            if (intTotalNumOfPages > 1)
            {
                // Display an image link to the last topic
                sb.AppendLine(("\r\n" + "\t\t<td align=\"right\" class=\"text\" nowrap=\"nowrap\">"));
                // Display a prev link if previous pages are available
                if ((intRecordPositionPageNum > 1))
                {
                    sb.AppendLine(("<a href=\"forum_topics.aspx?FID="
                                    + (Variables.Forum.intForumID + ("&PN="
                                    + ((intRecordPositionPageNum - 1) + ("\"><&lt "
                                    + (Variables.Forum.strTxtPrevious + "</a> ")))))));
                }

                sb.AppendLine((Variables.Forum.strTxtPage + (" " + ("\r\n" + "\t\t <select onChange=\"ForumJump(this)\" name=\"SelectTopicPage\">"))));
                // Loop round to display links to all the other pages
                for (intTopicPageLoopCounter = 1; (intTopicPageLoopCounter <= intTotalNumOfPages); intTopicPageLoopCounter++)
                {
                    // Display a link in the link list to the another topic page
                    sb.Append("\r\n" + "\t\t  <option value=\"forum_topics.aspx?FID="
                                    + Variables.Forum.intForumID + "&PN="
                                    + intTopicPageLoopCounter + "\"");
                    // If this page number to display is the same as the page being displayed then make sure it's selected
                    if (intTopicPageLoopCounter == intRecordPositionPageNum)
                    {
                        sb.Append(" selected");
                    }

                    // Display the link page number
                    sb.Append(">" + intTopicPageLoopCounter + "</option>");
                }

                // End the drop down list
                sb.AppendLine(("\r\n" + ("\t\t</select> "
                                + (Variables.Forum.strTxtOf + (" " + intTotalNumOfPages)))));
                // Display a next link if needed
                if ((intRecordPositionPageNum != intTotalNumOfPages))
                {
                    sb.AppendLine((" <a href=\"forum_topics.aspx?FID="
                                    + (Variables.Forum.intForumID + ("&PN="
                                    + ((intRecordPositionPageNum + 1) + ("\">"
                                    + (Variables.Forum.strTxtNext + " >></a>")))))));
                }

                sb.AppendLine("</td>");
            }
            sb.AppendLine("</tr>");
            sb.AppendLine("<tr>");
            //sb.AppendLine(@"<td><forum_jump:forum_jump ID=""forum_jump"" runat=""server"" /></td>");
            sb.AppendLine("<td>" + forum_jump_inc.Render() + "</td>");
            sb.AppendLine(@"<td align=""right""><a href=""post_message_form.aspx?FID=");
            sb.AppendLine(Variables.Forum.intForumID.ToString());
            sb.AppendLine(@""" target=""_self"">[" + Variables.Forum.strTxtNewTopic + "]</a>");
            // If the user can create a poll disply a create poll link
            if ((Variables.Forum.blnPollCreate == true))
            {
                sb.AppendLine(("<a href=\"poll_create_form.aspx?FID="
                                + (Variables.Forum.intForumID + ("\"  target=\"_self\">["
                                + (Variables.Forum.strTxtCreateNewPoll + "]</a>")))));
            }

            sb.AppendLine("</td>");
            sb.AppendLine("</form></tr>");
            sb.AppendLine("</table>");
            sb.AppendLine(@"<div align=""center"">");
            sb.AppendLine(@"<table width=""");
            sb.AppendLine(Variables.Forum.strTableVariableWidth);
            sb.AppendLine(@""" border=""0"" cellspacing=""0"" cellpadding=""1"">");
            sb.AppendLine("<tr>");
            sb.AppendLine(@"<td width=""60%"">");
            sb.AppendLine(@"<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""2"">");
            sb.AppendLine("<tr>");
            sb.AppendLine(@"<td class=""smText"" align=""left""><img src=""");
            sb.AppendLine(Variables.Forum.strImagePath);
            sb.AppendLine(@"no_new_posts_icon.gif"" alt=""");
            sb.AppendLine(Variables.Forum.strTxtOpenTopic);
            sb.AppendLine(@""">");
            sb.AppendLine(Variables.Forum.strTxtOpenTopic);
            sb.AppendLine("</td>");
            sb.AppendLine(@"<td class=""smText"" align=""left""><img src=""");
            sb.AppendLine(Variables.Forum.strImagePath);
            sb.AppendLine(@"hot_topic_no_new_posts_icon.gif"" alt=""");
            sb.AppendLine(Variables.Forum.strTxtHotTopic);
            sb.AppendLine(@""">");
            sb.AppendLine(Variables.Forum.strTxtHotTopic);
            sb.AppendLine("</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("<tr>");
            sb.AppendLine(@"<td class=""smText"" align=""left""><img src=""");
            sb.AppendLine(Variables.Forum.strImagePath);
            sb.AppendLine(@"new_posts_icon.gif"" alt=""");
            sb.AppendLine(Variables.Forum.strTxtOpenTopicNewReplies);
            sb.AppendLine(@""">");
            sb.AppendLine(Variables.Forum.strTxtOpenTopicNewReplies);
            sb.AppendLine("</td>");
            sb.AppendLine(@"<td class=""smText"" align=""left""><img src=""");
            sb.AppendLine(Variables.Forum.strImagePath);
            sb.AppendLine(@"hot_topic_new_posts_icon.gif"" alt=""");
            sb.AppendLine(Variables.Forum.strTxtHotTopicNewReplies);
            sb.AppendLine(@""">");
            sb.AppendLine(Variables.Forum.strTxtHotTopicNewReplies);
            sb.AppendLine("</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("<tr>");
            sb.AppendLine(@"<td class=""smText"" align=""left""><img src=""");
            sb.AppendLine(Variables.Forum.strImagePath);
            sb.AppendLine(@"closed_topic_icon.gif"" alt=""");
            sb.AppendLine(Variables.Forum.strTxtLockedTopic);
            sb.AppendLine(@""">");
            sb.AppendLine(Variables.Forum.strTxtLockedTopic);
            sb.AppendLine("</td>");
            sb.AppendLine(@"<td class=""smText"" align=""left""><img src=""");
            sb.AppendLine(Variables.Forum.strImagePath);
            sb.AppendLine(@"priority_post_icon.gif"" alt=""");
            sb.AppendLine(Variables.Forum.strTxtHighPriorityPost);
            sb.AppendLine(@""">");
            sb.AppendLine(Variables.Forum.strTxtHighPriorityPost);
            sb.AppendLine("</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("<tr>");
            sb.AppendLine(@"<td class=""smText"" align=""left""><img src=""");
            sb.AppendLine(Variables.Forum.strImagePath);
            sb.AppendLine(@"pinned_topic_icon.gif"" alt=""");
            sb.AppendLine(Variables.Forum.strTxtPinnedTopic);
            sb.AppendLine(@""">");
            sb.AppendLine(Variables.Forum.strTxtPinnedTopic);
            sb.AppendLine("</td>");
            sb.AppendLine(@"<td class=""smText"" align=""left""><img src=""");
            sb.AppendLine(Variables.Forum.strImagePath);
            sb.AppendLine(@"priority_post_locked_icon.gif"" alt=""");
            sb.AppendLine(Variables.Forum.strTxtHighPriorityPostLocked);
            sb.AppendLine(@""">");
            sb.AppendLine(Variables.Forum.strTxtHighPriorityPostLocked);
            sb.AppendLine("</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</table>");
            sb.AppendLine("</td>");
            sb.AppendLine(@"<td width=""40%"" align=""right"" class=""smText"" nowrap=""nowrap""><forum_permissions:forum_permissions ID=""common2"" runat=""server"" /></td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</table>");
            sb.AppendLine("<br />");
            sb.AppendLine("</div>");
            sb.AppendLine(@"<div align=""center"">");
            // Display the process time
            // If Variables.Forum.blnShowProcessTime Then sb.AppendLine("<span class=""smText""><br /><br />" & Variables.Forum.strTxtThisPageWasGeneratedIn & " " & FormatNumber(Timer() - Variables.Forum.dblStartTime, 4) & " " & Variables.Forum.strTxtSeconds & "</span>")}
            sb.AppendLine("</div>");
            // Display an alert message letting the user know the topic has been deleted
            if ((Request.QueryString["DL"] == "1"))
            {
                sb.AppendLine("<script language=\"JavaScript\">");
                sb.AppendLine(("alert(\'"
                                + (Variables.Forum.strTxtTheTopicIsNowDeleted + "\')")));
                sb.AppendLine("</script>");
            }

            // Display an alert message if the user is watching this forum for email notification
            if ((Request.QueryString["EN"] == "FS"))
            {
                sb.AppendLine("<script language=\"JavaScript\">");
                sb.AppendLine(("alert(\'"
                                + (Variables.Forum.strTxtYouAreNowBeNotifiedOfPostsInThisForum + "\')")));
                sb.AppendLine("</script>");
            }

            // Display an alert message if the user is not watching this forum for email notification
            if ((Request.QueryString["EN"] == "FU"))
            {
                sb.AppendLine("<script language=\"JavaScript\">");
                sb.AppendLine(("alert(\'"
                                + (Variables.Forum.strTxtYouAreNowNOTBeNotifiedOfPostsInThisForum + "\')")));
                sb.AppendLine("</script>");
            }
            return sb.ToString();
        }

    }

}
