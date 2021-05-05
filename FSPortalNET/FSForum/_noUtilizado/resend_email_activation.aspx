

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<!--#include virtual="/fsportalnet/includes/funcionesMail.aspx" -->
<%
Response.Buffer = True


'Dimension variables
Dim strUsuariosEmail	'Holds the users e-mail address
Dim strFormMessage	'Holds the message in the form
Dim strEmailBody	'Holds the body of the e-mail
Dim blnSentEmail	'Set to true when the e-mail is sent
Dim strSubject		'Holds the subject of the e-mail
Dim strRealName		'Holds the Usuarioss real name
Dim intForumID

'Initialise variables
blnSentEmail = False


'If the user is user is using a banned IP or the account has been deactivated redirect to an error page
If bannedIP() OR InStr(1, strLoggedInUserCode, "N0act", vbTextCompare) Then
	
	'Clean up
	Set rsCommon = Nothing
	adoCon.Close
	Set adoCon = Nothing
	
	'Redirect
	Response.Redirect("insufficient_permission.aspx")

End If


'See if the user is allowed to get an email activation
If portal.variablesForum.blnEmail = False OR portal.variablesForum.lngLoggedInUserID = 2 OR blnActiveMember OR portal.variablesForum.blnLoggedInUserEmail = False Then 
	'Clean up
	Set rsCommon = Nothing
	adoCon.Close
	Set adoCon = Nothing
	
	'Redirect
	response.redirect("default.aspx"
End If

'Inititlaise the subject of the e-mail that may be sent in the next if/ifelse statements
strSubject = "" & portal.variablesForum.strTxtWelcome & " " & portal.variablesForum.strTxtEmailToThe & " " & strMainForumName

'Send an e-mail to enable the users account to be activated
'Initailise the e-mail body variable with the body of the e-mail
strEmailBody = portal.variablesForum.strTxtHi & " " & decodeString(strLoggedInusuario)
strEmailBody = strEmailBody & vbCrLf & vbCrLf & portal.variablesForum.strTxtEmailThankYouForRegistering & " " & strMainForumName & "."
strEmailBody = strEmailBody & vbCrLf & vbCrLf & portal.variablesForum.strTxtToActivateYourMembershipFor & " " & strMainForumName & " " & portal.variablesForum.strTxtForumClickOnTheLinkBelow & ": -"
strEmailBody = strEmailBody & vbCrLf & vbCrLf & strForumPath & "/activate.aspx?ID=" & Server.URLEncode(strLoggedInUserCode)

'Send the e-mail using the Send Mail function created on the send_mail_function.inc file
blnSentEmail = funcMail.SendMailForo(strEmailBody, decodeString(strLoggedInusuario), decodeString(strLoggedInUserEmail), strMainForumName, decodeString(strForumEmailAddress), strSubject, strMailComponent, false)


'Reset server objects
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing
%>
<html>
<head>

<title>Resend Activation Email</title>


     	

<!--#include file="includes/skin_file.aspx" -->
</head>
<body bgcolor="<% = strBgColour %>" text="<% = strTextColour %>" background="<% = strBgImage %>" marginheight="0" marginwidth="0" topmargin="0" leftmargin="0" OnLoad="self.focus();">
<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="1" align="center">
 <tr>
  <td align="center"><span class="heading"><% = portal.variablesForum.strTxtResendActivationEmail %></span></td>
 </tr>
</table>
<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="1" align="center">
 <tr>
  <td align="center" height="79" class="text"><% = portal.variablesForum.strTxtYouShouldReceiveAnEmail %></td>
 </tr>
</table>
<br />
<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="1" align="center">
 <tr>
  <td align="center" height="34"><a href="JavaScript:onClick=window.close()"><% = portal.variablesForum.strTxtCloseWindow %></a>
  <br /><br /><br />
<%

%>
  </td>
 </tr>
</table>
</body>
</html>