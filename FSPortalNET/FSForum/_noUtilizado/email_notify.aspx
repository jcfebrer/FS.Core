

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
'Set the response buffer to true as we maybe redirecting
Response.Buffer = True 

'Declare variables
Dim lngTopicID		'Holds the topic ID
Dim intForumID		'Holds the forum ID
Dim strReturnValue	'Holds the return value of the page
Dim strMode		'Holds the mode of the page
Dim strReturnPage	'Holds the return page
Dim intReadPermission	'Holds if the user has permission to read in this forum


'Read in the forum or topic ID
portal.variablesForum.intForumID = CInt(Request("FID"))
lngTopicID = CLng(Request.QueryString("TID"))
strMode = Request.QueryString("M")





'If there is no forum ID or Topic ID then send to the main forum page
If portal.variablesForum.intForumID = "" AND lngTopicID = "" Then 
	Set rsCommon = Nothing
	adoCon.Close
	Set adoCon = Nothing
	Response.Redirect("default.aspx")
End If






'If this is a Topic to watch then watch or unwatch this topic
If lngTopicID AND portal.variablesForum.blnEmail = True AND portal.variablesForum.intGroupID <> 2 AND strMode = "" Then Call WatchUnWatchTopic(lngTopicID)

'If this is a Topic to watch then watch or unwatch this topic
If portal.variablesForum.intForumID AND portal.variablesForum.blnEmail = True AND portal.variablesForum.intGroupID <> 2 AND strMode = "" Then Call WatchUnWatchForum(portal.variablesForum.intForumID)

'If this is a link form an unsubscribe email notify link in an email unwatch this topic or forum
If strMode = "Unsubscribe" AND portal.variablesForum.intForumID <> "" AND lngTopicID <> "" Then Call UnsubscribeEmailNotify(portal.variablesForum.intForumID, lngTopicID)

'If this is from the subscription page then add to the forum watch list
If portal.variablesForum.intForumID AND portal.variablesForum.blnEmail = True AND portal.variablesForum.intGroupID <> 2 AND strMode = "SP" Then Call WatchUnWatchForum(portal.variablesForum.intForumID)




'******************************************
'***  	  Watch or Unwatch Topic        ***
'******************************************

Private Function WatchUnWatchTopic(lngTopicID)

	'Initalise the strSQL variable with an SQL statement to query the database
	strSQL = "SELECT " & portal.variablesForum.strDbTable & "Forum.[Read] FROM " & portal.variablesForum.strDbTable & "Forum WHERE " & portal.variablesForum.strDbTable & "Forum.Forum_ID = " & portal.variablesForum.intForumID & ";"
	
	'Query the database
	rsCommon=db.execute(strSQL)
	
	'Read in the read permission for the forum
	intReadPermission = CInt(rsCommon("Read"))
	
	'Close recordset
	rsCommon.Close
	
	
	'Initalise the SQL string with a query to get the email notify topic details
	If portal.variablesForum.strDatabaseType = "SQLServer" Then
		strSQL = "EXECUTE " & portal.variablesForum.strDbProc & "TopicEmailNotify @lngUsuariosID = " & portal.variablesForum.lngLoggedInUserID & ", @lngTopicID= " & lngTopicID
	Else
		strSQL = "SELECT " & portal.variablesForum.strDbTable & "EmailNotify.*  "
		strSQL = strSQL & "FROM " & portal.variablesForum.strDbTable & "EmailNotify "
		strSQL = strSQL & "WHERE " & portal.variablesForum.strDbTable & "EmailNotify.UsuarioID=" & portal.variablesForum.lngLoggedInUserID & " AND " & portal.variablesForum.strDbTable & "EmailNotify.Topic_ID=" & lngTopicID & ";"
	End If

	With rsCommon

		'Set the cursor type property of the record set to Dynamic so we can navigate through the record set
		.CursorType = 2

		'Set the Lock Type for the records so that the record set is only locked when it is updated
		.LockType = 3
		
		'Query the database
		=db.execute(strSQL)


		'If the user no-longer wants email notification for this topic then remove the entry form the db
		If NOT .EOF Then
			
			Do while NOT .EOF

				'Delete the db entry
				.Delete
				
				'Move to next record
				.MoveNext
				
				'Set the return value
				strReturnValue = "&amp;eN=TU"
			Loop

		'Else if this is a new post and the user wants to be notified add the new entry to the database
		Else

			'Check to see if the user is allowed to view posts in this forum
			Call forumPermisisons(portal.variablesForum.intForumID, portal.variablesForum.intGroupID, intReadPermission, 0, 0, 0, 0, 0, 0, 0, 0, 0)
			
			'If the user can read in this forum the add them
			If portal.variablesForum.blnRead Then
				'Add new m_rs.AddNew
	
				'Create new entry
				.Fields("UsuarioID") = portal.variablesForum.lngLoggedInUserID
				.Fields("Topic_ID") = lngTopicID
	
				'Upade db with new m_rs.Update
				
				'Set the return value
				strReturnValue = "&amp;eN=TS"
			End If
		End If

		'Clean up
		.Close

	End With
	
	'Clean up
	Set rsCommon = Nothing
	adoCon.Close
	Set adoCon = Nothing
	
	'Return to Topic Page
	Response.Redirect("forum_posts.aspx?TID=" & lngTopicID & "&amp;pN=" & Request.QueryString("PN") & "&amp;tPN=" & Request.QueryString("TPN") & strReturnValue)
End Function






'******************************************
'***  	  Watch or Unwatch Forum        ***
'******************************************

Private Function WatchUnWatchForum(portal.variablesForum.intForumID)

	'Initalise the strSQL variable with an SQL statement to query the database
	strSQL = "SELECT " & portal.variablesForum.strDbTable & "Forum.[Read] FROM " & portal.variablesForum.strDbTable & "Forum WHERE " & portal.variablesForum.strDbTable & "Forum.Forum_ID = " & portal.variablesForum.intForumID & ";"
	
	'Query the database
	rsCommon=db.execute(strSQL)
	
	'Read in the read permission for the forum
	intReadPermission = CInt(rsCommon("Read"))
	
	'Close recordset
	rsCommon.Close

	'Initalise the SQL string with a query to get the email notify forum details
	If portal.variablesForum.strDatabaseType = "SQLServer" Then
		strSQL = "EXECUTE " & portal.variablesForum.strDbProc & "ForumEmailNotify @lngUsuariosID = " & portal.variablesForum.lngLoggedInUserID & ", @portal.variablesForum.intForumID= " & portal.variablesForum.intForumID
	Else
		strSQL = "SELECT " & portal.variablesForum.strDbTable & "EmailNotify.*  "
		strSQL = strSQL & "FROM " & portal.variablesForum.strDbTable & "EmailNotify "
		strSQL = strSQL & "WHERE " & portal.variablesForum.strDbTable & "EmailNotify.UsuarioID=" & portal.variablesForum.lngLoggedInUserID & " AND " & portal.variablesForum.strDbTable & "EmailNotify.Forum_ID=" & portal.variablesForum.intForumID & ";"
	End If

	With rsCommon

		'Set the cursor type property of the record set to Dynamic so we can navigate through the record set
		.CursorType = 2

		'Set the Lock Type for the records so that the record set is only locked when it is updated
		.LockType = 3
		
		'Query the database
		=db.execute(strSQL)


		'If the user no-longer wants email notification for this topic then remove the entry form the db
		If NOT .EOF Then

			'If this is not from teh subscription page then delete
			If strMode <> "SP" Then 
				Do while NOT .EOF
					'Delete the db entry
					.Delete
					
					'Move to next record
					.MoveNext
					
					'Set the return value
					strReturnValue = "&amp;eN=FU"
				Loop
			End If

		'Else if this is a new post and the user wants to be notified add the new entry to the database
		Else

			'Check to see if the user is allowed to view posts in this forum
			Call forumPermisisons(portal.variablesForum.intForumID, portal.variablesForum.intGroupID, intReadPermission, 0, 0, 0, 0, 0, 0, 0, 0, 0)
			
			'If the user can read in this forum the add them
			If portal.variablesForum.blnRead Then
				
				'Add new m_rs.AddNew
	
				'Create new entry
				.Fields("UsuarioID") = portal.variablesForum.lngLoggedInUserID
				.Fields("Forum_ID") = portal.variablesForum.intForumID
	
				'Upade db with new m_rs.Update
				
				'Set the return value
				strReturnValue = "&amp;eN=FS"
			End If
		End If

		'Clean up
		.Close

	End With
	
	'Clean up
	Set rsCommon = Nothing
	adoCon.Close
	Set adoCon = Nothing
	
	'Return to Forum Page
	If strMode = "SP" Then
		Response.Redirect("email_notify_subscriptions.aspx")
	Else
		Response.Redirect("forum_topics.aspx?FID=" & portal.variablesForum.intForumID & "&amp;pN=" & Request.QueryString("PN") & strReturnValue)
	End If
End Function






'******************************************
'*** Unsubscribe from email notify link ***
'******************************************

Private Function UnsubscribeEmailNotify(portal.variablesForum.intForumID, lngTopicID)

Response.Write("run")

	'If the user is not logged in then send them to the login page
	If portal.variablesForum.intGroupID = 2 Then Response.Redirect("login_user.aspx?FID=" & portal.variablesForum.intForumID & "&amp;tID=" & lngTopicID & "&amp;m=Unsubscribe")
	
	'Initalise the SQL string with a query to get the email notify topic details
	If portal.variablesForum.strDatabaseType = "SQLServer" Then
		strSQL = "EXECUTE " & portal.variablesForum.strDbProc & "TopicEmailNotify @lngUsuariosID = " & portal.variablesForum.lngLoggedInUserID & ", @lngTopicID= " & lngTopicID
	Else
		strSQL = "SELECT " & portal.variablesForum.strDbTable & "EmailNotify.*  "
		strSQL = strSQL & "FROM " & portal.variablesForum.strDbTable & "EmailNotify "
		strSQL = strSQL & "WHERE " & portal.variablesForum.strDbTable & "EmailNotify.UsuarioID=" & portal.variablesForum.lngLoggedInUserID & " AND " & portal.variablesForum.strDbTable & "EmailNotify.Topic_ID=" & lngTopicID & ";"
	End If

	With rsCommon

		'Set the cursor type property of the record set to Dynamic so we can navigate through the record set
		.CursorType = 2

		'Set the Lock Type for the records so that the record set is only locked when it is updated
		.LockType = 3
		
		'Query the database
		=db.execute(strSQL)


		'If a record is returned then the user is subscribed to the topic so delete their email notification
		If NOT .EOF Then

			Do while NOT .EOF
				'Delete the db entry
				.Delete
				
				'Move to next record
				.MoveNext
				
				'Set the return value
				strReturnValue = "&amp;eN=TU"
				strReturnPage = "forum_posts.aspx?TID=" & lngTopicID & strReturnValue
			Loop

		'Else the user is probally got forum post notification so check the db and delete that if they do
		Else
			'Clean up
			.Close
			
			'Initalise the SQL string with a query to get the poll details
			If portal.variablesForum.strDatabaseType = "SQLServer" Then
				strSQL = "EXECUTE " & portal.variablesForum.strDbProc & "ForumEmailNotify @lngUsuariosID = " & portal.variablesForum.lngLoggedInUserID & ", @portal.variablesForum.intForumID= " & portal.variablesForum.intForumID
			Else
				strSQL = "SELECT " & portal.variablesForum.strDbTable & "EmailNotify.*  "
				strSQL = strSQL & "FROM " & portal.variablesForum.strDbTable & "EmailNotify "
				strSQL = strSQL & "WHERE " & portal.variablesForum.strDbTable & "EmailNotify.UsuarioID=" & portal.variablesForum.lngLoggedInUserID & " AND " & portal.variablesForum.strDbTable & "EmailNotify.Forum_ID=" & portal.variablesForum.intForumID & ";"
			End If
		
		
			'Set the cursor type property of the record set to Dynamic so we can navigate through the record set
			.CursorType = 2
		
			'Set the Lock Type for the records so that the record set is only locked when it is updated
			.LockType = 3
				
			'Query the database
			=db.execute(strSQL)
		
			'If the user no-longer wants email notification for this topic then remove the entry form the db
			If NOT .EOF Then
		
				Do while NOT .EOF
					'Delete the db entry
					.Delete
					
					'Move to next record
					.MoveNext
						
					'Set the return value
					strReturnValue = "&amp;eN=FU"
					strReturnPage = "forum_topics.aspx?FID=" & portal.variablesForum.intForumID & strReturnValue
				Loop
			End If
		
		End If

		'Clean up
		.Close

	End With
	
	'Clean up
	Set rsCommon = Nothing
	adoCon.Close
	Set adoCon = Nothing
	
	'If there is no return page value then return to the forum
	If strReturnPage = "" Then
		Response.Redirect("forum_topics.aspx?FID=" & portal.variablesForum.intForumID)
	'Else return just to forum or topic that is related to
	Else
		Response.Redirect(strReturnPage)
	End If
End Function
%>