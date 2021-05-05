

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


'If the user is user is using a banned IP redirect to an error page
If bannedIP() Then
	
	'Clean up
	Set rsCommon = Nothing
	adoCon.Close
	Set adoCon = Nothing
	
	'Redirect
	Response.Redirect("insufficient_permission.aspx?M=IP")

End If


'If the user has not logged in then  or the page has not been passed with a topic id the redirect to the forum start page
If Request.QueryString("TID") = "" OR portal.variablesForum.blnEmail = False OR portal.variablesForum.intGroupID = 2 Then 
	'Clean up
	Set rsCommon = Nothing
	adoCon.Close
	Set adoCon = Nothing
	
	'Redirect
	response.redirect("default.aspx"
End If


'Initilise the message in the form
strFormMessage = portal.variablesForum.strTxtEmailFriendMessage & " " & strMainForumName & " " & portal.variablesForum.strTxtAt & ": -"
strFormMessage = strFormMessage & vbCrLf & vbCrLf & strForumPath & "/forum_posts.aspx?TID=" & CLng(Request.QueryString("TID"))
strFormMessage = strFormMessage & vbCrLf & vbCrLf & portal.variablesForum.strTxtRegards & "," & vbCrLf & strLoggedInusuario & vbCrLf


'Read in the users e-mail address

'Initalise the strSQL variable with an SQL statement to query the database get the thread details
strSQL = "SELECT " & "Usuarios.nombre, " & "Usuarios.email "
strSQL = strSQL & "FROM " & "Usuarios "
strSQL = strSQL & "WHERE (((" & "Usuarios.UsuarioID)=" & portal.variablesForum.lngLoggedInUserID & "));"

'Query the database
rsCommon=db.execute(strSQL)

'If there is an e-mail address for the user then read it in
If NOT rsCommon.EOF Then
	'Read in Usuarioss detals from the database
	strUsuariosEmail = rsCommon("email")
	strRealName = rsCommon("nombre")
End If


'If the form has been filled in then send the form
If NOT Request.Form("ToName") = "" AND NOT Request.Form("ToEmail") = "" AND NOT Request.Form("FromName") = "" AND NOT Request.Form("FromEmail") = "" AND NOT Request.Form("message") = "" Then

	'Check the session ID to stop spammers using the email form
	Call checkSessionID(Request.Form("sessionID"))

	'Initilalse the body of the email message
	strEmailBody = portal.variablesForum.strTxtHi & " " & Request.Form("ToName") & ","
	strEmailBody = strEmailBody & vbCrLf & vbCrLf & portal.variablesForum.strTxtTheFollowingEmailHasBeenSentToYouBy & " " & Request.Form("FromName") & " " & portal.variablesForum.strTxtFrom & " " & strMainForumName & "."
	strEmailBody = strEmailBody & vbCrLf & vbCrLf & portal.variablesForum.strTxtIfThisMessageIsAbusive & ": - "
	strEmailBody = strEmailBody & vbCrLf & vbCrLf & strForumEmailAddress
	strEmailBody = strEmailBody & vbCrLf & vbCrLf & portal.variablesForum.strTxtIncludeThisEmailAndTheFollowing & ": - " & "BBS=" & strMainForumName & ";ID=" & portal.variablesForum.lngLoggedInUserID & ";USR= " & strLoggedInusuario & ";"
	strEmailBody = strEmailBody & vbCrLf & vbCrLf & portal.variablesForum.strTxtReplyToEmailSetTo & " " & Request.Form("FromName") & "."
	strEmailBody = strEmailBody & vbCrLf & vbCrLf & portal.variablesForum.strTxtMessageSent & ": -"
	strEmailBody = strEmailBody & vbCrLf & "---------------------------------------------------------------------------------------"
	strEmailBody = strEmailBody & vbCrLf & vbCrLf & portal.variablesForum.strTxtHi & " " & Request.Form("ToName")
	strEmailBody = strEmailBody & vbCrLf & vbCrLf & Request.Form("message")

	'Inititlaise the subject of the e-mail
	strSubject = portal.variablesForum.strTxtInterestingForumPostOn & " " & strWebsiteName

	'Send the e-mail using the Send Mail function created on the send_mail_function.inc file
	blnSentEmail = funcMail.SendMailForo(strEmailBody, Request.Form("ToName"), Request.Form("ToEmail"), Request.Form("FromName"), Request.Form("FromEmail"), strSubject, strMailComponent, false)
End If

'Reset server objects
rsCommon.Close
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing
%>
<html>
<head>

<title>Email Topic To a Friend</title>


     	

<!-- Check the from is filled in correctly before submitting -->
<script  language="javascript">
<!-- Hide from older browsem_rs...

//Function to check form is filled in correctly before submitting
function CheckForm() {

	var errorMsg = "";

	//Check for a Friends Name
	if (document.frmEmailTopic.ToName.value == ""){
		errorMsg += "\n\t<% = portal.variablesForum.strTxtErrorFrinedsName %>";
	}

	//Check that the friends e-mail and it is valid address is valid
	if ((document.frmEmailTopic.ToEmail.value=="") || (document.frmEmailTopic.ToEmail.value.length > 0 && (document.frmEmailTopic.ToEmail.value.indexOf("@",0) == - 1 || document.frmEmailTopic.ToEmail.value.indexOf(".",0) == - 1))) {
		errorMsg += "\n\t<% = portal.variablesForum.strTxtErrorFriendsEmail %>";
	}

	//Check for a Users Name
	if (document.frmEmailTopic.FromName.value==""){
		errorMsg += "\n\t<% = portal.variablesForum.strTxtErrorYourName %>";
	}

	//Check for the users e-mail address and it is valid
	if ((document.frmEmailTopic.FromEmail.value=="") || (document.frmEmailTopic.FromEmail.value.length > 0 && (document.frmEmailTopic.FromEmail.value.indexOf("@",0) == - 1 || document.frmEmailTopic.FromEmail.value.indexOf(".",0) == - 1))) {
		errorMsg += "\n\t<% = portal.variablesForum.strTxtErrorYourEmail %>";
	}

	//Check for a Message
	if (document.frmEmailTopic.message.value==""){
		errorMsg += "\n\t<% = portal.variablesForum.strTxtErrorEmailMessage %>";
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
// -->
</script>
<!--#include file="includes/skin_file.aspx" -->
</head>
<body bgcolor="<% = strBgColour %>" text="<% = strTextColour %>" background="<% = strBgImage %>" marginheight="0" marginwidth="0" topmargin="0" leftmargin="0" OnLoad="self.focus();">
<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="1" align="center">
 <tr>
  <td align="center"><span class="heading"><% = portal.variablesForum.strTxtEmailTopicToFriend %></span></td>
 </tr>
</table><%

'If the email has been sent then display a message saying
If blnSentEmail = True Then
%>
<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="1" align="center">
 <tr>
  <td align="center" height="79" class="text"><% = portal.variablesForum.strTxtFriendSentEmail %></td>
 </tr>
</table>
<%
'Else the e-mail has not been sent so display the form
Else
%><br />
<form name="frmEmailTopic" method="post" action="email_topic.aspx?FID=<% = CInt(Request.QueryString("FID")) %>&amp;tID=<% = CLng(Request.QueryString("TID")) %>" onSubmit="return CheckForm();" onReset="return confirm('<% = strResetFormConfirm %>');">
 <table width="350" border="0" cellspacing="0" cellpadding="1" bgcolor="<% = portal.variablesForum.strTableBorderColour %>" align="center">
 <tr>
  <td>
  <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="<% = portal.variablesForum.strTableBgColour %>">
    <tr>
     <td bgcolor="<% = portal.variablesForum.strTableBgColour %>">
   <table width="100%" border="0" cellspacing="0" cellpadding="0" bgcolor="<% = portal.variablesForum.strTableBgColour %>">
    <tr>
   <td height="174">
    <table border="0" align="center" cellpadding="4" cellspacing="1" width="350">
     <tr align="left" bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableTitleBgImage %>">
      <td colspan="2" bgcolor="<% = portal.variablesForum.strTableTitleColour %>" background="<% = portal.variablesForum.strTableTitleBgImage %>" class="text">*<% = portal.variablesForum.strTxtRequiredFields %></td>
     </tr>
     <tr  bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
      <td align="left" width="115" class="text" background="<% = portal.variablesForum.strTableBgImage %>"><% = portal.variablesForum.strTxtusuario %></td>
      <td align="left" width="234" background="<% = portal.variablesForum.strTableBgImage %>" class="text"><% = strLoggedInusuario %></td>
     </tr>
     <tr  bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
      <td align="left" width="115" class="text" background="<% = portal.variablesForum.strTableBgImage %>"><% = portal.variablesForum.strTxtYourName %>*</td>
      <td align="left" width="234" background="<% = portal.variablesForum.strTableBgImage %>">
       <input type='text' name="FromName" size="20" maxlength="20" value="<% = strRealName %>" onFocus="FromName.value = ''" />
      </td>
     </tr>
     <tr  bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
      <td align="left" width="115" class="text" background="<% = portal.variablesForum.strTableBgImage %>"><% = portal.variablesForum.strTxtYourEmail %>*</td>
      <td align="left" width="234" background="<% = portal.variablesForum.strTableBgImage %>">
       <input type='text' name="FromEmail" size="20" maxlength="50" value="<% = strUsuariosEmail %>" onFocus="FromEmail.value = ''" />
      </td>
     </tr>
     <tr  bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
      <td align="left" width="115" bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>" class="text"><% = portal.variablesForum.strTxtFriendsName %>*</td>
      <td align="left" bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>" width="234" nowrap>
       <input type='text' name="ToName" size="20" maxlength="20" />
     </tr>
     <tr  bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
      <td align="left" width="115" class="text" background="<% = portal.variablesForum.strTableBgImage %>"><% = portal.variablesForum.strTxtFriendsEmail %>*</td>
      <td align="left" width="234" background="<% = portal.variablesForum.strTableBgImage %>">
       <input type='text' name="ToEmail" size="20" maxlength="50">
      </td>
     </tr>
     <tr  bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
      <td align="left" colspan="2" background="<% = portal.variablesForum.strTableBgImage %>"><span class="text"><% = portal.variablesForum.strTxtMessage %>:</span><br />
       <textarea name="message" cols="40" rows="4" wrap="OFF"><% = strFormMessage %></textarea>
      </td>
     </tr>
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>" align="center">
      <td valign="top" colspan="2" background="<% = portal.variablesForum.strTableBgImage %>">
       <p>
        <input type="hidden" name="sessionID" value="<% = Session.SessionID %>" />
        <input type='submit' name="Submit" value="<% = portal.variablesForum.strTxtSendEmail %>" />
        <input type="reset" name="Reset" value="<% = portal.variablesForum.strTxtResetForm %>" />
       </p>
      </td>
     </tr>
    </table>
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
<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="1" align="center">
 <tr>
  <td align="center" height="34"><a href="JavaScript:onClick=window.close()"><% = portal.variablesForum.strTxtCloseWindow %></a>
  <br /><br />
<%

%>
  </td>
 </tr>
</table>
</body>
</html>