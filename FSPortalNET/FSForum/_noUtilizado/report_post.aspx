

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<!--#include virtual="/fsportalnet/includes/funcionesMail.aspx" -->
<%
'Set the response buffer to true as we maybe redirecting
Response.Buffer = True


'Dimension variables
Dim lngPostID		'Holds the post ID number
Dim lngTopicID		'Holds the topic ID number
Dim intTopicPageNum	'Holds the topic page number
Dim intForumID		'Holds the forum ID
Dim strPostedMessage	'Holds the posted message



Dim lngToUserID		'Holds the user ID of who the email is to
Dim strToUser		'Holds the user name of the person the email is to
Dim blnShowEmail	'set to true if the user allws emailing to them
Dim strToEmail		'Holds the email address of who the email is to
Dim strFromEmail	'Holds the email address of who the email is from
Dim blnEmailSent	'Set to true if the email has been sent
Dim strEmailBody
Dim strSubject
Dim strMessagePoster


'Read in the details
lngPostID = CLng(Request("PID"))
lngTopicID = CLng(Request("TID"))
intTopicPageNum = CInt(Request("TPN"))
portal.variablesForum.intForumID = CInt(Request("FID"))


'If there is no recopinet for the email then send em to homepage
If lngPostID = 0 OR lngTopicID = 0 OR portal.variablesForum.blnEmail = False Then
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


'If this is a post back send the mail
If Request.Form("postBack") Then


	'Check the session ID to stop spammers using the email form
	Call checkSessionID(Request.Form("sessionID"))



	'Get the post to send with the email
	'Initalise the strSQL variable with an SQL statement to query the database
	strSQL = "SELECT " & "Usuarios.usuario, " & portal.variablesForum.strDbTable & "Thread.Message "
	strSQL = strSQL & "FROM " & "Usuarios, " & portal.variablesForum.strDbTable & "Thread  "
	strSQL = strSQL & "WHERE " & "Usuarios.UsuarioID = " & portal.variablesForum.strDbTable & "Thread.UsuarioID AND " & portal.variablesForum.strDbTable & "Thread.Thread_ID=" & lngPostID & ";"

	'Query the database
	rsCommon=db.execute(strSQL)

	'Read in the message to be sent with the report
	If NOT rsCommon.EOF Then
		strPostedMessage = rsCommon("Message")
		strMessagePoster = rsCommon("usuario")

		'Change	the path to the	emotion	symbols	to include the path to the images
		strPostedMessage = Replace(strPostedMessage, "src=""smileys/smiley", "src=""" & strForumPath & "/smileys/smiley", 1, -1, 1)
	End If

	'Clean up
	rsCommon.Close



	'Inititlaise the subject of the e-mail
	strSubject = portal.variablesForum.strTxtIssueWithPostOn & " " & strMainForumName

	'Initilalse the body of the email message
	strEmailBody = portal.variablesForum.strTxtHi & ","
	strEmailBody = strEmailBody & "<br /><br />" & portal.variablesForum.strTxtTheFollowingReportSubmittedBy & " " & decodeString(strLoggedInusuario) & ", " & portal.variablesForum.strTxtOn & " " & strMainForumName & " " & portal.variablesForum.strTxtWhoHasTheFollowingIssue & " : -"
	strEmailBody = strEmailBody & "<br /><br /><hr />"
	strEmailBody = strEmailBody & "<br />" & Replace(Request.Form("report"), vbCrLf, "<br />",	1, -1, 1) & "<br /><br />"
	strEmailBody = strEmailBody & "<hr />"
	strEmailBody = strEmailBody & "<br />" & portal.variablesForum.strTxtToViewThePostClickTheLink & " : -"
	strEmailBody = strEmailBody & "<br /><a href=""" & strForumPath & "/forum_posts.aspx?TID=" & lngTopicID & "&amp;tPN=" & intTopicPageNum & """>" & strForumPath & "/forum_posts.aspx?TID=" & lngTopicID & "&amp;tPN=" & intTopicPageNum & "</a>"
	strEmailBody = strEmailBody & "<br /><br /><hr /><br /><b>" & portal.variablesForum.strTxtPostedBy & ":</b> " & strMessagePoster & "<br /><br />"
	strEmailBody = strEmailBody & strPostedMessage



	'Get the email address of the boards admins to send the email to
	'Initalise the strSQL variable with an SQL statement to query the database
	strSQL = "SELECT " & "Usuarios.usuario, " & "Usuarios.email "
	strSQL = strSQL & "FROM " & "Usuarios  "
	strSQL = strSQL & "WHERE " & "Usuarios.Group_ID=1 AND " & "Usuarios.email <> '';"


	'Query the database
	rsCommon=db.execute(strSQL)
	
	'Send an email to the forum email address if there are no email addresses of admins in the database
	If rsCommon.EOF Then portal.variablesForum.blnEmailSent = funcMail.SendMailForo(strEmailBody, portal.variablesForum.strTxtForumAdministrator, strForumEmailAddress, strLoggedInusuario, strForumEmailAddress, strSubject, strMailComponent, true)
	
	'If there are email addresses returned send email to the forum admins
	Do while not rsCommon.EOF

		'Send the e-mail using the Send Mail function created on the send_mail_function.inc file
		portal.variablesForum.blnEmailSent = funcMail.SendMailForo(strEmailBody, rsCommon("usuario"), rsCommon("email"), strLoggedInusuario, strForumEmailAddress, strSubject, strMailComponent, true)

		'Move to next record
		rsCommon.MoveNext
	Loop

	'Clean up
	rsCommon.Close




	'Get the email address of the moderators to send the email to
	'Initalise the strSQL variable with an SQL statement to query the database
	strSQL = "SELECT " & "Usuarios.usuario, " & "Usuarios.email "
	strSQL = strSQL & "FROM " & portal.variablesForum.strDbTable & "Permissions, " & "Usuarios  "
	If portal.variablesForum.strDatabaseType = "SQLServer" Then
		strSQL = strSQL & "WHERE " & portal.variablesForum.strDbTable & "Permissions.Group_ID = " & "Usuarios.Group_ID AND (" & portal.variablesForum.strDbTable & "Permissions.Forum_ID=" & portal.variablesForum.intForumID & " AND " & portal.variablesForum.strDbTable & "Permissions.Moderate=1) AND " & "Usuarios.email <> '';"
	Else
		strSQL = strSQL & "WHERE " & portal.variablesForum.strDbTable & "Permissions.Group_ID = " & "Usuarios.Group_ID AND (" & portal.variablesForum.strDbTable & "Permissions.Forum_ID=" & portal.variablesForum.intForumID & " AND " & portal.variablesForum.strDbTable & "Permissions.Moderate=True) AND " & "Usuarios.email <> '';"
	End If

	'Query the database
	rsCommon=db.execute(strSQL)

	'Send an email to the moderators
	Do while not rsCommon.EOF

		'Send the e-mail using the Send Mail function created on the send_mail_function.inc file
		portal.variablesForum.blnEmailSent = funcMail.SendMailForo(strEmailBody, rsCommon("usuario"), rsCommon("email"), strLoggedInusuario, strForumEmailAddress, strSubject, strMailComponent, true)

		'Move to next record
		rsCommon.MoveNext
	Loop

	'Clean up
	rsCommon.Close
End If
%>
<html>
<head>
<title>Report Post</title>



     	

<!-- #include file="includes/header.aspx" -->
<navigation:navigation ID="common1" runat="server" />
<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="3" align="center">
  <tr>
  <td align="left" class="heading"><% = portal.variablesForum.strTxtReportPost %></td>
</tr>
 <tr>
  <td align="left" width="71%" class="bold"><img src="<% = portal.variablesForum.strImagePath %>open_folder_icon.gif" border="0" align="middle">&nbsp;<a href="default.aspx" target="_self" class="boldLink"><% = strMainForumName %></a><% = strNavSpacer %><% = portal.variablesForum.strTxtReportPost %><br /></td>
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

	Response.Write vbCrLf & "<div align=""center""><br /><br /><span class=""text"">" & portal.variablesForum.strTxtYourReportEmailHasBeenSent & "</span><br /><br /><a href=""default.aspx"" target=""_self"">" & portal.variablesForum.strTxtReturnToDiscussionForum & "</a><br /><br /><br /><br /><br /></div>"

'Else show the form so the person can be emailed
Else

%><form method="post" name="frmReport" action="report_post.aspx" onReset="return confirm('<% = strResetFormConfirm %>');">
 <table width="600" border="0" cellspacing="0" cellpadding="1" bgcolor="<% = portal.variablesForum.strTableBorderColour %>" height="230" align="center">
  <tr>
   <td height="66" width="967">
    <table width="600" border="0" cellspacing="0" cellpadding="0" align="center" bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>" height="201">
     <tr>
      <td>
       <table width="600" border="0" align="center" cellpadding="4" cellspacing="0">
        <tr>
         <td colspan="2" class="text"><% = portal.variablesForum.strTxtPleaseStateProblemWithPost %><br /><br /></td>
        </tr>
         <td valign="top" align="right" width="20%" class="text"><% = portal.variablesForum.strTxtProblemWithPost %>*:</td>
         <td width="65%" valign="top"><textarea name="report" cols="45" rows="9"></textarea>
         </td>
        </tr>
         <td><input name="PID" type="hidden" id="PID" value="<% = lngPostID %>">
             <input name="FID" type="hidden" id="FID" value="<% = portal.variablesForum.intForumID %>">
             <input name="TID" type="hidden" id="TID" value="<% = lngTopicID %>">
             <input name="TPN" type="hidden" id="TPN" value="<% = intTopicPageNum %>">
             <input name="postBack" id="postBack" type="hidden" value="true">&nbsp;</td>
        <td width="65%" align="left">
           <input type="hidden" name="sessionID" value="<% = Session.SessionID %>" />
	   <input type='submit' name="Submit" value="<% = portal.variablesForum.strTxtSendReport %>">
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

