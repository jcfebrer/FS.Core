

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
'****************************************************************************************
'**  Copyright Notice
'**
'**  Web Wiz Guide ASP Discussion Forum
'**
'**  Copyright 2001-2004 Bruce Corkhill All Rights Reserved.
'**
'**  This program is free software; you can modify (at your own risk) any part of it
'**  under the terms of the License that accompanies this software and use it both
'**  privately and commercially.
'**
'**  All copyright notices must remain in tacked in the scripts and the
'**  outputted HTML.
'**
'**  You may use parts of this program in your own private work, but you may NOT
'**  redistribute, repackage, or sell the whole or any part of this program even
'**  if it is modified or reverse engineered in whole or in part without express
'**  permission from the Usuarios.
'**
'**  You may not pass the whole or any part of this application off as your own work.
'**
'**  All links to Web Wiz Guide and powered by logo's must remain unchanged and in place
'**  and must remain visible when the pages are viewed unless permission is first granted
'**  by the copyright holder.
'**
'**  This program is distributed in the hope that it will be useful,
'**  but WITHOUT ANY WARRANTY; without even the implied warranty of
'**  MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE OR ANY OTHER
'**  WARRANTIES WHETHER EXPRESSED OR IMPLIED.
'**
'**  You should have received a copy of the License along with this program;
'**  if not, write to:- Web Wiz Guide, PO Box 4982, Bournemouth, BH8 8XP, United Kingdom.
'**
'**
'**  No official support is available for this program but you may post support questions at: -
'**  http://www.webwizguide.info/forum
'**
'**  Support questions are NOT answered by e-mail ever!
'**
'**  For correspondence or non support questions contact: -
'**  info@webwizguide.info
'**
'**  or at: -
'**
'**  Web Wiz Guide, PO Box 4982, Bournemouth, BH8 8XP, United Kingdom
'**
'****************************************************************************************

'Set the response buffer to true as we maybe redirecting
Response.Buffer = True


'Declare variables
Dim intForumID			'Holds the forum ID number
Dim lngTopicID			'Holds the topic number
Dim lngPollID			'Holds the poll ID
Dim lngPollVoteChoice		'Holds the users poll choice they are voting for
Dim blnForumLocked		'Make sure the forum hasn't been locked
Dim lngTotalChoiceVote		'Holds the number of votes the poll choice has received
Dim blnMultipleVotes		'set to true if multiple votes are allowed
Dim lngLastVoteUserID		'Holds the IP address of the voter
Dim blnAlreadyVoted		'Set to true if the user has already voted
Dim intResponseNum		'Holds the response number if there is one


'If the user is user is using a banned IP redirect to an error page
If bannedIP() Then
	'Clean up
	Set rsCommon = Nothing
	adoCon.Close
	Set adoCon = Nothing
	
	'Redirect
	Response.Redirect("insufficient_permission.aspx?M=IP")

End If



'Check the user has come to the file from a poll vote page if not send them to teh forum homepage
If Request.Form("PID") = "" OR Request.Form("TID") = "" Then Response.Redirect("default.aspx")


'Initlise variables
blnForumLocked = True
blnAlreadyVoted = False


'Read in the details form the poll form
portal.variablesForum.intForumID = CInt(Request.Form("FID"))
lngTopicID = CLng(Request.Form("TID"))
lngPollID = CLng(Request.Form("PID"))
lngPollVoteChoice = CLng(Request.Form("voteChoice"))



'Check the user is allowed to vote in this forum

'Read in the forum name and forum permssions from the database
'Initalise the strSQL variable with an SQL statement to query the database
If portal.variablesForum.strDatabaseType = "SQLServer" Then
	strSQL = "EXECUTE " & portal.variablesForum.strDbProc & "ForumsAllWhereForumIs @portal.variablesForum.intForumID = " & portal.variablesForum.intForumID
Else
	strSQL = "SELECT " & portal.variablesForum.strDbTable & "Forum.* FROM " & portal.variablesForum.strDbTable & "Forum WHERE " & portal.variablesForum.strDbTable & "Forum.Forum_ID = " & portal.variablesForum.intForumID & ";"
End If

'Query the database
rsCommon=db.execute(strSQL)


'If there is a record returned by the recordset then check to see if you need a clave to enter it
If NOT rsCommon.EOF Then

	'Read in wether the forum is locked or not
	blnForumLocked = CBool(rsCommon("Locked"))

	'Check the user is welcome in this forum
	Call forumPermisisons(portal.variablesForum.intForumID, portal.variablesForum.intGroupID, 0, 0, 0, 0, 0, 0, 0, CInt(rsCommon("Vote")), 0, 0)
End If

'Close the recordset
rsCommon.Close



'If the forum isn't locked and the user has the right to vote then let them vote
If blnForumLocked = False AND portal.variablesForum.blnVote = True AND lngPollVoteChoice <> "" AND lngPollVoteChoice > 0 Then


	'First check to see if multiple votes are allowed and if the user has voted before

	'Initlise the SQL query
	strSQL = "SELECT " & portal.variablesForum.strDbTable & "Poll.Multiple_votes, " & portal.variablesForum.strDbTable & "Poll.UsuarioID FROM " & portal.variablesForum.strDbTable & "Poll WHERE " & portal.variablesForum.strDbTable & "Poll.Poll_ID = " & lngPollID & ";"

	'Set the cursor type property of the record set to Dynamic so we can navigate through the record set
	rsCommon.CursorType = 2

	'Set the Lock Type for the records so that the record set is only locked when it is updated
	rsCommon.LockType = 3

	'Query the database
	rsCommon=db.execute(strSQL)

	'If a record is returned add 1 to it
	If NOT rsCommon.EOF Then

		'Read in if multiple votes are allowed
		blnMultipleVotes = CBool(rsCommon("Multiple_votes"))


		'If multiple votes are not allowed check the last ID of the last voter
		If blnMultipleVotes = False Then

			'Read the last voters IP
			lngLastVoteUserID = rsCommon("UsuarioID")

			'Check to see if the last voted ID matches the ID of the current user
			If lngLastVoteUserID = portal.variablesForum.lngLoggedInUserID AND portal.variablesForum.lngLoggedInUserID <> 2 Then blnAlreadyVoted = True

			'Check the user has not already voted by reading in a cookie from there system
			'Read in the Poll ID number of the last poll the user has voted in
			If CInt(func.ValorCookie(Request.Cookies(portal.variables.strCookieName),"PID" & lngPollID)) = lngPollID Then blnAlreadyVoted = True


			'If the user hasn't already voted then save their ID and move on to save their vote
			If blnAlreadyVoted = False Then

				'Update recordset
				rsCommon.Fields("UsuarioID") = portal.variablesForum.lngLoggedInUserID

				'Update the database with the voters ID
				rsCommon.Update

				'Save to a cookie as well
				'Write a cookie with the Poll ID number so the user cannot keep voting on this poll
				'Write the cookie with the name Poll containing the Poll ID
				Response.Cookies(portal.variables.strCookieName)("PID" & lngPollID) = lngPollID

				'Set the expiry date for 1 year
				Response.Cookies(portal.variables.strCookieName).Expires = Now() + 360
			End If
		End If
	End If

	'Close the recordset
	rsCommon.Close


	'If the already voted boolean is not set then save the vote
	If blnAlreadyVoted = False Then


		'Save the voters choice

		'Initlise the SQL query
		strSQL = "SELECT " & portal.variablesForum.strDbTable & "PollChoice.Votes FROM " & portal.variablesForum.strDbTable & "PollChoice WHERE " & portal.variablesForum.strDbTable & "PollChoice.Choice_ID = " & lngPollVoteChoice & ";"

		'Set the cursor type property of the record set to Dynamic so we can navigate through the record set
		rsCommon.CursorType = 2

		'Set the Lock Type for the records so that the record set is only locked when it is updated
		rsCommon.LockType = 3

		'Query the database
		rsCommon=db.execute(strSQL)

		'If a record is returned add 1 to it
		If NOT rsCommon.EOF Then

			'Read in the Poll Chioce Votes from rs
			lngTotalChoiceVote = CLng(rsCommon("Votes"))

			'Increment by 1
			lngTotalChoiceVote = lngTotalChoiceVote + 1

			'Update recordset
			rsCommon.Fields("Votes") = lngTotalChoiceVote

			'Update the database with the new poll choices
			rsCommon.Update
			
			'Set the error number to 1 for no error
			intResponseNum = 1
		End If

		'Close the recordset
		rsCommon.Close
	End If
End If

'Celan up
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing

'Set up the return error number
If lngPollVoteChoice = 0 Then intResponseNum = 2


'Go back to the forum posts page
Response.Redirect("forum_posts.aspx?TID=" & lngTopicID & "&amp;pN=" & Request.Form("PN") & "&amp;tPN=" & Request.Form("TPN") & "&amp;rN=" & intResponseNum)
%>