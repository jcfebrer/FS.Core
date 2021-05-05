// <fileheader>
// <copyright file="search.aspx.cs" company="Febrer Software">
//     Fecha: 30/11/2007
//     Path: forum\search.aspx.cs
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
    public class search : FSPortal.BasePage
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
            // Function to build SQL query's for seach all or any words
            // Dimension variables
            FSDatabase.BdUtils db = new FSDatabase.BdUtils("FSForum");
            DataTable rsTopic;
            // Holds the Recordset for the Topic details
            string strSQLResultsQuery = "";
            string strForumName;
            // Holds the forum name
            string strSearchKeywords;
            // Holds the keywords to search for
            string[] sarySearchWord;
            // Array to hold the search words
            string strSearchIn;
            // Holds where the serach is to be done
            int intSearchForumID = 0;
            // Holds the forum the result belongs to
            long lngNumberOfReplies = 0;
            // Holds the number of replies for a topic
            long lngTopicID = 0;
            // Holds the topic ID
            string strSubject = "";
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
            int intTotalNumOfPages;
            // Holds the total number of pages in the recordset
            int intRecordLoopCounter = 0;
            // Holds the loop counter numeber
            //bool blnReturnedSearchResults = true;
            // Set to true if there are search results returned
            int intTopicPageLoopCounter;
            // Holds the number of pages there are in the forum
            bool blnTopicLocked = false;
            // set to true if the topic is locked
            int intPriority = 0;
            // Holds the priority level of the topic
            int intNumberOfTopicPages;
            // Holds the number of topic pages
            int intTopicPagesLoopCounter;
            // Holds the number of loops
            int intHighlightLoopCounter;
            // Loop counter to loop through words and hightlight them
            bool blnNewPost = false;
            // Set to true if the post is a new post since the users last visit
            long lngPollID = 0;
            // Holds the topic poll id number
            string strSearchMode;
            // Holds the search mode
            System.DateTime dtmFirstEntryDate = new System.DateTime();
            // Holds the date of the first message
            DataTable rsCommon;
            // Initialise variables
            //blnReturnedSearchResults = true;
            strSearchKeywords = Request.QueryString["KW"].Substring(0, 35).Trim();
            strSearchIn = Request.QueryString["SI"].Substring(0, 3).Trim();
            Variables.Forum.intForumID = int.Parse(Request.QueryString["FM"]);
            strSearchMode = Request.QueryString["SM"].Substring(0, 3).Trim();
            // Replace _ with a space so that these words are split into multiple words
            if ((strSearchIn != "AR"))
            {
                strSearchKeywords = strSearchKeywords.Replace("_", " ");
            }

            // Filter the search words with the same filters as the text is saved with
            strSearchKeywords = FuncionesFilter.formatSQLInput(strSearchKeywords);
            // Split up the keywords to be searched
            sarySearchWord = strSearchKeywords.Trim().Split(' ');
            // If there is no keywords to search for then redirect to the forum homepage
            if ((strSearchKeywords == ""))
            {
                // Clean up
                rsCommon = null;
                Response.Redirect("default.aspx");
            }

            // If this is the first time the page is displayed then the Forum Topic record position is set to page 1
            if ((double.Parse(Request.QueryString["SPN"]) == 0))
            {
                intRecordPositionPageNum = 1;
                // Else the page has been displayed before so the Forum Topic record postion is set to the Record Position number
            }
            else
            {
                intRecordPositionPageNum = int.Parse(Request.QueryString["SPN"]);
            }

            // Call the moderator function and see if the user is a moderator
            if ((FSPortal.Variables.User.Administrador == false))
            {
                Variables.Forum.blnModerator = common.isModerator(Variables.Forum.intForumID, FSPortal.Variables.User.GroupId);
            }

            // If the user has selected to search in the Topic subjects then build the Where Clause of the Reseults Query with the Topics containg threads with the search words
            // Search in topic subject
            if ((strSearchIn == "TC"))
            {
                // Make the search words the same as encoded stuff
                strSearchKeywords = FuncionesForum.removeAllTags(strSearchKeywords);
                strSearchKeywords = FuncionesFilter.formatInput(strSearchKeywords);
                // Initalise the Results Query string with the select part of the query
                strSQLResultsQuery = ("SELECT "
                            + (Variables.Forum.strDbTable + ("Topic.Topic_ID, "
                            + (Variables.Forum.strDbTable + ("Topic.Forum_ID, "
                            + (Variables.Forum.strDbTable + ("Topic.Poll_ID, "
                            + (Variables.Forum.strDbTable + ("Topic.No_of_views, "
                            + (Variables.Forum.strDbTable + ("Topic.Subject, "
                            + (Variables.Forum.strDbTable + ("Topic.Last_entry_date, "
                            + (Variables.Forum.strDbTable + "Topic.Start_date "))))))))))))));
                strSQLResultsQuery = (strSQLResultsQuery + ("FROM "
                            + (Variables.Forum.strDbTable + "Topic WHERE ")));
                if ((((strSearchMode == "2")
                            || (strSearchMode == "1"))
                            && (strSearchIn == "Topic")))
                {
                    // Call the function to build the query
                    strSQLResultsQuery = (strSQLResultsQuery + FuncionesForum.BuildSQL((""
                                    + (Variables.Forum.strDbTable + "Topic.Subject")), sarySearchWord, strSearchMode));
                    // Else they have choosen 3 in topic
                }
                else
                {
                    strSQLResultsQuery = (strSQLResultsQuery + (""
                                + (Variables.Forum.strDbTable + ("Topic.Subject LIKE \'%"
                                + (strSearchKeywords + "%\'")))));
                }

                if ((Variables.Forum.intForumID != 0))
                {
                    // If the user has selected to search a certain forum then build the Results Query to only look in that forum
                    strSQLResultsQuery = (strSQLResultsQuery + (" AND "
                                + (Variables.Forum.strDbTable + ("Topic.Forum_ID ="
                                + (Variables.Forum.intForumID + " ")))));
                }

            }

            // If the user has selected to search in the message body then build the Where Clause of the Reseults Query with the Topics containg threads with the search words
            // Search in posts
            if ((strSearchIn == "PT"))
            {
                // Initialise the sql query to get the with a select statment to get the topic ID
                strSQLResultsQuery = ("SELECT "
                            + (Variables.Forum.strDbTable + ("Topic.Topic_ID, "
                            + (Variables.Forum.strDbTable + ("Topic.Forum_ID, "
                            + (Variables.Forum.strDbTable + ("Topic.Poll_ID, "
                            + (Variables.Forum.strDbTable + ("Topic.No_of_views, "
                            + (Variables.Forum.strDbTable + ("Topic.Subject, "
                            + (Variables.Forum.strDbTable + ("Topic.Last_entry_date, "
                            + (Variables.Forum.strDbTable + "Topic.Start_date "))))))))))))));
                strSQLResultsQuery = (strSQLResultsQuery + ("FROM "
                            + (Variables.Forum.strDbTable + "Topic ")));
                strSQLResultsQuery = (strSQLResultsQuery + ("WHERE "
                            + (Variables.Forum.strDbTable + "Topic.Topic_ID IN ")));
                strSQLResultsQuery = (strSQLResultsQuery + ("\t(SELECT "
                            + (Variables.Forum.strDbTable + "Thread.Topic_ID ")));
                strSQLResultsQuery = (strSQLResultsQuery + ("\tFROM "
                            + (Variables.Forum.strDbTable + "Thread ")));
                strSQLResultsQuery = (strSQLResultsQuery + "\tWHERE ( ");
                if (((strSearchMode == "2")
                            || (strSearchMode == "1")))
                {
                    // Call the function to build the query
                    strSQLResultsQuery = (strSQLResultsQuery + FuncionesForum.BuildSQL((""
                                    + (Variables.Forum.strDbTable + "Thread.Message")), sarySearchWord, strSearchMode));
                    // Else the user has choosen to only search for a message containg the 3
                }
                else
                {
                    strSQLResultsQuery = (strSQLResultsQuery + (""
                                + (Variables.Forum.strDbTable + ("Thread.Message LIKE \'%"
                                + (strSearchKeywords + "%\'")))));
                }

                strSQLResultsQuery = (strSQLResultsQuery + ")) ");
                if ((Variables.Forum.intForumID != 0))
                {
                    // If the user has selected to search a certain forum then intitilaise the SQL query to search only that forum
                    strSQLResultsQuery = (strSQLResultsQuery + (" AND ("
                                + (Variables.Forum.strDbTable + ("Topic.Forum_ID="
                                + (Variables.Forum.intForumID + ")")))));
                }

            }

            // If the user has selected to search in the message body then build the Where Clause of the Reseults Query with the Topics containg threads written by the Usuarios
            // Search by Usuarios
            if ((strSearchIn == "AR"))
            {
                // Take out parts of the usuario that are not permitted
                strSearchKeywords = common.disallowedMemberNames(strSearchKeywords);
                // Get rid of milisous code
                strSearchKeywords = FuncionesFilter.formatSQLInput(strSearchKeywords);
                // Initalise the strSQL variable with an SQL statement to query the database
                strSQLResultsQuery = ("SELECT DISTINCT "
                            + (Variables.Forum.strDbTable + ("Topic.Topic_ID, "
                            + (Variables.Forum.strDbTable + ("Topic.Forum_ID, "
                            + (Variables.Forum.strDbTable + ("Topic.Poll_ID, "
                            + (Variables.Forum.strDbTable + ("Topic.No_of_views, "
                            + (Variables.Forum.strDbTable + ("Topic.Subject, "
                            + (Variables.Forum.strDbTable + ("Topic.Last_entry_date, "
                            + (Variables.Forum.strDbTable + "Topic.Start_date "))))))))))))));
                strSQLResultsQuery = (strSQLResultsQuery + ("FROM "
                            + (Variables.Forum.strDbTable + ("Topic INNER JOIN (" + ("Usuarios INNER JOIN "
                            + (Variables.Forum.strDbTable + ("Thread ON " + ("Usuarios.UsuarioID = "
                            + (Variables.Forum.strDbTable + ("Thread.UsuarioID) ON "
                            + (Variables.Forum.strDbTable + ("Topic.Topic_ID = "
                            + (Variables.Forum.strDbTable + "Thread.Topic_ID ")))))))))))));
                strSQLResultsQuery = (strSQLResultsQuery + ("WHERE (((" + ("Usuarios.usuario) LIKE \'"
                            + (strSearchKeywords + "\') "))));
                if ((Variables.Forum.intForumID != 0))
                {
                    strSQLResultsQuery = (strSQLResultsQuery + ("AND (("
                                + (Variables.Forum.strDbTable + ("Topic.Forum_ID)="
                                + (Variables.Forum.intForumID + ") ")))));
                }

                strSQLResultsQuery = (strSQLResultsQuery + ") ");
            }

            // Tell the Results Query what order to place the results in
            switch (Request.QueryString["OB"].Substring(0, 3).Trim())
            {
                case "3":
                    strSQLResultsQuery = (strSQLResultsQuery + (" ORDER BY "
                                + (Variables.Forum.strDbTable + "Topic.Subject ASC;")));
                    break;
                case "4":
                    strSQLResultsQuery = (strSQLResultsQuery + (" ORDER BY "
                                + (Variables.Forum.strDbTable + "Topic.No_of_views DESC;")));
                    break;
                case "2":
                    strSQLResultsQuery = (strSQLResultsQuery + (" ORDER BY "
                                + (Variables.Forum.strDbTable + "Topic.Start_date ASC;")));
                    break;
                default:
                    strSQLResultsQuery = (strSQLResultsQuery + (" ORDER BY "
                                + (Variables.Forum.strDbTable + "Topic.Last_entry_date DESC;")));
                    break;
            }
            //sb.AppendLine("<html>");
            //sb.AppendLine("<head>");
            //sb.AppendLine("<title>");
            tituloPagina = Variables.Forum.strMainForumName + " Search Results: " + strSearchKeywords;
            //sb.AppendLine("</title>");
            //sb.AppendLine(@"<navigation:navigation ID=""common1"" runat=""server"" />");
            sb.AppendLine(navigation_buttons_inc.Render());
            sb.AppendLine(@"<table width=""");
            sb.AppendLine(Variables.Forum.strTableVariableWidth);
            sb.AppendLine(@""" border=""0"" cellspacing=""0"" cellpadding=""3"" align=""center"">");
            sb.AppendLine("<tr>");
            sb.AppendLine(@"<td align=""left"" class=""heading"">");
            sb.AppendLine(Variables.Forum.strTxtSearchResults);
            sb.AppendLine("</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("<tr>");
            sb.AppendLine(@"<td align=""left"" width=""71%"" class=""bold""><img src=""");
            sb.AppendLine(Variables.Forum.strImagePath);
            sb.AppendLine(@"open_folder_icon.gif"" border=""0"" align=""middle""> <a href=""default.aspx"" target=""_self"" class=""boldLink"">");
            sb.AppendLine(Variables.Forum.strMainForumName);
            sb.AppendLine("</a>");
            // Read in the forum name from the database
            // Initalise the strSQL variable with an SQL statement to query the database
            string strSQL = ("SELECT "
                        + (Variables.Forum.strDbTable + ("Forum.Forum_name FROM "
                        + (Variables.Forum.strDbTable + ("Forum WHERE Forum_ID = " + Variables.Forum.intForumID)))));
            // Query the database
            rsCommon = db.Execute(strSQL);
            // Check there are forum's to display
            if (((rsCommon.Rows.Count == 0)
                        && !(Variables.Forum.intForumID == 0)))
            {
                // If there are no forum's to display then display the appropriate error message
                sb.AppendLine(("\r\n" + ("<span class=\"bold\">"
                                + (Variables.Forum.strTxtNoForums + "</span>"))));
                // Else there the are forum's to write the HTML to display it the forum names and a discription
            }
            else
            {
                // Write the HTML of the forum descriptions as hyperlinks to the forums
                if ((Variables.Forum.intForumID > 0))
                {
                    // Read in the forum name
                    strForumName = rsCommon.Rows[0]["Forum_name"].ToString();
                    // Display the forum name
                    sb.AppendLine(("\r\n" + ("<a href=\"forum_topics.aspx?FID="
                                    + (Variables.Forum.intForumID + ("\" target=\"_self\" class=\"boldLink\">"
                                    + (strForumName + "</a>"))))));
                }

            }
            sb.AppendLine(@": <a href=""search.aspx?KW=");
            sb.AppendLine(Server.UrlEncode(Request.QueryString["KW"]));
            sb.AppendLine("&sM=");
            sb.AppendLine(strSearchMode);
            sb.AppendLine("&sI=");
            sb.AppendLine(strSearchIn);
            sb.AppendLine("&fM=");
            sb.AppendLine(Variables.Forum.intForumID.ToString());
            sb.AppendLine("&oB=");
            sb.AppendLine(Request.QueryString["OB"].Substring(0, 3).Trim());
            sb.AppendLine(@""" target=""_self""  class=""boldLink"">");
            sb.AppendLine(Variables.Forum.strTxtSearchResults);
            sb.AppendLine("</a>");
            sb.AppendLine("</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</table>");
            sb.AppendLine(@"<table width=""");
            sb.AppendLine(Variables.Forum.strTableVariableWidth);
            sb.AppendLine(@""" border=""0"" cellspacing=""0"" cellpadding=""4"" align=""center"">");
            sb.AppendLine("<tr>");
            sb.AppendLine(@"<td width=""90%"" class=""lgText"" colspan=""2"">");
            // Get the Topics for the forum from the database
            // Set the cursor type property of the record set to dynamic so we can naviagate through the record set
            // rsCommon.CursorType = 1
            // Query the database
            rsCommon = db.Execute(strSQLResultsQuery);
            // , intRecordPositionPageNum, Variables.Forum.intTopicPerPage)
            // Set the number of records to display on each page
            // rsCommon.PageSize = intTopicPerPage
            // If ther are records found say how many
            if ((rsCommon.Rows.Count > 0))
            {
                sb.AppendLine((Variables.Forum.strTxtYourSearchFor + (" \'"
                                + (strSearchKeywords + ("\' "
                                + (Variables.Forum.strTxtHasFound + (" "
                                + (rsCommon.Rows.Count + (" " + Variables.Forum.strTxtResults)))))))));
            }
            sb.AppendLine("</td>");
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
            sb.AppendLine(@"<table width=""100%"" border=""0"" cellspacing=""1"" cellpadding=""3"" height=""14"" bgcolor=""");
            sb.AppendLine(Variables.Forum.strTableBgColour);
            sb.AppendLine(@""">");
            // If there are no results display an error msg
            if ((rsCommon.Rows.Count == 0))
            {
                sb.AppendLine(("\r\n" + ("<tr><td bgcolor=\""
                                + (Variables.Forum.strTableColour + ("\" background=\""
                                + (Variables.Forum.strTableBgImage + ("\" colspan=\"5\" align=\"center\" height=\"150\" class=\"text\">" + Variables.Forum.strTxtNoSearchResults)))))));
                sb.AppendLine(("\r\n" + ("<br /><br /><a href=\"search_form.aspx?RP=S&kW="
                                + (Server.UrlEncode(Request.QueryString["KW"]) + ("&sM="
                                + (strSearchMode + ("&sI="
                                + (strSearchIn + ("&fM="
                                + (Variables.Forum.intForumID + ("&oB="
                                + (Request.QueryString["OB"].Substring(0, 3).Trim() + ("&sPN=1" + ("\" target=\"_self\">"
                                + (Variables.Forum.strTxtClickHereToRefineSearch + "</a></td></tr>")))))))))))))));
                // If there the are topic's so write the HTML to display the topic names and a discription
            }
            else
            {

            }
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
            // Get the record poistion to display from
            // rsCommon.AbsolutePage = intRecordPositionPageNum
            // Count the number of pages there are in the recordset calculated by the PageSize attribute set above
            intNumberOfTopicPages = rsCommon.Rows.Count;
            // Count the number of pages there are in the recordset calculated by the PageSize attribute set by admin
            intTotalNumOfPages = intNumberOfTopicPages % Variables.Forum.intThreadsPerPage;
            // rsPost.PageCount
            if (((intNumberOfTopicPages % Variables.Forum.intThreadsPerPage) > 0))
            {
                intTotalNumOfPages = (intTotalNumOfPages + 1);
            }

            // Loop round to read in all the Topics in the database
            // For intRecordLoopCounter = 1 to intTopicPerPage
            foreach (DataRow row in rsCommon.Rows)
            {
                // If there are no records left in the recordset to display then exit the for loop
                // If rsCommon.EOF Then Exit For
                // Read in Topic details from the database
                intSearchForumID = int.Parse(row["Forum_ID"].ToString());
                lngTopicID = long.Parse(row["Topic_ID"].ToString());
                lngPollID = long.Parse(row["Poll_ID"].ToString());
                lngNumberOfViews = long.Parse(row["No_of_views"].ToString());
                strSubject = row["Subject"].ToString();
                // Make search words in the subject highlighted
                for (intHighlightLoopCounter = 0; (intHighlightLoopCounter <= sarySearchWord.Length); intHighlightLoopCounter++)
                {
                    // Replace the search words with highlited ones
                    strSubject = strSubject.Replace(sarySearchWord[intHighlightLoopCounter], ("<span class=\"highlight\">"
                                    + (sarySearchWord[intHighlightLoopCounter] + "</span>")));
                }

                // Query the database to get if the topic is locked
                strSQL = ("SELECT TOP 1 "
                            + (Variables.Forum.strDbTable + ("Topic.Locked, "
                            + (Variables.Forum.strDbTable + ("Topic.Priority FROM "
                            + (Variables.Forum.strDbTable + ("Topic WHERE "
                            + (Variables.Forum.strDbTable + ("Topic.Topic_ID = "
                            + (lngTopicID + ";"))))))))));
                rsTopic = db.Execute(strSQL);
                // Read in if the topic is locked
                blnTopicLocked = Functions.ValorBool(rsTopic.Rows[0]["Locked"]);
                intPriority = int.Parse(rsTopic.Rows[0]["Priority"].ToString());
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
                                + (Variables.Forum.strDbTable + "Thread.Message_date "))))));
                    strSQL = (strSQL + ("FROM "
                                + (Variables.Forum.strDbTable + "Thread ")));
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
                    strTopicStartusuario = rsTopic.Rows[0]["usuarioId"].ToString();
                    lngTopicStartUserID = long.Parse(rsTopic.Rows[0]["UsuarioID"].ToString());
                    lngNumberOfReplies = rsTopic.Rows.Count;
                    dtmFirstEntryDate = System.DateTime.Parse(rsTopic.Rows[0]["Message_date"].ToString());
                    lngLastEntryMessageID = long.Parse(rsTopic.Rows[(rsTopic.Rows.Count - 1)]["Thread_ID"].ToString());
                    strLastEntryusuario = rsTopic.Rows[(rsTopic.Rows.Count - 1)]["usuarioID"].ToString();
                    lngLastEntryUserID = long.Parse(rsTopic.Rows[(rsTopic.Rows.Count - 1)]["UsuarioID"].ToString());
                    dtmLastEntryDate = System.DateTime.Parse(rsTopic.Rows[(rsTopic.Rows.Count - 1)]["Message_date"].ToString());
                }

                // Set the booleon varible if this is a new post since the users last visit and has not been read
                if (((System.DateTime.Parse(Functions.Valor(Session["dtmLastVisit"])) < dtmLastEntryDate)
                            && (Web.Cookie(Request.Cookies["RT"], ("TID" + lngTopicID)) == "")))
                {
                    blnNewPost = true;
                }
                else
                {
                    blnNewPost = false;
                }

                // Write the HTML of the Topic descriptions as hyperlinks to the Topic details and message}
            }
            sb.AppendLine("<tr>");
            sb.AppendLine(@"<td bgcolor=""");
            if (((intRecordLoopCounter % 2) == 0))
            {
                sb.AppendLine(Variables.Forum.strTableEvenRowColour);
            }
            else
            {
                sb.AppendLine(Variables.Forum.strTableOddRowColour);
            }
            sb.AppendLine(@""" background=""");
            sb.AppendLine(Variables.Forum.strTableBgImage);
            sb.AppendLine(@""" width=""3%"" align=""center"">");
            // If the topic is pinned then display the pinned icon
            if (intPriority == 1)
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
            sb.AppendLine(@""" width=""41%"" class=""text"">");
            // If the user is the forum admin or a moderator then give let them delete the topic
            if (((FSPortal.Variables.User.Administrador == true)
                        || (Variables.Forum.blnModerator == true)))
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

            // Disply link to topic and subject
            sb.AppendLine(("\t<a href=\"forum_posts.aspx?TID="
                            + (lngTopicID + ("&kW="
                            + (Server.UrlEncode(Request.QueryString["KW"]) + ("\" target=\"_self\" title=\""
                            + (Variables.Forum.strTxtThisTopicWasStarted
                            + (FuncionesFecha.DateFormat(dtmFirstEntryDate, FuncionesFecha.saryDateTimeData) + (" "
                            + (Variables.Forum.strTxtAt + (" "
                            + (FuncionesFecha.TimeFormat(dtmFirstEntryDate, FuncionesFecha.saryDateTimeData) + ("\">"
                            + (strSubject + "</a>"))))))))))))));
            // Calculate the number of pages for the topic and display links if there are more than 1 page
            intNumberOfTopicPages = (int)(lngNumberOfReplies + 1) % Variables.Forum.intThreadsPerPage;
            // If there is a remainder from calculating the num of pages add 1 to the number of pages
            if ((((lngNumberOfReplies + 1) % Variables.Forum.intThreadsPerPage) > 0))
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
                            sb.AppendLine((" <a href=\"forum_posts.aspx?TID="
                                            + (lngTopicID + ("&kW="
                                            + (Server.UrlEncode(Request.QueryString["KW"]) + "&TPN=8")))));
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
                            sb.AppendLine((" <a href=\"forum_posts.aspx?TID="
                                            + (lngTopicID + ("&kW="
                                            + (Server.UrlEncode(Request.QueryString["KW"]) + ("&TPN="
                                            + (intNumberOfTopicPages - 1)))))));
                            // If a priority topic need to make sure we don't change forum
                            if ((intPriority == 3))
                            {
                                sb.AppendLine(("&FID="
                                                + (Variables.Forum.intForumID + "&PR=3")));
                            }

                            sb.AppendLine(("\" target=\"_self\" class=\"smLink\">"
                                            + ((intNumberOfTopicPages - 1)
                                            + "</a>")));
                            sb.AppendLine((" <a href=\"forum_posts.aspx?TID="
                                            + (lngTopicID + ("&kW="
                                            + (Server.UrlEncode(Request.QueryString["KW"]) + ("&TPN=" + intNumberOfTopicPages))))));
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
                    sb.AppendLine((" <a href=\"forum_posts.aspx?TID="
                                    + (lngTopicID + ("&kW="
                                    + (Server.UrlEncode(Request.QueryString["KW"]) + ("&TPN=" + intTopicPagesLoopCounter))))));
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
            sb.AppendLine(@""" width=""15%"" class=""text""><a href=""JavaScript:openWin('pop_up_profile.aspx?PF=");
            sb.AppendLine(lngTopicStartUserID.ToString());
            sb.AppendLine("&FID=");
            sb.AppendLine(intSearchForumID.ToString());
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
            sb.AppendLine(@""" width=""29%"" align=""right"" class=""smText"" nowrap=""nowrap"">");
            FuncionesFecha.DateFormat(dtmLastEntryDate, FuncionesFecha.saryDateTimeData);
            sb.AppendLine(Variables.Forum.strTxtAt);
            FuncionesFecha.TimeFormat(dtmLastEntryDate, FuncionesFecha.saryDateTimeData);
            sb.AppendLine("<br />");
            sb.AppendLine(Variables.Forum.strTxtBy);
            sb.AppendLine(@"<a href=""JavaScript:openWin('pop_up_profile.aspx?PF=");
            sb.AppendLine(lngLastEntryUserID.ToString());
            sb.AppendLine(@"','profile','toolbar=0,location=0,status=0,menubar=0,scrollbars=1,resizable=1,width=590,height=425')"" class=""smLink"">");
            sb.AppendLine(strLastEntryusuario);
            sb.AppendLine(@"</a> <a href=""forum_posts.aspx?TID=");
            sb.AppendLine(lngTopicID.ToString());
            sb.AppendLine("&get=last#");
            sb.AppendLine(lngLastEntryMessageID.ToString());
            sb.AppendLine(@""" target=""_self""><img src=""");
            sb.AppendLine(Variables.Forum.strImagePath);
            sb.AppendLine(@"right_arrow.gif"" align=""middle"" border=""0"" alt=""");
            sb.AppendLine(Variables.Forum.strTxtViewLastPost);
            sb.AppendLine(@"""></a></td>");
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
            sb.AppendLine(@"<table width=""");
            sb.AppendLine(Variables.Forum.strTableVariableWidth);
            sb.AppendLine(@""" border=""0"" cellspacing=""0"" cellpadding=""4"" align=""center"">");
            sb.AppendLine("<tr>");
            sb.AppendLine(@"<td><forum_jump:forum_jump ID=""forum_jump"" runat=""server"" /></td>");
            // If there is more than 1 page of topics then dispaly drop down list to the other topics
            if ((intTotalNumOfPages > 1))
            {
                // Display an image link to the last topic
                sb.AppendLine(("\r\n" + "\t\t<td align=\"right\" class=\"text\">"));
                // Display a prev link if previous pages are available
                if ((intRecordPositionPageNum > 1))
                {
                    sb.AppendLine(("<a href=\"search.aspx?KW="
                                    + (Server.UrlEncode(Request.QueryString["KW"]) + ("&sM="
                                    + (strSearchMode + ("&sI="
                                    + (strSearchIn + ("&fM="
                                    + (Variables.Forum.intForumID + ("&oB="
                                    + (Request.QueryString["OB"].Substring(0, 3).Trim() + ("&sPN="
                                    + ((intRecordPositionPageNum - 1) + ("\"><&lt "
                                    + (Variables.Forum.strTxtPrevious + "</a> ")))))))))))))));
                }

                sb.AppendLine((Variables.Forum.strTxtPage + (" " + ("\r\n" + "\t\t  <select onChange=\"ForumJump(this)\" name=\"SelectTopicPage\">"))));
                // Loop round to display links to all the other pages
                for (intTopicPageLoopCounter = 1; (intTopicPageLoopCounter <= intTotalNumOfPages); intTopicPageLoopCounter++)
                {
                    // Display a link in the link list to the another topic page
                    sb.AppendLine(("\r\n" + ("\t\t  <option value=\"search.aspx?KW="
                                    + (Server.UrlEncode(Request.QueryString["KW"]) + ("&sM="
                                    + (strSearchMode + ("&sI="
                                    + (strSearchIn + ("&fM="
                                    + (Variables.Forum.intForumID + ("&oB="
                                    + (Request.QueryString["OB"].Substring(0, 3).Trim() + ("&sPN="
                                    + (intTopicPageLoopCounter + "\""))))))))))))));
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
                    sb.AppendLine((" <a href=\"search.aspx?KW="
                                    + (Server.UrlEncode(Request.QueryString["KW"]) + ("&sM="
                                    + (strSearchMode + ("&sI="
                                    + (strSearchIn + ("&fM="
                                    + (Variables.Forum.intForumID + ("&oB="
                                    + (Request.QueryString["OB"].Substring(0, 3).Trim() + ("&sPN="
                                    + ((intRecordPositionPageNum + 1) + ("\">"
                                    + (Variables.Forum.strTxtNext + " >></a>")))))))))))))));
                }

                sb.AppendLine("</td>");
            }

            // Reset Server Objects
            rsTopic = null;
            rsCommon = null;
            sb.AppendLine("</tr>");
            sb.AppendLine("</table>");
            sb.AppendLine("</form>");
            sb.AppendLine(@"<div align=""center"">");
            sb.AppendLine(@"<table width=""617"" border=""0"" cellspacing=""0"" cellpadding=""2"">");
            sb.AppendLine("<tr>");
            sb.AppendLine(@"<td class=""smText"" nowrap=""nowrap"" align=""left""><img src=""");
            sb.AppendLine(Variables.Forum.strImagePath);
            sb.AppendLine(@"no_new_posts_icon.gif"" alt=""");
            sb.AppendLine(Variables.Forum.strTxtOpenTopic);
            sb.AppendLine(@""">");
            sb.AppendLine(Variables.Forum.strTxtOpenTopic);
            sb.AppendLine("</td>");
            sb.AppendLine(@"<td class=""smText"" nowrap=""nowrap"" align=""left""><img src=""");
            sb.AppendLine(Variables.Forum.strImagePath);
            sb.AppendLine(@"hot_topic_no_new_posts_icon.gif"" alt=""");
            sb.AppendLine(Variables.Forum.strTxtHotTopic);
            sb.AppendLine(@""">");
            sb.AppendLine(Variables.Forum.strTxtHotTopic);
            sb.AppendLine("</td>");
            sb.AppendLine(@"<td class=""smText"" nowrap=""nowrap"" align=""left""><img src=""");
            sb.AppendLine(Variables.Forum.strImagePath);
            sb.AppendLine(@"priority_post_icon.gif"" alt=""");
            sb.AppendLine(Variables.Forum.strTxtHighPriorityPost);
            sb.AppendLine(@""">");
            sb.AppendLine(Variables.Forum.strTxtHighPriorityPost);
            sb.AppendLine("</td>");
            sb.AppendLine(@"<td class=""smText"" nowrap=""nowrap"" align=""left""><img src=""");
            sb.AppendLine(Variables.Forum.strImagePath);
            sb.AppendLine(@"pinned_topic_icon.gif"" alt=""");
            sb.AppendLine(Variables.Forum.strTxtPinnedTopic);
            sb.AppendLine(@""">");
            sb.AppendLine(Variables.Forum.strTxtPinnedTopic);
            sb.AppendLine("</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("<tr>");
            sb.AppendLine(@"<td class=""smText"" nowrap=""nowrap"" align=""left""><img src=""");
            sb.AppendLine(Variables.Forum.strImagePath);
            sb.AppendLine(@"new_posts_icon.gif"" alt=""");
            sb.AppendLine(Variables.Forum.strTxtOpenTopicNewReplies);
            sb.AppendLine(@""">");
            sb.AppendLine(Variables.Forum.strTxtOpenTopicNewReplies);
            sb.AppendLine("</td>");
            sb.AppendLine(@"<td class=""smText"" nowrap=""nowrap"" align=""left""><img src=""");
            sb.AppendLine(Variables.Forum.strImagePath);
            sb.AppendLine(@"hot_topic_new_posts_icon.gif"" alt=""");
            sb.AppendLine(Variables.Forum.strTxtHotTopicNewReplies);
            sb.AppendLine(@""">");
            sb.AppendLine(Variables.Forum.strTxtHotTopicNewReplies);
            sb.AppendLine("</td>");
            sb.AppendLine(@"<td class=""smText"" nowrap=""nowrap"" align=""left""><img src=""");
            sb.AppendLine(Variables.Forum.strImagePath);
            sb.AppendLine(@"priority_post_locked_icon.gif"" alt=""");
            sb.AppendLine(Variables.Forum.strTxtHighPriorityPostLocked);
            sb.AppendLine(@""">");
            sb.AppendLine(Variables.Forum.strTxtHighPriorityPostLocked);
            sb.AppendLine("</td>");
            sb.AppendLine(@"<td class=""smText"" nowrap=""nowrap"" align=""left""><img src=""");
            sb.AppendLine(Variables.Forum.strImagePath);
            sb.AppendLine(@"closed_topic_icon.gif"" alt=""");
            sb.AppendLine(Variables.Forum.strTxtLockedTopic);
            sb.AppendLine(@""">");
            sb.AppendLine(Variables.Forum.strTxtLockedTopic);
            sb.AppendLine("</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</table>");
            sb.AppendLine("<br />");
            // Display the process time
            // If Variables.Forum.blnShowProcessTime Then sb.AppendLine("<span class=""smText""><br /><br />" & Variables.Forum.strTxtThisPageWasGeneratedIn & " " & FormatNumber(Timer() - Variables.Forum.dblStartTime, 4) & " " & Variables.Forum.strTxtSeconds & "</span>")}
            sb.AppendLine("</div>");
            return sb.ToString();
        }
    }
}