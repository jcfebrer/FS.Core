// <fileheader>
// <copyright file="post_message.aspx.cs" company="Febrer Software">
//     Fecha: 30/11/2007
//     Path: forum\post_message.aspx.cs
//     Copyright (c) 2003-2007 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>
using FSForum.Includes;
using FSNetwork;
using FSLibrary;
using FSMail;
using FSPortal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;

namespace FSForum
{
    public class post_message : FSPortal.BasePage
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
            // Response.Buffer    = True
            // Dimension variables
            long lngNumOfPosts = 0;
            // Holds the number of posts a user has made
            bool blnEmailNotify;
            // Set to    true if    the users want to be notified by e-mail    of a post
            string strEmailSubject;
            // Holds the subject of the e-mail
            string strMessage;
            // Holds the Users Message
            long lngMessageID;
            // Holds the message ID number
            string strMode;
            // Holds the mode    of the page so we know whether we are editing, updating, or new    topic
            long lngTopicID;
            // Holds the topic ID number
            string strSubject;
            // Holds the subject
            string strPostDateTime;
            // Holds the current date    and time for the post
            string strusuario;
            // Holds the usuario of the person we are going to email
            long lngEmailUserID;
            // Holds the users ID of the person we are going to email
            string strUserEmail;
            // Holds the users e-mail    address
            string strEmailMessage;
            // Holds the body    of the e-mail
            bool blnSignature;
            // Holds wether a    signature is to    be shown or not
            int intPriority;
            // Holds the priority of tipics
            string strPostMessage = "";
            int intReturnPageNum;
            // Holds the page    number to return to
            string strForumName = "";
            int intNumOfPostsInFiveMin;
            // Holds the number of posts the user has    made in    the last 5 minutes
            string strReturnCode = "";
            string strPollQuestion = "";
            bool blnMultipleVotes = false;
            // Set to    true if    multiple votes are allowed
            bool blnPollReply = false;
            // Set to    true if    users can't reply to a poll
            string[] saryPollChoice = { "" };
            // Array to hold the poll    choices
            int intPollChoice;
            // Holds the poll    choices    loop counter
            string strBadWord;
            // Holds the bad words
            string strBadWordReplace;
            // Holds the rplacment word for the bad word
            long lngPollID;
            // Holds the poll    ID number
            bool blnForumLocked;
            // Set to true if the forum is locked
            bool blnTopicLocked;
            // Set to true if the topic is locked
            int intNewGroupID;
            // Holds the new group ID for the poster
            string strGuestName = "";
            // Initalise variables
            strPostDateTime = System.DateTime.Now.ToString();
            intNumOfPostsInFiveMin = 0;
            lngPollID = 0;
            blnForumLocked = false;
            blnTopicLocked = false;
            if (((FSPortal.Variables.User.UsuarioId == 0)
                        || (Variables.Forum.blnActiveMember == false)))
            {
                // Clean up
                // rsCommon = Nothing
                // adoCon.Close()
                // adoCon = Nothing
                // Redirect
                Response.Redirect("default.aspx");
            }

            // ******************************************
            // ***          Check IP address        ***
            // ******************************************
            // If the    user is    user is    using a    banned IP redirect to an error page
            if (common.bannedIP())
            {
                // Clean up
                // rsCommon = Nothing
                // adoCon.Close()
                // adoCon = Nothing
                // Redirect
                Response.Redirect("insufficient_permission.aspx?M=IP");
            }

            // ******************************************
            // ***      Read in form details        ***
            // ******************************************
            // Read in user deatils from the post message form
            strMode = Request.Form["mode"].Substring(0, 10).Trim();
            Variables.Forum.intForumID = int.Parse(Request.Form["FID"]);
            lngTopicID = long.Parse(Request.Form["TID"]);
            strSubject = Request.Form["subject"].Substring(0, 41).Trim();
            strMessage = Request.Form["Message"];
            lngMessageID = long.Parse(Request.Form["PID"]);
            blnEmailNotify = Functions.ValorBool(Request.Form["email"]);
            blnSignature = Functions.ValorBool(Request.Form["signature"]);
            intPriority = int.Parse(Request.Form["priority"]);
            if ((FSPortal.Variables.User.UsuarioId == 2))
            {
                strGuestName = Request.Form["Gname"].Substring(0, 20).Trim();
            }

            // ******************************************
            // ***         Get permissions          *****
            // ******************************************
            // Read in the forum name    and forum permssions from the database
            // Initalise the strSQL variable with an SQL statement to    query the database
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
                            + (Variables.Forum.strDbTable + ("Forum WHERE\tForum_ID = "
                            + (Variables.Forum.intForumID + ";"))))));
            }

            // Query the database
            DataTable rsCommon;
            rsCommon = db.Execute(strSQL);
            // If there is a record returned by the recordset    then get the forum permssions
            if ((rsCommon.Rows.Count > 0))
            {
                // See if the forum is locked if this is not the admin
                if ((FSPortal.Variables.User.Administrador == false))
                {
                    blnForumLocked = Functions.ValorBool(rsCommon.Rows[0]["Locked"]);
                }

                // Check the user    is welcome in this forum
                FuncionesForum.forumPermisisons(Variables.Forum.intForumID, FSPortal.Variables.User.GroupId, int.Parse(rsCommon.Rows[0]["Read"].ToString()), int.Parse(rsCommon.Rows[0]["Post"].ToString()), int.Parse(rsCommon.Rows[0]["Reply_posts"].ToString()), int.Parse(rsCommon.Rows[0]["Edit_posts"].ToString()), 0, int.Parse(rsCommon.Rows[0]["Priority_posts"].ToString()), int.Parse(rsCommon.Rows[0]["Poll_create"].ToString()), 0, 0, 0);
            }

            // ******************************************
            // ***    See if the topic is closed    *****
            // ******************************************
            // If this is not a new topic see if the topic is closed
            if (((strMode != "new")
                        && ((blnForumLocked == false)
                        && ((FSPortal.Variables.User.Administrador == false)
                        && (Variables.Forum.blnModerator == false)))))
            {
                // Initliase the SQL query to get the topic details from the database
                strSQL = ("SELECT "
                            + (Variables.Forum.strDbTable + ("Topic.Locked FROM "
                            + (Variables.Forum.strDbTable + ("Topic WHERE "
                            + (Variables.Forum.strDbTable + ("Topic.Topic_ID = "
                            + (lngTopicID + ";"))))))));
                rsCommon = db.Execute(strSQL);
                // If there is a record returened see if the topic is closed
                if ((rsCommon.Rows.Count > 0))
                {
                    blnTopicLocked = Functions.ValorBool(rsCommon.Rows[0]["Locked"]);
                }

            }

            // *****************************************************
            // ***   Redirect if the forum or topic is locked   ****
            // *****************************************************
            // If the forum or topic is locked then don't let the user post a message
            if ((Variables.Forum.blnForumLocked || Variables.Forum.blnTopicLocked))
            {
                // Clean up
                // Set rsCommon = Nothing
                // adoCon.Close
                // Set adoCon = Nothing
                // Redirect to error page
                if (blnForumLocked)
                {
                    Response.Redirect("not_posted.aspx?mode=FLocked");
                }
                else
                {
                    Response.Redirect("not_posted.aspx?mode=TClosed");
                }

            }

            // ******************************************
            // ***     Get return page details      *****
            // ******************************************
            // If there is no    number must be a new post
            if ((Request.Form["TPN"] == ""))
            {
                intReturnPageNum = 1;
            }
            else
            {
                intReturnPageNum = int.Parse(Request.Form["TPN"]);
            }

            // calcultae which page the tread    is posted on
            if (!(Request.Form["ThreadPos"] == ""))
            {
                // If the    position in the    topic is on next page add 1 to the return page number
                if ((int.Parse(Request.Form["ThreadPos"])
                            > (Variables.Forum.intThreadsPerPage * intReturnPageNum)))
                {
                    intReturnPageNum = (intReturnPageNum + 1);
                }

            }

            // ********************************************
            // ***  Clean up and check in form details  ***
            // ********************************************
            // If there is no    subject    or message then    don't post the message as won't    be able    to link    to it
            if (((strSubject == "")
                        && ((strMode == "new")
                        || ((strMode == "editTopic")
                        || (strMode == "poll")))))
            {
                strReturnCode = "noSubject";
            }

            if (((strMessage.Trim() == "")
                        || ((strMessage.Trim() == "<P> </P>")
                        || (strMessage.Trim() == "<br />"))))
            {
                strReturnCode = "noSubject";
            }

            if ((Request.Form["browser"] == "RTE"))
            {
                // Call the function to format WYSIWYG posts
                strMessage = FuncionesForum.WYSIWYGFormatPost(strMessage);
                // Else standrd editor is    used so    convert    forum codes
            }
            else
            {
                // Call the function to format posts
                strMessage = FuncionesForum.FormatPost(strMessage);
            }

            // If the user wants forum codes enabled then format the post using them
            if (Functions.ValorBool(Request.Form["forumCodes"]))
            {
                strMessage = FuncionesForum.FormatForumCodes(strMessage);
            }

            // Check the message for malicious HTML code
            strMessage = FuncionesFilter.checkHTML(strMessage);
            // Strip long text strings from message
            strMessage = FuncionesFilter.removeLongText(strMessage);
            // Get rid of scripting tags in the subject
            strSubject = FuncionesFilter.removeAllTags(strSubject);
            strSubject = FuncionesFilter.formatInput(strSubject);
            // If the user is in a guest then clean up their usuario to remove malicious code
            if ((FSPortal.Variables.User.UsuarioId == 2))
            {
                strGuestName = FuncionesFilter.formatSQLInput(strGuestName);
                strGuestName = FuncionesFilter.formatInput(strGuestName);
            }

            // ********************************************
            // ***    Read in    poll details (if Poll)      ***
            // ********************************************
            // If this is a poll then    clean read in the poll details
            if (((strMode == "poll")
                        && Variables.Forum.blnPollCreate))
            {
                // Read in poll question and multiple votes
                strPollQuestion = Request.Form["pollQuestion"].Substring(0, 70).Trim();
                blnMultipleVotes = bool.Parse(Request.Form["multiVote"]);
                blnPollReply = bool.Parse(Request.Form["pollReply"]);
                if ((strPollQuestion == ""))
                {
                    strReturnCode = "noPoll";
                }

                strPollQuestion = FuncionesFilter.removeAllTags(strPollQuestion);
                strPollQuestion = FuncionesFilter.formatInput(strPollQuestion);
                // Loop through and read in the poll question
                for (intPollChoice = 1; (intPollChoice <= Variables.Forum.intMaxPollChoices); intPollChoice++)
                {
                    // ReDimension the array for the correct number of choices
                    // ReDimensioning    arrays is bad for performance but usful    in this    for what I need    it for
                    //object saryPollChoice[intPollChoice];
                    // Read in the poll choice
                    saryPollChoice[intPollChoice] = Request.Form[("choice" + intPollChoice)].Substring(0, 60).Trim();
                    // If there is nothing in    position 1 and 2 throw up an error
                    if (((intPollChoice < 2)
                                && (saryPollChoice[intPollChoice] == "")))
                    {
                        strReturnCode = "noPoll";
                    }

                    if ((saryPollChoice[intPollChoice] == ""))
                    {
                        // ReDimension the array for the correct number of choices
                        //object Preserve;
                        Array.Resize(ref saryPollChoice, (intPollChoice - 1));
                        // Exit loop
                        break;
                    }

                    // Clean up input
                    saryPollChoice[intPollChoice] = FuncionesFilter.removeAllTags(saryPollChoice[intPollChoice]);
                    saryPollChoice[intPollChoice] = FuncionesFilter.formatInput(saryPollChoice[intPollChoice]);
                }

            }

            // ******************************************
            // ***         Filter Bad    Words          *****
            // ******************************************
            // Initalise the SQL string with a query to read in all the words    from the smut table
            strSQL = ("SELECT "
                        + (Variables.Forum.strDbTable + ("Smut.* FROM\t"
                        + (Variables.Forum.strDbTable + "Smut"))));
            rsCommon = db.Execute(strSQL);
            // Loop through all the words to check for
            foreach (DataRow row in rsCommon.Rows)
            {
                // Put the bad word into a string    for imporoved perfoamnce
                strBadWord = row["Smut"].ToString();
                strBadWordReplace = row["Word_replace"].ToString();
                // Replace the swear words with the words    in the database    the swear words
                strSubject = strSubject.Replace(strBadWord, strBadWordReplace);
                strMessage = strMessage.Replace(strBadWord, strBadWordReplace);
                // If this is a poll run the poll    choices    through    the bad    word filter as well
                if ((strMode == "poll"))
                {
                    // Clean up the poll question
                    strPollQuestion = strPollQuestion.Replace(strBadWord, strBadWordReplace);
                    // Loop though and check all the strings in the Poll array
                    for (intPollChoice = 1; (intPollChoice <= saryPollChoice.Length); intPollChoice++)
                    {
                        saryPollChoice[intPollChoice] = saryPollChoice[intPollChoice].Replace(strBadWord, strBadWordReplace);
                    }

                }

                // Move to the next word in the recordset
            }

            // ******************************************
            // ***          Anti-spam    Check        ***
            // ******************************************
            // Check the user    is not pressing    refresh    and submitting the same    post more than once
            if (((strMode != "edit")
                        && (strMode != "editTopic")))
            {
                // Initalise the SQL string with a query to read in the last post    from the database
                strSQL = ("SELECT TOP 15\t"
                            + (Variables.Forum.strDbTable + ("Thread.Message, "
                            + (Variables.Forum.strDbTable + ("Thread.UsuarioID,\t"
                            + (Variables.Forum.strDbTable + ("Thread.Message_date FROM "
                            + (Variables.Forum.strDbTable + ("Thread ORDER BY "
                            + (Variables.Forum.strDbTable + "Thread.Message_date DESC;"))))))))));
                rsCommon = db.Execute(strSQL);
                // If there is a post returned by    the recorset then check    it's not already posted    and for    spammers
                if ((rsCommon.Rows.Count > 0))
                {
                    // Check the last    message    posted is not the same as the new one
                    if (((rsCommon.Rows[0]["Message"].ToString() == strMessage)
                                && (strMode != "edit"
                                || (strMode == "editTopic"))))
                    {
                        // Set the return    code
                        strReturnCode = "posted";
                    }

                    // Check the user    hasn't posted in the last limit    set for    secounds and not more than 5 times in the last spam time limit set for minutes
                    foreach (DataRow row in rsCommon.Rows)
                    {
                        if (((FSPortal.Variables.User.Administrador == false)
                                    && (FSPortal.Variables.User.UsuarioId != 2)))
                        {
                            break;
                        }

                        // Check the user    hasn't posted in the last spam time limit set for seconds
                        if (((double.Parse(row["UsuarioID"].ToString()) == FSPortal.Variables.User.UsuarioId)
                                    && (System.DateTime.Now - System.DateTime.Parse(row["Message_date"].ToString())).TotalSeconds < Variables.Forum.intSpamTimeLimitSeconds)
                                    && (Variables.Forum.intSpamTimeLimitSeconds != 0))
                        {
                            // Set the return    code
                            strReturnCode = "maxS";
                        }

                        // Check that the    user hasn't posted 5 posts in the spam time limit set for minutes
                        if (((double.Parse(row["UsuarioID"].ToString()) == FSPortal.Variables.User.UsuarioId)
                                    && (System.DateTime.Now - System.DateTime.Parse(row["Message_date"].ToString())).TotalSeconds < Variables.Forum.intSpamTimeLimitMinutes)
                                    && (Variables.Forum.intSpamTimeLimitMinutes != 0))
                        {
                            // Add 1 to the number of    posts in the last 5 minutes
                            intNumOfPostsInFiveMin = (intNumOfPostsInFiveMin + 1);
                            // If the    number of posts    is more    than 3 then set    the return code
                            if ((intNumOfPostsInFiveMin == 5))
                            {
                                // Set the return    code
                                strReturnCode = "maxM";
                            }

                        }

                    }

                }

            }

            // **********************************************
            // ***  If input problems    send to    error page  ***
            // **********************************************
            // If there is a return code then    this post is not valid so redirect to error page
            if ((strReturnCode != ""))
            {
                // Clean up
                // Set rsCommon = Nothing
                // adoCon.Close
                // Set adoCon = Nothing
                // Redirect to error page
                Response.Redirect(("not_posted.aspx?mode=" + strReturnCode));
            }

            // ********************************************
            // ***          Save new Poll          ***
            // ********************************************
            // If this is a poll then    clean read in the poll details
            if (((strMode == "poll")
                        && Variables.Forum.blnPollCreate))
            {
                // ********************************************
                // ***         Save poll question          ***
                // ********************************************
                // Initalise the SQL string with a query to get the poll details
                strSQL = ("SELECT TOP 1 "
                            + (Variables.Forum.strDbTable + ("Poll.* FROM "
                            + (Variables.Forum.strDbTable + ("Poll ORDER BY "
                            + (Variables.Forum.strDbTable + "Poll.Poll_ID DESC;"))))));
                // With...
                // Set the cursor    type property of the record set    to Dynamic so we can navigate through the record set
                // .CursorType = 2
                // Set the Lock Type for the records so that the record set is only locked when it is updated
                // .LockType = 3
                // Open the Usuarios table
                // db.Execute(strSQL)
                // Insert    the new    poll question in the recordset
                // .AddNew
                // Update    recordset
                // .Fields("Poll_question") = strPollQuestion
                // .Fields("Multiple_votes") = blnMultipleVotes
                // .Fields("Reply") = blnPollReply
                // Update    the database with the new poll question
                // .Update
                // Re-run    the Query once the database has    been updated
                // .Requery
                FSDatabase.Register reg = new FSDatabase.Register();
                reg.Add(new FSDatabase.Field("Poll_question", strPollQuestion, typeof(string)));
                reg.Add(new FSDatabase.Field("Multiple_votes", blnMultipleVotes.ToString(), typeof(bool)));
                reg.Add(new FSDatabase.Field("Reply", blnPollReply.ToString(), typeof(bool)));
                db.ExecuteNonQuery(db.InsertSql("ForumPoll", reg, FSPortal.Variables.User.UsuarioId));
                // Read in the new topic's ID number
                lngPollID = long.Parse(db.GetIdentity());
                // ********************************************
                // ***          Save poll    choices          ***
                // ********************************************
                // Add the new poll choices
                for (intPollChoice = 1; (intPollChoice <= saryPollChoice.Length); intPollChoice++)
                {
                    // Initalise the SQL string with a query to get the choice
                    strSQL = ("SELECT TOP 1 "
                                + (Variables.Forum.strDbTable + ("PollChoice.* FROM "
                                + (Variables.Forum.strDbTable + "PollChoice;"))));
                    // With...
                    // Set the cursor    type property of the record set    to Dynamic so we can navigate through the record set
                    // .CursorType = 2
                    // Set the Lock Type for the records so that the record set is only locked when it is updated
                    // .LockType = 3
                    // Open the Usuarios table
                    // =db.Execute(strSQL)
                    // Insert    the new    poll choices in    the recordset
                    // .AddNew
                    // Update    recordset
                    // .Fields("Poll_ID") = lngPollID
                    // .Fields("Choice") = saryPollChoice(intPollChoice)
                    // Update    the database with the new poll choices
                    // .Update
                    // Re-run    the Query once the database has    been updated
                    // .Requery
                    FSDatabase.Register reg2 = new FSDatabase.Register();
                    reg2.Add(new FSDatabase.Field("Poll_ID", lngPollID.ToString(), typeof(string)));
                    reg2.Add(new FSDatabase.Field("Choice", saryPollChoice[intPollChoice], typeof(string)));
                    db.Execute(db.InsertSql("ForumPollChoice", reg2, FSPortal.Variables.User.UsuarioId));
                }

                // Change    the mode to new    to save    the new    polls post message
                strMode = "new";
            }

            // ******************************************
            // ***     Save new topic    subject        ***
            // ******************************************
            // If this is a new topic    then save the new subject heading and read back    the new    topic ID number
            if (((strMode == "new")
                        && (Variables.Forum.blnPost
                        || (Variables.Forum.blnPollCreate
                        || (FSPortal.Variables.User.Administrador || Variables.Forum.blnModerator)))))
            {
                // Initalise the SQL string with a query to get the Topic    details
                strSQL = ("SELECT TOP 1 "
                            + (Variables.Forum.strDbTable + ("Topic.* FROM "
                            + (Variables.Forum.strDbTable + "Topic\t"))));
                strSQL = (strSQL + ("WHERE Forum_ID ="
                            + (Variables.Forum.intForumID + " ")));
                strSQL = (strSQL + ("ORDER By "
                            + (Variables.Forum.strDbTable + "Topic.Start_date\tDESC;")));
                // With...
                // Set the cursor    type property of the record set    to Dynamic so we can navigate through the record set
                // .CursorType = 2
                // Set the Lock Type for the records so that the record set is only locked when it is updated
                // .LockType = 3
                // Open the Usuarios table
                // =db.Execute(strSQL)
                // Insert    the new    topic details in the recordset
                // .AddNew
                // Update    recordset
                // .Fields("Forum_ID") = intForumID
                // .Fields("Poll_ID") = lngPollID
                // .Fields("Subject") = strSubject
                // .Fields("Priority") = intPriority
                // .Fields("Start_date") =    strPostDateTime
                // Update    the database with the new topic    details
                // .Update
                // Re-run    the Query once the database has    been updated
                // .Requery
                FSDatabase.Register reg = new FSDatabase.Register();
                reg.Add(new FSDatabase.Field("Forum_ID", Variables.Forum.intForumID.ToString(), typeof(long)));
                reg.Add(new FSDatabase.Field("Poll_ID", lngPollID.ToString(), typeof(long)));
                reg.Add(new FSDatabase.Field("Subject", strSubject.ToString(), typeof(string)));
                reg.Add(new FSDatabase.Field("Priority", intPriority.ToString(), typeof(long)));
                reg.Add(new FSDatabase.Field("Start_date", strPostDateTime.ToString(), typeof(string)));
                db.ExecuteNonQuery(db.InsertSql("ForumTopic", reg, FSPortal.Variables.User.UsuarioId));
                // Read in the new topic's ID number
                lngTopicID = long.Parse(db.GetIdentity());
                intReturnPageNum = 1;
            }

            // ******************************************
            // ***         Edit Topic    Update        ***
            // ******************************************
            // If the    post is    the first in the thread    then update the    topic details
            if (((strMode == "editTopic")
                        && ((Variables.Forum.blnEdit == true)
                        || (FSPortal.Variables.User.Administrador || Variables.Forum.blnModerator))))
            {
                // Initalise the SQL string with a query to get the Topic    details
                strSQL = ("SELECT "
                            + (Variables.Forum.strDbTable + ("Topic.Subject, "
                            + (Variables.Forum.strDbTable + ("Topic.Priority FROM "
                            + (Variables.Forum.strDbTable + "Topic "))))));
                strSQL = (strSQL + ("WHERE Topic_ID ="
                            + (lngTopicID + ";")));
                // With...
                // Set the cursor    type property of the record set    to Dynamic so we can navigate through the record set
                // .CursorType = 2
                // Set the Lock Type for the records so that the record set is only locked when it is updated
                // .LockType = 3
                // Open the Usuarios table
                // =db.Execute(strSQL)
                // Update    the recorset
                // .Fields("Subject") = strSubject
                // .Fields("Priority") = intPriority
                // Update    the database with the new topic    details
                // .Update
                FSDatabase.Register reg = new FSDatabase.Register();
                reg.Add(new FSDatabase.Field("Subject", strSubject.ToString(), typeof(string)));
                reg.Add(new FSDatabase.Field("Priority", intPriority.ToString(), typeof(long)));
                db.Execute(db.UpdateSql("ForumTopic", reg, ("Topic_ID=" + lngTopicID)));
                // Change    the mode to edit
                strMode = "edit";
            }

            // ******************************************
            // ***        Edit Post Update        ***
            // ******************************************
            // If the    post is    a previous post    that has been edited then update the post
            if (((strMode == "edit")
                        && ((Variables.Forum.blnEdit == true)
                        || ((FSPortal.Variables.User.Administrador == true)
                        || (Variables.Forum.blnModerator == true)))))
            {
                // If we are to show who edit the post and time then contantinet it to the end of the message
                if (Variables.Forum.blnShowEditUser)
                {
                    strMessage = (strMessage + ("<edited><editID>"
                                + (Variables.Forum.strLoggedInusuario + ("</editID><editDate>"
                                + (System.DateTime.Now.ToString() + "</editDate></edited>")))));
                }

                // Initalise the strSQL variable with an SQL statement to    query the database get the message details
                strSQL = ("SELECT "
                            + (Variables.Forum.strDbTable + ("Thread.Thread_ID, "
                            + (Variables.Forum.strDbTable + ("Thread.Message, "
                            + (Variables.Forum.strDbTable + ("Thread.Show_signature, "
                            + (Variables.Forum.strDbTable + "Thread.IP_addr "))))))));
                strSQL = (strSQL + ("FROM\t"
                            + (Variables.Forum.strDbTable + "Thread ")));
                strSQL = (strSQL + ("WHERE "
                            + (Variables.Forum.strDbTable + ("Thread.Thread_ID="
                            + (lngMessageID + ";")))));
                // With...
                // Set the cursor    type property of the record set    to Dynamic so we can navigate through the record set
                // .CursorType = 2
                // Set the Lock Type for the records so that the record set is only locked when it is updated
                // .LockType = 3
                // Open the Usuarios table
                // =db.Execute(strSQL)
                // Enter the updated post    into the recordset
                // .Fields("Message") = strMessage
                // .Fields("Show_signature") = CBool(blnSignature)
                // .Fields("IP_addr") = getIP()
                // Update    the database
                // .Update
                FSDatabase.Register reg = new FSDatabase.Register();
                reg.Add(new FSDatabase.Field("Message", strMessage.ToString(), typeof(string)));
                reg.Add(new FSDatabase.Field("Show_signature", blnSignature.ToString(), typeof(bool)));
                reg.Add(new FSDatabase.Field("IP_addr", Http.IpAddress().ToString(), typeof(string)));
                db.Execute(db.UpdateSql("ForumThread", reg, ("Thread_ID=" + lngMessageID)));
                // ******************************************
                // ***      Else Process New Post        ***
                // ******************************************
                // Else this is a    new post so save the new post to the database
            }
            else if (((((strMode == "new")
                        && (Variables.Forum.blnPost || Variables.Forum.blnPollCreate))
                        || Variables.Forum.blnReply)
                        || (FSPortal.Variables.User.Administrador || Variables.Forum.blnModerator)))
            {
                // ******************************************
                // ***           Save New    Post        ***
                // ******************************************
                // Initalise the strSQL variable with an SQL statement to    query the database get the message details
                strSQL = ("SELECT TOP 1 "
                            + (Variables.Forum.strDbTable + ("Thread.Thread_ID, "
                            + (Variables.Forum.strDbTable + ("Thread.Topic_ID, "
                            + (Variables.Forum.strDbTable + ("Thread.UsuarioID,\t"
                            + (Variables.Forum.strDbTable + ("Thread.Message, "
                            + (Variables.Forum.strDbTable + ("Thread.Message_date, "
                            + (Variables.Forum.strDbTable + ("Thread.Show_signature, "
                            + (Variables.Forum.strDbTable + "Thread.IP_addr "))))))))))))));
                strSQL = (strSQL + ("FROM\t"
                            + (Variables.Forum.strDbTable + "Thread ")));
                strSQL = (strSQL + ("ORDER BY "
                            + (Variables.Forum.strDbTable + "Thread.Thread_ID DESC;")));
                // With...
                // Set the cursor    type property of the record set    to Dynamic so we can navigate through the record set
                // .CursorType = 2
                // Set the Lock Type for the records so that the record set is only locked when it is updated
                // .LockType = 3
                // Open the threads table
                // =db.Execute(strSQL)
                // Insert    the new    Thread details in the recordset
                // .AddNew
                // .Fields("Topic_ID") = lngTopicID
                // .Fields("UsuarioID") = FSPortal.Variables.User.UsuarioId
                // .Fields("Message") = strMessage
                // .Fields("Message_date")    = strPostDateTime
                // .Fields("Show_signature") = blnSignature
                // .Fields("IP_addr") = getIP()
                // Update    the database with the new Thread
                // .Update
                // Requery cuase Access is so slow (needed to get    accurate post count)
                // .Requery()
                FSDatabase.Register reg = new FSDatabase.Register();
                reg.Add(new FSDatabase.Field("Topic_ID", lngTopicID.ToString(), typeof(long)));
                reg.Add(new FSDatabase.Field("UsuarioID", FSPortal.Variables.User.UsuarioId.ToString(), typeof(long)));
                reg.Add(new FSDatabase.Field("Message", strMessage.ToString(), typeof(string)));
                reg.Add(new FSDatabase.Field("Message_date", strPostDateTime.ToString(), typeof(string)));
                reg.Add(new FSDatabase.Field("Show_signature", blnSignature.ToString(), typeof(bool)));
                reg.Add(new FSDatabase.Field("IP_addr", Http.IpAddress().ToString(), typeof(string)));
                db.ExecuteNonQuery(db.InsertSql("ForumThread", reg, FSPortal.Variables.User.UsuarioId));
                // Read in the thread ID for the guest posting
                lngMessageID = long.Parse(db.GetIdentity());
                // ******************************************
                // ***     Update    Topic Last Post    Date    ***
                // ******************************************
                // Initalise the SQL string with an SQL update command to    update the date    of the last post in the    Topic table
                strSQL = ("UPDATE "
                            + (Variables.Forum.strDbTable + "Topic SET "));
                strSQL = (strSQL
                            + (Variables.Forum.strDbTable + ("Topic.Last_entry_date = "
                            + (Variables.Forum.strDatabaseDateFunction + " "))));
                strSQL = (strSQL + ("WHERE ((("
                            + (Variables.Forum.strDbTable + ("Topic.Topic_ID)=\t"
                            + (lngTopicID + "));")))));
                db.Execute(strSQL);
                // ******************************************
                // ***     Save the guest usuario    ***
                // ******************************************
                // If this is a guest that is posting then save there name to the db
                if (((FSPortal.Variables.User.UsuarioId == 2)
                            && (strGuestName != "")))
                {
                    // Initalise the SQL string with an SQL update command to    update the date    of the last post in the    Topic table
                    strSQL = ("INSERT INTO "
                                + (Variables.Forum.strDbTable + "GuestName ("));
                    strSQL = (strSQL + "[Name], ");
                    strSQL = (strSQL + "[Thread_ID] ");
                    strSQL = (strSQL + ") ");
                    strSQL = (strSQL + "VALUES ");
                    strSQL = (strSQL + ("(\'"
                                + (strGuestName + "\', ")));
                    strSQL = (strSQL + ("\'"
                                + (lngMessageID + "\' ")));
                    strSQL = (strSQL + ")");
                    db.Execute(strSQL);
                }

                // ******************************************
                // ***    Update Usuarios Number of    Posts    ***
                // ******************************************
                // Initalise the strSQL variable with an SQL statement to    query the database to get the number of    posts the user has made
                strSQL = ("SELECT " + ("Usuarios.No_of_posts, "
                            + (Variables.Forum.strDbTable + "Group.Special_rank ")));
                strSQL = (strSQL + ("FROM\t" + ("Usuarios, "
                            + (Variables.Forum.strDbTable + "Group "))));
                strSQL = (strSQL + ("WHERE " + ("Usuarios.Group_ID = "
                            + (Variables.Forum.strDbTable + ("Group.Group_ID AND " + ("Usuarios.UsuarioID= "
                            + (FSPortal.Variables.User.UsuarioId + ";")))))));
                rsCommon = db.Execute(strSQL);
                // If there is a record returned by the database then read in the    no of posts and    increment it by    1
                if ((rsCommon.Rows.Count > 0))
                {
                    // Read in the no    of posts the user has made and usuario
                    lngNumOfPosts = long.Parse(rsCommon.Rows[0]["No_of_posts"].ToString());
                    lngNumOfPosts = (lngNumOfPosts + 1);
                    // Initalise the SQL string with an SQL update command to    update the number of posts the user has    made
                    strSQL = ("UPDATE " + "Usuarios SET ");
                    strSQL = (strSQL + ("" + ("Usuarios.No_of_posts = " + lngNumOfPosts)));
                    strSQL = (strSQL + (" WHERE " + ("Usuarios.UsuarioID= "
                                + (FSPortal.Variables.User.UsuarioId + ";"))));
                    db.Execute(strSQL);
                }

                // ******************************************
                // ***        Update Rank Group            ***
                // ******************************************
                // See if the user is a member of a rank group and if so update their group if they have enough posts
                // If there is a record returned by the database then see if it is a group that needs updating
                if ((rsCommon.Rows.Count > 0))
                {
                    // If not a non rank group then see if the group needs updating
                    if ((Functions.ValorBool(rsCommon.Rows[0]["Special_rank"]) == false))
                    {
                        // Initlise variables
                        intNewGroupID = FSPortal.Variables.User.GroupId;
                        // Get the rank group the member shoukd be part of
                        // Initalise the strSQL variable with an SQL statement to    query the database to get the number of    posts the user has made
                        strSQL = ("SELECT TOP 1 "
                                    + (Variables.Forum.strDbTable + "Group.Group_ID "));
                        strSQL = (strSQL + ("FROM "
                                    + (Variables.Forum.strDbTable + "Group ")));
                        strSQL = (strSQL + ("WHERE ("
                                    + (Variables.Forum.strDbTable + ("Group.Minimum_posts <= "
                                    + (lngNumOfPosts + (") And ("
                                    + (Variables.Forum.strDbTable + "Group.Minimum_posts >= 0) ")))))));
                        strSQL = (strSQL + ("ORDER BY "
                                    + (Variables.Forum.strDbTable + "Group.Minimum_posts DESC;")));
                        rsCommon = db.Execute(strSQL);
                        // Get the new Group ID
                        if ((rsCommon.Rows.Count > 0))
                        {
                            intNewGroupID = int.Parse(rsCommon.Rows[0]["Group_ID"].ToString());
                        }

                        if ((FSPortal.Variables.User.GroupId != intNewGroupID))
                        {
                            // Initalise the SQL string with an SQL update command to    update group ID of the Usuarios
                            strSQL = ("UPDATE " + "Usuarios SET ");
                            strSQL = (strSQL + ("" + ("Usuarios.Group_ID = " + intNewGroupID)));
                            strSQL = (strSQL + (" WHERE " + ("Usuarios.UsuarioID= "
                                        + (FSPortal.Variables.User.UsuarioId + ";"))));
                            db.Execute(strSQL);
                        }

                    }

                }

                // ******************************************
                // ***       Send    Email Notification     **
                // ******************************************
                if ((Variables.Forum.blnEmail == true))
                {
                    // **********************************************************
                    // *** Format the    post if    it is to be sent with the email     **
                    // **********************************************************
                    // Get the subject of the    topic the thread is posted in
                    // Initalise the SQL string with a query to get the Topic    details
                    strSQL = ("SELECT "
                                + (Variables.Forum.strDbTable + ("Forum.Forum_name, "
                                + (Variables.Forum.strDbTable + "Topic.Subject "))));
                    strSQL = (strSQL + ("FROM\t"
                                + (Variables.Forum.strDbTable + ("Forum INNER JOIN "
                                + (Variables.Forum.strDbTable + ("Topic ON\t"
                                + (Variables.Forum.strDbTable + ("Forum.Forum_ID = "
                                + (Variables.Forum.strDbTable + "Topic.Forum_ID ")))))))));
                    strSQL = (strSQL + ("WHERE Topic_ID ="
                                + (lngTopicID + ";")));
                    rsCommon = db.Execute(strSQL);
                    // If there is records returned then read    in the details
                    if ((rsCommon.Rows.Count > 0))
                    {
                        // Read in the forum name    and subject
                        strSubject = rsCommon.Rows[0]["Subject"].ToString();
                        strForumName = rsCommon.Rows[0]["Forum_name"].ToString();
                    }

                    // Set the e-mail    subject
                    strEmailSubject = (Variables.Forum.strMainForumName + (" : " + FuncionesFilter.decodeString(strSubject)));
                    // If we are to send an e-mail notification and send the post with the e-mail then format    the post for the e-mail
                    if ((Variables.Forum.blnSendPost == true))
                    {
                        // Format    the post to be sent with the e-mail
                        strPostMessage = ("<br /><b>"
                                    + (Variables.Forum.strTxtForum + (":</b> " + strForumName)));
                        strPostMessage = (strPostMessage + ("<br /><b>"
                                    + (Variables.Forum.strTxtTopic + (":</b> " + strSubject))));
                        strPostMessage = (strPostMessage + ("<br /><b>"
                                    + (Variables.Forum.strTxtPostedBy + (":</b>\t"
                                    + (Variables.Forum.strLoggedInusuario + "<br /><br />")))));
                        strPostMessage = (strPostMessage + strMessage);
                        // Change    the path to the    emotion    symbols    to include the path to the images
                        strPostMessage = strPostMessage.Replace("src=\"smileys/", ("src=\""
                                        + (FSPortal.Variables.App.webHttp + "/smileys/")));
                    }

                    // *******************************************
                    // ***       Send    Email Notification     ***
                    // *******************************************
                    // Initalise the strSQL variable with an SQL statement to    query the database get the details for the email
                    strSQL = ("SELECT DISTINCT "
                                + (Variables.Forum.strDbTable + ("EmailNotify.UsuarioID, " + ("Usuarios.usuario,\t" + "Usuarios.email "))));
                    strSQL = (strSQL + ("FROM\t" + ("Usuarios INNER\tJOIN "
                                + (Variables.Forum.strDbTable + ("EmailNotify ON " + ("Usuarios.UsuarioID = "
                                + (Variables.Forum.strDbTable + "EmailNotify.UsuarioID ")))))));
                    if ((FSDatabase.Utils.BDType == FSDatabase.Utils.TypeBd.SQLServer))
                    {
                        strSQL = (strSQL + ("WHERE ("
                                    + (Variables.Forum.strDbTable + ("EmailNotify.Forum_ID="
                                    + (Variables.Forum.intForumID + (" OR\t"
                                    + (Variables.Forum.strDbTable + ("EmailNotify.Topic_ID="
                                    + (lngTopicID + (") AND\t" + ("Usuarios.email Is Not Null AND " + "Usuarios.Active=1;")))))))))));
                    }
                    else
                    {
                        strSQL = (strSQL + ("WHERE ("
                                    + (Variables.Forum.strDbTable + ("EmailNotify.Forum_ID="
                                    + (Variables.Forum.intForumID + (" OR\t"
                                    + (Variables.Forum.strDbTable + ("EmailNotify.Topic_ID="
                                    + (lngTopicID + (") AND\t" + ("Usuarios.email Is Not Null AND " + "Usuarios.Active=True;")))))))))));
                    }

                    // Query the database
                    rsCommon = db.Execute(strSQL);
                    // If a record is    returned by the    recordset then read in the details and send the    e-mail
                    foreach (DataRow row in rsCommon.Rows)
                    {
                        // Read in the details from the recordset    for the    e-mail
                        strusuario = row["usuario"].ToString();
                        lngEmailUserID = long.Parse(row["UsuarioID"].ToString());
                        strUserEmail = row["email"].ToString();
                        // If the    user wants to be e-mailed and the user has enetered their e-mail and they are not the original topic writter then send an e-mail
                        if (((lngEmailUserID != FSPortal.Variables.User.UsuarioId)
                                    && (strUserEmail != "")))
                        {
                            // Initailise the    e-mail body variable with the body of the e-mail
                            strEmailMessage = (Variables.Forum.strTxtHi + (" "
                                        + (FuncionesFilter.decodeString(strusuario) + ",")));
                            strEmailMessage = (strEmailMessage + ("<br /><br />"
                                        + (Variables.Forum.strTxtEmailAMeesageHasBeenPosted + (" "
                                        + (Variables.Forum.strMainForumName + (" " + Variables.Forum.strTxtThatYouAskedKeepAnEyeOn))))));
                            strEmailMessage = (strEmailMessage + ("<br /><br />"
                                        + (Variables.Forum.strTxtEmailClickOnLinkBelowToView + " : -")));
                            strEmailMessage = (strEmailMessage + ("<br /><a href=\""
                                        + (FSPortal.Variables.App.webHttp + ("forum_posts.aspx?TID="
                                        + (lngTopicID + ("&TPN="
                                        + (intReturnPageNum + ("\">"
                                        + (FSPortal.Variables.App.webHttp + ("/forum_posts.aspx?TID="
                                        + (lngTopicID + ("&TPN="
                                        + (intReturnPageNum + "</a>")))))))))))));
                            strEmailMessage = (strEmailMessage + ("<br /><br />"
                                        + (Variables.Forum.strTxtClickTheLinkBelowToUnsubscribe + " :\t-")));
                            strEmailMessage = (strEmailMessage + ("<br /><a href=\""
                                        + (FSPortal.Variables.App.webHttp + ("/email_notify.aspx?TID="
                                        + (lngTopicID + ("&FID="
                                        + (Variables.Forum.intForumID + ("&m=Unsubscribe\">"
                                        + (FSPortal.Variables.App.webHttp + ("/email_notify.aspx?TID="
                                        + (lngTopicID + ("&FID="
                                        + (Variables.Forum.intForumID + "&m=Unsubscribe</a>")))))))))))));
                            if ((Variables.Forum.blnSendPost == true))
                            {
                                strEmailMessage = (strEmailMessage + ("<br /><br /><hr />" + strPostMessage));
                            }

                            // Call the function to send the e-mail
                            new SendMail().SendMailAsync(FuncionesFilter.decodeString(strUserEmail), "", "", strSubject, strEmailMessage, Variables.Forum.strMainForumName, FuncionesFilter.decodeString(strUserEmail), "");
                        }

                    }

                }

                // ******************************************
                // ***  Update The Number    of Forum Posts    ***
                // ******************************************
                // Update    the number of topics and posts in the database
                common.updateTopicPostCount(Variables.Forum.intForumID);
            }

            // ******************************************
            // ***         Update Email Notify    ***
            // ******************************************
            // Delete    or Save    email notification for the user, if email notify is enabled
            if ((((strMode == "new")
                        || ((strMode == "edit")
                        || (strMode == "reply")))
                        && (Variables.Forum.blnEmail == true)))
            {
                // Initalise the SQL string with a query to get the email    notify details
                if ((FSDatabase.Utils.BDType == FSDatabase.Utils.TypeBd.SQLServer))
                {
                    strSQL = ("Execute "
                                + (Variables.Forum.strDbProc + ("TopicEmailNotify\t@lngUsuariosID = "
                                + (FSPortal.Variables.User.UsuarioId + (", @lngTopicID= " + lngTopicID)))));
                }
                else
                {
                    strSQL = ("SELECT "
                                + (Variables.Forum.strDbTable + "EmailNotify.*  "));
                    strSQL = (strSQL + ("FROM\t"
                                + (Variables.Forum.strDbTable + "EmailNotify ")));
                    strSQL = (strSQL + ("WHERE "
                                + (Variables.Forum.strDbTable + ("EmailNotify.UsuarioID="
                                + (FSPortal.Variables.User.UsuarioId + (" AND\t"
                                + (Variables.Forum.strDbTable + ("EmailNotify.Topic_ID="
                                + (lngTopicID + ";")))))))));
                }

                rsCommon = db.Execute(strSQL);
                // With...
                // If the    user no-longer wants email notification    for this topic then remove the entry form the db
                if (((blnEmailNotify == false)
                            && !(rsCommon.Rows.Count > 0)))
                {
                    // Delete    the db entry
                    // ''''.Delete()
                    // Else if this is a new post and    the user wants to be notified add the new entry    to the database
                }
                else if ((((((strMode == "new")
                            || (strMode == "reply"))
                            || ((strMode == "edit")
                            && (rsCommon.Rows.Count > 0)))
                            && (blnEmailNotify == true))
                            && !(rsCommon.Rows.Count > 0)))
                {
                    // Add new dt.AddNew
                    // Create    new entry
                    // '''.Fields("UsuarioID") = FSPortal.Variables.User.UsuarioId
                    // '''.Fields("Topic_ID") = lngTopicID
                    // Upade db with new dt.Update
                }

            }

            // ******************************************
            // ***     Clean up and leave town    ***
            // ******************************************
            // Return    to the page showing the    threads
            Response.Redirect(("forum_posts.aspx?TID="
                            + (lngTopicID + ("&PN=1&TPN=" + intReturnPageNum))));
            return sb.ToString();
        }

    }
}