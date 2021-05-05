

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
'Set the response buffer to true as we maybe redirecting
Response.Buffer = True


Dim laryMesageID 	'Holds the message id number

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



'If this is delete all then delete all messages
If Request.Form("delAll") <> "" Then
	
	'Delete the PM from the database	
	'Initalise the strSQL variable
	strSQL = "DELETE FROM " & portal.variablesForum.strDbTable & "PMMessage WHERE (" & portal.variablesForum.strDbTable & "PMMessage.UsuarioID="  & portal.variablesForum.lngLoggedInUserID & " );"
			
	'Delete the message from the database
	db.execute(strSQL)
	
End If


'If this is deleting only the selected ones then  do so
If Request.Form("delSel") <> "" Then
	
	'Run through till all checked messages are deleted
	For each laryMesageID in Request.Form("chkDelete")
	
		'Delete the PM from the database	
		'Initalise the strSQL variable
		strSQL = "DELETE FROM " & portal.variablesForum.strDbTable & "PMMessage WHERE (((" & portal.variablesForum.strDbTable & "PMMessage.UsuarioID)="  & portal.variablesForum.lngLoggedInUserID & " ) AND ((" & portal.variablesForum.strDbTable & "PMMessage.PM_ID)= " & CLng(laryMesageID) & "));"
				
		'Delete the message from the database
		db.execute(strSQL)
	Next
End If

'This delete is for the delete button on the show pm page
If Request.QueryString("pm_id") <> "" Then
	
	'Delete the topic from the database	
	'Initalise the strSQL variable with an SQL statement to get the topic from the database
	strSQL = "DELETE FROM " & portal.variablesForum.strDbTable & "PMMessage WHERE (((" & portal.variablesForum.strDbTable & "PMMessage.UsuarioID)="  & portal.variablesForum.lngLoggedInUserID & " ) AND ((" & portal.variablesForum.strDbTable & "PMMessage.PM_ID)= " & CLng(Request.QueryString("pm_id")) & "));"
			
	'Delete the message from the database
	db.execute(strSQL)
End If


'Reset Server Objects
adoCon.Close
Set adoCon = Nothing


'Return to the page showing the threads
response.redirect("pm_inbox.aspx?MSG=DEL"
%>