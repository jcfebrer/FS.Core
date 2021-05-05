// <fileheader>
// <copyright file="active_topics.aspx.cs" company="Febrer Software">
//     Fecha: 30/11/2007
//     Path: forum\active_topics.aspx.cs
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
using FSForum;
using FSPortal;
using System.Text;
using FSForum.Includes;
using FSNetwork;

namespace FSForum
{
    public class active_topics : FSPortal.BasePage
    {
        // Dimension variables
        DataTable rsTopic;
        // Holds the Recordset for the Topic details
        DataTable rsForum;
        // Holds the forum topic details
        // Dim intForumID As Integer           'Holds the forum ID number
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
        // Dim intLinkPageNum As Integer       'Holss the page number to link to
        int intShowTopicsFrom = 0;
        // Holds when to show the topics from
        string strShowTopicsFrom = "";
        bool blnTopicLocked;
        // set to true if the topic is locked
        int intPriority;
        // Holds the priority level of the topic
        double dblActiveFrom = 0;
        // Holds the time to get active topics from
        int intNumberOfTopicPages = 0;
        // Holds the number of topic pages
        int intTopicPagesLoopCounter;
        // Holds the number of loops
        bool blnNewPost;
        // Set to true if the post is a new post since the users last visit
        // Dim intForumReadRights As Integer       'Holds the read rights of the forum
        string strForumclave;
        // Holds the clave for the forum
        string strForumPaswordCode;
        // Holds the code for the clave for the forum
        // Dim blnForumclaveOK As Boolean  'Set to true if the clave for the forum is OK
        long lngPollID;
        // Holds the topic poll id number
        System.DateTime dtmFirstEntryDate = new System.DateTime();
        // Holds the date of the first message
        int intForumGroupPermission;

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(Inicio());
        }

        public string Inicio()
        {
            StringBuilder sb = new StringBuilder();

            FSForum.Includes.common.Inicio();
            Inicializar();
            sb.AppendLine(Render());

            return sb.ToString();
        }

        private string Render()
        {
            StringBuilder sb = new StringBuilder();
            //sb.AppendLine("<html>");
            //sb.AppendLine("<head>");

            tituloPagina = Variables.Forum.strMainForumName + ": Active Topics";

            //sb.AppendLine(@"<!--include file = ""includes/header.aspx""-->");
            //sb.AppendLine(@"<navigation:navigation ID=""common1"" runat=""server"" />");
            sb.AppendLine(navigation_buttons_inc.Render());
            //<!--< table width = "< = Variables.Forum.strTableVariableWidth %>" border = "0" cellspacing = "0" cellpadding = "3" align = "center" >
            //        < tr >
            //        < td align = "left" class="heading">< = Variables.Forum.strTxtActiveTopics  ></td>
            //</tr>
            //<tr>
            //<td align = "left" width="71%" class="bold"><img src = "< = Variables.Forum.strImagePath %>open_folder_icon.gif" border="0" align="middle">&nbsp;<a href = "default.aspx" target="_self" class="boldLink">< = strMainForumName %></a>< = strNavSpacer %><a href = "active_topics.aspx" class="boldLink">< = Variables.Forum.strTxtActiveTopics %></a><br /></td>
            //</tr>
            //</table>-->
            sb.AppendLine(@"<table width = """ + Variables.Forum.strTableVariableWidth + @""" border=""0"" cellspacing=""0"" cellpadding=""4"" align=""center"">");
            sb.AppendLine("<tr>");
            sb.AppendLine("<form>");
            sb.AppendLine(@"<td><span class=""text"">" + Variables.Forum.strTxtShowActiveTopicsSince + "</span>");
            sb.AppendLine(@"<select name=""show"" onChange=""ShowTopicsAT(this);"">");
            sb.AppendLine(@"<option value=""1"" ");
            if (intShowTopicsFrom == 1)
            {
                sb.AppendLine("selected");
            }
            sb.AppendLine(">" + FuncionesFecha.DateFormat(System.DateTime.Parse(Session["dtmLastVisit"].ToString()), FuncionesFecha.saryDateTimeData) + " " + Variables.Forum.strTxtAt + " " + FuncionesFecha.TimeFormat(System.DateTime.Parse(Session["dtmLastVisit"].ToString()), FuncionesFecha.saryDateTimeData) + "</option>");
            sb.AppendLine(@"<option value=""2"" ");
            if (intShowTopicsFrom == 2)
            {
                sb.AppendLine("selected");
            }
            sb.AppendLine(">" + Variables.Forum.strTxtLastFifteenMinutes + "</option>");
            sb.AppendLine(@"<option value=""3"" ");
            if ((intShowTopicsFrom == 3))
            {
                sb.AppendLine("selected");
            }
            sb.AppendLine(">" + Variables.Forum.strTxtLastThirtyMinutes + "</option>");
            sb.AppendLine(@"<option value=""4"" ");
            if (intShowTopicsFrom == 4)
            {
                sb.AppendLine("selected");
            }
            sb.AppendLine(">" + Variables.Forum.strTxtLastFortyFiveMinutes + "</option>");
            sb.AppendLine(@"<option value=""5"" ");
            if (intShowTopicsFrom == 5)
            {
                sb.AppendLine("selected");
            }
            sb.AppendLine(">" + Variables.Forum.strTxtLastHour + "</option>");
            sb.AppendLine(@"<option value=""6"" ");
            if (intShowTopicsFrom == 6)
            {
                sb.AppendLine("selected");
            }
            sb.AppendLine(">" + Variables.Forum.strTxtLastTwoHours + "</option>");
            sb.AppendLine(@"<option value=""7"" ");
            if (intShowTopicsFrom == 7)
            {
                sb.AppendLine("selected");
            }
            sb.AppendLine(">" + Variables.Forum.strTxtLastFourHours + "</option>");
            sb.AppendLine(@"<option value=""8"" ");
            if ((intShowTopicsFrom == 8))
            {
                sb.AppendLine("selected");
            }
            sb.AppendLine(">" + Variables.Forum.strTxtLastSixHours + "</option>");
            sb.AppendLine(@"<option value=""9"" ");
            if (intShowTopicsFrom == 9)
            {
                sb.AppendLine("selected");
            }
            sb.AppendLine(">" + Variables.Forum.strTxtLastEightHours + "</option>");
            sb.AppendLine(@"<option value=""10"" ");
            if (intShowTopicsFrom == 10)
            {
                sb.AppendLine("selected");
            }
            sb.AppendLine(">" + Variables.Forum.strTxtLastTwelveHours + "</option>");
            sb.AppendLine(@"<option value=""11"" ");
            if (intShowTopicsFrom == 11)
            {
                sb.AppendLine("selected");
            }
            sb.AppendLine(">" + Variables.Forum.strTxtLastSixteenHours + "</option>");
            sb.AppendLine(@"<option value=""12"" ");
            if (intShowTopicsFrom == 12)
            {
                sb.AppendLine("selected");
            }
            sb.AppendLine(">" + Variables.Forum.strTxtYesterday + "</option>");
            sb.AppendLine(@"<option value=""13"" ");
            if (intShowTopicsFrom == 13)
            {
                sb.AppendLine("selected");
            }
            sb.AppendLine(">" + Variables.Forum.strTxtLastWeek + "</option>");
            sb.AppendLine(@"<option value = ""14"" ");
            if (intShowTopicsFrom == 14)
            {
                sb.AppendLine("selected");
            }
            sb.AppendLine(">" + Variables.Forum.strTxtLastMonth + "</option>");
            sb.AppendLine("</select></td></form></tr></table>");
            sb.AppendLine(@"<table width = """ + Variables.Forum.strTableVariableWidth + @""" border=""0"" cellspacing=""0"" cellpadding=""1"" bgcolor=""" + Variables.Forum.strTableBorderColour + @""" align=""center"">");
            sb.AppendLine("<tr>");
            sb.AppendLine("<td>");
            sb.AppendLine(@"<table width = ""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" bgcolor=""" + Variables.Forum.strTableBgColour + @""">");
            sb.AppendLine("<tr>");
            sb.AppendLine(@"<td bgcolor = """ + Variables.Forum.strTableBgColour + @""" >");
            sb.AppendLine(@"<table width=""100%"" border=""0"" cellspacing=""1"" cellpadding=""3"" height=""14"" bgcolor=""" + Variables.Forum.strTableBgColour + @""">");
            sb.AppendLine("<tr>");
            sb.AppendLine(@"<td bgcolor = """ + Variables.Forum.strTableTitleColour + @""" width=""3%"" class=""tHeading"" background=""" + Variables.Forum.strTableTitleBgImage + @"""> </td>");
            sb.AppendLine(@"<td bgcolor = """ + Variables.Forum.strTableTitleColour + @""" width=""41%"" class=""tHeading"" background=""" + Variables.Forum.strTableTitleBgImage + @""">" + Variables.Forum.strTxtTopics + "</td>");
            sb.AppendLine(@"<td bgcolor = """ + Variables.Forum.strTableTitleColour + @""" width=""15%"" class=""tHeading"" background=""" + Variables.Forum.strTableTitleBgImage + @""">" + Variables.Forum.strTxtThreadStarter + "</td>");
            sb.AppendLine(@"<td bgcolor = """ + Variables.Forum.strTableTitleColour + @""" width=""7%"" align=""center"" class=""tHeading"" background=""" + Variables.Forum.strTableTitleBgImage + @""">" + Variables.Forum.strTxtReplies + "</td>");
            sb.AppendLine(@"<td bgcolor = """ + Variables.Forum.strTableTitleColour + @""" width=""7%"" align=""center"" class=""tHeading"" background=""" + Variables.Forum.strTableTitleBgImage + @""">" + Variables.Forum.strTxtViews + "</td>");
            sb.AppendLine(@"<td bgcolor = """ + Variables.Forum.strTableTitleColour + @""" width=""29%"" align=""center"" class=""tHeading"" background=""" + Variables.Forum.strTableTitleBgImage + @""">" + Variables.Forum.strTxtLastPost + "</td>");
            sb.AppendLine("</tr>");


            //***************************************

            // Create a record set object to the Topics held in the database
            // Set rsForum = Server.CreateObject("ADODB.Recordset")
            // Set the cursor type property of the record set to dynamic so we can naviagate through the record set
            // rsForum.CursorType = 1
            FSDatabase.BdUtils db = new FSDatabase.BdUtils("FSForum");
            // Initalise the strSQL variable with an SQL statement to query the database to get the Usuarios and subject from the database for the topic
            string strSQL = "";
            if ((FSDatabase.Utils.BDType == FSDatabase.Utils.TypeBd.SQLServer))
            {
                strSQL = ("Execute "
                            + (Variables.Forum.strDbProc + ("ActiveTopics @dblActiveFrom = "
                            + (dblActiveFrom + (", @UsuariosID = "
                            + (FSPortal.Variables.User.UsuarioId + (", @GroupID = "
                            + (FSPortal.Variables.User.GroupId + (", @GroupPerm = " + intForumGroupPermission)))))))));
            }
            else
            {
                strSQL = ("SELECT "
                            + (Variables.Forum.strDbTable + ("Forum.Forum_name, "
                            + (Variables.Forum.strDbTable + ("Forum.clave, "
                            + (Variables.Forum.strDbTable + ("Forum.Forum_code, "
                            + (Variables.Forum.strDbTable + "Topic.* "))))))));
                strSQL = (strSQL + ("FROM "
                            + (Variables.Forum.strDbTable + ("Category, "
                            + (Variables.Forum.strDbTable + ("Forum, "
                            + (Variables.Forum.strDbTable + "Topic ")))))));
                strSQL = (strSQL + ("WHERE (("
                            + (Variables.Forum.strDbTable + ("Category.Cat_ID = "
                            + (Variables.Forum.strDbTable + ("Forum.Cat_ID AND "
                            + (Variables.Forum.strDbTable + ("Forum.Forum_ID = "
                            + (Variables.Forum.strDbTable + ("Topic.Forum_ID) AND ("
                            + (Variables.Forum.strDbTable + ("Topic.Last_entry_date > "
                            + (Variables.Forum.strDatabaseDateFunction + (" - "
                            + (dblActiveFrom + "))")))))))))))))));
                strSQL = (strSQL + (" AND ("
                            + (Variables.Forum.strDbTable + ("Forum.[Read] <= "
                            + (intForumGroupPermission + (" OR ("
                            + (Variables.Forum.strDbTable + "Topic.Forum_ID IN (")))))));
                strSQL = (strSQL + ("\tSELECT "
                            + (Variables.Forum.strDbTable + "Permissions.Forum_ID ")));
                strSQL = (strSQL + ("\tFROM "
                            + (Variables.Forum.strDbTable + "Permissions ")));
                strSQL = (strSQL + ("\tWHERE "
                            + (Variables.Forum.strDbTable + ("Permissions.UsuarioID="
                            + (FSPortal.Variables.User.UsuarioId + (" OR "
                            + (Variables.Forum.strDbTable + ("Permissions.Group_ID = "
                            + (FSPortal.Variables.User.GroupId + (" AND "
                            + (Variables.Forum.strDbTable + "Permissions.[Read]=TRUE))")))))))))));
                strSQL = (strSQL + "\t)");
                strSQL = (strSQL + ("ORDER BY "
                            + (Variables.Forum.strDbTable + ("Category.Cat_order ASC, "
                            + (Variables.Forum.strDbTable + ("Forum.Forum_Order ASC, "
                            + (Variables.Forum.strDbTable + "Topic.Last_entry_date DESC;")))))));
            }

            // Query the database
            rsForum = db.Execute(strSQL);
            // Initialse the string to display when active topics are shown since
            switch (intShowTopicsFrom)
            {
                case 1:
                    // Filter the recorset to leave only active topics since last vists (Filter used for overcome incompatibilty problems between application and database)
                    // rsForum.Filter = "Last_entry_date > #" & CDate(Session("dtmLastVisit")) & "#"
                    break;
                case 2:
                    // rsForum.Filter = "Last_entry_date > #" & DateAdd("n", -15, Now()) & "#"
                    break;
                case 3:
                    // rsForum.Filter = "Last_entry_date > #" & DateAdd("n", -30, Now()) & "#"
                    break;
                case 4:
                    // rsForum.Filter = "Last_entry_date > #" & DateAdd("n", -45, Now()) & "#"
                    break;
                case 5:
                    // rsForum.Filter = "Last_entry_date > #" & DateAdd("h", -1, Now()) & "#"
                    break;
                case 6:
                    // rsForum.Filter = "Last_entry_date > #" & DateAdd("h", -2, Now()) & "#"
                    break;
                case 7:
                    // rsForum.Filter = "Last_entry_date > #" & DateAdd("h", -4, Now()) & "#"
                    break;
                case 8:
                    // rsForum.Filter = "Last_entry_date > #" & DateAdd("h", -6, Now()) & "#"
                    break;
                case 9:
                    // rsForum.Filter = "Last_entry_date > #" & DateAdd("h", -8, Now()) & "#"
                    break;
                case 10:
                    // rsForum.Filter = "Last_entry_date > #" & DateAdd("h", -12, Now()) & "#"
                    break;
                case 11:
                    // rsForum.Filter = "Last_entry_date > #" & DateAdd("h", -16, Now()) & "#"
                    break;
            }
            // If there are no active topics display an error msg
            if ((rsForum.Rows.Count == 0))
            {
                // If there are no Active Topic's to display then display the appropriate error message
                sb.AppendLine(("\r\n" + ("<td bgcolor=\""
                                + (Variables.Forum.strTableColour + ("\" background=\""
                                + (Variables.Forum.strTableBgImage + ("\" colspan=\"6\" class=\"text\">"
                                + (Variables.Forum.strTxtNoActiveTopicsSince + (" "
                                + (strShowTopicsFrom + (" "
                                + (Variables.Forum.strTxtToDisplay + "</td>"))))))))))));
                return sb.ToString();
            }

            // Read in the forum ID
            Variables.Forum.intForumID = NumberUtils.NumberInt(rsForum.Rows[0]["Forum_ID"]);
            // Set the number of records to display on each page
            // rsForum.PageSize = intTopicPerPage
            // Get the record poistion to display from
            // rsForum.AbsolutePage = intRecordPositionPageNum
            // Count the number of pages there are in the recordset calculated by the PageSize attribute set above
            // intTotalNumOfPages = rsForum.PageCount
            // Craete a Recodset object for the topic details
            // rsTopic = Server.CreateObject("ADODB.Recordset")
            // Loop round to read in all the Topics in the database
            foreach (DataRow row in rsForum.Rows)
            {
                // For intRecordLoopCounter = 1 To intTopicPerPage
                // If there are no records left in the recordset to display then exit the for loop
                // If rsForum.EOF Then Exit For
                // Read in Topic details from the database
                Variables.Forum.intForumID = NumberUtils.NumberInt(row["Forum_ID"]);
                lngTopicID = NumberUtils.NumberLong(row["Topic_ID"]);
                lngPollID = NumberUtils.NumberLong(row["Poll_ID"]);
                lngNumberOfViews = NumberUtils.NumberLong(row["No_of_views"]);
                strSubject = Functions.Valor(row["Subject"]);
                blnTopicLocked = Functions.ValorBool(row["Locked"]);
                intPriority = NumberUtils.NumberInt(row["Priority"]);
                strForumclave = Functions.Valor(row["clave"]);
                strForumPaswordCode = Functions.Valor(row["Forum_code"]);
                // If the forum name is different to the one from the last forum display the forum name
                if ((row["Forum_name"].ToString() != strForumName))
                {
                    // Give the forum name the new forum name
                    strForumName = row["Forum_name"].ToString();
                    // Display the new forum name
                    sb.AppendLine(("\r\n" + ("<td bgcolor=\""
                                                    + (Variables.Forum.strTableTitleColour2 + ("\" background=\""
                                                    + (Variables.Forum.strTableTitleBgImage2 + ("\" colspan=\"6\"><a href=\"forum_topics.aspx?FID="
                                                    + (Variables.Forum.intForumID + ("\" target=\"_self\" class=\"cat\">"
                                                    + (strForumName + "</a></td>"))))))))));
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
                                + (Variables.Forum.strDbTable + ("Thread.Message_date, " + "Usuarios.usuario ")))))));
                    strSQL = (strSQL + ("FROM " + ("Usuarios INNER JOIN "
                                + (Variables.Forum.strDbTable + ("Thread ON " + ("Usuarios.UsuarioID = "
                                + (Variables.Forum.strDbTable + "Thread.UsuarioID ")))))));
                    strSQL = (strSQL + ("WHERE "
                                + (Variables.Forum.strDbTable + ("Thread.Topic_ID = "
                                + (lngTopicID + " ")))));
                    strSQL = (strSQL + ("ORDER BY "
                                + (Variables.Forum.strDbTable + "Thread.Message_date ASC;")));
                }

                // Query the database
                rsTopic = db.Execute(strSQL);
                // If there is info in the database relating to the topic then get them from the record set
                if ((rsTopic.Rows.Count > 0))
                {
                    // Read in the subject and Usuarios and number of replies from the record set
                    strTopicStartusuario = rsTopic.Rows[0]["usuario"].ToString();
                    lngTopicStartUserID = long.Parse(rsTopic.Rows[0]["UsuarioID"].ToString());
                    dtmFirstEntryDate = System.DateTime.Parse(rsTopic.Rows[0]["Message_date"].ToString());
                    lngLastEntryMessageID = NumberUtils.NumberLong(rsTopic.Rows[(rsTopic.Rows.Count - 1)]["Thread_ID"].ToString());
                    strLastEntryusuario = rsTopic.Rows[(rsTopic.Rows.Count - 1)]["usuario"].ToString();
                    lngLastEntryUserID = NumberUtils.NumberLong(rsTopic.Rows[(rsTopic.Rows.Count - 1)]["UsuarioID"].ToString());
                    dtmLastEntryDate = System.DateTime.Parse(rsTopic.Rows[(rsTopic.Rows.Count - 1)]["Message_date"].ToString());
                }

                // Set the booleon varible if this is a new post since the users last visit and has not been read
                if ((Request.Cookies["RT"] == null))
                {
                    blnNewPost = false;
                }
                else if (((System.DateTime.Parse(Session["dtmLastVisit"].ToString()) < dtmLastEntryDate)
                            && (Request.Cookies["RT"][("TID" + lngTopicID)] == "")))
                {
                    blnNewPost = true;
                }
                else
                {
                    blnNewPost = false;
                }

                // Write the HTML of the Topic descriptions as hyperlinks to the Topic details and message
                sb.AppendLine("\r\n" + "\t<tr>");
                sb.AppendLine("\r\n" + "\t<td bgcolor=\"");
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
                // If the user is a forum admin then give let them delete the topic
                if (FSPortal.Variables.User.Administrador)
                {
                    sb.AppendLine(("      <a href=\"javascript:openWin(\'pop_up_topic_admin.aspx?TID="
                                    + (lngTopicID + ("\',\'admin\',\'toolbar=0,location=0,status=0,menubar=0,scrollbars=1,resizable=1,width=590,height=425\')\"><" +
                                    "img src=\""
                                    + (Variables.Forum.strImagePath + ("small_admin_icon.gif\" align=\"middle\" border=\"0\" alt=\""
                                    + (Variables.Forum.strTxtTopicAdmin + "\"></a>")))))));
                }

                // If there is a poll display a poll text
                if ((lngPollID != 0))
                {
                    sb.AppendLine(Variables.Forum.strTxtPoll);
                }

                // Display the subject of the topic
                sb.AppendLine(("\r\n" + ("\t<a href=\"forum_posts.aspx?TID=" + lngTopicID)));
                if ((intPriority == 3))
                {
                    sb.AppendLine(("&FID="
                                    + (Variables.Forum.intForumID + "&PR=3")));
                }

                sb.AppendLine(("\" target=\"_self\" title=\""
                                + (Variables.Forum.strTxtThisTopicWasStarted
                                + (FuncionesFecha.DateFormat(dtmFirstEntryDate, FuncionesFecha.saryDateTimeData) + (" "
                                + (Variables.Forum.strTxtAt + (" "
                                + (FuncionesFecha.TimeFormat(dtmFirstEntryDate, FuncionesFecha.saryDateTimeData) + ("\">"
                                + (strSubject + "</a>"))))))))));
                // Calculate the number of pages for the topic and display links if there are more than 1 page
                // intNumberOfTopicPages = ((lngNumberOfReplies + 1) \ Variables.Forum.intThreadsPerPage)
                // If there is a remainder from calculating the num of pages add 1 to the number of pages
                if ((((lngNumberOfReplies + 1)
                            % Variables.Forum.intThreadsPerPage)
                            > 0))
                {
                    intNumberOfTopicPages = (intNumberOfTopicPages + 1);
                }

                // If there is more than 1 page for the topic display links to the other pages
                if ((intNumberOfTopicPages > 1))
                {
                    sb.AppendLine(("<br /><img src=\""
                                    + (Variables.Forum.strImagePath + ("pages_icon.gif\" align=\"middle\" alt=\""
                                    + (Variables.Forum.strTxtPages + "\">")))));
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
                                                + (lngTopicID + "&TPN=8"))));
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
                                                + (lngTopicID + ("&TPN="
                                                + (intNumberOfTopicPages - 1))))));
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
                                                + (lngTopicID + ("&TPN=" + intNumberOfTopicPages)))));
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
                                        + (lngTopicID + ("&TPN=" + intTopicPagesLoopCounter)))));
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
            //***************************************

            sb.AppendLine("</td>");
            sb.AppendLine(@"<td bgcolor =""");

            if ((intRecordLoopCounter % 2) == 0)
            {
                sb.AppendLine(Variables.Forum.strTableEvenRowColour);
            }
            else
            {
                sb.AppendLine(Variables.Forum.strTableOddRowColour);
            }
            sb.AppendLine(@""" background =""" + Variables.Forum.strTableBgImage + @""" width=""15%"" class=""text""><a href=""JavaScript:openWin('pop_up_profile.aspx?PF=" + lngTopicStartUserID + "&FID=" + Variables.Forum.intForumID + @"','profile','toolbar=0,location=0,status=0,menubar=0,scrollbars=1,resizable=1,width=590,height=425')"" title=""" + Variables.Forum.strTxtThisTopicWasStarted
                        + FuncionesFecha.DateFormat(dtmFirstEntryDate, FuncionesFecha.saryDateTimeData) + " "
                        + Variables.Forum.strTxtAt + " " + FuncionesFecha.TimeFormat(dtmFirstEntryDate, FuncionesFecha.saryDateTimeData) + @""">" + strTopicStartusuario + "</a></td>");
            sb.AppendLine(@"<td bgcolor = """);
            if ((intRecordLoopCounter % 2) == 0)
            {
                sb.AppendLine(Variables.Forum.strTableEvenRowColour);
            }
            else
            {
                sb.AppendLine(Variables.Forum.strTableOddRowColour);
            }
            sb.AppendLine(@""" background =""" + Variables.Forum.strTableBgImage + @""" width=""7%"" align=""center"" class=""text"">" + lngNumberOfReplies + "</td>");
            sb.AppendLine(@"<td bgcolor = """);
            if ((intRecordLoopCounter % 2) == 0)
            {
                sb.AppendLine(Variables.Forum.strTableEvenRowColour);
            }
            else
            {
                sb.AppendLine(Variables.Forum.strTableOddRowColour);
            }
            sb.AppendLine(@""" background =""" + Variables.Forum.strTableBgImage + @""" width=""7%"" align=""center"" class=""text"">" + lngNumberOfViews + "</td>");
            sb.AppendLine(@"<td bgcolor = """);
            if ((intRecordLoopCounter % 2) == 0)
            {
                sb.AppendLine(Variables.Forum.strTableEvenRowColour);
            }
            else
            {
                sb.AppendLine(Variables.Forum.strTableOddRowColour);
            }
            sb.AppendLine(@""" background =""" + Variables.Forum.strTableBgImage + @""" width=""29%"" class=""smText"" align=""right"" nowrap=""nowrap"">" + FuncionesFecha.DateFormat(dtmLastEntryDate, FuncionesFecha.saryDateTimeData) + " "
                        + Variables.Forum.strTxtAt + " " + FuncionesFecha.TimeFormat(dtmLastEntryDate, FuncionesFecha.saryDateTimeData));
            sb.AppendLine("<br />" + Variables.Forum.strTxtBy + @" <a href = ""JavaScript:openWin('pop_up_profile.aspx?PF=" + lngLastEntryUserID + @"','profile','toolbar=0,location=0,status=0,menubar=0,scrollbars=1,resizable=1,width=590,height=425')"" class=""smLink"">" + strLastEntryusuario + @"</a> <a href = ""forum_posts.aspx?TID=" + lngTopicID + "&get=last#" + lngLastEntryMessageID + @""" target=""_self"" class=""smLink""><img src = """ + Variables.Forum.strImagePath + @"right_arrow.gif"" align=""middle"" border=""0"" alt=""" + Variables.Forum.strTxtViewLastPost + @"""></a></td>");
            sb.AppendLine("</tr>");

            sb.AppendLine("</table>");
            sb.AppendLine("</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</table>");
            sb.AppendLine("</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</table>");
            sb.AppendLine("<br />");
            sb.AppendLine("<form>");
            sb.AppendLine(@"<table width = """ + Variables.Forum.strTableVariableWidth + @""" border=""0"" cellspacing=""0"" cellpadding=""4"" align=""center"">");
            sb.AppendLine("<tr>");
            sb.AppendLine("<td>");
            sb.AppendLine(FSForum.Includes.forum_jump_inc.Render());
            sb.AppendLine("</td>");

            // Release server objects
            // rsCommon = Nothing
            // adoCon.Close
            // Set adoCon = Nothing
            // If there is more than 1 page of topics then dispaly drop down list to the other topics
            if ((intTotalNumOfPages > 1))
            {
                // Display an image link to the last topic
                sb.AppendLine(("\r\n" + "\t\t<td align=\"right\" class=\"text\" nowrap=\"nowrap\">"));
                // Display a prev link if previous pages are available
                if ((intRecordPositionPageNum > 1))
                {
                    sb.AppendLine(("<a href=\"active_topics.aspx?PN="
                                + ((intRecordPositionPageNum - 1) + ("\"><&lt "
                                + (Variables.Forum.strTxtPrevious + "</a> ")))));
                }

                sb.AppendLine((Variables.Forum.strTxtPage + " <select onChange=\"ForumJump(this)\" name=\"SelectTopicPage\">"));
                // Loop round to display links to all the other pages
                for (intTopicPageLoopCounter = 1; (intTopicPageLoopCounter <= intTotalNumOfPages); intTopicPageLoopCounter++)
                {
                    // Display a link in the link list to the another topic page
                    sb.AppendLine(("\r\n" + ("\t\t  <option value=\"active_topics.aspx?PN="
                                + (intTopicPageLoopCounter + "\""))));
                    // If this page number to display is the same as the page being displayed then make sure it's selected
                    if ((intTopicPageLoopCounter == intRecordPositionPageNum))
                    {
                        sb.AppendLine(" selected");
                    }

                    // Display the link page number
                    sb.AppendLine((">"
                                + (intTopicPageLoopCounter + "</option>")));
                }

                // End the drop down list
                sb.AppendLine(("\r\n" + ("\t\t</select> "
                            + (Variables.Forum.strTxtOf + (" " + intTotalNumOfPages)))));
                // Display a next link if needed
                if ((intRecordPositionPageNum != intTotalNumOfPages))
                {
                    sb.AppendLine((" <a href=\"active_topics.aspx?PN="
                                + ((intRecordPositionPageNum + 1) + ("\">"
                                + (Variables.Forum.strTxtNext + " >></a>")))));
                }

                sb.AppendLine("</td>");
            }

            sb.AppendLine("</tr>");
            sb.AppendLine("</table>");
            sb.AppendLine("</form>");
            sb.AppendLine(@"<div align=""center"">");
            sb.AppendLine(@"<table width=""80%"" border=""0"" cellspacing=""0"" cellpadding=""2"">");
            sb.AppendLine("<tr>");
            sb.AppendLine(@"<td class=""text""><img src = """ + Variables.Forum.strImagePath + @"no_new_posts_icon.gif"" alt=""" + Variables.Forum.strTxtOpenTopic + @"""> <span class=""smText"">" + Variables.Forum.strTxtOpenTopic + "</span></td>");
            sb.AppendLine(@"<td class=""text""><img src = """ + Variables.Forum.strImagePath + @"hot_topic_no_new_posts_icon.gif"" alt=""" + Variables.Forum.strTxtHotTopic + @"""> <span class=""smText"">" + Variables.Forum.strTxtHotTopic + "</span></td>");
            sb.AppendLine(@"<td class=""text""><img src = """ + Variables.Forum.strImagePath + @"priority_post_icon.gif"" alt=""" + Variables.Forum.strTxtHighPriorityPost + @"""> <span class=""smText"">" + Variables.Forum.strTxtHighPriorityPost + "</span></td>");
            sb.AppendLine(@"<td class=""text""><img src = """ + Variables.Forum.strImagePath + @"pinned_topic_icon.gif"" alt=""" + Variables.Forum.strTxtPinnedTopic + @"""> <span class=""smText"">" + Variables.Forum.strTxtPinnedTopic + "</span></td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("<tr>");
            sb.AppendLine(@"<td class=""text""><img src = """ + Variables.Forum.strImagePath + @"new_posts_icon.gif"" alt=""" + Variables.Forum.strTxtOpenTopicNewReplies + @"> <span class=""smText"">" + Variables.Forum.strTxtOpenTopicNewReplies + "</span></td>");
            sb.AppendLine(@"<td class=""text""><img src = """ + Variables.Forum.strImagePath + @"hot_topic_new_posts_icon.gif"" alt=""" + Variables.Forum.strTxtHotTopicNewReplies + @"> <span class=""smText"">" + Variables.Forum.strTxtHotTopicNewReplies + "</span></td>");
            sb.AppendLine(@"<td class=""text""><img src = """ + Variables.Forum.strImagePath + @"priority_post_locked_icon.gif"" alt=""" + Variables.Forum.strTxtHighPriorityPostLocked + @"> <span class=""smText"">" + Variables.Forum.strTxtHighPriorityPostLocked + "</span></td>");
            sb.AppendLine(@"<td class=""text""><img src = """ + Variables.Forum.strImagePath + @"closed_topic_icon.gif"" alt=""" + Variables.Forum.strTxtLockedTopic + @"> <span class=""smText"">" + Variables.Forum.strTxtLockedTopic + "</span></td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</table>");
            sb.AppendLine("<br />");
            sb.AppendLine("</div>");
            sb.AppendLine(@"<div align = ""center"" >");

            // Display the process time
            // If blnShowProcessTime Then response.write("<span class=""smText""><br /><br />" & Variables.Forum.strTxtThisPageWasGeneratedIn & " " & FormatNumber(Timer() - dblStartTime, 4) & " " & Variables.Forum.strTxtSeconds & "</span>"}

            sb.AppendLine("</div>");

            return sb.ToString();
        }

        private void Inicializar()
        {
            // Holds the group permisison level for forums
            // If this is the first time the page is displayed then the Forum Topic record position is set to page 1
            if (((Request.QueryString["PN"] == "")
                        || (Web.RequestInt(Request.QueryString["PN"]) == 0)))
            {
                intRecordPositionPageNum = 1;
                // Else the page has been displayed before so the Forum Topic record postion is set to the Record Position number
            }
            else
            {
                intRecordPositionPageNum = int.Parse(Request.QueryString["PN"]);
            }

            // Initliase the forum groip permisions
            // If guest group
            if ((FSPortal.Variables.User.GroupId == 2))
            {
                intForumGroupPermission = 1;
                // If admin group
            }
            else if ((FSPortal.Variables.User.GroupId == 1))
            {
                intForumGroupPermission = 4;
                // All other groups
            }
            else
            {
                intForumGroupPermission = 2;
            }

            // Get what date to show active topics till from cookie
            if ((Web.Cookie(Request.Cookies["AT"]) != ""))
            {
                intShowTopicsFrom = NumberUtils.NumberInt(Web.Cookie(Request.Cookies["AT"]));
                // If this is not the first time the user has visted then use this date to show active topics from
                // ElseIf CDate(Session("dtmLastVisit")) < CDate(Functions.ValorCookie(Request.Cookies(Var.strCookieName), "LTVST")) Then
                // intShowTopicsFrom = 1 '1 = last visit
                // Else
                // intShowTopicsFrom = 7 '7 = yesterday
            }

            // Initialse the string to display when active topics are shown since
            switch (intShowTopicsFrom)
            {
                case 1:
                    strShowTopicsFrom = (Variables.Forum.strTxtLastVisitOn + (" "
                                + (FuncionesFecha.DateFormat(System.DateTime.Parse(Session["dtmLastVisit"].ToString()), FuncionesFecha.saryDateTimeData) + (" "
                                + (Variables.Forum.strTxtAt + (" " + FuncionesFecha.TimeFormat(System.DateTime.Parse(Session["dtmLastVisit"].ToString()), FuncionesFecha.saryDateTimeData)))))));
                    dblActiveFrom = (System.DateTime.Now - System.DateTime.Parse(Session["dtmLastVisit"].ToString())).TotalDays + 1;
                    break;
                case 2:
                    strShowTopicsFrom = Variables.Forum.strTxtLastFifteenMinutes;
                    dblActiveFrom = 1;
                    break;
                case 3:
                    strShowTopicsFrom = Variables.Forum.strTxtLastThirtyMinutes;
                    dblActiveFrom = 1;
                    break;
                case 4:
                    strShowTopicsFrom = Variables.Forum.strTxtLastFortyFiveMinutes;
                    dblActiveFrom = 1;
                    break;
                case 5:
                    strShowTopicsFrom = Variables.Forum.strTxtLastHour;
                    dblActiveFrom = 1;
                    break;
                case 6:
                    strShowTopicsFrom = Variables.Forum.strTxtLastTwoHours;
                    dblActiveFrom = 1;
                    break;
                case 7:
                    strShowTopicsFrom = Variables.Forum.strTxtLastFourHours;
                    dblActiveFrom = 1;
                    break;
                case 8:
                    strShowTopicsFrom = Variables.Forum.strTxtLastSixHours;
                    dblActiveFrom = 1;
                    break;
                case 9:
                    strShowTopicsFrom = Variables.Forum.strTxtLastEightHours;
                    dblActiveFrom = 1;
                    break;
                case 10:
                    strShowTopicsFrom = Variables.Forum.strTxtLastTwelveHours;
                    dblActiveFrom = 1;
                    break;
                case 11:
                    strShowTopicsFrom = Variables.Forum.strTxtLastSixteenHours;
                    dblActiveFrom = 1;
                    break;
                case 12:
                    strShowTopicsFrom = Variables.Forum.strTxtYesterday;
                    dblActiveFrom = 1;
                    break;
                case 13:
                    strShowTopicsFrom = Variables.Forum.strTxtLastWeek;
                    dblActiveFrom = 7;
                    break;
                case 14:
                    strShowTopicsFrom = Variables.Forum.strTxtLastMonth;
                    dblActiveFrom = 28;
                    break;
            }
        }
    }
}