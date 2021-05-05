

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_DeleteMember" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
'Set the response buffer to true as we maybe redirecting
Response.Buffer = True 
Set rsCommon = Nothing


'If the user is user is using a banned IP redirect to an error page
If bannedIP() Then
	
	'Clean up
	adoCon.Close
	Set adoCon = Nothing
	
	'Redirect
	Response.Redirect("insufficient_permission.aspx?M=IP")

End If

'dimension variables
Dim lngMemberID	'Holds the member Id to delete
Dim strReturn	'Holds the return page mode



'Intilise variables
strReturn = "UPD"


'Read in the member ID to delete
lngMemberID = CLng(Request.QueryString("MID"))


'If this is the forum admin and the ID number passed across is numeric then delete the member
If portal.variablesForum.blnAdmin = True AND isNumeric(lngMemberID) Then
	
	'Make sure we are not trying to delete the admin or geust account
	If lngMemberID > 2 Then
	
		'Delete the members buddy list
		'Initalise the strSQL variable with an SQL statement
		strSQL = "DELETE FROM " & portal.variablesForum.strDbTable & "BuddyList WHERE (UsuarioID ="  & lngMemberID & ") OR (Buddy_ID ="  & lngMemberID & ")"
		
		'Write to database
		db.execute(strSQL)	
		
		
		'Delete the members private msg's
		strSQL = "DELETE FROM " & portal.variablesForum.strDbTable & "PMMessage WHERE (UsuarioID ="  & lngMemberID & ")"
			
		'Write to database
		db.execute(strSQL)	
		
		
		'Delete the members private msg's
		strSQL = "DELETE FROM " & portal.variablesForum.strDbTable & "PMMessage WHERE (From_ID ="  & lngMemberID & ")"
			
		'Write to database
		db.execute(strSQL)
		
		
		'Set all the users private messages to Guest account
		strSQL = "UPDATE " & portal.variablesForum.strDbTable & "PMMessage SET From_ID=2 WHERE (From_ID ="  & lngMemberID & ")"
			
		'Write to database
		db.execute(strSQL)
		
		
		'Set all the users posts to the Guest account
		strSQL = "UPDATE " & portal.variablesForum.strDbTable & "Thread SET UsuarioID=2 WHERE (UsuarioID ="  & lngMemberID & ")"
			
		'Write to database
		db.execute(strSQL)
				
		
		'Delete the user from the email notify table
		strSQL = "DELETE FROM " & portal.variablesForum.strDbTable & "EmailNotify WHERE (UsuarioID ="  & lngMemberID & ")"
			
		'Write to database
		db.execute(strSQL)
		
		
		'Delete the user from forum permissions table
		strSQL = "DELETE FROM " & portal.variablesForum.strDbTable & "Permissions WHERE (UsuarioID ="  & lngMemberID & ")"
			
		'Write to database
		db.execute(strSQL)
		
		
		'Finally we can now delete the member from the forum
		strSQL = "DELETE FROM " & "Usuarios WHERE (UsuarioID ="  & lngMemberID & ")"
			
		'Write to database
		db.execute(strSQL)
		
		'Return page mode
		strReturn = "DEL"
	End If	
End If

'Reset main server variables
adoCon.Close
Set adoCon = Nothing

'Return to forum
Response.Redirect("register_confirm.aspx?TP=" & strReturn & "&amp;fID=0")
%>