// <fileheader>
// <copyright file="poll_display_inc.ascx.cs" company="Febrer Software">
//     Fecha: 30/11/2007
//     Path: forum\includes\poll_display_inc.ascx.cs
//     Copyright (c) 2003-2007 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>
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
    namespace Includes
    {
        /// <summary>
        /// Encuestas
        /// </summary>
        public class poll_display_inc
        {
            public string Render()
            {
                StringBuilder sb = new StringBuilder();
                // Declare variables
                string strPollQuestion = "";
                // Holds the poll question
                int intPollChoiceNumber = 0;
                // Holds the poll choice number
                string strPollChoice = "";
                // Holds the poll choice
                long lngPollChoiceVotes = 0;
                // Holds the choice number of votes
                long lngTotalPollVotes = 0;
                // Holds the total number of votes
                double dblPollVotePercentage = 0;
                // Holds the vote percentage for the vote choice
                long lngLastVoteUserID;
                // Holds the IP address of the voter
                bool blnAlreadyVoted;
                // Set to true if the user has already voted
                bool blnMultipleVotes;
                // set to true if multiple votes are allowed
                string strSQL;
                DataTable rsCommon;
                bool blnPollNoReply;
                // Initlise variables
                blnAlreadyVoted = false;
                FSDatabase.BdUtils db = new FSDatabase.BdUtils("FSForum");
                // Get the poll from the database
                // Initalise the strSQL variable with an SQL statement to query the database get the thread details
                if ((FSDatabase.Utils.BDType == FSDatabase.Utils.TypeBd.SQLServer))
                {
                    strSQL = ("Execute "
                                + (Variables.Forum.strDbProc + ("PollDetails @lngPollID = " + Variables.Forum.lngPollID)));
                }
                else
                {
                    strSQL = ("SELECT  "
                                + (Variables.Forum.strDbTable + ("Poll.*, "
                                + (Variables.Forum.strDbTable + "PollChoice.* "))));
                    strSQL = (strSQL + ("FROM "
                                + (Variables.Forum.strDbTable + ("Poll INNER JOIN "
                                + (Variables.Forum.strDbTable + ("PollChoice ON "
                                + (Variables.Forum.strDbTable + ("Poll.Poll_ID = "
                                + (Variables.Forum.strDbTable + "PollChoice.Poll_ID ")))))))));
                    strSQL = (strSQL + ("WHERE ((("
                                + (Variables.Forum.strDbTable + ("Poll.Poll_ID)="
                                + (Variables.Forum.lngPollID + "));")))));
                }

                // Query the database
                rsCommon = db.Execute(strSQL);
                // If there is a poll then display it
                if ((rsCommon.Rows.Count > 0))
                {
                    // Read in the poll question
                    strPollQuestion = rsCommon.Rows[0]["Poll_question"].ToString();
                    // Read the last voters ID
                    lngLastVoteUserID = long.Parse(rsCommon.Rows[0]["UsuarioID"].ToString());
                    blnMultipleVotes = bool.Parse(rsCommon.Rows[0]["Multiple_votes"].ToString());
                    blnPollNoReply = bool.Parse(rsCommon.Rows[0]["Reply"].ToString());
                    if ((blnMultipleVotes == false))
                    {
                        // Check to see if the last voted ID matches the ID of the current user unless the user is using the guest account
                        if (((lngLastVoteUserID == FSPortal.Variables.User.UsuarioId)
                                    && (FSPortal.Variables.User.UsuarioId != 2)))
                        {
                            blnAlreadyVoted = true;
                        }

                        if ((NumberUtils.NumberDouble(Web.Cookie(FSPortal.Variables.App.Page.Request.Cookies[FSPortal.Variables.App.strCookieName], ("PID" + Variables.Forum.lngPollID))) == Variables.Forum.lngPollID))
                        {
                            blnAlreadyVoted = true;
                        }

                        // Loop through and get the total number of votes
                        foreach (DataRow row in rsCommon.Rows)
                        {
                            // Get the total number of votes
                            lngTotalPollVotes += long.Parse(row["Votes"].ToString());
                        }

                    }

                }
                sb.AppendLine("<!-- Start Poll -->");
                sb.AppendLine(@"<table width=""");
                sb.AppendLine(Variables.Forum.strTablePollVariableWidth);
                sb.AppendLine(@""" border=""0"" cellspacing=""0"" cellpadding=""1"" bgcolor=""");
                sb.AppendLine(Variables.Forum.strTablePollBorderColour);
                sb.AppendLine(@""" align=""center"">");
                sb.AppendLine("<tr>");
                sb.AppendLine(@"<form name=""frmPoll"" method=""post"" action=""poll_cast_vote.aspx"">");
                sb.AppendLine(@"<td> <table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" bgcolor=""");
                sb.AppendLine(Variables.Forum.strTablePollBgColour);
                sb.AppendLine(@""">");
                sb.AppendLine("<tr>");
                sb.AppendLine(@"<td bgcolor=""");
                sb.AppendLine(Variables.Forum.strTablePollBgColour);
                sb.AppendLine(@"""> <table width=""100%"" border=""0"" cellspacing=""1"" cellpadding=""4"" bgcolor=""");
                sb.AppendLine(Variables.Forum.strTablePollBgColour);
                sb.AppendLine(@""">");
                sb.AppendLine("<tr>");
                sb.AppendLine(@"<td colspan=""4"" background=""");
                sb.AppendLine(Variables.Forum.strTablePollTitleBgImage);
                sb.AppendLine(@""" bgcolor=""");
                sb.AppendLine(Variables.Forum.strTablePollTitleColour);
                sb.AppendLine(@""" class=""tHeading"">");
                sb.AppendLine(Variables.Forum.strTxtPollQuestion);
                sb.AppendLine(":");
                sb.AppendLine(strPollQuestion);
                sb.AppendLine("</td>");
                sb.AppendLine("</tr>");
                sb.AppendLine("<tr>");
                // Display the vote choice slection column if the user CAN vote in this poll
                if (((Variables.Forum.blnVote == true)
                            && ((Variables.Forum.blnForumLocked == false)
                            && ((Variables.Forum.blnTopicLocked == false)
                            && ((Variables.Forum.blnActiveMember == true)
                            && (blnAlreadyVoted == false))))))
                {

                }
                sb.AppendLine(@"<td width=""3%"" align=""center"" background=""");
                sb.AppendLine(Variables.Forum.strTablePollColumnHeadingBgImage);
                sb.AppendLine(@""" bgcolor=""");
                sb.AppendLine(Variables.Forum.strTablePollColumnHeadingColour);
                sb.AppendLine(@""" class=""tiHeading"">");
                sb.AppendLine(Variables.Forum.strTxtVote);
                sb.AppendLine("</td>");
                sb.AppendLine(@"<td width=""47%"" background=""");
                sb.AppendLine(Variables.Forum.strTablePollColumnHeadingBgImage);
                sb.AppendLine(@""" bgcolor=""");
                sb.AppendLine(Variables.Forum.strTablePollColumnHeadingColour);
                sb.AppendLine(@""" class=""tiHeading"" nowrap=""nowrap"">");
                sb.AppendLine(Variables.Forum.strTxtPollChoice);
                sb.AppendLine("</td>");
                sb.AppendLine(@"<td width=""6%"" align=""center"" nowrap=""nowrap"" background=""");
                sb.AppendLine(Variables.Forum.strTablePollColumnHeadingBgImage);
                sb.AppendLine(@""" bgcolor=""");
                sb.AppendLine(Variables.Forum.strTablePollColumnHeadingColour);
                sb.AppendLine(@""" class=""tiHeading"">");
                sb.AppendLine(Variables.Forum.strTxtVotes);
                sb.AppendLine("</td>");
                sb.AppendLine(@"<td width=""47%"" background=""");
                sb.AppendLine(Variables.Forum.strTablePollColumnHeadingBgImage);
                sb.AppendLine(@""" bgcolor=""");
                sb.AppendLine(Variables.Forum.strTablePollColumnHeadingColour);
                sb.AppendLine(@""" class=""tiHeading"">");
                sb.AppendLine(Variables.Forum.strTxtPollStatistics);
                sb.AppendLine("</td>");
                sb.AppendLine("</tr>");
                // Loop through the Poll Choices
                int intRecordLoopCounter = 0;
                foreach (DataRow row in rsCommon.Rows)
                {
                    // Read in the poll details
                    intPollChoiceNumber = int.Parse(row["Choice_ID"].ToString());
                    strPollChoice = row["Choice"].ToString();
                    lngPollChoiceVotes = long.Parse(row["Votes"].ToString());
                    if ((lngTotalPollVotes == 0))
                    {
                        dblPollVotePercentage = double.Parse(String.Format("{0:P2}", 0));
                    }
                    else
                    {
                        dblPollVotePercentage = lngPollChoiceVotes / lngTotalPollVotes;
                    }

                    // Calculate the row colour
                    intRecordLoopCounter++;
                }
                sb.AppendLine("<tr>");
                //Display the vote radio buttons if the user CAN vote in this poll
                if (Variables.Forum.blnVote == true && Variables.Forum.blnForumLocked == false && Variables.Forum.blnTopicLocked == false && Variables.Forum.blnActiveMember == true && blnAlreadyVoted == false)
                {
                    sb.AppendLine(@"<td bgcolor=""");
                    if (((intRecordLoopCounter % 2)
                    == 0))
                    {
                        sb.AppendLine(Variables.Forum.strTablePollEvenRowColour);
                    }
                    else
                    {
                        sb.AppendLine(Variables.Forum.strTablePollOddRowColour);
                    }
                    sb.AppendLine(@""" background=""");
                    sb.AppendLine(Variables.Forum.strTablePollBgImage);
                    sb.AppendLine(@""" align=""center""><input type=""radio"" name=""voteChoice"" value=""");
                    sb.AppendLine(intPollChoiceNumber.ToString());
                    sb.AppendLine(@""" id=""P");
                    sb.AppendLine(intPollChoiceNumber.ToString());
                    sb.AppendLine(@"""></td>");
                }
                sb.AppendLine(@"<td bgcolor=""");
                if (((intRecordLoopCounter % 2)
                                    == 0))
                {
                    sb.AppendLine(Variables.Forum.strTablePollEvenRowColour);
                }
                else
                {
                    sb.AppendLine(Variables.Forum.strTablePollOddRowColour);
                }
                sb.AppendLine(@""" background=""");
                sb.AppendLine(Variables.Forum.strTablePollBgImage);
                sb.AppendLine(@""" class=""text""><label for=""P");
                sb.AppendLine(intPollChoiceNumber.ToString());
                sb.AppendLine(@""">");
                sb.AppendLine(strPollChoice);
                sb.AppendLine("</label></td>");
                sb.AppendLine(@"<td align=""center"" background=""");
                sb.AppendLine(Variables.Forum.strTablePollBgImage);
                sb.AppendLine(@""" bgcolor=""");
                if (((intRecordLoopCounter % 2)
                                    == 0))
                {
                    sb.AppendLine(Variables.Forum.strTablePollEvenRowColour);
                }
                else
                {
                    sb.AppendLine(Variables.Forum.strTablePollOddRowColour);
                }
                sb.AppendLine(@""" class=""text"">");
                sb.AppendLine(lngPollChoiceVotes.ToString());
                sb.AppendLine("</td>");
                sb.AppendLine(@"<td bgcolor=""");
                if (((intRecordLoopCounter % 2)
                                    == 0))
                {
                    sb.AppendLine(Variables.Forum.strTablePollEvenRowColour);
                }
                else
                {
                    sb.AppendLine(Variables.Forum.strTablePollOddRowColour);
                }
                sb.AppendLine(@""" background=""");
                sb.AppendLine(Variables.Forum.strTablePollBgImage);
                sb.AppendLine(@""" class=""smText"" nowrap=""nowrap""><img src=""");
                sb.AppendLine(Variables.Forum.strImagePath);
                sb.AppendLine(@"bar_graph_image.gif"" width=""");
                sb.AppendLine((int.Parse(String.Format("{0:P2}", dblPollVotePercentage).Replace("%", "")) * 2).ToString());
                sb.AppendLine(@""" height=""11"" align=""middle""> [");
                sb.AppendLine(String.Format("{0:P2}", dblPollVotePercentage));
                sb.AppendLine(@"<tr align=""center"">");
                sb.AppendLine(@"<td colspan=""4"" background=""");
                sb.AppendLine(Variables.Forum.strTableBgImage);
                sb.AppendLine(@""" bgcolor=""");
                sb.AppendLine(Variables.Forum.strTablePollBottomRowColour);
                sb.AppendLine(@""" class=""text"">");
                // Display either text msg if the user can NOT vote or a button if they can
                // If the forum is locked display a locked forum meesage
                if (((Variables.Forum.blnForumLocked == true)
                            || (Variables.Forum.blnTopicLocked == true)))
                {
                    sb.AppendLine(Variables.Forum.strTxtThisTopicIsClosedNoNewVotesAccepted);
                    // Else the user can not vote or they are not an active member of the forum
                }
                else if (((Variables.Forum.blnActiveMember == false)
                            || (Variables.Forum.blnVote == false)))
                {
                    sb.AppendLine(Variables.Forum.strsTxYouCanNotNotVoteInThisPoll);
                    // Else the user has already voted in this poll and multiple votes are not permitted
                }
                else if ((blnAlreadyVoted == true))
                {
                    sb.AppendLine(Variables.Forum.strTxtYouHaveAlreadyVotedInThisPoll);
                    // Else display vote button
                }
                else
                {

                }
                sb.AppendLine(@"<input type=""hidden"" name=""PID"" value=""");
                sb.AppendLine(Variables.Forum.lngPollID.ToString());
                sb.AppendLine(@""">");
                sb.AppendLine(@"<input type=""hidden"" name=""TID"" value=""");
                sb.AppendLine(Variables.Forum.lngTopicID.ToString());
                sb.AppendLine(@""">");
                sb.AppendLine(@"<input type=""hidden"" name=""FID"" value=""");
                sb.AppendLine(Variables.Forum.intForumID.ToString());
                sb.AppendLine(@""">");
                sb.AppendLine(@"<input type=""hidden"" name=""PN"" value=""");
                sb.AppendLine(Variables.Forum.intTopicPageNumber.ToString());
                sb.AppendLine(@""">");
                sb.AppendLine(@"<input type=""hidden"" name=""TPN"" value=""");
                sb.AppendLine(Variables.Forum.intRecordPositionPageNum.ToString());
                sb.AppendLine(@""">");
                sb.AppendLine(@"<input type='submit' name=""Submit"" value=""");
                sb.AppendLine(Variables.Forum.strTxtCastMyVote);
                sb.AppendLine(@""">");
                sb.AppendLine("</td>");
                sb.AppendLine("</tr>");
                sb.AppendLine("</table></td>");
                sb.AppendLine("</tr>");
                sb.AppendLine("</table></td>");
                sb.AppendLine("</form>");
                sb.AppendLine("</tr>");
                sb.AppendLine("</table>");
                sb.AppendLine("<br />");
                sb.AppendLine("<!-- End Poll -->");
                return sb.ToString();
            }

        }
    }
}