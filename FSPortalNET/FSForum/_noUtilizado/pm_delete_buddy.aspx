

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
'Set the response buffer to true as we maybe redirecting
Response.Buffer = True

'Clean up
Set rsCommon = Nothing

'If Priavte messages are not on then send them away
If blnPrivateMessages = False Then 
	'Clean up
	adoCon.Close
	Set adoCon = Nothing
	
	'Redirect
	Response.Redirect("default.aspx")
End If


'If the user is not allowed then send them away
If portal.variablesForum.intGroupID = 2 OR blnActiveMember = False Then 
	'Clean up
	adoCon.Close
	Set adoCon = Nothing
	
	'Redirect
	Response.Redirect("insufficient_permission.aspx")
End If

'Delete the topic from the database	
'Initalise the strSQL variable with an SQL statement to get the topic from the database
strSQL = "DELETE FROM " & portal.variablesForum.strDbTable & "BuddyList WHERE (((" & portal.variablesForum.strDbTable & "BuddyList.UsuarioID)="  & portal.variablesForum.lngLoggedInUserID & " ) AND ((" & portal.variablesForum.strDbTable & "BuddyList.Address_ID)= " & CLng(Request.QueryString("pm_id")) & "));"
		
'Delete the message from the database
db.execute(strSQL)

'Reset Server Objects
adoCon.Close
Set adoCon = Nothing

'Return to the page showing the threads
response.redirect("pm_buddy_list.aspx"
%>