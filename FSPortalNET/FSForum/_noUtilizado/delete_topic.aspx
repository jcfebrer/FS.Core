

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
'Set the response buffer to true as we maybe redirecting
Response.Buffer = True

'Dimension variables
Dim rsNumOfPosts		'Holds the database recordset for the number of posts the user has made
Dim rsForum			'Holds the forum for order
Dim strMode			'Holds the mode of the page
Dim lngTopicID 			'Holds the topic ID number to return to
Dim intForumID			'Holds the forum ID number
Dim lngPollID			'Holds the poll ID number if there is one
Dim lngDelMsgUsuariosID		'Holds the deleted message Usuarios ID
Dim lngNumOfPosts		'Holds the number of posts the user has made



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
lngTopicID = CLng(Request.Form("TID"))




'Initliase the SQL query to get the topic and forumID from the database
strSQL = "SELECT " & portal.variablesForum.strDbTable & "Topic.Forum_ID "
strSQL = strSQL & "FROM " & portal.variablesForum.strDbTable & "Topic "
strSQL = strSQL & "WHERE " & portal.variablesForum.strDbTable & "Topic.Topic_ID = " & lngTopicID & ";"

'Query the database
rsCommon=db.execute(strSQL)

'If there is a record returened read in the forum ID
If NOT rsCommon.EOF Then
	portal.variablesForum.intForumID = CInt(rsCommon("Forum_ID"))
End If

'Clean up
rsCommon.Close



'Call the moderator function and see if the user is a moderator
If portal.variablesForum.blnAdmin = False Then portal.variablesForum.blnModerator = isModerator(portal.variablesForum.intForumID, portal.variablesForum.intGroupID)



'Check to make sure the user is deleting the post is a moderator or the forum adminstrator
If portal.variablesForum.blnAdmin = True OR portal.variablesForum.blnModerator = True Then

	'See if there is a poll, if there is get the poll ID and delete

	'Initalise the strSQL variable with an SQL statement to get the topic from the database
	strSQL = "SELECT " & portal.variablesForum.strDbTable & "Topic.Poll_ID "
	strSQL = strSQL & "FROM " & portal.variablesForum.strDbTable & "Topic "
	strSQL = strSQL & "WHERE " & portal.variablesForum.strDbTable & "Topic.Topic_ID="  & lngTopicID  & ";"

	'Query the database
	rsCommon=db.execute(strSQL)

	'Get the Poll ID
	If NOT rsCommon.EOF Then lngPollID = CLng(rsCommon("Poll_ID"))

	'Close the recordset
	rsCommon.Close


	'Get the Posts to be deleted from the database
	strSQL = "SELECT " & portal.variablesForum.strDbTable & "Thread.* "
	strSQL = strSQL & "FROM " & portal.variablesForum.strDbTable & "Thread "
	strSQL = strSQL & "WHERE " & portal.variablesForum.strDbTable & "Thread.Topic_ID= "  & lngTopicID  & ";"

	'Query the database
	rsCommon=db.execute(strSQL)

	'Get the number of posts the user has made and take one away
	Set rsNumOfPosts = Server.CreateObject("ADODB.Recordset")


	'Loop through all the posts for the topic and delete them
	Do While NOT rsCommon.EOF
	
	
		'First we need to delete any entry in the GuestName table incase this was a guest poster posting the message
		strSQL = "DELETE FROM " & portal.variablesForum.strDbTable & "GuestName WHERE " & portal.variablesForum.strDbTable & "GuestName.Thread_ID=" & CLng(rsCommon("Thread_ID")) & ";"
	
		'Excute SQL
		db.execute(strSQL)
		

		'Initalise the strSQL variable with an SQL statement to query the database to get the number of posts the user has made
		strSQL = "SELECT " & "Usuarios.UsuarioID, " & "Usuarios.No_of_posts FROM " & "Usuarios WHERE " & "Usuarios.UsuarioID = " & CLng(rsCommon("UsuarioID")) & ";"

		'Query the database
		rsNumOfPosts=db.execute(strSQL)

		'If there is a record returned by the database then read in the no of posts and decrement it by 1
		If NOT rsNumOfPosts.EOF Then

			'Read in the no of posts the user has made and usuario
			lngNumOfPosts = CLng(rsNumOfPosts("No_of_posts"))

			'Decrement by 1 unless the number of posts is already 0
			If NOT lngNumOfPosts = 0 Then

				'decrement the number of posts by 1
				lngNumOfPosts = lngNumOfPosts - 1

				'Initalise the SQL string with an SQL update command to update the number of posts the user has made
				strSQL = "UPDATE " & "Usuarios SET "
				strSQL = strSQL & "" & "Usuarios.No_of_posts = " & lngNumOfPosts
				strSQL = strSQL & " WHERE " & "Usuarios.UsuarioID= " & CLng(rsCommon("UsuarioID")) & ";"

				'Write the updated number of posts to the database
				db.execute(strSQL)
			End If
		End If

		'Close the recordset
		rsNumOfPosts.Close

		'Move to the next record
		rsCommon.MoveNext
	Loop
	

	'Delete the posts in this topic
	strSQL = "DELETE FROM " & portal.variablesForum.strDbTable & "Thread WHERE " & portal.variablesForum.strDbTable & "Thread.Topic_ID="  & lngTopicID & ";"

	'Write to database
	db.execute(strSQL)


	'Delete the Poll in this topic, if there is one
	If lngPollID > 0 Then

		'Delete the Poll choices
		strSQL = "DELETE FROM " & portal.variablesForum.strDbTable & "PollChoice WHERE " & portal.variablesForum.strDbTable & "PollChoice.Poll_ID="  & lngPollID & ";"

		'Write to database
		db.execute(strSQL)

		'Delete the Poll
		strSQL = "DELETE FROM " & portal.variablesForum.strDbTable & "Poll WHERE " & portal.variablesForum.strDbTable & "Poll.Poll_ID="  & lngPollID & ";"

		'Write to database
		db.execute(strSQL)
	End If


	'Delete the topic from the database
	'Initalise the strSQL variable with an SQL statement to get the topic from the database
	strSQL = "DELETE FROM " & portal.variablesForum.strDbTable & "Topic WHERE " & portal.variablesForum.strDbTable & "Topic.Topic_ID="  & lngTopicID & ";"

	'Write the updated date of last post to the database
	db.execute(strSQL)


	'Reset Server Objects
	rsCommon.Close
	Set rsNumOfPosts = Nothing
End If


'Update the number of topics and posts in the database
Call updateTopicPostCount(portal.variablesForum.intForumID)


'Reset main server variables
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing

%>
<html>
<head>
<script language="javascript">
	window.opener.location.href = "forum_topics.aspx?FID=<% = portal.variablesForum.intForumID %>&amp;dL=1"
	window.close();
</script>
</head>
</html>