

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
'Set the response buffer to true as we maybe redirecting
Response.Buffer = True 

'Dimension variables
Dim rsTopicDelete		'Holds the deleting recordset
Dim intForumID			'Holds the forum ID number
Dim strMode			'Holds the mode of the page
Dim lngTopicID			'Holds the Topic ID number
Dim lngMessageID		'Holds the message ID to be deleted
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
lngMessageID = CLng(Request.QueryString("PID"))



'Read in the forum and topic ID from the database for this message

'Initliase the SQL query to get the topic and forumID from the database
strSQL = "SELECT " & portal.variablesForum.strDbTable & "Topic.Topic_ID, " & portal.variablesForum.strDbTable & "Topic.Forum_ID "
strSQL = strSQL & "FROM " & portal.variablesForum.strDbTable & "Topic, " & portal.variablesForum.strDbTable & "Thread "
strSQL = strSQL & "WHERE " & portal.variablesForum.strDbTable & "Topic.Topic_ID = " & portal.variablesForum.strDbTable & "Thread.Topic_ID AND " & portal.variablesForum.strDbTable & "Thread.Thread_ID=" & lngMessageID & ";"
	
'Query the database
rsCommon=db.execute(strSQL) 

'If there is a record returened read in the forum ID
If NOT rsCommon.EOF Then
	lngTopicID = CLng(rsCommon("Topic_ID"))
	portal.variablesForum.intForumID = CInt(rsCommon("Forum_ID"))
End If

'Clean up
rsCommon.Close
	



'Read in the forum name and forum permissions from the database
'Initalise the strSQL variable with an SQL statement to query the database
If portal.variablesForum.strDatabaseType = "SQLServer" Then
	strSQL = "EXECUTE " & portal.variablesForum.strDbProc & "ForumsAllWhereForumIs @portal.variablesForum.intForumID = " & portal.variablesForum.intForumID
Else
	strSQL = "SELECT " & portal.variablesForum.strDbTable & "Forum.* FROM " & portal.variablesForum.strDbTable & "Forum WHERE Forum_ID = " & portal.variablesForum.intForumID & ";"
End If

'Query the database
rsCommon=db.execute(strSQL)

'Read in wether the forum is locked or not
If NOT rsCommon.EOF Then 
	
	'Check the user is welcome in this forum
	Call forumPermisisons(portal.variablesForum.intForumID, portal.variablesForum.intGroupID, 0, 0, 0, 0, CInt(rsCommon("Delete_posts")), 0, 0, 0, 0, 0)
End If

'Clean up
rsCommon.Close




'Get the Post to be deleted from the database
	
'Initalise the strSQL variable with an SQL statement to get the post from the database
strSQL = "SELECT " & portal.variablesForum.strDbTable & "Thread.* FROM " & portal.variablesForum.strDbTable & "Thread WHERE " & portal.variablesForum.strDbTable & "Thread.Thread_ID ="  & lngMessageID & ";"

'Set the cursor type property of the record set to Dynamic so we can navigate through the record set
rsCommon.CursorType = 2

'Set set the lock type of the recordset to optomistic while the record is deleted
rsCommon.LockType = 3

'Query the database
rsCommon=db.execute(strSQL)  

'Read in the Usuarios ID of the message to be deleted
If NOT rsCommon.EOF Then lngDelMsgUsuariosID = CLng(rsCommon("UsuarioID"))




'Check to make sure the user is deleting the post enetered the post or a moderator with detlete rights or the forum adminstrator
If (lngDelMsgUsuariosID = portal.variablesForum.lngLoggedInUserID OR portal.variablesForum.blnAdmin = True OR portal.variablesForum.blnModerator = True) AND (portal.variablesForum.blnDelete = True OR portal.variablesForum.blnAdmin = True) Then


	'First we need to delete any entry in the GuestName table incase this was a guest poster posting the message
	strSQL = "DELETE FROM " & portal.variablesForum.strDbTable & "GuestName WHERE " & portal.variablesForum.strDbTable & "GuestName.Thread_ID="  & lngMessageID & ";"

	'Excute SQL
	db.execute(strSQL)
	
	


	'Delete the record set
	rsCommon.Delete
	
	'We need to requry the database before moving on as Access can take a few moments to delete the record
	rsCommon.Requery
	
	'Close the recordset
	rsCommon.Close
	
	
		
	'Initalise the strSQL variable with an SQL statement to query the database to get the number of posts the user has made
	strSQL = "SELECT " & "Usuarios.No_of_posts " 
	strSQL = strSQL & "FROM " & "Usuarios "
	strSQL = strSQL & "WHERE " & "Usuarios.UsuarioID= " & lngDelMsgUsuariosID & ";"
	
	'Set the cursor type property of the record set to Dynamic so we can navigate through the record set
	rsCommon.CursorType = 2
			
	'Set the Lock Type for the records so that the record set is only locked when it is updated
	rsCommon.LockType = 3
			
	'Query the database
	rsCommon=db.execute(strSQL)
		
	'If there is a record returned by the database then read in the no of posts and decrement it by 1
	If NOT rsCommon.EOF Then
		
		'Read in the no of posts the user has made and usuario
		lngNumOfPosts = CLng(rsCommon("No_of_posts"))
		
		'decrement the number of posts by 1
		lngNumOfPosts = lngNumOfPosts - 1
			
		'Place the new number of posts in the recordset
		rsCommon.Fields("No_of_posts") =  lngNumOfPosts
		
		'Update the database
		rsCommon.Update	
	End If
		
	'Close the recordset
	rsCommon.Close
	
	
	
	
	'Check there are other Threads for the Topic 	
	'Initalise the strSQL variable with an SQL statement to get the Threads from the database
	strSQL = "SELECT " & portal.variablesForum.strDbTable & "Thread.Thread_ID, " & portal.variablesForum.strDbTable & "Thread.Message_Date FROM " & portal.variablesForum.strDbTable & "Thread WHERE " & portal.variablesForum.strDbTable & "Thread.Topic_ID ="  & lngTopicID & " ORDER BY " & portal.variablesForum.strDbTable & "Thread.Message_date ASC;"
	
	'Query the database
	rsCommon=db.execute(strSQL)
	
	'Get the Topic from the database to be deleted
	'Create a recordset object for the Topic in the database
	Set rsTopicDelete = Server.CreateObject("ADODB.Recordset")
		
	'Initalise the strSQL variable with an SQL statement to get the topic from the database
	strSQL = "SELECT " & portal.variablesForum.strDbTable & "Topic.* FROM " & portal.variablesForum.strDbTable & "Topic WHERE " & portal.variablesForum.strDbTable & "Topic.Topic_ID ="  & lngTopicID & ";"
		
	'Set the cursor type property of the record set to Dynamic so we can navigate through the record set
	rsTopicDelete.CursorType = 2
		
	'Set set the lock type of the recordset to optomistic while the record is deleted
	rsTopicDelete.LockType = 3
		
	'Query the database
	rsTopicDelete=db.execute(strSQL)  
	
	'If there are threads left make sure the last topic date has the date of the last entry
	If NOT rsCommon.EOF Then
		
		'Place the date of the start date of the first message in the topic table
		rsTopicDelete("Start_date") = CDate(rsCommon("Message_date"))
		
		'Move to the last message in the topic to get the last message date
		rsCommon.MoveLast
		
		'Place the date of the last post in the last enry date
		rsTopicDelete("Last_entry_date") = CDate(rsCommon("Message_date"))
		rsTopicDelete.Update
		
	
	
	
	'If there are no more posts in the database for the topic then delete the topic from the database
	Else	
		
		'If there is a poll and no more posts left delete the poll as well
		If CLng(rsTopicDelete("Poll_ID")) <> 0 Then 

		          'Delete the Poll choices 
		          strSQL = "DELETE FROM " & portal.variablesForum.strDbTable & "PollChoice WHERE " & portal.variablesForum.strDbTable & "PollChoice.Poll_ID=" & CLng(rsTopicDelete("Poll_ID")) & ";" 
		
		          'Write to database 
		          db.execute(strSQL) 
		
		          'Delete the Poll 
		          strSQL = "DELETE FROM " & portal.variablesForum.strDbTable & "Poll WHERE " & portal.variablesForum.strDbTable & "Poll.Poll_ID=" & CLng(rsTopicDelete("Poll_ID")) & ";" 
		
		          'Write to database 
		          db.execute(strSQL) 
    		End If
		
		
		'Delete the record set
		rsTopicDelete.Delete
		
		'Update the number of topics and posts in the database
		Call updateTopicPostCount(portal.variablesForum.intForumID)
		
		'Reset Server Objects
		rsCommon.Close
		Set rsCommon = Nothing
		rsTopicDelete.Close
		Set rsTopicDelete = Nothing
		adoCon.Close
		Set adoCon = Nothing
		
		'Return to the page showing the the topics in the forum
		response.redirect("forum_topics.aspx?FID=" & portal.variablesForum.intForumID & "&amp;pN=" & Request.QueryString("PN")
	End If
	
	
	'Release server objects
	rsTopicDelete.Close
	Set rsTopicDelete = Nothing
End If



'Update the number of topics and posts in the database
Call updateTopicPostCount(portal.variablesForum.intForumID)



'Reset Server Objects
rsCommon.Close
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing




'Return to the page showing the threads
response.redirect("forum_posts.aspx?FID=" & portal.variablesForum.intForumID & "&amp;tID=" & lngTopicID & "&amp;pN=" & Request.QueryString("PN") & "&amp;tPN=" & Request.QueryString("TPN")
%>