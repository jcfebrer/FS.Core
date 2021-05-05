
<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_ForumActivate" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
'Set the response buffer to true as we maybe redirecting and setting a cookie
Response.Buffer = True 


'Dimension variables
Dim strUserCode			'Holds a code for the user
Dim lngUserID			'Holds the new users ID number
Dim blnActivated		'Set to true if the account is activated
Dim intForumID			'Holds the ID number of teh forum

blnActivated = False

'Read in the users ID from the query string
strUserCode = Trim(Mid(Request.QueryString("ID"), 1, 44))

'Make the usercode SQL safe
strUserCode = formatSQLInput(strUserCode)


'If therese a usercode activate it
If strUserCode <> "" Then
	
	'Intialise the strSQL variable with an SQL string to open a record set for the Usuarios table
	strSQL = "SELECT " & "Usuarios.* From " & "Usuarios WHERE " & "Usuarios.User_code = '" & strUserCode & "';"
	
	'Set the cursor type property of the record set to Dynamic so we can navigate through the record set
	rsCommon.CursorType = 2
	
	'Set the Lock Type for the records so that the record set is only locked when it is updated
	rsCommon.LockType = 3
	
	'Open the Usuarios table
	rsCommon=db.execute(strSQL)
	
	'If these a record returned then alls well so cteate a new id code and activate the membership
	If NOT rsCommon.EOF AND InStr(1, strUserCode, "N0act", vbTextCompare) = 0 Then
		
		'Calculate a new code for the user
		strUserCode = func.userCode(rsCommon("usuario"))

		'Update the database by actvating the users account
		rsCommon.Fields("User_code") = strUserCode
		rsCommon.Fields("Active") = 1
			
		'Update the database with the new user's details
		rsCommon.Update
			
		'Write a cookie with the User ID number so the user logged in throughout the forum	
		'Write the cookie with the name Forum containing the value UserID number
		Response.Cookies(portal.variables.strCookieName)("UID") = strUserCode
		
		'Set the activate boolean to true
		blnActivated = True
	End If
	
	'Release objects
	rsCommon.Close
End If
%>  
<html>
<head>
<title>Registrar nuevo usuario</title>

<!-- #include file="includes/header.aspx" -->
<navigation:navigation ID="common1" runat="server" />
  <table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="3" align="center">
 <tr> 
  <td align="left" class="heading"><% = portal.variablesForum.strTxtActivateAccount %></td>
</tr>
 <tr> 
  <td align="left" width="71%" class="bold"><img src="<% = portal.variablesForum.strImagePath %>open_folder_icon.gif" border="0" align="middle">&nbsp;<a href="default.aspx" target="_self" class="boldLink"><% = strMainForumName %></a><% = strNavSpacer %><% = portal.variablesForum.strTxtActivateAccount %><br /></td>
  </tr>
</table>
<div align="center">
<br />
  <br />
  <br />
<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="0" align="center">
  <tr>
  <td align="center" class="text"><%
'Reset Server Objects
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing  


'If the account is now active display a message
If blnActivated = True Then
  	
  	Response.Write(portal.variablesForum.strTxtYourForumMemIsNowActive & _
	vbCrLf & "<br /><br />" & portal.variablesForum.strTxtYouCanAccessCP & "<a href=""member_control_panel.aspx"" target=""_self"">" & portal.variablesForum.strTxtMemberCPMenu & "</a>") 

'If their account has been suspended and they are trying to reactivate it then tell them they can't
ElseIf InStr(1, strUserCode, "N0act", vbTextCompare) Then
	
	Response.Write(portal.variablesForum.strTxtForumMemberSuspended)


'Theres been a problem so display an error message
Else 
   	Response.Write(portal.variablesForum.strTxtErrorWithActvation & " " & strMainForumName & " " & " <a href=""mailto:" & strForumEmailAddress & """>" & portal.variablesForum.strTxtForumAdministrator & "</a>.")
End If 

Response.Write("<br /><br /><a href=""default.aspx"" target=""_self"">" & portal.variablesForum.strTxtReturnToDiscussionForum & "</a>")

%></td>
  </tr>
 </table>
</div> 
<br /><br /><br /><br /><br />
  <div align="center">
<% 

'Display the process time
If blnShowProcessTime Then response.write("<span class=""smText""><br /><br />" & portal.variablesForum.strTxtThisPageWasGeneratedIn & " " & FormatNumber(Timer() - dblStartTime, 4) & " " & portal.variablesForum.strTxtSeconds & "</span>"
%>
</div>

