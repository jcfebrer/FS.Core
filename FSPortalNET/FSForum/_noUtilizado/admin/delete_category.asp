

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
'Set the timeout of the page
Server.ScriptTimeout =  1000


'Set the response buffer to true as we maybe redirecting
Response.Buffer = True




'Dimension variables
Dim rsTopic		'Holds the database recordset for the Topic to be delete
Dim intCatID		'Holds the forum ID to be deleted
Dim lngPollID		'Holds the poll ID if there is one to delete
Dim intForumID		'Holds the forum ID to be deleted
Dim rsPost		'Recordset to get the post ID from db to check against guest post names


'Get the category ID to delete
intCatID = CInt(Request.QueryString("CatID"))



'Get all the Forums from the database to be deleted

'Create a recordset object for the Topic in the database
Set rsTopic = Server.CreateObject("ADODB.Recordset")
Set rsPost = Server.CreateObject("ADODB.Recordset")


'Initalise the strSQL variable with an SQL statement to get the topic from the database
strSQL = "SELECT " & portal.variablesForum.strDbTable & "Forum.* FROM " & portal.variablesForum.strDbTable & "Forum WHERE " & portal.variablesForum.strDbTable & "Forum.Cat_ID ="  & intCatID & ";"

'Query the database
rsCommon=db.execute(strSQL)



'Loop through all the forums for the topics and delete them
Do While NOT rsCommon.EOF

	'Read in the forum ID
	portal.variablesForum.intForumID = CInt(rsCommon("Forum_ID"))

	'Get all the Topics from the database to be deleted
	'Initalise the strSQL variable with an SQL statement to get the topic from the database
	strSQL = "SELECT " & portal.variablesForum.strDbTable & "Topic.* FROM " & portal.variablesForum.strDbTable & "Topic WHERE " & portal.variablesForum.strDbTable & "Topic.Forum_ID ="  & portal.variablesForum.intForumID & ";"

	'Query the database
	rsTopic=db.execute(strSQL)

	'Loop through all the threads for the topics and delete them
	Do While NOT rsTopic.EOF
	
	
	
		'First we need to delete any entry in the GuestName table incase this was a guest poster posting the message
		
		'Initalise the strSQL variable with an SQL statement to get thread ID from the database
		strSQL = "SELECT " & portal.variablesForum.strDbTable & "Thread.Thread_ID FROM " & portal.variablesForum.strDbTable & "Thread WHERE " & portal.variablesForum.strDbTable & "Thread.Topic_ID ="  & CLng(rsTopic("Topic_ID")) & ";"
		
		'Query the database
		rsPost=db.execute(strSQL)
		
		'Loop through thread ID's
		Do While NOT rsPost.EOF
		
			'First we need to delete any entry in the GuestName table incase this was a guest poster posting the message
			strSQL = "DELETE FROM " & portal.variablesForum.strDbTable & "GuestName WHERE " & portal.variablesForum.strDbTable & "GuestName.Thread_ID=" & CLng(rsPost("Thread_ID")) & ";"
		
			'Excute SQL
			db.execute(strSQL)
			
			'Movenext rs
			rsPost.MoveNext
		Loop
		
		'Close rs
		rsPost.Close
	
	

		'Delete the posts in this topic
		strSQL = "DELETE FROM " & portal.variablesForum.strDbTable & "Thread WHERE " & portal.variablesForum.strDbTable & "Thread.Topic_ID ="  & CLng(rsTopic("Topic_ID")) & ";"

		'Write to database
		db.execute(strSQL)


		'Delete any polls

		'Get the Poll ID
		lngPollID = CLng(rsTopic("Poll_ID"))

		'If there is a poll delete that as well
		If lngPollID > 0 Then

			'Delete the poll choice
			strSQL = "DELETE FROM " & portal.variablesForum.strDbTable & "PollChoice WHERE " & portal.variablesForum.strDbTable & "PollChoice.Poll_ID =" & lngPollID & ";"

			'Delete the threads
			db.execute(strSQL)

			'Delete the poll choice
			strSQL = "DELETE FROM " & portal.variablesForum.strDbTable & "Poll WHERE " & portal.variablesForum.strDbTable & "Poll.Poll_ID =" & lngPollID & ";"

			'Delete the threads
			db.execute(strSQL)
		End If


		'Move to the next record
		rsTopic.MoveNext
	Loop




	'Delete the topics in this forum
	strSQL = "DELETE FROM " & portal.variablesForum.strDbTable & "Topic WHERE " & portal.variablesForum.strDbTable & "Topic.Forum_ID ="  & portal.variablesForum.intForumID & ";"

	'Write to database
	db.execute(strSQL)



	'Delete any group permissions set for the forum
	strSQL = "DELETE FROM " & portal.variablesForum.strDbTable & "Permissions WHERE " & portal.variablesForum.strDbTable & "Permissions.Forum_ID ="  & portal.variablesForum.intForumID & ";"
	
	'Write to database
	db.execute(strSQL)


	'close recordset
	rsTopic.Close



	'Move to the next record
	rsCommon.MoveNext
Loop



'Delete the forum
strSQL = "DELETE FROM " & portal.variablesForum.strDbTable & "Forum WHERE " & portal.variablesForum.strDbTable & "Forum.Cat_ID ="  & intCatID & ";"

'Write to database
db.execute(strSQL)



'Delete the category
strSQL = "DELETE FROM " & portal.variablesForum.strDbTable & "Category WHERE " & portal.variablesForum.strDbTable & "Category.Cat_ID ="  & intCatID & ";"

'Write to database
db.execute(strSQL)



'Reset Server Objects
Set rsPost = Nothing
rsCommon.Close
Set rsCommon = Nothing
Set rsTopic = Nothing
adoCon.Close
Set adoCon = Nothing


'Return to the forum categories page
response.redirect("view_forums.aspx"
%>