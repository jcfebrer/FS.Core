

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<!--#include virtual="/fsportalnet/includes/funcionesMail.aspx" -->
<%
'Set the response buffer to true as we maybe redirecting
Response.Buffer = True


'Dimension variables
Dim lngToUserID		'Holds the user ID of who the email is to
Dim strToUser		'Holds the user name of the person the email is to
Dim intForumID		'Holds the forum ID
Dim blnShowEmail	'set to true if the user allws emailing to them
Dim strToEmail		'Holds the email address of who the email is to
Dim strFromEmail	'Holds the email address of who the email is from
Dim blnEmailSent	'Set to true if the email has been sent
Dim strEmailBody
Dim strSubject


'Get who the email is to
lngToUserID = CLng(Request("SEID"))

'If there is no recopinet for the email then send em to homepage
If Request("SEID") = "" OR portal.variablesForum.blnEmailMessenger = False OR portal.variablesForum.blnEmail = False Then
	Set rsCommon = Nothing
	adoCon.Close
	Set adoCon = Nothing

	Response.Redirect("default.aspx")
End If


'If the user is user is using a banned IP redirect to an error page
If bannedIP() Then
	
	'Clean up
	Set rsCommon = Nothing
	adoCon.Close
	Set adoCon = Nothing
	
	'Redirect
	Response.Redirect("insufficient_permission.aspx?M=IP")

End If


'Initlise variables
portal.variablesForum.blnEmailSent = False

'Get the email address and name of the person the email is to be sent to

'Initalise the strSQL variable with an SQL statement to query the database
strSQL = "SELECT " & "Usuarios.usuario, " & "Usuarios.email, " & "Usuarios.Show_email "
strSQL = strSQL & "FROM " & "Usuarios "
strSQL = strSQL & "WHERE " & "Usuarios.UsuarioID = " & lngToUserID

'Query the database
rsCommon=db.execute(strSQL)

'Read in the details from the user
If NOT rsCommon.EOF Then

	strToUser = rsCommon("usuario")
	strToEmail = rsCommon("email")
	blnShowEmail = CBool(rsCommon("Show_email"))
End If

'Clean up
rsCommon.Close


'Get the email of who the email is from
'Initalise the strSQL variable with an SQL statement to query the database
strSQL = "SELECT " & "Usuarios.email "
strSQL = strSQL & "FROM " & "Usuarios "
strSQL = strSQL & "WHERE " & "Usuarios.UsuarioID = " & portal.variablesForum.lngLoggedInUserID

'Query the database
rsCommon=db.execute(strSQL)

'Read in the details from the user
If NOT rsCommon.EOF Then

	strFromEmail = rsCommon("email")
End If

'Clean up
rsCommon.Close


'If this is a post back send the mail
If Request.Form("postBack") Then
	
	'Check the session ID to stop spammers using the email form
	Call checkSessionID(Request.Form("sessionID"))

	'Initilalse the body of the email message
	strEmailBody = portal.variablesForum.strTxtHi & " " & strToUser & ","
	strEmailBody = strEmailBody & vbCrLf & vbCrLf & portal.variablesForum.strTxtTheFollowingEmailHasBeenSentToYouBy & " " & strLoggedInusuario & " " & portal.variablesForum.strTxtFromYourAccountOnThe & " " & strMainForumName & "."
	strEmailBody = strEmailBody & vbCrLf & vbCrLf & portal.variablesForum.strTxtIfThisMessageIsAbusive & ": - "
	strEmailBody = strEmailBody & vbCrLf & vbCrLf & strForumEmailAddress
	strEmailBody = strEmailBody & vbCrLf & vbCrLf & portal.variablesForum.strTxtIncludeThisEmailAndTheFollowing & ": - " & "BBS=" & strMainForumName & ";ID=" & portal.variablesForum.lngLoggedInUserID & ";USR= " & strLoggedInusuario & ";"
	strEmailBody = strEmailBody & vbCrLf & vbCrLf & portal.variablesForum.strTxtReplyToEmailSetTo & " " & strLoggedInusuario & "."
	strEmailBody = strEmailBody & vbCrLf & vbCrLf & portal.variablesForum.strTxtMessageSent & ": -"
	strEmailBody = strEmailBody & vbCrLf & "---------------------------------------------------------------------------------------"
	strEmailBody = strEmailBody & vbCrLf & vbCrLf & Request.Form("message")

	'Inititlaise the subject of the e-mail
	strSubject = Request.Form("subject")

	'Send the e-mail using the Send Mail function created on the send_mail_function.inc file
	portal.variablesForum.blnEmailSent = funcMail.SendMailForo(strEmailBody, strToUser, strToEmail, strLoggedInusuario, strFromEmail, strSubject, strMailComponent, false)

	'If the user wants a copy of the email as well send em one
	If Request.Form("mySelf") Then
		Call funcMail.SendMailForo(strEmailBody, strLoggedInusuario, strFromEmail, strLoggedInusuario, strFromEmail, strSubject, strMailComponent, false)
	End If

End If
%>
<html>
<head>
<title>Email Messenger</title>



     	

<!-- Check the from is filled in correctly before submitting -->
<script language="javascript">

//Function to check form is filled in correctly before submitting
function CheckForm () {

	var errorMsg = "";

	//Check for a subject
	if (document.frmEmailMsg.subject.value==""){
		errorMsg += "\n\t<% = portal.variablesForum.strTxtErrorTopicSubject %>";
	}

	//Check for message
	if (document.frmEmailMsg.message.value==""){
		errorMsg += "\n\t<% = portal.variablesForum.strTxtNoMessageError %>";
	}

	//If there is aproblem with the form then display an error
	if (errorMsg != ""){
		msg = "<% = portal.variablesForum.strTxtErrorDisplayLine %>\n\n";
		msg += "<% = portal.variablesForum.strTxtErrorDisplayLine1 %>\n";
		msg += "<% = portal.variablesForum.strTxtErrorDisplayLine2 %>\n";
		msg += "<% = portal.variablesForum.strTxtErrorDisplayLine %>\n\n";
		msg += "<% = portal.variablesForum.strTxtErrorDisplayLine3 %>\n";

		errorMsg += alert(msg + errorMsg + "\n\n");
		return false;
	}

	return true;
}
</script>

<!-- #include file="includes/header.aspx" -->
<navigation:navigation ID="common1" runat="server" />
<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="3" align="center">
  <tr>
  <td align="left" class="heading"><% = portal.variablesForum.strTxtEmailMessenger %></td>
</tr>
 <tr>
  <td align="left" width="71%" class="bold"><img src="<% = portal.variablesForum.strImagePath %>open_folder_icon.gif" border="0" align="middle">&nbsp;<a href="default.aspx" target="_self" class="boldLink"><% = strMainForumName %></a><% = strNavSpacer %><% = portal.variablesForum.strTxtEmailMessenger %><br /></td>
  </tr>
</table>
 <%
'Clean up
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing

'If the users account is suspended then let them know
If blnActiveMember = False Then
	Response.Write vbCrLf & "<div align=""center""><br /><br /><span class=""text"">" & portal.variablesForum.strTxtForumMemberSuspended & "</span><br /><br /><br /><br /><br /></div>"

'Else if the user is not logged in so let them know to login
ElseIf portal.variablesForum.intGroupID = 2 Then

	Response.Write vbCrLf & "<div align=""center""><br /><br /><span class=""text"">" & portal.variablesForum.strTxtMustBeRegistered & "</span><br /><br />"
	Response.Write vbCrLf & "<a href=""registration_rules.aspx?FID=" & portal.variablesForum.intForumID & """ target=""_self""><img src=""" & portal.variablesForum.strImagePath & "register.gif""  alt=""" & portal.variablesForum.strTxtRegister & """ border=""0"" align=""middle"" /></a>&nbsp;&nbsp;<a href=""login_user.aspx?FID=" & portal.variablesForum.intForumID & """ target=""_self""><img src=""" & portal.variablesForum.strImagePath & "login.gif""  alt=""" & portal.variablesForum.strTxtLogin & """ border=""0"" align=""middle"" /></a><br /><br /><br /><br /></div>"

'If the email has been sent display the appropriate message
ElseIf portal.variablesForum.blnEmailSent Then

	Response.Write vbCrLf & "<div align=""center""><br /><br /><span class=""text"">" & portal.variablesForum.strTxtYourEmailHasBeenSentTo & " " & strToUser & "</span><br /><br /><a href=""default.aspx"" target=""_self"">" & portal.variablesForum.strTxtReturnToDiscussionForum & "</a><br /><br /><br /><br /><br /></div>"

'Else If the to user doesn't have an email address then don't the user can not send to them
ElseIf isNull(strToEmail) Or strToEmail = "" Then

	Response.Write vbCrLf & "<div align=""center""><br /><br /><span class=""text"">" & portal.variablesForum.strTxtYouCanNotEmail & " " & strToUser & ", " & portal.variablesForum.strTxtTheyDontHaveAValidEmailAddr & "</span><br /><br /><br /><br /><br /></div>"

'Else If the current user doesn't have a valid email address in their profile then they can't send an email
ElseIf isNull(strFromEmail) OR strFromEmail = "" Then

	Response.Write vbCrLf & "<div align=""center""><br /><br /><span class=""text"">" & portal.variablesForum.strTxtYouCanNotEmail & " " & strToUser & ", " & portal.variablesForum.strTxtYouDontHaveAValidEmailAddr & "</span><br /><br /><br /><br /><br /></div>"

'Else If the to user has choosen to hide their email address
ElseIf blnShowEmail = False AND portal.variablesForum.blnAdmin = False Then

	Response.Write vbCrLf & "<div align=""center""><br /><br /><span class=""text"">" & portal.variablesForum.strTxtYouCanNotEmail & " " & strToUser & ", " & portal.variablesForum.strTxtTheyHaveChoosenToHideThierEmailAddr & "</span><br /><br /><br /><br /><br /></div>"

'Else show the form so the person can be emailed
Else

%><br />
<form method="post" name="frmEmailMsg" action="email_messenger.aspx" onSubmit="return CheckForm();" onReset="return confirm('<% = strResetFormConfirm %>');">
 <table width="610" border="0" cellspacing="0" cellpadding="1" bgcolor="<% = portal.variablesForum.strTableBorderColour %>" height="230" align="center">
  <tr>
   <td height="66" width="967">
    <table width="610" border="0" cellspacing="0" cellpadding="0" align="center" bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>" height="201">
     <tr>
      <td height="199">
       <table width="610" border="0" align="center" cellpadding="2" cellspacing="0">
        <tr align="left">
         <td colspan="2" height="31" class="text">*<% = portal.variablesForum.strTxtRequiredFields %></td>
        </tr>
        <tr>
         <td align="right" width="15%" class="text"><% = portal.variablesForum.strTxtRecipient %>:</td>
         <td width="70%" class="bold"><% = strToUser %></td>
        </tr>
        <tr>
         <td align="right" width="15%" class="text"><% = portal.variablesForum.strTxtSubjectFolder %>*:</td>
         <td width="70%"> <input type='text' name="subject" size="30" maxlength="41"></td>
        </tr>
        <tr>
         <td valign="top" align="right" width="15%" class="text"><% = portal.variablesForum.strTxtMessage %>*:<br /><br />
         <span class="smText"><% = portal.variablesForum.strTxtNoHTMLorForumCodeInEmailBody %></span></td>
         <td width="70%" valign="top"><textarea name="message" cols="57" rows="12"></textarea>
         </td>
         <tr>
         <td align="right" width="15%" class="text">&nbsp;</td>
         <td width="70%" class="text">&nbsp;<input type='checkbox' name="mySelf" value="True"><% = portal.variablesForum.strTxtSendACopyOfThisEmailToMyself %></td>
        </tr>
        </tr>
         <td><input name="SEID" type="hidden" id="to" value="<% = lngToUserID %>"><input name="postBack" type="hidden" id="postBack" value="true">&nbsp;</td>
        <td width="70%" align="left">
           <input type="hidden" name="sessionID" value="<% = Session.SessionID %>" />
	   <input type='submit' name="Submit" value="<% = portal.variablesForum.strTxtSendEmail %>">
           <input type="reset" name="Reset" value="<% = portal.variablesForum.strTxtResetForm %>">
        </td>
        </tr>
       </table>
      </td>
     </tr>
    </table>
   </td>
  </tr>
 </table>
</form><%

End If
%>
<br />
<div align="center">
<%


'Display the process time
If blnShowProcessTime Then response.write("<span class=""smText""><br /><br />" & portal.variablesForum.strTxtThisPageWasGeneratedIn & " " & FormatNumber(Timer() - dblStartTime, 4) & " " & portal.variablesForum.strTxtSeconds & "</span>"
%>

