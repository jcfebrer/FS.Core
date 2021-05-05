

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_DeletePoll" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<!--#include file="language_files/admin_language_file_inc.aspx" -->
<%
'Set the response buffer to true as we maybe redirecting
Response.Buffer = True


'Dimension variables
Dim lngTopicID 		'Holds the topic ID number to return to
Dim intForumID		'Holds the forum ID number
Dim lngPollID		'Holds the poll ID
Dim strMode		'Holds the page mode
Dim intPollLoopCounter	'Holds the poll loop counter
Dim strPollQuestion	'Holds the poll question
Dim blnMultipleVotes	'Set to true if multiple votes are allowed
Dim blnPollNoReply	'Set to true if this is a no reply poll
Dim strPollChoice	'Holds the poll choice


'If the user is user is using a banned IP redirect to an error page
If bannedIP() Then
	'Clean up
	Set rsCommon = Nothing
	adoCon.Close
	Set adoCon = Nothing

	'Redirect
	Response.Redirect("insufficient_permission.aspx?M=IP")
End If



'Read in the message ID number to be deleted
lngTopicID = CLng(Request("TID"))


'If the person is not an admin or a moderator then send them away
If lngTopicID = "" Then
	'Clean up
	Set rsCommon = Nothing
	adoCon.Close
	Set adoCon = Nothing

	'Redirect
	Response.Redirect("default.aspx")
End If




'Initliase the SQL query to get the topic details from the database
strSQL = "SELECT " & portal.variablesForum.strDbTable & "Topic.Forum_ID, " & portal.variablesForum.strDbTable & "Topic.Poll_ID "
strSQL = strSQL & "FROM " & portal.variablesForum.strDbTable & "Topic "
strSQL = strSQL & "WHERE " & portal.variablesForum.strDbTable & "Topic.Topic_ID = " & lngTopicID & ";"

'Set the cursor	type property of the record set	to Dynamic so we can navigate through the record set
rsCommon.CursorType = 2

'Set the Lock Type for the records so that the record set is only locked when it is updated
rsCommon.LockType = 3

'Query the database
rsCommon=db.execute(strSQL)

'If there is a record returened read in the forum ID
If NOT rsCommon.EOF Then
	portal.variablesForum.intForumID = CInt(rsCommon("Forum_ID"))
	lngPollID = CLng(rsCommon("Poll_ID"))
End If





'Call the moderator function and see if the user is a moderator
If portal.variablesForum.blnAdmin = False Then portal.variablesForum.blnModerator = isModerator(portal.variablesForum.intForumID, portal.variablesForum.intGroupID)



'Check to make sure the user is deleting the post is a moderator or the forum adminstrator
If (portal.variablesForum.blnAdmin = True OR portal.variablesForum.blnModerator = True) AND lngPollID > 0 Then
	
	'Update the poll id with 0
	rsCommon.Fields("Poll_ID") = 0
	
	'Update the recordset
	rsCommon.Update
	
	'Delete the Poll choices
	strSQL = "DELETE FROM " & portal.variablesForum.strDbTable & "PollChoice WHERE " & portal.variablesForum.strDbTable & "PollChoice.Poll_ID="  & lngPollID & ";"

	'Write to database
	db.execute(strSQL)

	'Delete the Poll
	strSQL = "DELETE FROM " & portal.variablesForum.strDbTable & "Poll WHERE " & portal.variablesForum.strDbTable & "Poll.Poll_ID="  & lngPollID & ";"

	'Write to database
	db.execute(strSQL)
End If


'Clean up
rsCommon.Close
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing
%>
<html>
<head>
<script language="javascript">
	window.opener.location.href = "forum_posts.aspx?TID=<% = lngTopicID %>"
	window.close();
</script>
</head>
</html>