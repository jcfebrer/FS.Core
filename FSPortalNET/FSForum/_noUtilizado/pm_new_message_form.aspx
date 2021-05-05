

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<!--#include file="language_files/pm_language_file_inc.aspx" -->
<!--#include file="functions/functions_edit_post.aspx" -->

<!--#include file="includes/emoticons_inc.aspx" -->
<%
'Set the buffer to true
Response.Buffer = True

'Make sure this page is not cached
Response.Expires = -1
Response.ExpiresAbsolute = Now() - 2
Response.AddHeader "pragma","no-cache"
Response.AddHeader "cache-control","private"
Response.CacheControl = "No-Store"

'Declare variables
Dim strMode 			'Holds the mode of the page
Dim strPostPage 		'Holds the page the form is posted to
Dim lngMessageID		'Holds the pm id
Dim strTopicSubject		'Holds the subject
Dim strBuddyName		'Holds the to usuario
Dim dtmReplyPMDate		'Holds the reply pm date
Dim strMessage			'Holds the post message
Dim intForumID			'Holds the forum number
Dim intIndexPosition		'Holds the idex poistion in the emiticon array
Dim intNumberOfOuterLoops	'Holds the outer loop number for rows
Dim intLoop			'Holds the loop index position
Dim intInnerLoop		'Holds the inner loop number for columns


'Set the mode of the page
strMode = "PM"
lngMessageID = 0


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


'If there is a person who to send to then read in there name
'This is encoded before being displayed for security
strBuddyName = Request.QueryString("name")



'If edit read in the detials
If Request.QueryString("code") = "edit" Then
	'Read in the details of the message to be edited
	strTopicSubject = Trim(Mid(Request.Form("subject"), 1, 41))
	strMessage = Request.Form("PmMessage")
	strBuddyName = Trim(Mid(Request.Form("Buddy"), 1, 15))
End If



'If this is a reply to a pm then get the details from the db
If Request.QueryString("code") = "reply" Then

	'Read in the pm mesage number to reply to
	lngMessageID = CLng(Request.QueryString("pm"))

	'Get the pm from the database

	'Initlise the sql statement
	strSQL = "SELECT " & portal.variablesForum.strDbTable & "PMMessage.*, " & "Usuarios.usuario "
	strSQL = strSQL & "FROM " & "Usuarios INNER JOIN " & portal.variablesForum.strDbTable & "PMMessage ON " & "Usuarios.UsuarioID = " & portal.variablesForum.strDbTable & "PMMessage.From_ID "
	strSQL = strSQL & "WHERE " & portal.variablesForum.strDbTable & "PMMessage.PM_ID=" & lngMessageID & " AND " & portal.variablesForum.strDbTable & "PMMessage.UsuarioID=" & portal.variablesForum.lngLoggedInUserID & ";"

	'Query the database
	rsCommon=db.execute(strSQL)

	'Read in the date of the reply pm
	dtmReplyPMDate = CDate(rsCommon("PM_Message_Date"))

	'Read in the usuario to be the pm is a reply to
	strBuddyName = rsCommon("usuario")

	'Set up the pm title
	strTopicSubject = Replace(rsCommon("PM_Tittle"), "RE: ", "")
	strTopicSubject = "RE: " & strTopicSubject

	'Build up the reply pm
	strMessage = "-- " & portal.variablesForum.strTxtPreviousPrivateMessage & " --"
	strMessage = strMessage & vbCrLf & "[B]" & portal.variablesForum.strTxtSentBy & " :[/B] " & strBuddyName
	strMessage = strMessage & vbCrLf & "[B]" & portal.variablesForum.strTxtSent & " :[/B] " & funcFecha.DateFormat(dtmReplyPMDate, funcFecha.saryDateTimeData) & " at " & funcFecha.TimeFormat(dtmReplyPMDate, funcFecha.saryDateTimeData) & vbCrLf & vbCrLf

	'Read in the pm from the recordset
	strMessage = strMessage & rsCommon("PM_Message")

	'Convert the orginal PM back to forum codes
	strMessage = EditPostConvertion (strMessage)

	'Place a couple of carridge returns infront of the reply pm
	strMessage = vbCrLf & vbCrLf & vbCrLf & strMessage

	'Close recordset
	rsCommon.Close
End If
%>
<html>
<head>

<title>Private Messenger: Send New Message</title>


     	

<!-- Check the from is filled in correctly before submitting -->
<script  language="javascript">

//Function to check form is filled in correctly before submitting
function CheckForm () {

	var errorMsg = "";

<%
'If Gecko Madis API (RTE) need to strip default input from the API
If RTEenabled = "Gecko" Then Response.Write("	//For Gecko Madis API (RTE)" & vbCrLf & "	if (document.frmAddMessage.message.value.indexOf('<br />') > -1 && document.frmAddMessage.message.value.length == 8) document.frmAddMessage.message.value = '';" & vbCrLf)

%>
	//Check for a member name
	if ((document.frmAddMessage.member.value=="") && (document.frmAddMessage.selectMember.value=="")){
		errorMsg += "\n\t<% = portal.variablesForum.strTxtNoTousuarioErrorMsg %>";
	}

	//Check for a subject
	if (document.frmAddMessage.subject.value==""){
		errorMsg += "\n\t<% = portal.variablesForum.strTxtNoPMSubjectErrorMsg %>";
	}

	//Check for message
	if (document.frmAddMessage.message.value==""){
		errorMsg += "\n\t<% = portal.variablesForum.strTxtNoPMErrorMsg %>";
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

	//Reset the submition action
	document.frmAddMessage.action = "pm_post_message.aspx"
	document.frmAddMessage.target = "_self";
<% 
If RTEenabled() <> "false" AND blnRTEEditor AND blnWYSIWYGEditor Then Response.Write(vbCrLf & "	frmAddMessage.Submit.disabled=true;")
%>

	return true;
}
</script>

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
  <td width="60%"><span class="lgText"><img src="<% = portal.variablesForum.strImagePath %>subject_folder.gif" alt="<% = portal.variablesForum.strTxtSubjectFolder %>" align="middle"> <% = portal.variablesForum.strTxtPrivateMessenger & ": " & portal.variablesForum.strTxtSendNewMessage %></span></td>
  <td align="right" width="40%" nowrap="nowarp"><a href="pm_inbox.aspx" target="_self"><img src="<% = portal.variablesForum.strImagePath %>inbox.gif" alt="<% = portal.variablesForum.strTxtPrivateMessenger & " " & portal.variablesForum.strTxtInbox %>" border="0"></a><a href="pm_outbox.aspx" target="_self"><img src="<% = portal.variablesForum.strImagePath %>outbox.gif" alt="<% = portal.variablesForum.strTxtPrivateMessenger & " " & portal.variablesForum.strTxtOutbox %>" border="0"></a><a href="pm_buddy_list.aspx" target="_self"><img src="<% = portal.variablesForum.strImagePath %>buddy_list.gif" alt="<% = portal.variablesForum.strTxtPrivateMessenger & " " & portal.variablesForum.strTxtBuddyList %>" border="0"></a><a href="pm_new_message_form.aspx" target="_self"><img src="<% = portal.variablesForum.strImagePath %>new_private_message.gif" alt="<% = portal.variablesForum.strTxtNewPrivateMessage %>" border="0"></a></td>
 </tr>
</table>
<%

'See if the users browser is RTE enabled
If RTEenabled() <> "false" AND blnRTEEditor = True AND blnWYSIWYGEditor = True Then

	'If we are editing the post then we need to pass the edited message to the Iframe
	If Request.QueryString("code") = "edit" Then Session("PmMessage") = strMessage

	'Open the message form for RTE enabled browsers
	%><!--#include file="includes/RTE_message_form_inc.aspx" --><%
Else
	'Open up the mesage form for non RTE enabled browsers
	%><!--#include file="includes/message_form_inc.aspx" --><%
End If

'Reset server variables
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing

%><br />
<div align="center">
<%


'Display the process time
If blnShowProcessTime Then response.write("<span class=""smText""><br /><br />" & portal.variablesForum.strTxtThisPageWasGeneratedIn & " " & FormatNumber(Timer() - dblStartTime, 4) & " " & portal.variablesForum.strTxtSeconds & "</span>"
%>
</div>

