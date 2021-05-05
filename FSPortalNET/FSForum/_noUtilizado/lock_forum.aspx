

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
'Set the response buffer to true as we maybe redirecting
Response.Buffer = True 

'Dimension variables
Dim strMode		'Holds the mode of the page
Dim intForumID		'Holds the Forum ID number


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
portal.variablesForum.intForumID = CLng(Request.QueryString("FID"))
strMode = Request.QueryString("mode")

'Check that the user is admin
If portal.variablesForum.blnAdmin = True Then


	'Get the Forum from the database to be locked
		
	'Initalise the strSQL variable with an SQL statement to get the forum from the database
	strSQL = "SELECT " & portal.variablesForum.strDbTable & "Forum.Locked FROM " & portal.variablesForum.strDbTable & "Forum WHERE " & portal.variablesForum.strDbTable & "Forum.Forum_ID ="  & portal.variablesForum.intForumID & ";"
		
	'Set the cursor type property of the record set to Dynamic so we can navigate through the record set
	rsCommon.CursorType = 2
		
	'Set set the lock type of the recordset to optomistic while the record is deleted
	rsCommon.LockType = 3
		
	'Query the database
	rsCommon=db.execute(strSQL)  
	
	'If there is a forum returned then lock it
	If NOT rsCommon.EOF Then
		
		'Lock the topic
		If strMode = "Lock" Then
			rsCommon("Locked") = 1
		'Unlock topic
		ElseIf strMode = "UnLock" Then
			rsCommon("Locked") = 0
		End If
		rsCommon.Update
	End If
		
	
	'Release server objects
	rsCommon.Close
End If


'Reset Server Objects
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing

'If this is a lock from the admin area then return there
If CInt(Request.QueryString("code")) = 2 Then
	response.redirect("admin/view_forums.aspx"
Else
	'Return to the page showing the threads
	response.redirect("default.aspx"
End If
%>