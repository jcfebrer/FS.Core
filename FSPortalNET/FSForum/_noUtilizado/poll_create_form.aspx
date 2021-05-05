

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />

<!--#include file="functions/functions_edit_post.aspx" -->
<!--#include file="includes/emoticons_inc.aspx" -->
<%
'Set the response buffer to true as we maybe redirecting
Response.Buffer = True 

'Make sure this page is not cached
Response.Expires = -1
Response.ExpiresAbsolute = Now() - 2
Response.AddHeader "pragma","no-cache"
Response.AddHeader "cache-control","private"
Response.CacheControl = "No-Store"


'Dimension variables
Dim strMode			'Holds the mode of the page
Dim intForumID			'Holds the forum ID number
Dim lngTopicID			'Holds the Topic ID number
Dim lngMessageID		'Holds the Thread ID of the post
Dim strForumName		'Holds the name of the forum
Dim blnForumLocked		'Set to true if the forum is locked
Dim intTopicPriority		'Holds the priority of the topic
Dim strPostPage 		'Holds the page the form is posted to
Dim intRecordPositionPageNum	'Holds the recorset page number to show the Threads for
Dim strMessage			'Holds the post message
Dim intPollLoopCounter		'Holds the poll loop counter
Dim intIndexPosition		'Holds the index poistion in the emiticon array
Dim intNumberOfOuterLoops	'Holds the outer loop number for rows
Dim intLoop			'Holds the loop index position
Dim intInnerLoop		'Holds the inner loop number for columns


'If the user is user is using a banned IP redirect to an error page
If bannedIP() Then
	'Clean up
	Set rsCommon = Nothing
	adoCon.Close
	Set adoCon = Nothing
	
	'Redirect
	Response.Redirect("insufficient_permission.aspx?M=IP")

End If

'Intialise variables
lngTopicID = 0
lngMessageID = 0
intTopicPriority = 0
intRecordPositionPageNum = 1
strMode = "poll"


'Read in the forum and topic ID number and mode
portal.variablesForum.intForumID = CInt(Request.QueryString("FID"))


'Read in the forum name and forum permissions from the database
'Initalise the strSQL variable with an SQL statement to query the database
If portal.variablesForum.strDatabaseType = "SQLServer" Then
	strSQL = "EXECUTE " & portal.variablesForum.strDbProc & "ForumsAllWhereForumIs @portal.variablesForum.intForumID = " & portal.variablesForum.intForumID
Else
	strSQL = "SELECT " & portal.variablesForum.strDbTable & "Forum.* FROM " & portal.variablesForum.strDbTable & "Forum WHERE Forum_ID = " & portal.variablesForum.intForumID & ";"
End If

'Query the database
rsCommon=db.execute(strSQL)


'If there is a record returned by the recordset then check to see if you need a clave to enter it
If NOT rsCommon.EOF Then
	
	'Read in forum details from the database
	strForumName = rsCommon("Forum_name")
	
	'Read in wether the forum is locked or not
	blnForumLocked = CBool(rsCommon("Locked"))
	
	'Check the user is welcome in this forum
	Call forumPermisisons(portal.variablesForum.intForumID, portal.variablesForum.intGroupID, CInt(rsCommon("Read")), CInt(rsCommon("Post")), CInt(rsCommon("Reply_posts")), CInt(rsCommon("Edit_posts")), CInt(rsCommon("Delete_posts")), CInt(rsCommon("Priority_posts")), CInt(rsCommon("Poll_create")), CInt(rsCommon("Vote")), CInt(rsCommon("Attachments")), CInt(rsCommon("Image_upload")))
	
	'If the forum requires a clave and a logged in forum code is not found on the users machine then send them to a login page
	If NOT rsCommon("clave") = "" and NOT func.ValorCookie(Request.Cookies(portal.variables.strCookieName),"Forum" & portal.variablesForum.intForumID) = rsCommon("Forum_code") Then
		
		'Reset Server Objects
		rsCommon.Close
		Set rsCommon = Nothing 
		Set rsCommon = Nothing
		adoCon.Close
		Set adoCon = Nothing		
		
		'Redirect to a page asking for the user to enter the forum clave
		response.redirect("forum_clave_form.aspx?FID=" & portal.variablesForum.intForumID
	End If
End If

'Reset server object
rsCommon.Close

'If the forum level for the user on this forum is read only set the forum to be locked
If (portal.variablesForum.blnRead = False AND portal.variablesForum.blnModerator = False AND portal.variablesForum.blnAdmin = False) Then blnForumLocked = True

%>
<html> 
<head>

<title>Create New Poll</title>

<!-- The Web Wiz Guide ASP forum is written by Bruce Corkhill ©2001
    	 If you want your forum then goto http://www.webwizforums.com --> 

<!-- Check the from is filled in correctly before submitting -->
<script  language="javascript">
<!-- Hide from older browsem_rs...

//Function to check form is filled in correctly before submitting
function CheckForm () {
	
	var errorMsg = "";

<%
'If Gecko Madis API (RTE) need to strip default input from the API
If RTEenabled = "Gecko" Then Response.Write("	//For Gecko Madis API (RTE)" & vbCrLf & "	if (document.frmAddMessage.message.value.indexOf('<br />') > -1 && document.frmAddMessage.message.value.length == 8) document.frmAddMessage.message.value = '';" & vbCrLf)


'If this is a guest posting check that they have entered their name
If portal.variablesForum.blnPost And portal.variablesForum.lngLoggedInUserID = 2 Then
%>	
	//Check for a name
	if (document.frmAddMessage.Gname.value==""){
		errorMsg += "\n\t<% = portal.variablesForum.strTxtNoNameError %>";
	}
<%
End If	

%>
	//Check for a subject
	if (document.frmAddMessage.subject.value==""){
		errorMsg += "\n\t<% = portal.variablesForum.strTxtErrorTopicSubject %>";
	}
	
	//Check for poll question
	if (document.frmAddMessage.pollQuestion.value==""){
		errorMsg += "\n\t<% = portal.variablesForum.strTxtErrorPollQuestion %>";
	}
	
	//Check for poll at least two poll choices
	if ((document.frmAddMessage.choice1.value=="") || (document.frmAddMessage.choice2.value=="")){
		errorMsg += "\n\t<% = portal.variablesForum.strTxtErrorPollChoice %>";
	}
	
	//Check for message
	if (document.frmAddMessage.message.value==""){
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
	
	//Reset the submition page back to it's original place
	document.frmAddMessage.action = "post_message.aspx?PN=<% = CInt(Request.QueryString("PN")) %>"
	document.frmAddMessage.target = "_self";
<% 
If RTEenabled() <> "false" AND blnRTEEditor AND blnWYSIWYGEditor Then Response.Write(vbCrLf & "	document.frmAddMessage.Submit.disabled=true;")
%> 
	
	return true;
}
// -->
</script>

<!-- #include file="includes/header.aspx" -->
<navigation:navigation ID="common1" runat="server" />
<!--<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="3" align="center">
  <tr> 
  <td align="left" class="heading"><% = portal.variablesForum.strTxtCreateNewPoll %></td>
</tr>
 <tr> 
  <td align="left" width="71%" class="bold"><img src="<% = portal.variablesForum.strImagePath %>open_folder_icon.gif" border="0" align="middle">&nbsp;<a href="default.aspx" target="_self" class="boldLink"><% = strMainForumName %></a><% = strNavSpacer %><% Response.Write ("<a href=""forum_topics.aspx?FID=" & portal.variablesForum.intForumID & """ target=""_self"" class=""boldLink"">" & strForumName & "</a>" & strNavSpacer) %><% = portal.variablesForum.strTxtCreateNewPoll %><br /></td>
  </tr>
</table><br />-->

<%


'Clean up
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing

 
 
'If the user has logged in and allowed to create polls then display the from to allow the user to post a new message
If portal.variablesForum.blnPollCreate = True AND blnActiveMember = True AND (blnForumLocked = False OR portal.variablesForum.blnAdmin = True) Then

	'See if the users browser is RTE enabled
	If RTEenabled() <> "false" AND blnRTEEditor = True AND blnWYSIWYGEditor = True Then
					
		'Open the message form for RTE enabled browsers
		%><!--#include file="includes/RTE_message_form_inc.aspx" --><%
	Else
		'Open up the mesage form for non RTE enabled browsers
		%><!--#include file="includes/message_form_inc.aspx" --><%
	End If

'If the users account is suspended then let them know
ElseIf blnActiveMember = False Then
		
	Response.Write (vbCrLf & "<div align=""center""><br /><br /><span class=""text"">")
	
	'If mem suspended display message
	If  InStr(1, strLoggedInUserCode, "N0act", vbTextCompare) Then
		Response.Write(portal.variablesForum.strTxtForumMemberSuspended)
	'Else account not yet active
	Else
		Response.Write("<span class=""lgText"">" & portal.variablesForum.strTxtForumMembershipNotAct & "</span><br /><br />" & portal.variablesForum.strTxtToActivateYourForumMem)
	End If
	'If email is on then place a re-send activation email link
	If InStr(1, strLoggedInUserCode, "N0act", vbTextCompare) = False AND portal.variablesForum.blnEmailActivation AND portal.variablesForum.blnLoggedInUserEmail Then Response.Write("<br /><br /><a href=""JavaScript:openWin('resend_email_activation.aspx','actMail','toolbar=0,location=0,status=0,menubar=0,scrollbars=1,resizable=1,width=475,height=200')"">" & portal.variablesForum.strTxtResendActivationEmail & "</a>")
	
	Response.Write("</span><br /><br /><br /><br /></div>")

'Else if the forum is locked display a message telling the user so
ElseIf blnForumLocked = True Then
	
	Response.Write vbCrLf & "<div align=""center""><br /><br /><span class=""text"">" & portal.variablesForum.strTxtForumLockedByAdmim & "</span><br /><br /><br /><br /><br /></div>"

'Else if the user does not have permision to create polls
ElseIf portal.variablesForum.blnPollCreate = False AND strMode <> "poll" Then
	
	Response.Write vbCrLf & "<div align=""center""><br /><br /><span class=""text"">" & portal.variablesForum.strTxtSorryYouDoNotHavePermissionToPostInTisForum & "</span><br /><br /><br /><br /><br /></div>"

'Else the user is not logged in so let them know to login before they can post a message
Else
	Response.Write vbCrLf & "<div align=""center""><br /><br /><span class=""text"">" & portal.variablesForum.strTxtMustBeRegisteredToPost & "</span><br /><br />"
	Response.Write vbCrLf & "<a href=""registration_rules.aspx?FID=" & portal.variablesForum.intForumID & """ target=""_self""><img src=""" & portal.variablesForum.strImagePath & "register.gif""  alt=""" & portal.variablesForum.strTxtRegister & """ border=""0"" align=""middle"" /></a>&nbsp;&nbsp;<a href=""login_user.aspx?FID=" & portal.variablesForum.intForumID & """ target=""_self""><img src=""" & portal.variablesForum.strImagePath & "login.gif""  alt=""" & portal.variablesForum.strTxtLogin & """ border=""0"" align=""middle"" /></a><br /><br /><br /><br /></div>"
End If
%>
<br />
<div align="center">
<% 

'Display the process time
If blnShowProcessTime Then response.write("<span class=""smText""><br /><br />" & portal.variablesForum.strTxtThisPageWasGeneratedIn & " " & FormatNumber(Timer() - dblStartTime, 4) & " " & portal.variablesForum.strTxtSeconds & "</span>"
%>

