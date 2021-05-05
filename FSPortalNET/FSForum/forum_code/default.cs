// <fileheader>
// <copyright file="default.aspx.cs" company="Febrer Software">
//     Fecha: 30/11/2007
//     Path: forum\default.aspx.cs
//     Copyright (c) 2003-2007 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>
using FSLibrary;
using FSPortal;
using FSForum;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using FSForum.Includes;
using FSNetwork;
using FSDatabase;

namespace FSForum
{
    public class _Default : FSPortal.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            contenido = Inicio();
        }
        public string Inicio()
        {
            StringBuilder sb = new StringBuilder();
            // Dimension variables
            DataTable rsCategory;
            // Holds the categories for the forums
            DataTable rsForum;
            // Holds the Recordset for the forum details
            DataTable rsCommon;
            // Dim intForumID As Integer           'Holds the forum ID number
            string strCategory;
            // Holds the category name
            int intCatID;
            // Holds the id for the category
            string strForumName;
            // Holds the forum name
            string strForumDiscription;
            // Holds the forum description
            string strForumclave;
            // Holds the forum clave if there is one
            string strModeratorsList;
            // Holds a list of moderators for the forum
            System.DateTime dtmForumStartDate;
            // Holds the forum start date
            long lngNumberOfTopics;
            // Holds the number of topics in a forum
            long lngNumberOfPosts;
            // Holds the number of Posts in the forum
            long lngTotalNumberOfTopics = 0;
            // Holds the total number of topics in a forum
            long lngTotalNumberOfPosts = 0;
            // Holds the total number of Posts in the forum
            int intNumberofForums = 0;
            // Holds the number of forums
            long lngLastEntryMeassgeID = -1;
            // Holds the message ID of the last entry
            long lngLastEntryTopicID;
            // Holds the topic ID of the last entry
            System.DateTime dtmLastEntryDate;
            // Holds the date of the last entry to the forum
            string strLastEntryUser;
            // Holds the the usuario of the user who made the last entry
            long lngLastEntryUserID;
            // Holds the ID number of the last user to make and entry
            System.DateTime dtmLastEntryDateAllForums = new System.DateTime();
            // Holds the date of the last entry to all fourms
            string strLastEntryUserAllForums = "";
            long lngLastEntryUserIDAllForums = -1;
            // Holds the ID number of the last user to make and entry to all forums
            bool blnForumLocked;
            // Set to true if the forum is locked
            int intForumColourNumber = 0;
            // Holds the number to calculate the table row colour
            int intForumReadRights;
            // Holds the interger number to calculate if the user has read rights on the forum
            int intForumPostRights;
            // Holds the interger valuse to calculate if the suer can poist in the forum
            int intForumReplyRights;
            // Holds the interger value to calculate if the user can reply to a post
            bool blnHideForum;
            // Set to true if this is a hidden forum
            int intCatShow;
            // Holds the ID number of the category to show if only showing one category
            //int intActiveUsers = 0;
            // Holds the number of active users
            //int intActiveGuests = 0;
            // Holds the number of active guests
            //int intActiveMembers = 0;
            // Holds the number of logged in active members
            // Dim strMembersOnline as string        'Holds the names of the members online
            // Initialise variables
            //lngTotalNumberOfTopics = 0;
            //lngTotalNumberOfPosts = 0;
            //intNumberofForums = 0;
            //intForumColourNumber = 0;
            //intActiveMembers = 0;
            //intActiveGuests = 0;
            //intActiveUsers = 0;
            // Read in the category to show
            if ((Web.Request(Request.QueryString["C"]) != ""))
            {
                intCatShow = int.Parse(Request.QueryString["C"]);
            }
            else
            {
                intCatShow = 0;
            }

            FuncionesFecha.Init();
            FSDatabase.BdUtils db = new FSDatabase.BdUtils("FSForum");
            string strSQL;
            // Read the various categories from the database
            // Initalise the strSQL variable with an SQL statement to query the database
            if ((FSDatabase.Utils.BDType == FSDatabase.Utils.TypeBd.SQLServer))
            {
                strSQL = ("Execute " + (Variables.Forum.strDbProc + "CategoryAll"));
            }
            else
            {
                strSQL = ("SELECT "
                            + (Variables.Forum.strDbTable + ("Category.Cat_name, "
                            + (Variables.Forum.strDbTable + ("Category.Cat_ID FROM "
                            + (Variables.Forum.strDbTable + ("Category ORDER BY "
                            + (Variables.Forum.strDbTable + "Category.Cat_order ASC;"))))))));
            }

            // Query the database
            rsCategory = db.Execute(strSQL);
            //sb.AppendLine("<title>");
            tituloPagina = Variables.Forum.strMainForumName;
            //sb.AppendLine("</title>");
            //sb.AppendLine(@"<navigation:navigation ID=""navigation1"" runat=""server"" />");
            sb.AppendLine(navigation_buttons_inc.Render());
            sb.AppendLine(("\r\n" + ("   <table width=\""
                                    + (Variables.Forum.strTableVariableWidth + "\" border=\"0\" cellspacing=\"4\" cellpadding=\"3\" align=\"center\">"))));
            sb.AppendLine(("\r\n" + "    <tr>"));
            // sb.AppendLine(vbCrLf & "    <td class=""smText""> " & Variables.Forum.strTxtTheTimeNowIs & " " & FuncionesFecha.DateFormat(now(), FuncionesFecha.saryDateTimeData) & " " & Variables.Forum.strTxtAt & " " & FuncionesFecha.TimeFormat(now(), FuncionesFecha.saryDateTimeData) & ".<br />")
            // If this is not the first time the user has visted the site display the last visit time and date
            // If Functions.Valor(Session("dtmLastVisit")) < CDate(Functions.ValorCookie(Request.Cookies(Var.strCookieName),"LTVST")) Then
            // sb.AppendLine(Variables.Forum.strTxtYouLastVisitedOn & " " & FuncionesFecha.DateFormat(Session("dtmLastVisit"), FuncionesFecha.saryDateTimeData) & " " & Variables.Forum.strTxtAt & " " & FuncionesFecha.TimeFormat(Session("dtmLastVisit"), FuncionesFecha.saryDateTimeData) & ".")
            // End If
            // Display main page link if in a category view
            // If intCatShow <> 0 Then sb.AppendLine("<br /><span class=""bold""><img src=""" & Variables.Forum.strImagePath & "open_folder_icon.gif"" border=""0"" align=""middle"" /> <a href=""default.aspx"" target=""_self"" class=""boldLink"">" & strMainForumName & "</a></span>")
            sb.AppendLine(("\r\n" + "  <br /><br /></td>"));
            // If the user has not logged in (guest user ID = 2) then show them a quick login form
            if ((FSPortal.Variables.User.UsuarioId == 2))
            {
                // sb.AppendLine(vbCrLf & "    <td align=""right"" class=""smText""><form method=""post"" name=""frmLogin"" action=""login_user.aspx"">")
                // If blnLongSecurityCode Then sb.AppendLine(Variables.Forum.strTxtLogin) Else sb.AppendLine(Variables.Forum.strTxtQuickLogin)
                // sb.AppendLine(" " & _
                // vbCrLf & "        <input type=""text"" size=""10"" name=""name"" class=""smText"" />" & _
                // vbCrLf & "          <input type=""clave"" size=""10"" name=""clave"" class=""smText"" />" & _
                // vbCrLf & "        <input type=""hidden"" name=""QUIK"" value=""true"" />" & _
                // vbCrLf & "        <input type=""hidden"" name=""NS"" value=""1"" />")
                // If blnLongSecurityCode = false Then sb.AppendLine(vbCrLf & "        <input type=""hidden"" name=""sessionID"" value=""" & Session.SessionID & """ />")
                // sb.AppendLine(vbCrLf & "        <input type=""submit"" value=""" & Variables.Forum.strTxtGo & """ class=""smText"" />" & _
                // vbCrLf & "    </form></td>")    
            }

            sb.AppendLine(("\r\n" + "    </tr>"));
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
            sb.AppendLine(@"<table width=""100%"" border=""0"" cellspacing=""1"" cellpadding=""3"" height=""14"" bgcolor=""");
            sb.AppendLine(Variables.Forum.strTableBgColour);
            sb.AppendLine(@""">");
            sb.AppendLine("<tr>");
            sb.AppendLine(@"<td bgcolor=""");
            sb.AppendLine(Variables.Forum.strTableTitleColour);
            sb.AppendLine(@""" width=""1%"" class=""tHeading"" background=""");
            sb.AppendLine(Variables.Forum.strTableTitleBgImage);
            sb.AppendLine(@"""> </td>");
            sb.AppendLine(@"<td bgcolor=""");
            sb.AppendLine(Variables.Forum.strTableTitleColour);
            sb.AppendLine(@""" width=""56%"" class=""tHeading"" background=""");
            sb.AppendLine(Variables.Forum.strTableTitleBgImage);
            sb.AppendLine(@""">");
            sb.AppendLine(Variables.Forum.strTxtForum);
            sb.AppendLine("</td>");
            sb.AppendLine(@"<td bgcolor=""");
            sb.AppendLine(Variables.Forum.strTableTitleColour);
            sb.AppendLine(@""" width=""7%"" align=""center"" class=""tHeading"" background=""");
            sb.AppendLine(Variables.Forum.strTableTitleBgImage);
            sb.AppendLine(@""">");
            sb.AppendLine(Variables.Forum.strTxtTopics);
            sb.AppendLine("</td>");
            sb.AppendLine(@"<td bgcolor=""");
            sb.AppendLine(Variables.Forum.strTableTitleColour);
            sb.AppendLine(@""" width=""7%"" align=""center"" class=""tHeading"" background=""");
            sb.AppendLine(Variables.Forum.strTableTitleBgImage);
            sb.AppendLine(@""">");
            sb.AppendLine(Variables.Forum.strTxtPosts);
            sb.AppendLine("</td>");
            sb.AppendLine(@"<td bgcolor=""");
            sb.AppendLine(Variables.Forum.strTableTitleColour);
            sb.AppendLine(@""" width=""29%"" class=""tHeading"" align=""center"" background=""");
            sb.AppendLine(Variables.Forum.strTableTitleBgImage);
            sb.AppendLine(@""">");
            sb.AppendLine(Variables.Forum.strTxtLastPost);
            sb.AppendLine("</td>");
            sb.AppendLine("</tr>");
            // Check there are categories to display
            if ((rsCategory.Rows.Count == 0))
            {
                // If there are no categories to display then display the appropriate error message
                sb.AppendLine(("\r\n" + ("<tr><td bgcolor=\""
                                + (Variables.Forum.strTableColour + ("\" background=\""
                                + (Variables.Forum.strTableBgImage + ("\" colspan=\"5\" class=\"text\">"
                                + (Variables.Forum.strTxtNoForums + "</td></tr>"))))))));
                // Else there the are categories so write the HTML to display categories and the forum names and a discription
            }
            else
            {
                // Loop round to read in all the categories in the database
                foreach (DataRow row in rsCategory.Rows)
                {
                    // Get the category name from the database
                    strCategory = row["Cat_name"].ToString();
                    intCatID = int.Parse(row["Cat_ID"].ToString());
                    sb.AppendLine(("\r\n" + ("<tr><td bgcolor=\""
                                    + (Variables.Forum.strTableTitleColour2 + ("\" background=\""
                                    + (Variables.Forum.strTableTitleBgImage2 + ("\" colspan=\"5\"><a href=\"default.aspx?C="
                                    + (intCatID + ("\" target=\"_self\" class=\"cat\">"
                                    + (strCategory + "</a></td></tr>"))))))))));
                    // If there this is the cat to show then show it
                    if (((intCatShow == intCatID)
                                || (intCatShow == 0)))
                    {
                        // Read the various forums from the database
                        // Initalise the strSQL variable with an SQL statement to query the database
                        if ((FSDatabase.Utils.BDType == FSDatabase.Utils.TypeBd.SQLServer))
                        {
                            strSQL = ("Execute "
                                        + (Variables.Forum.strDbProc + ("ForumsAllWhereCatIs @intCatID = " + intCatID)));
                        }
                        else
                        {
                            strSQL = ("SELECT "
                                        + (Variables.Forum.strDbTable + ("Forum.* FROM "
                                        + (Variables.Forum.strDbTable + ("Forum WHERE "
                                        + (Variables.Forum.strDbTable + ("Forum.Cat_ID = "
                                        + (intCatID + (" ORDER BY "
                                        + (Variables.Forum.strDbTable + "Forum.Forum_Order ASC;"))))))))));
                        }

                        // Query the database
                        rsForum = db.Execute(strSQL);
                        // Check there are forum's to display
                        if ((rsForum.Rows.Count == 0))
                        {
                            // If there are no forum's to display then display the appropriate error message
                            sb.AppendLine(("\r\n" + ("<tr><td bgcolor=\""
                                            + (Variables.Forum.strTableColour + ("\"  background=\""
                                            + (Variables.Forum.strTableBgImage + ("\" colspan=\"5\" class=\"text\">"
                                            + (Variables.Forum.strTxtNoForums + "</td></tr>"))))))));
                            // Else there the are forum's to write the HTML to display it the forum names and a discription
                        }
                        else
                        {
                            // Loop round to read in all the forums in the database
                            foreach (DataRow rowForum in rsForum.Rows)
                            {
                                // Initialise variables
                                lngLastEntryTopicID = 0;
                                strModeratorsList = "";
                                Variables.Forum.intForumID = int.Parse(rowForum["Forum_ID"].ToString());
                                strForumName = rowForum["Forum_name"].ToString();
                                strForumDiscription = rowForum["Forum_description"].ToString();
                                dtmForumStartDate = System.DateTime.Parse(rowForum["Date_Started"].ToString());
                                strForumclave = rowForum["clave"].ToString();
                                lngNumberOfPosts = long.Parse(rowForum["No_of_posts"].ToString());
                                lngNumberOfTopics = long.Parse(rowForum["No_of_topics"].ToString());
                                blnForumLocked = bool.Parse(rowForum["Locked"].ToString());
                                intForumReadRights = int.Parse(rowForum["Read"].ToString());
                                intForumPostRights = int.Parse(rowForum["Post"].ToString());
                                intForumReplyRights = int.Parse(rowForum["Reply_posts"].ToString());
                                blnHideForum = bool.Parse(rowForum["Hide"].ToString());
                                FuncionesForum.forumPermisisons(Variables.Forum.intForumID, FSPortal.Variables.User.GroupId, intForumReadRights, intForumPostRights, intForumReplyRights, 0, 0, 0, 0, 0, 0, 0);
                                // Add all the posts and topics together to get the total number for the stats at the bottom of the page
                                lngTotalNumberOfPosts = (lngTotalNumberOfPosts + lngNumberOfPosts);
                                lngTotalNumberOfTopics = (lngTotalNumberOfTopics + lngNumberOfTopics);
                                // If this forum is to be hidden and but the user is allowed access to it set the hidden boolen back to false
                                if (((blnHideForum == true)
                                            && (Variables.Forum.blnRead == true)))
                                {
                                    blnHideForum = false;
                                }

                                if ((blnHideForum == false))
                                {
                                    // Get the row number
                                    intForumColourNumber = (intForumColourNumber + 1);
                                    // Initilaise variables for the information required for each forum
                                    dtmLastEntryDate = dtmForumStartDate;
                                    strLastEntryUser = Variables.Forum.strTxtForumAdministrator;
                                    lngLastEntryUserID = 1;
                                    // Get the List of Group Moderators for the Forum
                                    if (Variables.Forum.blnShowMod)
                                    {
                                        // Initalise the strSQL variable with an SQL statement to query the database to get the moderators for this forum
                                        if ((FSDatabase.Utils.BDType == FSDatabase.Utils.TypeBd.SQLServer))
                                        {
                                            strSQL = ("Execute "
                                                        + (Variables.Forum.strDbProc + ("ModeratorGroup @Variables.Forum.intForumID = " + Variables.Forum.intForumID)));
                                        }
                                        else
                                        {
                                            strSQL = ("SELECT "
                                                        + (Variables.Forum.strDbTable + ("Group.Group_ID, "
                                                        + (Variables.Forum.strDbTable + "Group.Name "))));
                                            strSQL = (strSQL + ("FROM "
                                                        + (Variables.Forum.strDbTable + ("Group, "
                                                        + (Variables.Forum.strDbTable + "Permissions ")))));
                                            strSQL = (strSQL + ("WHERE "
                                                        + (Variables.Forum.strDbTable + ("Group.Group_ID = "
                                                        + (Variables.Forum.strDbTable + ("Permissions.Group_ID AND "
                                                        + (Variables.Forum.strDbTable + ("Permissions.Moderate = True AND "
                                                        + (Variables.Forum.strDbTable + ("Permissions.Forum_ID = "
                                                        + (Variables.Forum.intForumID + ";")))))))))));
                                        }

                                        // Query the database
                                        rsCommon = db.Execute(strSQL);
                                        // Initlaise the Moderators List varible if there are records returned for the forum
                                        if ((rsCommon.Rows.Count > 0))
                                        {
                                            strModeratorsList = ("<br /><span class=\"smText\">"
                                                        + (Variables.Forum.strTxtModerators + ":</span>"));
                                        }

                                        foreach (DataRow rowC in rsCommon.Rows)
                                        {
                                            // Place the moderators usuario into the string
                                            strModeratorsList = (strModeratorsList + (" <a href=\"membedt.aspx?GID="
                                                        + (rowC["Group_ID"].ToString() + ("\" class=\"smLink\">"
                                                        + (rowC["Name"].ToString() + "</a>")))));
                                        }

                                        // Initalise the strSQL variable with an SQL statement to query the database to get the moderators for this forum
                                        if ((FSDatabase.Utils.BDType == FSDatabase.Utils.TypeBd.SQLServer))
                                        {
                                            strSQL = ("Execute "
                                                        + (Variables.Forum.strDbProc + ("Moderators @Variables.Forum.intForumID = " + Variables.Forum.intForumID)));
                                        }
                                        else
                                        {
                                            strSQL = ("SELECT "
                                                        + (Variables.Forum.strDbTable + "Permissions.UsuarioID "));
                                            strSQL = (strSQL + ("FROM "
                                                        + (Variables.Forum.strDbTable + "Permissions ")));
                                            strSQL = (strSQL + ("WHERE "
                                                        + (Variables.Forum.strDbTable + ("Permissions.Moderate = True AND "
                                                        + (Variables.Forum.strDbTable + ("Permissions.Forum_ID = "
                                                        + (Variables.Forum.intForumID + ";")))))));
                                        }

                                        // Query the database
                                        rsCommon = db.Execute(strSQL);
                                        // Initlaise the Moderators List varible if there are records returned for the forum
                                        if (((rsCommon.Rows.Count > 0)
                                                    && (strModeratorsList == "")))
                                        {
                                            strModeratorsList = ("<br /><span class=\"smText\">"
                                                        + (Variables.Forum.strTxtModerators + ":</span>"));
                                        }

                                        foreach (DataRow rowC in rsCommon.Rows)
                                        {
                                            // Place the moderators usuario into the string
                                            strModeratorsList = (strModeratorsList + (" <a href=\"JavaScript:openWin(\'pop_up_profile.aspx?PF="
                                                        + (rowC["UsuarioID"].ToString() + ("\',\'profile\',\'toolbar=0,location=0,status=0,menubar=0,scrollbars=1,resizable=1,width=590,height=425\')\"" +
                                                        " class=\"smLink\">"
                                                        + (rowC["usuarioId"].ToString() + "</a>")))));
                                        }

                                    }

                                    // Initalise the strSQL variable with an SQL statement to query the database for the date of the last entry and the Usuarios for the thread
                                    if ((FSDatabase.Utils.BDType == FSDatabase.Utils.TypeBd.SQLServer))
                                    {
                                        strSQL = ("Execute "
                                                    + (Variables.Forum.strDbProc + ("LastForumPostEntry @Variables.Forum.intForumID = " + Variables.Forum.intForumID)));
                                    }
                                    else
                                    {
                                        strSQL = ("SELECT Top 1 "
                                                    + (Variables.Forum.strDbTable + ("Thread.UsuarioID, "
                                                    + (Variables.Forum.strDbTable + ("Thread.Topic_ID, "
                                                    + (Variables.Forum.strDbTable + ("Thread.Thread_ID, "
                                                    + (Variables.Forum.strDbTable + "Thread.Message_date "))))))));
                                        strSQL = (strSQL + ("FROM "
                                                    + (Variables.Forum.strDbTable + "Thread  ")));
                                        strSQL = (strSQL + ("WHERE "
                                                    + (Variables.Forum.strDbTable + ("Thread.UsuarioID AND "
                                                    + (Variables.Forum.strDbTable + "Thread.Topic_ID IN ")))));
                                        strSQL = (strSQL + ("\t(SELECT TOP 1 "
                                                    + (Variables.Forum.strDbTable + "Topic.Topic_ID ")));
                                        strSQL = (strSQL + ("\tFROM "
                                                    + (Variables.Forum.strDbTable + "Topic ")));
                                        strSQL = (strSQL + ("\tWHERE "
                                                    + (Variables.Forum.strDbTable + ("Topic.Forum_ID = "
                                                    + (Variables.Forum.intForumID + " ")))));
                                        strSQL = (strSQL + ("\tORDER BY "
                                                    + (Variables.Forum.strDbTable + "Topic.Last_entry_date DESC) ")));
                                        strSQL = (strSQL + ("ORDER BY "
                                                    + (Variables.Forum.strDbTable + "Thread.Message_date DESC;")));
                                    }

                                    // Query the database
                                    rsCommon = db.Execute(strSQL);
                                    // If there are threads for topic then read in the date and Usuarios of the last entry
                                    if ((rsCommon.Rows.Count > 0))
                                    {
                                        // Read in the deatils from the recorset of the last post details
                                        lngLastEntryMeassgeID = long.Parse(rsCommon.Rows[0]["Thread_ID"].ToString());
                                        lngLastEntryTopicID = long.Parse(rsCommon.Rows[0]["Topic_ID"].ToString());
                                        dtmLastEntryDate = System.DateTime.Parse(rsCommon.Rows[0]["Message_date"].ToString());
                                        strLastEntryUser = rsCommon.Rows[0]["UsuarioID"].ToString();
                                        lngLastEntryUserID = long.Parse(rsCommon.Rows[0]["UsuarioID"].ToString());
                                    }

                                    // Calculate the last forum entry across all forums for the statistics at the bottom of the forum
                                    if ((dtmLastEntryDateAllForums < dtmLastEntryDate))
                                    {
                                        dtmLastEntryDateAllForums = dtmLastEntryDate;
                                        strLastEntryUserAllForums = strLastEntryUser;
                                        lngLastEntryUserIDAllForums = lngLastEntryUserID;
                                    }

                                    // Write the HTML of the forum descriptions and hyperlinks to the forums
                                    sb.AppendLine(("\r\n" + ("       <tr>" + ("\r\n" + "        <td bgcolor=\""))));
                                    if (((intForumColourNumber % 2)
                                                == 0))
                                    {
                                        sb.AppendLine(Variables.Forum.strTableEvenRowColour);
                                    }
                                    else
                                    {
                                        sb.AppendLine(Variables.Forum.strTableOddRowColour);
                                    }

                                    sb.AppendLine(("\" background=\""
                                                    + (Variables.Forum.strTableBgImage + "\" width=\"1%\" class=\"text\">")));
                                    // If the user has no access to a forum diplay a no access icon
                                    if (((Variables.Forum.blnRead == false)
                                                && ((Variables.Forum.blnModerator == false)
                                                && (FSPortal.Variables.User.Administrador == false))))
                                    {
                                        sb.AppendLine(("  <img src=\""
                                                        + (Variables.Forum.strImagePath + ("forum_no_entry_icon.gif\" alt=\""
                                                        + (Variables.Forum.strTxtNoAccess + "\">")))));
                                        // If the forum requires a clave diplay the clave icon
                                    }
                                    else if ((strForumclave != ""))
                                    {
                                        sb.AppendLine(("  <img src=\""
                                                        + (Variables.Forum.strImagePath + ("clave_required_icon.gif\" alt=\""
                                                        + (Variables.Forum.strTxtclaveRequired + "\">")))));
                                        // If the forum is read only and has new posts show the locked new posts icon
                                    }
                                    else 
                                    
                                    if(Session["dtmLastVisit"] != null)
                                        if (((System.DateTime.Parse(Session["dtmLastVisit"].ToString()) < dtmLastEntryDate)
                                                && ((blnForumLocked == true)
                                                && ((FSPortal.Variables.User.Administrador == false)
                                                && (Variables.Forum.blnModerator == false)))))
                                    {
                                        sb.AppendLine(("  <img src=\""
                                                        + (Variables.Forum.strImagePath + ("locked_new_posts_icon.gif\" alt=\""
                                                        + (Variables.Forum.strTxtReadOnlyNewReplies + "\">")))));
                                        // If the forum is read only show the locked new posts icon
                                    }
                                    else if (blnForumLocked)
                                    {
                                        sb.AppendLine(("  <img src=\""
                                                        + (Variables.Forum.strImagePath + ("closed_topic_icon.gif\" alt=\""
                                                        + (Variables.Forum.strTxtReadOnly + "\">")))));
                                        // If the forum has new posts show the new posts icon
                                    }
                                    else if ((System.DateTime.Parse(Session["dtmLastVisit"].ToString()) < dtmLastEntryDate))
                                    {
                                        sb.AppendLine(("  <img src=\""
                                                        + (Variables.Forum.strImagePath + ("new_posts_icon.gif\" alt=\""
                                                        + (Variables.Forum.strTxtOpenForumNewReplies + "\">")))));
                                        // If the forum is open but with no new replies
                                    }
                                    else
                                    {
                                        sb.AppendLine(("  <img src=\""
                                                        + (Variables.Forum.strImagePath + ("no_new_posts_icon.gif\" alt=\""
                                                        + (Variables.Forum.strTxtOpenForum + "\">")))));
                                    }

                                    sb.AppendLine(("\r\n" + ("        </td>" + ("\r\n" + "        <td bgcolor=\""))));
                                    if (((intForumColourNumber % 2)
                                                == 0))
                                    {
                                        sb.AppendLine(Variables.Forum.strTableEvenRowColour);
                                    }
                                    else
                                    {
                                        sb.AppendLine(Variables.Forum.strTableOddRowColour);
                                    }

                                    sb.AppendLine(("\" background=\""
                                                    + (Variables.Forum.strTableBgImage + "\" width=\"1%\" class=\"text\">")));
                                    // If this is the forum admin then let them have access to the forum admin pop up window
                                    if (FSPortal.Variables.User.Administrador)
                                    {
                                        sb.AppendLine(("      <a href=\"javascript:openWin(\'pop_up_forum_admin.aspx?FID="
                                                        + (Variables.Forum.intForumID + ("\',\'admin\',\'toolbar=0,location=0,status=0,menubar=0,scrollbars=1,resizable=1,width=590,height=325\')\"><" +
                                                        "img src=\""
                                                        + (Variables.Forum.strImagePath + ("small_admin_icon.gif\" align=\"middle\" border=\"0\" alt=\""
                                                        + (Variables.Forum.strTxtForumAdmin + "\"></a>")))))));
                                    }

                                    sb.AppendLine(("\r\n" + ("        <a href=\"forum_topics.aspx?FID="
                                                    + (Variables.Forum.intForumID + ("\" target=\"_self\">"
                                                    + (strForumName + ("</a><br />"
                                                    + (strForumDiscription
                                                    + (strModeratorsList + ("</td>" + ("\r\n" + "        <td bgcolor=\"")))))))))));
                                    if (((intForumColourNumber % 2)
                                                == 0))
                                    {
                                        sb.AppendLine(Variables.Forum.strTableEvenRowColour);
                                    }
                                    else
                                    {
                                        sb.AppendLine(Variables.Forum.strTableOddRowColour);
                                    }

                                    sb.AppendLine(("\" background=\""
                                                    + (Variables.Forum.strTableBgImage + ("\" width=\"7%\" align=\"center\" class=\"text\">"
                                                    + (lngNumberOfTopics + ("</td>" + ("\r\n" + "        <td bgcolor=\"")))))));
                                    if (((intForumColourNumber % 2)
                                                == 0))
                                    {
                                        sb.AppendLine(Variables.Forum.strTableEvenRowColour);
                                    }
                                    else
                                    {
                                        sb.AppendLine(Variables.Forum.strTableOddRowColour);
                                    }

                                    sb.AppendLine(("\" background=\""
                                                    + (Variables.Forum.strTableBgImage + ("\" width=\"7%\" align=\"center\" class=\"text\">"
                                                    + (lngNumberOfPosts + ("</td>" + ("\r\n" + "        <td bgcolor=\"")))))));
                                    if (((intForumColourNumber % 2)
                                                == 0))
                                    {
                                        sb.AppendLine(Variables.Forum.strTableEvenRowColour);
                                    }
                                    else
                                    {
                                        sb.AppendLine(Variables.Forum.strTableOddRowColour);
                                    }

                                    sb.AppendLine(("\" background=\""
                                                    + (Variables.Forum.strTableBgImage + ("\" width=\"29%\" class=\"smText\" align=\"right\"  nowrap=\"nowrap\">"
                                                    + (FuncionesFecha.DateFormat(dtmLastEntryDate, FuncionesFecha.saryDateTimeData) + (" "
                                                    + (Variables.Forum.strTxtAt + (" "
                                                    + (FuncionesFecha.TimeFormat(dtmLastEntryDate, FuncionesFecha.saryDateTimeData) + ("" + ("\r\n" + ("        <br />"
                                                    + (Variables.Forum.strTxtBy + (" <a href=\"JavaScript:openWin(\'pop_up_profile.aspx?PF="
                                                    + (lngLastEntryUserID + ("\',\'profile\',\'toolbar=0,location=0,status=0,menubar=0,scrollbars=1,resizable=1,width=590,height=425\')\"" +
                                                    " class=\"smLink\">"
                                                    + (strLastEntryUser + ("</a> <a href=\"forum_posts.aspx?TID="
                                                    + (lngLastEntryTopicID + ("&get=last#"
                                                    + (lngLastEntryMeassgeID + ("\" target=\"_self\"><img src=\""
                                                    + (Variables.Forum.strImagePath + ("right_arrow.gif\" align=\"middle\" border=\"0\" alt=\""
                                                    + (Variables.Forum.strTxtViewLastPost + ("\"></a></td>" + ("\r\n" + "       </tr>")))))))))))))))))))))))))));
                                }

                                // Count the number of forums
                                intNumberofForums = (intNumberofForums + 1);
                            }

                        }

                    }

                }

            }
            sb.AppendLine("</table>");
            sb.AppendLine("</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</table>");
            sb.AppendLine("</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</table>");
            sb.AppendLine("<br />");
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
            sb.AppendLine(@"<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" bgcolor=""");
            sb.AppendLine(Variables.Forum.strTableBgColour);
            sb.AppendLine(@""">");
            sb.AppendLine("<tr>");
            sb.AppendLine("<td>");
            sb.AppendLine(@"<table width=""100%"" border=""0"" cellspacing=""1"" cellpadding=""4"">");
            sb.AppendLine("<tr>");
            sb.AppendLine(@"<td bgcolor=""");
            sb.AppendLine(Variables.Forum.strTableTitleColour);
            sb.AppendLine(@""" width=""44%"" class=""tHeading"" background=""");
            sb.AppendLine(Variables.Forum.strTableTitleBgImage);
            sb.AppendLine(@""">");
            sb.AppendLine(Variables.Forum.strTxtForumStatistics);
            sb.AppendLine("</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("<tr>");
            sb.AppendLine(@"<td bgcolor=""");
            sb.AppendLine(Variables.Forum.strTableColour);
            sb.AppendLine(@""" background=""");
            sb.AppendLine(Variables.Forum.strTableBgImage);
            sb.AppendLine(@""" class=""smText"" valign=""top"">");
            sb.AppendLine(("\r\n" + ("\t\t"
                                    + (Variables.Forum.strTxtOurUserHavePosted + (" "
                                    + (lngTotalNumberOfPosts + (" "
                                    + (Variables.Forum.strTxtPostsIn + (" "
                                    + (lngTotalNumberOfTopics + (" "
                                    + (Variables.Forum.strTxtTopicsIn + (" "
                                    + (intNumberofForums + (" " + Variables.Forum.strTxtForums)))))))))))))));
            sb.AppendLine(("\r\n" + ("\t\t<br />"
                            + (Variables.Forum.strTxtLastPostOn + (" "
                            + (FuncionesFecha.DateFormat(dtmLastEntryDateAllForums, FuncionesFecha.saryDateTimeData) + (" "
                            + (Variables.Forum.strTxtAt + (" "
                            + (FuncionesFecha.TimeFormat(dtmLastEntryDateAllForums, FuncionesFecha.saryDateTimeData) + (" "
                            + (Variables.Forum.strTxtBy + (" <a href=\"JavaScript:openWin(\'pop_up_profile.aspx?PF="
                            + (lngLastEntryUserIDAllForums + ("\',\'profile\',\'toolbar=0,location=0,status=0,menubar=0,scrollbars=1,resizable=1,width=590,height=425\')\"" +
                            " class=\"smLink\">"
                            + (strLastEntryUserAllForums + "</a>"))))))))))))))));
            // Get the latest forum posts
            // Cursor type to one to count
            // rsCommon.CursorType = 1
            // Get the last signed up user
            // Initalise the strSQL variable with an SQL statement to query the database
            if ((FSDatabase.Utils.BDType == FSDatabase.Utils.TypeBd.SQLServer))
            {
                strSQL = ("Execute "
                            + (Variables.Forum.strDbProc + "UsuariosDesc"));
            }
            else
            {
                strSQL = ("SELECT " + ("Usuarios.usuario, " + "Usuarios.UsuarioID "));
                strSQL = (strSQL + ("FROM " + "Usuarios "));
                strSQL = (strSQL + ("ORDER BY " + "Usuarios.UsuarioID DESC;"));
            }

            // Query the database
            BdUtils dbPortal = new BdUtils(FSPortal.Variables.App.connectionString, FSPortal.Variables.App.providerName);
            rsCommon = dbPortal.Execute(strSQL);
            // Display some statistics for the members
            if ((rsCommon.Rows.Count > 0))
            {
                sb.AppendLine(("\r\n" + ("<br />"
                                + (Variables.Forum.strTxtWeHave + (" "
                                + (rsCommon.Rows.Count + (" "
                                + (Variables.Forum.strTxtForumMembers + ("\r\n" + ("\t\t<br />"
                                + (Variables.Forum.strTxtTheNewestForumMember + (" <a href=\"JavaScript:openWin(\'pop_up_profile.aspx?PF="
                                + (rsCommon.Rows[0]["UsuarioID"].ToString() + ("\',\'profile\',\'toolbar=0,location=0,status=0,menubar=0,scrollbars=1,resizable=1,width=590,height=425\')\"" +
                                " class=\"smLink\">"
                                + (rsCommon.Rows[0]["usuario"].ToString() + "</a>")))))))))))))));
            }
            sb.AppendLine("</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</table>");
            sb.AppendLine("</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</table>");
            sb.AppendLine("</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</table>");
            sb.AppendLine("</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</table>");
            sb.AppendLine(@"<div align=""center""><br />");
            sb.AppendLine(@"<table width=""533"" border=""0"" cellspacing=""0"" cellpadding=""2"">");
            sb.AppendLine("<tr>");
            sb.AppendLine(@"<td class=""smText"" align=""left""><img src=""");
            sb.AppendLine(Variables.Forum.strImagePath);
            sb.AppendLine(@"no_new_posts_icon.gif"" alt=""");
            sb.AppendLine(Variables.Forum.strTxtOpenForum);
            sb.AppendLine(@""">");
            sb.AppendLine(Variables.Forum.strTxtOpenForum);
            sb.AppendLine(@"</td>");
            sb.AppendLine(@"<td class=""smText"" align=""left""><img src=""");
            sb.AppendLine(Variables.Forum.strImagePath);
            sb.AppendLine(@"closed_topic_icon.gif"" alt=""");
            sb.AppendLine(Variables.Forum.strTxtReadOnly);
            sb.AppendLine(@""">");
            sb.AppendLine(Variables.Forum.strTxtReadOnly);
            sb.AppendLine("</td>");
            sb.AppendLine(@"<td class=""smText"" align=""left""><img src=""");
            sb.AppendLine(Variables.Forum.strImagePath);
            sb.AppendLine(@"password_required_icon.gif"" alt=""");
            sb.AppendLine(Variables.Forum.strTxtclaveRequired);
            sb.AppendLine(@""">");
            sb.AppendLine(Variables.Forum.strTxtclaveRequired);
            sb.AppendLine("</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("<tr>");
            sb.AppendLine(@"<td class=""smText"" align=""left""><img src=""");
            sb.AppendLine(Variables.Forum.strImagePath);
            sb.AppendLine(@"new_posts_icon.gif"" alt=""");
            sb.AppendLine(Variables.Forum.strTxtOpenForumNewReplies);
            sb.AppendLine(@""">");
            sb.AppendLine(Variables.Forum.strTxtOpenForumNewReplies);
            sb.AppendLine("</td>");
            sb.AppendLine(@"<td class=""smText"" align=""left""><img src=""");
            sb.AppendLine(Variables.Forum.strImagePath);
            sb.AppendLine(@"locked_new_posts_icon.gif"" alt=""");
            sb.AppendLine(Variables.Forum.strTxtReadOnlyNewReplies);
            sb.AppendLine(@""">");
            sb.AppendLine(Variables.Forum.strTxtReadOnlyNewReplies);
            sb.AppendLine("</td>");
            sb.AppendLine(@"<td class=""smText"" align=""left""><img src=""");
            sb.AppendLine(Variables.Forum.strImagePath);
            sb.AppendLine(@"forum_no_entry_icon.gif"" alt=""");
            sb.AppendLine(Variables.Forum.strTxtNoAccess);
            sb.AppendLine(@""">");
            sb.AppendLine(Variables.Forum.strTxtNoAccess);
            sb.AppendLine("</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</table>");
            sb.AppendLine("<br />");
            sb.AppendLine("<br />");
            sb.AppendLine("<br />");
            sb.AppendLine("</div>");

            return sb.ToString();
        }
    }
}
