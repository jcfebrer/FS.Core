

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<!--#include file="language_files/pm_language_file_inc.aspx" -->
<!--#include file="functions/functions_format_post.aspx" -->
<!--#include file="includes/emoticons_inc.aspx" -->
<!--#include virtual="/fsportalnet/includes/funcionesMail.aspx" -->
<%
'Set the buffer to true
Response.Buffer = True

'Declare variables
Dim strTousuario	'Holds the usuario the pm message is sent to
Dim lngToUserID		'Holds Usuarios id of the person who the pm is for
Dim strSubject		'Holds the subject of the pm
Dim strMessage		'Holds the pm
Dim blnReadEmailNotify	'Holds if the user is to be notified when the message is read
Dim blnTousuarioOK	'Set to false if the to usuario is not found
Dim blnMaxPMsOK		'Set to false if the max number of private messages is exceeded
Dim blnMessageSent	'Set to true if the message is already sent
Dim strEmailSubject	'Holds the subject of the e-mail
Dim strEmailBody	'Holds the body of the e-mail message
Dim blnEmailSent	'set to true if an e-mail is sent
Dim blnBlocked		'Set to true if the user is blocked from messaging this person
Dim blnNoSubject	'Set to true if there is no subject to the PM
Dim intForumID		'Holds the forum ID
Dim strToEmail		'To email address
Dim blnPMNotify		'Set to true if the user wants notifying by emial



'Initilaise varaibles
blnTousuarioOK = False
blnMaxPMsOK = False
blnMessageSent = False
blnBlocked = False
blnNoSubject = False



'If the user is user is using a banned IP redirect to an error page
If bannedIP() Then
	'Clean up
	Set rsCommon = Nothing
	adoCon.Close
	Set adoCon = Nothing

	'Redirect
	Response.Redirect("insufficient_permission.aspx?M=IP")

End If


'If Priavte messages are not on then send them away
If blnPrivateMessages = False Then
	'Clean up
	Set rsCommon = Nothing
	adoCon.Close
	Set adoCon = Nothing

	'Redirect
	Response.Redirect("default.aspx")
End If


'If the user is not allowed then send them away
If portal.variablesForum.intGroupID = 2 OR blnActiveMember = False Then
	'Clean up
	Set rsCommon = Nothing
	adoCon.Close
	Set adoCon = Nothing

	'Redirect
	Response.Redirect("insufficient_permission.aspx")
End If


'Read in the details for the pm
strSubject = Trim(Mid(Request.Form("subject"), 1, 41))
strMessage = Request.Form("Message")
portal.variablesForum.blnReadEmailNotify = CBool(Request.Form("email"))
strTousuario = Trim(Mid(Request.Form("member"), 1, 15))


'If the buddy text box is empty then read in the buddy from the list box
If strTousuario = "" Then strTousuario = Trim(Mid(Request.Form("selectMember"), 1, 15))

'Take out parts of the usuario that are not permitted
strTousuario = disallowedMemberNames(strTousuario)

'Run the to usuario through the same SQL filer it was created under otherwise it may not match
strTousuario = formatSQLInput(strTousuario)

'If there is no subject or message then don't post the message as won't be able to link to it
If strSubject = "" OR strMessage = "" Then blnNoSubject = True



'Check that the user the pm is being sent to exisits

'Initalise the SQL string with a query to read in the dteails of who the PM is to
strSQL = "SELECT " & "Usuarios.UsuarioID, " & "Usuarios.usuario, " & "Usuarios.email, " & "Usuarios.PM_notify "
strSQL = strSQL & "FROM " & "Usuarios "
strSQL = strSQL & "WHERE " & "Usuarios.usuario = '" & strTousuario & "';"

'Open the recordset
rsCommon=db.execute(strSQL)

'If the to buddy is found in the database run the rest of the code
If NOT rsCommon.EOF Then

	'usuario found so set to true
	blnTousuarioOK = True

	'Get details of who the PM is to
	lngToUserID = CLng(rsCommon("UsuarioID"))
	strToEmail = rsCommon("email")
	blnPMNotify = CBool(rsCommon("PM_notify"))


	'Don't let user send private message to guest account
	If (lngToUserID = 2 OR portal.variablesForum.intGroupID = 2) Then blnBlocked = True

	'Close the recordset
	rsCommon.Close



	'Check the user is not blocked from messaging this person

	'Initalise the SQL string with a query to read count the number of pm's the user has recieved
	strSQL = "SELECT " & portal.variablesForum.strDbTable & "BuddyList.Buddy_ID FROM " & portal.variablesForum.strDbTable & "BuddyList "
	If portal.variablesForum.strDatabaseType = "SQLServer" Then
		strSQL = strSQL & "WHERE " & portal.variablesForum.strDbTable & "BuddyList.Block = 1 AND " & portal.variablesForum.strDbTable & "BuddyList.Buddy_ID = " & portal.variablesForum.lngLoggedInUserID & " AND " & portal.variablesForum.strDbTable & "BuddyList.UsuarioID = " & lngToUserID & ";"
	Else
		strSQL = strSQL & "WHERE " & portal.variablesForum.strDbTable & "BuddyList.Block = True AND " & portal.variablesForum.strDbTable & "BuddyList.Buddy_ID = " & portal.variablesForum.lngLoggedInUserID & " AND " & portal.variablesForum.strDbTable & "BuddyList.UsuarioID = " & lngToUserID & ";"
	End If

	'Open the recordset
	rsCommon=db.execute(strSQL)

	'If a record is returned then this user is blocked from messaging this person so don't send the pm, unless this is the forum admin
	If NOT rsCommon.EOF AND portal.variablesForum.blnAdmin = False Then blnBlocked = True

	'Clean up
	rsCommon.Close




	'Check the user has not exceeded there allowed amount of private messages

	'Initalise the SQL string with a query to read count the number of pm's the user has recieved
	strSQL = "SELECT Count(" & portal.variablesForum.strDbTable & "PMMessage.PM_ID) AS CountOfPM FROM " & portal.variablesForum.strDbTable & "PMMessage "
	strSQL = strSQL & "GROUP BY " & portal.variablesForum.strDbTable & "PMMessage.UsuarioID "
	strSQL = strSQL & "HAVING " & portal.variablesForum.strDbTable & "PMMessage.UsuarioID=" & lngToUserID & ";"

	'Open the recordset
	rsCommon=db.execute(strSQL)

	'If there are records returned and the num of pm's is less than max alloed set blnMaxPMsOK to true
	If NOT rsCommon.EOF Then
		If (CInt(rsCommon("CountOfPM")) < intNumPrivateMessages) OR portal.variablesForum.lngLoggedInUserID = 1 OR lngToUserID = 1 Then blnMaxPMsOK = True
	'Else if no records returened they have no pm's set set blnMaxPMsOK to true anyway (it's intilised to false at the top)
	Else
		blnMaxPMsOK = True
	End If

	'Relese sever objects
	rsCommon.Close
Else

	'Clean up
	rsCommon.Close
End If




'If the user to send to is found and they don't exceed max num of pm's (unless the sender is admin) then send the pm
If blnTousuarioOK AND blnMaxPMsOK AND blnBlocked = False AND blnNoSubject = False Then

	'Place format posts posted with the WYSIWYG Editor
	If Request.Form("browser") = "RTE" Then

		'Call the function to format WYSIWYG posts
		strMessage = WYSIWYGFormatPost(strMessage)

	'Else standrd editor is used so convert forum codes
	Else
		'Call the function to format posts
		strMessage = FormatPost(strMessage)
	End If


	'If the user wants forum codes enabled then format the post using them
	If Request.Form("forumCodes") Then strMessage = FormatForumCodes(strMessage)

	'Check the message for malicious HTML code
	strMessage = checkHTML(strMessage)
	
	'Strip long text strings from message
	strMessage = removeLongText(strMessage)

	'Get rid of scripting tags in the subject
	strSubject = removeAllTags(strSubject)
	strSubject = func.formatInput(strSubject)



	'Replace swear words with other words with ***
	'Initalise the SQL string with a query to read in all the words from the smut table
	strSQL = "SELECT " & portal.variablesForum.strDbTable & "Smut.* FROM " & portal.variablesForum.strDbTable & "Smut"

	'Open the recordset
	rsCommon=db.execute(strSQL)

	'Loop through all the words to check for
	Do While NOT rsCommon.EOF

		'Replace the swear words with the words in the database the swear words
		strMessage = Replace(strMessage, rsCommon("Smut"), rsCommon("Word_replace"), 1, -1, 1)
		strSubject = Replace(strSubject, rsCommon("Smut"), rsCommon("Word_replace"), 1, -1, 1)

		'Move to the next word in the recordset
		rsCommon.MoveNext
	Loop

	'Release server objects
	rsCommon.Close




	'Send the private message
	'Initalise the SQL string with a query to read in all the words from the smut table
	strSQL = "SELECT TOP 1 " & portal.variablesForum.strDbTable & "PMMessage.* FROM " & portal.variablesForum.strDbTable & "PMMessage WHERE " & portal.variablesForum.strDbTable & "PMMessage.UsuarioID = " & lngToUserID & " ORDER BY " & portal.variablesForum.strDbTable & "PMMessage.PM_Message_Date DESC;"

	With rsCommon
		'Set the cursor type property of the record set to Dynamic so we can navigate through the record set
		.CursorType = 2

		'Set the Lock Type for the records so that the record set is only locked when it is updated
		.LockType = 3

		'Open the recordset
		=db.execute(strSQL)

		'Check to make sure the message is not already sent to the user
		If NOT .EOF Then
			If strMessage = rsCommon("PM_Message") Then blnMessageSent = True
		End IF

		'Save the pm
		If blnMessageSent = False Then

			'Add new record to recordset
			.AddNew
			.Fields("UsuarioID") = lngToUserID
			.Fields("From_ID") = portal.variablesForum.lngLoggedInUserID
			.Fields("PM_Tittle") = strSubject
			.Fields("PM_Message") = strMessage
			.Fields("PM_Message_Date") = Now()
			'Check to see if they want e-mail notification of read pm
			If portal.variablesForum.blnLoggedInUserEmail = True AND portal.variablesForum.blnReadEmailNotify = True Then
				.Fields("Email_notify") = 1
			Else
				.Fields("Email_notify") = 0
			End If
			.Update
		End If

		'Clean up
		.Close
	End With


	'If the person has requested an email sent to them notifying them of the PM then send it
	If portal.variablesForum.blnEmail AND blnPMNotify AND strToEmail <> "" Then

		'Set the subject
		strEmailSubject = strMainForumName & " " & portal.variablesForum.strTxtNotificationPM

		'Initailise the e-mail body variable with the body of the e-mail
		strEmailBody = portal.variablesForum.strTxtHi & " " & decodeString(strTousuario) & ","
		strEmailBody = strEmailBody & vbCrLf & vbCrLf & portal.variablesForum.strTxtThisIsToNotifyYouThat & " " & strLoggedInusuario & " " & portal.variablesForum.strTxtHasSentYouPM & ", " & decodeString(strSubject) & ", " & portal.variablesForum.strTxtOn & " " & strMainForumName & "."
		strEmailBody = strEmailBody & vbCrLf & vbCrLf & portal.variablesForum.strTxtToViewThePrivateMessage & " " & portal.variablesForum.strTxtForumClickOnTheLinkBelow & ": -"
                strEmailBody = strEmailBody & vbCrLf & vbCrLf & strForumPath & "/pm_inbox.aspx"

		'Call the function to send the e-mail
		portal.variablesForum.blnEmailSent = funcMail.SendMailForo(strEmailBody, decodeString(strTousuario), decodeString(strToEmail), strMainForumName, decodeString(strForumEmailAddress), strEmailSubject, strMailComponent, false)
	End If
End If
%>
<html>
<head>

<title>Private Messenger : Send New Message</title>


     	

<!-- #include file="includes/header.aspx" -->
<navigation:navigation ID="common1" runat="server" />
  <table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="3" align="center">
 <tr>
  <td align="left" class="heading"><% = portal.variablesForum.strTxtPrivateMessenger %></td>
</tr>
 <tr>
  <td align="left" width="71%" class="bold"><img src="<% = portal.variablesForum.strImagePath %>open_folder_icon.gif" border="0" align="middle">&nbsp;<a href="default.aspx" target="_self" class="boldLink"><% = strMainForumName %></a><% = strNavSpacer %><% = portal.variablesForum.strTxtPrivateMessenger %><br /></td>
  </tr>
</table>
<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="4" align="center">
 <tr>
  <td width="60%"><span class="lgText"><img src="<% = portal.variablesForum.strImagePath %>subject_folder.gif" width="26" height="26" alt="<% = portal.variablesForum.strTxtSubjectFolder %>" align="middle"> <% = portal.variablesForum.strTxtPrivateMessenger & ": " & portal.variablesForum.strTxtSendNewMessage %></span></td>
  <td align="right" width="40%" nowrap="nowrap"><a href="pm_inbox.aspx" target="_self"><img src="<% = portal.variablesForum.strImagePath %>inbox.gif" alt="<% = portal.variablesForum.strTxtPrivateMessenger & " " & portal.variablesForum.strTxtInbox %>" border="0"></a><a href="pm_outbox.aspx" target="_self"><img src="<% = portal.variablesForum.strImagePath %>outbox.gif" alt="<% = portal.variablesForum.strTxtPrivateMessenger & " " & portal.variablesForum.strTxtOutbox %>" border="0"></a><a href="pm_buddy_list.aspx" target="_self"><img src="<% = portal.variablesForum.strImagePath %>buddy_list.gif" alt="<% = portal.variablesForum.strTxtPrivateMessenger & " " & portal.variablesForum.strTxtBuddyList %>" border="0"></a><a href="pm_new_message_form.aspx" target="_self"><img src="<% = portal.variablesForum.strImagePath %>new_private_message.gif" alt="<% = portal.variablesForum.strTxtNewPrivateMessage %>" border="0"></a></td>
 </tr>
</table>
<div align="center"><br />
 <br />
 <br />
 <span class="text">
 <form method="post" name="frmEditMessage" action="pm_new_message_form.aspx?code=edit" onSubmit="return CheckForm();" onReset="return ResetForm();"><%

'Display message to user
If blnTousuarioOK = False Then

	'Display an error message
	Response.Write("<span class=""lgText"">" & portal.variablesForum.strTxtYourPrivateMessage & " &amp;quot;" & strSubject & "&amp;quot;, " & portal.variablesForum.strTxtHasNotBeenSent & "</span>")
	Response.Write("<br /><br />" & portal.variablesForum.strTxtTheusuarioCannotBeFound)
	Response.Write("<br /><br /><a href=""javascript:document.frmEditMessage.submit();"">" & portal.variablesForum.strTxtAmendYourPrivateMessage & "</a>")

	'Save the pm details so they can be edited
	Response.Write(vbCrLf & "<input type=""hidden"" name=""Subject"" value=""" & strSubject & """>")
	Response.Write(vbCrLf & "<input type=""hidden"" name=""Buddy"" value=""" & strTousuario & """>")
	Response.Write(vbCrLf & "<input type=""hidden"" name=""PmMessage"" value=""" & Request.Form("Message") & """>")

'If the message is blocked
ElseIf blnBlocked Then
	'Display an error message
	Response.Write("<span class=""lgText"">" & portal.variablesForum.strTxtYourPrivateMessage & " &amp;quot;" & strSubject & "&amp;quot;, " & portal.variablesForum.strTxtHasNotBeenSent & "</span>")
	Response.Write("<br /><br />" & portal.variablesForum.strTxtYouAreBlockedFromSendingPMsTo & " " & strTousuario & ".")
	Response.Write("<br /><br /><a href=""pm_welcome.aspx"">" & portal.variablesForum.strTxtReturnToYourPrivateMessenger & "</a>")

ElseIf blnMaxPMsOK = False Then
	'Display an error message
	Response.Write("<span class=""lgText"">" & portal.variablesForum.strTxtYourPrivateMessage & " &amp;quot;" & strSubject & "&amp;quot;, " & portal.variablesForum.strTxtHasNotBeenSent & "</span>")
	Response.Write("<br /><br />" & strTousuario & " " & portal.variablesForum.strTxtHasExceededMaxNumPPMs & ".")
	Response.Write("<br /><br /><a href=""pm_welcome.aspx"">" & portal.variablesForum.strTxtReturnToYourPrivateMessenger & "</a>")

'If there is no message body or subject display an error message
ElseIf blnNoSubject Then

	'Display an error message
	Response.Write("<span class=""lgText"">" & portal.variablesForum.strTxtYourPrivateMessage & " &amp;quot;" & strSubject & "&amp;quot;, " & portal.variablesForum.strTxtHasNotBeenSent & "</span>")
	Response.Write("<br /><br />" & portal.variablesForum.strTxtYourMessageNoValidSubjectHeading)
	Response.Write("<br /><br /><a href=""pm_new_message_form.aspx?code=edit"">" & portal.variablesForum.strTxtAmendYourPrivateMessage & "</a>")

	'Save the pm details so they can be edited
	Response.Write(vbCrLf & "<input type=""hidden"" name=""Subject"" value=""" & strSubject & """>")
	Response.Write(vbCrLf & "<input type=""hidden"" name=""Buddy"" value=""" & strTousuario & """>")
	Response.Write(vbCrLf & "<input type=""hidden"" name=""PmMessage"" value=""" & Request.Form("Message") & """>")

'Else display a message that the PM is sent
Else
	'Display a message to say the message is sent
	Response.Write("<span class=""lgText""><img src=""" & portal.variablesForum.strImagePath & "message_sent_icon.gif"" width=""32"" height=""32"" align=""middle"" /> " & portal.variablesForum.strTxtYourPrivateMessage & " &amp;quot;" & strSubject & "&amp;quot;, " & portal.variablesForum.strTxtHasBeenSentTo & " " & strTousuario & ".</span>")
	Response.Write("<br /><br /><a href=""pm_welcome.aspx"">" & portal.variablesForum.strTxtReturnToYourPrivateMessenger & "</a>")
End If
%></form>
 <br />
 </span>
 <br /><br />
 <br /><br /><br />
<%


'Release server objects
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing


'Display the process time
If blnShowProcessTime Then response.write("<span class=""smText""><br /><br />" & portal.variablesForum.strTxtThisPageWasGeneratedIn & " " & FormatNumber(Timer() - dblStartTime, 4) & " " & portal.variablesForum.strTxtSeconds & "</span>"
%>
 </div>
  
