

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
'Set the response buffer to true as we maybe redirecting
Response.Buffer = True

'Declare variables
Dim lngTopicID		'Holds the topic ID
Dim intForumID		'Holds the forum ID
Dim strSubject		'Holds the new subject
Dim lngOldTopicID	'Holds the old topic ID number
Dim lngPostID		'Holds the post ID
Dim strPostDateTime	'Holds the date of the post


'If the user is user is using a banned IP redirect to an error page
If bannedIP() Then
	'Clean up
	Set rsCommon = Nothing
	adoCon.Close
	Set adoCon = Nothing

	'Redirect
	Response.Redirect("insufficient_permission.aspx?M=IP")
End If



'Read in the post ID
lngPostID = CLng(Request.Form("PID"))


'Query the datbase to get the forum ID for this post
strSQL = "SELECT " & portal.variablesForum.strDbTable & "Topic.Forum_ID, " & portal.variablesForum.strDbTable & "Topic.Topic_ID, " & portal.variablesForum.strDbTable & "Thread.Message_date "
strSQL = strSQL & "FROM " & portal.variablesForum.strDbTable & "Topic, " & portal.variablesForum.strDbTable & "Thread "
strSQL = strSQL & "WHERE " & portal.variablesForum.strDbTable & "Topic.Topic_ID = " & portal.variablesForum.strDbTable & "Thread.Topic_ID AND " & portal.variablesForum.strDbTable & "Thread.Thread_ID = " & lngPostID & ";"

'Query the database
rsCommon=db.execute(strSQL)


'If there is a record returened read in the forum ID
If NOT rsCommon.EOF Then
	portal.variablesForum.intForumID = CInt(rsCommon("Forum_ID"))
	lngOldTopicID = CLng(rsCommon("Topic_ID"))
	strPostDateTime = CDate(rsCommon("Message_date"))
End If

'Clean up
rsCommon.Close


'Call the moderator function and see if the user is a moderator
If portal.variablesForum.blnAdmin = False Then portal.variablesForum.blnModerator = isModerator(portal.variablesForum.intForumID, portal.variablesForum.intGroupID)


'If the person is not a moderator or admin then send them away
If portal.variablesForum.blnAdmin = False AND portal.variablesForum.blnModerator = False Then
	'Reset Server Objects
	Set rsCommon = Nothing
	adoCon.Close
	Set adoCon = Nothing

	'Redirect
	Response.Redirect("default.aspx")


'The person is an admin of modertor so move the post
Else

	'Read in the forum details
	lngTopicID = CLng(Request.Form("topicSelect"))
	portal.variablesForum.intForumID = CInt(Request.Form("toFID"))
	strSubject = Request.Form("subject")




	'If a new subject has been entered then place it into the database
	If strSubject <> "" Then

		'Get rid of scripting tags in the subject
		strSubject = removeAllTags(strSubject)
		strSubject = func.formatInput(strSubject)

		'Initalise the SQL string with a query to get the Topic details
		strSQL = "SELECT TOP 1 " & portal.variablesForum.strDbTable & "Topic.* FROM " & portal.variablesForum.strDbTable & "Topic "
		strSQL = strSQL & "WHERE Forum_ID =" & portal.variablesForum.intForumID & " "
		strSQL = strSQL & "ORDER By " & portal.variablesForum.strDbTable & "Topic.Topic_ID DESC;"

		With rsCommon
			'Set the cursor type property of the record set to Dynamic so we can navigate through the record set
			.CursorType = 2

			'Set the Lock Type for the records so that the record set is only locked when it is updated
			.LockType = 3

			'Open the topic table
			=db.execute(strSQL)

			'Insert the new topic details in the recordset
			.AddNew

			.Fields("Forum_ID") = portal.variablesForum.intForumID
			.Fields("Subject") = strSubject
			.Fields("Start_date") =	strPostDateTime
			.Fields("Last_entry_date") = strPostDateTime
			.Fields("Poll_ID") = 0
			.Fields("Priority") = 0

			'Update the database with the new topic details
			.Update

			'Re-run the Query once the database has been updated
			.Requery

			'Move to the last record in the recordset to get the new topic's ID number
			.MoveLast

			'Read in the new topic's ID number
			lngTopicID = CLng(rsCommon("Topic_ID"))

			'Clean up
			.Close
		End With
	End If



		
	'Move the post to another topic use ADO otherwise access is to slow
	strSQL = "SELECT " & portal.variablesForum.strDbTable & "Thread.Topic_ID FROM " & portal.variablesForum.strDbTable & "Thread WHERE Thread_ID =" & lngPostID & ";"

	With rsCommon
	
		'Set the cursor type property of the record set to Dynamic so we can navigate through the record set
		.CursorType = 2
		
		'Set the Lock Type for the records so that the record set is only locked when it is updated
		.LockType = 3
		
		'Open the thread table
		=db.execute(strSQL)
		
		'If the record is returned update the topic ID
		If NOT .EOF Then
			
			.Fields("Topic_ID") = lngTopicID
		End If
		
		'Update the database
		.Update
		
		'Requery the db for slow old access
		.Requery
		
		'Clean up
		.Close
	
	
	
	
		'Check there are still topics in the old topic
		strSQL = "SELECT TOP 1 " & portal.variablesForum.strDbTable & "Thread.Thread_ID FROM " & portal.variablesForum.strDbTable & "Thread WHERE Topic_ID =" & lngOldTopicID & ";"
	
		'Open the thread table
		=db.execute(strSQL)
		
	
		'See if there is a topic left in the old topic
		If .EOF Then
			'If there are no topics left then delete the old topic
			strSQL = "DELETE FROM " & portal.variablesForum.strDbTable & "Topic WHERE Topic_ID=" & lngOldTopicID & ";"
	
			'Write to database
			db.execute(strSQL)
		End If
	
		'Close the recordset
		.Close
	End With

End If

'Reset main server variables
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