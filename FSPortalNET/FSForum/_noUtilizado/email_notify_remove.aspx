

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
'Set the response buffer to true as we maybe redirecting
Response.Buffer = True 

'Declare variables
Dim intForumID		'Holds the forum ID
Dim laryWatchID		'Array to hold the ID number for each email noti to be deleted
Dim strMode		'Holds the mode of the page
Dim lngEmailUserID		'Holds the user ID to look at email notification for

'Read in the mode of the page
strMode = Trim(Mid(Request.QueryString("M"), 1, 1))


'If this is not an admin but in admin mode then see if the user is a moderator
If portal.variablesForum.blnAdmin = False AND strMode = "A" Then
	
	'Initalise the strSQL variable with an SQL statement to query the database
        strSQL = "SELECT " & portal.variablesForum.strDbTable & "Permissions.Moderate "
        strSQL = strSQL & "FROM " & portal.variablesForum.strDbTable & "Permissions "
        If portal.variablesForum.strDatabaseType = "SQLServer" Then
                strSQL = strSQL & "WHERE " & portal.variablesForum.strDbTable & "Permissions.Group_ID = " & portal.variablesForum.intGroupID & " AND  " & portal.variablesForum.strDbTable & "Permissions.Moderate = 1;"
        Else
                strSQL = strSQL & "WHERE " & portal.variablesForum.strDbTable & "Permissions.Group_ID = " & portal.variablesForum.intGroupID & " AND  " & portal.variablesForum.strDbTable & "Permissions.Moderate = True;"
        End If
               

        'Query the database
         rsCommon=db.execute(strSQL)

        'If a record is returned then the user is a moderator in one of the forums
        If NOT rsCommon.EOF Then portal.variablesForum.blnModerator = True

        'Clean up
        rsCommon.Close
End If


'Get the user ID of the email notifications to look at
If (portal.variablesForum.blnAdmin OR (portal.variablesForum.blnModerator AND CLng(Request.QueryString("PF")) > 2)) AND strMode = "A" Then
	
	lngEmailUserID = CLng(Request.QueryString("PF"))

'Get the logged in ID number
Else
	lngEmailUserID = portal.variablesForum.lngLoggedInUserID
End If



'Run through till all checked email notifications are deleted
For each laryWatchID in Request.Form("chkDelete")
	
	'Delete the poll choice
	strSQL = "DELETE FROM " & portal.variablesForum.strDbTable & "EmailNotify WHERE " & portal.variablesForum.strDbTable & "EmailNotify.Watch_ID =" & CLng(laryWatchID) & " AND " & portal.variablesForum.strDbTable & "EmailNotify.UsuarioID=" & lngEmailUserID & ";"

	'Delete the threads
	db.execute(strSQL)
Next
	 
'Reset Server Variables
Set rsCommon = Nothing	
adoCon.Close
Set adoCon = Nothing

'Redirect back
Response.Redirect("email_notify_subscriptions.aspx?" & Request.QueryString)
%>