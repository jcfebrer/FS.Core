
<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
Session.Timeout =  1000

'Set the response buffer to true as we maybe redirecting
Response.Buffer = True

'Dimension variables
Dim intNoOfDays			'Holds the number of days to delete posts from
Dim intForumID			'Holds the forum ID number
Dim lngNumberOfTopics		'Holds the number of topics that are deleted
Dim lngPollID			'Holds the poll ID if there is one to delete
Dim rsThread			'Holds the threads recordset
Dim intPriority			'Holds the topic priority to delete

'Initilise variables
lngNumberOfTopics = 0

'get teh number of days to delte from
intNoOfDays = CInt(Request.Form("days"))
portal.variablesForum.intForumID = CInt(Request.Form("FID"))
intPriority = CInt(Request.Form("priority"))




'Get all the Topics from the database to be deleted

'Initalise the strSQL variable with an SQL statement to get the topic from the database
strSQL = "SELECT " & portal.variablesForum.strDbTable & "Topic.Topic_ID, " & portal.variablesForum.strDbTable & "Topic.Poll_ID FROM " & portal.variablesForum.strDbTable & "Topic "
If portal.variablesForum.intForumID = 0 Then
	strSQL = strSQL & "WHERE " & portal.variablesForum.strDbTable & "Topic.Last_entry_date < " & strDatabaseDateFunction & " - " & intNoOfDays
Else
	strSQL = strSQL & "WHERE (" & portal.variablesForum.strDbTable & "Topic.Last_entry_date < " & strDatabaseDateFunction & " - " & intNoOfDays  & ") AND (" & portal.variablesForum.strDbTable & "Topic.Forum_ID=" & portal.variablesForum.intForumID & ")"
End If
If intPriority <> 4 Then strSQL = strSQL & " AND (" & portal.variablesForum.strDbTable & "Topic.Priority=" & intPriority & ")"
strSQL = strSQL & ";"

'Query the database
rsCommon=db.execute(strSQL)


'Create a record set object to the Threads held in the database
Set rsThread = Server.CreateObject("ADODB.Recordset")


'Loop through all the topics to get all the thread in the topics to be deleted
Do While NOT rsCommon.EOF

	'Update the number of topics deletd
	lngNumberOfTopics = lngNumberOfTopics + 1
	
	'Get the Poll ID
	lngPollID = CLng(rsCommon("Poll_ID"))
	
	
	
	'See if there are any guest posters and delete thier names form the guest name table
	
	'Initalise the strSQL variable with an SQL statement to get the topic from the database
	strSQL = "SELECT " & portal.variablesForum.strDbTable & "Thread.Thread_ID FROM " & portal.variablesForum.strDbTable & "Thread WHERE " & portal.variablesForum.strDbTable & "Thread.Topic_ID=" & rsCommon("Topic_ID") & ";"
	
	'Query the database
	rsThread=db.execute(strSQL)
	
	'Loop through and delete al names in the guest name table
	Do While NOT rsThread.EOF
		
		'First we need to delete any entry in the GuestName table incase this was a guest poster posting the message
		strSQL = "DELETE FROM " & portal.variablesForum.strDbTable & "GuestName WHERE " & portal.variablesForum.strDbTable & "GuestName.Thread_ID=" & CLng(rsThread("Thread_ID")) & ";"
		
		'Excute SQL
		db.execute(strSQL)
		
		'Move next
		rsThread.MoveNext
	Loop
	
	'Close the rs
	rsThread.Close
	
	

	'Delete the thread
	strSQL = "DELETE FROM " & portal.variablesForum.strDbTable & "Thread WHERE " & portal.variablesForum.strDbTable & "Thread.Topic_ID =" & rsCommon("Topic_ID") & ";"

	'Delete the threads
	db.execute(strSQL)
	
	

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
	rsCommon.MoveNext
Loop



'Initalise the strSQL variable with an SQL statement to get the topic from the database
strSQL = "DELETE FROM " & portal.variablesForum.strDbTable & "Topic "
If portal.variablesForum.intForumID = 0 Then
	strSQL = strSQL & "WHERE " & portal.variablesForum.strDbTable & "Topic.Last_entry_date < " & strDatabaseDateFunction & " - " & intNoOfDays
Else
	strSQL = strSQL & "WHERE (" & portal.variablesForum.strDbTable & "Topic.Last_entry_date < " & strDatabaseDateFunction & " - " & intNoOfDays  & ") AND (" & portal.variablesForum.strDbTable & "Topic.Forum_ID=" & portal.variablesForum.intForumID & ")"
End If
If intPriority <> 4 Then strSQL = strSQL & " AND (" & portal.variablesForum.strDbTable & "Topic.Priority=" & intPriority & ")"
strSQL = strSQL & ";"

'Delete the topics
db.execute(strSQL)



'Update post count
updateTopicPostCount(portal.variablesForum.intForumID)


'Reset Server Objects
Set rsThread = Nothing
rsCommon.Close
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing

%>
<html>
<head>

<title>Batch Delete Forum Topics</title>

<link href="includes/default_style.css" rel="stylesheet" type="text/css">
</head>
<body  background="images/main_bg.gif" bgcolor="#FFFFFF" text="#000000">
<div align="center"> 
 <p class="text"><span class="heading">Batch Delete Forum Topics </span><br />
  <a href="admin_menu.aspx" target="_self">Return to the the Administration Menu</a><br />
  <br />
  <br />
  <br />
  <br />
  <br />
  <span class="bold">
  <% = lngNumberOfTopics %>
  Topics have been Deleted.</span><br />
  <br />
  <br />
  <br />
  <br />
  <a href="resync_forum_post_count.aspx">Click here to re-sync Post and Topic Counts for the Forums</a> </p>
</div>
</body>
</html>
